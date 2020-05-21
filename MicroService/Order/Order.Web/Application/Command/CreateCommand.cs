﻿/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Service.Dto
/// 文件名称    ：OrderCreateDto.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/11/30 11:10:34 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************


using MediatR;
using Shared.Domain.Abstractions;
using System;

namespace Order.Web.Application.Command
{
    public class CreateCommand : Entity<Guid>, IRequest 
    {
        public string OrderNum { get; set; }
    }
}
