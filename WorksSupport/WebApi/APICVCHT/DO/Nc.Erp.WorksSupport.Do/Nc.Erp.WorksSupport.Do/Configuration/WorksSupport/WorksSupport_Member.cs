﻿using System;

namespace Nc.Erp.WorksSupport.Do.Configuration.WorksSupport
{
    /// <summary>
    /// Luong trung nhan
    /// created: 15/06/2017
    /// Các thành viên tham gia vào công việc hỗ trợ
    /// </summary>
    public class WorksSupportMember
    {
        public int WorksSupportId { get; set; }
        public DateTime? WorksSupportDate { get; set; }
        public string MemberUserName { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public string Note { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    public class WorksSupportProcessMember
    {
        public string UserName { get; set; }
        public string FullName { get; set; }

    }
}