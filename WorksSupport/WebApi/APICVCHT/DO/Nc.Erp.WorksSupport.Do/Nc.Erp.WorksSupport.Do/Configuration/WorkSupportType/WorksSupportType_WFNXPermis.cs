namespace Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType
{
    /// <summary>
    /// Luong trung nhan
    /// created: 02/06/2017
    /// Quyền chuyển bước xử lý
    /// </summary>
    public class WorksSupportTypeWfnxPermis
    {
        public int WorksSupportStepId { get; set; }
        public string NextWorksSupportStepsName { get; set; }
        public int WorksSupportTypeId { get; set; }
        public int NextWorksSupportStepId { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
    }
}
