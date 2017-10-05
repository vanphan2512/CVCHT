﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using WebLibs.Cached;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using System.Web.Mvc;
using System.Net;
using WebClientAuthen;

using Microsoft.Office.Core;
using Vpdt.ManagerKeyCache;
namespace Library.Common
{
    public class Common
    {
        SSOTransac_WSError.ResultMessage objResultMessageError = new SSOTransac_WSError.ResultMessage();
        public StringBuilder GenHTMLUserTable(System.Data.DataTable dtb)
        {
            StringBuilder strBOBj = new StringBuilder();
            if (dtb != null && dtb.Rows.Count > 0)
            {


                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    strBOBj.Append(@"<tr><td><input type='checkbox' name='chbDataUser' data-name='" + dtb.Rows[i]["FULLNAME"] + "' data-username='" + dtb.Rows[i]["USERNAME"] + "'></td>"
                          + "<td>" + dtb.Rows[i]["USERNAME"] + "</td>"
                          + "<td><a href='/nhan-vien/thong-tin?username=" + dtb.Rows[i]["USERNAME"] + "' class='postuserinfo'>" + dtb.Rows[i]["FULLNAME"] + "</a></td>"
                          + "<td>" + dtb.Rows[i]["DEPARTMENTNAME"] + "</td>"
                          + "<td>" + dtb.Rows[i]["POSITIONNAME"] + "</td></tr>");
                }
            }
            else
            {
                strBOBj.Append(@"<tr><td colspan='5'><span style='text-align:center;'><h5>Không tìm thấy dữ liệu.</h5></span></td></tr>");
            }

