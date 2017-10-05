using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorksSupport
{
    public class DaWorksSupportWorkflow
    {
         #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion

        /// <summary>
        /// Nạp thông tin tiến độ công việc cần hỗ trợ
        /// </summary>
        /// <param name="lstWorksSupportWorkflow"></param>
        /// <param name="worksSupportId"></param>
        /// <param name="objIData"></param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage LoadInfo(ref List<WorksSupportWorkflow> lstWorksSupportWorkflow, int worksSupportId, IData objIData)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WorksSupportID", worksSupportId);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objWorksSupportWorkflow = new WorksSupportWorkflow();

                    if (!Convert.IsDBNull(reader["NOTE"])) objWorksSupportWorkflow.Note = Convert.ToString(reader["NOTE"]).Trim();

                    if (!Convert.IsDBNull(reader["PROCESSDATE"])) objWorksSupportWorkflow.ProcessDate = Convert.ToDateTime(reader["PROCESSDATE"]);

                    if (!Convert.IsDBNull(reader["FULLNAME"])) objWorksSupportWorkflow.ProcessUser = Convert.ToString(reader["FULLNAME"]).Trim();

                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWorksSupportWorkflow.WorkSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]);

                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTEPNAME"])) objWorksSupportWorkflow.WorksSupportStepName = Convert.ToString(reader["WORKSSUPPORTSTEPNAME"]);
                    lstWorksSupportWorkflow.Add(objWorksSupportWorkflow);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo, "Lỗi nạp thông tin tiến độ công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Workflow -> LoadInfo", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }
        
        
        /// <summary>
        /// Xóa thông tin tiến độ công việc cần hỗ trợ
        /// </summary>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage Delete(int workSupportId, IData objIData, string user)
        {
            var objResultMessage = new ResultMessage();
            try
            {
                this.Delete(objIData, workSupportId, user);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa thông tin tiến độ công việc cần hỗ trợ", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Workflow -> Delete", Globals.ModuleName);
                return objResultMessage;
            }
            return objResultMessage;
        }

        /// <summary>
        /// Xóa thông tin tiến độ công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData">Đối tượng Kết nối CSDL</param>
        /// <param name="worksSupportId"></param>
        /// <param name="user"></param>
        /// <returns>Kết quả trả về</returns>
        private void Delete(IData objIData, int worksSupportId, string user)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WorksSupportId", worksSupportId);
                objIData.AddParameter("@DeletedUser", user);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Xu ly buoc ke tiep cho cong viec can ho tro
        /// 1. Insert buoc ke tiep vao workssupport_worksflow
        /// 2. Cap nhat buoc hien tai isproccess =1
        /// </summary>
        /// <param name="worksSupportId"></param>
        /// <param name="worksSupportStepId"></param>
        /// <param name="updatedUser"></param>
        /// <param name="processUser"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ResultMessage InsertWorkFlowNextStep(int worksSupportId, int worksSupportStepId, string updatedUser, string processUser, string note)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                        objIData.Connect(); 
                        objIData.CreateNewStoredProcedure(SpAddnextstep);
                        objIData.AddParameter("@WORKSSUPPORTID", worksSupportId);
                        objIData.AddParameter("@WORKSSUPPORTSTEPID", worksSupportStepId);
                        objIData.AddParameter("@UPDATEDUSER", updatedUser);
                        objIData.AddParameter("@PROCESSUSER", processUser);
                        objIData.AddParameter("@NOTE", note);
                        objIData.ExecNonQuery();
                    }    
            
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert, "Lỗi thêm thông tin WorksSupport_Workflow", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DaWorksSupport_Workflow -> Insert", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        
        #region Stored Procedure Names

        public const string SpAdd = "WORKSSUPPORT_PRO_V2_ADD_BETA";
        public const string SpUpdate = "WORKSSUPPORT_PRO_V2_UPD_BETA";
        public const string SpDelete = "WORKSSUPPORT_WF_V2_DEL";
        public const string SpSelect = "WORKSSUPPORT_WF_V2_SEL";
        public const string SpSearch = "WORKSSUPPORT_PRO_V2_SRH_BETA";
        public const string SpUpdateindex = "WorksSupport_Workflow_UPDINDEX";
        public const string SpAddnextstep = "EO_WORKSSP_WORKFLOW_V2_ADD";

        #endregion
    }
}
