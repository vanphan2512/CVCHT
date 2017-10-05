using MyUtils.Validations;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Api.Models.Project
{
    /// <summary>
    /// Search param
    /// </summary>
    public class SearchParam
    {
        public string Keywords { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
     /// <summary>
    /// Search param
    /// </summary>
    public class GetAllUser
    {
        public string User{ get; set; }
    }
    /// <summary>
    /// Search param user
    /// </summary>
    public class SearchParamUser
    {
        public int DepartmentId { get; set; }
        public string Keywords { get; set; }

        [Required]
        public int WorksSupportTypeId { get; set; }

        
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
       // public int Id = -1;
        public string WorksSupportProjectName { get; set; }
        public int WorksSupportTypeId { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string CreatedUser { get; set; }
    }

    /// <summary>
    /// Object for update
    /// </summary>
    public class UpdateParam
    {
        [Required]
        public int Id { get; set; }
        public string WorksSupportProjectName { get; set; }
        public int WorksSupportTypeId { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string User { get; set; }
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

    public class SaveParamBy
    {
        [Required]
        public int Id { get; set; }
        public string WorksSupportProjectName { get; set; }
        [Required]
        public string WorksSupportTypeId { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        public string IconUrl { get; set; }
        public string User { get; set; }        
        public List<ProjectMember> LstNewMember { get; set; }
        public List<ProjectMember> LstDeleteMember { get; set; }
        public List<ProjectMember> LstEditMember { get; set; }
    }

    public class ProjectMember
    {
        public string UserName
        {
            get;
            set;
        }   
        public string FullName
        {
            get;
            set;
        }
        public int DepartmentId
        {
            get;
            set;
        }          
        public string WorksSupportMemberRoleName { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public string DefaultPictureUrl
        {
            get;
            set;
        }
        public int IsAutoAddMemberToWorksGroup { get; set; }
        public string MemberUserName { get; set; }
        public string DepartmentName
        {
            get;
            set;
        }
    }
    
}