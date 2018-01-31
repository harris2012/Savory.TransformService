using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Biz.Response
{
    public class TransformResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public TransformStatus TransformStatus { get; set; }

        /// <summary>
        /// 引擎状态
        /// </summary>
        public EngineStatus EngineStatus { get; set; }

        /// <summary>
        /// 引擎编译错误
        /// </summary>
        public string EngineCompileErrors { get; set; }

        /// <summary>
        /// 引擎运行异常
        /// </summary>
        public string EngineRunException { get; set; }

        /// <summary>
        /// 输出文本
        /// </summary>
        public string Output { get; set; }
    }
}
