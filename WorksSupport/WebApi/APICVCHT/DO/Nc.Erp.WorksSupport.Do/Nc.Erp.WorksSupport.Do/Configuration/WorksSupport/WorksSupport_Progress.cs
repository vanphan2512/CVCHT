using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc.Erp.WorksSupport.Do.Configuration.WorksSupport
{
    public class WorksSupport_Progress
    {
       #region Member Variables

		private string strProgressID = string.Empty;
		private int intWorksSupportID = 0;
		private int intProgressValue = 0;
		private int intWorksSupportStatusID = 0;
		private string strUpdatedUser = string.Empty;
		private DateTime? dtmUpdatedDate;
		private int intLogType = 0;
		private string strUserHostAddress = string.Empty;
		private string strCertificateString = string.Empty;
		private string strLoginLogID = string.Empty;

		#endregion


		#region Properties 

		/// <summary>
		/// ProgressID
		/// sys_guid()
		/// </summary>
		public string ProgressID
		{
			get { return  strProgressID; }
			set { strProgressID = value; }
		}

		/// <summary>
		/// WorksSupportID
		/// 
		/// </summary>
		public int WorksSupportID
		{
			get { return  intWorksSupportID; }
			set { intWorksSupportID = value; }
		}

		/// <summary>
		/// ProgressValue
		/// Giá trị tiến độ
		/// </summary>
		public int ProgressValue
		{
			get { return  intProgressValue; }
			set { intProgressValue = value; }
		}

		/// <summary>
		/// WorksSupportStatusID
		/// Trạng thái công việc cần hỗ trợ
		/// </summary>
		public int WorksSupportStatusID
		{
			get { return  intWorksSupportStatusID; }
			set { intWorksSupportStatusID = value; }
		}

		/// <summary>
		/// UpdatedUser
		/// 
		/// </summary>
		public string UpdatedUser
		{
			get { return  strUpdatedUser; }
			set { strUpdatedUser = value; }
		}

		/// <summary>
		/// UpdatedDate
		/// 
		/// </summary>
		public DateTime? UpdatedDate
		{
			get { return  dtmUpdatedDate; }
			set { dtmUpdatedDate = value; }
		}

		/// <summary>
		/// LogType
		/// 
		/// </summary>
		public int LogType
		{
			get { return  intLogType; }
			set { intLogType = value; }
		}

		/// <summary>
		/// UserHostAddress
		/// 
		/// </summary>
		public string UserHostAddress
		{
			get { return  strUserHostAddress; }
			set { strUserHostAddress = value; }
		}

		/// <summary>
		/// CertificateString
		/// 
		/// </summary>
		public string CertificateString
		{
			get { return  strCertificateString; }
			set { strCertificateString = value; }
		}

		/// <summary>
		/// LoginLogID
		/// 
		/// </summary>
		public string LoginLogID
		{
			get { return  strLoginLogID; }
			set { strLoginLogID = value; }
		}


		#endregion			
		
		
		#region Constructor

		public WorksSupport_Progress()
		{
		}
		#endregion


		#region Column Names

		public const String colProgressID = "ProgressID";
		public const String colWorksSupportID = "WorksSupportID";
		public const String colProgressValue = "ProgressValue";
		public const String colWorksSupportStatusID = "WorksSupportStatusID";
		public const String colUpdatedUser = "UpdatedUser";
		public const String colUpdatedDate = "UpdatedDate";
		public const String colLogType = "LogType";
		public const String colUserHostAddress = "UserHostAddress";
		public const String colCertificateString = "CertificateString";
		public const String colLoginLogID = "LoginLogID";

		#endregion //Column names
    }
}
