﻿using System;
using System.Collections.Generic;
using System.Linq;
using Library.DataAccess;
using Library.WebCore;
using MyUtils;
using Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorkSupportType
{
    /// <summary>
    ///     Luong trung nhan
    ///     created: 02/06/2017
    ///     bảng các thuộc tính bước xử lý
    /// </summary>
    public class DaWorkSupportTypeWorkFlow
    {
        #region Log Property

        public LogObject ObjLogObject = new LogObject();

        #endregion

        #region Public Methods

        /// <summary>
        ///     Tìm kiếm thông tin trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="list"></param>
        /// <param name="objKeywords"></param>
        /// <returns></returns>
        public ResultMessage SearchData(ref List<WorksSupportTypeWorkFlow> list, params object[] objKeywords)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSearch);
                objIData.AddParameter(objKeywords);
                var dtbData = objIData.ExecStoreToDataTable();
                list = DataTableExtensions.ToList<WorksSupportTypeWorkFlow>(dtbData);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.SearchData,
                    "Lỗi tìm kiếm thông tin trạng thái công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DA_WorksSupportType_WorkFlow -> SearchData", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        ///     GetAlll WorksSupportType_WorkFlow
        /// </summary>
        /// <param name="list"></param>
        /// <param name="intWorkSupportTypeId"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorksSupportTypeWorkFlow> list, int intWorkSupportTypeId)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intWorkSupportTypeId);

                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportTypeWorkFlow = new WorksSupportTypeWorkFlow();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPNAME"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepName =
                            Convert.ToString(reader["WORKSSUPPORTSTEPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepId = Convert.ToInt32(reader["WORKSSUPPORTSTEPID"]);
                    if (!Convert.IsDBNull(reader["UPDATEDUSER"]))
                    if (!Convert.IsDBNull(reader["UPDATEDDATE"]))
                    if (!Convert.IsDBNull(reader["STEPCOLORCODE"]))
                        objWorksSupportTypeWorkFlow.StepColorCode = Convert.ToString(reader["STEPCOLORCODE"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"]))
                        objWorksSupportTypeWorkFlow.ProcessFunctionId =
                            Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"]))
                        objWorksSupportTypeWorkFlow.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["MAXPROCESSTIME"]))
                        objWorksSupportTypeWorkFlow.MaxProcessTime = Convert.ToInt32(reader["MAXPROCESSTIME"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"]))
                        objWorksSupportTypeWorkFlow.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    if (!Convert.IsDBNull(reader["ISMUSTCHOOSEPROCESSUSER"]))
                        objWorksSupportTypeWorkFlow.IsMustChooseProcessUser =
                            Convert.ToInt32(reader["ISMUSTCHOOSEPROCESSUSER"]);
                    if (!Convert.IsDBNull(reader["ISINITSTEP"]))
                        objWorksSupportTypeWorkFlow.IsInitStep = Convert.ToInt32(reader["ISINITSTEP"]);
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"]))
                        objWorksSupportTypeWorkFlow.IsFinishStep = Convert.ToInt32(reader["ISFINISHSTEP"]);
                    if (!Convert.IsDBNull(reader["ISDELETED"]))
                    if (!Convert.IsDBNull(reader["ISCANUPDATEQUALITY"]))
                        objWorksSupportTypeWorkFlow.IsCanUpDateQuality = Convert.ToInt32(reader["ISCANUPDATEQUALITY"]);
                    if (!Convert.IsDBNull(reader["ISCANUPDATEPROCESS"]))
                        objWorksSupportTypeWorkFlow.IsCanUpdateProcess = Convert.ToInt32(reader["ISCANUPDATEPROCESS"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENTSOLUTION"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContentSolution =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENTSOLUTION"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENT"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContent =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENT"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCOMPLETEDDATE"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedCompletedDate =
                            Convert.ToInt32(reader["ISCANMODIFIEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["ISCANCOMMENT"]))
                        objWorksSupportTypeWorkFlow.IsCanComment = Convert.ToInt32(reader["ISCANCOMMENT"]);
                    if (!Convert.IsDBNull(reader["ISCANATTACH"]))
                        objWorksSupportTypeWorkFlow.IsCanAttach = Convert.ToInt32(reader["ISCANATTACH"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"]))
                        objWorksSupportTypeWorkFlow.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["DESCRIPTION"]))
                        objWorksSupportTypeWorkFlow.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["DELETEDUSER"]))
                    if (!Convert.IsDBNull(reader["DELETEDDATE"]))
                    if (!Convert.IsDBNull(reader["CREATEDUSER"]))
                    if (!Convert.IsDBNull(reader["CREATEDDATE"]))
                    if (!Convert.IsDBNull(reader["AUTOCHANGETOSTATUSID"]))
                        objWorksSupportTypeWorkFlow.AutoChangeToStatusId =
                            Convert.ToInt32(reader["AUTOCHANGETOSTATUSID"]);

                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStatusId =
                            Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);

                    var daWfNx = new DaWorksSupportTypeWfNx();
                    var lstWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();
                    objResultMessage = daWfNx.GetById(ref lstWorksSupportTypeWfNx,
                        objWorksSupportTypeWorkFlow.WorksSupportStepId, intWorkSupportTypeId, objIData);
                    if (lstWorksSupportTypeWfNx != null && lstWorksSupportTypeWfNx.Count > 0)
                        objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfNx = lstWorksSupportTypeWfNx;

                    var daWfnxPermis = new DaWorksSupportTypeWfnxPermis();
                    var lstWorksSupportTypeWfnxPermis = new List<WorksSupportTypeWfnxPermis>();
                    daWfnxPermis.GetById(ref lstWorksSupportTypeWfnxPermis, objWorksSupportTypeWorkFlow.WorksSupportStepId, intWorkSupportTypeId, objIData);
                    
                    list.Add(objWorksSupportTypeWorkFlow);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType_WorkFlow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        ///     Get WorksSupportType_WorkFlow by Id
        /// </summary>
        /// <param name="listWorksSupportTypeWorkFlow"></param>
        /// <param name="intWorkSupportTypeId"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref List<WorksSupportTypeWorkFlow> listWorksSupportTypeWorkFlow,
            int intWorkSupportTypeId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            //var objIData = Data.CreateData();
            try
            {
                //objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intWorkSupportTypeId);

                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportTypeWorkFlow = new WorksSupportTypeWorkFlow();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPNAME"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepName =
                            Convert.ToString(reader["WORKSSUPPORTSTEPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepId = Convert.ToInt32(reader["WORKSSUPPORTSTEPID"]);
                   
                    if (!Convert.IsDBNull(reader["STEPCOLORCODE"]))
                        objWorksSupportTypeWorkFlow.StepColorCode = Convert.ToString(reader["STEPCOLORCODE"]).Trim();

                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"]))
                        objWorksSupportTypeWorkFlow.ProcessFunctionId =
                            Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"]))
                        objWorksSupportTypeWorkFlow.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["MAXPROCESSTIME"]))
                        objWorksSupportTypeWorkFlow.MaxProcessTime = Convert.ToInt32(reader["MAXPROCESSTIME"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"]))
                        objWorksSupportTypeWorkFlow.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    if (!Convert.IsDBNull(reader["ISMUSTCHOOSEPROCESSUSER"]))
                        objWorksSupportTypeWorkFlow.IsMustChooseProcessUser =
                            Convert.ToInt32(reader["ISMUSTCHOOSEPROCESSUSER"]);
                    if (!Convert.IsDBNull(reader["ISINITSTEP"]))
                        objWorksSupportTypeWorkFlow.IsInitStep = Convert.ToInt32(reader["ISINITSTEP"]);
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"]))
                        objWorksSupportTypeWorkFlow.IsFinishStep = Convert.ToInt32(reader["ISFINISHSTEP"]);
                    if (!Convert.IsDBNull(reader["ISCANUPDATEQUALITY"]))
                        objWorksSupportTypeWorkFlow.IsCanUpDateQuality = Convert.ToInt32(reader["ISCANUPDATEQUALITY"]);
                    if (!Convert.IsDBNull(reader["ISCANUPDATEPROCESS"]))
                        objWorksSupportTypeWorkFlow.IsCanUpdateProcess = Convert.ToInt32(reader["ISCANUPDATEPROCESS"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENTSOLUTION"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContentSolution =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENTSOLUTION"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENT"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContent =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENT"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCOMPLETEDDATE"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedCompletedDate =
                            Convert.ToInt32(reader["ISCANMODIFIEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["ISCANCOMMENT"]))
                        objWorksSupportTypeWorkFlow.IsCanComment = Convert.ToInt32(reader["ISCANCOMMENT"]);
                    if (!Convert.IsDBNull(reader["ISCANATTACH"]))
                        objWorksSupportTypeWorkFlow.IsCanAttach = Convert.ToInt32(reader["ISCANATTACH"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"]))
                        objWorksSupportTypeWorkFlow.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["DESCRIPTION"]))
                        objWorksSupportTypeWorkFlow.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["AUTOCHANGETOSTATUSID"]))
                        objWorksSupportTypeWorkFlow.AutoChangeToStatusId =
                            Convert.ToInt32(reader["AUTOCHANGETOSTATUSID"]);

                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStatusId =
                            Convert.ToInt32(reader["WORKSSUPPORTSTATUSID"]);

                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPPROGRESS"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepProgress =
                            Convert.ToInt32(reader["WORKSSUPPORTSTEPPROGRESS"]);

                    var daWfNx = new DaWorksSupportTypeWfNx();

                    var lstWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();
                    daWfNx.GetById(ref lstWorksSupportTypeWfNx,
                        objWorksSupportTypeWorkFlow.WorksSupportStepId, intWorkSupportTypeId, objIData);

                    if (lstWorksSupportTypeWfNx != null && lstWorksSupportTypeWfNx.Count > 0)
                        objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfNx = lstWorksSupportTypeWfNx;

                    var daWfPermis = new DaWorksSupportTypeWfPermis();
                    var lstWorksSupportTypeWfPermis = new List<WorksSupportTypeWfPermis>();

                    objResultMessage = daWfPermis.GetById(ref lstWorksSupportTypeWfPermis,
                        objWorksSupportTypeWorkFlow.WorksSupportStepId, objIData);

                    if (lstWorksSupportTypeWfPermis != null && lstWorksSupportTypeWfPermis.Count > 0)
                        objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfPermis = lstWorksSupportTypeWfPermis;

                    var daWfnxPermis = new DaWorksSupportTypeWfnxPermis();
                    var lstWorksSupportTypeWfnxPermis = new List<WorksSupportTypeWfnxPermis>();
                    daWfnxPermis.GetById(ref lstWorksSupportTypeWfnxPermis, objWorksSupportTypeWorkFlow.WorksSupportStepId, intWorkSupportTypeId, objIData);
                    objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfNxPermis = lstWorksSupportTypeWfnxPermis;

                    listWorksSupportTypeWorkFlow.Add(objWorksSupportTypeWorkFlow);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType_WorkFlow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> GetById", "");
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Get WorksSupportType_WorkFlow by workssupportID + workssupporttypeID
        /// </summary>
        /// <param name="listWorksSupportTypeWorkFlow"></param>
        /// <param name="intWorkSupportTypeId"></param>
        /// <param name="intWorkSupportId"></param>
        /// <param name="objIData"></param>
        /// <returns></returns>
        public ResultMessage GetById(ref List<WorksSupportTypeWorkFlow> listWorksSupportTypeWorkFlow,
            int intWorkSupportTypeId, int intWorkSupportId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            //var objIData = Data.CreateData();
            try
            {
                //objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", intWorkSupportTypeId);
                objIData.AddParameter("@WORKSSUPPORTID", intWorkSupportId);

                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportTypeWorkFlow = new WorksSupportTypeWorkFlow();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPNAME"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepName =
                            Convert.ToString(reader["WORKSSUPPORTSTEPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepId = Convert.ToInt32(reader["WORKSSUPPORTSTEPID"]);
                    if (!Convert.IsDBNull(reader["STEPCOLORCODE"]))
                        objWorksSupportTypeWorkFlow.StepColorCode = Convert.ToString(reader["STEPCOLORCODE"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"]))
                        objWorksSupportTypeWorkFlow.ProcessFunctionId =
                            Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"]))
                        objWorksSupportTypeWorkFlow.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["MAXPROCESSTIME"]))
                        objWorksSupportTypeWorkFlow.MaxProcessTime = Convert.ToInt32(reader["MAXPROCESSTIME"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"]))
                        objWorksSupportTypeWorkFlow.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    if (!Convert.IsDBNull(reader["ISMUSTCHOOSEPROCESSUSER"]))
                        objWorksSupportTypeWorkFlow.IsMustChooseProcessUser =
                            Convert.ToInt32(reader["ISMUSTCHOOSEPROCESSUSER"]);
                    if (!Convert.IsDBNull(reader["ISINITSTEP"]))
                        objWorksSupportTypeWorkFlow.IsInitStep = Convert.ToInt32(reader["ISINITSTEP"]);
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"]))
                        objWorksSupportTypeWorkFlow.IsFinishStep = Convert.ToInt32(reader["ISFINISHSTEP"]);
                    if (!Convert.IsDBNull(reader["ISDELETED"]))
                    if (!Convert.IsDBNull(reader["ISCANUPDATEQUALITY"]))
                        objWorksSupportTypeWorkFlow.IsCanUpDateQuality = Convert.ToInt32(reader["ISCANUPDATEQUALITY"]);
                    if (!Convert.IsDBNull(reader["ISCANUPDATEPROCESS"]))
                        objWorksSupportTypeWorkFlow.IsCanUpdateProcess = Convert.ToInt32(reader["ISCANUPDATEPROCESS"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENTSOLUTION"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContentSolution =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENTSOLUTION"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENT"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContent =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENT"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCOMPLETEDDATE"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedCompletedDate =
                            Convert.ToInt32(reader["ISCANMODIFIEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["ISCANCOMMENT"]))
                        objWorksSupportTypeWorkFlow.IsCanComment = Convert.ToInt32(reader["ISCANCOMMENT"]);
                    if (!Convert.IsDBNull(reader["ISCANATTACH"]))
                        objWorksSupportTypeWorkFlow.IsCanAttach = Convert.ToInt32(reader["ISCANATTACH"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"]))
                        objWorksSupportTypeWorkFlow.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["DESCRIPTION"]))
                        objWorksSupportTypeWorkFlow.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["AUTOCHANGETOSTATUSID"]))
                        objWorksSupportTypeWorkFlow.AutoChangeToStatusId =
                            Convert.ToInt32(reader["AUTOCHANGETOSTATUSID"]);
                    // Nạp thông tin Bảng chứa các bước xử lý kế tiếp
                    var daWfNx = new DaWorksSupportTypeWfNx();
                    var lstWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();
                    daWfNx.GetById(ref lstWorksSupportTypeWfNx, objWorksSupportTypeWorkFlow.WorksSupportStepId,
                        intWorkSupportTypeId, objIData);
                    if (lstWorksSupportTypeWfNx != null && lstWorksSupportTypeWfNx.Count > 0)
                        objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfNx = lstWorksSupportTypeWfNx;
                    // Nạp thông tin Bảng chứa các bước xử lý kế tiếp
                    var daWfPermis = new DaWorksSupportTypeWfPermis();
                    var lstWorksSupportTypeWfPermis = new List<WorksSupportTypeWfPermis>();
                    objResultMessage = daWfPermis.GetById(ref lstWorksSupportTypeWfPermis,
                        objWorksSupportTypeWorkFlow.WorksSupportStepId, objIData);
                    if (lstWorksSupportTypeWfPermis != null && lstWorksSupportTypeWfPermis.Count > 0)
                        objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfPermis = lstWorksSupportTypeWfPermis;
                    listWorksSupportTypeWorkFlow.Add(objWorksSupportTypeWorkFlow);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType_WorkFlow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> GetById", "");
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        ///     Insert data
        /// </summary>
        /// <param name="listWorksSupportTypeWorkFlow"></param>
        /// <param name="objIData"></param>
        /// <param name="worksSupportTypeId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Insert(List<WorksSupportTypeWorkFlow> listWorksSupportTypeWorkFlow, IData objIData,
            int worksSupportTypeId, string user)
        {
            var objResultMessage = new ResultMessage();
            var listNew = new List<WorksSupportTypeWfNx>();
            try
            {
                if (listWorksSupportTypeWorkFlow != null && listWorksSupportTypeWorkFlow.Count > 0)
                {
                    listNew.AddRange(from itemFlow in listWorksSupportTypeWorkFlow
                                     let nextInsertId = Insert(objIData, itemFlow, worksSupportTypeId, user)
                        select new WorksSupportTypeWfNx
                        {
                            WorksSupportTypeId = worksSupportTypeId,
                            WorksSupportStepId = nextInsertId,
                            NextWorksSupportStepsId = itemFlow.WorksSupportStepId
                        });
                    foreach (var item in listWorksSupportTypeWorkFlow)
                    {
                        var obj = listNew.Find(o => o.NextWorksSupportStepsId == item.WorksSupportStepId);
                        if (item.ListWorksSupportTypeWfNx == null || item.ListWorksSupportTypeWfNx.Count <= 0) continue;
                        {
                            // insert WorksSupportType_WF_NX
                            var daWfNx = new DaWorksSupportTypeWfNx();
                            var listWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();

                            foreach (var nextNx in item.ListWorksSupportTypeWfNx)
                            {
                                var newObj = new WorksSupportTypeWfNx
                                {
                                    WorksSupportTypeId = obj.WorksSupportTypeId,
                                    WorksSupportStepId = obj.WorksSupportStepId
                                };
                                var nextObj = listNew.Find(o => o.NextWorksSupportStepsId == nextNx.WorksSupportStepId);
                                newObj.NextWorksSupportStepsId = nextObj.WorksSupportStepId;
                                listWorksSupportTypeWfNx.Add(newObj);
                            }
                            daWfNx.Insert(listWorksSupportTypeWfNx, objIData, worksSupportTypeId);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert,
                    "Lỗi thêm thông tin WorksSupportType_WorkFlow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Copy WorksType WorkFlow
        /// </summary>
        /// <param name="listWorksSupportTypeWorkFlow"></param>
        /// <param name="objIData"></param>
        /// <param name="worksSupportTypeId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage CopyWorkFlow(List<WorksSupportTypeWorkFlow> listWorksSupportTypeWorkFlow, IData objIData,
                                    int worksSupportTypeId, string user)
        {
            var objResultMessage = new ResultMessage();
            var listNew = new List<WorksSupportTypeWfNx>();
            try
            {
                if (listWorksSupportTypeWorkFlow.Count > 0)
                {
                    listNew.AddRange(
                                     from itemFlow in listWorksSupportTypeWorkFlow
                                     let nextInsertId = this.Insert(objIData, itemFlow, worksSupportTypeId, user)
                                     select new WorksSupportTypeWfNx
                                                {
                                                    WorksSupportTypeId = worksSupportTypeId,
                                                    WorksSupportStepId = nextInsertId,
                                                    NextWorksSupportStepsId = itemFlow.WorksSupportStepId
                                                });
                    foreach (var item in listWorksSupportTypeWorkFlow)
                    {
                        var obj = listNew.Find(o => o.NextWorksSupportStepsId == item.WorksSupportStepId);
                        if (item.ListWorksSupportTypeWfNx == null || item.ListWorksSupportTypeWfNx.Count <= 0) continue;
                        {
                            // insert WorksSupportType_WF_NX
                            var daWfNx = new DaWorksSupportTypeWfNx();
                            var listWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();

                            foreach (var nextNx in item.ListWorksSupportTypeWfNx)
                            {
                                var newObj = new WorksSupportTypeWfNx
                                                 {
                                                     WorksSupportTypeId = obj.WorksSupportTypeId,
                                                     WorksSupportStepId = obj.WorksSupportStepId
                                                 };
                                var nextObj = listNew.Find(o => o.NextWorksSupportStepsId == nextNx.NextWorksSupportStepsId);
                                newObj.NextWorksSupportStepsId = nextObj.WorksSupportStepId;
                                listWorksSupportTypeWfNx.Add(newObj);
                            }
                            daWfNx.Insert(listWorksSupportTypeWfNx, objIData, worksSupportTypeId);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert,
                    "Lỗi thêm thông tin WorksSupportType_WorkFlow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Cập nhật thông tin trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="listWorksSupportTypeWorkFlow"></param>
        /// <param name="objIData"></param>
        /// <param name="user"></param>
        /// <param name="listWorksFlowDeletedId"></param>
        /// <returns></returns>
        public ResultMessage Update(List<WorksSupportTypeWorkFlow> listWorksSupportTypeWorkFlow, IData objIData, string user, string listWorksFlowDeletedId)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                if (!string.IsNullOrWhiteSpace(listWorksFlowDeletedId))
                {
                   var list = listWorksFlowDeletedId.Split(',');
                   foreach (var item in list)
                   {
                       new DaWorkSupportTypeWorkFlow().DeleteById(int.Parse(item), objIData);
                   }
                }
                // Delete worksflow
                if (listWorksSupportTypeWorkFlow.Count > 0)
                {
                    new DaWorksSupportTypeWfNx().Delete(listWorksSupportTypeWorkFlow[0].WorksSupportTypeId, objIData);
                }

                var listNew = (from itemFlow in listWorksSupportTypeWorkFlow
                    let worksSupportTypeId = itemFlow.WorksSupportTypeId
                    where itemFlow.WorksSupportStepId < 0
                    let nextInsertId = Insert(objIData, itemFlow, worksSupportTypeId, user)
                    select new WorksSupportTypeWfNx
                    {
                        WorksSupportTypeId = worksSupportTypeId,
                        WorksSupportStepId = nextInsertId,
                        NextWorksSupportStepsId = itemFlow.WorksSupportStepId
                    }).ToList();


                if (listWorksSupportTypeWorkFlow.Count > 0)
                    foreach (var itemFlow in listWorksSupportTypeWorkFlow)
                    {
                        var nextId = 0;
                        if (itemFlow.WorksSupportStepId < 0)
                        {
                            foreach (var newItemNx in listNew)
                            {
                                if (newItemNx.NextWorksSupportStepsId != itemFlow.WorksSupportStepId)
                                    continue;
                                nextId = newItemNx.WorksSupportStepId;
                                itemFlow.WorksSupportStepId = nextId;
                                break;
                            }
                        }
                        
                        Update(objIData, itemFlow, user);
                        // Insert
                        if (itemFlow.ListWorksSupportTypeWfNx == null ||
                            itemFlow.ListWorksSupportTypeWfNx.Count <= 0) continue;
                        var list = new List<WorksSupportTypeWfNx>();
                        foreach (var itemNx in itemFlow.ListWorksSupportTypeWfNx)
                        {
                            
                            if (itemNx.WorksSupportStepId >= 0)
                                nextId = itemNx.WorksSupportStepId;
                            else
                                foreach (var newItemNx in listNew)
                                {
                                    if (newItemNx.NextWorksSupportStepsId != itemNx.WorksSupportStepId)
                                        continue;
                                    nextId = newItemNx.WorksSupportStepId;
                                    break;
                                }
                            var obj = new WorksSupportTypeWfNx
                            {
                                WorksSupportTypeId = itemFlow.WorksSupportTypeId,
                                NextWorksSupportStepsId = nextId,
                                WorksSupportStepId = itemFlow.WorksSupportStepId
                            };
                            list.Add(obj);
                        }
                        new DaWorksSupportTypeWfNx().Insert(list, objIData, itemFlow.WorksSupportTypeId);
                    }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Update,
                    "Lỗi cập nhật thông tin WorksSupportType_WorkFlow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> Update", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        ///     Xóa trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objWorksSupportTypeWorkFlow"></param>
        /// <param name="objIData"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Delete(WorksSupportTypeWorkFlow objWorksSupportTypeWorkFlow, IData objIData, string user)
        {
            ResultMessage objResultMessage;
            try
            {
                Delete(objIData, objWorksSupportTypeWorkFlow.WorksSupportTypeId);
                var objWfNx = new WorksSupportTypeWfNx
                {
                    WorksSupportTypeId = objWorksSupportTypeWorkFlow.WorksSupportTypeId
                };
                new DaWorksSupportTypeWfNx().Delete(objWfNx.WorksSupportTypeId, objIData);

                var daWorkFlow = new DaWorkSupportTypeWorkFlow();
                var lstWorksSupportTypeWorkFlow = new List<WorksSupportTypeWorkFlow>();
                objResultMessage = daWorkFlow.GetById(ref lstWorksSupportTypeWorkFlow,
                    objWorksSupportTypeWorkFlow.WorksSupportTypeId, objIData);
                if (lstWorksSupportTypeWorkFlow != null && lstWorksSupportTypeWorkFlow.Count > 0)
                    foreach (var objWorkFlow in lstWorksSupportTypeWorkFlow)
                    {
                        var daWfPermis = new DaWorksSupportTypeWfPermis();
                        var lstWfPermis = objWorkFlow.ListWorksSupportTypeWfPermis;
                        daWfPermis.Delete(lstWfPermis, objIData);
                    }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSuportStatus",
                    objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> Delete", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Delete data by WorksSupportStepId
        /// </summary>
        /// <param name="worksSupportStepId"></param>
        /// <param name="objIData"></param>
        public void DeleteById(int worksSupportStepId, IData objIData)
        {
            try
            {
                DeleteById(objIData, worksSupportStepId);
            }
            catch (Exception objEx)
            {
                var objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa WorksSuportStatus",
                    objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> Delete", Globals.ModuleName);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Thêm trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportTypeWorkFlow"></param>
        /// <param name="worksSupportTypeId"></param>
        /// <param name="user"></param>
        protected virtual int Insert(IData objIData, WorksSupportTypeWorkFlow objWorksSupportTypeWorkFlow,
            int worksSupportTypeId, string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAdd);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", worksSupportTypeId);
                objIData.AddParameter("@WORKSSUPPORTSTEPNAME", objWorksSupportTypeWorkFlow.WorksSupportStepName);
                objIData.AddParameter("@PROCESSFUNCTIONID", objWorksSupportTypeWorkFlow.ProcessFunctionId);
                objIData.AddParameter("@STEPCOLORCODE", objWorksSupportTypeWorkFlow.StepColorCode);
                objIData.AddParameter("@MAXPROCESSTIME", objWorksSupportTypeWorkFlow.MaxProcessTime);
                objIData.AddParameter("@ISINITSTEP", objWorksSupportTypeWorkFlow.IsInitStep);
                objIData.AddParameter("@ISFINISHSTEP", objWorksSupportTypeWorkFlow.IsFinishStep);
                objIData.AddParameter("@ISMUSTCHOOSEPROCESSUSER", objWorksSupportTypeWorkFlow.IsMustChooseProcessUser);
                objIData.AddParameter("@AUTOCHANGETOSTATUSID", objWorksSupportTypeWorkFlow.AutoChangeToStatusId);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportTypeWorkFlow.Description);
                objIData.AddParameter("@ORDERINDEX", objWorksSupportTypeWorkFlow.OrderIndex);
                objIData.AddParameter("@ISACTIVE", objWorksSupportTypeWorkFlow.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportTypeWorkFlow.IsSystem);
                objIData.AddParameter("@CREATEDUSER", user);
                objIData.AddParameter("@ISCANMODIFIEDCONTENT", objWorksSupportTypeWorkFlow.IsCanModifiedContent);
                objIData.AddParameter("@ISCANMODIFIEDCONTENTSOLUTION",
                    objWorksSupportTypeWorkFlow.IsCanModifiedContentSolution);
                objIData.AddParameter("@ISCANMODIFIEDCOMPLETEDDATE",
                    objWorksSupportTypeWorkFlow.IsCanModifiedCompletedDate);
                objIData.AddParameter("@ISCANATTACH", objWorksSupportTypeWorkFlow.IsCanAttach);
                objIData.AddParameter("@ISCANCOMMENT", objWorksSupportTypeWorkFlow.IsCanComment);
                objIData.AddParameter("@ISCANUPDATEPROCESS", objWorksSupportTypeWorkFlow.IsCanUpdateProcess);
                objIData.AddParameter("@ISCANUPDATEQUALITY", objWorksSupportTypeWorkFlow.IsCanUpDateQuality);
                objIData.AddParameter("@WORKSSUPPORTSTATUSID", objWorksSupportTypeWorkFlow.WorksSupportStatusId);
                objIData.AddParameter("@WORKSSUPPORTSTEPPROGRESS", objWorksSupportTypeWorkFlow.WorksSupportStepProgress);
                
                return Convert.ToInt32(objIData.ExecStoreToString());
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        ///     Cập nhật trạng thái công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportTypeWorkFlow"></param>
        /// <param name="user"></param>
        protected virtual void Update(IData objIData, WorksSupportTypeWorkFlow objWorksSupportTypeWorkFlow, string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpUpdate);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", objWorksSupportTypeWorkFlow.WorksSupportTypeId);
                objIData.AddParameter("@WORKSSUPPORTSTEPID", objWorksSupportTypeWorkFlow.WorksSupportStepId);
                objIData.AddParameter("@WORKSSUPPORTSTEPNAME", objWorksSupportTypeWorkFlow.WorksSupportStepName);
                objIData.AddParameter("@PROCESSFUNCTIONID", objWorksSupportTypeWorkFlow.ProcessFunctionId);
                objIData.AddParameter("@STEPCOLORCODE", objWorksSupportTypeWorkFlow.StepColorCode);
                objIData.AddParameter("@MAXPROCESSTIME", objWorksSupportTypeWorkFlow.MaxProcessTime);
                objIData.AddParameter("@ISINITSTEP", objWorksSupportTypeWorkFlow.IsInitStep);
                objIData.AddParameter("@ISFINISHSTEP", objWorksSupportTypeWorkFlow.IsFinishStep);
                objIData.AddParameter("@ISMUSTCHOOSEPROCESSUSER", objWorksSupportTypeWorkFlow.IsMustChooseProcessUser);
                objIData.AddParameter("@AUTOCHANGETOSTATUSID", objWorksSupportTypeWorkFlow.AutoChangeToStatusId);
                objIData.AddParameter("@DESCRIPTION", objWorksSupportTypeWorkFlow.Description);
                objIData.AddParameter("@ORDERINDEX", objWorksSupportTypeWorkFlow.OrderIndex);
                objIData.AddParameter("@ISACTIVE", objWorksSupportTypeWorkFlow.IsActived);
                objIData.AddParameter("@ISSYSTEM", objWorksSupportTypeWorkFlow.IsSystem);
                objIData.AddParameter("@UPDATEDUSER", user);
                objIData.AddParameter("@ISCANMODIFIEDCONTENT", objWorksSupportTypeWorkFlow.IsCanModifiedContent);
                objIData.AddParameter("@ISCANMODIFIEDCONTENTSOLUTION",
                    objWorksSupportTypeWorkFlow.IsCanModifiedContentSolution);
                objIData.AddParameter("@ISCANMODIFIEDCOMPLETEDDATE",
                    objWorksSupportTypeWorkFlow.IsCanModifiedCompletedDate);
                objIData.AddParameter("@ISCANATTACH", objWorksSupportTypeWorkFlow.IsCanAttach);
                objIData.AddParameter("@ISCANCOMMENT", objWorksSupportTypeWorkFlow.IsCanComment);
                objIData.AddParameter("@ISCANUPDATEPROCESS", objWorksSupportTypeWorkFlow.IsCanUpdateProcess);
                objIData.AddParameter("@ISCANUPDATEQUALITY", objWorksSupportTypeWorkFlow.IsCanUpDateQuality);
                objIData.AddParameter("@WORKSSUPPORTSTATUSID", objWorksSupportTypeWorkFlow.WorksSupportStatusId);
                objIData.AddParameter("@WORKSSUPPORTSTEPPROGRESS", objWorksSupportTypeWorkFlow.WorksSupportStepProgress);

                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        ///     Xóa trạng thái công việc cần hỗ trợ
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

        /// <summary>
        /// Delete data by Id
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="worksSupportStepId"></param>
        protected void DeleteById(IData objIData, int worksSupportStepId)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDeleteById);
                objIData.AddParameter("@WORKSSUPPORTSTEPID", worksSupportStepId);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }
        

        /// <summary>
        /// Get WorksSupportType_WorkFlow by Id
        /// </summary>
        /// <param name="listWorksSupportTypeWorkFlow"></param>
        /// <param name="workSupportTypeId"></param>
        /// <param name="worksFlowName"></param>
        /// <returns></returns>
        public ResultMessage GetByWoksSupportTypeIdAndName(
            ref List<WorksSupportTypeWorkFlow> listWorksSupportTypeWorkFlow, int workSupportTypeId,
            string worksFlowName)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectByIdAndName);
                objIData.AddParameter("@WORKSSUPPORTTYPEID", workSupportTypeId);
                objIData.AddParameter("@WORKSSUPPORTSTEPNAME", worksFlowName);

                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportTypeWorkFlow = new WorksSupportTypeWorkFlow();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTTYPEID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportTypeId = Convert.ToInt32(reader["WORKSSUPPORTTYPEID"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPNAME"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepName =
                            Convert.ToString(reader["WORKSSUPPORTSTEPNAME"]).Trim();
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPID"]))
                        objWorksSupportTypeWorkFlow.WorksSupportStepId = Convert.ToInt32(reader["WORKSSUPPORTSTEPID"]);
                    if (!Convert.IsDBNull(reader["STEPCOLORCODE"]))
                        objWorksSupportTypeWorkFlow.StepColorCode = Convert.ToString(reader["STEPCOLORCODE"]).Trim();
                    if (!Convert.IsDBNull(reader["PROCESSFUNCTIONID"]))
                        objWorksSupportTypeWorkFlow.ProcessFunctionId =
                            Convert.ToString(reader["PROCESSFUNCTIONID"]).Trim();
                    if (!Convert.IsDBNull(reader["ORDERINDEX"]))
                        objWorksSupportTypeWorkFlow.OrderIndex = Convert.ToInt32(reader["ORDERINDEX"]);
                    if (!Convert.IsDBNull(reader["MAXPROCESSTIME"]))
                        objWorksSupportTypeWorkFlow.MaxProcessTime = Convert.ToInt32(reader["MAXPROCESSTIME"]);
                    if (!Convert.IsDBNull(reader["ISSYSTEM"]))
                        objWorksSupportTypeWorkFlow.IsSystem = Convert.ToBoolean(reader["ISSYSTEM"]);
                    if (!Convert.IsDBNull(reader["ISMUSTCHOOSEPROCESSUSER"]))
                        objWorksSupportTypeWorkFlow.IsMustChooseProcessUser =
                            Convert.ToInt32(reader["ISMUSTCHOOSEPROCESSUSER"]);
                    if (!Convert.IsDBNull(reader["ISINITSTEP"]))
                        objWorksSupportTypeWorkFlow.IsInitStep = Convert.ToInt32(reader["ISINITSTEP"]);
                    if (!Convert.IsDBNull(reader["ISFINISHSTEP"]))
                        objWorksSupportTypeWorkFlow.IsFinishStep = Convert.ToInt32(reader["ISFINISHSTEP"]);
                    if (!Convert.IsDBNull(reader["ISCANUPDATEQUALITY"]))
                        objWorksSupportTypeWorkFlow.IsCanUpDateQuality = Convert.ToInt32(reader["ISCANUPDATEQUALITY"]);
                    if (!Convert.IsDBNull(reader["ISCANUPDATEPROCESS"]))
                        objWorksSupportTypeWorkFlow.IsCanUpdateProcess = Convert.ToInt32(reader["ISCANUPDATEPROCESS"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENTSOLUTION"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContentSolution =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENTSOLUTION"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCONTENT"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedContent =
                            Convert.ToInt32(reader["ISCANMODIFIEDCONTENT"]);
                    if (!Convert.IsDBNull(reader["ISCANMODIFIEDCOMPLETEDDATE"]))
                        objWorksSupportTypeWorkFlow.IsCanModifiedCompletedDate =
                            Convert.ToInt32(reader["ISCANMODIFIEDCOMPLETEDDATE"]);
                    if (!Convert.IsDBNull(reader["ISCANCOMMENT"]))
                        objWorksSupportTypeWorkFlow.IsCanComment = Convert.ToInt32(reader["ISCANCOMMENT"]);
                    if (!Convert.IsDBNull(reader["ISCANATTACH"]))
                        objWorksSupportTypeWorkFlow.IsCanAttach = Convert.ToInt32(reader["ISCANATTACH"]);
                    if (!Convert.IsDBNull(reader["ISACTIVE"]))
                        objWorksSupportTypeWorkFlow.IsActived = Convert.ToBoolean(reader["ISACTIVE"]);
                    if (!Convert.IsDBNull(reader["DESCRIPTION"]))
                        objWorksSupportTypeWorkFlow.Description = Convert.ToString(reader["DESCRIPTION"]).Trim();
                    if (!Convert.IsDBNull(reader["AUTOCHANGETOSTATUSID"]))
                        objWorksSupportTypeWorkFlow.AutoChangeToStatusId =
                            Convert.ToInt32(reader["AUTOCHANGETOSTATUSID"]);
                    // Nạp thông tin Bảng chứa các bước xử lý kế tiếp
                    var daWfNx = new DaWorksSupportTypeWfNx();
                    var lstWorksSupportTypeWfNx = new List<WorksSupportTypeWfNx>();
                    daWfNx.GetById(ref lstWorksSupportTypeWfNx, objWorksSupportTypeWorkFlow.WorksSupportStepId,
                        workSupportTypeId, objIData);
                    if (lstWorksSupportTypeWfNx != null && lstWorksSupportTypeWfNx.Count > 0)
                        objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfNx = lstWorksSupportTypeWfNx;
                    // Nạp thông tin Bảng chứa các bước xử lý kế tiếp
                    var daWfPermis = new DaWorksSupportTypeWfPermis();
                    var lstWorksSupportTypeWfPermis = new List<WorksSupportTypeWfPermis>();
                    objResultMessage = daWfPermis.GetById(ref lstWorksSupportTypeWfPermis,
                        objWorksSupportTypeWorkFlow.WorksSupportStepId, objIData);
                    if (lstWorksSupportTypeWfPermis != null && lstWorksSupportTypeWfPermis.Count > 0)
                        objWorksSupportTypeWorkFlow.ListWorksSupportTypeWfPermis = lstWorksSupportTypeWfPermis;
                    listWorksSupportTypeWorkFlow.Add(objWorksSupportTypeWorkFlow);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportType_WorkFlow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportType_WorkFlow -> GetByWoksSupportTypeIdAndName", "");
                return objResultMessage;
            }
            return objResultMessage;
        }

        #endregion

        #region Stored Procedure Names

        public const string SpSelectAll = "EO_WORKSSUPPORTTYPE_WF_GETALL";
        public const string SpSelect = "EO_WORKSSUPPORTTYPE_WF_SEL";
        public const string SpSelectByIdAndName = "EO_WORKSSUPPORTTYPE_WF_IDNAME";
        public const string SpAdd = "EO_WORKSSUPPORTTYPE_WF_ADD";
        public const string SpUpdate = "EO_WORKSSUPPORTTYPE_WF_UPDATE";
        public const string SpDelete = "EO_WORKSSUPPORTTYPE_WF_DEL";
        public const string SpDeleteById = "EO_WORKSSUPPORTTYPE_WF_DELBYID";
        
        public const string SpSearch = "EO_WORKSSUPPORTTYPE_WF_SRH";

        #endregion
    }
}