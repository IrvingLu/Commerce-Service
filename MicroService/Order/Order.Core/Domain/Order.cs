/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Core.Domain
/// 文件名称    ：Order.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/11/30 9:30:05 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using Order.Core.Enums;
using Shared.Domain.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Core.Domain
{
    public class Order : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string OrderNum { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}
