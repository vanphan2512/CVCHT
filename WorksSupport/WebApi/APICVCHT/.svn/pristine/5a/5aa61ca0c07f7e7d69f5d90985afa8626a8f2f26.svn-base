
namespace Library.Common
{

    #region using
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;
    #endregion
    /// <summary>
    /// Create by: Trần Cao Vỹ
    /// Date: 01/11/2014
    /// Des: đối tượng quản lý keyCache
    /// </summary>
    public class ManagerKeyCache
    {
        public ManagerKeyCache()
        {
            this.createDate = DateTime.Now;
        }
        public ManagerKeyCache(string strProjectName, int inttimeCahe, string strServerCacheIP)
        {
            this.projectName = strProjectName;
            this.timeCahe = inttimeCahe;
            this.serverCacheIP = strServerCacheIP;
        }

        private string keyCacheName = string.Empty;
        private DateTime createDate;
        private string projectName = string.Empty;
        private int timeCahe = 0;
        private string serverCacheIP = string.Empty;
        public ObjectId Id { get; set; }
        
        public string KeyCacheName
        {
            get { return keyCacheName; }
            set { keyCacheName = value; }
        }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate
        {
            get { return DateTime.Now; }
        }
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        public int TimeCahe
        {
            get { return timeCahe; }
            set { timeCahe = value; }
        }
        public string ServerCacheIP
        {
            get { return serverCacheIP; }
            set { serverCacheIP = value; }
        }
    }
}
