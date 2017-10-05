using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Da.Configuration.ReportProces;
using Nc.Erp.WorksSupport.Do.Configuration.Group;
using Nc.Erp.WorksSupport.Do.Configuration.Project;
using Nc.Erp.WorksSupport.Do.Configuration.ReportProces;
using System;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Da.Configuration.Group
{
    /// <summary>
    /// Created by      : Nguyen Thi Kim Ngan
    /// Created date    : 08/06/2017
    /// Xử lý nghiệp vụ đối tượng ERP.EO_WORKSSUPPORTGROUP.
    /// </summary>
    public class DaWorksSupportGroup
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion

        #region Public Methods

        /// <summary>
        /// Tìm kiếm thông tin nhom công việc cần hỗ trợ
        /// </summary>
        /// <param name="listWorksSupportGroups"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<WorksSupportGroup> listWorksSupportGroups, params object[] objKeywords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearch);
                objIData.AddParameter(objKeywords);
                var dtbData = objIData.ExecStoreToDataTable();
                listWorksSupportGroups = MyUtils.DataTableExtensions.ToList<WorksSupportGroup>(dtbData);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin nhóm công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupportGroup -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// check values existed
        /// </summary>
        /// <param name="objWorkGroup"></param>
        /// <param name="intGroupId"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public ResultMessage CheckValuesExisted(ref ProjectValidation objWorkGroup, int intGroupId, string strName, int intProjectId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpCheck);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", intGroupId);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", intProjectId);
                objIData.AddParameter("@WORKSSUPPORTGROUPNAME", strName);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objWorkGroup = new ProjectValidation();
                    if (!Convert.IsDBNull(reader["GROUPNAME"])) objWorkGroup.CountName = Convert.ToInt32(reader["GROUPNAME"]);
                }
                else
                {
                    objWorkGroup = null;
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi kiểm tra thuộc tính trùng nhau của trạng thái dự án cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportProject -> CheckNameExisted", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportGroup by Id
        /// </summary>
        /// <param name="listWorksGroup"></param>
        /// <param name="workSupportProjectId"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref List<WorksSupportGroup> listWorksGroup, int workSupportProjectId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchById);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", workSupportProjectId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportGroup = new WorksSupportGroup();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWorksSupportGroup.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objWorksSupportGroup.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTID"])) objWorksSupportGroup.WorksSupportProjectId = Convert.ToInt32(reader["WORKSSUPPORTPROJECTID"]);
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportGroup.Description = Convert.ToString(reader["DESCRIPTION"]);
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportGroup.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportGroup.IsActived = Convert.ToInt32(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportGroup.IsSystem = Convert.ToInt32(reader["ISSYSTEM"]);
                    listWorksGroup.Add(objWorksSupportGroup);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportGroup", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get work support group by project id
        /// </summary>
        /// <param name="lstWorkGroup"></param>
        /// <param name="intProjectId"></param>
        /// <returns></returns>
        public ResultMessage GetWorksGroupsByProjectId(ref List<WorksSupportGroup> lstWorkGroup, int intProjectId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetWorksGroupByProjectId);
                objIData.AddParameter("@PROJECTID", intProjectId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportGroup = new WorksSupportGroup();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWorksSupportGroup.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objWorksSupportGroup.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["NUMBEROFMEMBER"])) objWorksSupportGroup.NumberOfMember = Convert.ToInt32(reader["NUMBEROFMEMBER"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWorksSupportGroup.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportGroup.IconUrl = Convert.ToString(reader["ICONURL"]);
                    lstWorkGroup.Add(objWorksSupportGroup);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportGroup", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> GetAllByProjectId", "");
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
        /// <param name="objWorksSupportStatus"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ResultMessage Insert(WorksSupportGroup objParam)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            int groupId = -1;
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();

                InsertGroup(objIData, objParam, ref groupId);
                if (groupId > -1)
                {
                    foreach (var item in objParam.LstNewMember)
                    {

                        item.WorksSupportGroupId = groupId;
                        new DaWorksSupportGroupMember().InsertGroup_Member(objIData, item);
                    }
                }

                objIData.CommitTransaction();
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
        /// Update data
        /// </summary>
        /// <param name="objWorksGroup"></param>
        /// <returns></returns>
        public ResultMessage UpdateGroup(WorksSupportGroup objWorksGroup)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                Update(objIData, objWorksGroup);
                // delete member
                foreach (var item in objWorksGroup.LstDeleteMember)
                {
                    Delete_GroupMember(objIData, objWorksGroup.DeletedUser, objWorksGroup.WorksSupportGroupId, item.UserName);
                }

                // edit member
                foreach (var item in objWorksGroup.LstEditMember)
                {
                    Update_MemberGroup(objIData, item);
                }

                foreach (var item in objWorksGroup.LstNewMember)
                {
                    InsertGroup_Member(objIData, item.WorksSupportGroupId, item.UserName, item.WorksSupportMemberRoleId, item.IsAutoaddMemberToWorksSupport, item.CreatedUser);
                }
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi cap nhat thông tin WorksSupportProject", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportProject -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Xóa nhom công việc cần hỗ trợ
        /// </summary>
        /// <param name="worksSupportGroupId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Delete(int worksSupportGroupId, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                DeleteStatus(objIData, user, worksSupportGroupId);
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
        /// Get WorksSupportProject by Id
        /// </summary>
        /// <param name="objProject"></param>
        /// <param name="worksSupportGroupId"></param>
        /// <returns></returns>
        public ResultMessage GetByWorksGroupId(ref WorksSupportGroup objProject, int worksSupportGroupId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetById);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worksSupportGroupId);

                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objProject.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objProject.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTID"])) objProject.WorksSupportProjectId = Convert.ToInt32(reader["WORKSSUPPORTPROJECTID"]);
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objProject.Description = Convert.ToString(reader["DESCRIPTION"]);
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objProject.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ICONURL"])) objProject.IconUrl = Convert.ToString(reader["ICONURL"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objProject.IsActived = Convert.ToInt32(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objProject.IsSystem = Convert.ToInt32(reader["ISSYSTEM"]);
                }
                else
                {
                    objProject = null;
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportProject", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportStatus -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportProject_Member by Id
        /// </summary>
        /// <param name="listGroupMember"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ResultMessage WorksSupportProjectMemberByGroupId(ref List<WorksSupportGroupMember> listGroupMember, int groupId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetMemberByGroupId);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", groupId);

                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objGroupMember = new WorksSupportGroupMember();
                    if (!Convert.IsDBNull(reader["USERNAME"])) objGroupMember.UserName = Convert.ToString(reader["USERNAME"]);
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objGroupMember.FullName = Convert.ToString(reader["FULLNAME"]);
                    if (!Convert.IsDBNull(reader["DEFAULTPICTUREURL"])) objGroupMember.DefaultPictureUrl = Convert.ToString(reader["DEFAULTPICTUREURL"]);
                    if (!Convert.IsDBNull(reader["DEPARTMENTNAME"])) objGroupMember.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]);
                    if (!Convert.IsDBNull(reader["ISAUTOADDMEMBERTOWORKSSUPPORT"])) objGroupMember.IsAutoAddMemberToWorksGroup = Convert.ToInt32(reader["ISAUTOADDMEMBERTOWORKSSUPPORT"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objGroupMember.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objGroupMember.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]).Trim();
                    listGroupMember.Add(objGroupMember);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportProject", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportStatus -> GetById", "");
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
        /// <param name="objWorksSupportGroup"></param>
        /// <param name="groupId"></param>
        protected virtual void InsertGroup(IData objIData, WorksSupportGroup objWorksSupportGroup, ref int groupId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAdd);
                objIData.AddParameter("@WORKSSUPPORTGROUPNAME", objWorksSupportGroup.WorksSupportGroupName);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", objWorksSupportGroup.WorksSupportProjectId);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportGroup.Description);
                objIData.AddParameter("@ICONURL", objWorksSupportGroup.IconUrl);
                objIData.AddParameter("@CREATEDUSER", objWorksSupportGroup.CreatedUser);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);
                groupId = Convert.ToInt32(objIData.ExecStoreToString());
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
        /// <param name="worksSupportGroupId"></param>
        protected void DeleteStatus(IData objIData, string user, int worksSupportGroupId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", worksSupportGroupId);
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
        /// get all member in work group
        /// </summary>
        /// <param name="lstGroupMember"></param>
        /// <param name="intWorkGroupId"></param>
        /// <returns></returns>
        public ResultMessage GetAllMemberByGroupId(ref List<WorkGroupMember> lstGroupMember, int intWorkGroupId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetAllMemberByGroupId);
                objIData.AddParameter("@WORKSGROUPID", intWorkGroupId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objGroupMember = new WorkGroupMember();
                    if (!Convert.IsDBNull(reader["USERNAME"])) objGroupMember.UserId = Convert.ToInt32(reader["USERNAME"]);
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objGroupMember.FullName = Convert.ToString(reader["FULLNAME"]);
                    if (!Convert.IsDBNull(reader["DEFAULTPICTUREURL"])) objGroupMember.DefaultPictureUrl = Convert.ToString(reader["DEFAULTPICTUREURL"]);
                    if (!Convert.IsDBNull(reader["DEPARTMENTNAME"])) objGroupMember.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objGroupMember.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objGroupMember.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]);
                    if (!Convert.IsDBNull(reader["ISAUTOADDMEMBERTOWORKSSUPPORT"])) objGroupMember.IsAutoAddMemberToWorksSupport = Convert.ToInt32(reader["ISAUTOADDMEMBERTOWORKSSUPPORT"]);

                    lstGroupMember.Add(objGroupMember);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin thành viên trong nhóm công việc", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> GetAllMemberByGroupId", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Xoa thanh vien trong nhom cong viec
        /// </summary>
        /// <param name="user"></param>
        /// <param name="memberId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ResultMessage DeleteMember(string user, int memberId, int groupId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpDeleteGroupMember);
                objIData.AddParameter("@USERID", memberId);
                objIData.AddParameter("@DELETEUSER", user);
                objIData.AddParameter("@GROUPID", groupId);
                objIData.ExecNonQuery();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi xóa thành viên trong nhóm công việc", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> DeleteMember", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        public ResultMessage InsertGroupMember(WorkGroupMember objMember)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.CreateNewStoredProcedure(SpAddGroupMember);
                objIData.AddParameter("@GROUPID", objMember.WorksSupportGroupId);
                objIData.AddParameter("@USERNAME", objMember.MemberUserName);
                objIData.AddParameter("@MEMBERROLEID", objMember.WorksSupportMemberRoleId);
                objIData.AddParameter("@CREATEDUSER", objMember.CreatedUser);
                objIData.ExecNonQuery();

            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin EO_WORKSSUPPORTGROUP_MEMBER", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportGroup -> InsertGroupMember", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        public ResultMessage GetAllMemberByProjectId(ref List<WorksSupportProject_Member> lstProjectMember, string userName)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetAllMemberByProjectId);
                objIData.AddParameter("@PROJECTID", userName);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objProject = new WorksSupportProject_Member();
                    if (!Convert.IsDBNull(reader["USERNAME"])) objProject.UserName = Convert.ToString(reader["USERNAME"]);
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objProject.FullName = Convert.ToString(reader["FULLNAME"]);
                    if (!Convert.IsDBNull(reader["DEFAULTPICTUREURL"])) objProject.DefaultPictureUrl = Convert.ToString(reader["DEFAULTPICTUREURL"]);
                    if (!Convert.IsDBNull(reader["DEPARTMENTNAME"])) objProject.DepartmentName = Convert.ToString(reader["DEPARTMENTNAME"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLEID"])) objProject.WorksSupportMemberRoleId = Convert.ToInt32(reader["WORKSSUPPORTMEMBERROLEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTMEMBERROLENAME"])) objProject.WorksSupportMemberRoleName = Convert.ToString(reader["WORKSSUPPORTMEMBERROLENAME"]);
                    if (!Convert.IsDBNull(reader["ISAUTOADDMEMBERTOWORKSGROUP"])) objProject.IsAutoAddMemberToWorksGroup = Convert.ToInt32(reader["ISAUTOADDMEMBERTOWORKSGROUP"]);
                    lstProjectMember.Add(objProject);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin thành viên dự án cho nhóm công việc", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> GetAllMemberByProjectId", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// Cập nhật trạng thái dự án cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportGroup"></param>
        protected virtual void Update(IData objIData, WorksSupportGroup objWorksSupportGroup)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", objWorksSupportGroup.WorksSupportGroupId);
                objIData.AddParameter("@WORKSSUPPORTGROUPNAME", objWorksSupportGroup.WorksSupportGroupName);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", objWorksSupportGroup.WorksSupportProjectId);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportGroup.Description);
                objIData.AddParameter("@UPDATEDUSER", objWorksSupportGroup.UpdatedUser);
                objIData.AddParameter("@ICONURL", objWorksSupportGroup.IconUrl);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);
                objIData.ExecNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get WorksSupportProject_Member by Id
        /// </summary>
        /// <param name="listWorksSupportProject"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage GetAllProjectByMemberId(ref List<WorksSupportProject> listWorksSupportProject, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetAllProjectByMemberId);
                objIData.AddParameter("@USERNAME", user);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportProject = new WorksSupportProject();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTID"])) objWorksSupportProject.WorksSupportProjectId = Convert.ToInt32(reader["WORKSSUPPORTPROJECTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTNAME"])) objWorksSupportProject.WorksSupportProjectName = Convert.ToString(reader["WORKSSUPPORTPROJECTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportProject.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORKGROUP"])) objWorksSupportProject.NumberOfWorkGroup = Convert.ToInt32(reader["NUMBEROFWORKGROUP"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFMEMBER"])) objWorksSupportProject.NumberOfMember = Convert.ToInt32(reader["NUMBEROFMEMBER"]);
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportProject.IconUrl = Convert.ToString(reader["ICONURL"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportProject.IsActived = Convert.ToInt32(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportProject.IsSystem = Convert.ToInt32(reader["ISSYSTEM"]);

                    listWorksSupportProject.Add(objWorksSupportProject);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType_Quality", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportProject -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// lay vai tro nhom cong viec theo vai tro thanh vien trong du an
        /// </summary>
        /// <param name="objWorkGroup"></param>
        /// <param name="intProjectId"></param>
        /// <param name="intWorkTypeId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ResultMessage GetworksGroupRole(ref WorksSupportGroup objWorkGroup, int intProjectId, int intWorkTypeId, string userName)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpGetWorkGroupRole);
                objIData.AddParameter("@PROJECTID", intProjectId);
                objIData.AddParameter("@USERNAME", userName);
                objIData.AddParameter("@WORKTYPEID", intWorkTypeId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    objWorkGroup = new WorksSupportGroup();
                    if (!Convert.IsDBNull(reader["ISCANADDWORKSSUPPORTGROUP"])) objWorkGroup.IsCanAddWorksSupportGroup = Convert.ToBoolean(reader["ISCANADDWORKSSUPPORTGROUP"]);
                    if (!Convert.IsDBNull(reader["ISCANADDWORKSSUPPORT"])) objWorkGroup.IsCanAddWorksSupport = Convert.ToBoolean(reader["ISCANADDWORKSSUPPORT"]);
                    if (!Convert.IsDBNull(reader["ISCANEDITWORKSSUPPORTGROUP"])) objWorkGroup.IsCanEditWorksSupportGroup = Convert.ToBoolean(reader["ISCANEDITWORKSSUPPORTGROUP"]);
                    if (!Convert.IsDBNull(reader["ISCANEDITWORKSSUPPORT"])) objWorkGroup.IsCandEitWorksSupport = Convert.ToBoolean(reader["ISCANEDITWORKSSUPPORT"]);
                    if (!Convert.IsDBNull(reader["ISCANDELETEWORKSSUPPORTGROUP"])) objWorkGroup.IsCanDeleteWorksSupportGroup = Convert.ToBoolean(reader["ISCANDELETEWORKSSUPPORTGROUP"]);
                    if (!Convert.IsDBNull(reader["ISCANDELETEWORKSSUPPORT"])) objWorkGroup.IsCanDeleteworkssupport = Convert.ToBoolean(reader["ISCANDELETEWORKSSUPPORT"]);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin vai tro danh sách WorksSupportGroup", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> GetworksGroupRole", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Xoa thanh vien cua nhom cong viec
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportStatus"></param>
        public void Delete_GroupMember(IData objIData, string DeleteUser, int intGroupId, string strUserName)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelWorkGroupMember);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", intGroupId);
                objIData.AddParameter("@MEMBERUSERNAME", strUserName);
                objIData.AddParameter("@DELETEDUSER", DeleteUser);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Cập nhật thành viên nhóm công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportProject"></param>
        protected virtual void Update_MemberGroup(IData objIData, WorksSupportGroupMember objGroupMember)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdateGroupMember);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", objGroupMember.WorksSupportGroupId);
                objIData.AddParameter("@MEMBERUSERNAME", objGroupMember.UserName);
                objIData.AddParameter("@UPDATEDUSER", objGroupMember.UpdatedUser);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", objGroupMember.WorksSupportMemberRoleId);
                objIData.AddParameter("@ISAUTOADDMEMBER", objGroupMember.IsAutoaddMemberToWorksSupport);

                objIData.ExecNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Thêm thanh vien trong ngom cong viec
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportStatus"></param>
        protected virtual void InsertGroup_Member(IData objIData, int intWS_GroupId, string strMemberUserName, int intWP_MemberRoleId, int intIsAutoGroup, string strUser)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAddWorkGroupMember);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", intWS_GroupId);
                objIData.AddParameter("@MEMBERUSERNAME", strMemberUserName);
                objIData.AddParameter("@WORKSSUPPORTMEMBERROLEID", intWP_MemberRoleId);
                objIData.AddParameter("@ISAUTOADDGROUPMEMBER", intIsAutoGroup);
                objIData.AddParameter("@CREATEDUSER", strUser);
                objIData.ExecNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// writen by  : Huan 
        /// create date: 09/08/2017
        /// kiểm tra xem thành viên đó có xử lý công việc nào hay không
        /// </summary>
        /// <param name="objWorkGroup"></param>
        /// <param name="userName"></param>
        /// <param name="workGroupId"></param>
        /// <returns></returns>
        public ResultMessage CheckUserIsProccess(ref WorksSupportGroup objWorkGroup, string userName, int workGroupId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectCheckUserWorkflow);
                objIData.AddParameter("@PROCESSUSER", userName);
                objIData.AddParameter("@WORKSSUPPORTGROUPID", workGroupId);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    if (!Convert.IsDBNull(reader["CHECKPROCESSUSER"])) objWorkGroup.NumberOfMember = Convert.ToInt32(reader["CHECKPROCESSUSER"]);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tìm kiếm user đang xử lý công việc", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> CheckUserIsProccess", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        /// <summary>
        /// Writen by  : Nguyen van Huan
        /// Create by  : 16.08.2017
        /// Description: get all project that is active and user have role with project.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage GetProjectIsActived(ref List<WorksSupportProject> list, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectAllProjectIsActive);

                objIData.AddParameter("@USERNAME", user);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportProject = new WorksSupportProject();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTID"])) objWorksSupportProject.WorksSupportProjectId = Convert.ToInt32(reader["WORKSSUPPORTPROJECTID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTPROJECTNAME"])) objWorksSupportProject.WorksSupportProjectName = Convert.ToString(reader["WORKSSUPPORTPROJECTNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportProject.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportProject.IconUrl = Convert.ToString(reader["ICONURL"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportProject.IsActived = Convert.ToInt32(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportProject.IsSystem = Convert.ToInt32(reader["ISSYSTEM"]);
                    if (!Convert.IsDBNull(reader["ISCANADD"])) objWorksSupportProject.IsCanAdd = Convert.ToBoolean(reader["ISCANADD"]);
                    if (!Convert.IsDBNull(reader["ISCANDELETE"])) objWorksSupportProject.IsCanDelete = Convert.ToBoolean(reader["ISCANDELETE"]);
                    if (!Convert.IsDBNull(reader["ISCANEDIT"])) objWorksSupportProject.IsCanEdit = Convert.ToBoolean(reader["ISCANEDIT"]);
                    if (!Convert.IsDBNull(reader["ISCANVIEW"])) objWorksSupportProject.IsCanView = Convert.ToBoolean(reader["ISCANVIEW"]);
                    if (!Convert.IsDBNull(reader["ISCANVIEWREPORT"])) objWorksSupportProject.IsCanViewReport = Convert.ToBoolean(reader["ISCANVIEWREPORT"]);

                    list.Add(objWorksSupportProject);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách dự án ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> GetProjectIsActived", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        /// tim kiem nhom cong viec
        /// </summary>
        /// <param name="lstWorkGroup"></param>
        /// <param name="intProjectId"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public ResultMessage SearchWorksGroupsByKeyWords(ref List<WorksSupportGroup> lstWorkGroup, int intProjectId, string keyWords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchById);
                objIData.AddParameter("@WORKSSUPPORTPROJECTID", intProjectId);
                objIData.AddParameter("@KEYWORD", keyWords);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportGroup = new WorksSupportGroup();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPID"])) objWorksSupportGroup.WorksSupportGroupId = Convert.ToInt32(reader["WORKSSUPPORTGROUPID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTGROUPNAME"])) objWorksSupportGroup.WorksSupportGroupName = Convert.ToString(reader["WORKSSUPPORTGROUPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["NUMBEROFMEMBER"])) objWorksSupportGroup.NumberOfMember = Convert.ToInt32(reader["NUMBEROFMEMBER"]);
                    if (!Convert.IsDBNull(reader["NUMBEROFWORK"])) objWorksSupportGroup.NumberOfWork = Convert.ToInt32(reader["NUMBEROFWORK"]);
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportGroup.IconUrl = Convert.ToString(reader["ICONURL"]);
                    lstWorkGroup.Add(objWorksSupportGroup);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp tim kiếm thông tin danh sách WorksSupportGroup", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportGroup -> SearchWorksGroupsByKeyWorkds", "");
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
        public const string SpSearch = "WORKSSUPPORTGROUP_V2_SEARCH";
        public const string SpSearchById = "WORKSSUPPORTGROUP_V2_SRH";
        public const string SpGetWorksGroupByProjectId = "WORKSSUPPORTGROUP_V2_GETGROUP";
        public const string SpAdd = "WORKSSUPPORTGROUP_V2_ADD";
        public const string SpUpdate = "WORKSSUPPORTGROUP_V2_UPD";
        //public const string SpDelete = "WORKSSUPPORTGROUP_V2_DEL";
        public const string SpDelete = "WORKSUPORTGROUP_V2_DEL";
        public const string SpGetAllMemberByGroupId = "WORKSSUPPORTGROUP_V2_MEMBERS";
        public const string SpDeleteGroupMember = "WORKSSUPPORTGROUP_MEMBER_DEL";
        public const string SpAddGroupMember = "WORKSSUPPORTGROUPMEMBER_V2_ADD";
        public const string SpGetAllMemberByProjectId = "WORKSSUPPORTPROJ_V2_MEMBYPROJ";
        public const string SpCheck = "WORKSSUPORTGROUP_V2_CHECK";
        public const string SpGetById = "WORKSSUPORTGROUP_V2_GETBYID";
        public const string SpGetMemberByGroupId = "WORKSSUPORTGROUP_V2_GETMEMBER";
        public const string SpGetAllProjectByMemberId = "WORKSSUPORTPROJECT_V2_ISACTIVE";
        public const string SpGetWorkGroupRole = "WORKSSUPPORTGROUP_V2_GETROLE";
        public const string SpDelWorkGroupMember = "WSGROUP_MEMBER_V2_DEL";
        public const string SpUpdateGroupMember = "WSGROUP_MEMBER_V2_UPD";
        public const string SpAddWorkGroupMember = "WS_GROUP_MEMBER_V2_ADD";
        public const string SpSelectCheckUserWorkflow = "WORKSSUPORTGROUP_V2_CHK_USER";
        public const string SpSelectAllProjectIsActive = "WORKSSUPORTPROJECT_V2_ROLE_ACT";
        #endregion
    }
}
