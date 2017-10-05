using System;
using System.Collections.Generic;
using System.Configuration;

using WebLibs.Cached;

namespace WebClientAuthen
{
    public static class CacheHelper
    {
        public static String GenKeyCache(params object[] args)
        {
            if (args.Length > 0)
            {
                string tmp = "9phut.com";
                foreach (var item in args)
                {
                    if (item == null)
                        tmp += "_null";
                    else
                        tmp += "_" + item.ToString().Trim();
                }

                return tmp;
            }
            return null;
        }

        static ICached objICache = null;

        public static ICached iCached
        {
            get
            {
                if (objICache == null)
                    objICache = CachedFactory.Create(Cached.CacheDSN);

                return objICache;
            }
        }

        #region Delete Cache
        public static bool ClearCachedWebMenuByType(int keyCached)
        {
            bool success = true;
            try
            {
                WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
                string strCacheKey = "K_d_UtilR_WebMenuByType_" + keyCached.ToString();
                iCached.Remove(strCacheKey);//Duc_24.06.2016_Xoa cach khi thay doi nhom quyen nguoi dung
                return success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void ClearCachedMenuWeb()
        {
            List<string> arr = new List<string>() { "K_d_HomeC_GenMenuHomeSite_site-menu-menusite", "K_d_HomeC_GenMenuHomeSite_site-menu-menuapplesite", "K_d_HomeC_GenMultiMenu_site-menu-menuhomeios", "K_d_HomeC_GenMultiMenu_site-menu-menuhomeandroid", "K_d_HomeC_GenMultiMenu_site-menu-menuhomeaccessories", "K_d_HomeC_GenMultiMenu_site-menu-menucatesamsung", "K_d_HomeC_GenMultiMenu_site-menu-menucateold", "K_d_HomeC_GenMultiMenu_site-menu-menucate" };

            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("K_d_ProductR_AlbumPrd_");
            foreach (var item in arr)
            {
                iCached.Remove(item);
            }
        }

        public static void ClearCachedCMSGetAllButtonProduct()
        {
            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            string strCacheKey = "K_d_UtilR_GetAllButtonProduct";
            iCached.Remove(strCacheKey);
        }

        public static void ClearCachedCMSWebContent(string WebcontentID)
        {
            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            string strCacheKey = "K_d_UtilR_CMSWebContent_" + WebcontentID.ToString();
            iCached.Remove(strCacheKey);
        }

        public static void ClearCachedCMSGetAlbumPrd(string Productid, string type)
        {
            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            string strCacheKey = "K_d_ProductR_AlbumPrd_" + Productid + "_" + type;
            iCached.Remove(strCacheKey);
        }

        public static void ClearCachedCMSBrandsWeb()
        {
            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            string strCacheKey = "K_d_BrandR_GetBrandsWeb";
            iCached.Remove(strCacheKey);
        }

        public static void ClearCachedContentWeb()
        {
            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("K_d_ProductR_AlbumPrd_");
            iCached.Remove("K_d_HomeController_Footer_footer");
        }

        public static void ClearCachedGetAllCMSMainGroupOnly()
        {
            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            iCached.Remove("K_d_MainGroupRepository_GetAllCMSMainGroupOnly");
        }
        ///// <summary>
        ///// Xóa khi thay đổi cấu hình menu
        ///// </summary>
        ///// <param name="strKeyCache"></param>
        //public static void RemoveCacheQuickMenu(string key, string strUserName)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    string strKeyCache = "vpdt.com_" + key + "_" + strUserName;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Xóa khi thay đổi cấu hình mặc định theo user đăng nhập
        ///// </summary>
        ///// <param name="strUserName"></param>
        //public static void RemoveCacheSchoolSetting(string strUserName)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //Phân quyền quản lý trường cho user 
        //    //string strCacheKeySchool = WebClientAuthen.CacheHelper.GenKeyCache("PLCMasterData", "GetSchoolByUser", RDAuthorize.Username);
        //    string strKeyCache = "eschool.com_PLCMasterData_GetSchoolByUser_" + strUserName;
        //    icache.Remove(strKeyCache);

        //    //Lấy thông tin cấu hình mặc định của User
        //    strKeyCache = "eschool.com_PLCMasterData_GetSchoolSetting_" + strUserName;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Xóa khi phân công giáo viên bộ môn
        ///// </summary>
        ///// <param name="intSchoolID"></param>
        ///// <param name="strUserName"></param>
        ///// <param name="intSchoolYearID"></param>
        ///// <param name="intSchoolTermID"></param>
        //public static void RemoveCachePCGVBM(int intSchoolID, string strUserName, int intSchoolYearID, int intSchoolTermID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //Xóa cache liên quan phân công bộ môn
        //    //string strKeyCache = WebClientAuthen.CacheHelper.GenKeyCache("PLCSchoolClassTeacher", "GetAllTeacherSubjectBySchoolID", intSchoolID);
        //    string strKeyCache = "eschool.com_PLCSchoolClassTeacher_GetAllTeacherSubjectBySchoolID_" + intSchoolID.ToString();
        //    icache.Remove(strKeyCache);

        //    //Xóa cache khi phân công giáo viên cho một môn nào đó của một lớp để dùng cho phần fillter môn học khi nhập điểm
        //    //string strCacheKeyTeacherSubject = WebClientAuthen.CacheHelper.GenKeyCache("PLCSchoolClassTeacher", "TeacherSubject", intSchoolID, intSchoolYearID, intSchoolTermID);
        //    //string strCacheKeyTeacherSubject = "eschool.com_PLCSchoolClassTeacher_TeacherSubject_" + intSchoolID + "_" + intSchoolYearID + "_" + intSchoolTermID;
        //    string strCacheKeyTeacherSubject = "eschool.com_SCTTS_" + intSchoolID + "_" + intSchoolYearID + "_" + intSchoolTermID;
        //    icache.Remove(strCacheKeyTeacherSubject);

        //    //string strCacheKeySchoolClass = WebClientAuthen.CacheHelper.GenKeyCache("PLCMasterData", "GetSchoolClassByUserBeta", RDAuthorize.Username);
        //    string strCacheKeySchoolClass = "eschool.com_MDGSCBUB_" + strUserName;
        //    icache.Remove(strCacheKeySchoolClass);

        //    strKeyCache = "eschool.com_WSULFP_" + strUserName;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Xóa khi phân công giáo viên chủ nhiệm
        ///// </summary>
        ///// <param name="intSchoolID"></param>
        ///// <param name="intSchoolYearID"></param>
        ///// <param name="intSchoolTermID"></param>
        //public static void RemoveCachePCGVCN(int intSchoolID, int intSchoolYearID, int intSchoolTermID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //Xóa cache liên quan phân công gvcn
        //    //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("PLCSchoolClassHeadTeacher", "GetDataHeadTeacher", intSchoolID, intSchoolYearID, intSchoolTermID);
        //    string strKeyCache = "eschool.com_PLCSchoolClassHeadTeacher_GetDataHeadTeacher_" + intSchoolID + "_" + intSchoolYearID + "_" + intSchoolTermID;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Xóa key cache quyền trên lớp của giáo viên khi phân công giáo viên chủ nhiệm
        ///// </summary>
        ///// <param name="lstHeadTeacherName"></param>
        //public static void RemoveCachePCGVCNV1(List<string> lstHeadTeacherName)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //Xóa key cache quyền trên lớp
        //    if (lstHeadTeacherName != null && lstHeadTeacherName.Count > 0)
        //    {
        //        for (int i = 0; i < lstHeadTeacherName.Count; i++)
        //        {
        //            string strCacheKeySchoolClass = "eschool.com_MDGSCBUB_" + lstHeadTeacherName[i];
        //            icache.Remove(strCacheKeySchoolClass);

        //            string strKeyCache = "eschool.com_WSULFP_" + lstHeadTeacherName[i];
        //            icache.Remove(strKeyCache);
        //        }
        //    }

        //}

        ///// <summary>
        ///// Thay đổi trạng thái điểm danh
        ///// </summary>
        //public static void RemoveCacheChangStatusAttendance()
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("MasterData", "GetPupilAttendanceStatus");
        //    string strKeyCache = "eschool.com_MasterData_GetPupilAttendanceStatus";
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi khối học của trường
        ///// </summary>
        //public static void RemoveCacheChangClassOfSchool(int intSchoolID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //string strCacheKeyClass = WebClientAuthen.CacheHelper.GenKeyCache("PLCSchoolClassTeacher", "GetClassBySchoolID", intSchoolID);
        //    string strKeyCache = "eschool.com_PLCSchoolClassTeacher_GetClassBySchoolID_" + intSchoolID;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi buổi dạy
        ///// </summary>
        //public static void RemoveCacheChangeTeachingSession()
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //string strKeyCache = WebClientAuthen.CacheHelper.GenKeyCache("PLCSchoolClassTeacher", "TeachingSessionAll_Web");
        //    string strKeyCache = "eschool.com_PLCSchoolClassTeacher_TeachingSessionAll_Web";
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi lớp học, khối học của trường
        ///// </summary>
        ///// <param name="intSchoolID"></param>
        //public static void RemoveCacheChangeSchoolClass(int intSchoolID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("PLCMasterData", "GetSchoolClassBySchoolID", intSchoolID);
        //    string strKeyCache = "eschool.com_PLCMasterData_GetSchoolClassBySchoolID_" + intSchoolID;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi thời khóa biểu của trường
        ///// </summary>
        ///// <param name="intSchoolID"></param>
        //public static void RemoveCacheChangeTimeTable(int intSchoolID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("PLCTimeTable", "SearchDataTimeTableBySchool", intSchoolID);
        //    string strKeyCache = "eschool.com_PLCTimeTable_SearchDataTimeTableBySchool_" + intSchoolID;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi điểm học sinh
        ///// </summary>
        ///// <param name="intSchoolID"></param>
        //public static void RemoveCacheChangePupilMark(int intSchoolID, int intClassID, int intSchoolYearID, int intSchoolTermID, int intSubjectID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);

        //    //string strKeyCache = WebClientAuthen.CacheHelper.GenKeyCache("RPT_EDU_PP_MarkBySubject", intSchoolID, intClassID, intSchoolYearID, intSchoolTermID, intSubjectID);
        //    //string strCacheKeySchoolClass = WebClientAuthen.CacheHelper.GenKeyCache("RPT_EDU_PP_MarkBySubject", intSchoolId, intClassId, intSchoolYearId, intSchoolTermId, intSubjectId);
        //    //string strCacheKeySchoolClass = WebClientAuthen.CacheHelper.GenKeyCache("RPT_EDU_PP_MarkBySubject", intSchoolID, intSchoolYearID, intSchoolTermID, intSubjectID);
        //    string strCacheKeySchoolClass = "eschool.com_RPT_EDU_PP_MarkBySubject_" + intSchoolID + "_" + intClassID + "_" + intSchoolYearID + "_" + intSchoolTermID + "_" + intSubjectID;
        //    icache.Remove(strCacheKeySchoolClass);

        //    //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("LoadData_Pupil_Mark_CalcTermMark", intSchoolID, intSchoolYearID, intSchoolTermID);
        //    string strCacheKey = "eschool.com_LoadData_Pupil_Mark_CalcTermMark_" + intSchoolID + "_" + intSchoolYearID + "_" + intSchoolTermID;

        //    string strCacheKey1 = "eschool.com_LoadData_Pupil_Mark_CalcTermMark_" + intSchoolID + "_" + intClassID + "_" + intSchoolYearID + "_" + intSchoolTermID;

        //    icache.Remove(strCacheKey);
        //    icache.Remove(strCacheKey1);
        //}

        ///// <summary>
        ///// Thay đổi quyền cho User
        ///// </summary>
        ///// <param name="strUserName"></param>
        //public static void RemoveCacheFuction(string strUserName)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //Danh sách quyền theo user khi đăng nhập
        //    //string strKeyCache = "eschool.com_WebSessionUser_LoadFunctionPermission_" + strUserName;
        //    string strKeyCache = "vpdt.com_WSULFP_" + strUserName;
        //    icache.Remove(strKeyCache);

        //    //Lấy danh sách lớp theo User được phân quyền (lấy lớp theo quyền trên lớp)
        //    strKeyCache = "vpdt.com_MDGSCBUB_" + strUserName;
        //    icache.Remove(strKeyCache);

        //    //Lấy thông tin cấu hình mặc định của User
        //    //strKeyCache = "eschool.com_PLCMasterData_GetSchoolSetting_" + strUserName;
        //    //icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi appsetting của hệ thống
        ///// </summary>
        //public static void RemoveCacheAppSetting()
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //AppSetting 
        //    string strKeyCache = "eschool.com_PLCMasterData_GetAppSetting";
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi môn học, thuộc tính môn học của trường
        ///// </summary>
        //public static void RemoveCacheChangeSubject(int intSchoolID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //AppSetting 
        //    //string strKeyCache = WebClientAuthen.CacheHelper.GenKeyCache("PLCSchoolClassTeacher", "GetSubjectBySchoolClassWeb", intSchoolID);
        //    string strKeyCache = "eschool.com_PLCSchoolClassTeacher_GetSubjectBySchoolClassWeb_" + intSchoolID;
        //    icache.Remove(strKeyCache);
        //}

        ///// <summary>
        ///// Thay đổi điểm môn học, hoặc tính điểm TBM hoặc tổng kết
        ///// </summary>
        //public static void RemoveCacheChangePupilMarkV1(int intSchoolID, int intClassID, int intSchoolClassID, int intSchoolYearID, int intSchoolTermID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    string strKeyCache = "eschool.com_PLCReportDocumentPupil_ReportPupilMark_" + intSchoolID + "_" + intClassID + "_" + intSchoolClassID + "_" + intSchoolYearID + "_" + intSchoolTermID;
        //    icache.Remove(strKeyCache);

        //    //eschool.com_PLCRDP_RPM_{schoolid}_{schoolclassid}_{schoolyearid}_{schooltermid}
        //    strKeyCache = "eschool.com_PLCRDP_RPM_" + intSchoolID + "_" + intSchoolClassID + "_" + intSchoolYearID + "_" + intSchoolTermID;
        //    icache.Remove(strKeyCache);
        //}

        //public static void RemoveCacheChangePupilMarkByPupilID(int intSchoolYearID, int intSchoolTermID, int intSchoolClassID, string strPupilID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    string strCacheKey = "DataTablePupil_Mark" + intSchoolYearID + "_" + intSchoolTermID + "_" + intSchoolClassID + "_" + strPupilID;
        //    icache.Remove(strCacheKey);
        //}

        ////20/12/2016
        ////Bach Xuan Cuong
        ////Xoa chache cho phan nhap diem
        //public static void RemoveCacheForInputMark(int intSchoolID, int intClassID, int intSchoolClassID, int intSchoolTermID, int intSchoolYearID, int intSubjectID)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);

        //    //So cot cua mon hoc
        //    //string strCacheKeySchoolClass = WebClientAuthen.CacheHelper.GenKeyCache("PLCMasterData", "GetColumnDetailSubject", intSchoolClassID, intSchoolTermID, intSchoolYearID, intSubjectID);
        //    //string strCacheKeyColumnDetailSubject = "eschool.com_PLCMasterData_GetColumnDetailSubject_" + intSchoolClassID + "_" + intSchoolTermID + "_" + intSchoolYearID + "_" + intSubjectID;
        //    string strCacheKeyColumnDetailSubject = "eschool.com_MDCDS_" + intSchoolClassID + "_" + intSchoolTermID + "_" + intSchoolYearID + "_" + intSubjectID;

        //    //Co ke hoach nhap diem hay khong
        //    //string strCacheKeyImputPlan = WebClientAuthen.CacheHelper.GenKeyCache("PLCInputMarkPlan", "CheckExistInputMarkPlan", intSchoolId, intSchoolYearId, intSchoolTermId);
        //    string strCacheKeyIsHasInputPlan = "eschool.com_PLCInputMarkPlan_CheckExistInputMarkPlan_" + intSchoolID + "_" + intSchoolYearID + "_" + intSchoolTermID;

        //    //Danh sach ke hoach nhap diem
        //    //string strCacheKeyInputPlan = WebClientAuthen.CacheHelper.GenKeyCache("PLCMasterData", "LoadDataInputMarkPlan", intSchoolId, intSchoolYearid, intSchoolTermId, intSubjectId);
        //    string strCacheKeyInputPlan = "eschool.com_PLCMasterData_LoadDataInputMarkPlan_" + intSchoolID + "_" + intSchoolYearID + "_" + intSchoolTermID + "_" + intSubjectID;

        //    //Sau khi tinh diem trung binh
        //    string strCacheKeyMarkBySubject = "eschool.com_RPT_EDU_PP_MarkBySubject_" + intSchoolID + "_" + intClassID + "_" + intSchoolYearID + "_" + intSchoolTermID + "_" + intSubjectID;

        //    icache.Remove(strCacheKeyColumnDetailSubject);
        //    icache.Remove(strCacheKeyIsHasInputPlan);
        //    icache.Remove(strCacheKeyInputPlan);
        //    icache.Remove(strCacheKeyMarkBySubject);
        //}

        //public static void RemoveCacheChangeAllowAccessIP(string strUserName)
        //{
        //    ICached icache = CachedFactory.Create(ConfigurationManager.AppSettings["Page:Cached:SV:Remove"]);
        //    //string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("SystemPCL", "GetListAllowAccessIPByUser", strUserName);
        //    string strCacheKey = "eschool.com_SystemPCL_GetListAllowAccessIPByUser_" + strUserName;
        //    icache.Remove(strCacheKey);
        //}


        #endregion

        #region Xóa cached loại công việc cần hỗ trợ
        public static void ClearCachedWorkSupportType()
        {
            WebLibs.Cached.ICached iCached = WebLibs.Cached.CachedFactory.Create(WebClientAuthen.Cached.CacheDSN);
            string strCacheKey = WebClientAuthen.CacheHelper.GenKeyCache("PLCWorksSupport", "GetWorksSupportType_GetAll");
            iCached.Remove(strCacheKey);
        }
        #endregion
    }
    public static class Cached
    {
        static int defaultCacheTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["Page:Cached:Timeout:Default"]);
        static int maxTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["Page:Cached:Timeout:Max"]);
        static int minTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["Page:Cached:Timeout:Min"]);
        static string cachedDSN = ConfigurationManager.AppSettings["Page:Cached:SV"];

        public static int DefaultTimeoutCached
        {
            get
            {
                return defaultCacheTimeout;
            }
        }

        public static int CacheMaxtime
        {
            get
            {
                return maxTimeout;
            }
        }

        public static int CacheMintime
        {
            get
            {
                return minTimeout;
            }
        }

        public static string CacheDSN
        {
            get
            {
                return cachedDSN;
            }
        }

        public static string CacheIIS
        {
            get
            {
                return "iis";
            }
        }

    }
}
