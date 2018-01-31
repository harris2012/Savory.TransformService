using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Biz.Response
{
    /// <summary>
    /// 引擎状态码
    /// </summary>
    public enum EngineStatus
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 模版没有传模版
        /// </summary>
        TemplateBodyRequired = 101,

        /// <summary>
        /// 编译出错
        /// </summary>
        CompileError = 201,

        /// <summary>
        /// 运行时发生异常
        /// </summary>
        RunException = 202
    }
}
