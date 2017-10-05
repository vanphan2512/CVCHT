using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nc.Erp.WorksSupport.Api.Models.WorksSupport
{
    public class WorksSupport_WorkflowParam
    {
        public int WorkflowId { get; set; }
        public int WorksSupportId { get; set; }
        public DateTime? WorksSupportDate { get; set; }
        public int WorksSupportStepId { get; set; }
        public int IsProcess { get; set; }
        public string ProcessUser { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string Note { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}