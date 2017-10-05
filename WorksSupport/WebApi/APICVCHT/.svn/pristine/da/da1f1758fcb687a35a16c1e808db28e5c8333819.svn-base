using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.Work;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nc.Erp.WorksSupport.Da.Configuration.Works
{
    public class DaWorksSupport_Works
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="objWorksSupportStatus"></param>
        /// <returns></returns>
        public ResultMessage Insert(int intworksSupportId,List<int> lstworksId, string strnote, ref List<WorksSupport_Works> data)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            WorksSupport_Works objWork = new WorksSupport_Works();
            try
            {
                
                objIData.Connect();
                string item = Convert.ToString(lstworksId);
                //var lstWorkInID = item.Split(',');
                for (int i = 0; i < lstworksId.Count; i++)
                {
                    Insert(objIData, intworksSupportId, lstworksId[i], strnote, ref data);
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

        #region Protected Methods

        /// <summary>
        /// Thêm cong viec lien quan vao eo_workssupport_works
        /// </summary>
        /// <param name="objIData"></param>
        /// <param name="objWorksSupportStatus"></param>
        protected virtual void Insert(IData objIData, int intworksSupportId, int lstworksId, string strnote, ref List<WorksSupport_Works> list)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SpAdd);
                objIData.AddParameter("@WORKSSUPPORTID", intworksSupportId);
                objIData.AddParameter("@WORKSID", lstworksId);
                objIData.AddParameter("@NOTE", strnote);
                objIData.ExecStoreToDataReader();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }
        #endregion

        #region Stored Procedure Names

        public const string SpAdd = "WORKSSUPPORT_WORK_V2_ADD";
        #endregion
    }
}
