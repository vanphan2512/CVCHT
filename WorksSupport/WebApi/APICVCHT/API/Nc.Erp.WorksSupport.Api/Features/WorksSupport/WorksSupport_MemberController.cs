using Nc.Erp.WorksSupport.Api.Common;
using Nc.Erp.WorksSupport.Api.Models.WorksSupport;
using Nc.Erp.WorksSupport.Da.Configuration.WorksSupport;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nc.Erp.WorksSupport.Api.Features.WorksSupport
{
    [RoutePrefix("api/v2/WorksSupport_Member")]
    public class WorksSupportMemberController : ApiController
    {
        /// <summary>
        /// Lấy Danh sach nguoi thuc hien theo buoc xu ly thuoc vai tro 
        /// </summary>
        /// <param name="searchByUserId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{searchByUserId}")]
        public Response<List<WorksSupportMember>> GetProcessUser([FromUri] int searchByUserId)
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportMember>>();
            try
            {
                // Definition object return
                var listProcessUser = new List<WorksSupportMember>();
                // Call DaWorksSupportType to search data
                new DaWorksSupportMember().LoadProcessUserBy_StepID(ref listProcessUser, searchByUserId);
                if (listProcessUser != null)
                {
                    ret.Success = true;
                    ret.Data = listProcessUser;
                    ret.TotalRecord = listProcessUser.Count;
                }
                else
                {
                    ret.Success = false;
                }
                // Return data.
                return ret;
            }
            catch (Exception e)
            {
                var desc = e.InnerException != null ? e.InnerException.ToString() : e.Message;
                var msg = Request.CreateErrorResponse(HttpStatusCode.BadRequest, desc);
                throw new HttpResponseException(msg);
            }
        }

        /// them thanh vien cho cong viec can ho tro
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertMember")]
        public Response<WorksSupportMember> Insert_WS_Member([FromBody] SaveWSMember objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportMember>();
            try
            {
                // Definition object return
                var obj = new WorksSupportMember()
                {
                    WorksSupportId = objparam.WorksSupportId,
                    WorksSupportDate = objparam.WorksSupportDate,
                    MemberUserName = objparam.MemberUserName,
                    WorksSupportMemberRoleId = objparam.WorksSupportMemberRoleId,
                    UpdatedUser = objparam.UpdatedUser
                };
                // Call DaWorksSupportStatus to search data.
                var objResultMessage = new DaWorksSupportMember().Insert_WS_Member(obj, ref obj);

                if (!objResultMessage.IsError)
                {
                    ret.Success = true;
                    ret.Data = obj;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessage.Message;
                }
                // Return data.
                return ret;
            }
            catch (Exception e)
            {
                var desc = e.InnerException != null ? e.InnerException.ToString() : e.Message;
                var msg = Request.CreateErrorResponse(HttpStatusCode.BadRequest, desc);
                throw new HttpResponseException(msg);
            }
        }
    }
}
