using MyUtils.Validations;

namespace Nc.Erp.WorksSupport.Api.Models.Account
{
    public class LoginParam
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}