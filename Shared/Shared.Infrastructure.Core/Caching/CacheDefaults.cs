/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Infrastructure.Core.Caching
/// 文件名称    ：CacheDefaults.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/26 15:14:18 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************


namespace Shared.Infrastructure.Core.Caching
{
    public static class CacheDefaults
    {

        /// <summary>
        /// 缓存默认时间
        /// </summary>
        public static int CacheTime => 86400;
        /// <summary>
        /// 键名
        /// </summary>
        public static string CacheKey => "Cache";
    }
}
