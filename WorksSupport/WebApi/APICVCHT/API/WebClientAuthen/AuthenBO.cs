using System;
namespace WebClientAuthen
{
    [Serializable]
    public class AuthenBO
    {
        public AuthenBO() { }

        private bool bolIsPreHandShake = false;
        private bool bolIsHandShake = false;
        private String strGUID = string.Empty;
        private String strAutheData = String.Empty;
        private String strEncryptKey = String.Empty;
        private double dbUnEventTime = 0;
        private System.Net.CookieContainer objCookieContainer = null;
        private String strErrorMessage = String.Empty;

        public System.Net.CookieContainer CookieContainer
        {
            get
            {
                return this.objCookieContainer;
            }
            set { this.objCookieContainer = value; }
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
        public String StrAutheData
        {
            get { return strAutheData; }
            set { strAutheData = value; }
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

        public String ErrorMessage
        {
            get { return strErrorMessage; }
            set { strErrorMessage = value; }
        }

        #region LazyLoad
        static AuthenBO instance;
        public static AuthenBO Current
        {
            get
            {
                return instance ?? (instance = new AuthenBO());
            }
        }
        #endregion
    }
}
