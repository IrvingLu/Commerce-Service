/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Data.Dapper
/// 文件名称    ：SortingExtensions.cs
/// =================================
/// 创 建 者    ：xinqy
/// 创建日期    ：2019/12/4 13:54:05 
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DapperExtensions;
using JetBrains.Annotations;
using Order.Core.BaseModel;

namespace Order.Data.Dapper
{
    internal static class DapperExtensions
    {
        [NotNull]
        public static List<ISort> ToSortable<T>([NotNull] this Expression<Func<T, object>>[] sortingExpression, bool ascending = true)
        {
            if (sortingExpression==null|| !sortingExpression.Any())
            {
                var parameterName = nameof(sortingExpression);
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }
            var sortList = new List<ISort>();
            sortingExpression.ToList().ForEach(sortExpression =>
            {
                MemberInfo sortProperty = ReflectionHelper.GetProperty(sortExpression);
                sortList.Add(new Sort { Ascending = ascending, PropertyName = sortProperty.Name });
            });

            return sortList;
        }

        [NotNull]
        public static IPredicate ToPredicateGroup<TEntity, TPrimaryKey>([NotNull] this Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity<TPrimaryKey>
        {
            if (expression == null)
            {
                var parameterName = nameof(expression);
                throw new ArgumentNullException(parameterName);
            }
            var dev = new DapperExpressionVisitor<TEntity, TPrimaryKey>();
            IPredicate pg = dev.Process(expression);

            return pg;
        }
    }
}
