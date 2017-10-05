using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType;

namespace Nc.Erp.WorksSupport.Da.Configuration.SupportType
{
    /// <summary>
    /// Created by      : Nguyễn Thị Kim Ngân
    /// Created date    : 02/06/2017
    /// Xử lý nghiệp vụ đối tượng ERP.EO.WORKSSUPPORTTYPE.
    /// Khai báo loại công việc YCCVCHT
    /// </summary>
    public class DaWorksSupportType
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion

        #region Public Methods
        /// <summary>
        /// Tìm kiếm thông tin loại công viêc trong công việc cần hỗ trợ
        /// </summary>
        /// <param name="dtbData"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<WorksSupportType> list, params object[] objKeywords)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Library.DataAccess.Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearch);
                objIData.AddParameter(objKeywords);
                DataTable dtbData = objIData.ExecStoreToDataTable();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportType>(dtbData);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportType -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// GetAlll WorksSupportStatus 
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorksSupportType> list)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchAll);
                DataTable dtb = objIData.ExecStoreToDataTable();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportType>(dtb);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType -> GetAll", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportStatus by Id
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref WorksSupportType objWorksSupportType, int intWorksSupportTypeId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetBy);
                objIData.AddParameter("@WorksSupportTypeId", intWorksSupportTypeId);
                DataTable dtb = objIData.ExecStoreToDataTable();
                List<WorksSupportType> list = new List<WorksSupportType>();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportType>(dtb);
                if (list != null && list.Count > 0)
                {
                    objWorksSupportType = list[0];
                }
                else
                {
                    objWorksSupportType = null;
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
        /// Insert data
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Insert(WorksSupportType objWorksSupportType, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                Insert(objIData, objWorksSupportType, user);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportType -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Cập nhật thông tin trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Update(WorksSupportType objWorksSupportType, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                Update(objIData, objWorksSupportType, user);
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Update, "Lỗi cập nhật thông tin WorksSupportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportType -> Update", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Xóa trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Delete(WorksSupportType objWorksSupportType, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                Delete(objIData, objWorksSupportType, user);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSupportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportType -> Delete", Globals.ModuleName);
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
        /// Thêm Loại công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        protected virtual void Insert(IData objIData, WorksSupportType objWorksSupportType, string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAdd);
                objIData.AddParameter("@WORKSSUPPORTTYPENAME", objWorksSupportType.WorksSupportTypeName);
                objIData.AddParameter("@ICONURL", objWorksSupportType.IconUrl);
                objIData.AddParameter("@ADDFUNCTIONID", objWorksSupportType.AddFunctionId);
                objIData.AddParameter("@VIEWALLFUNCTIONID", objWorksSupportType.ViewAllFunctionId);
                objIData.AddParameter("@EDITFUNCTIONID", objWorksSupportType.EditFunctionId);
                objIData.AddParameter("@EDITALLFUNCTIONID", objWorksSupportType.EditAllFunctionId);
                objIData.AddParameter("@DELETEFUNCTIONID", objWorksSupportType.DeleteFunctionId);
                objIData.AddParameter("@DELETEALLFUNCTIONID", objWorksSupportType.EditAllFunctionId);
                objIData.AddParameter("@PROCESSFUNCTIONID", objWorksSupportType.ProcessFunctionId);
                objIData.AddParameter("@COMMENTFUNCTIONID", objWorksSupportType.CommentFunctionId);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportType.Description);
                objIData.AddParameter("@ORDERINDEX", objWorksSupportType.OrderIndex);
                objIData.AddParameter("@ISACTIVE", objWorksSupportType.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportType.IsSystem);
                objIData.AddParameter("@CREATEDUSER", user);
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
        /// Cập nhật trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        protected virtual void Update(IData objIData, WorksSupportType objWorksSupportType , string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", objWorksSupportType.WorksSupportTypeId);
                objIData.AddParameter("@WORKSSUPPORTTYPENAME", objWorksSupportType.WorksSupportTypeName);
                objIData.AddParameter("@ICONURL", objWorksSupportType.IconUrl);
                objIData.AddParameter("@ADDFUNCTIONID", objWorksSupportType.AddFunctionId);
                objIData.AddParameter("@VIEWALLFUNCTIONID", objWorksSupportType.ViewAllFunctionId);
                objIData.AddParameter("@EDITFUNCTIONID", objWorksSupportType.EditFunctionId);
                objIData.AddParameter("@EDITALLFUNCTIONID", objWorksSupportType.EditAllFunctionId);
                objIData.AddParameter("@DELETEFUNCTIONID", objWorksSupportType.DeleteFunctionId);
                objIData.AddParameter("@DELETEALLFUNCTIONID", objWorksSupportType.DeleteAllFunctionId);
                objIData.AddParameter("@PROCESSFUNCTIONID", objWorksSupportType.ProcessFunctionId);
                objIData.AddParameter("@COMMENTFUNCTIONID", objWorksSupportType.CommentFunctionId);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportType.Description);
                objIData.AddParameter("@ORDERINDEX", objWorksSupportType.OrderIndex);
                objIData.AddParameter("@ISACTIVE", objWorksSupportType.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportType.IsSystem);
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
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        protected void Delete(IData objIData, WorksSupportType objWorksSupportType, string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", objWorksSupportType.WorksSupportTypeId);
                objIData.AddParameter("@DELETEDUSER" + user);
                objIData.AddParameter("@CERTIFICATESTRING" + ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS" + ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID" + ObjLogObject.LoginLogID);
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
        public const string SpSearch = "WORKSSUPPORTTYPE_V2_SRH_PAGE";
        public const string SpSearchAll = "WORKSSUPPORTTYPE_V2_SRH";
        public const string SpGetBy = "WORKSSUPPORTTYPE_V2_GETBYID";
        public const string SpAdd = "WORKSSUPPORTTYPE_V2_ADD";
        public const string SpUpdate = "WORKSSUPPORTTYPE_V2_UPD";
        public const string SpDelete = "WORKSSUPPORTTYPE_V2_DEL";

        #endregion
    }
    
}
