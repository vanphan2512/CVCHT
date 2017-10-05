using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nc.Erp.WorksSupport.Api.Models.WorksSupport
{
    public class WorksSupport_CommentParam
    {
        public decimal decCommentID { get; set; }
        public decimal decWorksSupportID { get; set; }
        public DateTime? dtmCommentDate { get; set; }
        public string strCommentContent { get; set; }
        public string strCommentUser { get; set; }
        public decimal decOrderIndex { get; set; }
        public bool bolIsActive { get; set; }
        public bool bolIsSystem { get; set; }
        public string strCreatedUser { get; set; }
        public DateTime? dtmCreatedDate { get; set; }
        public string strUpdatedUser { get; set; }
        public DateTime? dtmUpdatedDate { get; set; }
        public bool bolIsDeleted { get; set; }
        public string strDeletedUser { get; set; }
        public DateTime? dtmDeletedDate { get; set; }
        public bool bolIsExist { get; set; }
        public string strWorksSupportName { get; set; }
    }
}