using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc.Erp.WorksSupport.Do.Configuration.Priority
{
    /// <summary>
    /// Created by      : Lương Trung Nhân
    /// Created date    : 29/05/2017
    /// Đối tượng ERP.EO.WORKSSUPPORTSTATUS
    /// </summary>
    public class WorkSupportStatus
    {
        public int WorksSupportStatusId { get; set; }
        public string WorksSupportStatusName { get; set; }
        public string IconURL { get; set; }
        public bool IsInitStatus { get; set; }
        public bool IsCompleteStatus { get; set; }
        public bool IsCloseStatus { get; set; }
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

        public const string colWorksSupportStatusId = "WORKSSUPPORTSTATUSID";
        public const string colWorksSupportStatusName = "WORKSSUPPORTSTATUSNAME";
        public const string colIconURL = "ICONURL";
        public const string colIsInitStatus = "ISINITSTATUS";
        public const string colIsCompleteStatus = "ISCOMPLETESTATUS";
        public const string colIsCloseStatus = "ISCLOSESTATUS";
        public const string colDescription = "DESCRIPTION";
        public const string colOrderIndex = "ORDERINDEX";
        public const string colIsActived = "ISACTIVE";
        public const string colIsSystem = "ISSYSTEM";
        public const string colCreatedUser = "CREATEDUSER";
        public const string colCreatedDate = "CREATEDDATE";
        public const string colUpdatedUser = "UPDATEDUSER";
        public const string colUpdatedDate = "UPDATEDDATE";
        public const string colIsDeleted = "ISDELETED";
        public const string colDeletedUser = "DELETEDUSER";
        public const string colDeletedDate = "DELETEDDATE"; 
    }
}
