using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Biz.Response
{
    public class EngineResult
    {
        /// <summary>
        /// 引擎处理结果状态码
        /// </summary>
        public EngineStatus EngineStatus { get; set; }

        /// <summary>
        /// 编译错误
        /// </summary>
        public string CompileErrors { get; set; }

        /// <summary>
        /// 运行异常
        /// </summary>
        public string RunException { get; set; }

        /// <summary>
        /// 输出
        /// </summary>
        public string Output { get; set; }
    }
}
