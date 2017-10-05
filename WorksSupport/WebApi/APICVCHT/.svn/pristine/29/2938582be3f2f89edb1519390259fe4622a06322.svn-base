using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Common
{
    public class CommonVariable
    {
        public static string strFormmatMoney = "#,###,###,###,###,##.##";//16 number
        public static string strFormmatNumber = "#,###,###,##0";
        public static string strFormmatDatetime = "dd/MM/yyyy";
        public static string strFormmatDatetimeS = "dd/MM/yyyy hh:mm";
        public static string strFormmatDateFull = "dd/MM/yyyy HH:mm";
        public static string strFormmatHoursFull = "HH:mm";
        public static string Root
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("Host_String").ToString();
            }
        }
        public static string UserName
        {
            get
            {
                return "__UserName__";
            }
        }
        public static string SessionUser
        {
            get
            {
                return "SessionUser";
            }
        }
        public static string HstbFunction
        {
            get { return "HstbFunction"; }
        }

    }
}
