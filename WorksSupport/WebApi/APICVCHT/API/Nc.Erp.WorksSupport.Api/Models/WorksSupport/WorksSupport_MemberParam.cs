using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nc.Erp.WorksSupport.Api.Models.WorksSupport
{
    public class WorksSupport_MemberParam
    {
        public int WorksSupportId { get; set; }
        public DateTime WorksSupportDate { get; set; }
        public string MemberUserName { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public string Note { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}