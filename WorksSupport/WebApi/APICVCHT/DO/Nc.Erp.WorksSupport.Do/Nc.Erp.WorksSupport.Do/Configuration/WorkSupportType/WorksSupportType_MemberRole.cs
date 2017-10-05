namespace Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType
{
    /// <summary>
    /// Luong trung nhan
    /// created: 07/06/2017
    /// Danh sách các vai trò của một công việc cần hỗ trợ
    /// </summary>
    public class WorksSupportTypeMemberRole
    {
        public int WorksSupportTypeId { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public string WorksSupportMemberRoleName { get; set; }
        public int IsCanAddWorksSupportGroup { get; set; }
        public int IsCanAddWorksSupport { get; set; }
        public int IsCanEditWorksSupportGroup { get; set; }
        public int IsCanEditWorksSupport { get; set; }
        public int IsCanDeleteWorksSupportGroup { get; set; }
        public int IsCanDeleteWorksSupport { get; set; }
        public int IsDefaultRole { get; set; }

    }
}
