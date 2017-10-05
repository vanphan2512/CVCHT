﻿using Nc.Erp.WorksSupport.Api.Common;
using Nc.Erp.WorksSupport.Api.Models.WorksSupport;
using Nc.Erp.WorksSupport.Da.Configuration.Sys;
using Nc.Erp.WorksSupport.Da.Configuration.WorksSupport;
using Nc.Erp.WorksSupport.Do.Configuration.Sys;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nc.Erp.WorksSupport.Da.Configuration.WorkSupportType;
using Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType;

namespace Nc.Erp.WorksSupport.Api.Features.WorksSupport
{
    using System.Threading.Tasks;

    [RoutePrefix("api/v2/WorksSupport")]
    public class WorksSupportController : BaseController
    {
        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("loadalluser")]
        public Response<List<User>> LoadAllUser()
        {
            // Definition object to return.
            var ret = new Response<List<User>>();
            try
            {
                // Definition object return
                var listWorksSupport = new List<User>();


                // Call DaSystemManagement to search data.
                var objResultMessage = new DaSys().LoadAll(ref listWorksSupport);

                if (listWorksSupport != null)
                {
                    foreach (var item in listWorksSupport)
                    {
                        if (item.DefaultPICTUREURL != "")
                        {
                            item.DefaultPICTUREURL = GetAvatar(item.DefaultPICTUREURL);
                        }
                    }
                    ret.Success = true;
                    ret.Data = listWorksSupport;
                    ret.TotalRecord = listWorksSupport.Count;
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
                var msg = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, desc);
                throw new HttpResponseException(msg);
            }
        }

        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getUserByName/{userName}")]
        public Response<List<User>> LoadAllUser([FromUri] string userName)
        {
            // Definition object to return.
            var ret = new Response<List<User>>();
            try
            {
                // Definition object return
                var listWorksSupport = new List<User>();


                // Call DaSystemManagement to search data.
                var objResultMessage = new DaSys().LoadByUserName(ref listWorksSupport, userName);

                if (listWorksSupport != null)
                {
                    foreach (var item in listWorksSupport)
                    {
                        if (item.DefaultPICTUREURL != "")
                        {
                            item.DefaultPICTUREURL = GetAvatar(item.DefaultPICTUREURL);
                        }
                    }
                    ret.Success = true;
                    ret.Data = listWorksSupport;
                    ret.TotalRecord = listWorksSupport.Count;
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
                var msg = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, desc);
                throw new HttpResponseException(msg);
            }
        }

