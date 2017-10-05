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

    public class DaWorksSupport_WorksSupportTypePeriod
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion

        #region
        /// <summary>
        /// GetAlll WorksSupportStatus 
        /// </summary>
        /// <param name="objWorksSupportStatus"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<WorksSupport_WorksSupportTypePeriod> list)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SpSelectAll);
                IDataReader reader = objIData.ExecStoreToDataReader();
                //DataTable dtb = objIData.ExecStoreToDataTable();
                //list = MyUtils.DataTableExtensions.ToList<WorksSupportStatus>(dtb);
                WorksSupport_WorksSupportTypePeriod objWorksSupportTypePeriod;
                while (reader.Read())
                {
                    objWorksSupportTypePeriod = new WorksSupport_WorksSupportTypePeriod();
                    if (!Convert.IsDBNull(reader["PERIODID"])) objWorksSupportTypePeriod.PeriodId = Convert.ToInt32(reader["PERIODID"]);
                    if (!Convert.IsDBNull(reader["PERIODNAME"])) objWorksSupportTypePeriod.PeriodName = Convert.ToString(reader["PERIODNAME"]).Trim();

                    list.Add(objWorksSupportTypePeriod);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách WorksSupportTypePeriod", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaWorksSupportTypePeriod -> GetAll", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        #endregion

        #region
        public const string SpSelectAll = "WORKSSUPPORTTYPEPERIOD_V2_GETS";
        #endregion
    }
}
