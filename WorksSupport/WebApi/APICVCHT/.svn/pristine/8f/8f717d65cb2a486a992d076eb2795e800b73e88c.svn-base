using Nc.Erp.WorksSupport.Api.Common;
using Nc.Erp.WorksSupport.Api.Models.ReportProces;
using Nc.Erp.WorksSupport.Da.Configuration.Group;
using Nc.Erp.WorksSupport.Da.Configuration.ReportProces;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nc.Erp.WorksSupport.Do.Configuration.Group;
using Nc.Erp.WorksSupport.Do.Configuration.ReportProces;
using System.IO;
using System.Net.Http.Headers;
using System.Data;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Globalization;

namespace Nc.Erp.WorksSupport.Api.Features.WorksSupport
{
    [RoutePrefix("api/v2/Report")]
    public class WorksSupportReportController : ApiController
    {
        /// <summary>
        /// Tìm kiếm bao cao tien do cầu công việc cần hỗ trợ theo keyword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public Response<List<WorkSupport>> Search([FromUri] SearchParam objparam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupport>>();
            try
            {
                // Definition object return
                var listWorksSupport = new List<WorkSupport>();
                // Call DaSystemManagement to search data.
                var objResultMessage = new DaReportProcess().SearchData_Works(ref listWorksSupport, objparam.Keywords, objparam.WorksSupportGroupId, objparam.WorksSupportProjectId, objparam.MemberUerName, objparam.KeyStatus);

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
        /// Lấy danh sách nhom công việc cần hỗ trợ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public Response<List<WorksSupportGroup>> GetAll()
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportGroup>>();
            try
            {
                // Definition object return
                var listWorksSupportStatus = new List<WorksSupportGroup>();

                // Call DaSystemManagement to search data.
                var objResultMessage = new DaWorksSupportGroup().SearchData(ref listWorksSupportStatus);

                if (listWorksSupportStatus != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportStatus;
                    ret.TotalRecord = listWorksSupportStatus.Count;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get project by user is can view report.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LoadProject/{userName}")]
        public Response<List<WorkSupportProject>> GetProjectByUser(string userName)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportProject>>();
            try
            {
                // Definition object return
                var lstProject = new List<WorkSupportProject>();

                // Call DaSystemManagement to search data.
                var objResultMessage = new DaReportProcess().LoadProjectByUser(ref lstProject, userName);

                if (lstProject != null)
                {
                    ret.Success = true;
                    ret.Data = lstProject;
                    ret.TotalRecord = lstProject.Count;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get worksgroup by project id.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetWorkGroupByProjectId/{projectId}")]
        public Response<List<WorkSupportGroup>> GetWorkGroupByProjectId(int projectId)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportGroup>>();
            try
            {
                // Definition object return
                var lstWorkGroup = new List<WorkSupportGroup>();

                // Call DaSystemManagement to search data.
                var objResultMessage = new DaReportProcess().LoadWorksGroupByProjectId(ref lstWorkGroup, projectId);

                if (lstWorkGroup != null)
                {
                    ret.Success = true;
                    ret.Data = lstWorkGroup;
                    ret.TotalRecord = lstWorkGroup.Count;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get report for work 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReport")]
        public Response<WorkSupportReport> GetReport([FromBody] SearchReportParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<WorkSupportReport>();
            try
            {
                // Definition object return
                var objReport = new WorkSupportReport();
                var lstWorkStatus = new List<WorkSupportStatus>();
                var objWorkWrong = new Object();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadReportByWorkStatus(ref lstWorkStatus, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                var objResultMessageState = new DaReportProcess().LoadReportByWorkState(ref objWorkWrong, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);

                if (lstWorkStatus != null && objWorkWrong != null)
                {
                    objReport.WorkStatusList = lstWorkStatus;
                    objReport.WorkWrong = objWorkWrong;
                    ret.Success = true;
                    ret.Data = objReport;
                    ret.TotalRecord = 1;
                }
                else
                {
                    if (lstWorkStatus != null)
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageStatue.Message;
                    }
                    else
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageState.Message;
                    }

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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get report for work by day
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLineChartByDay")]
        public Response<List<WorkStateReportByTime>> GetLineChartByDay([FromBody] SearchReportParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkStateReportByTime>>();
            try
            {
                // Definition object return
                var lstWorkTime = new List<WorkStateReportByTime>();
                var lstWorkStatuslate = new List<WorkSupportReportTime>();
                var lstWorkWrong = new List<WorkSupportReportTime>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorkStateLateByDay(ref lstWorkStatuslate, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                var objResultMessageState = new DaReportProcess().LoadWorkStateWrongByDay(ref lstWorkWrong, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                HandleLineChartByDay(lstWorkStatuslate, lstWorkWrong, reportParam, ref lstWorkTime);
                if (lstWorkStatuslate != null && lstWorkWrong != null)
                {
                    ret.Success = true;
                    ret.Data = lstWorkTime;
                    ret.TotalRecord = 1;
                }
                else
                {
                    if (lstWorkStatuslate != null)
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageStatue.Message;
                    }
                    else
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageState.Message;
                    }

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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get report for work by week
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLineChartByWeek")]
        public Response<List<WorkStateReportByTime>> GetLineChartByWeek([FromBody] SearchReportWeek reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkStateReportByTime>>();
            try
            {
                // Definition object return
                var lstWorkTime = new List<WorkStateReportByTime>();
                var lstWorkStatuslate = new List<WorkSupportReportTime>();
                var lstWorkWrong = new List<WorkSupportReportTime>();
                var objResultMessageStatue = new DaReportProcess().LoadWorkStateLateByWeek(ref lstWorkStatuslate, reportParam.ProjectId, reportParam.WorkGroupId, date);
                var objResultMessageState = new DaReportProcess().LoadWorkStateWrongByWeek(ref lstWorkWrong, reportParam.ProjectId, reportParam.WorkGroupId, date);
                // handle array before response.
               
                HandleLineChartByWeek(lstWorkStatuslate, lstWorkWrong, reportParam, ref lstWorkTime);
                if (lstWorkStatuslate != null && lstWorkWrong != null)
                {
                    ret.Success = true;
                    ret.Data = lstWorkTime;
                    ret.TotalRecord = 1;
                }
                else
                {
                    if (lstWorkStatuslate != null)
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageStatue.Message;
                    }
                    else
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageState.Message;
                    }

                }
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get report for work state by month
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLineChartByMonth")]
        public Response<List<WorkStateReportByTime>> GetLineChartByMonth([FromBody] SearchReportMonth reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkStateReportByTime>>();
            try
            {
                // Definition object return
                var lstWorkTime = new List<WorkStateReportByTime>();
                var objReport = new WorkSupportReportDate();
                var lstWorkStatuslate = new List<WorkSupportReportTime>();
                var lstWorkWrong = new List<WorkSupportReportTime>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorkStateLateByMonth(ref lstWorkStatuslate, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.Date);
                var objResultMessageState = new DaReportProcess().LoadWorkStateWrongByMonth(ref lstWorkWrong, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.Date);
                // handle array before response.
              //  HandleLineChartByMonth(lstWorkStatuslate, lstWorkWrong, reportParam, ref lstWorkTime);
                if (lstWorkStatuslate != null && lstWorkWrong != null)
                {
                    ret.Success = true;
                    ret.Data = lstWorkTime;
                    ret.TotalRecord = 1;
                }
                else
                {
                    if (lstWorkStatuslate != null)
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageStatue.Message;
                    }
                    else
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageState.Message;
                    }

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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get report for work by year
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLineChartByYear")]
        public Response<List<WorkStateReportByTime>> GetLineChartByYear([FromBody] SearchReportParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkStateReportByTime>>();
            try
            {
                // Definition object return
                var lstWorkTime = new List<WorkStateReportByTime>();
                var lstWorkStatuslate = new List<WorkSupportReportTime>();
                var lstWorkWrong = new List<WorkSupportReportTime>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorkStateLateByYear(ref lstWorkStatuslate, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                var objResultMessageState = new DaReportProcess().LoadWorkStateWrongByYear(ref lstWorkWrong, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                HandleLineChartByYear(lstWorkStatuslate, lstWorkWrong, reportParam, ref lstWorkTime);
                if (lstWorkStatuslate != null && lstWorkWrong != null)
                {
                    ret.Success = true;
                    ret.Data = lstWorkTime;
                    ret.TotalRecord = 1;
                }
                else
                {
                    if (lstWorkStatuslate != null)
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageStatue.Message;
                    }
                    else
                    {
                        ret.Success = false;
                        ret.Message = objResultMessageState.Message;
                    }

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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get report for work status
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReportByStatus")]
        public Response<List<WorkSupportStatus>> GetReportByStatus([FromBody] SearchReportParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportStatus>>();
            try
            {
                // Definition object return 
                var lstWorkStatus = new List<WorkSupportStatus>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadReportByWorkStatus(ref lstWorkStatus, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                if (lstWorkStatus != null)
                {
                    ret.Success = true;
                    ret.Data = lstWorkStatus;
                    ret.TotalRecord = lstWorkStatus.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageStatue.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get workgroup by work status  
        /// </summary>
        /// <param name="reportParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkGroupByStatus")]
        public Response<List<WorkSupportGroup>> GetWorkGroupByStatus([FromBody] SearchWorkGroupParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportGroup>>();
            try
            {
                // Definition object return 
                var lstWorkGroup = new List<WorkSupportGroup>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadReportByWorkGroupStatus(ref lstWorkGroup, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate, reportParam.StatusId);
                if (lstWorkGroup != null)
                {
                    ret.Success = true;
                    ret.Data = lstWorkGroup;
                    ret.TotalRecord = lstWorkGroup.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageStatue.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work status by workgroup id
        /// </summary>
        /// <param name="reportParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkStatusByWorkGroupId")]
        public Response<List<WorkSupportDetail>> GetWorkStatusByWorkGroupId([FromBody] SearchWorkGroupParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportDetail>>();
            try
            {
                // Definition object return 
                var lstWork = new List<WorkSupportDetail>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorksStatusByWorkGroupId(ref lstWork, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate, reportParam.StatusId);
                if (lstWork != null)
                {
                    ret.Success = true;
                    ret.Data = lstWork;
                    ret.TotalRecord = lstWork.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageStatue.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work group list by group type
        /// </summary>
        /// <param name="reportParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkGroupByGroupType")]
        public Response<List<WorkSupportGroup>> GetWorkGroupByGroupType([FromBody] SearchReportParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportGroup>>();
            try
            {
                // Definition object return 
                var lstWrokGroup = new List<WorkSupportGroup>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorkGroupByGroupType(ref lstWrokGroup, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                if (lstWrokGroup != null)
                {
                    ret.Success = true;
                    ret.Data = lstWrokGroup;
                    ret.TotalRecord = lstWrokGroup.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageStatue.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work list by group type
        /// </summary>
        /// <param name="reportParam">GroupId, FromDate, ToDate </param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkDetailByGroupType")]
        public Response<List<WorkSupportDetail>> GetWorkDetailByGroupType([FromBody] SearchReportParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportDetail>>();
            try
            {
                // Definition object return 
                var lstWrokGroup = new List<WorkSupportDetail>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorkDetailsByGroupType(ref lstWrokGroup, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                if (lstWrokGroup != null)
                {
                    ret.Success = true;
                    ret.Data = lstWrokGroup;
                    ret.TotalRecord = lstWrokGroup.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageStatue.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work State by State type 
        /// </summary>
        /// <param name="reportParam">projectId, GroupId, FromDate, ToDate</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkStateByStateType")]
        public Response<Object> GetWorkStateByStateType([FromBody] SearchReportParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<Object>();
            try
            {
                // Definition object return 
                var objWorkWrong = new Object();
                // Call DaSystemManagement to search data.
                var objResultMessageState = new DaReportProcess().LoadReportByWorkState(ref objWorkWrong, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
                if (objWorkWrong != null)
                {
                    ret.Success = true;
                    ret.Data = objWorkWrong;
                    ret.TotalRecord = 1;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageState.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work group list by state type
        /// </summary>
        /// <param name="reportParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoadWorkGroupsByState")]
        public Response<List<WorkSupportGroup>> LoadWorkGroupsByState([FromBody] SearchWorkGroupStateParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportGroup>>();
            try
            {
                // Definition object return 
                var lstWrokGroup = new List<WorkSupportGroup>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorkGroupsByState(ref lstWrokGroup, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate, reportParam.type);
                if (lstWrokGroup != null)
                {
                    ret.Success = true;
                    ret.Data = lstWrokGroup;
                    ret.TotalRecord = lstWrokGroup.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageStatue.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work  list by state type
        /// </summary>
        /// <param name="reportParam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkDetailByStateType")]
        public Response<List<WorkSupportDetail>> GetWorkDetailByStateType([FromBody] SearchWorkGroupStateParam reportParam)
        {
            // Definition object to return.
            var ret = new Response<List<WorkSupportDetail>>();
            try
            {
                // Definition object return 
                var lstWrokGroup = new List<WorkSupportDetail>();
                // Call DaSystemManagement to search data.
                var objResultMessageStatue = new DaReportProcess().LoadWorkDetailsByStateType(ref lstWrokGroup, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate, reportParam.type);
                if (lstWrokGroup != null)
                {
                    ret.Success = true;
                    ret.Data = lstWrokGroup;
                    ret.TotalRecord = lstWrokGroup.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessageStatue.Message;
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  download excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadReportExcel")]
        public HttpResponseMessage DownloadReportExcel([FromBody] SearchReportParam reportParam)
        {
            reportParam = new SearchReportParam();
            reportParam.ProjectId = 432;
            reportParam.WorkGroupId = 453;
            reportParam.ProjectName = "huan";
            reportParam.FromDate = null;
            reportParam.ToDate = null;
            MediaTypeHeaderValue mediaType = MediaTypeHeaderValue.Parse("application/octet-stream");

            string fileName = "Bao_Cao_Cong_Viec.xlsx";
            // Definition object return 
            var excelReport = new List<ExcelReport>();
            // Call DaSystemManagement to search data.
            var objReport = new WorkSupportReport();
            var lstWorkStatus = new List<WorkSupportStatus>();
            var objWorkWrong = new Object();
            // Call DaSystemManagement to search data.
            //new DaReportProcess().LoadReportByWorkStatus(ref lstWorkStatus, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
            //new DaReportProcess().LoadReportByWorkState(ref objWorkWrong, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);

            var daExcel = new DaReportProcess().LoadDataForExcelReport(ref excelReport, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
            byte[] excelFile = ExcelSheet(excelReport, reportParam);
            MemoryStream memoryStream = new MemoryStream(excelFile);
            HttpResponseMessage response = response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(memoryStream);
            response.Content.Headers.ContentType = mediaType;
            response.Content.ReadAsStringAsync();
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("fileName") { FileName = fileName };
            return response;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  create excel file.
        /// </summary>
        /// <param name="lstExcel"></param>
        /// <param name="reportParam"></param>
        /// <returns></returns>
        public byte[] ExcelSheet(List<ExcelReport> lstExcel, SearchReportParam reportParam)
        {
            var package = new ExcelPackage();
            #region  thêm dữ liệu và định dạng sheet tổng quan
            string sheetName = "Tổng quan";
            var worksheet = package.Workbook.Worksheets.Add(sheetName);
            DataTable dtOverView = new DataTable();
            SetDataForOverViewSheet(ref dtOverView, reportParam);
            worksheet.Cells["A1:A3"].Style.Font.Bold = true;
            worksheet.Cells["A1:A3"].Style.Font.Size = 18;
            worksheet.Cells["A1:A3"].LoadFromDataTable(dtOverView, true);
            AddPieChartIntoOverViewSheet(ref package, lstExcel, ref worksheet);
            AddColumnChartIntoOverViewSheet(ref package, lstExcel, ref worksheet, reportParam, sheetName);
            worksheet.DeleteRow(1);
            for (int i = 1; i < 14; i++)
            {
                worksheet.Column(i).AutoFit();
            }
            #endregion

            #region thêm dữ liệu và định dạng các sheet trạng thái công việc
            //1. thêm sheet cho từng trạng thái
            while (lstExcel.Count > 0)
            {
                // package.Workbook.Worksheets.Add("Đang xử lý")
                DataTable dt = new DataTable();
                //1.0 lay trang thai de tao ten sheet.
                int statusId = 0;
                string statusName = "";
                List<ExcelReport> lstWork = new List<ExcelReport>();
                //1.2 lay ten sheet
                GetSatusName(ref lstExcel, ref statusId, ref statusName);
                //1.3 tao ten sheet
                var workSheet = package.Workbook.Worksheets.Add(statusName);
                //1.4 lay du lieu cho sheet
                GetWorkList(ref lstExcel, ref statusId, ref lstWork);
                //1.5 thêm dữ liệu vào sheet
                AddWorkStatusInSheet(ref dt, lstWork, ref workSheet);
                workSheet.Cells["A1"].LoadFromDataTable(dt, true);
                //1.6 căn chỉnh độ rộng ô cho sheet
                for (int i = 1; i < 5; i++)
                {
                    workSheet.Column(i).AutoFit();
                }
                //1.7 xóa dòng định nghĩa.
                workSheet.DeleteRow(1);
            }
            #endregion
            byte[] bytes = package.GetAsByteArray();
            return bytes;

        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : add data for excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="lstWork"></param>
        private void AddWorkStatusInSheet(ref DataTable dt, List<ExcelReport> lstWork, ref ExcelWorksheet worksheet)
        {
            // defined type date.
            int numberOfWork = 0;
            int? groupId = -999;
            int row = 2;// dòng bắt đầu.
            // add columns
            dt.Columns.Add("1", typeof(string));
            dt.Columns.Add("2", typeof(string));
            dt.Columns.Add("3", typeof(string));
            dt.Columns.Add("4", typeof(string));
            dt.Columns.Add("5", typeof(string));
            dt.Rows.Add("");
            // kiểm tra dữ liệu lấy từ data
            foreach (var item in lstWork)
            {
                row++;
                if (item.WorksSupportGroupId == null)
                {
                    dt.Rows.Add("Chưa có báo cáo nào thỏa yêu cầu tìm kiếm!");
                    break;
                }
                else
                {
                    //kiem tra nhom cong viec
                    if (groupId != item.WorksSupportGroupId)
                    {
                        groupId = item.WorksSupportGroupId;
                        GetGroupName(lstWork, groupId, ref numberOfWork);
                        // thêm dòng trống
                        dt.Rows.Add("");
                        row++;// tăng thêm dòng 
                        // thêm tên nhóm công việc.
                        dt.Rows.Add("",
                             item.WorksSupportGroupName + "(" + numberOfWork + ")"
                             );
                        row++;
                        int rowGroup = row - 1;
                        // định dạng đường tên nhóm công việc
                        worksheet.Cells["B" + rowGroup].Style.Font.Bold = true;
                        // thêm title cho cong viec.
                        dt.Rows.Add("",
                            "Công việc",
                            "Hạn xử lý",
                            "Ngày hoàn thành",
                            "Tiến độ(%)"
                            );
                        // định dạng tiêu đề cho công việc
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Font.Bold = true;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Fill.BackgroundColor.SetColor(Color.Green);
                        row++;
                        // thêm chi tiết công việc
                        dt.Rows.Add(
                        "",
                        item.WorksSupportName,
                        item.ExpectedCompletedDate,
                        item.CompletedDate,
                        item.CurrentProgress
                        );
                        // định dạng đường viền cho công việc
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        numberOfWork = 0;
                    }
                    else
                    {
                        // thêm chi tiết công việc
                        dt.Rows.Add(
                         "",
                         item.WorksSupportName,
                         item.ExpectedCompletedDate,
                         item.CompletedDate,
                         item.CurrentProgress
                         );
                        // định dạng đường viền cho công việc
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B" + row + ":" + "E" + row].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
            }
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get status name
        /// </summary>
        /// <param name="lstExcel"></param>
        /// <param name="statusId"></param>
        /// <param name="statusName"></param>
        private void GetSatusName(ref List<ExcelReport> lstExcel, ref int statusId, ref string statusName)
        {
            statusId = lstExcel.Count > 0 ? lstExcel[0].WorksSupportStatusId : 0;
            statusName = lstExcel.Count > 0 ? lstExcel[0].WorksSupportStatusName : "";
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work group name 
        /// </summary>
        /// <param name="lstExcel"></param>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        private void GetGroupName(List<ExcelReport> lstExcel, int? groupId, ref int numberOfWork)
        {
            foreach (var item in lstExcel)
            {
                if (groupId == item.WorksSupportGroupId)
                {
                    numberOfWork++;
                }
            }
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  get work list. 
        /// </summary>
        /// <param name="lstExcel"></param>
        /// <param name="statusId"></param>
        /// <param name="lstWork"></param>
        private void GetWorkList(ref List<ExcelReport> lstExcel, ref int statusId, ref List<ExcelReport> lstWork)
        {
            foreach (var item in lstExcel)
            {
                if (statusId == item.WorksSupportStatusId)
                {
                    lstWork.Add(item);
                }
                else
                {
                    break;
                }
            }
            foreach (var item in lstWork)
            {
                lstExcel.Remove(item);
            }
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : set pie chart into sheet 
        /// </summary>
        /// <param name="excelPackage"></param>
        /// <param name="lstExcel"></param>
        /// <param name="worksheet"></param>
        private void AddPieChartIntoOverViewSheet(ref ExcelPackage excelPackage, List<ExcelReport> lstExcel, ref ExcelWorksheet worksheet)
        {
            List<PieChart> lstPieChart = new List<PieChart>();
            int numberOfWork = 0;
            string statusName = "";
            string color = "";
            int statusId = lstExcel.Count > 0 ? lstExcel[0].WorksSupportStatusId : -9999;
            int initRow = 10;
            int intColumn = 10;
            // get number of work in status.
            var flag = false;
            // dem so cong viec theo trang thai chi mot lan duyet.
            for (int i = 0; i < lstExcel.Count; i++)
            {
                //1.0 kiem tra trang thai khac thi luu lai gia tri vua dem
                if (lstExcel[i].WorksSupportStatusId != statusId)
                {
                    //1.1 neu nhom cong viec is null thì trạng thái chưa có công việc.
                    if (flag)
                    {
                        lstPieChart.Add(new PieChart()
                        {
                            StatusId = statusId,
                            StatusName = statusName,
                            NumberOfWork = 0,
                            ColorCode = color
                        });
                        flag = false;
                    }
                    else
                    {
                        lstPieChart.Add(new PieChart()
                        {
                            StatusId = statusId,
                            StatusName = statusName,
                            NumberOfWork = numberOfWork,
                            ColorCode = color
                        });
                    }
                    numberOfWork = 1;
                    statusId = lstExcel[i].WorksSupportStatusId;
                    //1.2 kiem tra phan tu cuoi mang
                    if (i == lstExcel.Count - 1)
                    {
                        lstPieChart.Add(new PieChart()
                        {
                            StatusId = lstExcel[i].WorksSupportStatusId,
                            StatusName = lstExcel[i].WorksSupportStatusName,
                            NumberOfWork = 1,
                            ColorCode = lstExcel[i].ColorCode
                        });
                    }
                    if (lstExcel[i].WorksSupportGroupId == null)
                    {
                        statusId = lstExcel[i].WorksSupportStatusId;
                        statusName = lstExcel[i].WorksSupportStatusName;
                        color = lstExcel[i].ColorCode;
                        flag = true;
                    }
                }
                else
                {
                    numberOfWork++;
                    statusName = lstExcel[i].WorksSupportStatusName;
                    statusId = lstExcel[i].WorksSupportStatusId;
                    color = lstExcel[i].ColorCode;
                    //1.3 kiem tra phan tu cuoi mang
                    if (i == lstExcel.Count - 1)
                    {
                        lstPieChart.Add(new PieChart()
                        {
                            StatusId = statusId,
                            StatusName = statusName,
                            NumberOfWork = numberOfWork,
                            ColorCode = color
                        });
                    }
                }

            }
            // add data for pieChart into sheet.
            var data = new List<KeyValuePair<string, int>>();
            // add data for piechart
            foreach (var item in lstPieChart)
            {
                data.Add(new KeyValuePair<string, int>(item.StatusName, item.NumberOfWork));
            }
            // Fill the table
            //ExcelRange rangeex = worksheet.Cells["K10:L10"];
            //rangeex.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);
            var startCell = worksheet.Cells[1 + initRow, 1 + intColumn];
            startCell.Offset(0, 0).Value = "Trạng thái công việc";
            startCell.Offset(0, 0).Style.Font.Bold = true;
            startCell.Offset(0, 0).Style.Border.Left.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 0).Style.Border.Top.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 0).Style.Border.Right.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 0).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 1).Value = "Số công việc";
            startCell.Offset(0, 1).Style.Font.Bold = true;
            startCell.Offset(0, 0).Style.Fill.PatternType = ExcelFillStyle.Solid;
            startCell.Offset(0, 0).Style.Fill.BackgroundColor.SetColor(Color.Green);
            startCell.Offset(0, 1).Style.Border.Left.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 1).Style.Border.Top.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 1).Style.Border.Right.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 1).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            startCell.Offset(0, 1).Style.Fill.PatternType = ExcelFillStyle.Solid;
            startCell.Offset(0, 1).Style.Fill.BackgroundColor.SetColor(Color.Green);
            for (var i = 0; i < data.Count; i++)
            {
                startCell.Offset(i + 1, 0).Value = data[i].Key;
                startCell.Offset(i + 1, 1).Value = data[i].Value;
                startCell.Offset(i + 1, 0).Style.Border.Left.Style = ExcelBorderStyle.Thin;
                startCell.Offset(i + 1, 0).Style.Border.Top.Style = ExcelBorderStyle.Thin;
                startCell.Offset(i + 1, 0).Style.Border.Right.Style = ExcelBorderStyle.Thin;
                startCell.Offset(i + 1, 0).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                startCell.Offset(i + 1, 1).Style.Border.Left.Style = ExcelBorderStyle.Thin;
                startCell.Offset(i + 1, 1).Style.Border.Top.Style = ExcelBorderStyle.Thin;
                startCell.Offset(i + 1, 1).Style.Border.Right.Style = ExcelBorderStyle.Thin;
                startCell.Offset(i + 1, 1).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
            // Add the chart to the sheet
            string pieChartName = "Trạng thái công việc cần hỗ trợ";
            var pieChart = worksheet.Drawings.AddChart(pieChartName, eChartType.Pie);
            pieChart.SetPosition(initRow - 1, 0, 0, 0);
            pieChart.SetSize(575, 300);
            pieChart.Title.Text = "Trạng thái công việc cần hỗ trợ";
            pieChart.Title.Font.Bold = true;
            pieChart.Title.Font.Size = 12;
            // pieChart.Series("S1").Points(0).Color = System.Drawing.Color.Teal;
            // Set the data range
            var series = pieChart.Series.Add(worksheet.Cells[2 + initRow - 1, 2 + intColumn, data.Count + 1 + initRow - 1, 2 + intColumn], worksheet.Cells[2 + initRow - 1, 1 + intColumn, data.Count + 1 + initRow - 1, 1 + intColumn]);
            var pieSeries = (ExcelPieChartSerie)series;
            pieSeries.Explosion = 5;
            // Format the labels
            pieSeries.DataLabel.Font.Bold = true;
            pieSeries.DataLabel.ShowValue = true;
            pieSeries.DataLabel.ShowPercent = true;
            pieSeries.DataLabel.ShowLeaderLines = true;
            pieSeries.DataLabel.Separator = ";";
            pieSeries.DataLabel.Position = eLabelPosition.BestFit;
            // Format the legend
            pieChart.Legend.Add();
            pieChart.Legend.Border.Width = 0;
            pieChart.Legend.Font.Size = 12;
            pieChart.Legend.Font.Bold = true;
            pieChart.Legend.Position = eLegendPosition.Right;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : set column chart into sheet
        /// </summary>
        /// <param name="excelPackage"></param>
        /// <param name="lstExcel"></param>
        /// <param name="worksheet"></param>
        private void AddColumnChartIntoOverViewSheet(ref ExcelPackage excelPackage, List<ExcelReport> lstExcel, ref ExcelWorksheet worksheet, SearchReportParam reportParam, string sheetName)
        {
            // get mindate of work
            string minDate = GetMinDateOfWork(reportParam.ProjectId, reportParam.WorkGroupId);
            // get work late
            List<WorkSupportReportTime> lstWorkLate = new List<WorkSupportReportTime>();
            GetWorkLateList(reportParam, ref lstWorkLate);
            // get work wrong
            List<WorkSupportReportTime> lstWorkWrong = new List<WorkSupportReportTime>();
            GetWorkWrongList(reportParam, ref lstWorkWrong);
            // get data for column chart
            List<ColumnChartData> lstDataChart = SetDataForColumnChart(minDate, lstWorkLate, lstWorkWrong, reportParam);
            // tre han
            int startRow = 29;
            int endRow = 29;
            string startCellWTime = "K";
            string startCellWLate = "L";
            string startCellWWrong = "M";
            worksheet.Cells[startCellWLate + startRow].Value = "Trễ hạn";
            worksheet.Cells[startCellWWrong + startRow].Value = "Vi phạm";
            worksheet.Cells[startCellWTime + startRow].Value = "Thời gian";
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + startRow].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + startRow].Style.Fill.BackgroundColor.SetColor(Color.Green);
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + startRow].Style.Font.Bold = true;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + startRow].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + startRow].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + startRow].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + startRow].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            foreach (var item in lstDataChart)
            {
                endRow++;
                worksheet.Cells[startCellWLate + endRow].Value = item.WorkLate;
                worksheet.Cells[startCellWWrong + endRow].Value = item.WorkWrong;
                worksheet.Cells[startCellWTime + endRow].Value = item.WorkTime;
            }
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + endRow].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + endRow].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + endRow].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[startCellWTime + startRow + ":" + startCellWWrong + endRow].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            endRow--;
            string chartName = "Tình trạng công việc cần hỗ trợ";
            var chart = worksheet.Drawings.AddChart(chartName, eChartType.ColumnClustered);
            chart.SetPosition(startRow - 1, 0, 0, 0);
            chart.Title.Text = "Tình trạng công việc cần hỗ trợ";
            chart.SetSize(700, 450);
            chart.EditAs = eEditAs.OneCell;
            chart.DisplayBlanksAs = eDisplayBlanksAs.Span;
            chart.Axis[0].Title.Text = "Thời gian";
            chart.Axis[0].Title.Font.Size = 8;
            chart.Axis[0].Title.Rotation = 0;
            chart.Axis[0].Title.Overlay = false;
            chart.Axis[1].Title.Text = "Công việc";
            chart.Axis[1].Title.Font.Size = 8;
            chart.Axis[1].Title.AnchorCtr = true;
            chart.Axis[1].Title.TextVertical = eTextVerticalType.Vertical270;
            chart.Axis[1].Title.Border.LineStyle = eLineStyle.LongDashDotDot;
            var seriesWorkLate = chart.Series.Add(startCellWLate + startRow + ":" + startCellWLate + endRow, startCellWTime + startRow + ":" + startCellWTime + endRow);
            startRow--;
            seriesWorkLate.HeaderAddress = new ExcelAddress("'" + sheetName + "'!" + startCellWWrong + startRow);
            startRow++;
            var seriesWorkWrong = chart.Series.Add(startCellWWrong + startRow + ":" + startCellWWrong + endRow, startCellWTime + startRow + ":" + startCellWTime + endRow);
            startRow--;
            seriesWorkWrong.HeaderAddress = new ExcelAddress("'" + sheetName + "'!" + startCellWWrong + startRow);
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : get mindate 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="workGroupId"></param>
        /// <returns></returns>
        private string GetMinDateOfWork(int projectId, int? workGroupId)
        {
            try
            {
                string minDate = "";
                new DaReportProcess().LoadDataMinDateWork(ref minDate, projectId, workGroupId);
                return minDate = minDate == null ? DateTime.Now.ToString("dd/mm/yyyy") : minDate;
            }
            catch
            {
                return DateTime.Now.ToString("dd/mm/yyyy");
            }

        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : get work late list
        /// </summary>
        /// <param name="reportParam"></param>
        /// <param name="lstWorkTime"></param>
        private void GetWorkLateList(SearchReportParam reportParam, ref List<WorkSupportReportTime> lstWorkTime)
        {
            lstWorkTime = new List<WorkSupportReportTime>();
            var objResultMessageStatue = new DaReportProcess().LoadWorkStateLateByExcel(ref lstWorkTime, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : get work wrong list
        /// </summary>
        /// <param name="reportParam"></param>
        /// <param name="lstWorkTime"></param>
        private void GetWorkWrongList(SearchReportParam reportParam, ref List<WorkSupportReportTime> lstWorkTime)
        {
            lstWorkTime = new List<WorkSupportReportTime>();
            new DaReportProcess().LoadWorkStateWrongByExcel(ref lstWorkTime, reportParam.ProjectId, reportParam.WorkGroupId, reportParam.FromDate, reportParam.ToDate);
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : set data of column chart. 
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="lstWorkLate"></param>
        /// <param name="lstWorkWrong"></param>
        /// <param name="reportParam"></param>
        /// <returns></returns>
        private List<ColumnChartData> SetDataForColumnChart(string minDate, List<WorkSupportReportTime> lstWorkLate, List<WorkSupportReportTime> lstWorkWrong, SearchReportParam reportParam)
        {

            //string fromDate = reportParam.FromDate != null ? reportParam.FromDate.Value  : GetMinDate(reportParam.ProjectId, reportParam.WorkGroupId);
            DateTime dt = DateTime.Now;
            List<ColumnChartData> lstWorkLateReport = new List<ColumnChartData>();
            string fromDate = reportParam.FromDate != null ? reportParam.FromDate.Value.ToString("MM/yyyy") : minDate.Substring(3, 7); //Split('/')[1]+"/"+ minDate.Split('/')[2];
            string toDate = reportParam.ToDate != null ? reportParam.ToDate.Value.ToString("MM/yyyy") : dt.ToString("MM/yyyy");
            int fmonth = Convert.ToInt32(fromDate.Split('/')[0]);
            int fYear = Convert.ToInt32(fromDate.Split('/')[1]);
            int tMonth = Convert.ToInt32(toDate.Split('/')[0]);
            int tyear = Convert.ToInt32(toDate.Split('/')[1]);
            while (fYear * 100 + fmonth <= tyear * 100 + tMonth)
            {
                //
                var workChart = new ColumnChartData();
                var wLate = lstWorkLate.Find(work => work.WorkTime == (fmonth < 10 ? "0" + fmonth + "/" + fYear : fmonth + "/" + fYear));
                var wWrong = lstWorkWrong.Find(work => work.WorkTime == (fmonth < 10 ? "0" + fmonth + "/" + fYear : fmonth + "/" + fYear));
                // add data for WorkLate
                if (wLate == null)
                {
                    workChart.WorkTime = fmonth + "/" + fYear;
                    workChart.WorkLate = 0;
                }
                else
                {
                    workChart.WorkTime = wLate.WorkTime;
                    workChart.WorkLate = wLate.NumberOfWork;
                }
                // add data for WorkWrong
                if (wWrong == null)
                {
                    workChart.WorkWrong = 0;
                }
                else
                {
                    workChart.WorkWrong = wWrong.NumberOfWork;
                }
                // check date.
                if (fmonth == 12)
                {
                    fYear++;
                    fmonth = 1;
                }
                else
                {
                    fmonth++;
                }
                // add data list
                lstWorkLateReport.Add(workChart);
            }
            return lstWorkLateReport;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : set data for over view sheet
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="reportParam"></param>
        private void SetDataForOverViewSheet(ref DataTable dt, SearchReportParam reportParam)
        {
            dt.Columns.Add("1", typeof(string));
            dt.Columns.Add("2", typeof(string));
            dt.Columns.Add("3", typeof(string));
            dt.Columns.Add("4", typeof(string));
            dt.Columns.Add("5", typeof(string));
            dt.Rows.Add("Báo cáo công việc cần hỗ trợ");
            dt.Rows.Add("Dự án", reportParam.ProjectName);
            dt.Rows.Add("Nhóm công việc", reportParam.WorkGroupName);
            dt.Rows.Add("Thời gian", reportParam.FromDate + "-" + reportParam.ToDate);
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  format line chart data by day
        /// </summary>
        /// <param name="workLate"></param>
        /// <param name="workWrong"></param>
        /// <param name="reportParam"></param>
        /// <param name="lstReport"></param>
        private void HandleLineChartByDay(List<WorkSupportReportTime> workLate, List<WorkSupportReportTime> workWrong, SearchReportParam reportParam, ref List<WorkStateReportByTime> lstReport)
        {
            // check fromdate, toDate is null
            try
            {
                DateTime now = DateTime.Now;
                string fromDate = reportParam.FromDate != null ? reportParam.FromDate.Value.Day + "/" + reportParam.FromDate.Value.Month + "/" + reportParam.FromDate.Value.Year : GetMinDateOfWork(reportParam.ProjectId, reportParam.WorkGroupId);
                string toDate = reportParam.ToDate != null ? reportParam.ToDate.Value.Day + "/" + reportParam.ToDate.Value.Month + "/" + reportParam.ToDate.Value.Year : now.Day + "/" + now.Month + "/" + now.Year;
                int[] arrDayOfLeapMonth = new int[] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int[] arrDayOfNormalMonth = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int fDay = Convert.ToInt32(fromDate.Split('/')[0]);
                int fMonth = Convert.ToInt32(fromDate.Split('/')[1]);
                int fYear = Convert.ToInt32(fromDate.Split('/')[2]);
                int tDay = Convert.ToInt32(toDate.Split('/')[0]);
                int tMonth = Convert.ToInt32(toDate.Split('/')[1]);
                int tYear = Convert.ToInt32(toDate.Split('/')[2]);
                while (fDay + fMonth * 100 + fYear * 10000 <= tDay + tMonth * 100 + tYear * 10000)
                {
                    var objReport = new WorkStateReportByTime();
                    string tempDay = fDay < 10 ? "0" + fDay : fDay.ToString();
                    string tempTime = tempDay + "/" + (fMonth < 10 ? "0" + fMonth + "/" + fYear : fMonth + "/" + fYear);
                    var objWorkLate = workLate.Find(wLate => wLate.WorkTime == tempTime);
                    var objWorkWrong = workWrong.Find(wWrong => wWrong.WorkTime == tempTime);
                    objReport.WorkTime = tempTime;
                    // check work wrong not found
                    if (objWorkWrong == null)
                    {
                        objReport.WorkWrong = 0;
                    }
                    else
                    {
                        objReport.WorkWrong = objWorkWrong.NumberOfWork;
                    }
                    // check work late not null
                    if (objWorkLate == null)
                    {
                        objReport.WorkLate = 0;
                    }
                    else
                    {
                        objReport.WorkLate = objWorkLate.NumberOfWork;
                    }
                    lstReport.Add(objReport);

                    // check day of month
                    if ((fYear % 4 == 0 && fYear % 100 != 0) || fYear % 400 == 0)
                    {
                        fDay++;
                        // check day of leap month.
                        if (fDay > arrDayOfLeapMonth[fMonth])
                        {
                            fDay = 1;
                            fMonth++;
                        }
                        // check month of leap year.
                        if (fMonth > 12)
                        {
                            fMonth = 1;
                            fYear++;
                        }
                    }
                    else
                    {
                        fDay++;
                        // check day of normal month.
                        if (fDay > arrDayOfNormalMonth[fMonth])
                        {
                            fDay = 1;
                            fMonth++;
                        }
                        // check month of normal year.
                        if (fMonth > 12)
                        {
                            fMonth = 1;
                            fYear++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  format line chart data by month
        /// </summary>
        /// <param name="workLate"></param>
        /// <param name="workWrong"></param>
        /// <param name="reportParam"></param>
        /// <param name="lstReport"></param>
        private void HandleLineChartByMonth(List<WorkSupportReportTime> workLate, List<WorkSupportReportTime> workWrong, string strMonth, ref List<WorkStateReportByTime> lstReport)
        {
            // check fromdate, toDate is null
            //try
            //{
            //    DateTime now = DateTime.Now;
            //    string fromDate = reportParam.FromDate != null ? reportParam.FromDate.Value.Month + "/" + reportParam.FromDate.Value.Year : GetMinDateOfWork(reportParam.ProjectId, reportParam.WorkGroupId).Substring(3, 7);
            //    string toDate = reportParam.ToDate != null ? reportParam.ToDate.Value.Month + "/" + reportParam.ToDate.Value.Year : now.Month + "/" + now.Year;
            //    int fMonth = Convert.ToInt32(fromDate.Split('/')[0]);
            //    int fYear = Convert.ToInt32(fromDate.Split('/')[1]);
            //    int tMonth = Convert.ToInt32(toDate.Split('/')[0]);
            //    int tYear = Convert.ToInt32(toDate.Split('/')[1]);
            //    while (fMonth + fYear * 100 <= tMonth + tYear * 100)
            //    {
            //        var objReport = new WorkStateReportByTime();
            //        objReport.WorkTime = (fMonth < 10 ? "0" + fMonth + "/" + fYear : fMonth + "/" + fYear);
            //        var objWorkLate = workLate.Find(wLate => wLate.WorkTime == (fMonth < 10 ? "0" + fMonth + "/" + fYear : fMonth + "/" + fYear));
            //        var objWorkWrong = workWrong.Find(wWrong => wWrong.WorkTime == (fMonth < 10 ? "0" + fMonth + "/" + fYear : fMonth + "/" + fYear));
            //        // check work wrong not found
            //        if (objWorkWrong == null)
            //        {
            //            objReport.WorkWrong = 0;
            //        }
            //        else
            //        {
            //            objReport.WorkWrong = objWorkWrong.NumberOfWork;
            //        }
            //        // check work late not null
            //        if (objWorkLate == null)
            //        {
            //            objReport.WorkLate = 0;
            //        }
            //        else
            //        {
            //            objReport.WorkLate = objWorkLate.NumberOfWork;
            //        }
            //        lstReport.Add(objReport);
            //        // check last month.
            //        if (fMonth == 12)
            //        {
            //            fYear++;
            //            fMonth = 1;
            //        }
            //        else
            //        {
            //            fMonth++;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  format line chart data by year
        /// </summary>
        /// <param name="workLate"></param>
        /// <param name="workWrong"></param>
        /// <param name="reportParam"></param>
        /// <param name="lstReport"></param>
        private void HandleLineChartByYear(List<WorkSupportReportTime> workLate, List<WorkSupportReportTime> workWrong, SearchReportParam reportParam, ref List<WorkStateReportByTime> lstReport)
        {
            // check fromdate, toDate is null
            try
            {
                DateTime now = DateTime.Now;
                string minDate = GetMinDateOfWork(reportParam.ProjectId, reportParam.WorkGroupId) != null ? GetMinDateOfWork(reportParam.ProjectId, reportParam.WorkGroupId) : now.Day + "/" + now.Month + "/" + now.Year;
                int fYear = reportParam.FromDate != null ? reportParam.FromDate.Value.Year : Convert.ToInt32(minDate.Substring(6, 4));
                int tYear = reportParam.ToDate != null ? reportParam.ToDate.Value.Year : now.Year;
                while (fYear <= tYear)
                {
                    var objReport = new WorkStateReportByTime();
                    objReport.WorkTime = fYear.ToString();
                    var objWorkLate = workLate.Find(wLate => wLate.WorkTime == fYear.ToString());
                    var objWorkWrong = workWrong.Find(wWrong => wWrong.WorkTime == fYear.ToString());
                    // check work wrong not found
                    if (objWorkWrong == null)
                    {
                        objReport.WorkWrong = 0;
                    }
                    else
                    {
                        objReport.WorkWrong = objWorkWrong.NumberOfWork;
                    }
                    // check work late not null
                    if (objWorkLate == null)
                    {
                        objReport.WorkLate = 0;
                    }
                    else
                    {
                        objReport.WorkLate = objWorkLate.NumberOfWork;
                    }
                    lstReport.Add(objReport);
                    fYear++;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     :  format line chart data by week
        /// </summary>
        /// <param name="workLate"></param>
        /// <param name="workWrong"></param>
        /// <param name="reportParam"></param>
        /// <param name="lstReport"></param>
        private void HandleLineChartByWeek(List<WorkSupportReportTime> lstWorkLate, List<WorkSupportReportTime> lstWorkWrong, SearchReportWeek reportParam, ref List<WorkStateReportByTime> lstReport)
        {
            // check fromdate, toDate is null
            try
            {
                if (reportParam.Year != null && reportParam.Month != null && reportParam.Week != null)
                {
                    string[] arrWeek = reportParam.Week.Split(',');
                    WorkStateReportByTime report;
                    for (int i = 0; i < reportParam.Week.Length; i++)
                    {
                        report = new WorkStateReportByTime();
                        report.WorkTime = arrWeek[i];
                        var workLate = lstWorkLate.Find(work => work.WorkTime == arrWeek[i]);
                        var workWrong = lstWorkWrong.Find(work => work.WorkTime == arrWeek[i]);
                        report.WorkLate = workLate != null ? workLate.NumberOfWork : 0;
                        report.WorkWrong = workWrong != null ? workWrong.NumberOfWork : 0;
                        lstReport.Add(report);
                    }
                } 
            }
            catch
            {
                throw;
            }

        }
    }
}
