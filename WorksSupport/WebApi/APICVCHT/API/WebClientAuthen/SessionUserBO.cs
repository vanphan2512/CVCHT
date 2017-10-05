﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebClientAuthen
{
    [Serializable]
    public class SessionUserBO
    {
        private String strDefaultPictureURL = string.Empty;
        private String strUserName = "";		// Tên đăng nhập
        private String strFullName = "";		// Họ tên
        private int intFunctionID = 0;		    // Chức vụ
        private String strFunction = "";		// Chức vụ
        private String strPassword = "";		// Mật khẩu
        private int intDepartmentID = 0;		// Mã phòng ban
        private String strDepartment = "";		// Tên phòng ban
        private string strAddress = "";		    // Địa chỉ
        private string strPhoneNumber = "";		// Số điện thoại
        private int intReviewLevelID = 0;		// Mã mức độ duyệt
        private bool bolIsLogin = false;	    // Cho biết đã đăng nhập hay chưa (true)
        private String strLoginLogID = "";      //Mã ghi nhận thông tin login tương ứng
        private int intSecurityLevelID = 0;
        private int intPositionID = 0;
        private String strPositionName = "";        

        private bool bolIsCertificate = true;
        private String strTokenString = "";

        public Hashtable hstbPermission = new Hashtable(); //Lưu danh sách các key quyền
        private DataTable tblMainGroupPermission = new DataTable();
        private DataTable tblStorePermission = new DataTable();
        private DataTable tblCompanyPermission = new DataTable();
     

        public String DefaultPictureURL
        {
            get { return strDefaultPictureURL; }
            set { strDefaultPictureURL = value; }
        }

        public String UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }

        public String Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public String FullName
        {
            get { return strFullName; }
            set { strFullName = value; }
        }

        public String Function
        {
            get { return strFunction; }
            set { strFunction = value; }
        }

        public Boolean IsLogin
        {
            get { return bolIsLogin; }
            set { bolIsLogin = value; }
        }

        public String Department
        {
            get { return strDepartment; }
            set { strDepartment = value; }
        }

        public int DepartmentID
        {
            get { return intDepartmentID; }
            set { intDepartmentID = value; }
        }

        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }

        public int FunctionID
        {
            get { return intFunctionID; }
            set { intFunctionID = value; }
        }

        public string PhoneNumber
        {
            get { return strPhoneNumber; }
            set { strPhoneNumber = value; }
        }

        public int ReviewLevelID
        {
            get { return intReviewLevelID; }
            set { intReviewLevelID = value; }
        }

        public String LoginLogID
        {
            get { return strLoginLogID; }
            set { strLoginLogID = value; }
        }

        public int SecurityLevelID
        {
            get { return intSecurityLevelID; }
            set { intSecurityLevelID = value; }
        }

        public int PositionID
        {
            get { return intPositionID; }
            set { intPositionID = value; }
        }

        public String PositionName
        {
            get { return strPositionName; }
            set { strPositionName = value; }
        }

        public bool IsCertificate
        {
            get { return bolIsCertificate; }
            set { bolIsCertificate = value; }
        }

        public String TokenString
        {
            get { return strTokenString; }
            set { strTokenString = value; }
        }

        public DataTable MainGroupPermission
        {
            get { return tblMainGroupPermission; }
            set { tblMainGroupPermission = value; }
        }

        public DataTable StorePermission
        {
            get { return tblStorePermission; }
            set { tblStorePermission = value; }
        }

        public DataTable CompanyPermission
        {
            get { return tblCompanyPermission; }
            set { tblCompanyPermission = value; }
        }


        #region LazyLoad
        static SessionUserBO instance;
        public static SessionUserBO Current
        {
            get
            {
                return instance ?? (instance = new SessionUserBO());
            }
        }
        #endregion
    }
}
