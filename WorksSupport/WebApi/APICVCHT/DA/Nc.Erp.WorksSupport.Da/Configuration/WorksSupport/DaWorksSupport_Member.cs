using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorksSupport
{
    public class DaWorksSupportMember
    {
        #region Methods			
		

		/// <summary>
		/// Nạp thông tin danh sách file đính kèm của công việc cần hỗ trợ
		/// </summary>
		/// <param name="objWorksSupport_Member">Đối tượng trả về</param>
		/// <returns>Kết quả trả về</returns>
        public ResultMessage LoadInfo(ref List<WorksSupportMember> lstWorksSupport_Member, int intWorksSupportID, IData objIData)
		{
			ResultMessage objResultMessage = new ResultMessage();
            //IData objIData = Library.DataAccess.Data.CreateData();
			try 
			{
                //objIData.Connect();
				objIData.CreateNewStoredProcedure(SP_SELECT);
                objIData.AddParameter("@WORKSSUPPORTID", intWorksSupportID);
				IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    WorksSupportMember objWorksSupport_Member = new WorksSupportMember();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport_Member.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTDATE"])) objWorksSupport_Member.WorksSupportDate = Convert.ToDateTime(reader["WORKSSUPPORTDATE"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupport_Member.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["NOTE"])) objWorksSupport_Member.Note = Convert.ToString(reader["NOTE"]).Trim();
                    if (!Convert.IsDBNull(reader["MEMBERUSERNAME"])) objWorksSupport_Member.MemberUserName = Convert.ToString(reader["MEMBERUSERNAME"]);
                    if (!Convert.IsDBNull(reader["UPDATEDUSER"])) objWorksSupport_Member.UpdatedUser = Convert.ToString(reader["UPDATEDUSER"]);
                    if (!Convert.IsDBNull(reader["UPDATEDDATE"])) objWorksSupport_Member.UpdatedDate = Convert.ToDateTime(reader["UPDATEDDATE"]);
                    if (!Convert.IsDBNull(reader["ISDELETED"])) objWorksSupport_Member.IsDeleted = Convert.ToBoolean(reader["ISDELETED"]);
                    if (!Convert.IsDBNull(reader["DELETEDUSER"])) objWorksSupport_Member.DeletedUser = Convert.ToString(reader["DELETEDUSER"]);
                    if (!Convert.IsDBNull(reader["DELETEDDATE"])) objWorksSupport_Member.DeletedDate = Convert.ToDateTime(reader["DELETEDDATE"]);
                    lstWorksSupport_Member.Add(objWorksSupport_Member);
                }
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.LoadInfo, "Lỗi nạp thông tin danh sách file đính kèm của công việc cần hỗ trợ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Member -> LoadInfo", Globals.ModuleName);
				return objResultMessage;
			}
			finally
    		{
                //objIData.Disconnect();
    		}
    		return objResultMessage;
		}

		/// <summary>
		/// Thêm thông tin danh sách file đính kèm của công việc cần hỗ trợ
		/// </summary>
		/// <param name="objWorksSupport_Member">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
		public ResultMessage Insert(WorksSupportMember objWorksSupport_Member,IData objIData)
		{
			ResultMessage objResultMessage = new ResultMessage();
            //IData objIData = Library.DataAccess.Data.CreateData();
			try 
			{
                //objIData.Connect();
				Insert(objIData, objWorksSupport_Member);
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin danh sách file đính kèm của công việc cần hỗ trợ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Member -> Insert", Globals.ModuleName);
				return objResultMessage;
			}
			finally
    		{
                //objIData.Disconnect();
    		}
    		return objResultMessage;
		}


		/// <summary>
		/// Thêm thông tin danh sách file đính kèm của công việc cần hỗ trợ
		/// </summary>
		/// <param name="objIData">Đối tượng Kết nối CSDL</param>
		/// <param name="objWorksSupport_Member">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
		protected void Insert(IData objIData, WorksSupportMember objWorksSupport_Member)
		{
			try 
			{
				objIData.CreateNewStoredProcedure(SP_ADD);
                //objIData.AddParameter("@WorksSupportID", objWorksSupport_Member.WorksSupportID);
                //objIData.AddParameter("@FilePath", objWorksSupport_Member.FilePath);
                //objIData.AddParameter("@" + "FileName, objWorksSupport_Member.FileName);
                //objIData.AddParameter("@" + "CreatedUser, objWorksSupport_Member.CreatedUser);
                //objIData.AddParameter("@FILEID", objWorksSupport_Member.FileID);
                objIData.ExecNonQuery();
			}
			catch (Exception objEx)
			{
				objIData.RollBackTransaction();
				throw (objEx);
			}
		}


		/// <summary>
		/// Cập nhật thông tin danh sách file đính kèm của công việc cần hỗ trợ
		/// </summary>
		/// <param name="objWorksSupport_Member">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
        public ResultMessage Update(WorksSupportMember objWorksSupport_Member, IData objIData)
		{
			ResultMessage objResultMessage = new ResultMessage();
            //IData objIData = Library.DataAccess.Data.CreateData();
			try 
			{
				objIData.Connect();
				Update(objIData, objWorksSupport_Member);
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.Update, "Lỗi cập nhật thông tin danh sách file đính kèm của công việc cần hỗ trợ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Member -> Update", Globals.ModuleName);
				return objResultMessage;
			}
			finally
    		{
                //objIData.Disconnect();
    		}
    		return objResultMessage;
		}


		/// <summary>
		/// Cập nhật thông tin danh sách file đính kèm của công việc cần hỗ trợ
		/// </summary>
		/// <param name="objIData">Đối tượng Kết nối CSDL</param>
		/// <param name="objWorksSupport_Member">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
		public void Update(IData objIData, WorksSupportMember objWorksSupport_Member)
		{
			try 
			{
				objIData.CreateNewStoredProcedure(SP_UPDATE);
                //objIData.AddParameter("@" + "AttachmentID, objWorksSupport_Member.AttachmentID);
                //objIData.AddParameter("@" + "WorksSupportID, objWorksSupport_Member.WorksSupportID);
                //objIData.AddParameter("@" + "FilePath, objWorksSupport_Member.FilePath);
                //objIData.AddParameter("@" + "FileName, objWorksSupport_Member.FileName);
                objIData.ExecNonQuery();
			}
			catch (Exception objEx)
			{
				objIData.RollBackTransaction();
				throw (objEx);
			}
		}


		/// <summary>
		/// Xóa thông tin danh sách file đính kèm của công việc cần hỗ trợ
		/// </summary>
		/// <param name="objWorksSupport_Member">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
        public ResultMessage Delete(WorksSupportMember objWorksSupport_Member, IData objIData)
		{
			ResultMessage objResultMessage = new ResultMessage();
            //IData objIData = Library.DataAccess.Data.CreateData();
			try 
			{
                //objIData.Connect();
				Delete(objIData, objWorksSupport_Member);
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.Delete, "Lỗi xóa thông tin danh sách file đính kèm của công việc cần hỗ trợ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Member -> Delete", Globals.ModuleName);
				return objResultMessage;
			}
			finally
    		{
                //objIData.Disconnect();
    		}
    		return objResultMessage;
		}


		/// <summary>
		/// Xóa thông tin danh sách file đính kèm của công việc cần hỗ trợ
		/// </summary>
		/// <param name="objIData">Đối tượng Kết nối CSDL</param>
		/// <param name="objWorksSupport_Member">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
		private void Delete(IData objIData, WorksSupportMember objWorksSupport_Member)
		{
			try 
			{
				objIData.CreateNewStoredProcedure(SP_DELETE);
                objIData.AddParameter("@WorksSupportId", objWorksSupport_Member.WorksSupportId);
				objIData.AddParameter("@DeletedUser", objWorksSupport_Member.DeletedUser);
 				objIData.ExecNonQuery();
			}
			catch (Exception objEx)
			{
				objIData.RollBackTransaction();
				throw (objEx);
			}
		}

		#endregion

        #region Public Methods

        /// <summary>
        /// Tìm kiếm thông tin trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="dtbData"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<WorksSupportMember> list, params object[] objKeywords)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SP_SEARCH);
                objIData.AddParameter(objKeywords);
                DataTable dtbData = objIData.ExecStoreToDataTable();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportMember>(dtbData);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportStatus -> SearchData", Globals.ModuleName);
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
        /// <param name="objWorksSupport_Member"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorksSupportMember> list)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SP_SELECT);
                IDataReader reader = objIData.ExecStoreToDataReader();
                //DataTable dtb = objIData.ExecStoreToDataTable();
                //list = MyUtils.DataTableExtensions.ToList<WorksSupportStatus>(dtb);
                WorksSupportMember objWorksSupport_Member;
                while (reader.Read())
                {
                    objWorksSupport_Member = new WorksSupportMember();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport_Member.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTDATE"])) objWorksSupport_Member.WorksSupportDate = Convert.ToDateTime(reader["WORKSSUPPORTDATE"]);
                    if (!Convert.IsDBNull(reader["MEMBERUSERNAME"])) objWorksSupport_Member.MemberUserName = Convert.ToString(reader["MEMBERUSERNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupport_Member.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["NOTE"])) objWorksSupport_Member.Note = Convert.ToString(reader["NOTE"]);
                    if (!Convert.IsDBNull(reader["UPDATEDUSER"])) objWorksSupport_Member.UpdatedUser = Convert.ToString(reader["UPDATEDUSER"]);
                    if (!Convert.IsDBNull(reader["UPDATEDDATE"])) objWorksSupport_Member.UpdatedDate = Convert.ToDateTime(reader["UPDATEDDATE"]);
                    if (!Convert.IsDBNull(reader["ISDELETED"])) objWorksSupport_Member.IsDeleted = Convert.ToBoolean(reader["ISDELETED"]);
                    if (!Convert.IsDBNull(reader["DELETEDUSER"])) objWorksSupport_Member.DeletedUser = Convert.ToString(reader["DELETEDUSER"]);
                    if (!Convert.IsDBNull(reader["DELETEDDATE"])) objWorksSupport_Member.DeletedDate = Convert.ToDateTime(reader["DELETEDDATE"]);

                    list.Add(objWorksSupport_Member);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupport_Member", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupport_Member -> GetById", "");
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
        /// <param name="objWorksSupport_Member"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref WorksSupportMember listWorksSupport_Member, int intWorkSupportID)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SP_GETBY);
                objIData.AddParameter("@WORKSSUPPORTID", intWorkSupportID);
                IDataReader reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    WorksSupportMember objWorksSupport_Member = new WorksSupportMember();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport_Member.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTDATE"])) objWorksSupport_Member.WorksSupportDate = Convert.ToDateTime(reader["WORKSSUPPORTDATE"]);
                    if (!Convert.IsDBNull(reader["MEMBERUSERNAME"])) objWorksSupport_Member.MemberUserName = Convert.ToString(reader["MEMBERUSERNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupport_Member.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["NOTE"])) objWorksSupport_Member.Note = Convert.ToString(reader["NOTE"]);
                    if (!Convert.IsDBNull(reader["UPDATEDUSER"])) objWorksSupport_Member.UpdatedUser = Convert.ToString(reader["UPDATEDUSER"]);
                    if (!Convert.IsDBNull(reader["UPDATEDDATE"])) objWorksSupport_Member.UpdatedDate = Convert.ToDateTime(reader["UPDATEDDATE"]);
                    if (!Convert.IsDBNull(reader["ISDELETED"])) objWorksSupport_Member.IsDeleted = Convert.ToBoolean(reader["ISDELETED"]);
                    if (!Convert.IsDBNull(reader["DELETEDUSER"])) objWorksSupport_Member.DeletedUser = Convert.ToString(reader["DELETEDUSER"]);
                    if (!Convert.IsDBNull(reader["DELETEDDATE"])) objWorksSupport_Member.DeletedDate = Convert.ToDateTime(reader["DELETEDDATE"]);

                    listWorksSupport_Member = objWorksSupport_Member;
                }
                else
                {
                    listWorksSupport_Member = null;
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupport_Member", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupport_Member -> GetById", "");
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
        /// <param name="objWorksSupportmMember"></param>
        /// <returns></returns>
        public ResultMessage Insert(WorksSupportMember objWorksSupportmMember, ref WorksSupportMember data)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                if (objWorksSupportmMember.WorksSupportId > 0)
                {
                    Update_WorksSupportMember(objIData, objWorksSupportmMember, ref data);
                }
                else
                {
                    Insert_WorksSupportMember(objIData, objWorksSupportmMember);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupportStatus", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportStatus -> Insert", Globals.ModuleName);
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
        /// <param name="objWorksSupport"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //ref WorkSupport objWorksSupport
        //ref DataTable dtbData
        public ResultMessage LoadProcessUserBy_StepID(ref List<WorksSupportMember> list, int intStepId)
        {
            var objResultMessage = new ResultMessage();
            IData objIData = Library.DataAccess.Data.CreateData();
            DataTable dtb = new DataTable();
            try
            {
                objIData.Connect();
                //objIData.BeginTransaction();
                objIData.CreateNewStoredProcedure(SpLoad_ProcessUser);
                objIData.AddParameter("@WORKSSUPPORTSTEPID", intStepId);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    WorksSupportMember objWorksSupport = new WorksSupportMember();
                    if (!Convert.IsDBNull(reader["MEMBERUSERNAME"])) objWorksSupport.MemberUserName = Convert.ToString(reader["MEMBERUSERNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupport.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);

                    list.Add(objWorksSupport);
                }
                //else
                //{
                //    objWorksSupport = null;
                //}
                //reader.Close();

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
        /// Them thanh vien cho cong viec can ho tro EO_WORKSSUPPORT_MEMBER
        /// </summary>
        /// <param name="objWorksSupportmMember"></param>
        /// <returns></returns>
        public ResultMessage Insert_WS_Member(WorksSupportMember objWorksSupportmMember, ref WorksSupportMember data)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                Insert_WS_Member(objIData, objWorksSupportmMember);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupportStatus", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportStatus -> Insert", Globals.ModuleName);
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
        /// Thêm trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportmMember"></param>
        protected virtual void Insert_WorksSupportMember(IData objIData, WorksSupportMember objWorksSupportmMember)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SP_ADD);
                //objIData.AddParameter("@WORKSSUPPORTSTATUSID", objWorksSupportmMember.WorksSupportStatusId);
                objIData.AddParameter("@WORKSSUPPORTID", objWorksSupportmMember.WorksSupportId);
                objIData.AddParameter("@WORKSSUPPORTDATE", objWorksSupportmMember.WorksSupportDate);
                objIData.AddParameter("@MEMBERUSERNAME", objWorksSupportmMember.MemberUserName);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", objWorksSupportmMember.WorksSupportMemberRoleId);
                objIData.AddParameter("@NOTE", objWorksSupportmMember.Note);
                objIData.AddParameter("@UPDATEDUSER", objWorksSupportmMember.UpdatedUser);
                objIData.AddParameter("@UPDATEDDATE", objWorksSupportmMember.UpdatedDate);
                objIData.AddParameter("@ISDELETED", objWorksSupportmMember.IsDeleted);
                objIData.AddParameter("@DELETEDUSER", objWorksSupportmMember.DeletedUser);
                objIData.AddParameter("@DELETEDDATE", objWorksSupportmMember.DeletedDate);
              
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Thêm thanh vien cho cong viec can ho tro
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportmMember"></param>
        protected virtual void Insert_WS_Member(IData objIData, WorksSupportMember objWorksSupportmMember)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SP_ADD);
                objIData.AddParameter("@WORKSSUPPORTID", objWorksSupportmMember.WorksSupportId);
                objIData.AddParameter("@WORKSSUPPORTDATE", objWorksSupportmMember.WorksSupportDate);
                objIData.AddParameter("@MEMBERUSERNAME", objWorksSupportmMember.MemberUserName);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", objWorksSupportmMember.WorksSupportMemberRoleId);
                objIData.AddParameter("@UPDATEDUSER", objWorksSupportmMember.Note);
                //objIData.AddParameter("@UPDATEDDATE", objWorksSupportmMember.UpdatedUser);

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
        /// <param name="objWorksSupportmMember"></param>
        protected virtual void Update_WorksSupportMember(IData objIData, WorksSupportMember objWorksSupportMember, ref WorksSupportMember lstWorksSupportMember)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SP_UPDATE);
                objIData.AddParameter("@WORKSSUPPORTID", objWorksSupportMember.WorksSupportId);
                objIData.AddParameter("@WORKSSUPPORTDATE", objWorksSupportMember.WorksSupportDate);
                objIData.AddParameter("@MEMBERUSERNAME", objWorksSupportMember.MemberUserName);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", objWorksSupportMember.WorksSupportMemberRoleId);
                objIData.AddParameter("@NOTE", objWorksSupportMember.Note);
                objIData.AddParameter("@UPDATEDUSER", objWorksSupportMember.UpdatedUser);
                objIData.AddParameter("@UPDATEDDATE", objWorksSupportMember.UpdatedDate);
                objIData.AddParameter("@ISDELETED", objWorksSupportMember.IsDeleted);
                objIData.AddParameter("@DELETEDUSER", objWorksSupportMember.DeletedUser);
                objIData.AddParameter("@DELETEDDATE", objWorksSupportMember.DeletedDate);
                DataTable dtb = objIData.ExecStoreToDataTable();
                List<WorksSupportMember> list = new List<WorksSupportMember>();
                list = MyUtils.DataTableExtensions.ToList<WorksSupportMember>(dtb);
                if (list != null && list.Count > 0)
                {
                    lstWorksSupportMember = list[0];
                }
                else
                {
                    lstWorksSupportMember = null;
                }

            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }
        #endregion      

        #region Stored Procedure Names

        public const String SP_ADD = "WORKSSUPPORT_MEMBER_V2_ADD";
		public const String SP_UPDATE = "WORKSSUPPORT_MEMBER_UPD";
        public const String SP_DELETE = "WORKSSUPPORT_MEMBER_DEL";
        public const String SP_SELECT = "WORKSSUPPORT_MEMBER_V2_GETALL";
        public const String SP_SEARCH = "WORKSSUPPORT_MEMBER_V2_SRH";
        public const String SP_GETBY = "WORKSSUPPORT_MEMBER_V2_SEL";
        public const string SpLoad_ProcessUser = "WORKSSUPPORT_WFNX_V2_MEMBER";

		#endregion
    }
}
