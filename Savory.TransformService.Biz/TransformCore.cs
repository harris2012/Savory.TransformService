using Newtonsoft.Json;
using Savory.Repository.TransformDB.Entity;
using Savory.TransformService.Biz.Response;
using Savory.TransformService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Biz
{
    public class TransformCore
    {
        public TransformResult Transofm(string name, string version, string itemJsonValue)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new TransformResult { TransformStatus = TransformStatus.NameRequired };
            }

            if (string.IsNullOrEmpty(version))
            {
                return new TransformResult { TransformStatus = TransformStatus.VersionRequired };
            }

            TemplateEntity templateEntity = null;
            using (var context = new SavoryTransformDBContext())
            {
                templateEntity = context.Template.FirstOrDefault(v => v.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && v.Version.Equals(version, StringComparison.OrdinalIgnoreCase));
            }

            if (templateEntity == null)
            {
                return new TransformResult { TransformStatus = TransformStatus.TemplateNotFound };
            }

            CodeEngine engine = new CodeEngine();

            var engineResult = engine.ToCode(templateEntity.Body, templateEntity.Extension, itemJsonValue);

            return ToTransformResult(engineResult);
        }

        public PreviewResult Preview(string templateBody, string extension, string itemJsonValue)
        {
            if (string.IsNullOrEmpty(templateBody))
            {
                return new PreviewResult { PreviewStatus = PreviewStatus.TemplateBodyRequired };
            }

            CodeEngine engine = new CodeEngine();

            var engineResult = engine.ToCode(templateBody, extension, itemJsonValue);

            return ToPreviewResult(engineResult);
        }

        private PreviewResult ToPreviewResult(EngineResult engineResult)
        {
            PreviewResult previewResult = new PreviewResult();

            previewResult.PreviewStatus = PreviewStatus.Success;
            previewResult.EngineStatus = engineResult.EngineStatus;
            previewResult.Output = engineResult.Output;

            if (engineResult.EngineStatus != EngineStatus.Success)
            {
                previewResult.PreviewStatus = PreviewStatus.EngineSaysNo;
                previewResult.EngineCompileErrors = engineResult.CompileErrors;
                previewResult.EngineRunException = engineResult.RunException;
            }

            return previewResult;
        }

        private TransformResult ToTransformResult(EngineResult engineResult)
        {
            TransformResult transformResult = new TransformResult();

            transformResult.TransformStatus = TransformStatus.Success;
            transformResult.EngineStatus = engineResult.EngineStatus;
            transformResult.Output = engineResult.Output;

            if (engineResult.EngineStatus != EngineStatus.Success)
            {
                transformResult.TransformStatus = TransformStatus.EngineSaysNo;
                transformResult.EngineCompileErrors = engineResult.CompileErrors;
                transformResult.EngineRunException = engineResult.RunException;
            }

            return transformResult;
        }
    }
}
