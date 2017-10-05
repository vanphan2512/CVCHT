using MyUtils.Validations;

namespace Nc.Erp.WorksSupport.Api.Models.MemberRole
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
        /// Get By param
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
        public int Id = -1;
        public string WorksSupportMemberRoleName { get; set; }       
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
        public int WorksSupportMemberRoleID { get; set; }
        public string WorksSupportMemberRoleName { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public int IsActived { get; set; }
        public int IsSystem { get; set; }
        public string UpdatedUser { get; set; }
    }

    /// <summary>
    /// Object for delete
    /// </summary>
    public class DeleteParam
    {
        [Required]
        public string Id { get; set; }
        //[Required]
        public string User { get; set; }
    }
   
    /// <summary>
    /// Object for  Insert + Update
    /// </summary>
    public class SaveParamBy
    {
        [Required]
        public int Id { get; set; }
        public string WorksSupportMemberRoleName { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        //public string UpdatedUser { get; set; }
        //public string CreateUser { get; set; }
        public string User { get; set; }

    }
}