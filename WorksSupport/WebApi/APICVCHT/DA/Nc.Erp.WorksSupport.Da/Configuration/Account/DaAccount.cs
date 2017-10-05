using System;
using System.Collections.Generic;
using Library.DataAccess;
using Library.WebCore;
using Nc.Erp.WorksSupport.Do.Configuration.Account;

namespace Nc.Erp.WorksSupport.Da.Configuration.Account
{
    public class DaAccount
    {
        #region Log Property

        public LogObject objLogObject = new LogObject();

        #endregion

        #region Methods

        /// <summary>
        ///     Nạp thông tin người dùng
        /// </summary>
        /// <param name="objUser">Đối tượng trả về</param>
        /// <param name="strUserName"></param>
        /// <param name="strPassword"></param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage LoadInfo(ref User objUser, string strUserName, string strPassword)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure(SP_SELECT);
                objIData.AddParameter("@" + User.colUserName, strUserName);
                objIData.AddParameter("@" + User.colPassword, strPassword);
                var reader = objIData.ExecStoreToDataReader();
                if (reader.Read())
                {
                    objUser = new User();
                    if (!Convert.IsDBNull(reader[User.colUserName]))
                        objUser.UserName = Convert.ToString(reader[User.colUserName]).Trim();
                    if (!Convert.IsDBNull(reader[User.colPassword]))
                        objUser.Password = Convert.ToString(reader[User.colPassword]).Trim();
                    if (!Convert.IsDBNull(reader[User.colFullName]))
                        objUser.FullName = Convert.ToString(reader[User.colFullName]).Trim();
                    if (!Convert.IsDBNull(reader[User.colFirstName]))
                        objUser.FirstName = Convert.ToString(reader[User.colFirstName]).Trim();
                    if (!Convert.IsDBNull(reader[User.colLastName]))
                        objUser.LastName = Convert.ToString(reader[User.colLastName]).Trim();
                    if (!Convert.IsDBNull(reader[User.colGender]))
                        objUser.Gender = Convert.ToBoolean(reader[User.colGender]);
                    if (!Convert.IsDBNull(reader[User.colBirthday]))
                        objUser.Birthday = Convert.ToDateTime(reader[User.colBirthday]);
                    if (!Convert.IsDBNull(reader[User.colEmail]))
                        objUser.Email = Convert.ToString(reader[User.colEmail]).Trim();
                    if (!Convert.IsDBNull(reader[User.colPhoneNumber]))
                        objUser.PhoneNumber = Convert.ToString(reader[User.colPhoneNumber]).Trim();
                    if (!Convert.IsDBNull(reader[User.colMOBI]))
                        objUser.MOBI = Convert.ToString(reader[User.colMOBI]).Trim();
                    if (!Convert.IsDBNull(reader[User.colAddress]))
                        objUser.Address = Convert.ToString(reader[User.colAddress]).Trim();
                    if (!Convert.IsDBNull(reader[User.colDepartmentID]))
                        objUser.DepartmentID = Convert.ToInt32(reader[User.colDepartmentID]);
                    if (!Convert.IsDBNull(reader[User.colPositionID]))
                        objUser.PositionID = Convert.ToInt32(reader[User.colPositionID]);
                    if (!Convert.IsDBNull(reader[User.colAreaID]))
                        objUser.AreaID = Convert.ToInt32(reader[User.colAreaID]);
                    if (!Convert.IsDBNull(reader[User.colDescription]))
                        objUser.Description = Convert.ToString(reader[User.colDescription]).Trim();
                    if (!Convert.IsDBNull(reader[User.colBankAccountID]))
                        objUser.BankAccountID = Convert.ToString(reader[User.colBankAccountID]).Trim();
                    if (!Convert.IsDBNull(reader[User.colIDCard]))
                        objUser.IDCard = Convert.ToString(reader[User.colIDCard]).Trim();
                    if (!Convert.IsDBNull(reader[User.colBeginWorkDate]))
                        objUser.BeginWorkDate = Convert.ToDateTime(reader[User.colBeginWorkDate]);
                    if (!Convert.IsDBNull(reader[User.colWorkStoreID]))
                        objUser.WorkStoreID = Convert.ToInt32(reader[User.colWorkStoreID]);
                    if (!Convert.IsDBNull(reader[User.colSIGNATUREPICTURE]))
                        objUser.SIGNATUREPICTURE = Convert.ToString(reader[User.colSIGNATUREPICTURE]).Trim();
                    if (!Convert.IsDBNull(reader[User.colDefaultPictureURL]))
                        objUser.DefaultPictureURL = Convert.ToString(reader[User.colDefaultPictureURL]).Trim();
                    if (!Convert.IsDBNull(reader[User.colIsGroupUser]))
                        objUser.IsGroupUser = Convert.ToBoolean(reader[User.colIsGroupUser]);
                    if (!Convert.IsDBNull(reader[User.colIsActive]))
                        objUser.IsActive = Convert.ToBoolean(reader[User.colIsActive]);
                    if (!Convert.IsDBNull(reader[User.colIsSystem]))
                        objUser.IsSystem = Convert.ToBoolean(reader[User.colIsSystem]);
                    if (!Convert.IsDBNull(reader[User.colCreatedUser]))
                        objUser.CreatedUser = Convert.ToString(reader[User.colCreatedUser]).Trim();
                    if (!Convert.IsDBNull(reader[User.colCreatedDate]))
                        objUser.CreatedDate = Convert.ToDateTime(reader[User.colCreatedDate]);
                    if (!Convert.IsDBNull(reader[User.colUpdatedUser]))
                        objUser.UpdatedUser = Convert.ToString(reader[User.colUpdatedUser]).Trim();
                    if (!Convert.IsDBNull(reader[User.colUpdatedDate]))
                        objUser.UpdatedDate = Convert.ToDateTime(reader[User.colUpdatedDate]);
                    if (!Convert.IsDBNull(reader[User.colIsDeleted]))
                        objUser.IsDeleted = Convert.ToBoolean(reader[User.colIsDeleted]);
                    if (!Convert.IsDBNull(reader[User.colDeletedUser]))
                        objUser.DeletedUser = Convert.ToString(reader[User.colDeletedUser]).Trim();
                    if (!Convert.IsDBNull(reader[User.colDeletedDate]))
                        objUser.DeletedDate = Convert.ToDateTime(reader[User.colDeletedDate]);
                    if (!Convert.IsDBNull(reader[User.colIsAutoAddCumulationTimeoffDays]))
                        objUser.IsAutoAddCumulationTimeoffDays =
                            Convert.ToBoolean(reader[User.colIsAutoAddCumulationTimeoffDays]);
                    if (!Convert.IsDBNull(reader[User.colDefaultPictureFileID]))
                        objUser.DefaultPictureFileID = Convert.ToString(reader[User.colDefaultPictureFileID]).Trim();
                    if (!Convert.IsDBNull(reader[User.colSignaturePictureFileID]))
                        objUser.SigNaturePictureFileID = Convert.ToString(reader[User.colSignaturePictureFileID]);
                    if (!Convert.IsDBNull(reader[User.colWorkingPositionID]))
                        objUser.WorkingPositionID = Convert.ToInt32(reader[User.colWorkingPositionID]);
                    if (objUser.UserName != "")
                        objUser.LsUserFunction = GetUserUserFunction(objUser.UserName);
                }
                else
                {
                    objUser = null;
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.LoadInfo,
                    "Lỗi nạp thông tin người dùng", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_User -> LoadInfo",
                    Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }

