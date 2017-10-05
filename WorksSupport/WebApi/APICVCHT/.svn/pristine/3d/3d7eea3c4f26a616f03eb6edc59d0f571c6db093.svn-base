using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.Department;
using Nc.Erp.WorksSupport.Do.Configuration.MemberRole;
using System;
using System.Collections.Generic;
using System.Data;

namespace Nc.Erp.WorksSupport.Da.Configuration.MemberRole
{
    /// <summary>
    /// Created by      : Nguyen Thi Kim Ngan
    /// Created date    : 30.05.2017
    /// Xử lý nghiệp vụ đối tượng ERP.EO_WORKSSUPPORTMEMBERROLE.
    /// </summary>
    public class DaWorksSupportMemberRole
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();

        #endregion

        #region Public Methods

        /// <summary>
        /// Tìm kiếm thông tin vai tro công việc cần hỗ trợ
        /// </summary>
        /// <param name="list"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<WorksSupportMemberRole> list, params object[] objKeywords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter(objKeywords);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportMemberRole = new WorksSupportMemberRole();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupportMemberRole.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objWorksSupportMemberRole.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportMemberRole.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportMemberRole.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportMemberRole.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);                    
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportMemberRole.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportMemberRole.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    list.Add(objWorksSupportMemberRole);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportMemberRole -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        public ResultMessage SearchDataActived(ref List<WorksSupportMemberRole> list, params object[] objKeywords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectByActived);
                objIData.AddParameter(objKeywords);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportMemberRole = new WorksSupportMemberRole();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupportMemberRole.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objWorksSupportMemberRole.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportMemberRole.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportMemberRole.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportMemberRole.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportMemberRole.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportMemberRole.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    list.Add(objWorksSupportMemberRole);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportMemberRole -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        
        public ResultMessage GetAll(ref List<WorksSupportMemberRole> list)
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
                    var objWorksSupportMemberRole = new WorksSupportMemberRole();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupportMemberRole.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objWorksSupportMemberRole.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportMemberRole.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportMemberRole.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportMemberRole.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportMemberRole.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportMemberRole.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    list.Add(objWorksSupportMemberRole);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportMemberRole", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportMemberRole -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        public ResultMessage GetAllActived(ref List<WorksSupportMemberRole> list)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectAllActived);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportMemberRole = new WorksSupportMemberRole();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupportMemberRole.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objWorksSupportMemberRole.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportMemberRole.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportMemberRole.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportMemberRole.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportMemberRole.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportMemberRole.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    list.Add(objWorksSupportMemberRole);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportMemberRole", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportMemberRole -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportMemberRole by Id
        /// </summary>
        /// <param name="objWorksSupportMemberRole"></param>
        /// <param name="worksSupportMemberRoleId"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref WorksSupportMemberRole objWorksSupportMemberRole, int worksSupportMemberRoleId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSel);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", worksSupportMemberRoleId);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    WorksSupportMemberRole memberRole = new WorksSupportMemberRole();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) memberRole.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) memberRole.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) memberRole.Description = Convert.ToString(reader["DESCRIPTION"]);
                    if (!Convert.IsDBNull(reader["ICONURL"])) memberRole.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) memberRole.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["isactive"])) memberRole.IsActived = Convert.ToBoolean(reader["isactive"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) memberRole.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    if (!Convert.IsDBNull(reader["CREATEDDATE"])) memberRole.CreateDate = Convert.ToDateTime(reader["CREATEDDATE"]);
                    if (!Convert.IsDBNull(reader["UPDATEDUSER"])) memberRole.UpdatedUser = Convert.ToString(reader["UPDATEDUSER"]);
                    if (!Convert.IsDBNull(reader["CREATEDUSER"])) memberRole.CreatedUser = Convert.ToString(reader["CREATEDUSER"]);
                    if (!Convert.IsDBNull(reader["UPDATEDDATE"])) memberRole.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                    objWorksSupportMemberRole = memberRole;
                }
                else
                {
                    objWorksSupportMemberRole = null;
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportMemberRole", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportMemberRole -> GetById", "");
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
        /// <param name="objWorksSupportMemberRole"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResultMessage Insert(WorksSupportMemberRole objWorksSupportMemberRole, ref WorksSupportMemberRole obj)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                if (objWorksSupportMemberRole.WorksSupportMemberRoleId > 0)
                {
                    Update(objIData, objWorksSupportMemberRole, ref obj);
                }
                else
                {
                    Insert(objIData, objWorksSupportMemberRole);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupportMemberRole", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportMemberRole -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Xóa WorksSupportPriority
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultMessage Delete(string user, string ids)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                var listMember = ids.Split(',');
                foreach (var t in listMember)
                {
                    if (string.IsNullOrEmpty(t))
                        continue;
                    DeleteMemberRole(objIData, user, Convert.ToInt32(t));
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSupportMemberRole", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportMemberRole -> Delete", Globals.ModuleName);
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

        public ResultMessage SearchData_WorksSupportPriority(ref DataTable dtbData, string strKeywords, int intDelete, int intPageIndex, int intPageSize, int intIsActive)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@Keywords", strKeywords);
                objIData.AddParameter("@PAGEINDEX", intPageIndex);
                objIData.AddParameter("@PAGESIZE", intPageSize);
                objIData.AddParameter("@ISDELETED", intDelete);
                objIData.AddParameter("@ISACTIVE", intIsActive);
                dtbData = objIData.ExecStoreToDataTable();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin nhóm nội dung web", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportMemberRole -> WorksSupportMemberRole", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Insert data to table WorksSupportPriority
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportMemberRole"></param>
        protected virtual void Insert(IData objIData, WorksSupportMemberRole objWorksSupportMemberRole)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAdd);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLENAME", objWorksSupportMemberRole.WorksSupportMemberRoleName);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportMemberRole.Description);
                objIData.AddParameter("@ICONURL", objWorksSupportMemberRole.IconUrl);
                objIData.AddParameter("@ORDERINDEX", objWorksSupportMemberRole.OrderIndex);
                objIData.AddParameter("@ISACTIVE", objWorksSupportMemberRole.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportMemberRole.IsSystem);
                objIData.AddParameter("@CREATEDUSER", objWorksSupportMemberRole.CreatedUser);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);

                objWorksSupportMemberRole.WorksSupportMemberRoleId = Convert.ToInt32(objIData.ExecStoreToString());
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật data
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportMemberRole"></param>
        /// <param name="listWorksSupportMemberRole"></param>
        protected virtual void Update(IData objIData, WorksSupportMemberRole objWorksSupportMemberRole, ref WorksSupportMemberRole listWorksSupportMemberRole)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", objWorksSupportMemberRole.WorksSupportMemberRoleId);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLENAME", objWorksSupportMemberRole.WorksSupportMemberRoleName);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportMemberRole.Description);
                objIData.AddParameter("@ICONURL", objWorksSupportMemberRole.IconUrl);
                objIData.AddParameter("@ORDERINDEX", objWorksSupportMemberRole.OrderIndex);
                objIData.AddParameter("@ISACTIVE", objWorksSupportMemberRole.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportMemberRole.IsSystem);
                objIData.AddParameter("@UPDATEDUSER", objWorksSupportMemberRole.UpdatedUser);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);
                //objIData.ExecNonQuery();
                var dtb = objIData.ExecStoreToDataTable();
                var list = MyUtils.DataTableExtensions.ToList<WorksSupportMemberRole>(dtb);
                if (list != null && list.Count > 0)
                {
                    listWorksSupportMemberRole = list[0];
                }
                else
                {
                    listWorksSupportMemberRole = null;
                }
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Xóa data
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="user"></param>
        /// <param name="worksSupportMemberRoleId"></param>
        protected void DeleteMemberRole(IData objIData, string user, int worksSupportMemberRoleId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", worksSupportMemberRoleId);
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
        /// check name Existed
        /// </summary>
        /// <param name="objMemberValidate"></param>
        /// <param name="id"></param>
        /// <param name="strName"></param>
        /// <param name="intOrderindex"></param>
        /// <returns></returns>
        public ResultMessage CheckNameExisted(ref WorksSupportMemberRoleValidation objMemberValidate, int id, string strName, int intOrderindex)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchNameExisted);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", id);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLENAME", strName);
                objIData.AddParameter("@ORDERINDEX", intOrderindex);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objMemberValidate = new WorksSupportMemberRoleValidation();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objMemberValidate.CountName = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLENAME"]);
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objMemberValidate.CountOrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                }
                else
                {
                    objMemberValidate = null;
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi kiểm tra tên trùng nhau của quyền trên loại", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportMemberRole -> CheckNameExisted", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        
        /// <summary>
        /// get role by project id
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage SearchRoleByProjectId(ref List<WorksSupportMemberRole> list, int id)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectRoleByProjectId);
                objIData.AddParameter("@PROJECTID", id);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportMemberRole = new WorksSupportMemberRole();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objWorksSupportMemberRole.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objWorksSupportMemberRole.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]).Trim();

                    list.Add(objWorksSupportMemberRole);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin role theo projectId", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportMemberRole -> SearchRoleByProjectId", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Get all department
        /// </summary>
        /// <param name="lstDepartment"></param>
        /// <returns></returns>
        public ResultMessage GetDepartmentAll(ref List<MembersOfDepartment> lstDepartment)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectDepartmentAll);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objDepartment = new MembersOfDepartment();
                    if (!Convert.IsDBNull(reader["DEPARTMENTID"])) objDepartment.DepartmentId = Convert.ToString(reader["DEPARTMENTID"]);
                    if (!Convert.IsDBNull(reader["DEPARTMENTNAME"])) objDepartment.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["USERNAME"])) objDepartment.UserId = Convert.ToString(reader["USERNAME"]);
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objDepartment.UserName = Convert.ToString(reader["FULLNAME"]);
                    objDepartment.IsMember = null;
                    objDepartment.IsShowByDepart = true;
                    objDepartment.IsShowByMember = true;

                    lstDepartment.Add(objDepartment);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách SYS_DEPARTMENT", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportMemberRole -> GetDepartmentAll", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// 	CREATED BY	:	NGUYỄN VĂN HUÂN
        ///     DATE		:	22/8/2017
	    ///     DESCRIPTION	: KIỂM TRA VAI TRÒ ĐÃ ĐƯỢC SỬ DỤNG CHO DỰ ÁN.
        /// </summary>
        /// <param name="numberOfRole">số loại công việc đã có vai trò sử dụng</param>
        /// <param name="worksSupportMemberRoleId"></param>
        /// <returns></returns>
        public ResultMessage CheckRoleIsUsed(ref int numberOfRole, int worksSupportMemberRoleId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(spCheckRoleIsUsed);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", worksSupportMemberRoleId);
                numberOfRole = Convert.ToInt32(objIData.ExecStoreToString()); 
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp kiểm tra vai tro đã sử dụng cho dự án", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportMemberRole -> CheckRoleIsUsed", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        #endregion

        #region Stored Procedure Names

        public const string SpSelect = "WORKSSUPPORTMEMBERROLE_V2_SEL";
        public const string SpAdd = "WORKSSUPPORTMEMBERROLE_V2_ADD";
        public const string SpUpdate = "WORKSSUPPORTMEMBERROLE_V2_UPD";
        public const string SpDelete = "WORKSSUPPORTMEMBERROLE_V2_DEL";
        public const string SpSelectAll = "WORKSSUPPORTMEMBERROLE_V2_GETS";
        public const string SpSelectAllActived = "WORKSSUPPORTMEMBERROLE_V2_AT";
        public const string SpSelectByActived = "WORKSSUPPORTMEMBERROLE_V2_AT";
        public const string SpSel = "WORKSSUPPORTMEMBERROLE_V2_BY";
        public const string SpSearchNameExisted = "WORKSMEMBERROLE_CHECKNAME";
        public const string SpSelectRoleByProjectId = "WORKSSUPPORT_GROUP_SEL_ROLE";
        public const string SpSelectDepartmentAll = "SYS_DEPARTMENT_MEMBERS_SRH";
        public const string spCheckRoleIsUsed = "WSMEMBERROLE_V2_CHK";
        #endregion
    }
}
