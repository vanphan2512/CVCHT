using System;
namespace Nc.Erp.WorksSupport.Api.Common
{
    /// <summary>
    /// Created by      : Trương Hoàng Nhi
    /// Created date    : 01/06/2017
    /// Token
    /// </summary>
    public class AuthenticationToken
    {
        public AuthenticationToken(Guid value, DateTime expiredAfter)
        {
            Value = value.ToString();
            ExpiredAfter = expiredAfter;
        }

        public AuthenticationToken(string value)
        {
            Value = value;
            ExpiredAfter = DateTime.Now;
        }

        public string Value { get; set; }
        public DateTime ExpiredAfter { get; set; }
    }
}