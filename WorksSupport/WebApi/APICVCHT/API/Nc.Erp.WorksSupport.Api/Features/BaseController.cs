using System;
using System.Web.Http;
using MyUtils;

namespace Nc.Erp.WorksSupport.Api.Features
{
    /// <summary>
    /// Created by      : Truong Hoang Nhi
    /// Created date    : 25/05/2017
    /// Definition base controller.
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// Validate param
        /// </summary>
        /// <param name="param"></param>
        protected virtual void ValidateParams(object param)
        {
            try
            {
                AttributeValidateUtil.Validate(param);
            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.ToString() : e.Message;
                throw new Exception(msg);
            }
        }
    }
}