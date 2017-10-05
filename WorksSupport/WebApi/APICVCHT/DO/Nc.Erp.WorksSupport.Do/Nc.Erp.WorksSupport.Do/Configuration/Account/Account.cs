using System;
namespace Nc.Erp.WorksSupport.Do.Configuration.Account
{
    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime LastLoggedInDate { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiredAfter { get; set; }
        public string Password { get; set; }
        public string LanguageCode { get; set; }
    }
}
