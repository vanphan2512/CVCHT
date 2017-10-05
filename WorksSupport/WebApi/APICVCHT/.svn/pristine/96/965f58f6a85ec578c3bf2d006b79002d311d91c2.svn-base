using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebClientAuthen
{
    [Serializable]
    public class ServiceHostBO
    {
        private System.Net.CookieContainer objCookieContainer = null;
        private String strErrorMessage = "";
        private String strHostName = "";
        private String strHostURL = "";
        private bool bolIsPreHandShake = false;
        private bool bolIsHandShake = false;
        private String strGUID = "";
        private String strEncryptKey = "";
        private double dbUnEventTime = 0;//Thời gian chênh lệch giữa client và Server, tính bằng s

        public System.Net.CookieContainer CookieContainer
        {
            get
            {
                return this.objCookieContainer;
            }
            set { this.objCookieContainer = value; }
        }

        public String ErrorMessage
        {
            get { return strErrorMessage; }
            set { strErrorMessage = value; }
        }

        public String HostName
        {
            get { return strHostName; }
            set { strHostName = value; }
        }

        public String HostURL
        {
            get { return strHostURL; }
            set { strHostURL = value; }
        }


        public bool IsPreHandShake
        {
            get { return bolIsPreHandShake; }
            set { bolIsPreHandShake = value; }
        }

        public bool IsHandShake
        {
            get { return bolIsHandShake; }
            set { bolIsHandShake = value; }
        }

        public String GUID
        {
            get { return strGUID; }
            set { strGUID = value; }
        }

        public String EncryptKey
        {
            get { return strEncryptKey; }
            set { strEncryptKey = value; }
        }

        /// <summary>
        /// Thời gian chênh lệch giữa client và server, tính bằng giây
        /// </summary>
        public double UnEventTime
        {
            get { return dbUnEventTime; }
            set { dbUnEventTime = value; }
        }

        #region LazyLoad
        static ServiceHostBO instance;
        public static ServiceHostBO Current
        {
            get
            {
                return instance ?? (instance = new ServiceHostBO());
            }
        }
        #endregion

    }
}
