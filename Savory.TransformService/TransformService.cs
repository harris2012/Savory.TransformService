using Savory.TransformService.Biz;
using Savory.TransformService.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Savory.TransformService.Contract.Response;

namespace Savory.TransformService
{
    public class TransformService : ServiceBase, ITransformService
    {
        ServiceHost host = null;

        #region ServiceBase Members
        protected override void OnStart(string[] args)
        {
            try
            {
                host = new ServiceHost(this.GetType());

                host.Open();
            }
            catch (Exception ex)
            {
                string fileName = string.Format("OnStartException_{0:yyyyMMddHHmmss}", DateTime.Now);

                File.WriteAllText(fileName, ex.ToString());
            }
        }

        protected override void OnStop()
        {
            try
            {
                host.Close();
            }
            catch (Exception ex)
            {
                string fileName = string.Format("OnStopException_{0:yyyyMMddHHmmss}", DateTime.Now);

                File.WriteAllText(fileName, ex.ToString());
            }
        }
        #endregion

        #region ITitanService Members

        public PreviewResult Preview(string templateBody, string extension, string itemJsonValue)
        {
            TransformCore core = new TransformCore();

            var previewResult = core.Preview(templateBody, extension, itemJsonValue);

            PreviewResult returnValue = new PreviewResult();
            returnValue.PreviewStatus = (int)ToVo(previewResult.PreviewStatus);
            returnValue.EngineStatus = (int)ToVo(previewResult.EngineStatus);
            returnValue.EngineCompileErrors = previewResult.EngineCompileErrors;
            returnValue.EngineRunException = previewResult.EngineRunException;
            returnValue.Output = previewResult.Output;

            return returnValue;
        }

        public TransformResult Transform(string name, string version)
        {
            return Transform(name, version, null);
        }

        public TransformResult Transform(string name, string version, string itemJsonValue)
        {
            TransformCore core = new TransformCore();

            var transformResult = core.Transofm(name, version, itemJsonValue);

            TransformResult returnValue = new TransformResult();
            returnValue.TransformStatus = (int)ToVo(transformResult.TransformStatus);
            returnValue.EngineStatus = (int)ToVo(transformResult.EngineStatus);
            returnValue.EngineCompileErrors = transformResult.EngineCompileErrors;
            returnValue.EngineRunException = transformResult.EngineRunException;
            returnValue.Output = transformResult.Output;

            return returnValue;
        }
        #endregion

        private PreviewStatus ToVo(Biz.Response.PreviewStatus status)
        {
            switch (status)
            {
                case Biz.Response.PreviewStatus.Success:
                    return PreviewStatus.Success;

                case Biz.Response.PreviewStatus.TemplateBodyRequired:
                    return PreviewStatus.TemplateBodyRequired;

                case Biz.Response.PreviewStatus.EngineSaysNo:
                    return PreviewStatus.EngineSaysNo;

                case Biz.Response.PreviewStatus.None:
                default:
                    return PreviewStatus.None;
            }
        }

        private TransformStatus ToVo(Biz.Response.TransformStatus status)
        {
            switch (status)
            {
                case Biz.Response.TransformStatus.Success:
                    return TransformStatus.Success;

                case Biz.Response.TransformStatus.NameRequired:
                    return TransformStatus.NameRequired;

                case Biz.Response.TransformStatus.VersionRequired:
                    return TransformStatus.VersionRequired;

                case Biz.Response.TransformStatus.TemplateNotFound:
                    return TransformStatus.TemplateNotFound;

                case Biz.Response.TransformStatus.EngineSaysNo:
                    return TransformStatus.EngineSaysNo;

                case Biz.Response.TransformStatus.None:
                default:
                    return TransformStatus.None;
            }
        }

        private EngineStatus ToVo(Biz.Response.EngineStatus status)
        {
            switch (status)
            {
                case Biz.Response.EngineStatus.Success:
                    return EngineStatus.Success;

                case Biz.Response.EngineStatus.TemplateBodyRequired:
                    return EngineStatus.TemplateBodyRequired;

                case Biz.Response.EngineStatus.CompileError:
                    return EngineStatus.CompileError;

                case Biz.Response.EngineStatus.RunException:
                    return EngineStatus.RunException;

                case Biz.Response.EngineStatus.None:
                default:
                    return EngineStatus.None;
            }
        }
    }
}
