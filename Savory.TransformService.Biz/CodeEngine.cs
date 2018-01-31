using Microsoft.VisualStudio.TextTemplating;
using Newtonsoft.Json;
using Savory.TransformService.Biz.Response;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Biz
{
    public class CodeEngine
    {
        public EngineResult ToCode(string template, string extension, string itemJsonValue)
        {
            if (string.IsNullOrEmpty(template))
            {
                return new EngineResult { EngineStatus = EngineStatus.TemplateBodyRequired };
            }

            string className = "className";
            string classNamespace = "classNamespace";

            Engine engine = new Engine();

            TemplateHost host = new TemplateHost();

            if (!string.IsNullOrEmpty(extension))
            {
                template += "<#+" + extension + "#>";
            }
            else
            {
                template += "<#+ public dynamic Item { get; set; } #>";
            }

            string language = null;
            string[] references = null;
            var sourceCode = engine.PreprocessTemplate(template, host, className, classNamespace, out language, out references);

            CompilerParameters parameters = new CompilerParameters();

            parameters.CompilerOptions = "/target:library /optimize";

            parameters.GenerateInMemory = true;

            parameters.IncludeDebugInformation = false;

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");

            var compiler = CodeDomProvider.CreateProvider("CSharp");

            CompilerResults compilerResults = compiler.CompileAssemblyFromSource(parameters, new string[] { sourceCode });
            if (compilerResults.Errors.HasErrors)
            {
                string errorJson = JsonConvert.SerializeObject(compilerResults.Errors);

                return new EngineResult { EngineStatus = EngineStatus.CompileError, CompileErrors = errorJson };
            }

            var assembly = compilerResults.CompiledAssembly;

            var instance = assembly.CreateInstance(string.Format("{0}.{1}", classNamespace, className));

            var type = instance.GetType();
            var itemProperty = type.GetProperty("Item");
            if (itemProperty != null && !string.IsNullOrEmpty(itemJsonValue))
            {
                var item = JsonConvert.DeserializeObject(itemJsonValue, itemProperty.PropertyType);

                itemProperty.SetValue(instance, item);
            }

            var method = type.GetMethod("TransformText");

            try
            {
                var output = method.Invoke(instance, null) as string;

                return new EngineResult { EngineStatus = EngineStatus.Success, Output = output };
            }
            catch (Exception ex)
            {
                return new EngineResult { EngineStatus = EngineStatus.RunException, RunException = ex.ToString() };
            }
        }
    }
}
