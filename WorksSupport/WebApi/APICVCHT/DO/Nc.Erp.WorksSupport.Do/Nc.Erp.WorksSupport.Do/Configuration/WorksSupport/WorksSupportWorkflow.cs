using System;

namespace Nc.Erp.WorksSupport.Do.Configuration.WorksSupport
{
    /// <summary>
    /// Luong trung nhan
    /// created: 15/06/2017
    /// Bước xử lý công việc cần hỗ trợ
    /// </summary>
    public class WorksSupportWorkflow
    {
        public int WorkflowId { get; set; }
        public int WorksSupportId { get; set; }
        public string WorksSupportStepName { get; set; }
        public string ProcessUser { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string Note { get; set; }

        public string WorkSupportStatusName { get; set; }

        public int WorksSupportStepId { get; set; }
    }
}
