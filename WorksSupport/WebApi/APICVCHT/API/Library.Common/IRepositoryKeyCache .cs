

namespace Library.Common
{

    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    #endregion
    /// <summary>
    /// Create by: Trần Cao Vỹ
    /// Date: 01.11.2014
    /// Des: interface 
    /// </summary>
    public interface IRepositoryKeyCache : IDisposable
    {
        void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
        
        void Delete<T>(Expression<Func<T, bool>> expression, T item) where T : class,new();
        
        T Single<T>(Expression<Func<T, bool>> expression) where T : class,new();
        
        System.Linq.IQueryable<T> All<T>() where T : class,new();

        System.Linq.IQueryable<T> All<T>(Expression<Func<T, bool>> expression) where T : class,new();
        
        System.Linq.IQueryable<T> All<T>(Expression<Func<T, bool>> expression,int pageIndex, int pageSize) where T : class,new();

        //-----Thêm interface cho hàm ALlBeta để lấy tất cả T có orderby--- 22/06/2016 ---
        System.Linq.IQueryable<T> AllBeta<T>(Expression<Func<T, bool>> expression, Expression<Func<T, DateTime?>> orderby, int pageIndex, int pageSize) where T : class,new();
        //---------------------End-----------------------------------------------

        void Add<T>(T item) where T : class,new();
        
        void Add<T>(IEnumerable<T> item) where T : class,new();
        
        void AddNotExitBy<T>(Expression<Func<T, bool>> expression, T item) where T : class,new();

        int Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new();

    }
}
