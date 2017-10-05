using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;
using System.Data;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorksSupport
{
    /// <summary>
    /// Created by 		: Luong Trung Nhan
    /// Created date 	: 17/06/2017
    /// 
    /// </summary>	
    public class DaWorksSupportComment
    {
        #region Methods			

        /// <summary>
        /// Nạp thông tin 
        /// </summary>
        /// <param name="listWorksSupportComment"></param>
        /// <param name="worksSupportId"></param>
        /// <param name="objIData"></param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage LoadInfo(ref List<WorksSupportComment> listWorksSupportComment, int worksSupportId, IData objIData)
		{
			ResultMessage objResultMessage = new ResultMessage();
			try 
			{
				objIData.CreateNewStoredProcedure(SP_SELECT);
                objIData.AddParameter("@WORKSSUPPORTID" , worksSupportId);
				IDataReader reader = objIData.ExecStoreToDataReader();
				while (reader.Read())
 				{
                    var objWorksSupportComment = new WorksSupportComment();
                    if (!Convert.IsDBNull(reader["COMMENTID"])) objWorksSupportComment.CommentId = Convert.ToInt32(reader["COMMENTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupportComment.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
 					if (!Convert.IsDBNull(reader["COMMENTDATE"])) objWorksSupportComment.CommentDate = Convert.ToDateTime(reader["COMMENTDATE"]);
 					if (!Convert.IsDBNull(reader["COMMENTCONTENT"])) objWorksSupportComment.CommentContent = Convert.ToString(reader["COMMENTCONTENT"]).Trim();
 					if (!Convert.IsDBNull(reader["COMMENTUSER"])) objWorksSupportComment.CommentUser = Convert.ToString(reader["COMMENTUSER"]).Trim();

                    if (!Convert.IsDBNull(reader["FULLNAME"]))
                        objWorksSupportComment.FullName = Convert.ToString(reader["FULLNAME"]);
                    if (!Convert.IsDBNull(reader["POSITIONNAME"]))
                        objWorksSupportComment.PositionName = Convert.ToString(reader["POSITIONNAME"]);
                    if (!Convert.IsDBNull(reader["DEPARTMENTNAME"]))
                        objWorksSupportComment.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]);
                    if (!Convert.IsDBNull(reader["DEFAULTPICTUREURL"]))
                        objWorksSupportComment.DefaultPictureUrl = Convert.ToString(reader["DEFAULTPICTUREURL"]);
                    objWorksSupportComment.ListCommentAttachment = new DaWorksSupportCommentAttachment().GetListCommentAttachment(objIData, Convert.ToString(objWorksSupportComment.CommentId));
                    listWorksSupportComment.Add(objWorksSupportComment);
 				}
 			
				reader.Close();
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo, "Lỗi nạp thông tin ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupport_Comment -> LoadInfo", Globals.ModuleName);
				return objResultMessage;
			}
    		return objResultMessage;
		}
        public List<WorksSupportComment> SearchDataToList(int workSupportID)
        {
            var objIData = Data.CreateData();
            var listWorksSupportComment = new List<WorksSupportComment>();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SP_SEARCH);
                objIData.AddParameter("@WORKSSUPPORTID" , workSupportID);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportComment = new WorksSupportComment();
                    if (!Convert.IsDBNull(reader["COMMENTID"])) objWorksSupportComment.CommentId = Convert.ToInt32(reader["COMMENTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"])) objWorksSupportComment.WorksSupportId = Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    if (!Convert.IsDBNull(reader["COMMENTDATE"])) objWorksSupportComment.CommentDate = Convert.ToDateTime(reader["COMMENTDATE"]);
                    if (!Convert.IsDBNull(reader["COMMENTCONTENT"])) objWorksSupportComment.CommentContent = Convert.ToString(reader["COMMENTCONTENT"]).Trim();
                    if (!Convert.IsDBNull(reader["COMMENTUSER"])) objWorksSupportComment.CommentUser = Convert.ToString(reader["COMMENTUSER"]).Trim();

                    if (!Convert.IsDBNull(reader["FULLNAME"]))
                        objWorksSupportComment.FullName = Convert.ToString(reader["FULLNAME"]);
                    if (!Convert.IsDBNull(reader["POSITIONNAME"]))
                        objWorksSupportComment.PositionName = Convert.ToString(reader["POSITIONNAME"]);
                    if (!Convert.IsDBNull(reader["DEPARTMENTNAME"]))
                        objWorksSupportComment.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]);
                    if (!Convert.IsDBNull(reader["DEFAULTPICTUREURL"]))
                        objWorksSupportComment.DefaultPictureUrl = Convert.ToString(reader["DEFAULTPICTUREURL"]);
                    objWorksSupportComment.ListCommentAttachment = new DaWorksSupportCommentAttachment().GetListCommentAttachment(objIData, Convert.ToString(objWorksSupportComment.CommentId));
                    listWorksSupportComment.Add(objWorksSupportComment);
                }
               
                reader.Close();
            }
            catch (Exception objEx)
            {
                throw new Exception("Lỗi nạp thông tin bình luận công việc cần hỗ trợ !",objEx.InnerException);
            }
            finally
            {
                objIData.Disconnect();
            }
            return listWorksSupportComment;
        }

		/// <summary>
		/// Thêm thông tin 
		/// </summary>
		/// <param name="objIData">Đối tượng Kết nối CSDL</param>
		/// <param name="objWorksSupportComment">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
		public decimal Insert(IData objIData, WorksSupportComment objWorksSupportComment)
		{
			try
			{
				objIData.CreateNewStoredProcedure(SP_ADD);
                //objIData.AddParameter("@" + WorksSupportComment.colWorksSupportID, objWorksSupportComment.WorksSupportId);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentContent, objWorksSupportComment.CommentContent);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentUser, objWorksSupportComment.CommentUser);
                //objIData.AddParameter("@" + WorksSupportComment.colCreatedUser, objWorksSupportComment.CreatedUser);
                return Convert.ToDecimal(objIData.ExecStoreToString());
			}
			catch (Exception objEx)
			{
				objIData.RollBackTransaction();
				throw (objEx);
			}
		}

        /// <summary>
        /// Thêm thông tin 
        /// </summary>
        /// <param name="objIData">Đối tượng Kết nối CSDL</param>
        /// <param name="objWorksSupportComment">Đối tượng truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public decimal Insert_New(IData objIData, WorksSupportComment objWorksSupportComment)
        {
            try
            {
                objIData.CreateNewStoredProcedure("EO_WORKSSUPPORT_CM_INSERT_BETA");
                //objIData.AddParameter("@" + WorksSupportComment.colWorksSupportID, objWorksSupportComment.WorksSupportID);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentContent, objWorksSupportComment.CommentContent);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentUser, objWorksSupportComment.CommentUser);
                //objIData.AddParameter("@" + WorksSupportComment.colCreatedUser, objWorksSupportComment.CreatedUser);
                //objIData.AddParameter("@" + WorksSupportComment.colOrderIndex, objWorksSupportComment.OrderIndex);
                //objIData.AddParameter("@" + WorksSupportComment.colIsActive, objWorksSupportComment.IsActive);
                //objIData.AddParameter("@" + WorksSupportComment.colIsSystem, objWorksSupportComment.IsSystem);
                return Convert.ToDecimal(objIData.ExecStoreToString());
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
                throw (objEx);
            }
        }

		/// <summary>
		/// Cập nhật thông tin 
		/// </summary>
		/// <param name="objWorksSupportComment">Đối tượng truyền vào</param>
		/// <param name="lstOrderIndex">Danh sách OrderIndex</param>
		/// <returns>Kết quả trả về</returns>
        //public ResultMessage Update(WorksSupportComment objWorksSupportComment, List<object> lstOrderIndex)
        //{
        //    ResultMessage objResultMessage = new ResultMessage();
        //    IData objIData = Library.DataAccess.Data.CreateData();
        //    try 
        //    {
        //        objIData.Connect();
        //        objIData.BeginTransaction();
        //        Update(objIData, objWorksSupportComment);
        //        if (lstOrderIndex != null && lstOrderIndex.Count > 0)
        //        {
        //            for (int i = 0; i < lstOrderIndex.Count; i++)
        //            {
        //                UpdateOrderIndex(objIData, Convert.ToInt32(lstOrderIndex[i]), i);
        //            }
        //        }
        //        objIData.CommitTransaction();
        //    }
        //    catch (Exception objEx)
        //    {
        //        objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.Update, "Lỗi cập nhật thông tin ", objEx.ToString());
        //        ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupport_Comment -> Update", Globals.ModuleName);
        //        return objResultMessage;
        //    }
        //    finally
        //    {
        //        objIData.Disconnect();
        //    }
        //    return objResultMessage;
        //}


		/// <summary>
		/// Cập nhật thông tin 
		/// </summary>
		/// <param name="objIData">Đối tượng Kết nối CSDL</param>
		/// <param name="objWorksSupportComment">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
		public void Update(IData objIData, WorksSupportComment objWorksSupportComment)
		{
			try 
			{
				objIData.CreateNewStoredProcedure(SP_UPDATE);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentID, objWorksSupportComment.CommentId);
                //objIData.AddParameter("@" + WorksSupportComment.colWorksSupportID, objWorksSupportComment.WorksSupportID);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentDate, objWorksSupportComment.CommentDate);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentContent, objWorksSupportComment.CommentContent);
                //objIData.AddParameter("@" + WorksSupportComment.colCommentUser, objWorksSupportComment.CommentUser);
                //objIData.AddParameter("@" + WorksSupportComment.colOrderIndex, objWorksSupportComment.OrderIndex);
                //objIData.AddParameter("@" + WorksSupportComment.colIsActive, objWorksSupportComment.IsActive);
                //objIData.AddParameter("@" + WorksSupportComment.colIsSystem, objWorksSupportComment.IsSystem);
                //objIData.AddParameter("@" + WorksSupportComment.colUpdatedUser, objWorksSupportComment.UpdatedUser);
                objIData.ExecNonQuery();
			}
			catch (Exception objEx)
			{
				objIData.RollBackTransaction();
				throw (objEx);
			}
		}


        ///// <summary>
        ///// Cập nhật thông tin 
        ///// </summary>
        ///// <param name="objIData">Đối tượng Kết nối CSDL</param>
        ///// <param name="intOrderIndex">Thứ tự</param>
        ///// <returns>Kết quả trả về</returns>
        //public void UpdateOrderIndex(IData objIData, decimal decCommentID, int intOrderIndex)
        //{
        //    try 
        //    {
        //        objIData.CreateNewStoredProcedure(SP_UPDATEINDEX);
        //        objIData.AddParameter("@" + WorksSupportComment.colCommentID, decCommentID);
        //        objIData.AddParameter("@OrderIndex", intOrderIndex);
        //        objIData.ExecNonQuery();
        //    }
        //    catch (Exception objEx)
        //    {
        //        objIData.RollBackTransaction();
        //        throw (objEx);
        //    }
        //}


		/// <summary>
		/// Xóa thông tin 
		/// </summary>
		/// <param name="objWorksSupportComment">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
        public ResultMessage Delete(WorksSupportComment objComment, IData objIData, string user)
		{
			ResultMessage objResultMessage = new ResultMessage();
			try 
			{
                Delete(objIData, objComment, user);
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.Delete, "Lỗi xóa thông tin ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupport_Comment -> Delete", Globals.ModuleName);
				return objResultMessage;
			}
			finally
    		{
                //objIData.Disconnect();
    		}
    		return objResultMessage;
		}


		/// <summary>
		/// Xóa thông tin 
		/// </summary>
		/// <param name="objIData">Đối tượng Kết nối CSDL</param>
		/// <param name="objWorksSupportComment">Đối tượng truyền vào</param>
		/// <returns>Kết quả trả về</returns>
        private void Delete(IData objIData, WorksSupportComment obj, string user)
		{
			try 
			{
				objIData.CreateNewStoredProcedure(SP_DELETE);
                objIData.AddParameter("@WORKSSUPPORTID", obj.WorksSupportId);
                objIData.AddParameter("@DELETEDUSER" , user);
 				objIData.ExecNonQuery();
			}
			catch (Exception objEx)
			{
				objIData.RollBackTransaction();
				throw (objEx);
			}
		}


        /// <summary>
        /// Tìm kiếm thông tin 
        /// </summary>
        /// <param name="dtbData">Dữ liệu trả về</param>
        /// <param name="objKeywords">Điều kiện tìm kiếm truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage SearchData(ref DataTable dtbData, params object[] objKeywords)
        {
            ResultMessage objResultMessage = new ResultMessage();
            IData objIData = Library.DataAccess.Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SP_SEARCH);
                objIData.AddParameter(objKeywords);
                dtbData = objIData.ExecStoreToDataTable();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupport_Comment -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

		#endregion

		
		#region Constructor

		public DaWorksSupportComment()
		{
		}
		#endregion


		#region Stored Procedure Names

        public const String SP_ADD = "EWORKSSUPPORT_CM_V2_ADD_BETA";
        public const String SP_UPDATE = "WORKSSUPPORT_CM_V2_UPD_BETA";
        public const String SP_DELETE = "WORKSSUPPORT_CM_DEL_BETA";
        public const String SP_SELECT = "WORKSSUPPORT_COMMENT_V2_SEL";
        public const String SP_SEARCH = "WORKSSUPPORT_CM_V2_SRH_BETA";
		public const String SP_UPDATEINDEX = "WorksSupport_Comment_UPDINDEX";
        const string SP_GetByWSID = "WORKSSUPPORT_CM_V2_GETID_BETA";
        public const String SP_SELECTBYWorksSupportID = "WORKSSUPPORT_CM_V2_SEWSID_BETA";

		#endregion


        #region DaWorksSupport_Comment's Members

        /// <summary>
        /// Author          : Byrs
        /// Date created    : Apr 18, 2013
        /// Description     : Get list of comments by work support identify
        /// </summary>
        /// <param name="decWSID"></param>
        /// <returns></returns>
        public List<WorksSupportComment> GetByWorksSupportId(decimal decWSID, int intPageIdx, int intPageSize)
        {
            IData objIData = Library.DataAccess.Data.CreateData();
            List<WorksSupportComment> list = new List<WorksSupportComment>();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SP_GetByWSID);
                objIData.AddParameter("@WORKSSUPPORTID" , decWSID);
                objIData.AddParameter("@PageIdx", intPageIdx);
                objIData.AddParameter("@PageSize", intPageSize);
                IDataReader reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    WorksSupportComment objWorksSupportComment = new WorksSupportComment();
                    if (!Convert.IsDBNull(reader["COMMENTID"]))
                        objWorksSupportComment.CommentId = Convert.ToInt32(reader["COMMENTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTID"]))
                    {
                        objWorksSupportComment.WorksSupportId =
                            Convert.ToInt32(reader["WORKSSUPPORTID"]);
                    }
                    if (!Convert.IsDBNull(reader["COMMENTDATE"]))
                        objWorksSupportComment.CommentDate = Convert.ToDateTime(reader["COMMENTDATE"]);
                    if (!Convert.IsDBNull(reader["COMMENTCONTENT"]))
                    {
                        objWorksSupportComment.CommentContent =
                            Convert.ToString(reader["COMMENTCONTENT"]).Trim();
                    }
                    if (!Convert.IsDBNull(reader["COMMENTUSER"]))
                    {
                        objWorksSupportComment.CommentUser =
                            Convert.ToString(reader["COMMENTUSER"]).Trim();
                    }
                   
                     if (!Convert.IsDBNull(reader["FULLNAME"]))
                        objWorksSupportComment.FullName = Convert.ToString(reader["FULLNAME"]).Trim();

                     if (!Convert.IsDBNull(reader["POSITIONNAME"]))
                         objWorksSupportComment.PositionName = Convert.ToString(reader["POSITIONNAME"]).Trim();

                      if (!Convert.IsDBNull(reader["DEPARTMENTNAME"]))
                        objWorksSupportComment.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]).Trim();

                     if (!Convert.IsDBNull(reader["DEFAULTPICTUREURL"]))
                        objWorksSupportComment.DefaultPictureUrl = Convert.ToString(reader["DEFAULTPICTUREURL"]).Trim();

                    list.Add(objWorksSupportComment);
                }

                reader.Close();
            }
            catch (Exception objEx)
            {
                throw new Exception("Lỗi nạp thông tin bình luận công việc cần hỗ trợ !", objEx.InnerException);
            }
            finally
            {
                objIData.Disconnect();
            }

            return list;
        }

        /// <summary>
        /// Author          : hoc.lenho
        /// Date created    : 7/23 2013
        /// Description     : Get list of comments by work support identify
        /// </summary>
        /// <param name="decWSID"></param>
        /// <returns></returns>
        //public ResultMessage LoadInfoCommentByWorksSupportId(ref List<CommentView> lstobjCommentView, decimal decWSID, int intPageIdx, int intPageSize)
        //{
        //    ResultMessage objResultMessage = new ResultMessage();
        //    IData objIData = Library.DataAccess.Data.CreateData();
        //    try
        //    {
        //        objIData.Connect();
        //        objIData.CreateNewStoredProcedure(SP_SELECTBYWorksSupportID);
        //        objIData.AddParameter("@WORKSSUPPORTID" , decWSID);
        //        objIData.AddParameter("@PageIndex", intPageIdx);
        //        objIData.AddParameter("@PageSize", intPageSize);
        //        IDataReader reader = objIData.ExecStoreToDataReader();
        //        lstobjCommentView = new List<CommentView>();
        //        CommentView objCommentView;
        //        while (reader.Read())
        //        {
        //            objCommentView = new CommentView();
        //            objCommentView.WorksSupport_comment = new WorksSupportComment();
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colCommentID]))
        //                objCommentView.WorksSupport_comment.CommentId = Convert.ToDecimal(reader[WorksSupportComment.colCommentID]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colWorksSupportID]))
        //            {
        //                objCommentView.WorksSupport_comment.WorksSupportID =
        //                    Convert.ToDecimal(reader[WorksSupportComment.colWorksSupportID]);
        //            }
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colCommentDate]))
        //                objCommentView.WorksSupport_comment.CommentDate = Convert.ToDateTime(reader[WorksSupportComment.colCommentDate]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colCommentContent]))
        //            {
        //                objCommentView.WorksSupport_comment.CommentContent =
        //                    Convert.ToString(reader[WorksSupportComment.colCommentContent]).Trim();
        //            }
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colCommentUser]))
        //            {
        //                objCommentView.WorksSupport_comment.CommentUser =
        //                    Convert.ToString(reader[WorksSupportComment.colCommentUser]).Trim();
        //            }
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colOrderIndex]))
        //                objCommentView.WorksSupport_comment.OrderIndex = Convert.ToDecimal(reader[WorksSupportComment.colOrderIndex]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colIsActive]))
        //                objCommentView.WorksSupport_comment.IsActive = Convert.ToBoolean(reader[WorksSupportComment.colIsActive]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colIsSystem]))
        //                objCommentView.WorksSupport_comment.IsSystem = Convert.ToBoolean(reader[WorksSupportComment.colIsSystem]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colCreatedUser]))
        //            {
        //                objCommentView.WorksSupport_comment.CreatedUser =
        //                    Convert.ToString(reader[WorksSupportComment.colCreatedUser]).Trim();
        //            }
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colCreatedDate]))
        //                objCommentView.WorksSupport_comment.CreatedDate = Convert.ToDateTime(reader[WorksSupportComment.colCreatedDate]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colUpdatedUser]))
        //            {
        //                objCommentView.WorksSupport_comment.UpdatedUser =
        //                    Convert.ToString(reader[WorksSupportComment.colUpdatedUser]).Trim();
        //            }
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colUpdatedDate]))
        //                objCommentView.WorksSupport_comment.UpdatedDate = Convert.ToDateTime(reader[WorksSupportComment.colUpdatedDate]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colIsDeleted]))
        //                objCommentView.WorksSupport_comment.IsDeleted = Convert.ToBoolean(reader[WorksSupportComment.colIsDeleted]);
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colDeletedUser]))
        //            {
        //                objCommentView.WorksSupport_comment.DeletedUser = Convert.ToString(reader[WorksSupportComment.colDeletedUser]).Trim();
        //            }
        //            if (!Convert.IsDBNull(reader[WorksSupportComment.colDeletedDate]))
        //                objCommentView.WorksSupport_comment.DeletedDate = Convert.ToDateTime(reader[WorksSupportComment.colDeletedDate]);

        //            if (!Convert.IsDBNull(reader["FULLNAME"]))
        //                objCommentView.WorksSupport_comment.FullName = Convert.ToString(reader["FULLNAME"]).Trim();

        //            if (!Convert.IsDBNull(reader["POSITIONNAME"]))
        //                objCommentView.WorksSupport_comment.PositionName = Convert.ToString(reader["POSITIONNAME"]).Trim();

        //            if (!Convert.IsDBNull(reader["DEPARTMENTNAME"]))
        //                objCommentView.WorksSupport_comment.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]).Trim();

        //            if (!Convert.IsDBNull(reader["DEFAULTPICTUREURL"]))
        //                objCommentView.WorksSupport_comment.DefaultPictureUrl = Convert.ToString(reader["DEFAULTPICTUREURL"]).Trim();

        //            if (!Convert.IsDBNull(reader["TOTALROWS"])) objCommentView.TotalRows = Convert.ToInt32(reader["TOTALROWS"]);
        //            if (!Convert.IsDBNull(reader["STT"])) objCommentView.Stt = Convert.ToString(reader["STT"]);
        //            lstobjCommentView.Add(objCommentView);
        //        }
        //        reader.Close();
        //    }
        //    catch (Exception objEx)
        //    {
        //        objResultMessage = new ResultMessage(true, Library.WebCore.SystemError.ErrorTypes.LoadInfo, "Lỗi nạp thông tin bình luận công việc cần hổ trợ LoadInfoByID", objEx.ToString());
        //        ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_EO_EForm_Comment -> LoadInfoByEFormID", Globals.ModuleName);
        //        return objResultMessage;
        //    }
        //    finally
        //    {
        //        objIData.Disconnect();
        //    }
        //    return objResultMessage;
        //}

        #endregion
    }
}
