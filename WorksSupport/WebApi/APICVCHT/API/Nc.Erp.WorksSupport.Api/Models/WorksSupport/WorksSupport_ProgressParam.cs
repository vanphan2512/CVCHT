using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nc.Erp.WorksSupport.Api.Models.WorksSupport
{
    public class WorksSupport_ProgressParam
    {
        public string strProgressID { get; set; }
        public int intWorksSupportID { get; set; }
        public int intProgressValue { get; set; }
        public int intWorksSupportStatusID { get; set; }
        public string strUpdatedUser { get; set; }
        public DateTime? dtmUpdatedDate { get; set; }
        public int intLogType { get; set; }
        public string strUserHostAddress { get; set; }
        public string strCertificateString { get; set; }
        public string strLoginLogID { get; set; }
    }
}