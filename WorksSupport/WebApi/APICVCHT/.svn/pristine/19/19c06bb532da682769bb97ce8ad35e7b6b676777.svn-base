using Library.Common.ERPFMSServices_WSFileManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using nc.web.common;
using WebClientAuthen;
using System.IO;

namespace Library.Common
{
    public class PLCFileManager
    {
        string WSHostName = System.Configuration.ConfigurationManager.AppSettings["WSHost_ERPFMSServices"].ToString();
        const string WSHostPath = "WSFileManager.asmx";
        string WSHostNameCMSWebContent = System.Configuration.ConfigurationManager.AppSettings["WSHost_ERPFMSServices3"].ToString();
        const string WSHostPathCMSWebContent = "WSFileManager.asmx";

        ERPFMSServices_WSFileManager.WSFileManager objWSFileManager;
        ERPFMSServices_WSFileManager.WSFileManager objWSFileManagerCMS;
        ERPFMSServices_WSFileManager.WSFileManager WSFileManager
        {
            get
            {
                if (objWSFileManager == null)
                {
                    objWSFileManager = new ERPFMSServices_WSFileManager.WSFileManager();
                    objWSFileManager.Url = RDAuthorize.GetWSHostUrl(WSHostName) + WSHostPath;
                }
                return objWSFileManager;
            }
        }

        ERPFMSServices_WSFileManager.WSFileManager WSFileManagerCMS
        {
            get
            {
                if (objWSFileManagerCMS == null)
                {
                    objWSFileManagerCMS = new ERPFMSServices_WSFileManager.WSFileManager();
                    objWSFileManagerCMS.Url = RDAuthorize.GetWSHostUrl(WSHostNameCMSWebContent) + WSHostPathCMSWebContent;
                }
                return objWSFileManagerCMS;
            }
        }

        public ResultMessage UploadFile(ERPFMSServices_WSFileManager.UploadFile objUploadFile, ref string strFileID, ref string strFilePath, string strUrlLogPath = "")
        {
            ResultMessage objResultMessage = new ResultMessage();

            try
            {
                if (RDAuthorize.IsLogedIn() == false)
                {
                    string userName = ConfigHelper.GetString("Site:Order:Usr");
                    string passWord = ConfigHelper.GetString("Site:Order:Pwd");
                    if (!RDAuthorize.Login(userName, passWord, false))
                        return null;
                }
                string strAuthenData = RDAuthorize.GetAuthenData(WSHostName);
                string strGuid = RDAuthorize.GetGUID(WSHostName);

                DateTime dtFromTime = DateTime.Now;
                String strLogID = Guid.NewGuid().ToString();
                Library.WebCore.WebExecuteLog.LogStartExecuteTime(strGuid, strLogID, "InsertPrintLog", objUploadFile != null ? objUploadFile.ToString() : "");

                objResultMessage = WSFileManager.UploadFile(strAuthenData, strGuid, objUploadFile, ref strFileID, ref strFilePath);
                if (CheckError.CheckMissSession((int)objResultMessage.ErrorType, true))
                {
                    RDAuthorize.SetUnhandshake(WSHostName);
                    strAuthenData = RDAuthorize.GetAuthenData(WSHostName);
                    strGuid = RDAuthorize.GetGUID(WSHostName);
                    objResultMessage = WSFileManager.UploadFile(strAuthenData, strGuid, objUploadFile, ref strFileID, ref strFilePath);
                }

                if (!string.IsNullOrEmpty(strUrlLogPath))
                {
                    Library.WebCore.FileLogger.LogPath = System.IO.Path.Combine(strUrlLogPath, "LogInternalServerError.log");
                    Library.WebCore.FileLogger.LogAction("Loi upload hinh loai van ban Lỗi: " + objResultMessage.IsError + " \n Thông tin: " + objResultMessage.Message + " \n detail:" + objResultMessage.MessageDetail + " URL: " + WSFileManager.Url);
                }
                Library.WebCore.WebExecuteLog.LogExecuteTime(strGuid, strLogID, "InsertPrintLog", "CZ_CITIZEN_PRINTLOG", RDAuthorize.Username, dtFromTime);
            }
            catch (System.Exception objEx)
            {
                Library.Common.SSOTransac_WSError.ResultMessage objResultMessageS = new Library.Common.SSOTransac_WSError.ResultMessage();
                objResultMessageS.IsError = true;
                objResultMessageS.Message = objEx.Source;
                objResultMessageS.MessageDetail = objResultMessage.MessageDetail;
                Library.Common.WriteErrorLog.Current.WriteLog(objResultMessageS, "UploadFile ---> PLCFilemanager");
            }
            return objResultMessage;
        }
        public ResultMessage DownloadFile(ref ERPFMSServices_WSFileManager.DownloadFile objDownloadFile, string log = "")
        {
            ResultMessage objResultMessage = new ResultMessage();

            try
            {
                string strAuthenData = RDAuthorize.GetAuthenData(WSHostName);
                string strGuid = RDAuthorize.GetGUID(WSHostName);

                DateTime dtFromTime = DateTime.Now;
                String strLogID = Guid.NewGuid().ToString();
                Library.WebCore.WebExecuteLog.LogStartExecuteTime(strGuid, strLogID, "InsertPrintLog", "");

                objResultMessage = WSFileManager.DownloadFile(strAuthenData, strGuid, ref objDownloadFile);
                if (CheckError.CheckMissSession((int)objResultMessage.ErrorType, true))
                {
                    RDAuthorize.SetUnhandshake(WSHostName);
                    strAuthenData = RDAuthorize.GetAuthenData(WSHostName);
                    strGuid = RDAuthorize.GetGUID(WSHostName);
                    objResultMessage = WSFileManager.DownloadFile(strAuthenData, strGuid, ref objDownloadFile);
                }


                Library.WebCore.FileLogger.LogPath = System.IO.Path.Combine(log, "LogInternalServerError.log");
                Library.WebCore.FileLogger.LogAction("Download tập tin đính kèm Lỗi: " + objResultMessage.IsError + " \n Thông tin: " + objResultMessage.Message + " \n: " + objDownloadFile.FileID + " Path: " + objDownloadFile.FilePath + " application: " + objDownloadFile.FMSApplicationID);

                Library.WebCore.WebExecuteLog.LogExecuteTime(strGuid, strLogID, "InsertPrintLog", "CZ_CITIZEN_PRINTLOG", RDAuthorize.Username, dtFromTime);
            }
            catch (System.Exception objEx)
            {
                Library.Common.SSOTransac_WSError.ResultMessage objResultMessageS = new Library.Common.SSOTransac_WSError.ResultMessage();
                objResultMessageS.IsError = true;
                objResultMessageS.Message = objEx.Source;
                objResultMessageS.MessageDetail = objResultMessage.MessageDetail;
                Library.Common.WriteErrorLog.Current.WriteLog(objResultMessageS, "DownloadFile ---> PLCFilemanager");
            }
            return objResultMessage;
        }

