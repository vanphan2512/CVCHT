﻿using System;

namespace Nc.Erp.WorksSupport.Do.Configuration
{
    /// <summary>
    /// Created by      : Truong Hoang Nhi
    /// Created date    : 25/05/2017
    /// Đối tượng ERP.EO.WORKSSUPPORTPRIORITY.
    /// </summary>
    public class WorksSupportPriority
    {
        public int WorksSupportPriorityId { get; set; }

        public string WorksSupportPriorityName { get; set; }

        public string IconUrl { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }

        public int OrderIndex { get; set; }

        public bool IsActived { get; set; }

        public bool IsSystem { get; set; }

        public string CreatedUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedUser { get; set; }

        public DateTime? DeletedDate { get; set; }
        
        //#endregion

        //#region Column Names

        public const string colWorksSupportPriorityID = "WorksSupportPriorityID";
        //public const String colWorksSupportPriorityName = "WorkSPriorityName";
        //public const String colIconUrl = "IconUrl";
        //public const String colDescription = "Description";
        //public const String colOrderIndex = "OrderIndex";
        //public const String colIsActive = "IsActive";
        //public const String colIsSystem = "IsSystem";
        //public const String colCreatedUser = "CreatedUser";
        //public const String colCreatedDate = "CreatedDate";
        //public const String colUpdatedUser = "UpdatedUser";
        //public const String colUpdatedDate = "UpdatedDate";
        //public const String colIsDeleted = "IsDeleted";
        public const string colDeletedUser = "DeletedUser";
        //public const String colDeletedDate = "DeletedDate";

        //#endregion //Column names

    }
}