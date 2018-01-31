using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Biz
{
    [Serializable]
    public class TemplateHost : ITextTemplatingEngineHost
    {
        public CompilerErrorCollection CompilerErrorCollection { get; private set; }

        /// <summary>
        /// 模版需调用的其他程序集引用
        /// </summary>
        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 模版调用标准程序集引用
        /// </summary>
        public IList<string> StandardImports
        {
            get
            {
                return new List<string> { "System", "System.Text", "System.Collections.Generic" };
            }
        }

        /// <summary>
        /// 模版文件
        /// 会包含在生成的文件中
        /// </summary>
        public string TemplateFile
        {
            get
            {
                return @"CodeTemplate.tt";
            }
        }

        public object GetHostOption(string optionName)
        {
            return optionName == "CacheAssemblies";
        }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = string.Empty;
            location = string.Empty;

            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }
            return false;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            this.CompilerErrorCollection = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CreateDomain("Generation App Domain");
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            Console.WriteLine("assemblyReference: {0}", assemblyReference);

            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }
            string path = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);
            if (File.Exists(path))
            {
                return path;
            }
            return string.Empty;
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            throw new NotImplementedException();
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }
            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }
            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }
            return string.Empty;
        }

        public string ResolvePath(string path)
        {
            //if (fileName == null)
            //{
            //    throw new ArgumentNullException("the file name cannot be null");
            //}
            //if (!File.Exists(fileName))
            //{
            //    string path = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);
            //    if (File.Exists(path))
            //    {
            //        return path;
            //    }
            //}
            //return fileName;
            throw new NotImplementedException();
        }

        public void SetFileExtension(string extension)
        {
            //this._fileExtensionValue = extension;
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            //this._fileEncodingValue = encoding;
        }
    }
}
