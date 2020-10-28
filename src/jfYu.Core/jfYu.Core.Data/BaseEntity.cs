using System;
using System.ComponentModel;

namespace jfYu.Core.Data
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        public Guid Guid { get; set; }

        /// <summary>
        /// 0禁用 1正常
        /// </summary>
        [DisplayName("状态")]
        public int State { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>

        [DisplayName("修改时间")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

    }
}
