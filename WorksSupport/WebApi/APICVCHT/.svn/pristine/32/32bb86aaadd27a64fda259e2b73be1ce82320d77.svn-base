using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorkSupportType
{
    /// <summary>
    /// Lương Trung Nhân
    /// created: 07/06/2017
    /// Quyền theo dự án
    /// </summary>
    public class DaWorksSupportProjectPermis
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion

        #region Public Methods

        /// <summary>
        /// Tìm kiếm thông tin trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="list"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<WorksSupportProjectPermis> list, params object[] objKeywords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearch);
                objIData.AddParameter(objKeywords);
                var dtbData = objIData.ExecStoreToDataTable();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportProjectPermis>(dtbData);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportProject_Permis -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// GetAlll WorksSupportProject_Permis 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorksSupportProjectPermis> list)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectAll);
                var dtb = objIData.ExecStoreToDataTable();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportProjectPermis>(dtb);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportProject_Permis", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportProject_Permis -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportProject_Permis by Id
        /// </summary>
        /// <param name="listWorksSupportProjectPermis"></param>
        /// <param name="intWorkSupportTypeId"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref List<WorksSupportProjectPermis> listWorksSupportProjectPermis, int intWorkSupportTypeId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intWorkSupportTypeId);

                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportProjectPermis = new WorksSupportProjectPermis();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportProjectPermis.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["USERNAME"])) objWorksSupportProjectPermis.UserName = Convert.ToString(reader["USERNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objWorksSupportProjectPermis.FullName = Convert.ToString(reader["FULLNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ISCANADDPROJECT"])) objWorksSupportProjectPermis.IsCanAddProject = Convert.ToInt32(reader["ISCANADDPROJECT"]);
                    if (!Convert.IsDBNull(reader["ISCANEDITPROJECT"])) objWorksSupportProjectPermis.IsCanEditProject = Convert.ToInt32(reader["ISCANEDITPROJECT"]);
                    if (!Convert.IsDBNull(reader["ISCANDELETEPROJECT"])) objWorksSupportProjectPermis.IsCanDeleteProject = Convert.ToInt32(reader["ISCANDELETEPROJECT"]);
                    if (!Convert.IsDBNull(reader["ISCANVIEWPROJECT"])) objWorksSupportProjectPermis.IsCanViewProject = Convert.ToInt32(reader["ISCANVIEWPROJECT"]);
                    if (!Convert.IsDBNull(reader["ISCANVIEWPROJECTREPORT"])) objWorksSupportProjectPermis.IsCanViewProjectReport = Convert.ToInt32(reader["ISCANVIEWPROJECTREPORT"]);
                    listWorksSupportProjectPermis.Add(objWorksSupportProjectPermis);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportProject_Permis", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportProject_Permis -> GetById", "");
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="listWorksSupportProjectPermis"></param>
        /// <param name="objIData"></param>
        /// <param name="worksSupportTypeId"></param>
        /// <returns></returns>
        public ResultMessage Insert(List<WorksSupportProjectPermis> listWorksSupportProjectPermis, int worksSupportTypeId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                if (listWorksSupportProjectPermis != null && listWorksSupportProjectPermis.Count > 0)
                {
                    foreach (var objWorksSupportProjectPermis in listWorksSupportProjectPermis)
                    {
                        objWorksSupportProjectPermis.IsCanViewProject = 1;
                        objWorksSupportProjectPermis.WorksSupportTypeId = worksSupportTypeId;
                        Insert(objIData, objWorksSupportProjectPermis);
                    }
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupportProject_Permis", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportProject_Permis -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worksSupportProjectPermis"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage InsertProjectPermission(WorksSupportProjectPermis worksSupportProjectPermis, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                Insert(objIData, worksSupportProjectPermis);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupportProject_Permis", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportProject_Permis -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Xóa trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="worksSupportTypeId"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage Delete(int worksSupportTypeId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                Delete(objIData, worksSupportTypeId);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa EO_WORKSSUPPORTPROJECT_PERMIS", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportProject_Permis -> Delete", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Thêm trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportProjectPermis"></param>
        protected virtual void Insert(IData objIData, WorksSupportProjectPermis objWorksSupportProjectPermis)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAdd);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", objWorksSupportProjectPermis.WorksSupportTypeId);
                objIData.AddParameter("@USERNAME", objWorksSupportProjectPermis.UserName.Trim());
                objIData.AddParameter("@ISCANVIEWPROJECT", objWorksSupportProjectPermis.IsCanViewProject);
                objIData.AddParameter("@ISCANEDITPROJECT", objWorksSupportProjectPermis.IsCanEditProject);
                objIData.AddParameter("@ISCANDELETEPROJECT", objWorksSupportProjectPermis.IsCanDeleteProject);
                objIData.AddParameter("@ISCANADDPROJECT", objWorksSupportProjectPermis.IsCanAddProject);
                objIData.AddParameter("@ISCANVIEWPROJECTREPORT", objWorksSupportProjectPermis.IsCanViewProjectReport);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportProjectPermis"></param>
        protected virtual void Update(IData objIData, WorksSupportProjectPermis objWorksSupportProjectPermis)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", objWorksSupportProjectPermis.WorksSupportTypeId);
                objIData.AddParameter("@USERNAME", objWorksSupportProjectPermis.UserName);
                objIData.AddParameter("@ISCANVIEWPROJECT", objWorksSupportProjectPermis.IsCanViewProject);
                objIData.AddParameter("@ISCANEDITPROJECT", objWorksSupportProjectPermis.IsCanEditProject);
                objIData.AddParameter("@ISCANDELETEPROJECT", objWorksSupportProjectPermis.IsCanDeleteProject);
                objIData.AddParameter("@ISCANADDPROJECT", objWorksSupportProjectPermis.IsCanAddProject);
                objIData.AddParameter("@ISCANVIEWPROJECTREPORT", objWorksSupportProjectPermis.IsCanViewProjectReport);
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
        /// <param name="worksSupportTypeId"></param>
        protected void Delete(IData objIData, int worksSupportTypeId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", worksSupportTypeId);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }
        #endregion

        #region Stored Procedure Names

        public const string SpSelectAll = "EO_WORKSSUPPORTPROJECT_P_GETALL";
        public const string SpSelect = "EO_WORKSSUPPORTPROJECT_P_SEL";
        public const string SpAdd = "EO_WORKSSUPPORTPROJECT_P_ADD";
        public const string SpUpdate = "EO_WORKSSUPPORTPROJECT_P_UPD";
        public const string SpDelete = "EO_WORKSSUPPORTPROJECT_P_DEL";
        public const string SpSearch = "EO_WORKSSUPPORTPROJECT_P_SRH";
        #endregion
    }
}
