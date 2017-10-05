using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using Nc.Erp.WorksSupport.Api.Common;
using Nc.Erp.WorksSupport.Api.Models.Account;
using Nc.Erp.WorksSupport.Da.Configuration.Account;
using Nc.Erp.WorksSupport.Do.Configuration.Account;
using MyUtils;

namespace Nc.Erp.WorksSupport.Api.Features.WorksSupport
{
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// Created by      : Lương Trung Nhân
    /// Created date    : 30/05/2017
    /// Api tài khoản
    /// </summary>
    [RoutePrefix("api/v2/account")]
    public class AccountController : BaseController
    {
        public static string GetAvatar(string strUrlAvatar)
        {
            if (string.IsNullOrEmpty(strUrlAvatar))
            {
                return ConfigurationManager.AppSettings["UserImageDefault"];
            }

            ////ICached iCached = CachedFactory.Create(Library.Common.Cached.CacheDSN);
            //string strCacheKey = Library.Common.CacheHelper.GenKeyCache("Library.Common", "GetAvatar", strUrlAvatar);

            var strResult = string.IsNullOrEmpty(strUrlAvatar) ? ConfigurationManager.AppSettings["UserImageDefault"] : ConfigurationManager.AppSettings[(strUrlAvatar.IndexOf("UserImages/", StringComparison.Ordinal) > -1) ? "UserImageHostUrl" : "WebAlbumUrl"] + strUrlAvatar;
            strResult = strResult.Replace('\\', '/');
            return strResult;
        }
        /// <summary>
        /// Đăng nhập test
        /// </summary>
        /// <param name="objparam"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signin")]
        public Response<AccountBO> Login([FromBody] LoginParam objparam)
        {
            // Definition object to return.
            var ret = new Response<AccountBO>();
            var listError = new List<ValidationError>();
            try
            {
                // Validate param
                ValidateParams(objparam);
                var encodeKey = ConfigurationManager.AppSettings["DecryptionKey"];
                var objAccountBo = new AccountBO();
                var objUser = new User();
                var da = new DaAccount();
                var user = CryptoUtil.DecryptString(objparam.UserName, encodeKey, true, 256);
                var pass = CryptoUtil.DecryptString(objparam.PassWord, encodeKey, true, 256);

                da.LoadInfo(ref objUser, user, MaHoaMd5(pass));
                if (!string.IsNullOrEmpty(objUser.UserName))
                {
                    WebApiApplication.UserName = user;
                    WebApiApplication.Password = pass;
                    var objSessionBo = new Profile
                    {
                        UserName = objUser.UserName,
                        TokenString = "",
                        StorePermission = null,
                        SecurityLevelID = 0,
                        ReviewLevelID = 0,
                        PositionName = "",
                        PositionID = objUser.PositionID,
                        PhoneNumber = objUser.PhoneNumber,
                        Password = objUser.Password,
                        MainGroupPermission = null,
                        LoginLogID = "",
                        IsLogin = true,
                        hstbPermission = null,
                        FullName = objUser.FullName,
                        DepartmentID = objUser.DepartmentID,
                        Address = objUser.Address,
                        DefaultPictureURL = objUser.DefaultPictureURL
                    };
                    objAccountBo.Profile = objSessionBo;
                    objAccountBo.LsUserFunction = objUser.LsUserFunction;
                    ret.Success = true;
                    ret.Data = objAccountBo;
                    ret.TotalRecord = 1;
                }
                else
                {
                    var error = new ValidationError
                                    {
                                        Key = "username",
                                        Message = "Tên đăng nhập hoặc mật khẩu không đúng!"
                                    };
                    listError.Add(error);
                    ret.Success = false;
                    ret.Errors = listError;
                }
            }
            catch (Exception)
            {
                var error = new ValidationError
                                {
                                    Key = "username",
                                    Message = "Tên đăng nhập hoặc mật khẩu không đúng!"
                                };
                listError.Add(error);
                ret.Success = false;
                ret.Errors = listError;
            }
            return ret;
        }
        private string MaHoaMd5(string pass)
        {
            try
            {

                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] data = Encoding.UTF8.GetBytes(pass);
                data = md5.ComputeHash(data);
                StringBuilder buider = new StringBuilder();
                foreach (byte b in data)
                {
                    buider.Append(b.ToString("x2"));
                }
                return buider.ToString();
            }
            catch
            {
                return pass;
            }
        }


    }
}