        /// <summary>
        ///     Thêm thông tin người dùng
        /// </summary>
        /// <param name="objUser">Đối tượng truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage Insert(User objUser)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                Insert(objIData, objUser);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Insert,
                    "Lỗi thêm thông tin người dùng", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_User -> Insert",
                    Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }


        /// <summary>
        ///     Thêm thông tin người dùng
        /// </summary>
        /// <param name="objIData">Đối tượng Kết nối CSDL</param>
        /// <param name="objUser">Đối tượng truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public void Insert(IData objIData, User objUser)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SP_ADD);
                objIData.AddParameter("@" + User.colUserName, objUser.UserName);
                objIData.AddParameter("@" + User.colPassword, objUser.Password);
                objIData.AddParameter("@" + User.colFullName, objUser.FullName);
                objIData.AddParameter("@" + User.colFirstName, objUser.FirstName);
                objIData.AddParameter("@" + User.colLastName, objUser.LastName);
                objIData.AddParameter("@" + User.colGender, objUser.Gender);
                objIData.AddParameter("@" + User.colBirthday, objUser.Birthday);
                objIData.AddParameter("@" + User.colEmail, objUser.Email);
                objIData.AddParameter("@" + User.colPhoneNumber, objUser.PhoneNumber);
                objIData.AddParameter("@" + User.colMOBI, objUser.MOBI);
                objIData.AddParameter("@" + User.colAddress, objUser.Address);
                objIData.AddParameter("@" + User.colDepartmentID, objUser.DepartmentID);
                objIData.AddParameter("@" + User.colPositionID, objUser.PositionID);
                objIData.AddParameter("@" + User.colAreaID, objUser.AreaID);
                objIData.AddParameter("@" + User.colDescription, objUser.Description);
                objIData.AddParameter("@" + User.colBankAccountID, objUser.BankAccountID);
                objIData.AddParameter("@" + User.colIDCard, objUser.IDCard);
                objIData.AddParameter("@" + User.colBeginWorkDate, objUser.BeginWorkDate);
                objIData.AddParameter("@" + User.colWorkStoreID, objUser.WorkStoreID);
                objIData.AddParameter("@" + User.colSIGNATUREPICTURE, objUser.SIGNATUREPICTURE);
                objIData.AddParameter("@" + User.colDefaultPictureURL, objUser.DefaultPictureURL);
                objIData.AddParameter("@" + User.colIsGroupUser, objUser.IsGroupUser);
                objIData.AddParameter("@" + User.colIsActive, objUser.IsActive);
                objIData.AddParameter("@" + User.colIsSystem, objUser.IsSystem);
                objIData.AddParameter("@" + User.colCreatedUser, objUser.CreatedUser);
                objIData.AddParameter("@ISAUTOADDCUMUTIMEOFFDAYS", objUser.IsAutoAddCumulationTimeoffDays);
                objIData.AddParameter("@" + User.colDefaultPictureFileID, objUser.DefaultPictureFileID);
                objIData.AddParameter("@" + User.colSignaturePictureFileID, objUser.SigNaturePictureFileID);
                objIData.AddParameter("@" + User.colWorkingPositionID, objUser.WorkingPositionID);
                objIData.AddParameter("@UserHostAddress", objLogObject.UserHostAddress);
                objIData.AddParameter("@CertificateString", objLogObject.CertificateString);
                objIData.AddParameter("@LoginLogID", objLogObject.LoginLogID);
                objIData.AddParameter("@ISAUTOGENID", objUser.IsAutoGenID);
                objIData.AddParameter("@IsCreatedEmployee", objUser.IsCreatedEmployee);
                objIData.ExecNonQuery();
            }
            catch (Exception objEx)
            {
                objIData.RollBackTransaction();
                throw objEx;
            }
        }


        /// <summary>
        ///     Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="objUser">Đối tượng truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage Update(User objUser)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                Update(objIData, objUser);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Update,
                    "Lỗi cập nhật thông tin người dùng", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_User -> Update",
                    Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }


        /// <summary>
        ///     Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="objIData">Đối tượng Kết nối CSDL</param>
        /// <param name="objUser">Đối tượng truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public void Update(IData objIData, User objUser)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SP_UPDATE);
                objIData.AddParameter("@" + User.colUserName, objUser.UserName);
                objIData.AddParameter("@" + User.colPassword, objUser.Password);
                objIData.AddParameter("@" + User.colFullName, objUser.FullName);
                objIData.AddParameter("@" + User.colFirstName, objUser.FirstName);
                objIData.AddParameter("@" + User.colLastName, objUser.LastName);
                objIData.AddParameter("@" + User.colGender, objUser.Gender);
                objIData.AddParameter("@" + User.colBirthday, objUser.Birthday);
                objIData.AddParameter("@" + User.colEmail, objUser.Email);
                objIData.AddParameter("@" + User.colPhoneNumber, objUser.PhoneNumber);
                objIData.AddParameter("@" + User.colMOBI, objUser.MOBI);
                objIData.AddParameter("@" + User.colAddress, objUser.Address);
                objIData.AddParameter("@" + User.colDepartmentID, objUser.DepartmentID);
                objIData.AddParameter("@" + User.colPositionID, objUser.PositionID);
                objIData.AddParameter("@" + User.colAreaID, objUser.AreaID);
                objIData.AddParameter("@" + User.colDescription, objUser.Description);
                objIData.AddParameter("@" + User.colBankAccountID, objUser.BankAccountID);
                objIData.AddParameter("@" + User.colIDCard, objUser.IDCard);
                objIData.AddParameter("@" + User.colBeginWorkDate, objUser.BeginWorkDate);
                objIData.AddParameter("@" + User.colWorkStoreID, objUser.WorkStoreID);
                objIData.AddParameter("@" + User.colSIGNATUREPICTURE, objUser.SIGNATUREPICTURE);
                objIData.AddParameter("@" + User.colDefaultPictureURL, objUser.DefaultPictureURL);
                objIData.AddParameter("@" + User.colIsGroupUser, objUser.IsGroupUser);
                objIData.AddParameter("@" + User.colIsActive, objUser.IsActive);
                objIData.AddParameter("@" + User.colIsSystem, objUser.IsSystem);
                objIData.AddParameter("@" + User.colUpdatedUser, objUser.UpdatedUser);
                objIData.AddParameter("@ISAUTOADDCUMUTIMEOFFDAYS", objUser.IsAutoAddCumulationTimeoffDays);
                objIData.AddParameter("@UserHostAddress", objLogObject.UserHostAddress);
                objIData.AddParameter("@CertificateString", objLogObject.CertificateString);
                objIData.AddParameter("@LoginLogID", objLogObject.LoginLogID);
                objIData.AddParameter("@" + User.colDefaultPictureFileID, objUser.DefaultPictureFileID);
                objIData.AddParameter("@" + User.colSignaturePictureFileID, objUser.SigNaturePictureFileID);
                objIData.AddParameter("@" + User.colWorkingPositionID, objUser.WorkingPositionID);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }


        /// <summary>
        ///     Xóa thông tin người dùng
        /// </summary>
        /// <param name="objUser">Đối tượng truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public ResultMessage Delete(User objUser)
        {
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                Delete(objIData, objUser);
            }
            catch (Exception objEx)
            {
                objResultMessage = new ResultMessage(true, SystemError.ErrorTypes.Delete,
                    "Lỗi xóa thông tin người dùng", objEx.ToString());
                ErrorLog.Add(objIData, objResultMessage.Message, objResultMessage.MessageDetail, "DA_User -> Delete",
                    Globals.ModuleName);
                return objResultMessage;
            }
            finally
            {
                objIData.Disconnect();
            }
            return objResultMessage;
        }


        /// <summary>
        ///     Xóa thông tin người dùng
        /// </summary>
        /// <param name="objIData">Đối tượng Kết nối CSDL</param>
        /// <param name="objUser">Đối tượng truyền vào</param>
        /// <returns>Kết quả trả về</returns>
        public void Delete(IData objIData, User objUser)
        {
            try
            {
                objIData.CreateNewStoredProcedure(SP_DELETE);
                objIData.AddParameter("@" + User.colUserName, objUser.UserName);
                objIData.AddParameter("@" + User.colDeletedUser, objUser.DeletedUser);
                objIData.AddParameter("@UserHostAddress", objLogObject.UserHostAddress);
                objIData.AddParameter("@CertificateString", objLogObject.CertificateString);
                objIData.AddParameter("@LoginLogID", objLogObject.LoginLogID);
                objIData.ExecNonQuery();
            }
            catch (Exception)
            {
                objIData.RollBackTransaction();
                throw;
            }
        }

        public static List<UserFunction> GetUserUserFunction(string strUserName)
        {
            var lstUserFunction = new List<UserFunction>();
            var objResultMessage = new ResultMessage();
            var objIData = Data.CreateData();
            try
            {
                objIData.Connect();
                objIData.CreateNewStoredProcedure("SYS_USER_LISTFUNCTION");
                objIData.AddParameter("@USERNAME", strUserName);
                var reader = objIData.ExecStoreToDataReader();
                while (reader.Read())
                {
                    var objSubject = new UserFunction();
                    if (!Convert.IsDBNull(reader[UserFunction.colFunctionID]))
                        objSubject.FunctionID = Convert.ToString(reader[UserFunction.colFunctionID]).Trim();
                    if (!Convert.IsDBNull(reader[UserFunction.colFunctionName]))
                        objSubject.FunctionName = Convert.ToString(reader[UserFunction.colFunctionName]).Trim();
                    if (!Convert.IsDBNull(reader[UserFunction.colDescription]))
                        objSubject.Description = Convert.ToString(reader[UserFunction.colDescription]).Trim();
                    if (!Convert.IsDBNull(reader[UserFunction.colSecurityLevelId]))
                        objSubject.SecurityLevelId = Convert.ToInt32(reader[UserFunction.colSecurityLevelId]);

                    lstUserFunction.Add(objSubject);
                }
                reader.Close();
            }
            catch (Exception objEx)
            {
                objResultMessage.MessageDetail = objEx.Message;
                FileLogger.LogAction("WebSessionUser Login Ex:  " + objEx.StackTrace);
                return null;
            }
            finally
            {
                objIData.Disconnect();
            }
            return lstUserFunction;
        }

        #endregion


        #region Constructor

        #endregion


        #region Stored Procedure Names

        public const string SP_ADD = "SYS_USER_ADD";
        public const string SP_UPDATE = "SYS_USER_UPD";
        public const string SP_DELETE = "SYS_USER_DEL";
        public const string SP_SELECT = "SYS_USER_SEL_API";
        public const string SP_SEARCH = "SYS_USER_SRH";
        public const string SP_UPDATEINDEX = "SYS_USER_UPDINDEX";

        #endregion
    }
}
