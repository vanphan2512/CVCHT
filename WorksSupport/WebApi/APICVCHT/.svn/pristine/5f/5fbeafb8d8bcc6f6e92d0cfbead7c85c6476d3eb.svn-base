
#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
#endregion

namespace Library.Common
{

    /// <summary>
    /// Create by: Trần Cao Vỹ
    /// Date: 01.11.2014
    /// Des: KeyCacheRepository 
    /// </summary>
    public class KeyCacheRepository : IRepositoryKeyCache
    {
        private string dBName = "ChatDB";
        private string IpServerMongo = "mongodb://trancaovy:12345678@11.0.1.4:27017/ChatDB";
        private string CollectionName = string.Empty;

        public KeyCacheRepository()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["DBName"] != null)
                this.dBName = System.Configuration.ConfigurationManager.AppSettings["DBName"];
            if (System.Configuration.ConfigurationManager.AppSettings["LinkServer"] != null)
                this.IpServerMongo = System.Configuration.ConfigurationManager.AppSettings["LinkServer"];
        }

        public MongoClient Provider
        {
            get { return new MongoClient(IpServerMongo); }
        }

        public MongoServer _sv { get { return this.Provider.GetServer(); } }

        public MongoDatabase _db { get { return this._sv.GetDatabase(dBName); } }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class,new()
        {
            var Sel = _db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(expression);
            var query = ((MongoQueryable<T>)Sel).GetMongoQuery();
            _db.GetCollection<T>(typeof(T).Name).Remove(query);
        }

        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression, T item) where T : class, new()
        {
            var Sel = _db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(expression);
            var query = ((MongoQueryable<T>)Sel).GetMongoQuery();
            _db.GetCollection<T>(typeof(T).Name).Remove(query);

        }

        public IQueryable<T> All<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression, int page, int pageSize) where T : class, new()
        {
            if (expression == null)
            {
                return PagingExtensions.Page(All<T>(), page, pageSize);
            }
            else
            {
                return PagingExtensions.Page(All<T>(expression), page, pageSize);
            }

        }

        public int Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            if (expression != null)
            {
                var Sel = _db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(expression);

                return Sel != null ? Sel.Count() : 0;
            }
            else
            {
                var Sel = _db.GetCollection<T>(typeof(T).Name).AsQueryable<T>();
                return Sel != null ? Sel.Count() : 0;
            }

        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return _db.GetCollection<T>(typeof(T).Name).FindAll().AsQueryable();
        }

        public IQueryable<T> All<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            var Sel = _db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(expression);
            var query = ((MongoQueryable<T>)Sel).GetMongoQuery();
            return _db.GetCollection<T>(typeof(T).Name).Find(query).AsQueryable();
        }

        //Lấy danh sách ClientNotification đã sắp xếp theo CreatedDate --- 22/06/2016---
        public IQueryable<T> AllBeta<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression, System.Linq.Expressions.Expression<Func<T, DateTime?>> orderby, int page, int pageSize) where T : class, new()
        {
            if (expression == null)
            {
                if (orderby == null)
                    return PagingExtensions.Page(All<T>(), page, pageSize);
                else return PagingExtensions.Page(AllBeta<T>(orderby), page, pageSize);
            }
            else
            {
                if (orderby == null)
                    return PagingExtensions.Page(All<T>(expression), page, pageSize);
                else return PagingExtensions.Page(AllBeta<T>(expression, orderby), page, pageSize);
            }

        }
        public IQueryable<T> AllBeta<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression, System.Linq.Expressions.Expression<Func<T, DateTime?>> orderby) where T : class, new()
        {
            var Sel = _db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(expression);
            var query = ((MongoQueryable<T>)Sel).GetMongoQuery();
            return _db.GetCollection<T>(typeof(T).Name).Find(query).AsQueryable().OrderByDescending(orderby);
        }
        public IQueryable<T> AllBeta<T>(System.Linq.Expressions.Expression<Func<T, DateTime?>> orderby) where T : class, new()
        {
            return _db.GetCollection<T>(typeof(T).Name).FindAll().AsQueryable().OrderByDescending(orderby);
        }
        //------------------------------End--------------------------------------------

        public void Add<T>(T items) where T : class, new()
        {
            _db.GetCollection<T>(GetClassName<T>(items)).Save(items);
        }

        public void AddNotExitBy<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression, T items) where T : class, new()
        {
            var exit = _db.GetCollection<T>(typeof(T).Name).AsQueryable<T>().Where(expression);
            if (exit.Count() == 0)
                _db.GetCollection<T>(GetClassName<T>(items)).Save(items);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public string GetClassName<T>(T items)
        {
            return items.GetType().Name;
        }

        public void Dispose()
        {
            _sv.Disconnect();

        }

    }
}
