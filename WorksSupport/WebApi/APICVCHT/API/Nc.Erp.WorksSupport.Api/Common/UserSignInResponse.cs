using Nc.Erp.WorksSupport.Do.Configuration.Account;

namespace Nc.Erp.WorksSupport.Api.Common
{
    public class UserSignInResponse
    {
        public UserSignInResponse(AuthenticationToken token, Account userProfile)
        {
            Token = token;
            Profile = userProfile;
        }
        public AuthenticationToken Token { get; set; }
        public Account Profile { get; set; }
    }
}