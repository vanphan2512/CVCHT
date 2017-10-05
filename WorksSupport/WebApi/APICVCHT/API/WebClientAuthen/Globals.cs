using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WebLibs.Cached;

namespace WebClientAuthen
{
    public class Globals
    {
        static string _LST_SV_HOST_CACHED_KEY_ = ConfigurationManager.AppSettings["_LST_SV_HOST_CACHED_KEY_"];
        static int intCacheTimerHourWebserviceshost = Convert.ToInt32(ConfigurationManager.AppSettings["CacheTimerHourWebserviceshost"]);
        private static object _lock = new object();

        public static WebServiceHost GetWebServiceHost(String strHostName)
        {
            try
            {
                Hashtable hstbWebServiceHostList = LoadWebServiceHostList2();
                if (hstbWebServiceHostList != null && hstbWebServiceHostList.ContainsKey(strHostName))
                {
                    WebServiceHost ws = CacheHelper.iCached.Get<WebServiceHost>(strHostName);
                    if (ws != null)
                    {
                        return ws;
                    }
                    else
                    {
                        CacheHelper.iCached.Set<WebServiceHost>(strHostName, hstbWebServiceHostList[strHostName] as WebServiceHost, 1440);
                        // HttpContext.Current.Session[strHostName] = hstbWebServiceHostList[strHostName];
                        return (hstbWebServiceHostList[strHostName] as WebServiceHost);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                string dtmlog6 = "myhost11_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond + ".txt";
                string strPath6 = "D:\\Projects\\Sources ProErp2015\\ProERP2015\\ProERP2015\\Content\\Files\\FilesContent\\" + dtmlog6;
                string[] lines6 = { ex.Message, ex.ToString() };
                System.IO.File.WriteAllLines(strPath6, lines6);
                return null;
            }
        }
        //public static WebServiceHost GetWebServiceHost(String strHostName)
        //{
        //    hstbWebServiceHostList = LoadWebServiceHostList2();
        //    if (hstbWebServiceHostList != null && hstbWebServiceHostList.ContainsKey(strHostName))
        //        return (hstbWebServiceHostList[strHostName] as WebServiceHost);

        //    return null;
        //}

        //public static void SetWebServiceHost(WebServiceHost objWebServiceHost, bool bolUpdateCached)
        //{
        //    if (hstbWebServiceHostList != null && hstbWebServiceHostList.Contains(objWebServiceHost.HostName))
        //        hstbWebServiceHostList[objWebServiceHost.HostName] = objWebServiceHost;
        //    else
        //    {
        //        hstbWebServiceHostList = hstbWebServiceHostList == null ? new Hashtable() : hstbWebServiceHostList;
        //        hstbWebServiceHostList.Add(objWebServiceHost.HostName, objWebServiceHost);
        //    }

        //    if (bolUpdateCached)
        //        SaveWSHostToCached();
        //}

        public static Hashtable LoadWebServiceHostList2()
        {
            try
            {
                Hashtable hstbWSHCached = CacheHelper.iCached.Get<Hashtable>(_LST_SV_HOST_CACHED_KEY_);

                if (hstbWSHCached != null && hstbWSHCached.Count > 0)
                    return hstbWSHCached;

                hstbWSHCached = new Hashtable();

                SSOService.WSWebServiceHost.WSWebServiceHost objWSWebServiceHostProxy =
                    new SSOService.WSWebServiceHost.WSWebServiceHost();
                objWSWebServiceHostProxy.Url = ConfigurationManager.AppSettings["WSHost_SSOService9P"] + "WSWebServiceHost.asmx";
             
                objWSWebServiceHostProxy.EnableDecompression = true;
                //DataTable tblWebServiceHost = objWSWebServiceHostProxy.GetWebServiceHostList();
                DataTable tblWebServiceHost = null;

                tblWebServiceHost = objWSWebServiceHostProxy.GetWebServiceHostList();
                //if (objResultMessage != null && objResultMessage.IsError)
                //{
                //    Library.WebCore.FileLogger.LogAction("GetWebServiceHostListForWeb Message:  " + objResultMessage.Message + " MessageDetail: " + objResultMessage.MessageDetail);
                //}

                if (tblWebServiceHost != null)
                    foreach (DataRow objRow in tblWebServiceHost.Rows)
                    {
                        WebServiceHost objWebServiceHost = new WebServiceHost(
                            Convert.ToString(objRow["WEBSERVICEHOSTID"]).Trim(),
                            Convert.ToString(objRow["HOSTURL"]).Trim(),
                            Convert.ToBoolean(objRow["ISPREHANDSHAKE"]));

                        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
                    }

                if (hstbWSHCached.Count > 0)
                {
                    CacheHelper.iCached.Set<Hashtable>(_LST_SV_HOST_CACHED_KEY_, hstbWSHCached, intCacheTimerHourWebserviceshost);
                }
               
                return hstbWSHCached;
            }
            catch (Exception ex)
            {
                Library.WebCore.FileLogger.LogAction("LoadWebServiceHostList2 ex:  " + ex.ToString());
                Library.WebCore.FileLogger.LogAction("LoadWebServiceHostList2 ex StackTrace:  " + ex.StackTrace);
                throw;
            }
        }

        //public static Hashtable LoadWebServiceHostList2()
        //{
        //    try
        //    {
        //        Hashtable hstbWSHCached = CacheHelper.iCached.Get<Hashtable>(_LST_SV_HOST_CACHED_KEY_);

        //        if (hstbWSHCached != null)
        //            return hstbWSHCached;

        //        hstbWSHCached = new Hashtable();

        //        WebServiceHost objWebServiceHost = new WebServiceHost("WSHost_SAMSPublicServiceServices", "http://samspublicserviceservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSPublicServiceProduct", "http://samsregistrypriceservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSSysUserServices", "http://samssysuserservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSMasterDataServicesWeb", "http://samsmasterdataservicesweb.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_EOfficeMasterDataServices9P", "http://samseofficemasterdataservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        WebServiceHost objWebServiceHost1 = new WebServiceHost("WSHost_ERPOfficeService9P", "http://samseofficeservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost1.HostName, objWebServiceHost1);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSEduServices", "http://samseduservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SignalRNotificationServices", "http://samsnotificationservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSOCRCaptureServices", "http://samsocrcaptureservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSEDocumentServices", "http://samsedocumentservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSCivilStatusServices", "http://samcivilstatusservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSInternalGatewayServices", "http://samsinternalgatewayservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SSOService9P", "http://ssoservice.hnegov.vn/", true);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_ERPMasterDataServices", "http://samsmasterdataservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_ERPTransactionServices", "http://erptransactionservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_ERPReportServices", "http://erpreportservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_ERPSMSServices", "http://erpsmsservices.nc.com/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSFMSServices", "http://samsfmsservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSReportServices", "http://samsreportservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSOeServices", "http://samsoeservice.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSFamilyRegisterServices", "http://samsfamilyregisterservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSOperationServices", "http://samsoperationservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSTransactionServices", "http://samstransactionservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSMasterDataServices", "http://samsmasterdataservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSEgovermentServices", "http://smasegovermentservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSCitizenCacheServices", "http://samscitizencacheservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSEOfficeWebservices", "http://sameofficewebservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSPassportRegisterServices", "http://samspassportregisterservices.cahn.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSResideBookCacheServices", "http://samsresidebookcacheservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSResideBookServices", "http://samsresidebookservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSEgovermentMemberServices", "http://samsegovermentmemberservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        objWebServiceHost = new WebServiceHost("WSHost_SAMSLisaServices", "http://samslisaservices.hnegov.vn/", false);
        //        hstbWSHCached.Add(objWebServiceHost.HostName, objWebServiceHost);
        //        CacheHelper.iCached.Set<Hashtable>(_LST_SV_HOST_CACHED_KEY_, hstbWSHCached, intCacheTimerHourWebserviceshost);
        //        return hstbWSHCached;
        //    }
        //    catch (Exception ex)
        //    {
        //        Library.WebCore.FileLogger.LogAction("LoadWebServiceHostList2 ex:  " + ex.ToString());
        //        Library.WebCore.FileLogger.LogAction("LoadWebServiceHostList2 ex StackTrace:  " + ex.StackTrace);
        //        throw;
        //    }
        //}

        //static bool SaveWSHostToCached()
        //{
        //    try
        //    {
        //        //lock (_lock)
        //        {
        //            return CacheHelper.iCached.Set<Hashtable>(_LST_SV_HOST_CACHED_KEY_, hstbWebServiceHostList, intCacheTimerHourWebserviceshost);
        //        }
        //    }
        //    catch (Exception objEx)
        //    {
        //        Library.WebCore.FileLogger.LogAction("Globals SaveWSHostToCached Ex: " + objEx.Message);
        //        return false;
        //    }
        //}

        static Hashtable GetWSHostFromCached()
        {
            try
            {
                //lock (_lock)
                {
                    return CacheHelper.iCached.Get<Hashtable>(_LST_SV_HOST_CACHED_KEY_);
                }
            }
            catch (Exception objEx)
            {
                Library.WebCore.FileLogger.LogAction("Globals GetWSHostFromCached Ex: " + objEx.Message);
                return null;
            }
        }
    }
}
