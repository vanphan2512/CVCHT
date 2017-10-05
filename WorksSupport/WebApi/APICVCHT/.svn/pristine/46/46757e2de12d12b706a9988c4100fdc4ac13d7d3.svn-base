

namespace Library.Common
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion
    /// <summary>
    /// Create By: Trần Cao Vỹ
    /// Date:03/11/2014
    /// Des: Hàm phân trang theo LinQ
    /// </summary>
    public static class PagingExtensions
    {
        public static IQueryable<TSource> Page<TSource>(IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
