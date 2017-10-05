﻿using System;
using System.Collections.Generic;
using System.Data;
using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType;
using Newtonsoft.Json;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorkSupportType
{
    /// <summary>
    /// Created by      : Lương Trung Nhân
    /// Created date    : 29/05/2017
    /// Xử lý nghiệp vụ đối tượng ERP.EO.WORKSSUPPORTType.
    /// Khai báo trạng thái công việc cần hỗ trợ
    /// </summary>
    public class DaWorksSupportType
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion

        #region Chi tiết công việc cần hỗ trợ
        #region Public Methods

        /// <summary>
        /// Tìm kiếm thông tin trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="list"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<WorksSupportType> list, params object[] objKeywords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearch);
                objIData.AddParameter(objKeywords);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportType = new WorksSupportType();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportType.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPENAME"])) objWorksSupportType.WorksSupportTypeName = Convert.ToString(reader["WORKSSUPPORTTYPENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportType.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ADDFUNCTIONID"])) objWorksSupportType.AddFunctionId = Convert.ToString(reader["ADDFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["VIEWALLFUNCTIONID"])) objWorksSupportType.ViewAllFunctionId = Convert.ToString(reader["VIEWALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITFUNCTIONID"])) objWorksSupportType.EditFunctionId = Convert.ToString(reader["EDITFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITALLFUNCTIONID"])) objWorksSupportType.EditAllFunctionId = Convert.ToString(reader["EDITALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEFUNCTIONID"])) objWorksSupportType.DeleteFunctionId = Convert.ToString(reader["DELETEFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEALLFUNCTIONID"])) objWorksSupportType.DeleteAllFunctionId = Convert.ToString(reader["DELETEALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"])) objWorksSupportType.ProcessFunctionId = Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["COMMENTFUNCTIONID"])) objWorksSupportType.CommentFunctionId = Convert.ToString(reader["COMMENTFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportType.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportType.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportType.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportType.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    list.Add(objWorksSupportType);
                }

            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData, "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
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
        /// GetAlll WorksSupportType 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorksSupportType> list)
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
                    var objWorksSupportType = new WorksSupportType();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportType.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPENAME"])) objWorksSupportType.WorksSupportTypeName = Convert.ToString(reader["WORKSSUPPORTTYPENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportType.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ADDFUNCTIONID"])) objWorksSupportType.AddFunctionId = Convert.ToString(reader["ADDFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["VIEWALLFUNCTIONID"])) objWorksSupportType.ViewAllFunctionId = Convert.ToString(reader["VIEWALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITFUNCTIONID"])) objWorksSupportType.EditFunctionId = Convert.ToString(reader["EDITFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITALLFUNCTIONID"])) objWorksSupportType.EditAllFunctionId = Convert.ToString(reader["EDITALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEFUNCTIONID"])) objWorksSupportType.DeleteFunctionId = Convert.ToString(reader["DELETEFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEALLFUNCTIONID"])) objWorksSupportType.DeleteAllFunctionId = Convert.ToString(reader["DELETEALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"])) objWorksSupportType.ProcessFunctionId = Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["COMMENTFUNCTIONID"])) objWorksSupportType.CommentFunctionId = Convert.ToString(reader["COMMENTFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportType.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportType.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportType.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportType.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);

                    list.Add(objWorksSupportType);
                }
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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

        public ResultMessage GetAllActived(ref List<WorksSupportType> list)
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
                    var objWorksSupportType = new WorksSupportType();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportType.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPENAME"])) objWorksSupportType.WorksSupportTypeName = Convert.ToString(reader["WORKSSUPPORTTYPENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportType.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ADDFUNCTIONID"])) objWorksSupportType.AddFunctionId = Convert.ToString(reader["ADDFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["VIEWALLFUNCTIONID"])) objWorksSupportType.ViewAllFunctionId = Convert.ToString(reader["VIEWALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITFUNCTIONID"])) objWorksSupportType.EditFunctionId = Convert.ToString(reader["EDITFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITALLFUNCTIONID"])) objWorksSupportType.EditAllFunctionId = Convert.ToString(reader["EDITALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEFUNCTIONID"])) objWorksSupportType.DeleteFunctionId = Convert.ToString(reader["DELETEFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEALLFUNCTIONID"])) objWorksSupportType.DeleteAllFunctionId = Convert.ToString(reader["DELETEALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"])) objWorksSupportType.ProcessFunctionId = Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["COMMENTFUNCTIONID"])) objWorksSupportType.CommentFunctionId = Convert.ToString(reader["COMMENTFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportType.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportType.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportType.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportType.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);

                    list.Add(objWorksSupportType);
                }
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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
        /// Get WorksSupportType by Id
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="intWorkSupportTypeId"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref WorksSupportType objWorksSupportType, int intWorkSupportTypeId)
        {
            ResultMessage objResultMessage;
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intWorkSupportTypeId);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objWorksSupportType = new WorksSupportType();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportType.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPENAME"])) objWorksSupportType.WorksSupportTypeName = Convert.ToString(reader["WORKSSUPPORTTYPENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportType.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ADDFUNCTIONID"])) objWorksSupportType.AddFunctionId = Convert.ToString(reader["ADDFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["VIEWALLFUNCTIONID"])) objWorksSupportType.ViewAllFunctionId = Convert.ToString(reader["VIEWALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITFUNCTIONID"])) objWorksSupportType.EditFunctionId = Convert.ToString(reader["EDITFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITALLFUNCTIONID"])) objWorksSupportType.EditAllFunctionId = Convert.ToString(reader["EDITALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEFUNCTIONID"])) objWorksSupportType.DeleteFunctionId = Convert.ToString(reader["DELETEFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEALLFUNCTIONID"])) objWorksSupportType.DeleteAllFunctionId = Convert.ToString(reader["DELETEALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"])) objWorksSupportType.ProcessFunctionId = Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["COMMENTFUNCTIONID"])) objWorksSupportType.CommentFunctionId = Convert.ToString(reader["COMMENTFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportType.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportType.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportType.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportType.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                }
                // Nạp thông tin Bảng liên kết loại công việc và độ ưu tiên
                var daPriority = new DaWorkSupportTypePriority();
                var lstWorksSupportTypePriority = new List<WorksSupportTypePriority>();
                daPriority.GetById(ref lstWorksSupportTypePriority, intWorkSupportTypeId, objIData);
                if(lstWorksSupportTypePriority !=null && lstWorksSupportTypePriority.Count > 0)
                {
                    objWorksSupportType.ListWorksSupportTypePriority = lstWorksSupportTypePriority;
                }
                // Nạp thông tin Bảng liên kết loại công việc và chất lượng công việc
                var daQuality = new DaWorkSupportTypeQuality();
                var lstWorksSupportTypeQuality = new List<WorksSupportTypeQuality>();
                daQuality.GetById(ref lstWorksSupportTypeQuality, intWorkSupportTypeId, objIData);
                if (lstWorksSupportTypeQuality != null && lstWorksSupportTypeQuality.Count > 0)
                {
                    objWorksSupportType.ListWorksSupportTypeQuality = lstWorksSupportTypeQuality;
                }
                // Nạp thông tin Bảng chứa các thuộc tính bước xử lý
                var daWorkFlow = new DaWorkSupportTypeWorkFlow();
                var lstWorksSupportTypeWorkFlow = new List<WorksSupportTypeWorkFlow>();
                // WorksSupportType_WorkFlow objWorksSupportType_WorkFlow = new WorksSupportType_WorkFlow();
                daWorkFlow.GetById(ref lstWorksSupportTypeWorkFlow, intWorkSupportTypeId, objIData);
                {
                    objWorksSupportType.ListWorksSupportTypeWorkFlow = lstWorksSupportTypeWorkFlow;
                }
                // Nạp thông tin Bảng chứa các thuộc tính bước xử lý
                var daMemberRole = new DaWorksSupportTypeMemberRole();
                var lstWorksSupportTypeMemberRole = new List<WorksSupportTypeMemberRole>();
                // WorksSupportType_WorkFlow objWorksSupportType_WorkFlow = new WorksSupportType_WorkFlow();
                objResultMessage = daMemberRole.GetByIdForPermission(ref lstWorksSupportTypeMemberRole, intWorkSupportTypeId, objIData);
                if (!objResultMessage.IsError && lstWorksSupportTypeMemberRole != null && lstWorksSupportTypeMemberRole.Count > 0)
                {
                    objWorksSupportType.ListWorksSupportTypeMemberRole = lstWorksSupportTypeMemberRole;
                }
                // Nạp thông tin Bảng chứa các thuộc tính bước xử lý
                var daProjectPermi = new DaWorksSupportProjectPermis();
                var lstWorksSupportProjectPermis = new List<WorksSupportProjectPermis>();
                // WorksSupportType_WorkFlow objWorksSupportType_WorkFlow = new WorksSupportType_WorkFlow();
                objResultMessage = daProjectPermi.GetById(ref lstWorksSupportProjectPermis, intWorkSupportTypeId, objIData);
                if (!objResultMessage.IsError && lstWorksSupportProjectPermis != null && lstWorksSupportProjectPermis.Count > 0)
                {
                    objWorksSupportType.ListWorksSupportProjectPermis = lstWorksSupportProjectPermis;
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
                    objIData.BeginTransaction();
                    var nextWorksSupportTypeId = Insert(objIData, objWorksSupportType, user);
                    //insert WorksSupportType_priority
                    if (objWorksSupportType.ListWorksSupportTypePriority != null && objWorksSupportType.ListWorksSupportTypePriority.Count > 0)
                    {
                        new DaWorkSupportTypePriority().Insert(objWorksSupportType.ListWorksSupportTypePriority, objIData, nextWorksSupportTypeId);
                    }   
                    // insert WorkSupportType_Quality
                    if (objWorksSupportType.ListWorksSupportTypeQuality != null && objWorksSupportType.ListWorksSupportTypeQuality.Count > 0)
                    {
                        new DaWorkSupportTypeQuality().Insert(objWorksSupportType.ListWorksSupportTypeQuality, objIData, nextWorksSupportTypeId);
                    }
                    // insert WorkSupportType_WorkFlow
                    if (objWorksSupportType.ListWorksSupportTypeWorkFlow != null && objWorksSupportType.ListWorksSupportTypeWorkFlow.Count > 0)
                    {
                        new DaWorkSupportTypeWorkFlow().Insert(objWorksSupportType.ListWorksSupportTypeWorkFlow, objIData, nextWorksSupportTypeId, user);
                    }
                    // insert WorksSupportProject_Permis
                    if (objWorksSupportType.ListWorksSupportProjectPermis != null && objWorksSupportType.ListWorksSupportProjectPermis.Count > 0)
                    {
                        new DaWorksSupportProjectPermis().Insert(objWorksSupportType.ListWorksSupportProjectPermis, nextWorksSupportTypeId, objIData);
                    }
                    // insert WorksSupportType_MemberRole
                    if (objWorksSupportType.ListWorksSupportTypeMemberRole != null && objWorksSupportType.ListWorksSupportTypeMemberRole.Count > 0)
                    {
                        new DaWorksSupportTypeMemberRole().Insert(objWorksSupportType.ListWorksSupportTypeMemberRole,nextWorksSupportTypeId, objIData);
                    }
                    objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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
        /// Copy WorksType
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage CopyWorksType(WorksSupportType objWorksSupportType, string user)
        {
            var prefix = "Copy_";
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                objWorksSupportType.WorksSupportTypeName = prefix + objWorksSupportType.WorksSupportTypeName;
                var nextWorksSupportTypeId = Insert(objIData, objWorksSupportType, user);
                //insert WorksSupportType_priority
                if (objWorksSupportType.ListWorksSupportTypePriority != null && objWorksSupportType.ListWorksSupportTypePriority.Count > 0)
                {
                    new DaWorkSupportTypePriority().Insert(objWorksSupportType.ListWorksSupportTypePriority, objIData, nextWorksSupportTypeId);
                }
                // insert WorkSupportType_Quality
                if (objWorksSupportType.ListWorksSupportTypeQuality != null && objWorksSupportType.ListWorksSupportTypeQuality.Count > 0)
                {
                    new DaWorkSupportTypeQuality().Insert(objWorksSupportType.ListWorksSupportTypeQuality, objIData, nextWorksSupportTypeId);
                }
                // insert WorkSupportType_WorkFlow
                if (objWorksSupportType.ListWorksSupportTypeWorkFlow != null && objWorksSupportType.ListWorksSupportTypeWorkFlow.Count > 0)
                {
                    new DaWorkSupportTypeWorkFlow().CopyWorkFlow(objWorksSupportType.ListWorksSupportTypeWorkFlow, objIData, nextWorksSupportTypeId, user);
                }
                // insert WorksSupportProject_Permis
                /*if (objWorksSupportType.ListWorksSupportProjectPermis != null && objWorksSupportType.ListWorksSupportProjectPermis.Count > 0)
                {
                    new DaWorksSupportProjectPermis().Insert(objWorksSupportType.ListWorksSupportProjectPermis, nextWorksSupportTypeId, objIData);
                }
                // insert WorksSupportType_MemberRole
                if (objWorksSupportType.ListWorksSupportTypeMemberRole != null && objWorksSupportType.ListWorksSupportTypeMemberRole.Count > 0)
                {
                    new DaWorksSupportTypeMemberRole().Insert(objWorksSupportType.ListWorksSupportTypeMemberRole, nextWorksSupportTypeId, objIData);
                }*/
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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
        /// Update data
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        /// <param name="listWorksFlowDeletedId"></param>
        /// <returns></returns>
        public ResultMessage UpdateWorksType(WorksSupportType objWorksSupportType, string user, string listWorksFlowDeletedId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();

                // Update data.
                Update(objIData, objWorksSupportType, user);

                //insert WorksSupportType_priority
                var daWorkSupportTypePriority = new DaWorkSupportTypePriority();
                
                    daWorkSupportTypePriority.Update(objWorksSupportType.ListWorksSupportTypePriority, objIData, objWorksSupportType.WorksSupportTypeId);
                // update WorkSupportType_Quality
                
                    new DaWorkSupportTypeQuality().Update(objWorksSupportType.ListWorksSupportTypeQuality, objIData, objWorksSupportType.WorksSupportTypeId);
                // insert WorkSupportType_WorkFlow
               
                    new DaWorkSupportTypeWorkFlow().Update(objWorksSupportType.ListWorksSupportTypeWorkFlow, objIData, user, listWorksFlowDeletedId);
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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
        /// Xóa trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Delete(WorksSupportType objWorksSupportType, string user)
        {
            ResultMessage objResultMessage;
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                // Delete worksSupportStatus
                Delete(objIData, objWorksSupportType, user);
                //Xoas bảng liên kết loại công việc và độ ưu tiên
                var daPriority = new DaWorkSupportTypePriority();
                var objPriority =
                    new WorksSupportTypePriority {WorksSupportTypeId = objWorksSupportType.WorksSupportTypeId};
                daPriority.Delete(objPriority, objIData);
                // Xoa bảng liên kết loại công việc và chat luong
                var daQuality = new DaWorkSupportTypeQuality();
                daQuality.Delete(objWorksSupportType.WorksSupportTypeId, objIData);
                //xóa bảng bước xử lý
                var daWorkFlow = new DaWorkSupportTypeWorkFlow();
                var objWorkFlow =
                    new WorksSupportTypeWorkFlow
                    {
                        WorksSupportTypeId = objWorksSupportType.WorksSupportTypeId
                    };
                daWorkFlow.Delete(objWorkFlow, objIData, user);
                //xóa bảng bước xử lý
                var daMemberRole = new DaWorksSupportTypeMemberRole();
                daMemberRole.Delete(objWorksSupportType.WorksSupportTypeId, objIData);
                //xóa bảng bước xử lý
                var daPermis = new DaWorksSupportProjectPermis();
                var objProjectPermis =
                    new WorksSupportProjectPermis
                    {
                        WorksSupportTypeId = objWorksSupportType.WorksSupportTypeId,
                        UserName = "-1"
                    };
                //Xóa hết thông tin theo workssuportTypeID
                objResultMessage = daPermis.Delete(objProjectPermis.WorksSupportTypeId, objIData);
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSuportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupportType -> Delete", Globals.ModuleName);
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
        /// <param name="userDelete"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultMessage DeleteWorksType(string userDelete, string ids)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();

                var lstString = ids.Split(',');
                foreach (var t in lstString)
                {
                    if (string.IsNullOrEmpty(t))
                        continue;
                    var id = t;
                    var priorityId = Convert.ToInt32(id);
                    DeleteWorksType(objIData, userDelete, priorityId);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSuportWorksType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSuportWorksType -> Delete", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        // check name Existed
        public ResultMessage CheckValueExist(ref WorksSupportTypeValidation objTypeValidate, int intId, string strName, int intOrderindex)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearchNameExisted);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intId);
                objIData.AddParameter("@WORKSSUPPORTTYPENAME", strName);
                objIData.AddParameter("@ORDERINDEX", intOrderindex);
                IDataReader reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objTypeValidate = new WorksSupportTypeValidation();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPENAME"])) objTypeValidate.CountName = Convert.ToInt32(reader["WORKSSUPPORTTYPENAME"]);
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objTypeValidate.CountOrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                }
                else
                {
                    objTypeValidate = null;
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
        /// Save Permission
        /// </summary>
        /// <param name="jSonPermission"></param>
        /// <returns></returns>
        public ResultMessage SaveEditPermission(string jSonPermission)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {

                objIData.Connect();
                objIData.BeginTransaction();

                dynamic permissionDeserializeObject = JsonConvert.DeserializeObject(jSonPermission);

                var worksSupportTypeId = int.Parse(permissionDeserializeObject.WorksSupportTypeId.ToString());
                var user = permissionDeserializeObject.User.ToString();

                var listWorksSupportProjectPermission = permissionDeserializeObject.ListWorksSupportProjectPermis;
                var listWorksSupportTypeMemberRole = permissionDeserializeObject.ListWorksSupportTypeMemberRole;
                var listWorksSupportTypeWorkFlow = permissionDeserializeObject.ListWorksSupportTypeWorkFlow;

                new DaWorksSupportTypeWfPermis().DeleteDataByWorksSupportTypeId(worksSupportTypeId, objIData);
                new DaWorksSupportTypeWfnxPermis().Delete(worksSupportTypeId, objIData);
                if (listWorksSupportTypeWorkFlow != null)
                {
                    foreach (var workFlow in listWorksSupportTypeWorkFlow)
                    {
                        var listWorksSupportTypeWfNxPermission = workFlow.ListWorksSupportTypeWfNxPermis;
                        var listWorksSupportTypeWfPermission = workFlow.ListWorksSupportTypeWfPermis;
                        if (listWorksSupportTypeWfPermission != null)
                        {
                            foreach (var item in listWorksSupportTypeWfPermission)
                            {

                                var worksSupportMemberRoleId = 0;
                                if (item.WorksSupportMemberRoleId != null)
                                {
                                    worksSupportMemberRoleId = int.Parse(item.WorksSupportMemberRoleId.ToString());
                                }
                                var worksSupportStepId = 0;
                                if (item.WorksSupportStepId != null)
                                {
                                    worksSupportStepId = int.Parse(item.WorksSupportStepId.ToString());
                                }

                                if (worksSupportMemberRoleId <= 0) continue;
                                // Delete all data of WorksSupportTypeWfPermission
                                var checkIsProcessWorkFlow = CheckIsProcessWorkFlow(worksSupportStepId, listWorksSupportTypeWfNxPermission);
                                var itemPermission = new WorksSupportTypeWfPermis
                                                     {
                                                         WorksSupportStepId = worksSupportStepId,
                                                         WorksSupportMemberRoleId = worksSupportMemberRoleId,
                                                         IsCanAddAttachment = item.IsCanAddAttachment,
                                                         IsCanChangeProgress = item.IsCanChangeProgress,
                                                         IsCanComment = item.IsCanComment,
                                                         IsCanEditContent = item.IsCanEditContent,
                                                         IsCanEditExpectedCompletedDate = item.IsCanEditExpectedCompletedDate,
                                                         IsCanEditQuality = item.IsCanEditQuality,
                                                         IsCanEditSolutionContent = item.IsCanEditSolutionContent,
                                                         IsCanProcessWorkflow = checkIsProcessWorkFlow
                                                     };
                                new DaWorksSupportTypeWfPermis().InsertWorksFlowPermission(itemPermission, objIData, user);
                            }
                        }

                        if (listWorksSupportTypeWfNxPermission == null) continue;
                        {
                            foreach (var item in listWorksSupportTypeWfNxPermission)
                            {
                                var nextWorksSupportStepId = 0;
                                if (item.NextWorksSupportStepId != null)
                                {
                                    nextWorksSupportStepId = item.NextWorksSupportStepId;
                                }
                                var worksSupportStepId = 0;
                                if (item.WorksSupportStepId != null)
                                {
                                    worksSupportStepId = item.WorksSupportStepId;
                                }
                                var worksSupportMemberRoleId = 0;
                                if (item.WorksSupportMemberRoleId != null)
                                {
                                    worksSupportMemberRoleId = item.WorksSupportMemberRoleId;
                                }
                                var itePermission = new WorksSupportTypeWfnxPermis
                                                        {
                                                            WorksSupportTypeId = worksSupportTypeId,
                                                            NextWorksSupportStepId = nextWorksSupportStepId,
                                                            WorksSupportStepId = worksSupportStepId,
                                                            WorksSupportMemberRoleId = worksSupportMemberRoleId
                                                        };
                                new DaWorksSupportTypeWfnxPermis().Insert(itePermission, objIData);
                            }
                        }
                    }
                }

                // Delete data in project permission by WorksSupportTypeId.
                new DaWorksSupportProjectPermis().Delete(worksSupportTypeId, objIData);
                
                if (listWorksSupportProjectPermission != null)
                {
                    foreach (var itemFPermission in listWorksSupportProjectPermission)
                    {
                        var newItemFPermission = new WorksSupportProjectPermis
                        {
                            WorksSupportTypeId = worksSupportTypeId,
                            UserName = itemFPermission.UserName,
                            IsCanAddProject = itemFPermission.IsCanAddProject,
                            IsCanDeleteProject = itemFPermission.IsCanDeleteProject,
                            IsCanEditProject = itemFPermission.IsCanEditProject,
                            IsCanViewProject = 1,
                            IsCanViewProjectReport = itemFPermission.IsCanViewProjectReport
                        };
                        new DaWorksSupportProjectPermis().InsertProjectPermission(newItemFPermission, objIData);
                    }
                }

                // Delete data in member role by WorksSupportTypeId
                new DaWorksSupportTypeMemberRole().DeleteByTypeIdAndMemberRoleId(worksSupportTypeId, objIData);

                if (listWorksSupportTypeMemberRole != null)
                {
                    foreach (var itemMemberRole in listWorksSupportTypeMemberRole)
                    {
                        var worksSupportMemberRoleId = 0;
                        if (itemMemberRole.WorksSupportMemberRoleId != null)
                        {
                            worksSupportMemberRoleId = itemMemberRole.WorksSupportMemberRoleId;
                        }

                        var newItemMemberRole = new WorksSupportTypeMemberRole
                        {
                            WorksSupportTypeId = worksSupportTypeId,
                            WorksSupportMemberRoleId = worksSupportMemberRoleId,
                            WorksSupportMemberRoleName = itemMemberRole.WorksSupportMemberRoleName,
                            IsCanAddWorksSupportGroup = itemMemberRole.IsCanAddWorksSupportGroup,
                            IsCanAddWorksSupport = itemMemberRole.IsCanAddWorksSupport,
                            IsCanEditWorksSupportGroup = itemMemberRole.IsCanEditWorksSupportGroup,
                            IsCanEditWorksSupport = itemMemberRole.IsCanEditWorksSupport,
                            IsCanDeleteWorksSupportGroup = itemMemberRole.IsCanDeleteWorksSupportGroup,
                            IsCanDeleteWorksSupport = itemMemberRole.IsCanDeleteWorksSupport,
                            IsDefaultRole = itemMemberRole.IsDefaultRole == false ? 0 : 1
                        };

                        new DaWorksSupportTypeMemberRole().InsertMemberRole(newItemMemberRole,worksSupportTypeId, objIData);
                    }
                }
                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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

        private bool CheckIsProcessWorkFlow(int worksSupportStepId, dynamic list)
        {
            if (list == null) return false;
            foreach (var item in list)
            {
                if (item.WorksSupportStepId != worksSupportStepId) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Copy data
        /// </summary>
        /// <param name="strFromWorksTypeId"></param>
        /// <param name="strToWorksTypeId"></param>
        /// <returns></returns>
        public ResultMessage CopyWorksType(string strFromWorksTypeId, string strToWorksTypeId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            var fromWorksTypeId = int.Parse(strFromWorksTypeId);
            var toWorksTypeId = int.Parse(strToWorksTypeId);
            try
            {
                
                objIData.Connect();
                objIData.BeginTransaction();
                // Copy data WorksSupportProjectPermis
                var listWorksSupportProjectPermis = new List<WorksSupportProjectPermis>();
                new DaWorksSupportProjectPermis().GetById(ref listWorksSupportProjectPermis, fromWorksTypeId, objIData);
                // Delete Project persmission
                new DaWorksSupportProjectPermis().Delete(toWorksTypeId, objIData);
                if (listWorksSupportProjectPermis.Count > 0)
                {
                    foreach (var itemFPermis in listWorksSupportProjectPermis)
                    {
                        var newItemFPermis = new WorksSupportProjectPermis
                        {
                            WorksSupportTypeId = toWorksTypeId,
                            UserName = itemFPermis.UserName,
                            IsCanAddProject = itemFPermis.IsCanAddProject,
                            IsCanDeleteProject = itemFPermis.IsCanDeleteProject,
                            IsCanEditProject = itemFPermis.IsCanEditProject,
                            IsCanViewProject = itemFPermis.IsCanViewProject,
                            IsCanViewProjectReport = itemFPermis.IsCanViewProjectReport
                        };
                        new DaWorksSupportProjectPermis().InsertProjectPermission(newItemFPermis, objIData);
                    }
                }

                // Copy data WorksSupportTypeMemberRole
                var listWorksSupportTypeMemberRole = new List<WorksSupportTypeMemberRole>();
                new DaWorksSupportTypeMemberRole().GetById(ref listWorksSupportTypeMemberRole, fromWorksTypeId, objIData);
                // Delete MemberRole 
                new DaWorksSupportTypeMemberRole().Delete(toWorksTypeId, objIData);
                if (listWorksSupportTypeMemberRole.Count > 0)
                {
                    foreach (var itemMemberRole in listWorksSupportTypeMemberRole)
                    {
                        var newItemMemberRole = new WorksSupportTypeMemberRole
                        {
                            WorksSupportTypeId = toWorksTypeId,
                            WorksSupportMemberRoleId = itemMemberRole.WorksSupportMemberRoleId,
                            WorksSupportMemberRoleName = itemMemberRole.WorksSupportMemberRoleName,
                            IsCanAddWorksSupportGroup = itemMemberRole.IsCanAddWorksSupportGroup,
                            IsCanAddWorksSupport = itemMemberRole.IsCanAddWorksSupport,
                            IsCanEditWorksSupportGroup = itemMemberRole.IsCanEditWorksSupportGroup,
                            IsCanEditWorksSupport = itemMemberRole.IsCanEditWorksSupport,
                            IsCanDeleteWorksSupportGroup = itemMemberRole.IsCanDeleteWorksSupportGroup,
                            IsCanDeleteWorksSupport = itemMemberRole.IsCanDeleteWorksSupport,
                            IsDefaultRole = itemMemberRole.IsDefaultRole
                        };

                        new DaWorksSupportTypeMemberRole().InsertMemberRole(newItemMemberRole, toWorksTypeId, objIData);
                    }
                }

                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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
        ///  Writen by  : Nguyen van Huan
        /// Create by  : 10.08.2017
        /// Description: kiem tra loai cong viec da duoc su dung chua
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResultMessage CheckWorkTypeIsUsed(ref WorksSupportType checkWorkType, int workSupportTypeId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpCheckWorkTypeIsUsed);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", workSupportTypeId);
                checkWorkType.IsUsed = Convert.ToInt32(objIData.ExecStoreToString());
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin kiểm tra danh sách WorksSupportType", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType -> CheckWorkTypeIsUsed", "");
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
        /// <param name="objWorksSupportType"></param>
        /// <param name="user"></param>
        protected virtual int Insert(IData objIData, WorksSupportType objWorksSupportType, string user)
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
                objIData.AddParameter("@DELETEALLFUNCTIONID", objWorksSupportType.DeleteAllFunctionId);
                objIData.AddParameter("@PROCESSFUNCTIONID", objWorksSupportType.ProcessFunctionId);
                objIData.AddParameter("@COMMENTFUNCTIONID", objWorksSupportType.CommentFunctionId);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportType.Description);
                objIData.AddParameter("@ISACTIVE", objWorksSupportType.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportType.IsSystem);
                objIData.AddParameter("@CREATEDUSER", user);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID", ObjLogObject.LoginLogID);
                return Convert.ToInt32(objIData.ExecStoreToString());
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
        protected virtual void UpdateData(IData objIData, WorksSupportType objWorksSupportType, string user)
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
                objIData.AddParameter("@UpdatedUser", user);
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
        protected virtual void Update(IData objIData, WorksSupportType objWorksSupportType, string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);

                objIData.AddParameter("@WORKSSUPPORTTYPEID", objWorksSupportType.WorksSupportTypeId);
                objIData.AddParameter("@WORKSSUPPORTTYPENAME", objWorksSupportType.WorksSupportTypeName);
                objIData.AddParameter("@ICONURL", objWorksSupportType.IconUrl);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportType.Description);
                objIData.AddParameter("@ORDERINDEX", objWorksSupportType.OrderIndex);
                objIData.AddParameter("@ISACTIVE", objWorksSupportType.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportType.IsSystem);
                objIData.AddParameter("@UPDATEDUSER", user.Trim());
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
                objIData.AddParameter("@DELETEDUSER", user);
                objIData.AddParameter("@CERTIFICATESTRING", ObjLogObject.CertificateString);
                objIData.AddParameter("@USERHOSTADDRESS", ObjLogObject.UserHostAddress);
                objIData.AddParameter("@LOGINLOGID ", ObjLogObject.LoginLogID);
                objIData.ExecNonQuery();
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
        /// <param name="deletedUser"></param>
        /// <param name="id"></param>
        protected void DeleteWorksType(IData objIData, string deletedUser, int id)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", id);
                objIData.AddParameter("@DELETEDUSER", deletedUser);
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
        /// Insert data
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="data"></param>
        /// /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage InsertWorksSupportType(WorksSupportType objWorksSupportType, ref WorksSupportType data, string user)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                if (objWorksSupportType.WorksSupportTypeId > 0)
                {
                    Update(objIData, objWorksSupportType, user);
                }
                else
                {
                    Insert(objIData, objWorksSupportType, user);
                }

                objIData.CommitTransaction();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
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
        #endregion

        #region Stored Procedure Names

        public const string SpSelectAll = "WORKSSUPPORTTYPE_V2_GETALL";
        public const string SpSelectAllActived = "WORKSSUPPORTTYPE_V2_ACTIVED";
        public const string SpSelect = "WORKSSUPPORTTYPE_V2_SEL";
        public const string SpAdd = "WORKSSUPPORTTYPE_V2_ADD";
        public const string SpUpdate = "WORKSSUPPORTTYPE_V2_UPD";
        public const string SpDelete = "WORKSSUPPORTTYPE_V2_DEL";
        public const string SpSearch = "WORKSSUPPORTTYPE_V2_SRH_PAGE";
        public const string SpSearchNameExisted = "WORKSSUPPORTTYPE_CHECKNAME";
        public const string SpCheckWorkTypeIsUsed = "WORKSSUPPORTTYPE_V2_CHK_USED";

        #endregion
        #endregion

        #region kiểm tra tên đã tồn tại
        /// <summary>
        /// check values existed
        /// </summary>
        /// <param name="objValidate"></param>
        /// <param name="intId"></param>
        /// <param name="strName"></param>
        /// <param name="inOrderIndex"></param>
        /// <returns></returns>
        public ResultMessage CheckValuesExisted(ref WorksSupportTypeValidation objValidate, int intId, string strName, int inOrderIndex)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpCheck);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intId);
                objIData.AddParameter("@WORKSSUPPORTTYPENAME", strName);
                objIData.AddParameter("@ORDERINDEX", inOrderIndex);
                IDataReader reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objValidate = new WorksSupportTypeValidation();
                    if (!Convert.IsDBNull(reader["PORTTYPENAME"])) objValidate.CountName = Convert.ToInt32(reader["PORTTYPENAME"]);
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objValidate.CountOrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                }
                else
                {
                    objValidate = null;
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi kiểm tra trùng tên của loại công việc", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType -> CheckValuesExisted", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        #endregion

        #region Phân quyền loại công việc cần hỗ trợ
        #region Public Methods

        /// <summary>
        /// Get WorksSupportType by Id
        /// </summary>
        /// <param name="objWorksSupportType"></param>
        /// <param name="intWorkSupportTypeId"></param>
        /// <returns></returns>
        public ResultMessage GetWorksSupportPermisById(ref WorksSupportType objWorksSupportType, int intWorkSupportTypeId)
        {
            ResultMessage objResultMessage;
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.BeginTransaction();
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intWorkSupportTypeId);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objWorksSupportType = new WorksSupportType();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"])) objWorksSupportType.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPENAME"])) objWorksSupportType.WorksSupportTypeName = Convert.ToString(reader["WORKSSUPPORTTYPENAME"]).Trim();
                    if (!Convert.IsDBNull(reader["ICONURL"])) objWorksSupportType.IconUrl = Convert.ToString(reader["ICONURL"]).Trim();
                    if (!Convert.IsDBNull(reader["ADDFUNCTIONID"])) objWorksSupportType.AddFunctionId = Convert.ToString(reader["ADDFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["VIEWALLFUNCTIONID"])) objWorksSupportType.ViewAllFunctionId = Convert.ToString(reader["VIEWALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITFUNCTIONID"])) objWorksSupportType.EditFunctionId = Convert.ToString(reader["EDITFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["EDITALLFUNCTIONID"])) objWorksSupportType.EditAllFunctionId = Convert.ToString(reader["EDITALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEFUNCTIONID"])) objWorksSupportType.DeleteFunctionId = Convert.ToString(reader["DELETEFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEALLFUNCTIONID"])) objWorksSupportType.DeleteAllFunctionId = Convert.ToString(reader["DELETEALLFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"])) objWorksSupportType.ProcessFunctionId = Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["COMMENTFUNCTIONID"])) objWorksSupportType.CommentFunctionId = Convert.ToString(reader["COMMENTFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["DESCRIPTION"])) objWorksSupportType.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"])) objWorksSupportType.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"])) objWorksSupportType.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"])) objWorksSupportType.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                }
                ////Nạp thông tin quyền theo dự án
                var daProjectPermis = new DaWorksSupportProjectPermis();
                var lstWorksSupportProjectPermis = new List<WorksSupportProjectPermis>();
                daProjectPermis.GetById(ref lstWorksSupportProjectPermis, intWorkSupportTypeId, objIData);
                if (lstWorksSupportProjectPermis != null && lstWorksSupportProjectPermis.Count > 0)
                {
                    objWorksSupportType.ListWorksSupportProjectPermis = lstWorksSupportProjectPermis;
                }
                ////Nạp thông tin quyền theo vai trò
                var daMemberRole = new DaWorksSupportTypeMemberRole();
                var lstWorksSupportTypeMemberRole = new List<WorksSupportTypeMemberRole>();
                daMemberRole.GetById(ref lstWorksSupportTypeMemberRole, intWorkSupportTypeId, objIData);
                if (lstWorksSupportTypeMemberRole != null && lstWorksSupportTypeMemberRole.Count > 0)
                {
                    objWorksSupportType.ListWorksSupportTypeMemberRole = lstWorksSupportTypeMemberRole;
                }
                ////Nạp thông tin bước xử lý
                var daWorkFlow = new DaWorkSupportTypeWorkFlow();
                var lstWorksSupportTypeWorkFlow = new List<WorksSupportTypeWorkFlow>();
                objResultMessage = daWorkFlow.GetById(ref lstWorksSupportTypeWorkFlow, intWorkSupportTypeId, objIData);
                if (lstWorksSupportTypeWorkFlow != null && lstWorksSupportTypeWorkFlow.Count > 0)
                {
                    objWorksSupportType.ListWorksSupportTypeWorkFlow = lstWorksSupportTypeWorkFlow;
                }
                objIData.CommitTransaction();
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
       
        #endregion

        #region Protected Methods

        #endregion

        #region Stored Procedure Names
        public const string SpSelectPermis = "EO_WORKSSUPPORTTYPE_PER_SEL";
        public const string SpCheck = "WORKSSUPPORTTYPE_V2_CHECK";
        
        #endregion

        #endregion
    }
}