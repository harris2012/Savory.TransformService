using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.TransformService.Biz.Response
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum TransformStatus
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 0,

        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 操作失败
        /// </summary>
        NameRequired = 101,

        /// <summary>
        /// 版本号必传
        /// </summary>
        VersionRequired = 102,

        /// <summary>
        /// 模版没有找到
        /// </summary>
        TemplateNotFound = 103,

        /// <summary>
        /// 引擎处理失败
        /// </summary>
        EngineSaysNo = 201
    }
}
