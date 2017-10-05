using Library.WebCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Nc.Erp.WorksSupport.Do.Configuration.Project;
using Library.DataAccess;

namespace Nc.Erp.WorksSupport.Da.Configuration.Project
{
    public class DaProject
    {
        /// <summary>
        /// GetAlll Eo_Project
        /// </summary>
        /// <param name="listProjects"></param>
        /// <returns></returns>
        public ResultMessage GetAll(ref List<Projects> listProjects)
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
                    var objWorksProjects = new Projects();
                    if (!Convert.IsDBNull(reader["PROJECTID"]))
                        objWorksProjects.ProjectId = Convert.ToString(reader["PROJECTID"]).Trim();
                    if (!Convert.IsDBNull(reader["PROJECTNAME"]))
                        objWorksProjects.ProjectName = Convert.ToString(reader["PROJECTNAME"]).Trim();
                    listProjects.Add(objWorksProjects);
                }
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin danh sách Projects", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail,
                    "DaProjects -> GetById", "");
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        #region Stored Procedure Names

        public const string SpSelectAll = "EO_PROJECT_V2_SHR";
        

        #endregion
    }
}
