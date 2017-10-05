using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.ReportProces;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;
using System.Data;

namespace Nc.Erp.WorksSupport.Da.Configuration.ReportProces
{
    public class DaReportProcess
    {
        /// <summary>
        /// Created by      : Nguyen Thi Kim Ngan
        /// Created date    : 20/06/2017
        /// Description     :Bao cao tien do cong viec can ho tro.
        /// </summary>
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion

        #region Public Methods


        /// Tìm kiếm thông tin công việc cần hỗ trợ
        /// </summary>
        /// <param name="dtbData"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData_Works(ref List<WorkSupport> list, string KeyWorks, int WorksGroupId, int WorksProject, string MemberUser, int KeyStatus)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                objIData.CreateNewStoredProcedure(SpSearchRepHr);
                objIData.AddParameter("@KEYWORDS", KeyWorks);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", WorksGroupId);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", WorksProject);
                objIData.AddParameter("@MEMBERUERNAME", MemberUser);
                objIData.AddParameter("@KEYSTATUS", KeyStatus);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    WorkSupport objWorksSupport = new WorkSupport();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupport.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWorksSupport.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]);
                    if (!Convert.IsDBNull(reader["CONTENT"])) objWorksSupport.Content = Convert.ToString(reader["CONTENT"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"])) objWorksSupport.WorksSupportStatusId = Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"])) objWorksSupport.ExpectedCompletedDate = Convert.ToDateTime(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (objWorksSupport.ExpectedCompletedDate == DateTime.MinValue)
                    {
                        objWorksSupport.ExpectedCompletedDate = null;
                    }
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWorksSupport.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWorksSupport.Currentprogress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["CREATEDUSER"])) objWorksSupport.CreatedUser = Convert.ToString(reader["CREATEDUSER"]);
                    if (!Convert.IsDBNull(reader["ATTACHMENTFILECOUNT"])) objWorksSupport.AttachmentFileCount = Convert.ToInt32(reader["ATTACHMENTFILECOUNT"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWorksSupport.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPRIORITYID"])) objWorksSupport.WorksSupportPriorityId = Convert.ToInt32(reader["WORKSSUPPORTPRIORITYID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTQUALITYID"])) objWorksSupport.WorksSupportQualityId = Convert.ToInt32(reader["WORKSSUPPORTQUALITYID"]);
                    if (!Convert.IsDBNull(reader["SOLUTIONCONTENT"])) objWorksSupport.SolutionContent = Convert.ToString(reader["SOLUTIONCONTENT"]);
                    if (!Convert.IsDBNull(reader["ISINITSTATUS"])) objWorksSupport.IsinitStatus = Convert.ToString(reader["ISINITSTATUS"]);
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"])) objWorksSupport.IsCompleteStatus = Convert.ToBoolean(reader["ISFINISHSTEP"]);
                    if (!Convert.IsDBNull(reader["KHOITAO"])) objWorksSupport.KhoiTao = Convert.ToInt32(reader["KHOITAO"]);
                    if (!Convert.IsDBNull(reader["XULY"])) objWorksSupport.XuLy = Convert.ToInt32(reader["XULY"]);
                    if (!Convert.IsDBNull(reader["TREHAN"])) objWorksSupport.TreHan = Convert.ToInt32(reader["TREHAN"]);
                    if (!Convert.IsDBNull(reader["HOANTAT"])) objWorksSupport.HoanTat = Convert.ToInt32(reader["HOANTAT"]);

                    list.Add(objWorksSupport);

                    //// Nap thong tin nguoi thuc hien theo du an 
                    // if (objWorksSupport.WorksSupportProjectId > 0)
                    // {

                    //     DaWorksSupportProjectMember DaProject_Member = new DaWorksSupportProjectMember();
                    //     List<WorksSupportProject_Member> lstProject_Member = new List<WorksSupportProject_Member>();
                    //     objResultMessage = DaProject_Member.GetById(ref lstProject_Member, WorksProject, objIData);
                    //     if (!objResultMessage.IsError && lstProject_Member != null && lstProject_Member.Count > 0)
                    //     {
                    //         objWorksSupport.lstWorksSupportProject_Member =lstProject_Member;
                    //     }
                    // }
                    // // Nap thong tin nguoi thuc hien theo nhom cong viec 
                    // if (objWorksSupport.WorksSupportGroupId > 0)
                    // {
                    //     // Nap thong tin nguoi thuc hien theo du an 
                    //     DaWorksSupportGroupMember DaGroup_Member = new DaWorksSupportGroupMember();
                    //     List<WorksSupportGroupMember> lstGroup_Member = new List<WorksSupportGroupMember>();
                    //     objResultMessage = DaGroup_Member.GetById(ref lstGroup_Member, WorksGroupId, objIData);
                    //     if (!objResultMessage.IsError && lstGroup_Member != null && lstGroup_Member.Count > 0)
                    //     {
                    //         objWorksSupport.lstWorksSupportGroup_Member = lstGroup_Member;
                    //     }
                    // }

                }
                objIData.CommitTransaction();
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// xuat bao cao tien do cua cong viec can ho tro theo du an
        /// </summary>
        /// <param name="dtbData"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchReportByProject(ref List<WorkSupport> list, int WorksGroupId, int WorksProject)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchRep);
                objIData.AddParameter("@PROJECT", WorksProject);
                //var data = objIData.ExecStoreToDataSet("V_OUT1", "V_OUT2");
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    WorkSupport objWorksSupport = new WorkSupport();
                    if (!Convert.IsDBNull(reader["SUMGROUP"])) objWorksSupport.SumGroup = Convert.ToInt32(reader["SUMGROUP"]);
                    if (!Convert.IsDBNull(reader["SUMWORKS"])) objWorksSupport.SumWorks = Convert.ToInt32(reader["SUMWORKS"]);
                    if (!Convert.IsDBNull(reader["SUMPERSON"])) objWorksSupport.SumPerson = Convert.ToInt32(reader["SUMPERSON"]);
                    if (!Convert.IsDBNull(reader["SUMSTATUS"])) objWorksSupport.SumStatus = Convert.ToInt32(reader["SUMSTATUS"]);

                    list.Add(objWorksSupport);
                }
                if (WorksProject > 0)
                {
                    WorkSupport objWorksSupport = new WorkSupport();
                    List<WorkSupport> listGroup = new List<WorkSupport>();
                    // objResultMessage = DaReportProcess.SearchWorksByGroup(WorksGroupId);
                }

                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi xuat du lieu bao cao tien do", objEx.ToString());
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
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach du an
        /// </summary>
        /// <param name="lstProject"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ResultMessage LoadProjectByUser(ref List<WorkSupportProject> lstProject, string userName)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchProjectByUser);
                objIData.AddParameter("@USERNAME", userName);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    WorkSupportProject objProject = new WorkSupportProject();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTID"])) objProject.WorksSupportProjectId = Convert.ToInt32(reader["WORKSSUPPORTPROJECTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTNAME"])) objProject.WorksSupportProjectName = Convert.ToString(reader["WORKSSUPPORTPROJECTNAME"]);
                    lstProject.Add(objProject);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách dự án báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadProjectByUser", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach nhom cong viec
        /// </summary>
        /// <param name="lstProject"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ResultMessage LoadWorksGroupByProjectId(ref List<WorkSupportGroup> lstWorksGroup, int projectId)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkGroupByProjectId);
                objIData.AddParameter("@PROJECTID", projectId);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objProject = new WorkSupportGroup();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objProject.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objProject.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]);
                    lstWorksGroup.Add(objProject);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách nhom cong viec báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorksGroupByProjectId", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec  theo trang thai
        /// </summary>
        /// <param name="lstWorkLate"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadReportByWorkStatus(ref List<WorkSupportStatus> lstWorkLate, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStatus);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWork = new WorkSupportStatus();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"])) objWork.StatusId = Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWork.StatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]);
                    if (!Convert.IsDBNull(reader["COLORCODE"])) objWork.ColorCode = Convert.ToString(reader["COLORCODE"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWork.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    lstWorkLate.Add(objWork);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách nhom cong viec bi tre cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadReportByWorkStatus", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec tinh trang
        /// </summary>
        /// <param name="objReport"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadReportByWorkState(ref Object objReport, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByState);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objReport = new
                    {
                        NumberOfWorkLate = !Convert.IsDBNull(reader["NUMBEROFWORKLATE"]) ? Convert.ToInt32(reader["NUMBEROFWORKLATE"]) : 0,
                        NumberOfWorkWrong = !Convert.IsDBNull(reader["NUMBEROFWORKWRONG"]) ? Convert.ToInt32(reader["NUMBEROFWORKWRONG"]) : 0
                    };
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách nhom cong viec bi tre cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadReportByWorkState", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec tre theo tinh trang  
        /// </summary>
        /// <param name="lstReportLate"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateLateByDay(ref List<WorkSupportReportTime> lstReportLate, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateLateDay);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportLate = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMELATE"])) objReportLate.WorkTime = Convert.ToString(reader["TIMELATE"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKLATE"])) objReportLate.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKLATE"]);
                    lstReportLate.Add(objReportLate);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec bi tre cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateLateByDay", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec tre theo tinh trang  tuan
        /// </summary>
        /// <param name="lstReportLate"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="date"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateLateByWeek(ref List<WorkSupportReportTime> lstReportLate, int projectId, int? worGroupId, string date)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateLateWeek);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", date);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportLate = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMELATE"])) objReportLate.WorkTime = Convert.ToString(reader["TIMELATE"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKLATE"])) objReportLate.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKLATE"]);
                    lstReportLate.Add(objReportLate);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec bi tre cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateLateByWeek", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }       
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec tre theo tinh trang  
        /// </summary>
        /// <param name="lstReportLate"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateLateByMonth(ref List<WorkSupportReportTime> lstReportLate, int projectId, int? worGroupId, string date)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateLateMonth);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", date);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportLate = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMELATE"])) objReportLate.WorkTime = Convert.ToString(reader["TIMELATE"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKLATE"])) objReportLate.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKLATE"]);
                    lstReportLate.Add(objReportLate);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec bi tre cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateLateByMonth", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec tre theo tinh trang  
        /// </summary>
        /// <param name="lstReportLate"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateLateByYear(ref List<WorkSupportReportTime> lstReportLate, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateLateYear);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportLate = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMELATE"])) objReportLate.WorkTime = Convert.ToString(reader["TIMELATE"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKLATE"])) objReportLate.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKLATE"]);
                    lstReportLate.Add(objReportLate);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec bi tre theo nam cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateLateByYear", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec vi pham theo tinh trang  
        /// </summary>
        /// <param name="lstReportWrong"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateWrongByDay(ref List<WorkSupportReportTime> lstReportWrong, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateWrongDay);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportWrong = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMEWRONG"])) objReportWrong.WorkTime = Convert.ToString(reader["TIMEWRONG"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKWRONG"])) objReportWrong.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKWRONG"]);
                    lstReportWrong.Add(objReportWrong);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec vi pham theo ngay cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateWrongByDay", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec vi pham theo tinh trang  
        /// </summary>
        /// <param name="lstReportWrong"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="date"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateWrongByWeek(ref List<WorkSupportReportTime> lstReportWrong, int projectId, int? worGroupId, string date)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateWrongWeek);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", date);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportWrong = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMEWRONG"])) objReportWrong.WorkTime = Convert.ToString(reader["TIMEWRONG"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKWRONG"])) objReportWrong.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKWRONG"]);
                    lstReportWrong.Add(objReportWrong);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec vi pham theo tuan cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateWrongByWeek", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec vi pham theo tinh trang  
        /// </summary>
        /// <param name="lstReportWrong"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateWrongByMonth(ref List<WorkSupportReportTime> lstReportWrong, int projectId, int? worGroupId, string date)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateWrongMonth);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", date);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportWrong = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMEWRONG"])) objReportWrong.WorkTime = Convert.ToString(reader["TIMEWRONG"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKWRONG"])) objReportWrong.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKWRONG"]);
                    lstReportWrong.Add(objReportWrong);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec vi pham cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateWrongByMonth", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach cong viec vi pham theo tinh trang  
        /// </summary>
        /// <param name="lstReportWrong"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateWrongByYear(ref List<WorkSupportReportTime> lstReportWrong, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateWrongYear);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportWrong = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMEWRONG"])) objReportWrong.WorkTime = Convert.ToString(reader["TIMEWRONG"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKWRONG"])) objReportWrong.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKWRONG"]);
                    lstReportWrong.Add(objReportWrong);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec vi pham theo nam cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateWrongByYear", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lay danh sach nhom cong viec theo trang thai cong viec
        /// </summary>
        /// <param name="lstReportWrong"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadReportByWorkGroupStatus(ref List<WorkSupportGroup> lstGroup, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate, int statusId)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkGroupReportByWorkGroupStatus);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                objIData.AddParameter("@WORKSSUPPORTSTATUSID", statusId);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorkGroup = new WorkSupportGroup();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWorkGroup.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objWorkGroup.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWorkGroup.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    lstGroup.Add(objWorkGroup);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách nhóm công việc theo trạng thái cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadReportByWorkGroupStatus", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lấy danh sách công việc theo nhóm công việc với trạng thái công việc.
        /// </summary>
        /// <param name="lstWork"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public ResultMessage LoadWorksStatusByWorkGroupId(ref List<WorkSupportDetail> lstWork, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate, int statusId)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByWorkGroupIdStatus);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                objIData.AddParameter("@WORKSSUPPORTSTATUSID", statusId);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWork = new WorkSupportDetail();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWork.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWork.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]);
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"])) objWork.ExpectedCompletedDate = Convert.ToDateTime(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWork.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWork.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWork.CurrentProgress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["LATEDATE"])) objWork.NumberOfLateDate = Convert.ToInt32(reader["LATEDATE"]) < 0 ? 0 : Convert.ToInt32(reader["LATEDATE"]);
                    lstWork.Add(objWork);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách công việc theo trạng thái công việc và nhóm công việc cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorksByWorkGroupIdStatus", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lấy danh sách nhóm công việc theo kiểu nhóm công việc.
        /// </summary>
        /// <param name="lstWorkGroup"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkGroupByGroupType(ref List<WorkSupportGroup> lstWorkGroup, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkGroupReportByWorkGroup);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWork = new WorkSupportGroup();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWork.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objWork.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWork.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    lstWorkGroup.Add(objWork);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp chi tiết danh sách nhóm công việc theo kiểu nhóm công việc cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkGroupByGroupType", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lấy danh sách công việc theo nhóm công việc.
        /// </summary>
        /// <param name="lstWork"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkDetailsByGroupType(ref List<WorkSupportDetail> lstWork, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkDetailReportByWorkGroup);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWork = new WorkSupportDetail();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWork.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWork.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWork.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWork.WorksSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]);
                    if (!Convert.IsDBNull(reader["LATEDATE"])) objWork.NumberOfLateDate = Convert.ToInt32(reader["LATEDATE"]) < 0? 0: Convert.ToInt32(reader["LATEDATE"]);
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"])) objWork.ExpectedCompletedDate = Convert.ToDateTime(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWork.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWork.CurrentProgress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    lstWork.Add(objWork);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp chi tiết danh sách nhóm công việc theo kiểu nhóm công việc cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkGroupByGroupType", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lấy danh sách nhóm công việc theo tình trạng công việc.
        /// </summary>
        /// <param name="lstWork"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkGroupsByState(ref List<WorkSupportGroup> lstWork,int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate, int type)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkGroupReportByState);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                objIData.AddParameter("@TYPE", type);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWork = new WorkSupportGroup();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWork.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objWork.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWork.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    lstWork.Add(objWork);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách nhóm công việc theo tình trạng công việc cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkGroupsByState", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lấy danh sách công việc theo tình trạng công việc.
        /// </summary>
        /// <param name="lstWork"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkDetailsByStateType(ref List<WorkSupportDetail> lstWork, int? worGroupId, DateTime? fromDate, DateTime? toDate, int type)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkDetailReportByState);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                objIData.AddParameter("@TYPE", type);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWork = new WorkSupportDetail();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWork.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWork.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWork.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWork.CurrentProgress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWork.WorksSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]);
                    if (!Convert.IsDBNull(reader["LATEDATE"])) objWork.NumberOfLateDate = Convert.ToInt32(reader["LATEDATE"]) < 0 ? 0 : Convert.ToInt32(reader["LATEDATE"]);
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"])) objWork.ExpectedCompletedDate = Convert.ToDateTime(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWork.CompletedDate = Convert.ToDateTime(reader["COMPLETEDDATE"]);
                    lstWork.Add(objWork);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp chi tiết danh sách nhóm công việc theo kiểu nhóm công việc cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkGroupByGroupType", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstWork"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResultMessage LoadDataForExcelReport(ref List<ExcelReport> lstExcel, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkDetailReporExcel);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWork = new ExcelReport();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWork.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objWork.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"])) objWork.WorksSupportStatusId = Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWork.WorksSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWork.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["EXPECTEDCOMPLETEDDATE"])) objWork.ExpectedCompletedDate = Convert.ToString(reader["EXPECTEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["COMPLETEDDATE"])) objWork.CompletedDate = Convert.ToString(reader["COMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTNAME"])) objWork.WorksSupportName = Convert.ToString(reader["WORKSSUPPORTNAME"]);
                    if (!Convert.IsDBNull(reader["CURRENTPROGRESS"])) objWork.CurrentProgress = Convert.ToInt32(reader["CURRENTPROGRESS"]);
                    if (!Convert.IsDBNull(reader["COLORCODE"])) objWork.ColorCode = Convert.ToString(reader["COLORCODE"]);
                    lstExcel.Add(objWork);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp du lieu xuat excel", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadDataForExcelReport", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Created by      : Nguyen van huan
        /// Created date    : 11/09/2017
        /// Description     : lấy thoi gian cong viec tao dau tien
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <returns></returns>
        public ResultMessage LoadDataMinDateWork(ref string minDate, int projectId, int? worGroupId)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkMinDateReporExcel);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                IDataReader reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    var objWork = new ExcelReport();
                    if (!Convert.IsDBNull(reader["DATETIME"])) minDate = Convert.ToString(reader["DATETIME"]);
                     
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi lay thoi gian cong viec tao dau tien", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadDataMinDateWork", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstReportLate"></param>
        /// <param name="projectId"></param>
        /// <param name="worGroupId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ResultMessage LoadWorkStateLateByExcel(ref List<WorkSupportReportTime> lstReportLate, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateLateExcel);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportLate = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMELATE"])) objReportLate.WorkTime = Convert.ToString(reader["TIMELATE"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKLATE"])) objReportLate.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKLATE"]);
                    lstReportLate.Add(objReportLate);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec bi tre cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateLateByMonth", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        public ResultMessage LoadWorkStateWrongByExcel(ref List<WorkSupportReportTime> lstReportWrong, int projectId, int? worGroupId, DateTime? fromDate, DateTime? toDate)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchWorkReportByStateWrongExcel);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", projectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worGroupId);
                objIData.AddParameter("@FROMDATE", fromDate);
                objIData.AddParameter("@TODATE", toDate);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objReportWrong = new WorkSupportReportTime();
                    if (!Convert.IsDBNull(reader["TIMEWRONG"])) objReportWrong.WorkTime = Convert.ToString(reader["TIMEWRONG"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKWRONG"])) objReportWrong.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORKWRONG"]);
                    lstReportWrong.Add(objReportWrong);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi nạp danh sách cong viec vi pham cho báo cáo", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> LoadWorkStateWrongByMonth", Globals.ModuleName);
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
        /// nap du lieu cong viec theo nhom thuoc du an
        /// </summary>
        /// <param name="dtbData"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        //public ResultMessage SearchWorksByGroup(ref List<WorkSupport> list, int WorksGroupId)
        //{
        //    ResultMessage objResultMessage = new ResultMessage();
        //    IData objIData = Data.CreateData();
        //    try
        //    {
        //        objIData.Connect();
        //        objIData.CreateNewStoredProcedure(SpSearchRep);
        //        objIData.AddParameter("@GROUPID", WorksGroupId);
        //        var data = objIData.ExecStoreToDataSet("V_OUT1", "V_OUT2");
        //        IDataReader reader = objIData.ExecStoreToDataReader();
        //        while (reader.Read())
        //        {
        //            WorkSupport objWorksSupport = new WorkSupport();
        //            if (!Convert.IsDBNull(reader["SUMGROUP"])) objWorksSupport.SumGroup = Convert.ToInt32(reader["SUMGROUP"]);
        //            if (!Convert.IsDBNull(reader["SUMWORKS"])) objWorksSupport.SumWorks = Convert.ToInt32(reader["SUMWORKS"]);
        //            if (!Convert.IsDBNull(reader["SUMPERSON"])) objWorksSupport.SumPerson = Convert.ToInt32(reader["SUMPERSON"]);
        //            if (!Convert.IsDBNull(reader["SUMSTATUS"])) objWorksSupport.SumStatus = Convert.ToInt32(reader["SUMSTATUS"]);
        //            list.Add(objWorksSupport);
        //        }

        //        reader.Close();
        //    }
        //    catch (Exception objEx)
        //    {
        //        objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi xuat du lieu bao cao tien do", objEx.ToString());
        //        ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport -> SearchData", Globals.ModuleName);
        //        return objResultMessage;
        //    }
        //    finally
        //    {
        //        objIData.Disconnect();
        //    }
        //    return objResultMessage;
        //}
        #endregion

        #region Stored Procedure Names
        public const string SpSearch = "WORKSSUPPORTGROUP_V2_SEARCH";
        public const string SpSearchRep = "EO_WORKSSUPPORT_REPEXC";
        public const string SpSearchRepHr = "EO_WORKSSUPPORT_REPSHR";
        public const string SpSearchProjectByUser = "WORKSSUPORTREPORT_V2_PROJECT";
        public const string SpSearchWorkGroupByProjectId = "WORKSSUPPORTREPORT_V2_GETGROUP";
        public const string SpSearchWorkReportByStatus = "WORKSSUPPORTREPORT_V2_STATUS";
        public const string SpSearchWorkReportByState = "WORKSSUPPORTREPORT_V2_STATE";
        public const string SpSearchWorkReportByStateLateDay = "WS_REPORT_V2_WL_STATE_DAY";
        public const string SpSearchWorkReportByStateLateMonth = "WS_REPORT_V2_WL_STATE_MONTH";
        public const string SpSearchWorkReportByStateLateExcel = "WS_REPORT_EXCEL_V2_WL_STATE";
        public const string SpSearchWorkReportByStateLateWeek = "WS_REPORT_V2_WL_STATE_WEEK";
        public const string SpSearchWorkReportByStateLateYear = "WS_REPORT_V2_WL_STATE_YEAR";
        public const string SpSearchWorkReportByStateWrongMonth = "WS_REPORT_V2_WW_STATE_MONTH";
        public const string SpSearchWorkReportByStateWrongExcel = "WS_REPORT_EXCEL_V2_WW_STATE";
        public const string SpSearchWorkReportByStateWrongWeek = "WS_REPORT_V2_WW_STATE_WEEK";
        public const string SpSearchWorkReportByStateWrongDay = "WS_REPORT_V2_WW_STATE_DAY";
        public const string SpSearchWorkReportByStateWrongYear = "WS_REPORT_V2_WW_STATE_YEAR";
        public const string SpSearchWorkGroupReportByWorkGroupStatus = "WORKSSUPPORTREPORT_V2_WGSTATUS";
        public const string SpSearchWorkReportByWorkGroupIdStatus = "WORKSSUPPORTREPORT_V2_W_STATUS";
        public const string SpSearchWorkGroupReportByWorkGroup = "WS_REPORT_V2_WORKGROUP_GROUP";
        public const string SpSearchWorkDetailReportByWorkGroup = "WS_REPORT_V2_WDETAIL_GROUP";
        public const string SpSearchWorkGroupReportByState = "WS_REPORT_V2_WORKGROUP_STATE";
        public const string SpSearchWorkDetailReportByState = "WS_REPORT_V2_WDETAIL_STATE";
        public const string SpSearchWorkDetailReporExcel = "WS_REPORT_V2_STATUS_EXCEL";
        public const string SpSearchWorkMinDateReporExcel = "WS_REPORT_V2_MINDATEWORK";
        #endregion
    }
}
