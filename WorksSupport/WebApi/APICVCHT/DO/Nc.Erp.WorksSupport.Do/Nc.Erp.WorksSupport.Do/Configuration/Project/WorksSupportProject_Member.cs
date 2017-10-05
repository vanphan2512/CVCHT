﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc.Erp.WorksSupport.Do.Configuration.Project
{
    /// <summary>
    /// Nguyễn Văn Phận
    /// created: 09/06/2017
    /// Quyền các thành viên dự án
    /// </summary>
    public class WorksSupportProject_Member
    {
        public int WorksSupportProjectId { get; set; }
        public string MemberUserName { get; set; }
        public string FullName { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public int IsAutoAddMemberToWorksGroup { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DefaultPictureUrl { get; set; }
        public string DepartmentName { get; set; }
        public string WorksSupportMemberRoleName { get; set; }
        public string UserName { get; set; }

        public int DepartmentID { get; set; }
      
        public string DefaultPictureURL { get; set; }
    
    }
}
