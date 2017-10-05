﻿namespace Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType
{
     /// <summary>
    /// Luong trung nhan
    /// created: 07/06/2017
    /// Quyền trên mỗi bước công việc cần hỗ trợ
    /// </summary>
    public class WorksSupportTypeWfPermis
    {
        public int WorksSupportStepId { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public bool IsCanAddAttachment{ get; set; }
        public bool IsCanChangeProgress{ get; set; }
        public bool IsCanComment{ get; set; }
        public bool IsCanEditContent{ get; set; }
        public bool IsCanEditExpectedCompletedDate{ get; set; }
        public bool IsCanEditQuality{ get; set; }
        public bool IsCanEditSolutionContent{ get; set; }
        public bool IsCanProcessWorkflow { get; set; }
        public bool IsMustChooseProcessUser { get; set; }
    }
}