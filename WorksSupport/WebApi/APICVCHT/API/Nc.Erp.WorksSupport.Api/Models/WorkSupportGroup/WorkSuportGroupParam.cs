﻿using MyUtils.Validations;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Api.Models.WorkSupportGroup
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
    /// GetByParam
    /// </summary>
    public class GetByParam
    {
        [Required]
        public int Id { get; set; }
    }
    public class GetGroupByProjectId
    { 
        public int ProjectId { get; set; }
        public int WorkTypeId { get; set; }
        public string UserName { get; set; }
    }
    /// <summary>
    /// Params for object Save
    /// </summary>
    public class SaveParam
    {

        [Required]
        public string WorksSupportGroupName { get; set; }
        [Required]
        public int WorksSupportProjectId { get; set; }
        public string Description { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string User { get; set; }
    }
    /// <summary>
    /// Object for update
    /// </summary>
    public class UpdateParam
    {
        [Required]
        public int Id { get; set; }
        public string WorksSupportGroupName { get; set; }
        public int WorksSupportProjectId { get; set; }
        public string Description { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
    }
    /// <summary>
    /// Object for delete
    /// </summary>
    public class DeleteParam
    {
        [Required]
        public int Id { get; set; }
        public string User { get; set; }
    }
    /// <summary>
    /// Object for  Insert + Update
    /// </summary>
    public class SaveParamBy
    {
        [Required]
        public int Id { get; set; }
        public string WorksSupportGroupName { get; set; }
        public int WorksSupportProjectId { get; set; }
        public string Description { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string IconUrl { get; set; }
        public string User { get; set; }
        public int IsAutoAddMemberToWorksGroup { get; set; }
        public List<WorksGroupMember> LstNewMember { get; set; }
        public List<WorksGroupMember> LstDeleteMember { get; set; }
        public List<WorksGroupMember> LstEditMember { get; set; }
    }
    public class WorksGroupMember
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
        public int DepartmentID
        {
            get;
            set;
        }
        public string WorksSupportMemberRoleName { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public string DefaultPictureURL
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
    public class DeleteParamBy
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public string User { get; set; }
    }

    public class GroupMember
    {
        [Required]
        public int WorksSupportGroupId { get; set; }
        [Required]
        public string MemberUserName { get; set; }
        [Required]
        public int WorksSupportMemberRoleId { get; set; }
        public int OrderIndex { get; set; }
        public string CreatedUser { get; set; }


    }
    /// <summary>
    /// Save param
    /// </summary>
    public class SaveGroupMBParam
    {
        public int WorksSupportGroupId { get; set; }
        public string UserName { get; set; }
        public int WorksSupportMemberRoleId { get; set; }
        public int IsAutoAddGroupMember { get; set; }
        public string User { get; set; }

    }
}