﻿namespace Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType
{
    /// <summary>
    /// Luong trung nhan
    /// created: 07/06/2017
    /// Quyền trên dự án công việc cần hỗ trợ
    /// </summary>
    public class WorksSupportProjectPermis
    {
        public int WorksSupportTypeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int IsCanAddProject { get; set; }
        public int IsCanEditProject { get; set; }
        public int IsCanDeleteProject { get; set; }
        public int IsCanViewProject { get; set; }
        public int IsCanViewProjectReport { get; set; }

    }
}