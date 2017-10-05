using MyUtils.Validations;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Api.Models.WorksSupportType
{
    /// <summary>
    /// Search param
    /// </summary>
    public class SearchParam
    {
        [Required]
        public string Keywords { get; set; }
        [Required]
        public int PageSize { get; set; }
        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int IsDeleted { get; set; }
    }
    /// <summary>
    /// Search param
    /// </summary>
    public class GetByParam
    {
        [Required]
        public int Id { get; set; }
    }

    /// <summary>
    /// Params for object Save
    /// </summary>
    public class SaveParam
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string WorksSupportTypeName { get; set; }
        [Required]
        public string IconURL { get; set; }
        [Required]
        public string AddFunctionId { get; set; }
        [Required]
        public string ViewAllFunctionId { get; set; }
        [Required]
        public string EditFunctionId { get; set; }
        [Required]
        public string EditAllFunctionId { get; set; }
        [Required]
        public string DeleteFunctionId { get; set; }
        [Required]
        public string DeleteAllFunctionId { get; set; }
        [Required]
        public string ProcessFunctionId { get; set; }
        [Required]
        public string CommentFunctionId { get; set; }
        public string Description { get; set; }
        //[Required]
        //public int OrderIndex { get; set; }
        [Required]
        public bool IsActived { get; set; }
        [Required]
        public bool IsSystem { get; set; }
        [Required]
        public string CreatedUser { get; set; }
        [Required]
        public List<WorksSupportTypeWorkFlowParam> lstWorksSupportType_WorkFlow { get; set; }
        [Required]
        public List<WorksSupportType_PriorityParam> lstWorksSupportType_Priority { get; set; }
        [Required]
        public List<WorksSupportType_QualityParam> lstWorksSupportType_Quality { get; set; }
        [Required]
        public List<WorksSupportTypeMemberRoleParam> lstWorksSupportType_MemberRole { get; set; }
        [Required]
        public List<WorksSupportProject_PermisParam> lstWorksSupportProject_Permis { get; set; }
    }

    /// <summary>
    /// Object for update
    /// </summary>
    public class UpdateParam
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string WorksSupportTypeName { get; set; }
        [Required]
        public string IconURL { get; set; }
        [Required]
        public string AddFunctionId { get; set; }
        [Required]
        public string ViewAllFunctionId { get; set; }
        [Required]
        public string EditFunctionId { get; set; }
        [Required]
        public string EditAllFunctionId { get; set; }
        [Required]
        public string DeleteFunctionId { get; set; }
        [Required]
        public string DeleteAllFunctionId { get; set; }
        [Required]
        public string ProcessFunctionId { get; set; }
        [Required]
        public string CommentFunctionId { get; set; }
        public string Description { get; set; }
        [Required]
        public int OrderIndex { get; set; }
        [Required]
        public bool IsActived { get; set; }
        [Required]
        public bool IsSystem { get; set; }
        [Required]
        public string UpdatedUser { get; set; }
        [Required]
        public List<WorksSupportTypeWorkFlowParam> lstWorksSupportType_WorkFlow { get; set; }
        [Required]
        public List<WorksSupportType_PriorityParam> lstWorksSupportType_Priority { get; set; }
        [Required]
        public List<WorksSupportType_QualityParam> lstWorksSupportType_Quality { get; set; }
        [Required]
        public List<WorksSupportTypeMemberRoleParam> lstWorksSupportType_MemberRole { get; set; }
        [Required]
        public List<WorksSupportProject_PermisParam> lstWorksSupportProject_Permis { get; set; }
    }

    /// <summary>
    /// Object for delete
    /// </summary>
    public class DeleteParam
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string User { get; set; }
    }
    /// <summary>
    /// Object for delete
    /// </summary>
    public class DeleteProjectPermisParam
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
    public class SaveParamBy
    {
        [Required]
        public int Id { get; set; }
        public string WorksSupportTypeName { get; set; }
        public string IconUrL { get; set; }
        //[Required]
        //public string AddFunctionId { get; set; }
       // [Required]
        //public string ViewAllFunctionId { get; set; }
        //[Required]
       // public string EditFunctionId { get; set; }
       // [Required]
       // public string EditAllFunctionId { get; set; }
        //[Required]
       // public string DeleteFunctionId { get; set; }
        //[Required]
       // public string DeleteAllFunctionId { get; set; }
        //[Required]
       // public string ProcessFunctionId { get; set; }
       // public string CommentFunctionId { get; set; }
       // public string AddProjectFunctionId { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string User { get; set; }

    }
    /// <summary>
    /// Object for delete 
    /// </summary>
    public class DeleteWorksTypeParam
    {
        [Required]
        public string Id { get; set; }
        public string User { get; set; }
    }
    public class SaveWorkTypeParam
    {
        public int WorksSupportTypeId { get; set; }
        [Required]
        public string WorksSupportTypeName { get; set; }
        public string Description { get; set; }
        public string IconUrL { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        public string Priority { get; set; } //1,2,4
        public string Quality { get; set; } //1,3
        public string Step { get; set; } //JSON
        public string User { get; set; }
        public int OrderIndex { get; set; }
        public string DeletedStepId { get; set; } //JSON

    }

    public class PermissionParam
    {
        [Required]
        public string Permissions { get; set; } //JSON
    }

    public class CopyPermissionParam
    {
        public string FromWorksTypeId { get; set; }
        public string ToWorksTypeId { get; set; }
        public string User { get; set; }

    }

    public class CopyWorksSupportTypeParam
    {
        public string WorksTypeId { get; set; }
        public string User { get; set; }
    }
}