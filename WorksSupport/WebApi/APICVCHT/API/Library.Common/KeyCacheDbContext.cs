using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace Library.Common
{
    /// <summary>
    /// Create by: Trần Cao Vỹ
    /// Date: 01.11.2014
    /// Des: KeyCacheDbContext của keyCache 
    /// </summary>
    public static class KeyCacheDbContext
    {
        private const string HTTPCONTEXTKEY = "Session.Base.HttpContext.Key";
        private static readonly Hashtable _threads = new Hashtable();
        public static IRepositoryKeyCache Current
        {
            get
            {
                return GetOrCreateSession();
            }
        }
        public static bool IsOpen
        {
            get
            {
                IRepositoryKeyCache session = GetSession();
                return (session != null);
            }
        }

        private static IRepositoryKeyCache GetOrCreateSession()
        {
            IRepositoryKeyCache session = GetSession();
            if (session == null)
            {
                session = ObjectFactory.GetInstance<IRepositoryKeyCache>();

                SaveSession(session);
            }

            return session;
        }
        private static IRepositoryKeyCache GetSession()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(HTTPCONTEXTKEY))
                {
                    return (IRepositoryKeyCache)HttpContext.Current.Items[HTTPCONTEXTKEY];
                }

                return null;
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    thread.Name = Guid.NewGuid().ToString();
                    return null;
                }
                else
                {
                    lock (_threads.SyncRoot)
                    {
                        return (IRepositoryKeyCache)_threads[Thread.CurrentThread.Name];
                    }
                }
            }
        }
        private static void SaveSession(IRepositoryKeyCache session)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[HTTPCONTEXTKEY] = session;
            }
            else
            {
                lock (_threads.SyncRoot)
                {
                    _threads[Thread.CurrentThread.Name] = session;
                }
            }
        }
    }
}
