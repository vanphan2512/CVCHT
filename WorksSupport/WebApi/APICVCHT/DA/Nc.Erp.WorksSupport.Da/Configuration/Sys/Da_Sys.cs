using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.Sys;
using System;
using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Da.Configuration.Sys
{
       
    public class DaSys
    {
        #region Log Property
        public LogObject ObjLogObject = new LogObject();
        #endregion
        #region method

        /// <summary>
        /// Nạp thông tin người dùng
        /// </summary>
        /// <param name="lstUser"></param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage LoadAll(ref List<User> lstUser)
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
                    var objUser = new User();
                    if (!Convert.IsDBNull(reader[User.colUserName])) objUser.UserName = Convert.ToString(reader[User.colUserName]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colPassword])) objUser.Password = Convert.ToString(reader[User.colPassword]).Trim();
                    if (!Convert.IsDBNull(reader[User.colFullName])) objUser.FullName = Convert.ToString(reader[User.colFullName]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colFirstName])) objUser.FirstName = Convert.ToString(reader[User.colFirstName]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colLastName])) objUser.LastName = Convert.ToString(reader[User.colLastName]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colGender])) objUser.Gender = Convert.ToBoolean(reader[User.colGender]);
                    //if (!Convert.IsDBNull(reader[User.colBirthday])) objUser.Birthday = Convert.ToDateTime(reader[User.colBirthday]);
                    //if (!Convert.IsDBNull(reader[User.colEmail])) objUser.Email = Convert.ToString(reader[User.colEmail]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colPhoneNumber])) objUser.PhoneNumber = Convert.ToString(reader[User.colPhoneNumber]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colMOBI])) objUser.MOBI = Convert.ToString(reader[User.colMOBI]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colAddress])) objUser.Address = Convert.ToString(reader[User.colAddress]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colDepartmentID])) objUser.DepartmentID = Convert.ToInt32(reader[User.colDepartmentID]);
                    //if (!Convert.IsDBNull(reader[User.colPositionID])) objUser.PositionID = Convert.ToInt32(reader[User.colPositionID]);
                    //if (!Convert.IsDBNull(reader[User.colAreaID])) objUser.AreaID = Convert.ToInt32(reader[User.colAreaID]);
                    //if (!Convert.IsDBNull(reader[User.colDescription])) objUser.Description = Convert.ToString(reader[User.colDescription]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colBankAccountID])) objUser.BankAccountID = Convert.ToString(reader[User.colBankAccountID]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colIDCard])) objUser.IDCard = Convert.ToString(reader[User.colIDCard]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colBeginWorkDate])) objUser.BeginWorkDate = Convert.ToDateTime(reader[User.colBeginWorkDate]);
                    //if (!Convert.IsDBNull(reader[User.colWorkStoreID])) objUser.WorkStoreID = Convert.ToInt32(reader[User.colWorkStoreID]);
                    //if (!Convert.IsDBNull(reader[User.colSIGNATUREPICTURE])) objUser.SIGNATUREPICTURE = Convert.ToString(reader[User.colSIGNATUREPICTURE]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colDefaultPICTUREURL])) objUser.DefaultPICTUREURL = Convert.ToString(reader[User.colDefaultPICTUREURL]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colIsGroupUser])) objUser.IsGroupUser = Convert.ToBoolean(reader[User.colIsGroupUser]);
                    //if (!Convert.IsDBNull(reader[User.colIsActive])) objUser.IsActive = Convert.ToBoolean(reader[User.colIsActive]);
                    //if (!Convert.IsDBNull(reader[User.colIsSystem])) objUser.IsSystem = Convert.ToBoolean(reader[User.colIsSystem]);
                    //if (!Convert.IsDBNull(reader[User.colCreatedUser])) objUser.CreatedUser = Convert.ToString(reader[User.colCreatedUser]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colCreatedDate])) objUser.CreatedDate = Convert.ToDateTime(reader[User.colCreatedDate]);
                    //if (!Convert.IsDBNull(reader[User.colUpdatedUser])) objUser.UpdatedUser = Convert.ToString(reader[User.colUpdatedUser]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colUpdatedDate])) objUser.UpdatedDate = Convert.ToDateTime(reader[User.colUpdatedDate]);
                    //if (!Convert.IsDBNull(reader[User.colIsDeleted])) objUser.IsDeleted = Convert.ToBoolean(reader[User.colIsDeleted]);
                    //if (!Convert.IsDBNull(reader[User.colDeletedUser])) objUser.DeletedUser = Convert.ToString(reader[User.colDeletedUser]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colDeletedDate])) objUser.DeletedDate = Convert.ToDateTime(reader[User.colDeletedDate]);
                    //if (!Convert.IsDBNull(reader[User.colIsAutoAddCumulationTimeoffDays])) objUser.IsAutoAddCumulationTimeoffDays = Convert.ToBoolean(reader[User.colIsAutoAddCumulationTimeoffDays]);
                    //if (!Convert.IsDBNull(reader[User.colDefaultPictureFileID])) objUser.DefaultPictureFileID = Convert.ToString(reader[User.colDefaultPictureFileID]).Trim();
                    //if (!Convert.IsDBNull(reader[User.colSignaturePictureFileID])) objUser.SigNaturePictureFileID = Convert.ToString(reader[User.colSignaturePictureFileID]);
                    //if (!Convert.IsDBNull(reader[User.colWorkingPositionID])) objUser.WorkingPositionID = Convert.ToInt32(reader[User.colWorkingPositionID]);
                    lstUser.Add(objUser);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo, "Lỗi nạp thông tin người dùng", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_User -> LoadInfo", Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }
        public ResultMessage LoadByUserName(ref List<User> lstUser, string userName)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();

                objIData.CreateNewStoredProcedure(SpSelectByUser);
                objIData.AddParameter("@USERNAME", userName);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objUser = new User();
                    if (!Convert.IsDBNull(reader[User.colUserName])) objUser.UserName = Convert.ToString(reader[User.colUserName]).Trim();
                    if (!Convert.IsDBNull(reader[User.colFullName])) objUser.FullName = Convert.ToString(reader[User.colFullName]).Trim();
                    if (!Convert.IsDBNull(reader[User.colDefaultPICTUREURL])) objUser.DefaultPICTUREURL = Convert.ToString(reader[User.colDefaultPICTUREURL]).Trim();
                    lstUser.Add(objUser);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo, "Lỗi nạp thông tin người dùng", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_User -> LoadByUserName", Globals.ModuleName);
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
        public const string SpSelectAll = "SYS_USER_SRH_ALL_V2";
        public const string SpSelectByUser = "SYS_USER_SRH_USER_V2";
        #endregion
        #region Constructor
		#endregion
    }
}
