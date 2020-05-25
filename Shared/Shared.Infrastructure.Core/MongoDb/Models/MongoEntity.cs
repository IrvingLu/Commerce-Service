

using MongoDB.Bson;
/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Infrastructure.Core.MongoDb.Models
/// 文件名称    ：MongoEntity.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/26 16:01:15 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************
namespace Shared.Infrastructure.Core.MongoDb.Models
{
    public abstract class MongoEntity
    {
        /// <summary>
        /// BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId
        /// </summary>
        public ObjectId Id { get; set; }
        /// <summary>
        /// 返回给上传者的自编主键
        /// </summary>
        public string GuidID { get; set; }

    }
}
