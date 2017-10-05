﻿using MyUtils.Validations;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Api.Models.WorkSuportPriority
{
    /// <summary>
    /// Search key param
    /// </summary>
    public class SearchKeyParam
    {   
        public string Keywords { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// Search param
    /// </summary>
    public class SearchParam
    {
        public string Keywords { get; set; }
        [Required]
        public int IsDeleted { get; set; }
    }
    /// <summary>
    /// Get By param
    /// </summary>
    public class GetByParam
    {
        [Required]
        public int Id { get; set; }
    }
    /// <summary>
    /// Get By param project
    /// </summary>
    public class GetByParamProject
    {
        [Required]
        public int Id { get; set; }
    }
    /// <summary>
    /// Object for delete
    /// </summary>
    public class DeleteParam
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string User { get; set; }
    }
    
    /// <summary>
    /// Params for object Save
    /// </summary>
    public class SaveParam
    {
       // [Required]
        //public int WorksSupportPriorityId = -1;
        public string WorksSupportPriorityName { get; set; }
        public string IconURL { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public string CreatedUser { get; set; }
    }

    /// <summary>
    /// Object for update
    /// </summary>
    public class UpdateParam
    {
        [Required]
        public int WorksSupportPriorityId { get; set; }
        public string WorksSupportPriorityName { get; set; }
    }

    /// <summary>
    /// Params for object Save
    /// </summary>
    public class SaveParamBy
    {
     
        public int Id{ get; set; }
        public string WorksSupportPriorityName { get; set; }
        public string IconUrl { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        public string User { get; set; }
    }

}