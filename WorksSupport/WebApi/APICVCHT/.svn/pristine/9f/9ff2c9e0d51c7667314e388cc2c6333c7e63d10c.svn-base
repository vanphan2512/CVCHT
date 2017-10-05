using Library.WebCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebClientAuthen
{
    [Serializable]
    public class WebServiceHost : ServiceHostBO
    {
        private const int KEYLENGTH = 32;
        private const double MAX_UNEVENT_TIME_ALLOW = 3; //Thời gian lớn nhất cho phép chênh lệch giữa client và server tính bằng phút.
        private static object _lock = new object();
        

        #region Các phương thức khởi tạo
        public WebServiceHost()
        {
        }

        public WebServiceHost(String strHostName, String strHostURL)
        {
            ServiceHostBO.Current.HostName = strHostName;
            ServiceHostBO.Current.HostURL = strHostURL;
        }

        public WebServiceHost(String strHostName, String strHostURL, bool bolIsPreHandShake)
        {
            this.HostName = strHostName;
            this.HostURL = strHostURL;
            this.IsPreHandShake = bolIsPreHandShake;
        }

        public WebServiceHost(String strHostName, String strHostURL, bool bolIsHandShake, String strGUID, String strEncryptKey, int intUnEventTime)
        {
            ServiceHostBO.Current.HostName = strHostName;
            ServiceHostBO.Current.HostURL = strHostURL;
            ServiceHostBO.Current.IsHandShake = bolIsHandShake;
            ServiceHostBO.Current.GUID = strGUID;
            ServiceHostBO.Current.EncryptKey = strEncryptKey;
            ServiceHostBO.Current.UnEventTime = intUnEventTime;
        }
        #endregion

        public String CreateIV()
        {
            DateTime dtIVTime = DateTime.Now.AddSeconds(this.UnEventTime);
            String strIV = dtIVTime.ToString("yyyyMMdd") + "." + dtIVTime.ToString("HHmm");
            return strIV;
        }

        public String RemoveIV(String strData)
        {
            return strData.Substring(0, strData.Length - 13);
        }

        private double GetUnEventTime(String strServerTime)
        {
            DateTime dtmServerTime = ConvertData.ConvertStrToDateTime(strServerTime);
            return GetUnEventTime(dtmServerTime);
        }

        private double GetUnEventTime(DateTime dtmServerTime)
        {
            TimeSpan objInterval = dtmServerTime - DateTime.Now;
            return objInterval.TotalSeconds;
        }

        public String CreateLoginData(String strUserName, String strPassword)
        {
            String strIV = CreateIV();
            String strData = strUserName + "|" + Library.WebCore.Cryptography.HashingMD5(strPassword) + "|" + "100" + "|" + SystemConfig.CertificateString + "|" + "" + "|" + strIV;
            strData = Cryptography.AesEncryptString(strData, this.EncryptKey);
            return strData;
        }

        public String CreateLoginDataPassMD5(String strUserName, String strPassword)
        {
            String strIV = CreateIV();

            String strData = strUserName + "|" + strPassword + "|" + "100" + "|" + SystemConfig.CertificateString + "|" + "" + "|" + strIV;
            strData = Cryptography.AesEncryptString(strData, this.EncryptKey);
            return strData;
        }

        public String CreateAuthenticateData(WebSessionUser objWebSessionUser)
        {
            return CreateAuthenticateData(objWebSessionUser.TokenString, objWebSessionUser.LoginLogID);
        }

        public String CreateAuthenticateData(String strTokenString, String strLoginLogID)
        {
            String strIV = CreateIV();
            String strData = Cryptography.AesEncryptString(strTokenString + "|" + strIV, this.EncryptKey);
            return strData;
        }

        public bool HandShake(String strHostName)
        {
            //lock (_lock)
            {
                try
                {
                    WebServiceHost ws = CacheHelper.iCached.Get<WebServiceHost>(strHostName);
                    if (ws != null && ws.IsHandShake) return true;
                    //if ((HttpContext.Current.Session[strHostName] as WebServiceHost) != null && ( HttpContext.Current.Session[strHostName] as WebServiceHost).IsHandShake) return true;

                    DateTime dtmServerTime = DateTime.Now;

                    String strURL = this.HostURL + "HandShake.asmx";
                    SSOService.HandShake.HandShake objHandShake = new SSOService.HandShake.HandShake();
                    objHandShake.Url = strURL;
                    objHandShake.CookieContainer = new System.Net.CookieContainer();
                    objHandShake.EnableDecompression = true;

                    if (!String.IsNullOrEmpty(this.GUID) && !String.IsNullOrEmpty(this.EncryptKey))
                    {
                        String strServerTime = objHandShake.CheckHandShakeExt(this.GUID);

                        if (strServerTime.Length > 0)
                        {
                            dtmServerTime = ConvertData.ConvertStrToDateTime(strServerTime);
                            this.UnEventTime = GetUnEventTime(dtmServerTime);
                            this.IsHandShake = true;
                            if (Math.Abs(ServiceHostBO.Current.UnEventTime / 60) > MAX_UNEVENT_TIME_ALLOW)
                            {
                                this.ErrorMessage = "Thời gian của máy chênh lệch với thời gian server quá " + MAX_UNEVENT_TIME_ALLOW.ToString() + " phút." + Environment.NewLine
                                                      + "Thời gian server: " + dtmServerTime.ToString("dd/MM/yyyy HH:mm");
                                Library.WebCore.FileLogger.LogAction("ServerTime 1: " + this.ErrorMessage);
                                return false;
                            }
                            this.CookieContainer = objHandShake.CookieContainer;
                            //Globals.GetWebServiceHost(this.HostName);
                            //HttpContext.Current.Session[strHostName] = this;
                            CacheHelper.iCached.Set<WebServiceHost>(strHostName, this, 1440);
                            return true;
                        }
                    }

                    DiffieHellman.DiffieHellman client = new DiffieHellman.DiffieHellman(KEYLENGTH);
                    client.GenerateRequest();
                    String strServerResponse = objHandShake.KeyExchange(client.ToString());
                    String[] arrResponse = strServerResponse.Split('|');
                    this.GUID = arrResponse[1];
                    client.HandleResponse(arrResponse[0]);

                    dtmServerTime = ConvertData.ConvertStrToDateTime(arrResponse[2]);
                    this.EncryptKey = Convert.ToBase64String(client.Key);
                    this.UnEventTime = GetUnEventTime(dtmServerTime);

                    if (Math.Abs(this.UnEventTime / 60) > MAX_UNEVENT_TIME_ALLOW)
                    {
                        this.ErrorMessage = "Thời gian của máy chênh lệch với thời gian server quá " + MAX_UNEVENT_TIME_ALLOW.ToString() + " phút." + Environment.NewLine
                                               + "Thời gian server: " + dtmServerTime.ToString("dd/MM/yyyy HH:mm");

                        Library.WebCore.FileLogger.LogAction("ServerTime 2: " + this.ErrorMessage);

                        return false;
                    }
                    this.IsHandShake = true;
                    this.CookieContainer = objHandShake.CookieContainer;
                    CacheHelper.iCached.Set<WebServiceHost>(strHostName, this, 1440);
                    //HttpContext.Current.Session[strHostName] = this;
                    //Globals.SetWebServiceHost(this.HostName);

                    return true;
                }
                catch (Exception objEx)
                {
                    this.IsHandShake = false;
                    this.ErrorMessage = "Lỗi xác nhận kết nối với server";
                    ErrorLog.Add("Lỗi bắt tay " + this.HostURL, objEx, "WEB.ERP.Authen");
                    Library.WebCore.FileLogger.LogAction("HandShake: " + objEx.Message + " || " + this.ErrorMessage);
                    return false;
                }
            }

        }

    }
}
