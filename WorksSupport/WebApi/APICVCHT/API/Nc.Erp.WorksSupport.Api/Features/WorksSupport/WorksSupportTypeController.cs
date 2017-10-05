﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nc.Erp.WorksSupport.Api.Common;
using Nc.Erp.WorksSupport.Api.Models.WorksSupportType;
using Nc.Erp.WorksSupport.Da.Configuration.WorkSupportType;
using Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType;
using Newtonsoft.Json;

namespace Nc.Erp.WorksSupport.Api.Features.WorksSupport
{
    /// <summary>
    /// Created by      : Lương Trung Nhân
    /// Created date    : 30/05/2017
    /// Api độ trạng thái "ERP.EO.WorksSupportType"
    /// </summary>
    [RoutePrefix("api/v2/workssupporttypes")]
    public class WorksSupportTypeController : BaseController
    {
        /// <summary>
        /// Tìm kiếm chất lượng yêu cầu công việc cần hỗ trợ theo keyword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public Response<List<WorksSupportType>> Search([FromUri] SearchParam objparam)
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportType>>();
            try
            {
                // Definition object return
                var listWorksSupportType = new List<WorksSupportType>();
                if (objparam.Keywords == null)
                {
                    objparam.Keywords = "";
                }
                //object param
                object[] objKeywords = {
                    "@Keywords", objparam.Keywords,
                    "@PageSize", objparam.PageSize,
                    "@PageIndex", objparam.PageIndex,
                    "@IsDeleted",objparam.IsDeleted
                };
                // Call DaSystemManagement to search data.
                var objResultMessage = new DaWorksSupportType().SearchData(ref listWorksSupportType, objKeywords);

                if (listWorksSupportType != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportType;
                    ret.TotalRecord = listWorksSupportType.Count;
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
        /// Lấy danh sách loại công việc yêu cầu cần hỗ trợ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public Response<List<WorksSupportType>> GetAll()
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportType>>();
            try
            {
                // Definition object return
                var listWorksSupportType = new List<WorksSupportType>();

                // Call DaSystemManagement to search data.
                var objResultMessage = new DaWorksSupportType().GetAll(ref listWorksSupportType);

                if (!objResultMessage.IsError && listWorksSupportType != null && listWorksSupportType.Count > 0)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportType;
                    ret.TotalRecord = listWorksSupportType.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessage.Message;
                    // ret.Error = "";
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
        [Route("worksTypeActived")]
        public Response<List<WorksSupportType>> GetWorksSupportTypeActived()
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportType>>();
            try
            {
                // Definition object return
                var listWorksSupportType = new List<WorksSupportType>();

                // Call DaSystemManagement to search data.
                var objResultMessage = new DaWorksSupportType().GetAllActived(ref listWorksSupportType);

                if (!objResultMessage.IsError && listWorksSupportType != null && listWorksSupportType.Count > 0)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportType;
                    ret.TotalRecord = listWorksSupportType.Count;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessage.Message;
                    // ret.Error = "";
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
        /// Lấy chi tiết loại công việc yêu cầu cần hỗ trợ theo ID
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id}")]
        public Response<WorksSupportType> GetById([FromUri] GetByParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportType>();
            try
            {
                //validate param
                ValidateParams(objparam);
                // Definition object return
                var obj = new WorksSupportType();

                // Call DaWorksSupportType to search data.
                var objResultMessage = new DaWorksSupportType().GetById(ref obj, objparam.Id);

                if (obj != null)
                {
                    if (obj.WorksSupportTypeId > 0)
                    {
                        ret.Success = true;
                        ret.Data = obj;
                        ret.TotalRecord = 1;
                    }
                    else
                    {
                        ret.Success = true;
                        ret.Data = null;
                        ret.TotalRecord = 0;
                    }
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
        /// Lấy chi tiết quyền trên loại công việc yêu cầu cần hỗ trợ theo ID
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Permis/{Id}")]
        public Response<WorksSupportType> GetWorksSupportPermisById([FromUri] GetByParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportType>();
            try
            {
                //validate param
                ValidateParams(objparam);
                // Definition object return
                var obj = new WorksSupportType();

                // Call DaWorksSupportType to search data.
                var objResultMessage = new DaWorksSupportType().GetWorksSupportPermisById(ref obj, objparam.Id);

                if (obj != null && !objResultMessage.IsError)
                {
                    ret.Success = true;
                    ret.Data = obj;
                    ret.TotalRecord = 1;
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
        /// Xóa loại công việc yêu cầu cần hỗ trợ theo ID
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Deleted")]
        public Response<WorksSupportType> Delete([FromBody] DeleteParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportType>();
            try
            {
                //Validate param
                ValidateParams(objparam);
                // Definition object return
                var obj = new WorksSupportType { WorksSupportTypeId = objparam.Id };
                // Call DaWorksSupportType to search data.
                var objResultMessage = new DaWorksSupportType().Delete(obj, objparam.User);

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

        /// <summary>
        /// Thêm loại công việc yêu cầu cần hỗ trợ
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public Response<WorksSupportType> SaveData([FromBody] SaveParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportType>();
            try
            {
                //validate param
                ValidateParams(objparam);
                // Definition object return
                //WorksSupportType_Priority
                var lstWorksSupportTypePriority = new List<WorksSupportTypePriority>();
                if (objparam.lstWorksSupportType_Priority != null && objparam.lstWorksSupportType_Priority.Count > 0)
                {
                    lstWorksSupportTypePriority.AddRange(objparam.lstWorksSupportType_Priority.Select(item => new WorksSupportTypePriority()
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        WorksSupportPriorityId = item.WorksSupportPriorityId
                    }));
                }
                ////WorksSupportType_Priority
                var lstWorksSupportTypeQuality = new List<WorksSupportTypeQuality>();
                if (objparam.lstWorksSupportType_Priority != null && objparam.lstWorksSupportType_Quality.Count > 0)
                {
                    lstWorksSupportTypeQuality.AddRange(objparam.lstWorksSupportType_Quality.Select(item => new WorksSupportTypeQuality()
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        WorksSupportQualityId = item.WorksSupportQualityId
                    }));
                }
                ////WorksSupportType_MemberRole
                var lstWorksSupportTypeMemberRole = new List<WorksSupportTypeMemberRole>();
                if (objparam.lstWorksSupportType_MemberRole != null && objparam.lstWorksSupportType_MemberRole.Count > 0)
                {
                    lstWorksSupportTypeMemberRole.AddRange(objparam.lstWorksSupportType_MemberRole.Select(item => new WorksSupportTypeMemberRole
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        WorksSupportMemberRoleName = item.WorksSupportMemberRoleName,
                        WorksSupportMemberRoleId = item.WorksSupportTypeId,
                        IsCanEditWorksSupportGroup = item.IsCanEditWorksSupportGroup,
                        IsCanEditWorksSupport = item.IsCanEditWorksSupport,
                        IsCanDeleteWorksSupportGroup = item.IsCanDeleteWorksSupportGroup,
                        IsCanDeleteWorksSupport = item.IsCanDeleteWorksSupport,
                        IsCanAddWorksSupportGroup = item.IsCanAddWorksSupportGroup,
                        IsCanAddWorksSupport = item.IsCanAddWorksSupport
                    }));
                }
                ////WorksSupportType_MemberRole
                var lstWorksSupportProjectPermis = new List<WorksSupportProjectPermis>();
                if (objparam.lstWorksSupportProject_Permis != null && objparam.lstWorksSupportProject_Permis.Count > 0)
                {
                    lstWorksSupportProjectPermis.AddRange(objparam.lstWorksSupportProject_Permis.Select(item => new WorksSupportProjectPermis
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        UserName = item.UserName,
                        IsCanViewProject = item.IsCanViewProject,
                        IsCanDeleteProject = item.IsCanDeleteProject,
                        IsCanAddProject = item.IsCanAddProject,
                        IsCanEditProject = item.IsCanEditProject
                    }));
                }
                // WorksSupportType_WorkFlow
                var lstWorksSupportTypeWorkFlow = new List<WorksSupportTypeWorkFlow>();
                if (objparam.lstWorksSupportType_WorkFlow != null && objparam.lstWorksSupportType_WorkFlow.Count > 0)
                {
                    foreach (var item in objparam.lstWorksSupportType_WorkFlow)
                    {
                        var lstWorksSupportTypeWfPermis = new List<WorksSupportTypeWfPermis>();
                        if (item.LstWorksSupportTypeWfPermis != null && item.LstWorksSupportTypeWfPermis.Count > 0)
                        {
                            lstWorksSupportTypeWfPermis.AddRange(item.LstWorksSupportTypeWfPermis.Select(itemWorksSupportTypeWfPermis => new WorksSupportTypeWfPermis
                            {
                                WorksSupportStepId = itemWorksSupportTypeWfPermis.WorksSupportStepId,
                                WorksSupportMemberRoleId = itemWorksSupportTypeWfPermis.WorksSupportMemberRoleId
                            }));
                        }
                        var objWorkFlow = new WorksSupportTypeWorkFlow
                        {
                            WorksSupportTypeId = item.WorksSupportTypeId,
                            WorksSupportStepId = item.WorksSupportStepId,
                            WorksSupportStepName = item.WorksSupportStepName,
                            StepColorCode = item.Stepcolorcode,
                            ProcessFunctionId = item.ProccessFunctionId,
                            OrderIndex = item.OrderIndex,
                            MaxProcessTime = item.Maxprocesstime,
                            IsSystem = item.IsSystem,
                            IsMustChooseProcessUser = item.Ismustchooseprocessuser,
                            IsInitStep = item.Isinitstep,
                            IsFinishStep = item.Isfinishstep,
                            IsCanUpDateQuality = item.IsCanUppateQuality,
                            IsCanUpdateProcess = item.IsCanUpdateProcess,
                            IsCanModifiedContentSolution = item.IsCanModifiedContentSolution,
                            IsCanModifiedContent = item.IsCanModifiedContent,
                            IsCanModifiedCompletedDate = item.IsCanModifiedCompletedDate,
                            IsCanComment = item.IsCanComment,
                            IsCanAttach = item.IsCanattach,
                            IsActived = item.IsActived,
                            Description = item.Description,
                            AutoChangeToStatusId = item.Autochangetostatusid,
                            // ListWorksSupportTypeWfNx = lstWorksSupportTypeWfNx,
                            ListWorksSupportTypeWfPermis = lstWorksSupportTypeWfPermis
                        };
                        lstWorksSupportTypeWorkFlow.Add(objWorkFlow);
                    }
                }
                var obj = new WorksSupportType()
                {
                    WorksSupportTypeId = objparam.Id,
                    WorksSupportTypeName = objparam.WorksSupportTypeName,
                    IconUrl = objparam.IconURL,
                    AddFunctionId = objparam.AddFunctionId,
                    ViewAllFunctionId = objparam.ViewAllFunctionId,
                    EditFunctionId = objparam.EditFunctionId,
                    EditAllFunctionId = objparam.EditAllFunctionId,
                    DeleteFunctionId = objparam.DeleteFunctionId,
                    DeleteAllFunctionId = objparam.DeleteAllFunctionId,
                    ProcessFunctionId = objparam.ProcessFunctionId,
                    CommentFunctionId = objparam.CommentFunctionId,
                    Description = objparam.Description,
                    IsActived = objparam.IsActived,
                    IsSystem = objparam.IsSystem,
                    ListWorksSupportTypePriority = lstWorksSupportTypePriority,
                    ListWorksSupportTypeQuality = lstWorksSupportTypeQuality,
                    ListWorksSupportTypeWorkFlow = lstWorksSupportTypeWorkFlow,
                    ListWorksSupportTypeMemberRole = lstWorksSupportTypeMemberRole,
                    ListWorksSupportProjectPermis = lstWorksSupportProjectPermis
                };

                // Call DaWorksSupportType to search data.
                var objResultMessage = new DaWorksSupportType().Insert(obj, objparam.CreatedUser);

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

        /// <summary>
        /// Thêm loại công việc yêu cầu cần hỗ trợ
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public Response<WorksSupportType> UpdateData([FromBody] UpdateParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportType>();
            try
            {
                //validate param
                ValidateParams(objparam);
                // Definition object return
                //WorksSupportType_Priority
                var lstWorksSupportTypePriority = new List<WorksSupportTypePriority>();
                if (objparam.lstWorksSupportType_Priority != null && objparam.lstWorksSupportType_Priority.Count > 0)
                {
                    lstWorksSupportTypePriority.AddRange(objparam.lstWorksSupportType_Priority.Select(item => new WorksSupportTypePriority()
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        WorksSupportPriorityId = item.WorksSupportPriorityId
                    }));
                }
                ////WorksSupportType_Priority
                var lstWorksSupportTypeQuality = new List<WorksSupportTypeQuality>();
                if (objparam.lstWorksSupportType_Priority != null && objparam.lstWorksSupportType_Quality.Count > 0)
                {
                    lstWorksSupportTypeQuality.AddRange(objparam.lstWorksSupportType_Quality.Select(item => new WorksSupportTypeQuality()
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        WorksSupportQualityId = item.WorksSupportQualityId
                    }));
                }
                ////WorksSupportType_MemberRole
                var lstWorksSupportTypeMemberRole = new List<WorksSupportTypeMemberRole>();
                if (objparam.lstWorksSupportType_MemberRole != null && objparam.lstWorksSupportType_MemberRole.Count > 0)
                {
                    lstWorksSupportTypeMemberRole.AddRange(objparam.lstWorksSupportType_MemberRole.Select(item => new WorksSupportTypeMemberRole()
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        WorksSupportMemberRoleName = item.WorksSupportMemberRoleName,
                        WorksSupportMemberRoleId = item.WorksSupportTypeId,
                        IsCanEditWorksSupportGroup = item.IsCanEditWorksSupportGroup,
                        IsCanEditWorksSupport = item.IsCanEditWorksSupport,
                        IsCanDeleteWorksSupportGroup = item.IsCanDeleteWorksSupportGroup,
                        IsCanDeleteWorksSupport = item.IsCanDeleteWorksSupport,
                        IsCanAddWorksSupportGroup = item.IsCanAddWorksSupportGroup,
                        IsCanAddWorksSupport = item.IsCanAddWorksSupport,
                    }));
                }
                // WorksSupportType_MemberRole
                var lstWorksSupportProjectPermis = new List<WorksSupportProjectPermis>();
                if (objparam.lstWorksSupportProject_Permis != null && objparam.lstWorksSupportProject_Permis.Count > 0)
                {
                    lstWorksSupportProjectPermis.AddRange(objparam.lstWorksSupportProject_Permis.Select(item => new WorksSupportProjectPermis()
                    {
                        WorksSupportTypeId = item.WorksSupportTypeId,
                        UserName = item.UserName,
                        IsCanViewProject = item.IsCanViewProject,
                        IsCanDeleteProject = item.IsCanDeleteProject,
                        IsCanAddProject = item.IsCanAddProject,
                        IsCanEditProject = item.IsCanEditProject
                    }));
                }
                // WorksSupportType_WorkFlow
                var lstWorksSupportTypeWorkFlow = new List<WorksSupportTypeWorkFlow>();
                if (objparam.lstWorksSupportType_WorkFlow != null && objparam.lstWorksSupportType_WorkFlow.Count > 0)
                {
                    foreach (var item in objparam.lstWorksSupportType_WorkFlow)
                    {
                        ////WorksSupportType_WF_NX
                        var lstWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();
                        ////WorksSupportType_WF_
                        var lstWorksSupportTypeWfPermis = new List<WorksSupportTypeWfPermis>();
                        if (item.LstWorksSupportTypeWfPermis != null && item.LstWorksSupportTypeWfPermis.Count > 0)
                        {
                            lstWorksSupportTypeWfPermis.AddRange(item.LstWorksSupportTypeWfPermis.Select(itemWorksSupportTypeWfPermis => new WorksSupportTypeWfPermis()
                            {
                                WorksSupportStepId = itemWorksSupportTypeWfPermis.WorksSupportStepId,
                                WorksSupportMemberRoleId = itemWorksSupportTypeWfPermis.WorksSupportMemberRoleId
                            }));
                        }
                        var objWorkFlow = new WorksSupportTypeWorkFlow()
                        {
                            WorksSupportTypeId = item.WorksSupportTypeId,
                            WorksSupportStepId = item.WorksSupportStepId,
                            WorksSupportStepName = item.WorksSupportStepName,
                            StepColorCode = item.Stepcolorcode,
                            ProcessFunctionId = item.ProccessFunctionId,
                            OrderIndex = item.OrderIndex,
                            MaxProcessTime = item.Maxprocesstime,
                            IsSystem = item.IsSystem,
                            IsMustChooseProcessUser = item.Ismustchooseprocessuser,
                            IsInitStep = item.Isinitstep,
                            IsFinishStep = item.Isfinishstep,
                            IsCanUpDateQuality = item.IsCanUppateQuality,
                            IsCanUpdateProcess = item.IsCanUpdateProcess,
                            IsCanModifiedContentSolution = item.IsCanModifiedContentSolution,
                            IsCanModifiedContent = item.IsCanModifiedContent,
                            IsCanModifiedCompletedDate = item.IsCanModifiedCompletedDate,
                            IsCanComment = item.IsCanComment,
                            IsCanAttach = item.IsCanattach,
                            IsActived = item.IsActived,
                            Description = item.Description,
                            AutoChangeToStatusId = item.Autochangetostatusid,
                            ListWorksSupportTypeWfNx = lstWorksSupportTypeWfNx,
                            ListWorksSupportTypeWfPermis = lstWorksSupportTypeWfPermis
                        };
                        lstWorksSupportTypeWorkFlow.Add(objWorkFlow);
                    }
                }
                var obj = new WorksSupportType
                {
                    WorksSupportTypeId = objparam.Id,
                    WorksSupportTypeName = objparam.WorksSupportTypeName,
                    IconUrl = objparam.IconURL,
                    AddFunctionId = objparam.AddFunctionId,
                    ViewAllFunctionId = objparam.ViewAllFunctionId,
                    EditFunctionId = objparam.EditFunctionId,
                    EditAllFunctionId = objparam.EditAllFunctionId,
                    DeleteFunctionId = objparam.DeleteFunctionId,
                    DeleteAllFunctionId = objparam.DeleteAllFunctionId,
                    ProcessFunctionId = objparam.ProcessFunctionId,
                    CommentFunctionId = objparam.CommentFunctionId,
                    Description = objparam.Description,
                    IsActived = objparam.IsActived,
                    IsSystem = objparam.IsSystem,
                    ListWorksSupportTypePriority = lstWorksSupportTypePriority,
                    ListWorksSupportTypeQuality = lstWorksSupportTypeQuality,
                    ListWorksSupportTypeWorkFlow = lstWorksSupportTypeWorkFlow,
                    ListWorksSupportTypeMemberRole = lstWorksSupportTypeMemberRole,
                    ListWorksSupportProjectPermis = lstWorksSupportProjectPermis
                };

                // Call DaWorksSupportType to search data.
                var objResultMessage = new DaWorksSupportType().UpdateWorksType(obj, objparam.UpdatedUser, null);
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
        /// <summary>
        /// Tìm kiếm thông tin vai trò của một công việc cần hỗ trợ
        /// pageindex, pagesize mặc định  -1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("memberrole/search")]
        public Response<List<WorksSupportTypeMemberRole>> SearchMemberRole([FromUri] SearchParam objparam)
        {
            // Definition object to return.
            var ret = new Response<List<WorksSupportTypeMemberRole>>();
            try
            {
                // Definition object return
                var listWorksSupportTypeMemberRole = new List<WorksSupportTypeMemberRole>();

                //object param
                var objKeywords = new object[]
                {
                    "@Keywords", objparam.Keywords,
                    "@PageSize", objparam.PageSize,
                    "@PageIndex", objparam.PageIndex,
                    "@IsDeleted",objparam.IsDeleted
                };
                // Call DaSystemManagement to search data.
                var objResultMessage = new DaWorksSupportTypeMemberRole().SearchDataMemberRole(ref listWorksSupportTypeMemberRole, objKeywords);

                if (listWorksSupportTypeMemberRole != null)
                {
                    ret.Success = true;
                    ret.Data = listWorksSupportTypeMemberRole;
                    ret.TotalRecord = listWorksSupportTypeMemberRole.Count;
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
        /// Lấy chi tiết thông tin vai trò của một công việc cần hỗ trợ theo ID
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("memberrole/{Id}")]
        public Response<WorksSupportTypeMemberRole> GetMemberRoleById([FromUri] GetByParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportTypeMemberRole>();
            try
            {
                //validate param
                ValidateParams(objparam);
                // Definition object return
                var obj = new WorksSupportTypeMemberRole();

                // Call DaWorksSupportType to search data.
                var objResultMessage = new DaWorksSupportTypeMemberRole().GetMemberRoleById(ref obj, objparam.Id);

                if (obj != null)
                {
                    ret.Success = true;
                    ret.Data = obj;
                    ret.TotalRecord = 1;
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
        /// Thêm loại công việc yêu cầu cần hỗ trợ
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertBy")]
        public Response<WorksSupportType> SaveData([FromBody] SaveWorkTypeParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportType>();
            try
            {
                // vlidate param
                // ValidateParams(objparam);
                var objCheck = new WorksSupportTypeValidation();
                var listError = new List<ValidationError>();
                new DaWorksSupportType().CheckValuesExisted(ref objCheck, objparam.WorksSupportTypeId, objparam.WorksSupportTypeName, objparam.OrderIndex);
                if (objCheck.CountName >= 1)
                {
                    var error = new ValidationError
                    {
                        Key = "WorksSupportTypeName",
                        Message = "Tên loại công việc đã tồn tại"
                    };
                    listError.Add(error);
                    ret.Success = false;
                    ret.Errors = listError;
                    return ret;
                }
                var worksSupportTypeId = objparam.WorksSupportTypeId;
                var listWorksFlowDeletedId = objparam.DeletedStepId;
                string[] priority = null;
                if (objparam.Priority != "")
                {
                    priority = objparam.Priority.Split(',');
                }
                string[] quality = null;

                if (objparam.Quality != "")
                {
                    quality = objparam.Quality.Split(',');
                }

                // WorksSupportType_Priority
                var lstWorksSupportTypePriority = new List<WorksSupportTypePriority>();
                if (priority != null && priority.Length > 0)
                {
                    lstWorksSupportTypePriority.AddRange(priority.Where(t => !string.IsNullOrWhiteSpace(t))
                        .Select(t => new WorksSupportTypePriority
                        {
                            WorksSupportTypeId = worksSupportTypeId,
                            WorksSupportPriorityId = Convert.ToInt16(t.ToString())
                        }));
                }

                // WorksSupportType_Priority
                var lstWorksSupportTypeQuality = new List<WorksSupportTypeQuality>();
                if (quality != null && quality.Length > 0)
                {
                    lstWorksSupportTypeQuality.AddRange(from t in quality
                                                        where !string.IsNullOrWhiteSpace(t)
                                                        select new WorksSupportTypeQuality
                                                        {
                                                            WorksSupportTypeId = worksSupportTypeId,
                                                            WorksSupportQualityId = Convert.ToInt16(t)
                                                        });
                }

                var lstWorksSupportTypeWorkFlow = new List<WorksSupportTypeWorkFlow>();
                dynamic rs = JsonConvert.DeserializeObject(objparam.Step);
                foreach (var i in rs)
                {
                    var lstWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();
                    var stepId = i.StepID;
                    var stepName = i.StepNAME;
                    var description = i.StepDESCRIPTION;
                    var stepStepColorCode = i.StepSTEPCOLORCODE;
                    var isMustChooseProcessUser = (i.isMustChooseProcessUser.ToString()) == "True" ? 1 : 0;
                    var stepMaxProcessTime = 0;
                    if (i.StepMAXPROCESSTIME != null)
                    {
                        stepMaxProcessTime = int.Parse(i.StepMAXPROCESSTIME.ToString());
                    }
                    var worksSupportStatusId = 0;
                    if (i.StepSTATUS != null)
                    {
                        worksSupportStatusId = int.Parse(i.StepSTATUS.ToString());
                    }
                    var worksSupportStepProgress = 0;
                    if (i.WORKSSUPPORTSTEPPROGRESS != null)
                    {
                        worksSupportStepProgress = int.Parse(i.WORKSSUPPORTSTEPPROGRESS.ToString());
                    }
                    var stepIsInitStep = i.StepISINITSTEP;
                    var stepIsFinishStep = i.StepISFINISHSTEP;
                    var worksFlow = new WorksSupportTypeWorkFlow
                    {
                        WorksSupportStepId = stepId,
                        Description = description,
                        WorksSupportTypeId = worksSupportTypeId,
                        WorksSupportStepName = stepName,
                        StepColorCode = stepStepColorCode,
                        MaxProcessTime = stepMaxProcessTime,
                        IsInitStep = stepIsInitStep,
                        IsFinishStep = stepIsFinishStep,
                        WorksSupportStatusId = worksSupportStatusId,
                        WorksSupportStepProgress = worksSupportStepProgress,
                        IsMustChooseProcessUser = isMustChooseProcessUser
                    };

                    var listNextStep = i.listNextStep;
                    if (listNextStep.Count > 0)
                    {
                        foreach (var item in listNextStep)
                        {
                            var nextStep = new WorksSupportTypeWfNx
                            {
                                WorksSupportStepId = item
                            };
                            lstWorksSupportTypeWfNx.Add(nextStep);
                        }
                    }
                    worksFlow.ListWorksSupportTypeWfNx = lstWorksSupportTypeWfNx;
                    lstWorksSupportTypeWorkFlow.Add(worksFlow);
                }

                var objWorksType = new WorksSupportType
                {
                    WorksSupportTypeId = objparam.WorksSupportTypeId,
                    WorksSupportTypeName = objparam.WorksSupportTypeName,
                    IconUrl = objparam.IconUrL,
                    Description = objparam.Description,
                    IsActived = objparam.IsActived,
                    IsSystem = objparam.IsSystem,
                    OrderIndex = objparam.OrderIndex,
                    ListWorksSupportTypePriority = lstWorksSupportTypePriority,
                    ListWorksSupportTypeQuality = lstWorksSupportTypeQuality,
                    ListWorksSupportTypeWorkFlow = lstWorksSupportTypeWorkFlow
                };

                // Call DaWorksSupportType to search data.
                if (objWorksType.WorksSupportTypeId == 0)
                {
                    var objResultMessage = new DaWorksSupportType().Insert(objWorksType, objparam.User);

                    if (!objResultMessage.IsError)
                    {
                        ret.Success = true;
                        ret.Data = objWorksType;
                    }
                    else
                    {
                        ret.Success = false;
                        ret.Message = objResultMessage.Message;
                    }
                }
                else
                {
                    var objResultMessage = new DaWorksSupportType().UpdateWorksType(objWorksType, objparam.User, listWorksFlowDeletedId);

                    if (!objResultMessage.IsError)
                    {
                        ret.Success = true;
                        ret.Data = objWorksType;
                    }
                    else
                    {
                        ret.Success = false;
                        ret.Message = objResultMessage.Message;
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
        /// Xóa trạng thái công việc cần hỗ trợ theo ID
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Del")]
        public Response<WorksSupportType> DeleteWorksType([FromBody] DeleteWorksTypeParam objparam)
        {
            // Definition object to return.
            var ret = new Response<WorksSupportType>();
            try
            {
                //Validate param
                ValidateParams(objparam);
                // Call DaWorksSupportStatus to search data.
                var objResultMessage = new DaWorksSupportType().DeleteWorksType(objparam.User, objparam.Id);

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

        [HttpPost]
        [Route("SaveOrUpdatePermission")]
        public Response<WorksSupportType> SavePermission([FromBody]  string param)
        {
            try
            {
                // Definition object to return.
                var ret = new Response<WorksSupportType>();
                var listError = new List<ValidationError>();
                var objResultMessage = new DaWorksSupportType().SaveEditPermission(param);
                if (!objResultMessage.IsError)
                {
                    ret.Success = true;
                }
                else
                {
                    var error = new ValidationError { Key = "fail", Message = "Thêm thất bại!" };
                    ret.Success = false;
                    ret.Message = objResultMessage.Message;
                    ret.Errors = listError;
                    listError.Add(error);
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

        [HttpPost]
        [Route("copyPermission")]
        public Response<WorksSupportType> CopyPermission([FromBody] CopyPermissionParam param)
        {
            try
            {
                // Definition object to return.
                var ret = new Response<WorksSupportType>();
                var objResultMessage = new DaWorksSupportType().CopyWorksType(param.FromWorksTypeId, param.ToWorksTypeId);
                if (!objResultMessage.IsError)
                {
                    ret.Success = true;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessage.Message;
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

        [HttpPost]
        [Route("copyworkssupporttype")]
        public Response<WorksSupportType> CopyWorksSupportType([FromBody] CopyWorksSupportTypeParam param)
        {
            try
            {
                // Definition object to return.
                var ret = new Response<WorksSupportType>();

                // Definition object return
                var obj = new WorksSupportType();

                // Call DaWorksSupportType to search data.
                var objResultMessage = new DaWorksSupportType().GetById(ref obj, int.Parse(param.WorksTypeId));
                if (obj != null)
                {
                    new DaWorksSupportType().CopyWorksType(obj, param.User);
                }
                if (!objResultMessage.IsError)
                {
                    ret.Success = true;
                }
                else
                {
                    ret.Success = false;
                    ret.Message = objResultMessage.Message;
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
        /// kiem tra loai cong viec 
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckWorkTypeIsUsed/{Id}")]
        public Response<WorksSupportType> CheckWorkTypeIsUsed([FromUri] GetByParam objparam)
        {
            try
            {
                // Definition object to return.
                var ret = new Response<WorksSupportType>();
                var objkWorkType = new WorksSupportType();
                var objResultMessage = new DaWorksSupportType().CheckWorkTypeIsUsed(ref objkWorkType, objparam.Id);
                if (objkWorkType != null)
                {
                    ret.Success = true;
                    ret.Data = objkWorkType;
                    ret.TotalRecord = 1;
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