            return strBOBj;
        }
        public StringBuilder GenHTMLUserTableS(System.Data.DataTable dtb)
        {
            StringBuilder strBOBj = new StringBuilder();
            if (dtb != null && dtb.Rows.Count > 0)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    strBOBj.Append(@"<tr data-id='" + dtb.Rows[i]["USERNAME"] + "'><td><img src='" + GetAvatar(dtb.Rows[i]["DEFAULTPICTUREURL"].ToString()) + "' style='width:50px;height:40px;' /></td>"
                          + "<td>" + dtb.Rows[i]["USERNAME"] + "</td>"
                          + "<td><a href='/nhan-vien/thong-tin?username=" + dtb.Rows[i]["USERNAME"] + "' class='postuserinfo'>" + dtb.Rows[i]["FULLNAME"] + "</a></td>"
                          + "<td>" + dtb.Rows[i]["DEPARTMENTNAME"] + "</td>"
                          + "<td>" + dtb.Rows[i]["POSITIONNAME"] + "</td>"
                          + "<td>" + dtb.Rows[i]["MOBI"] + "</td>"
                          + "<td>" + dtb.Rows[i]["EMAIL"] + "</td></tr>");
                }
            }
            else
            {
                strBOBj.Append(@"<tr><td colspan='7'><span style='text-align:center;'><h5>Không tìm thấy dữ liệu.</h5></span></td></tr>");
            }

            return strBOBj;
        }

        public System.Data.DataTable GetDataTableUser(System.Data.DataTable dtb, string keyName, string baseWith, string department, string position)
        {

            string strFilter = "(ISACTIVE = 1) ";
            if (!(string.IsNullOrEmpty(department) || department == "-1"))
            {
                strFilter += "and ( DEPARTMENTID in (" + department + "))";
            }
            if (!string.IsNullOrEmpty(position))
            {
                strFilter += "and (" + position + "= '0' or " + position + "= '-1' or POSITIONID = " + position + ")";
            }

            if (!string.IsNullOrEmpty(keyName.Trim()))
            {
                strFilter += "and (1 = 0 ";
                if (baseWith == "0" || baseWith == "1")
                    strFilter += " or USERNAME like '%" + keyName.Trim() + "%'";
                if (baseWith == "0" || baseWith == "2")
                    strFilter += " or FULLNAME like '%" + keyName.Trim() + "%'";
                if (baseWith == "0" || baseWith == "3")
                    strFilter += " or PHONENUMBER like '%" + keyName.Trim() + "%' or MOBI like '%" + keyName.Trim() + "%'";
                strFilter += ")  ";
            }
            dtb.DefaultView.RowFilter = strFilter;
            return dtb.DefaultView.ToTable();
        }

        public bool CheckArray(string[] strArray, string e)
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == e)
                {
                    return false;
                }
            }
            return true;
        }

        private string GetExtension(string strFile)
        {
            var ext = Path.GetExtension(strFile).ToLower();
            if (ext == ".doc" || ext == ".docx")
            {
                return "file_word.png";
            }
            else if (ext == ".ppt" || ext == ".pptx")
            {
                return "file_powerpoint.png";
            }
            else if (ext == ".xls" || ext == ".xlsx")
            {
                return "file_excel.png";
            }
            else if (ext == ".pdf")
            {
                return "file_pdf.png";
            }
            else
                return "file_picture.png";
        }
        public string AvatarUrl(string Pic)
        {
            return (GetAvatar(Pic));
        }


        #region GenHTMLTimeOffs

        /// <summary>
        /// Author          : CuRin
        /// Date created    : Dece 12, 2013
        /// Description     : 
        /// </summary>
        public StringBuilder GenHTMLTimeOffsType(System.Data.DataTable dtb, System.Data.DataTable dtbRiviewUser, int date)
        {

            StringBuilder strBOBj = new StringBuilder();
            if (dtb != null && dtb.Rows.Count > 0 && dtbRiviewUser != null && dtbRiviewUser.Rows.Count > 0)
            {
                var dt = dtb.Select("", "REVIEWORDERINDEX ASC");
                strBOBj.Append(@"<table class='table table-striped table-bordered table-hover'>
                        <thead>
                            <tr style='border: 1px solid #ACACAC; background-color: #EAEAEA;'>
                                <th style='text-align: center;'>Thứ tự duyệt</th>
                                <th style='text-align: center;'>Mức duyệt</th>
                                <th style='text-align: center;'>Nhân viên duyệt</th>
                            </tr>
                        </thead><tbody id='tbdUser' style='border: 1px solid #acacac;'>");
                for (int i = 0; i < dt.Count(); i++)
                {
                    var b = Convert.ToInt32(dt[i]["MINTIMEOFFDAYSNEEDREVIEW"]);
                    // var intDepartmentId = Convert.ToInt32(dtb.Select("'"+dtb.Rows[i]["TIMEOFFREVIEWLEVELID"]+"'")[0]["TIMEOFFREVIEWLEVELID"]);

                    if (date >= b)
                    {
                        var idType = dt[i]["TIMEOFFREVIEWLEVELID"];

                        DataRow[] dtrRiviewUserId = dtbRiviewUser.Select("TIMEOFFREVIEWLEVELID=" + idType);
                        strBOBj.Append(@"<tr >"
                                       + @"<td style='text-align: center;'>" + (i + 1) + "</td>"
                                       + @"<input   type='hidden' name='dtpInForm'  value='" + dt[i]["TIMEOFFREVIEWLEVELID"] + "'/>"
                                       + @"<td style='text-align: center;'>" + dt[i]["TIMEOFFREVIEWLEVELNAME"] + "</td>");
                        strBOBj.Append(@"<td style='text-align: center;'><div class='selectUser'><select style='margin: 5px;' >");
                        foreach (DataRow objRow in dtrRiviewUserId)
                        {
                            if (RDAuthorize.Username != objRow["USERNAME"].ToString() && (Convert.ToInt16(objRow["maxtimeoffdayscanreview"]) >= date || i < dt.Count() - 1))
                            {
                                if (objRow["ISDEFAULT"].ToString() == "1")
                                {
                                    strBOBj.Append(" <option value=\"" + objRow["USERNAME"] + "\" Selected> " + objRow["FULLNAME"] + " </option>");
                                }
                                else
                                {
                                    strBOBj.Append(" <option value=\"" + objRow["USERNAME"] + "\"> " + objRow["FULLNAME"] + " </option>");
                                }
                            }
                        }
                        strBOBj.Append(" </select></div></td>"
                                     + "</tr>");
                    }
                }
                strBOBj.Append(@"</tbody></table>");
            }
            else
            {
                strBOBj.Append(@"<h4>Không có mức duyệt</h4>");
            }
            return strBOBj;
        }
        #endregion

        public static bool GenTable<T>(List<T> objLst)
        {
            foreach (var item in objLst)
            {
                //item.
            }
            return true;
        }

        #region View render
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DatetimeRender(DateTime? date)
        {
            if (date.HasValue)
            {
                if (date.Value.Date == DateTime.Now.Date)
                {
                    return "Hôm nay - " + date.Value.ToString("HH:mm");
                }
                else
                {
                    return date.Value.ToString("dd/MM/yyyy HH:mm");
                }
            }
            return "Unknown";
        }

        /// <summary>
        /// 
        /// </summary>hoc.lenho
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DatetimeRenderdetail(DateTime? date)
        {
            if (date != null)
                if (date.HasValue)
                {
                    if (date.Value.Date == DateTime.Now.Date)
                    {
                        return "<span class='datecreate'>" + date.Value.ToString("HH:mm") + "</span>";
                    }
                    else
                    {
                        return date.Value.ToString("dd/MM/yyyy");
                    }
                }
            return "Unknown";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            input = HttpUtility.HtmlDecode(input);
            Regex regex = new Regex(@"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", RegexOptions.Singleline);
            input = regex.Replace(input, " ");
            if (input.Length > length)
                return input.Substring(0, length) + " ...";
            return input;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string NOT_FOUND_URL
        {
            get
            {
                return "/error/err_not_found?refer=" + HttpUtility.UrlEncode(Library.WebCore.Common.CurrentUrl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string NOT_PERMISSION_URL
        {
            get
            {
                return "/error/err_permission?refer=" + HttpUtility.UrlEncode(Library.WebCore.Common.CurrentUrl);
            }
        }

        public static string DateRender(DateTime? date)
        {
            if (date.HasValue)
            {
                if (date.Value.Date == DateTime.Now.Date)
                {
                    return "Hôm nay ";
                }
                else
                {
                    return date.Value.ToString("dd/MM/yyyy HH:mm");
                }
            }
            return "Unknown";
        }

        public static string DateRenderDay(DateTime? date)
        {
            if (date.HasValue)
            {
                if (date.Value.Day == DateTime.Now.Day)
                {
                    return "<span style='color:#149591;'>Hôm nay</span>";
                }
                else
                {
                    return "<span >" + date.Value.ToString("dd/MM/yyyy") + "</span>";//style='margin-left: 22px;'
                }
            }
            return "Unknown";
        }
        #endregion

        #region Read XML
        public static List<Menu> ReadXMLFile(string strFilePath)
        {
            List<Menu> LstMenu = new List<Menu>();
            Menu objMenu = null;
            Menu objMenuSub = null;

            XmlReader reader = XmlReader.Create(strFilePath);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode rootNode = doc.SelectSingleNode("root");
            if (rootNode != null)
            {
                XmlNodeList MenuNodeList = rootNode.SelectNodes("menuitem");
                if (MenuNodeList != null && MenuNodeList.Count > 0)
                {
                    foreach (XmlNode node in MenuNodeList)
                    {
                        objMenu = new Menu();
                        objMenu.SubMenu = new List<Menu>();
                        if (node.Attributes.GetNamedItem("name") != null)
                            objMenu.Name = node.Attributes.GetNamedItem("name").Value;
                        if (node.Attributes.GetNamedItem("href") != null)
                            objMenu.Href = node.Attributes.GetNamedItem("href").Value;
                        if (node.Attributes.GetNamedItem("data-id") != null)
                            objMenu.DataId = node.Attributes.GetNamedItem("data-id").Value;
                        if (node.Attributes.GetNamedItem("icon") != null)
                            objMenu.Icon = node.Attributes.GetNamedItem("icon").Value;
                        if (node.Attributes.GetNamedItem("class") != null)
                            objMenu.Class = node.Attributes.GetNamedItem("class").Value;
                        if (node.Attributes.GetNamedItem("permission") != null)
                            objMenu.Permission = node.Attributes.GetNamedItem("permission").Value;

                        if (node.HasChildNodes)
                        {
                            XmlNodeList subMenuNodeList = node.FirstChild.SelectNodes("submenuitem");

                            if (subMenuNodeList != null && subMenuNodeList.Count > 0)
                            {
                                foreach (XmlNode nodeSub in subMenuNodeList)
                                {
                                    objMenuSub = new Menu();
                                    if (nodeSub.Attributes.GetNamedItem("name") != null)
                                        objMenuSub.Name = nodeSub.Attributes.GetNamedItem("name").Value;
                                    if (nodeSub.Attributes.GetNamedItem("href") != null)
                                        objMenuSub.Href = nodeSub.Attributes.GetNamedItem("href").Value;
                                    if (nodeSub.Attributes.GetNamedItem("data-id") != null)
                                        objMenuSub.DataId = nodeSub.Attributes.GetNamedItem("data-id").Value;
                                    if (nodeSub.Attributes.GetNamedItem("icon") != null)
                                        objMenuSub.Icon = nodeSub.Attributes.GetNamedItem("icon").Value;
                                    if (nodeSub.Attributes.GetNamedItem("class") != null)
                                        objMenuSub.Class = nodeSub.Attributes.GetNamedItem("class").Value;
                                    if (nodeSub.Attributes.GetNamedItem("permission") != null)
                                        objMenuSub.Permission = nodeSub.Attributes.GetNamedItem("permission").Value;

                                    objMenu.SubMenu.Add(objMenuSub);
                                }
                            }
                        }
                        LstMenu.Add(objMenu);
                    }
                }
            }
            return LstMenu;
        }

        public static List<Menu> ReadXMLFileRecursive(string strFilePath, string strNote)
        {
            List<Menu> LstMenu = new List<Menu>();
            Menu objMenu = null;
            Menu objMenuSub = null;

            XmlReader reader = XmlReader.Create(strFilePath);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode rootNodeTemp = doc.SelectSingleNode("main");
            XmlNode rootNode = rootNodeTemp.SelectSingleNode(strNote);
            if (rootNode != null)
            {
                XmlNodeList MenuNodeList = rootNode.SelectNodes("menuitem");
                if (MenuNodeList != null && MenuNodeList.Count > 0)
                {
                    foreach (XmlNode node in MenuNodeList)
                    {
                        objMenu = new Menu();
                        objMenu.SubMenu = new List<Menu>();
                        if (node.Attributes.GetNamedItem("name") != null)
                            objMenu.Name = node.Attributes.GetNamedItem("name").Value;
                        if (node.Attributes.GetNamedItem("href") != null)
                            objMenu.Href = node.Attributes.GetNamedItem("href").Value;
                        if (node.Attributes.GetNamedItem("data-id") != null)
                            objMenu.DataId = node.Attributes.GetNamedItem("data-id").Value;
                        if (node.Attributes.GetNamedItem("icon") != null)
                            objMenu.Icon = node.Attributes.GetNamedItem("icon").Value;
                        if (node.Attributes.GetNamedItem("class") != null)
                            objMenu.Class = node.Attributes.GetNamedItem("class").Value;
                        if (node.Attributes.GetNamedItem("permission") != null)
                            objMenu.Permission = node.Attributes.GetNamedItem("permission").Value;

                        if (node.HasChildNodes)
                        {
                            XmlNodeList subMenuNodeList = node.FirstChild.SelectNodes("submenuitem");

                            if (subMenuNodeList != null && subMenuNodeList.Count > 0)
                            {
                                foreach (XmlNode nodeSub in subMenuNodeList)
                                {
                                    objMenuSub = new Menu();
                                    if (nodeSub.Attributes.GetNamedItem("name") != null)
                                        objMenuSub.Name = nodeSub.Attributes.GetNamedItem("name").Value;
                                    if (nodeSub.Attributes.GetNamedItem("href") != null)
                                        objMenuSub.Href = nodeSub.Attributes.GetNamedItem("href").Value;
                                    if (nodeSub.Attributes.GetNamedItem("data-id") != null)
                                        objMenuSub.DataId = nodeSub.Attributes.GetNamedItem("data-id").Value;
                                    if (nodeSub.Attributes.GetNamedItem("icon") != null)
                                        objMenuSub.Icon = nodeSub.Attributes.GetNamedItem("icon").Value;
                                    if (nodeSub.Attributes.GetNamedItem("class") != null)
                                        objMenuSub.Class = nodeSub.Attributes.GetNamedItem("class").Value;
                                    if (nodeSub.Attributes.GetNamedItem("permission") != null)
                                        objMenuSub.Permission = nodeSub.Attributes.GetNamedItem("permission").Value;

                                    //Gọi sub menu cấp 3
                                    if (nodeSub.HasChildNodes)
                                    {
                                        XmlNodeList subMenuNodeList3 = nodeSub.FirstChild.SelectNodes("submenuitem");

                                        if (subMenuNodeList3 != null && subMenuNodeList3.Count > 0)
                                        {
                                            objMenuSub.SubMenu = new List<Menu>();
                                            foreach (XmlNode nodeSub3 in subMenuNodeList3)
                                            {
                                                Menu objMenuSubSub = new Menu();
                                                if (nodeSub3.Attributes.GetNamedItem("name") != null)
                                                    objMenuSubSub.Name = nodeSub3.Attributes.GetNamedItem("name").Value;
                                                if (nodeSub3.Attributes.GetNamedItem("href") != null)
                                                    objMenuSubSub.Href = nodeSub3.Attributes.GetNamedItem("href").Value;
                                                if (nodeSub3.Attributes.GetNamedItem("data-id") != null)
                                                    objMenuSubSub.DataId = nodeSub3.Attributes.GetNamedItem("data-id").Value;
                                                if (nodeSub3.Attributes.GetNamedItem("icon") != null)
                                                    objMenuSubSub.Icon = nodeSub3.Attributes.GetNamedItem("icon").Value;
                                                if (nodeSub3.Attributes.GetNamedItem("class") != null)
                                                    objMenuSubSub.Class = nodeSub3.Attributes.GetNamedItem("class").Value;
                                                if (nodeSub3.Attributes.GetNamedItem("permission") != null)
                                                    objMenuSubSub.Permission = nodeSub3.Attributes.GetNamedItem("permission").Value;

                                                objMenuSub.SubMenu.Add(objMenuSubSub);
                                            }
                                        }
                                    }

                                    objMenu.SubMenu.Add(objMenuSub);
                                }
                            }
                        }
                        LstMenu.Add(objMenu);
                    }
                }
            }
            return LstMenu;
        }
        #endregion


        public static void DownloadFile(string strFileID, int Type, ref ERPFMSServices_WSFileManager.DownloadFile objDownloadFile, string userName, string password)
        {
            string strFMSApplicationID = "";
            switch (Type)
            {
                case 1:
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_Report"];
                    break;
                case 2:
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_InForm"];
                    break;
                case 3:
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_WorkSupport"];
                    break;
                case 4:
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_News"];
                    break;
                case 5:
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_Album"];
                    break;
                case 6:
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_Document"];
                    break;
                case 7: // CMS Image Webcontent
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_WebImageContent"];
                    break;
                case 8: //Eform
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_EForm"];
                    break;
                case 9: // WorksSupport
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_WorksSupport_Document"];
                    break;
                default:
                    strFMSApplicationID = System.Configuration.ConfigurationManager.AppSettings["FMSAplication_EOffice_Report"];
                    break;
            }
            objDownloadFile = new ERPFMSServices_WSFileManager.DownloadFile(); 

            if (Type == 7)
            {
                objDownloadFile.FileID = strFileID;
                objDownloadFile.FMSApplicationID = strFMSApplicationID;
            }
            else
            {
                objDownloadFile.FileID = strFileID;
                objDownloadFile.FMSApplicationID = strFMSApplicationID;
                new PlcFileManager().DownloadFile(ref objDownloadFile, userName, password);
            }
        }

        public static string FirstWords(string input, int numOfWords)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var wordCount = 0;
            for (var index = 0; index < input.Length; index++)
            {
                if (input[index] == ' ')
                {
                    wordCount++;
                }
                if (wordCount == numOfWords)
                {
                    return input.Substring(0, index) + " ...";
                }
            }
            return input;
        }

        public static string RemoveHTML(string strHTML)
        {
            string strResult = Regex.Replace(strHTML, "<(.|\n)*?>", "");
            strResult = strResult.Replace("\n", "").Replace("\r", "").Replace("'", "");

            return HttpUtility.HtmlDecode(strResult);
        }

        public static string RemoveHTML(string strHTML, int numOfWords)
        {
            if (strHTML != null && strHTML.Length <= numOfWords)
                return RemoveHTML(strHTML);


            string strResult = Regex.Replace(strHTML, "<(.|\n)*?>", "");
            strResult = strResult.Replace("\n", "").Replace("\r", "").Replace("'", "");

            if (strResult != null && strResult.Length > numOfWords)
                strResult = strResult.Substring(0, numOfWords);

            return HttpUtility.HtmlDecode(strResult);
        }

        public static string GetAvatar(string strUrlAvatar)
        {
            if (string.IsNullOrEmpty(strUrlAvatar))
            {
                return System.Configuration.ConfigurationManager.AppSettings["UserImageDefault"];
            }

            ICached iCached = CachedFactory.Create(Library.Common.Cached.CacheDSN);
            string strCacheKey = Library.Common.CacheHelper.GenKeyCache(KeyCache.KEYCACHE_LIBRARYCOMMON_GETAVATAR, strUrlAvatar);
            string strResult = iCached.Get<string>(strCacheKey);

            if (string.IsNullOrEmpty(strResult))
            {
                strResult = (string.IsNullOrEmpty(strUrlAvatar) ? System.Configuration.ConfigurationManager.AppSettings["UserImageDefault"] : System.Configuration.ConfigurationManager.AppSettings[(strUrlAvatar.IndexOf("UserImages/") > -1) ? "UserImageHostUrl" : "WebAlbumUrl"] + strUrlAvatar);
                strResult = strResult.Replace('\\', '/');
                iCached.Set<string>(strCacheKey, strResult, Library.Common.Cached.DefaultTimeoutCached);
            }
            return strResult;
        }


        public static string GetVisitorIPAddress(bool GetLan = false)
        {
            string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan)
            {
                if (string.IsNullOrEmpty(visitorIPAddress))
                {
                    //This is for Local(LAN) Connected ID Address
                    string stringHostName = Dns.GetHostName();
                    //Get Ip Host Entry
                    IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                    //Get Ip Address From The Ip Host Entry Address List
                    IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                    try
                    {
                        visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                    }
                    catch
                    {
                        try
                        {
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            try
                            {
                                arrIpAddress = Dns.GetHostAddresses(stringHostName);
                                visitorIPAddress = arrIpAddress[0].ToString();
                            }
                            catch
                            {
                                visitorIPAddress = "127.0.0.1";
                            }
                        }
                    }
                }
            }
            return visitorIPAddress;
        }
        
        public System.Data.DataTable PagingTable(System.Data.DataTable dtbInput, int intPageIndex, int intPageSize, ref int intTotalRows)
        {
            try
            {
                if (dtbInput == null || dtbInput.Rows.Count == 0)
                    return null;
                intTotalRows = dtbInput.Rows.Count;
                if (dtbInput.Rows.Count <= intPageSize || intPageSize == -1)
                    return dtbInput;

                int intRowStart = intPageIndex * intPageSize;
                int intRowEnd = intRowStart + intPageSize;

                if (intRowStart > dtbInput.Rows.Count - 1)
                    return null;
                if (intRowEnd > dtbInput.Rows.Count - 1)
                    intRowEnd = dtbInput.Rows.Count - 1;

                System.Data.DataTable dtbOutput = dtbInput.AsEnumerable().Skip(intRowStart).Take(intPageSize).CopyToDataTable();

                return dtbOutput;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> PagingListObject<T>(IEnumerable<T> dataInput, int intPageIndex, int intPageSize, ref int intTotalRows)
        {
            try
            {
                if (dataInput == null || dataInput.Count() == 0)
                    return null;
                intTotalRows = dataInput.Count();
                if (dataInput.Count() <= intPageSize || intPageSize == -1)
                    return dataInput;

                int intRowStart = intPageIndex * intPageSize;
                int intRowEnd = intRowStart + intPageSize;

                if (intRowStart > dataInput.Count() - 1)
                    return null;
                if (intRowEnd > dataInput.Count() - 1)
                    intRowEnd = dataInput.Count() - 1;

                IEnumerable<T> dtbOutput = dataInput.Skip(intRowStart).Take(intPageSize).ToList();

                return dtbOutput;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetFileName(string strFileName)
        {
            int intStart = strFileName.LastIndexOf("\\");
            int intEnd = strFileName.LastIndexOf(".");
            return strFileName.Substring(intStart, intEnd - intStart).Replace("\\", "");
        }

        public static string GetFileNameFull(string strFileName)
        {
            int intStart = strFileName.LastIndexOf("\\");
            int intEnd = strFileName.Length;
            return strFileName.Substring(intStart, intEnd - intStart).Replace("\\", "");
        }

        public static string GetUserConfigValueByUserAndConfigID(string strUserName, string strUserConfigID, DataTable dtbInput)
        {
            string strResult = string.Empty;
            if (dtbInput != null && dtbInput.Rows.Count > 0)
            {
                DataRow[] dr = dtbInput.Select("UserName = '" + strUserName + "' AND  UserConfigID = '" + strUserConfigID + "'");
                if (dr != null && dr.Length > 0)
                    strResult = dr[0]["UserConfigValue"].ToString();
            }
            return strResult;
        }

        public DataTable OpenExcel2DataTable(String strFileName, int intMaxColCount, string[] arrColumnsCheck)
        {
            C1.C1Excel.C1XLBook objBook = new C1.C1Excel.C1XLBook();
            try
            {
                if (strFileName.Substring(strFileName.Length - 5, 5).Equals(".xlsx"))
                {
                    objBook.Load(strFileName, C1.C1Excel.FileFormat.OpenXml);
                }
                else
                {
                    objBook.Load(strFileName);
                }

                DataTable tblResult = new DataTable();
                C1.C1Excel.XLSheet objSheet = objBook.Sheets[0];

                if (intMaxColCount == 0)
                    intMaxColCount = objSheet.Columns.Count;

                for (int i = 0; i < intMaxColCount; i++)
                {
                    if (i < arrColumnsCheck.Length)
                        tblResult.Columns.Add(arrColumnsCheck[i].ToString());
                }

                for (int i = 0; i < objSheet.Rows.Count; i++)
                {
                    bool bolIsRowRull = true;
                    DataRow objRow = tblResult.NewRow();
                    for (int j = 0; j < tblResult.Columns.Count; j++)
                    {
                        objRow[j] = objSheet[i, j].Value;
                        bolIsRowRull = bolIsRowRull && Convert.IsDBNull(objRow[j]);
                    }

                    tblResult.Rows.Add(objRow);
                }
                return tblResult;
            }
            catch (Exception objExce)
            {
                throw;
            }

            return null;

        }
    }

    [Serializable]
    public class Menu
    {
        public string Name { get; set; }

        public string Href { get; set; }
        public string DataId { get; set; }

        public string Icon { get; set; }

        public string Class { get; set; }

        public string OrderIndex { get; set; }

        public List<Menu> SubMenu { get; set; }

        public string Permission { get; set; }
    }
}
