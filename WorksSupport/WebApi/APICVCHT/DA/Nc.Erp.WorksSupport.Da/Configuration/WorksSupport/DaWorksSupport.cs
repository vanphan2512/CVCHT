using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;
using Nc.Erp.WorksSupport.Da.Configuration.Works;
using Nc.Erp.WorksSupport.Do.Configuration.Project;
using Nc.Erp.WorksSupport.Do.Configuration.Work;


namespace Nc.Erp.WorksSupport.Da.Configuration.WorksSupport
{
    using Newtonsoft.Json;

    public class DaWorksSupport
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion
       
        
        #region Public Methods

        /// <summary>
        /// Tìm kiếm thông tin công việc cần hỗ trợ
        /// </summary>
        /// <param name="listWork"></param>
        /// <param name="worksGroupId"></param>
        /// <param name="keyWorks"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<Do.Configuration.WorksSupport.WorksSupport> listWork, int worksGroupId, string keyWorks, int pageIndex, int pageSize, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearch);
                objIData.AddParameter("@WORKSGROUPID", worksGroupId);
                objIData.AddParameter("@KEYWORDS", keyWorks);
                objIData.AddParameter("@PAGEINDEX", pageIndex);
                objIData.AddParameter("@PAGESIZE", pageSize);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupport = new Do.Configuration.WorksSupport.WorksSupport();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWorksSupport.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["CREATEDDATE"])) objWorksSupport.CreatedDate = Convert.ToDateTime(reader["CREATEDDATE"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWorksSupport.Currentprogress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["PROCESSUSER"])) objWorksSupport.ProcessUser = Convert.ToString(reader["PROCESSUSER"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWorksSupport.WorksSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["TOTALROWS"])) objWorksSupport.TotalRow = Convert.ToInt32(reader["TOTALROWS"]);
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"])) objWorksSupport.IsCompleteStatus = Convert.ToBoolean(reader["ISFINISHSTEP"]);
                    
                    var workNextStepPermission = new WorksNextStepPermission();
                    new DaWorks().GetPermissionNextStepByWorksSupportIdAndUserName(objIData, ref workNextStepPermission, objWorksSupport.WorksSupportId, user);
                    if (workNextStepPermission != null)
                    {
                        objWorksSupport.IsCanProcessWorkFlow = workNextStepPermission.IsCanProcessWorkFlow;
                    }
                    listWork.Add(objWorksSupport);
                }
                
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Load công việc cần hỗ trợ
        /// </summary>
        /// <param name="listWorkSupport"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorkSupport> listWorkSupport)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectAll);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupport = new WorkSupport();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"] + "");
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupport.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWorksSupport.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["CONTENT"])) objWorksSupport.Content = Convert.ToString(reader["CONTENT"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"])) objWorksSupport.WorksSupportStatusId = Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWorksSupport.WorkSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]);
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWorksSupport.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWorksSupport.Currentprogress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"])) objWorksSupport.IsCompleteStatus = Convert.ToBoolean(reader["ISFINISHSTEP"]);
                    if (!Convert.IsDBNull(reader["CREATEDUSER"])) objWorksSupport.CreatedUser = Convert.ToString(reader["CREATEDUSER"]).Trim();               

                    listWorkSupport.Add(objWorksSupport);
                }
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupport", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupport -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportType by Id
        /// </summary>
        /// <param name="objWorksSupport"></param>
        /// <param name="workSupportId"></param>
        /// <returns></returns>
        public ResultMessage GetWorksDetailById(ref WorkSupport objWorksSupport, int workSupportId)
        {
            ResultMessage objResultMessage;
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                objIData.CreateNewStoredProcedure("EO_WORKSSUPPORT_DETAIL");
                objIData.AddParameter("@WORKSSUPPORTID", workSupportId);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objWorksSupport = new WorkSupport();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupport.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"])) objWorksSupport.WorksSupportStatusId = Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTQUALITYID"])) objWorksSupport.WorksSupportQualityId = Convert.ToInt32(reader["WORKSSUPPORTQUALITYID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTQUALITYNOTE"])) objWorksSupport.WorksSupportQualityNode = Convert.ToString(reader["WORKSSUPPORTQUALITYNOTE"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPRIORITYID"])) objWorksSupport.WorksSupportPriorityId = Convert.ToInt32(reader["WORKSSUPPORTPRIORITYID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWorksSupport.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWorksSupport.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"])) objWorksSupport.ExpectedCompletedDate = Convert.ToDateTime(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (objWorksSupport.ExpectedCompletedDate == DateTime.MinValue)
                    {
                        objWorksSupport.ExpectedCompletedDate = null;
                    }
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWorksSupport.Currentprogress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["CONTENT"])) objWorksSupport.Content = Convert.ToString(reader["CONTENT"]);
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWorksSupport.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["ATTACHMENTFILECOUNT"])) objWorksSupport.AttachmentFileCount = Convert.ToInt32(reader["ATTACHMENTFILECOUNT"]);
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objWorksSupport.CreatedUser = Convert.ToString(reader["FULLNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"])) objWorksSupport.IsCompleteStatus = Convert.ToBoolean(reader["ISFINISHSTEP"]);
                    
                    if (!Convert.IsDBNull(reader["CREATEDDATE"])) objWorksSupport.CreatedDate = Convert.ToDateTime(reader["CREATEDDATE"]);

                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWorksSupport.WorkSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPRIORITYNAME"])) objWorksSupport.WorkSupportPriorityName = Convert.ToString(reader["WORKSSUPPORTPRIORITYNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["REORITYCOLORCODE"])) objWorksSupport.WorkSupportPriorityColor = Convert.ToString(reader["REORITYCOLORCODE"]).Trim();
                    if (!Convert.IsDBNull(reader["STATUSCOLOR"])) objWorksSupport.WorkSupportStatusColor = Convert.ToString(reader["STATUSCOLOR"]).Trim();
                    
                    // Get infor solution
                    if (!Convert.IsDBNull(reader["SOLUTIONCONTENT"])) objWorksSupport.SolutionContent = Convert.ToString(reader["SOLUTIONCONTENT"]).Trim();
                    if (!Convert.IsDBNull(reader["UPDATESOLUTIONUSER"])) objWorksSupport.SolutionUser = Convert.ToString(reader["UPDATESOLUTIONUSER"]).Trim();
                    if (!Convert.IsDBNull(reader["UPDATESOLUTIONDATE"])) objWorksSupport.SolutionDate = Convert.ToDateTime(reader["UPDATESOLUTIONDATE"]);
                    if (!Convert.IsDBNull(reader["UPDATESOLUTIONUSERURL"])) objWorksSupport.SolutionUserPictureUrl = Convert.ToString(reader["UPDATESOLUTIONUSERURL"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSUSER"])) objWorksSupport.ProcessUser = Convert.ToString(reader["PROCESSUSER"]).Trim();
                    
                }
                // Nạp danh sách công việc liên quan
                var listWorksInvole = new List<WorksInvole>();
                this.GetWorksInvole(ref listWorksInvole, workSupportId, objIData);
                objWorksSupport.ListWorksInvole = listWorksInvole;

                var daSupportAttachment = new DaWorksSupportAttachment();
                var lstWorksSupportAttachment = new List<WorksSupportAttachment>();
                daSupportAttachment.LoadInfo(ref lstWorksSupportAttachment, workSupportId, objIData);
                if (lstWorksSupportAttachment.Count > 0)
                {
                    objWorksSupport.ListWorksSupportAttachment = lstWorksSupportAttachment;
                }

                // Solution Attachment
                var daSupportSolutionAttachment = new DaWorksSupportSolutionAttachment();
                var lstWorksSupportSolutionAttachment = daSupportSolutionAttachment.GetListWorksSupportSolutionAttachment(objIData, workSupportId);
                if (lstWorksSupportSolutionAttachment.Count > 0)
                {
                    objWorksSupport.ListWorksSupportSolutionAttachment = lstWorksSupportSolutionAttachment;
                }

                // Danh sach comment va files
                var daWorksSupportComment = new DaWorksSupportComment();
                var lstWorksSupportComment = new List<WorksSupportComment>();
                daWorksSupportComment.LoadInfo(ref lstWorksSupportComment, workSupportId, objIData);
                if (lstWorksSupportComment.Count > 0)
                {
                    objWorksSupport.ListWorksSupportComment = lstWorksSupportComment;
                }

                var daWorksSupportProgress = new DaWorksSupportProgress();
                var lstWorksSupportProgress = new List<WorksSupportProgress>();
                daWorksSupportProgress.GetWorksSupportProgressByWorkSupportId(ref lstWorksSupportProgress, workSupportId, objIData);
                if (lstWorksSupportProgress.Count > 0)
                {
                    objWorksSupport.ListWorksSupportProgressHistory = lstWorksSupportProgress;
                }
                // Nạp thông tin Bảng chứa các thuộc tính bước xử lý
                var daWorkFlow = new DaWorksSupportWorkflow();
                var lstWorksSupportWorkFlow = new List<WorksSupportWorkflow>();
                //WorksSupport_WorkFlow objWorksSupport_WorkFlow = new WorksSupport_WorkFlow();
                objResultMessage = daWorkFlow.LoadInfo(ref lstWorksSupportWorkFlow, workSupportId, objIData);
                {
                    objWorksSupport.ListWorksSupportWorkFlow = lstWorksSupportWorkFlow;
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get works by WorkSupportId
        /// </summary>
        /// <param name="objWorksSupport"></param>
        /// <param name="workSupportId"></param>
        /// <returns></returns>
        public ResultMessage GetWorksById(ref Do.Configuration.WorksSupport.WorksSupport objWorksSupport, int workSupportId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                objIData.CreateNewStoredProcedure("EO_WORKSSUPPORT_SEL_ID");
                objIData.AddParameter("@WORKSSUPPORTID", workSupportId);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objWorksSupport = new Do.Configuration.WorksSupport.WorksSupport();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWorksSupport.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["CREATEDDATE"])) objWorksSupport.CreatedDate = Convert.ToDateTime(reader["CREATEDDATE"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWorksSupport.Currentprogress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                   
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"])) objWorksSupport.ExpectedCompletedDate = Convert.ToDateTime(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (objWorksSupport.ExpectedCompletedDate == DateTime.MinValue)
                    {
                        objWorksSupport.ExpectedCompletedDate = null;
                    }
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWorksSupport.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["CONTENT"])) objWorksSupport.Content = Convert.ToString(reader["CONTENT"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPRIORITYID"])) objWorksSupport.WorksSupportPriorityId = Convert.ToInt32(reader["WORKSSUPPORTPRIORITYID"]);
                    if (!Convert.IsDBNull(reader["ATTACHMENTFILECOUNT"]))
                        objWorksSupport.AttachmentFileCount = Convert.ToInt32(reader["ATTACHMENTFILECOUNT"]);
                }
                var listWorksInvole = new List<WorksInvole>();
                this.GetWorksInvole(ref listWorksInvole, workSupportId, objIData);
                objWorksSupport.ListWorksInvole = listWorksInvole;

                // Nạp thông tin Bảng liên kết loại công việc và độ ưu tiên
                var daSupportAttachment = new DaWorksSupportAttachment();
                var lstWorksSupportAttachment = new List<WorksSupportAttachment>();
                daSupportAttachment.LoadInfo(ref lstWorksSupportAttachment, workSupportId, objIData);
                objWorksSupport.ListWorksSupportAttachment = lstWorksSupportAttachment;
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get danh sach cong viec lien quan
        /// </summary>
        /// <param name="listWorksInvole"></param>
        /// <param name="worksId"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage GetWorksInvole(ref List<WorksInvole> listWorksInvole, int worksId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                objIData.CreateNewStoredProcedure(SpSelectGetWorksInvoleByWorksId);
                objIData.AddParameter("@WORKSSUPPORTID", worksId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksInvole = new WorksInvole();
                    if (!Convert.IsDBNull(reader["WORKSID"]))
                        objWorksInvole.WorksId = Convert.ToInt32(reader["WORKSID"]);

                    if (!Convert.IsDBNull(reader["WORKSNAME"]))
                        objWorksInvole.WorksName = Convert.ToString(reader["WORKSNAME"]).Trim();

                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"]))
                        objWorksInvole.CurrentProcess = Convert.ToInt32(reader["CURRENTPROGRESS"]);

                    if (!Convert.IsDBNull(reader["STARTDATE"]))
                        objWorksInvole.StartDate = Convert.ToDateTime(reader["STARTDATE"]);

                    if (!Convert.IsDBNull(reader["ENDDATE"]))
                        objWorksInvole.EndDate = Convert.ToDateTime(reader["ENDDATE"]);

                    listWorksInvole.Add(objWorksInvole);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách công việc liên quan", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaProjects -> GetWorksInvole", "");
                return objResultMessage;
            }
            
            return objResultMessage;
        }


        /// <summary>
        /// Xóa công việc cần hỗ trợ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="worksSupportId"></param>
        /// <returns></returns>
        public ResultMessage Delete(string user, int worksSupportId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                //objIData.BeginTransaction();
                    // Xoa cong viec can ho tro
                DeleteWorkSupport(objIData, user, worksSupportId);
                var daAttachment = new DaWorksSupportAttachment();
                
                daAttachment.Delete(worksSupportId, objIData, user);

                    var daComment = new DaWorksSupportComment();
                var objComment = new WorksSupportComment
                {
                    WorksSupportId = worksSupportId,
                };
                daComment.Delete(objComment, objIData, user);

                    DaWorksSupportMember daMember = new DaWorksSupportMember();
                    WorksSupportMember objMember = new WorksSupportMember();
                    objMember.WorksSupportId = worksSupportId;
                    objMember.DeletedUser = user;
                    daMember.Delete(objMember, objIData);

                    var daProgress = new DaWorksSupportProgress();
                    daProgress.Delete(worksSupportId, objIData);
                
                    var daWorkflow = new DaWorksSupportWorkflow();
                
                daWorkflow.Delete(worksSupportId, objIData, user);
                
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSuportStatus", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportStatus -> Delete", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Load danh sach buoc xu ly ke tiep bang workssupportId
        /// </summary>
        /// <param name="listWorksSupportNextStep"></param>
        /// <param name="workSupportId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //ref WorkSupport objWorksSupport
        //ref DataTable dtbData
        public ResultMessage LoadWorksSupportWfNx(ref List<WorksSupportNextStep> listWorksSupportNextStep, int workSupportId, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpLoadWfNx);
                objIData.AddParameter("@WORKSSUPPORTID", workSupportId);
                objIData.AddParameter("@USER", user);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var worksSupportNextStep = new WorksSupportNextStep();
                    if (!Convert.IsDBNull(reader["NEXTWORKSSUPPORTSTEPID"])) worksSupportNextStep.NextWorksSupportStepId = Convert.ToInt32(reader["NEXTWORKSSUPPORTSTEPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPNAME"])) worksSupportNextStep.WorksSupportStepName = Convert.ToString(reader["WORKSSUPPORTSTEPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ISMUSTCHOOSEPROCESSUSER"])) worksSupportNextStep.IsMustChooseProcessUser = Convert.ToBoolean(reader["ISMUSTCHOOSEPROCESSUSER"]);
                    listWorksSupportNextStep.Add(worksSupportNextStep);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupport", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupport -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                  objIData.Disconnect();
            }
             return objResultMessage;
        }

        /// <summary>
        /// Load Danh sach cong viec tu nhom cong viec
        /// </summary>
        /// <param name="listWork"></param>
        /// <param name="workSupportGroupId"></param>
        /// <returns></returns>
        public ResultMessage GetByIdGroup(ref List<WorkSupport> listWork, int workSupportGroupId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpLoadGroup);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", workSupportGroupId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupport = new WorkSupport();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupport.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWorksSupport.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["CONTENT"])) objWorksSupport.Content = Convert.ToString(reader["CONTENT"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"]))
                        objWorksSupport.WorksSupportStatusId = Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"]))
                        objWorksSupport.ExpectedCompletedDate = Convert.ToDateTime(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (objWorksSupport.ExpectedCompletedDate == DateTime.MinValue)
                    {
                        objWorksSupport.ExpectedCompletedDate = null;
                    }
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"]))
                        objWorksSupport.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWorksSupport.Currentprogress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["ATTACHMENTFILECOUNT"])) objWorksSupport.AttachmentFileCount = Convert.ToInt32(reader["ATTACHMENTFILECOUNT"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWorksSupport.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPRIORITYID"])) objWorksSupport.WorksSupportPriorityId = Convert.ToInt32(reader["WORKSSUPPORTPRIORITYID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTQUALITYID"])) objWorksSupport.WorksSupportQualityId = Convert.ToInt32(reader["WORKSSUPPORTQUALITYID"]);
                    if (!Convert.IsDBNull(reader["SOLUTIONCONTENT"])) objWorksSupport.SolutionContent = Convert.ToString(reader["SOLUTIONCONTENT"]);
                    listWork.Add(objWorksSupport);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupport", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupport -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// THEM CONG VIEC CAN HO TRO
        /// </summary>
        /// <param name="objWorksSupport"></param>
        /// <param name="listInvoleId"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResultMessage InsertWorksSupport(WorkSupport objWorksSupport, string listInvoleId, ref WorkSupport obj, string user, string listAttachment, string listDeletedAttachment)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                if (objWorksSupport.WorksSupportId > 0)
                {
                    Update(objIData, objWorksSupport, user);
                    // Delete cong viec lien quan by worksId
                    new DaWorks().DeleteByWorkSupportId(objIData, objWorksSupport.WorksSupportId);

                    // Insert Công việc liên quan
                    if (!string.IsNullOrWhiteSpace(listInvoleId))
                    {
                        var listId = listInvoleId.Split(',');
                        foreach (var id in listId)
                        {
                            new DaWorks().InsertWorksInvoleByWorkSupportIdAndWorkId(objIData, objWorksSupport.WorksSupportId, int.Parse(id));
                        }
                    }
                    if (!string.IsNullOrEmpty(listAttachment))
                    {
                        dynamic rs = JsonConvert.DeserializeObject(listAttachment);
                        if (rs.Count > 0)
                        {
                            var daWorksSupportAttachment = new DaWorksSupportAttachment();
                            foreach (var r in rs)
                            {
                                var item = new WorksSupportAttachment
                                               {
                                                   WorksSupportId = objWorksSupport.WorksSupportId,
                                                   FileId = r.FILEID,
                                                   FileName = r.FILENAME,
                                                   FilePath = r.FILEPATH
                                               };
                                daWorksSupportAttachment.Insert(objIData, item, user);
                            }
                        }
                    }

                    // Delete attachment file
                    if (!string.IsNullOrEmpty(listDeletedAttachment))
                    {
                        var listDeleted = listDeletedAttachment.Split(',');
                        foreach (var file in listDeleted)
                        {
                            new DaWorksSupportAttachment().DeleteWorkSupportAttachmentByAttachment(user, file);
                        }
                    }
                }
                else
                {
                    // Insert WorksSupport
                    var nextWorksId = 0;
                    InsertWorksSupport(objIData, objWorksSupport, ref nextWorksId);
                    if (nextWorksId > 0)
                    {
                        // Insert Công việc liên quan
                        if (!string.IsNullOrWhiteSpace(listInvoleId))
                        {
                            var listId = listInvoleId.Split(',');
                            foreach (var id in listId)
                            {
                                new DaWorks().InsertWorksInvoleByWorkSupportIdAndWorkId(objIData, nextWorksId, int.Parse(id));
                            }
                        }
                        // Insert WorksSupport_Attachment
                        if (!string.IsNullOrEmpty(listAttachment))
                        {
                            dynamic rs = JsonConvert.DeserializeObject(listAttachment);
                            if (rs.Count > 0)
                            {
                                var daWorksSupportAttachment = new DaWorksSupportAttachment();
                                foreach (var r in rs)
                                {
                                    var item = new WorksSupportAttachment
                                                   {
                                                       WorksSupportId = nextWorksId,
                                                       FileId = r.FILEID,
                                                       FileName = r.FILENAME,
                                                       FilePath = r.FILEPATH
                                                   };
                                    daWorksSupportAttachment.Insert(objIData, item, user);
                                }
                            }
                        }
                    }
                }
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupport", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupport -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Load danh sach buoc xu ly ke tiep bang workssupportId
        /// </summary>
        /// <param name="listWorksSupportProcessMember"></param>
        /// <param name="worksId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public ResultMessage LoadProcessUserByWorkIdAndStepId(ref List<WorksSupportProcessMember> listWorksSupportProcessMember, int worksId, int stepId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpLoadProcessUser);
                objIData.AddParameter("@WORKSSUPPORTID", worksId);
                objIData.AddParameter("@WORKSSUPPORTSTEPID", stepId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupport = new WorksSupportProcessMember();
                    if (!Convert.IsDBNull(reader["USERNAME"])) objWorksSupport.UserName = Convert.ToString(reader["USERNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objWorksSupport.FullName = Convert.ToString(reader["FULLNAME"]).Trim();
                    
                    listWorksSupportProcessMember.Add(objWorksSupport);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupport", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupport -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        #endregion
       
        #region Protected Methods

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="objIData">
        /// The obj i data.
        /// </param>
        /// <param name="objWorksSupport">
        /// The obj works support.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        protected virtual void Update(IData objIData, WorkSupport objWorksSupport, string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);
                objIData.AddParameter("@WORKSSUPPORTID", objWorksSupport.WorksSupportId);
                objIData.AddParameter("@WORKSSUPPORTNAME", objWorksSupport.WorksSupportName);
                objIData.AddParameter("@CONTENT", objWorksSupport.Content);
                if (objWorksSupport.ExpectedCompletedDate != DateTime.MinValue)
                {
                    objIData.AddParameter("@EXPECTEDCOMPLETEDDATE", objWorksSupport.ExpectedCompletedDate);
                }
                else
                {
                    objIData.AddParameter("@EXPECTEDCOMPLETEDDATE", "");
                }
                objIData.AddParameter("@WORKSSUPPORTPRIORITYID", objWorksSupport.WorksSupportPriorityId );
                objIData.AddParameter("@WORKSSUPPORTGROUPID", objWorksSupport.WorksSupportGroupId);
                objIData.AddParameter("@UPDATEDUSER", user);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);
                objIData.ExecNonQuery();

            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Xóa trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="user"></param>
        /// <param name="worksSupportId"></param>
        protected void DeleteWorkSupport(IData objIData, string user, int worksSupportId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WORKSSUPPORTID", worksSupportId);
                objIData.AddParameter("@DELETEDUSER", user);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// THEM CONG VIEC CAN HO TRO
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupport"></param>
        /// <param name="worksId"></param>
        /// <returns></returns>
        protected virtual void InsertWorksSupport(IData objIData, WorkSupport objWorksSupport, ref int worksId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAddWorks);
                objIData.AddParameter("@WORKSSUPPORTNAME", objWorksSupport.WorksSupportName);
                objIData.AddParameter("@CONTENT", objWorksSupport.Content);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", objWorksSupport.WorksSupportGroupId);
                objIData.AddParameter("@WORKSSUPPORTPRIORITYID", objWorksSupport.WorksSupportPriorityId);
                if (objWorksSupport.ExpectedCompletedDate != DateTime.MinValue)
                {
                    objIData.AddParameter("@EXPECTEDCOMPLETEDDATE", objWorksSupport.ExpectedCompletedDate);
                }
                else
                {
                    objIData.AddParameter("@EXPECTEDCOMPLETEDDATE", "");
                }
                objIData.AddParameter("@CREATEDUSER", objWorksSupport.CreatedUser);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);
                worksId = Convert.ToInt32(objIData.ExecStoreToString());
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        #endregion

        #region Stored Procedure Names

        public const string SpSelectAll = "WORKSSUPPORT_V2_GETALL";
        public const string SpAddWorks = "WORKSSUPPORT_V2_ADD";
        public const string SpUpdate = "WORKS_SUPPORT_V2_UPD";
        public const string SpDelete = "WORKSSUPPORT_V2_DELETE";
        public const string SpSearch = "EO_WORKSSUPPORT_SEARCH";
        public const string SpLoadWfNx = "WORKSSUPPORT_WFNX_V2_GETBY";
        public const string SpLoadGroup = "WORKSSUPPORT_GROUP_V2_SHR";
        public const string SpLoadProcessUser = "WORKSSUPPORT_WFNX_V2_MEMBER";
        public const string SpSelectGetWorksInvoleByWorksId = "EO_WORKS_GET_WORKSSUPPORTID";

        #endregion
    }
}
