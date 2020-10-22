using System.Collections.Generic;

namespace jfYu.Core.Common
{
    public class AjaxResult
    {
        /// <summary>
        /// 状态码,默认200成功
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        ///返回字符串
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 返回错误集合
        /// </summary>
        public List<KeyValuePair<string, string>> Errors { get; set; } = new List<KeyValuePair<string, string>>();
    }
}
