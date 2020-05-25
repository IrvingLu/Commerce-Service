/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Infrastructure.Core.MongoDb.Models
/// 文件名称    ：MongodbHost.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/26 15:54:12 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Infrastructure.Core.MongoDb.Models
{
   public class MongodbHost
    {
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// 表明
        /// </summary>
        public string Table { get; set; }
    }
}
