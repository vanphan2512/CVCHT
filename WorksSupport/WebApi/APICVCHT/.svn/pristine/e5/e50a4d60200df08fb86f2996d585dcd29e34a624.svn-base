using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.WorksSupport;
using System;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Da.Configuration.WorksSupport
{
    /// <summary>
    /// Created by 		: Luong Trung Nhan
    /// Created date 	: 15/06/2017
    /// Tiến độ công việc cần hỗ trợ
    /// </summary>	
    public class DaWorksSupportProgress
    {
        #region Methods			

        /// <summary>
        /// Nạp thông tin tiến độ công việc cần hỗ trợ
        /// </summary>
        /// <param name="lstWorksSupportProgress"></param>
        /// <param name="worksSupportId"></param>
        /// <param name="objIData"></param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage GetWorksSupportProgressByWorkSupportId(ref List<WorksSupportProgress> lstWorksSupportProgress, int worksSupportId, IData objIData)
		{
			var objResultMessage = new ResultMessage();
			try 
			{
				objIData.CreateNewStoredProcedure(SpSelect);
                objIData.AddParameter("@WORKSSUPPORTID", worksSupportId);
				var reader = objIData.ExecStoreToDataReader();
			    while (reader.Read())
 				{
 					var objWorksSupportProgress = new WorksSupportProgress();
                    if (!Convert.IsDBNull(reader["UPDATEDDATE"])) objWorksSupportProgress.ProcessDate = Convert.ToDateTime(reader["UPDATEDDATE"]);
                    if (!Convert.IsDBNull(reader["WORKSSUPPORTSTATUSNAME"])) objWorksSupportProgress.WorksSupportStatusName = Convert.ToString(reader["WORKSSUPPORTSTATUSNAME"]);
                    if (!Convert.IsDBNull(reader["FULLNAME"])) objWorksSupportProgress.FullName = Convert.ToString(reader["FULLNAME"]);
                    if (!Convert.IsDBNull(reader["PROGRESSVALUE"])) objWorksSupportProgress.ProgressValue = Convert.ToInt32(reader["PROGRESSVALUE"]);

                    lstWorksSupportProgress.Add(objWorksSupportProgress);
 				}
				reader.Close();
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo, "Lỗi nạp thông tin tiến độ công việc cần hỗ trợ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Progress -> LoadInfo", Globals.ModuleName);
				return objResultMessage;
			}
    		return objResultMessage;
		}

        /// <summary>
        /// Xóa thông tin tiến độ công việc cần hỗ trợ
        /// </summary>
        /// <param name="worksSupportId"></param>
        /// <param name="objIData"></param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage Delete(int worksSupportId, IData objIData)
		{
			var objResultMessage = new ResultMessage();
			try 
			{
			    this.DeleteWorksSupportProcess(objIData, worksSupportId);
			}
			catch (Exception objEx)
			{
				objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete, "Lỗi xóa thông tin tiến độ công việc cần hỗ trợ", objEx.ToString());
				ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_WorksSupport_Progress -> Delete", Globals.ModuleName);
				return objResultMessage;
			}
    		return objResultMessage;
		}

        /// <summary>
        /// Xóa thông tin tiến độ công việc cần hỗ trợ
        /// </summary>
        /// <param name="objIData">Đối tượng Kết nối CSDL</param>
        /// <param name="worksSupportId"></param>
        /// <returns>Kết quả trả về</returns>
        private void DeleteWorksSupportProcess(IData objIData, int worksSupportId)
		{
			try 
			{
				objIData.CreateNewStoredProcedure(SpDelete);
                objIData.AddParameter("@WORKSSUPPORTID" , worksSupportId);
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

        public const string SpAdd = "WORKSSUPPORT_PRO__V2_ADD_BETA";
        public const string SpUpdate = "WORKSSUPPORT_PRO_V2_UPD_BETA";
        public const string SpDelete = "WORKSSUPPORT_PRO_V2_DEL_BETA";
        public const string SpSelect = "WORKSSUPPORT_PRO_V2_SEL_WORKID";
        public const string SpSearch = "WORKSSUPPORT_PRO_V2_SRH_BETA";
        public const string SpUpdateindex = "WorksSupport_Progress_UPDINDEX";
		#endregion
    }
}