        /// <summary>
        /// Load danh sach buoc xu ly ke tiep bang workssupportId
        /// </summary>
        /// <param name="worksId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("loadStep/{worksId}/{user}")]
        public Response<List<WorksSupportNextStep>> GetById([FromUri] int worksId, string user)
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportNextStep>>();
            try
            {
                // Definition object return
                var listWorksSupportProcess = new List<WorksSupportNextStep>();
                // Call DaWorksSupportType to search data
                var objResultMessage = new DaWorksSupport().LoadWorksSupportWfNx(ref listWorksSupportProcess, worksId, user);

                if (listWorksSupportProcess != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportProcess;
                    ret.TotalRecord = listWorksSupportProcess.Count;
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
                var msg = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, desc);
                throw new HttpResponseException(msg);
            }
        }
        /// <summary>
        /// Lấy Danh sach nguoi thuc hien theo buoc xu ly thuoc vai tro 
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("getProcessMember/{worksId}/{stepId}")]
        public async Task<Response<List<WorksSupportProcessMember>>> GetProcessUserByWorkIdAndStepId([FromUri] ProcessUserParam objparam)
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportProcessMember>>();
            try
            {
                // Definition object return
                var listWorksSupportMember = new List<WorksSupportProcessMember>();
                // Call DaWorksSupportType to search data
                new DaWorksSupport().LoadProcessUserByWorkIdAndStepId(ref listWorksSupportMember, objparam.WorksId, objparam.StepId);

                if (listWorksSupportMember != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportMember;
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

        /// <summary>
        /// Tìm kiếm công việc cần hỗ trợ theo keyword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public Response<List<Do.Configuration.WorksSupport.WorksSupport>> Search([FromUri] SearchParam objparam)
        {
            // Definition object to return.
            var ret = new Response<List<Do.Configuration.WorksSupport.WorksSupport>>();
            try
            {
                // Definition object return
                var listWorksSupport = new List<Do.Configuration.WorksSupport.WorksSupport>();

                // Call DaSystemManagement to search data.
               new DaWorksSupport().SearchData(ref listWorksSupport, objparam.WorksGroupId, objparam.Keywords, objparam.PageIndex, objparam.PageSize, objparam.User);

                if (listWorksSupport != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupport;
                    ret.TotalRecord = listWorksSupport.Count;
                }
                else
                {
                    ret.Success = false;
                   // ret.Message = objResultMessage.Message;
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

        /// <summary>
        /// Lấy danh sách công việc cần hỗ trợ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public Response<List<WorkSupport>> GetAll()
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupport>>();
            try
            {
                // Definition object return
                var listWorksSupport = new List<WorkSupport>();
                // Call DaSystemManagement to search data.
                var objResultMessage = new DaWorksSupport().GetAll(ref listWorksSupport);

                if (listWorksSupport != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupport;
                    ret.TotalRecord = listWorksSupport.Count;
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
        /// <summary>
        /// Xóa công việc yêu cầu cần hỗ trợ theo ID
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Del")]
        public Response<WorkSupport> Delete([FromBody] DeleteParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorkSupport>();
            try
            {
                //Validate param
                ValidateParams(objparam);
                // Call DaWorksSupport to search data.
                var objResultMessage = new DaWorksSupport().Delete(objparam.User, objparam.Id);

                if (!objResultMessage.IsError)
                {
                    ret.Success = true;
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

        
        #region công việc cần hỗ trợ chi tiết


        #endregion
        #region bước xử lý công việc
        /// <summary>
        /// lay thong tin eo_workssupporttype_workflow
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetWorkSupportTypeWorkFlow/{Id}")]
        public Response<List<WorksSupportTypeWorkFlow>> GetWorkSupportTypeWorkFlow([FromUri] GetByParam obj)
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportTypeWorkFlow>>();
            try
            {
                // Definition object return
                var listWorksSupport = new List<WorksSupportTypeWorkFlow>();

                var objResultMessage = new DaWorkSupportTypeWorkFlow().GetAll(ref listWorksSupport, obj.Id);

                if (listWorksSupport != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupport;
                    ret.TotalRecord = listWorksSupport.Count;
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

        [HttpGet]
        [Route("getworksflowbyidandname")]
        public Response<List<WorksSupportTypeWorkFlow>> GetByWoksSupportTypeIdAndName([FromUri] GetByParam obj)
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportTypeWorkFlow>>();
            try
            {
                // Definition object return
                var listWorksSupport = new List<WorksSupportTypeWorkFlow>();

                var objResultMessage = new DaWorkSupportTypeWorkFlow().GetByWoksSupportTypeIdAndName(ref listWorksSupport, obj.Id, obj.KeyWork);

                if (listWorksSupport != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupport;
                    ret.TotalRecord = listWorksSupport.Count;
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

        /// <summary>
        /// Get avatar
        /// </summary>
        /// <param name="strUrlAvatar"></param>
        /// <returns></returns>
        public static string GetAvatar(string strUrlAvatar)
        {
            if (string.IsNullOrEmpty(strUrlAvatar))
            {
                return System.Configuration.ConfigurationManager.AppSettings["UserImageDefault"];
            }
            var strResult = (string.IsNullOrEmpty(strUrlAvatar) ? System.Configuration.ConfigurationManager.AppSettings["UserImageDefault"] : System.Configuration.ConfigurationManager.AppSettings[(strUrlAvatar.IndexOf("UserImages/", StringComparison.Ordinal) > -1) ? "UserImageHostUrl" : "WebAlbumUrl"] + strUrlAvatar);
            strResult = strResult.Replace('\\', '/');
            return strResult;
        }
        #endregion

        /// <summary>
        /// Load cong viec theo (ngay, tuan, thang)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TypePeriod")]
        public Response<List<WorksSupport_WorksSupportTypePeriod>> GetAllTypePeriod()
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupport_WorksSupportTypePeriod>>();
            try
            {
                // Definition object return
                var listWorksSupportTypePeriod = new List<WorksSupport_WorksSupportTypePeriod>();

                // Call DaSystemManagement to search data.
                var objResultMessage = new DaWorksSupport_WorksSupportTypePeriod().GetAll(ref listWorksSupportTypePeriod);

                if (listWorksSupportTypePeriod != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportTypePeriod;
                    ret.TotalRecord = listWorksSupportTypePeriod.Count;
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

        /// Chon buoc xu ly ke tiep cho cong viec can ho tro
        /// 1. Insert buoc ke tiep vao workssupport_worksflow
        /// 2. Cap nhat buoc hien tai isproccess =1
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveWorksProcess")]
        public Response<WorksSupportWorkflow> InsertWorkFlowNextStep([FromBody] SaveNextStepParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportWorkflow>();
            try
            {
                //validate param
                ValidateParams(objparam);
                // check name is existed in database
                // Definition object return
                var obj = new WorksSupportWorkflow
                {
                    WorksSupportStepId = objparam.NextWorksSupportStepId,
                    WorksSupportId = objparam.WorksSupportId,
                    ProcessUser = objparam.ProcessUser,
                    Note = objparam.Note,
                };
                // Call DaWorksSupportStatus to search data.
                var objResultMessage = new DaWorksSupportWorkflow().InsertWorkFlowNextStep(objparam.WorksSupportId, objparam.NextWorksSupportStepId, objparam.UpdatedUser, objparam.ProcessUser, objparam.Note);

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
