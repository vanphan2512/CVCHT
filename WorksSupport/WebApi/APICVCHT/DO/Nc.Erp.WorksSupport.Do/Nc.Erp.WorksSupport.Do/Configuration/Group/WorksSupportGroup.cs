using Nc.Erp.WorksSupport.Do.Configuration.ReportProces;
using System;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Do.Configuration.Group
{
    /// <summary>
    /// Created by      : Nguyen Thi Kim Ngan
    /// Created date    : 08/06/2017
    /// Đối tượng ERP.EO.WORKSSUPPORTGROUP.
    /// </summary>
    public class WorksSupportGroup
    {
        public int WorksSupportGroupId { get; set; }
        public string WorksSupportGroupName { get; set; }
        public int WorksSupportProjectId { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set;}
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int NumberOfMember { get; set; }
        public int NumberOfWork { get; set; }
        public string IconUrl { get; set; }
        public bool IsCanAddWorksSupportGroup { get; set; }
        public bool IsCanAddWorksSupport { get; set; }
        public bool IsCanEditWorksSupportGroup { get; set; }
        public bool IsCandEitWorksSupport { get; set; }
        public bool IsCanDeleteWorksSupportGroup { get; set; }
        public bool IsCanDeleteworkssupport { get; set; }
        public List<WorksSupportGroupMember> LstNewMember { get; set; }
        public List<WorksSupportGroupMember> LstEditMember { get; set; }
        public List<WorksSupportGroupMember> LstDeleteMember { get; set; }

    }
}