        public ResultMessage UploadFileCMSWebContent(ERPFMSServices_WSFileManager.UploadFile objUploadFile, ref string strFileID, ref string strFilePath)
        {
            ResultMessage objResultMessage = new ResultMessage();

            try
            {
                string strAuthenData = RDAuthorize.GetAuthenData(WSHostNameCMSWebContent);
                string strGuid = RDAuthorize.GetGUID(WSHostNameCMSWebContent);

                DateTime dtFromTime = DateTime.Now;
                String strLogID = Guid.NewGuid().ToString();
                Library.WebCore.WebExecuteLog.LogStartExecuteTime(strGuid, strLogID, "InsertPrintLog", objUploadFile != null ? objUploadFile.ToString() : "");

                objResultMessage = WSFileManagerCMS.UploadFile(strAuthenData, strGuid, objUploadFile, ref strFileID, ref strFilePath);
                if (CheckError.CheckMissSession((int)objResultMessage.ErrorType, true))
                {
                    RDAuthorize.SetUnhandshake(WSHostNameCMSWebContent);
                    strAuthenData = RDAuthorize.GetAuthenData(WSHostNameCMSWebContent);
                    strGuid = RDAuthorize.GetGUID(WSHostNameCMSWebContent);
                    objResultMessage = WSFileManagerCMS.UploadFile(strAuthenData, strGuid, objUploadFile, ref strFileID, ref strFilePath);
                }

                Library.WebCore.WebExecuteLog.LogExecuteTime(strGuid, strLogID, "InsertPrintLog", "CZ_CITIZEN_PRINTLOG", RDAuthorize.Username, dtFromTime);
            }
            catch (System.Exception objEx)
            {
                Library.Common.SSOTransac_WSError.ResultMessage objResultMessageS = new Library.Common.SSOTransac_WSError.ResultMessage();
                objResultMessageS.IsError = true;
                objResultMessageS.Message = objEx.Source;
                objResultMessageS.MessageDetail = objResultMessage.MessageDetail;
                objResultMessage.IsError = true;
                objResultMessage.Message = objEx.Source;
                objResultMessage.MessageDetail = objEx.Message;
                Library.Common.WriteErrorLog.Current.WriteLog(objResultMessageS, "UploadFileCMSWebContent ---> PLCFilemanager");
            }
            return objResultMessage;
        }
        public ResultMessage DownloadFileCMSWebContent(ref ERPFMSServices_WSFileManager.DownloadFile objDownloadFile)
        {
            ResultMessage objResultMessage = new ResultMessage();

            try
            {
                string strAuthenData = RDAuthorize.GetAuthenData(WSHostNameCMSWebContent);
                string strGuid = RDAuthorize.GetGUID(WSHostNameCMSWebContent);

                DateTime dtFromTime = DateTime.Now;
                String strLogID = Guid.NewGuid().ToString();
                Library.WebCore.WebExecuteLog.LogStartExecuteTime(strGuid, strLogID, "InsertPrintLog", "");

                objResultMessage = WSFileManagerCMS.DownloadFile(strAuthenData, strGuid, ref objDownloadFile);
                if (CheckError.CheckMissSession((int)objResultMessage.ErrorType, true))
                {
                    RDAuthorize.SetUnhandshake(WSHostNameCMSWebContent);
                    strAuthenData = RDAuthorize.GetAuthenData(WSHostNameCMSWebContent);
                    strGuid = RDAuthorize.GetGUID(WSHostNameCMSWebContent);
                    objResultMessage = WSFileManagerCMS.DownloadFile(strAuthenData, strGuid, ref objDownloadFile);
                }

                Library.WebCore.WebExecuteLog.LogExecuteTime(strGuid, strLogID, "InsertPrintLog", "CZ_CITIZEN_PRINTLOG", RDAuthorize.Username, dtFromTime);
            }
            catch (System.Exception objEx)
            {
                Library.Common.SSOTransac_WSError.ResultMessage objResultMessageS = new Library.Common.SSOTransac_WSError.ResultMessage();
                objResultMessageS.IsError = true;
                objResultMessageS.Message = objEx.Source;
                objResultMessageS.MessageDetail = objResultMessage.MessageDetail;
                Library.Common.WriteErrorLog.Current.WriteLog(objResultMessageS, "DownloadFileCMSWebContent ---> PLCFilemanager");
            }
            return objResultMessage;
        }
    }
}
