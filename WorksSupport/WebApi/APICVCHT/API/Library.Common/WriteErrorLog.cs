using Library.Common.SSOTransac_WSError;
using System;
using WebClientAuthen;
namespace Library.Common
{
    public class WriteErrorLog
    {
        static WriteErrorLog instance;
        public static WriteErrorLog Current
        {
            get
            {
                return instance ?? (instance = new WriteErrorLog());
            }
        }
        const string WSHostName = "WSHost_SSOService9P";
        const string PATH_WS = "common/Error.asmx";
        SSOTransac_WSError.ResultMessage WSResultMessage = new SSOTransac_WSError.ResultMessage();

        SSOTransac_WSError.Error objProxySS0;
        /// <summary>
        /// 
        /// </summary>
        SSOTransac_WSError.Error WSProxySS0
        {
            get
            {
                if (objProxySS0 == null)
                {
                    objProxySS0 = new Error();
                    objProxySS0.Url = RDAuthorize.GetWSHostUrl(WSHostName) + PATH_WS;
                    objProxySS0.UserAgent = "vpdt.com";
                }
                return objProxySS0;
            }
        }

        /// <summary>
        /// ghi lỗi hệ thống.
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strContent"></param>
        /// <param name="strEvent"></param>
        /// <param name="strModuleName"></param>
        /// <returns></returns>
//        public SSOTransac_WSError.ResultMessage WriteLog(string strTitle, string strContent, string strEvent, string strModuleName)
//        {
//            string strAuthenData = RDAuthorize.GetAuthenData(WSHostName);
//            string strGuid = RDAuthorize.GetGUID(WSHostName);
//            SSOTransac_WSError.Error WSError = new SSOTransac_WSError.Error();
//            WSError.Url = RDAuthorize.GetWSHostUrl(WSHostName) + PATH_WS;
//            WSResultMessage = WSError.Insert(strAuthenData, strGuid, strTitle, strContent, strEvent, strModuleName);
//            if (CheckError.CheckMissSession((int)WSResultMessage.ErrorType, true))
//            {
//                RDAuthorize.SetUnhandshake(WSHostName);
//                strAuthenData = RDAuthorize.GetAuthenData(WSHostName);
//                strGuid = RDAuthorize.GetGUID(WSHostName);
//                WSError.Url = RDAuthorize.GetWSHostUrl(WSHostName) + PATH_WS;
//                WSResultMessage = WSError.Insert(strAuthenData, strGuid, strTitle, strContent, strEvent, strModuleName);
//            }
//            return WSResultMessage;
//        }
//
//        public void WriteLog(ResultMessage objResultMessage, string strEvent)
//        {
//            String authenData = RDAuthorize.GetAuthenData(WSHostName);
//            String guid = RDAuthorize.GetGUID(WSHostName);
//
//            var ResultMessage = WSProxySS0.Insert(authenData, guid, objResultMessage.Message, objResultMessage.MessageDetail, strEvent, "ProERP2015");
//        }
    }
}
