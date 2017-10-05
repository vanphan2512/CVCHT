using System;
using System.Collections.Generic;
using System.Data;
using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorkSupportType
{
    /// <summary>
    /// Lương Trung Nhân
    /// created: 02/06/2017
    /// Bảng liên kết loại công việc và độ ưu tiên
    /// </summary>
    public class DaWorkSupportTypePriority
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
        public ResultMessage SearchData(ref List<WorksSupportTypePriority> list, params object[] objKeywords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearch);
                objIData.AddParameter(objKeywords);
                DataTable dtbData = objIData.ExecStoreToDataTable();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportTypePriority>(dtbData);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportType_Priority -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// GetAlll WorksSupportType_Priority 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorksSupportTypePriority> list)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectAll);
                var dtb = objIData.ExecStoreToDataTable();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportTypePriority>(dtb);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType_Priority", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_Priority -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportType_Priority by Id
        /// </summary>
        /// <param name="listWorksSupportTypePriority"></param>
        /// <param name="intWorkSupportTypeId"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref List<WorksSupportTypePriority> listWorksSupportTypePriority, int intWorkSupportTypeId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intWorkSupportTypeId);
                var dtb = objIData.ExecStoreToDataTable();
                listWorksSupportTypePriority = MyUtils.DataTableExtensions.ToList<WorksSupportTypePriority>(dtb);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType_Priority", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_Priority -> GetById", "");
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="listWorksSupportTypePriority"></param>
        /// <param name="objIData"></param>
        /// <param name="worksSupportTypeId"></param>
        /// <returns></returns>
        public ResultMessage Insert(List<WorksSupportTypePriority> listWorksSupportTypePriority, IData objIData, int worksSupportTypeId)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                if (listWorksSupportTypePriority != null && listWorksSupportTypePriority.Count > 0)
                {
                    foreach (var itemPriority in listWorksSupportTypePriority)
                    {
                        Insert(objIData, itemPriority, worksSupportTypeId);
                    }
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupportType_Priority", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportType_Priority -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            
            return objResultMessage;
        }

        /// <summary>
        /// Cập nhật thông tin trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="listWorksSupportTypePriority"></param>
        /// <param name="objIData"></param>
        /// <param name="worksSupportTypeId"></param>
        /// <returns></returns>
        public ResultMessage Update(List<WorksSupportTypePriority> listWorksSupportTypePriority, IData objIData, int worksSupportTypeId)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                DeletePriority(objIData, worksSupportTypeId);
                if (listWorksSupportTypePriority != null && listWorksSupportTypePriority.Count > 0)
                {
                    foreach (var objWorksSupportTypePriority in listWorksSupportTypePriority)
                    {
                        Insert(objIData, objWorksSupportTypePriority, worksSupportTypeId);                   
                    }
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Update, "Lỗi cập nhật thông tin WorksSupportType_Priority", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportType_Priority -> Update", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Xóa trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objWorksSupportTypePriority"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage Delete(WorksSupportTypePriority objWorksSupportTypePriority, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                DeletePriority(objIData, objWorksSupportTypePriority.WorksSupportTypeId);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSuportStatus", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportType_Priority -> Delete", Globals.ModuleName);
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
        /// <param name="objWorksSupportTypePriority"></param>
        /// <param name="worksSupportTypeId"></param>
        protected virtual void Insert(IData objIData, WorksSupportTypePriority objWorksSupportTypePriority, int worksSupportTypeId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAdd);
                objIData.AddParameter("@WorksSupportTypeId", worksSupportTypeId);
                objIData.AddParameter("@WorksSupportPriorityId", objWorksSupportTypePriority.WorksSupportPriorityId);
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
        /// <param name="objWorksSupportTypePriority"></param>
        protected virtual void Update(IData objIData, WorksSupportTypePriority objWorksSupportTypePriority)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);
                objIData.AddParameter("@WorksSupportTypeId", objWorksSupportTypePriority.WorksSupportTypeId);
                objIData.AddParameter("@WorksSupportPriorityId", objWorksSupportTypePriority.WorksSupportPriorityId);
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
        protected void DeletePriority(IData objIData, int worksSupportTypeId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WorksSupportTypeId", worksSupportTypeId);
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

        public const string SpSelectAll = "EO_WorksSupportType_Priority_GETALL";
        public const string SpSelect = "EO_WORKSSUPPORTTYPE_P_SEL";
        public const string SpAdd = "EO_WORKSSUPPORTTYPE_P_ADD";
        public const string SpUpdate = "EO_WorksSupportType_Priority_UPD";
        public const string SpDelete = "EO_WORKSSUPPORTTYPE_P_DEL";
        public const string SpSearch = "EO_WorksSupportType_Priority_SRH";
        #endregion
    }
}
