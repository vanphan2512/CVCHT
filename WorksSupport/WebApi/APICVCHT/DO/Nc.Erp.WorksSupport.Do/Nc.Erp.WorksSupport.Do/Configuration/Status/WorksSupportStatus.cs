﻿using System;

namespace Nc.Erp.WorksSupport.Do.Configuration
{
    public class WorksSupportStatus
    {
        public int WorksSupportStatusId { get; set; }
        public string WorksSupportStatusName { get; set; }
        public string IconUrl { get; set; }
        public string ColorCode { get; set; }
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

    }

    public class StatusCheck
    {
        public bool IsInitStatus { get; set; }
        public bool IsCompleteStatus { get; set; }
        public bool IsCloseStatus { get; set; }

        public int Total { get; set; }
    }
}