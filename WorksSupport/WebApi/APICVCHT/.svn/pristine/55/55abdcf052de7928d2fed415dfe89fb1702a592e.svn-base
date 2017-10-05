using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
namespace Library.Common
{
    public class DateTimeProcessor
    {
        public static void InitDDLNgayThangNam(DropDownList DDLNgay, DropDownList DDLThang, DropDownList DDLNam)
        {
            int i = 0;
            string strValue = "";
            string strText = "";
            // Ngày
            DDLNgay.Items.Add(new ListItem("---- Chọn ----", "-1"));
            for (i = 1; i <= 31; i++)
            {
                strValue = (i < 10) ? "0" + i.ToString() : i.ToString();
                strText = strValue;
                DDLNgay.Items.Add(new ListItem(strText, strValue));
            }

            // Tháng
            DDLThang.Items.Add(new ListItem("---- Chọn ----", "-1"));
            for (i = 1; i <= 12; i++)
            {
                strValue = (i < 10) ? "0" + i.ToString() : i.ToString();
                strText = strValue;
                DDLThang.Items.Add(new ListItem(strText, strValue));
            }

            // Năm
            DDLNam.Items.Add(new ListItem("---- Chọn ----", "-1"));
            int NamTu = DateTime.Now.Year - 10;
            int NamDen = DateTime.Now.Year + 10;
            for (i = NamTu; i <= NamDen; i++)
            {
                strValue = i.ToString();
                strText = strValue;
                DDLNam.Items.Add(new ListItem(strText, strValue));
            }
        }

        public static DateTime? BuildDateTime_TuNgay_DenNgay(int Ngay, int Thang, int Nam, bool IsStartDate)
        {
            if (Ngay == -1 && Thang == -1 && Nam == -1) return null;
            DateTime dt;

            if (Ngay == -1 && Thang == -1) // Chỉ lấy năm
            {
                if (IsStartDate)
                {
                    dt = new DateTime(Nam, 1, 1);
                    return new DateTime?(dt);
                }
                else
                {
                    dt = new DateTime(Nam, 12, 31);
                    return new DateTime?(dt);
                }
            }

            if (Ngay == -1) // Lấy tháng và năm
            {
                if (IsStartDate)
                {
                    dt = new DateTime(Nam, Thang, 1);
                    return new DateTime?(dt);
                }
                else
                {
                    dt = new DateTime(Nam, Thang, DateTime.DaysInMonth(Nam, Thang));
                    return new DateTime?(dt);
                }
            }

            // orther wise, lấy ngày tháng năm
            return new DateTime(Nam, Thang, Ngay);
        }

        public static DateTime SubtractOneYear(DateTime dt)
        {
            return new DateTime(dt.Year - 1, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }

        public static DateTime SubtractOneMonth(DateTime dt)
        {
            if (dt.Month == 1)
            {
                return new DateTime(dt.Year - 1, 12, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
            }

            return new DateTime(dt.Year, dt.Month - 1, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }

        public static DateTime SubtractMonth(DateTime dt, int NumberMonth)
        {
            for (int i = 0; i < NumberMonth; i++)
            {
                dt = SubtractOneMonth(dt);
            }
            return dt;
        }

        public static string DateToString(DateTime date, string format)
        {
            string str = "";
            if (date != new DateTime()) str = date.ToString(format);
            return str;
        }
        public static string DateToString(DateTime date, string format, DateTime strdate)
        {
            string str = "";
            if (date != new DateTime()) str = date.ToString(format);
            else str = strdate.ToString(format);
            return str;
        }
        public static DateTime FromDate(DateTime date)
        {
            if (date == new DateTime()) date = new DateTime(1750, 1, 1, 0, 0, 0, 0);
            else date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            return date;
        }
        public static DateTime ToDate(DateTime date)
        {
            if (date == new DateTime()) date = DateTime.Now;
            else date = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            return date;
        }
        public static DateTime FromDate(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, CommonVariable.strFormmatDatetime, null);
            if (date == new DateTime()) date = new DateTime(1750, 1, 1, 0, 0, 0, 0);
            else date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            return date;
        }
        public static DateTime FromTime(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, CommonVariable.strFormmatDatetime, null);
            if (date == new DateTime()) date = new DateTime(1750, 1, 1, 0, 0, 0, 0);
            else date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            return date;
        }
        /// <summary>
        /// chính bảo add function FromTimeV1
        /// date 23/04/2016
        /// </summary>
        /// <param name="sdate"></param>
        /// <returns></returns>
        public static DateTime FromTimeV1(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, CommonVariable.strFormmatHoursFull, null);
            if (date == new DateTime()) date = new DateTime(1750, 1, 1, 0, 0, 0, 0);
            else date = new DateTime(0, 0,0, date.Hour, date.Minute, 0, 0);
            return date;
        }
        public static DateTime FromDateS(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, CommonVariable.strFormmatDatetimeS, CultureInfo.InvariantCulture);
            return date;
        }
        public static DateTime ToDate(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, CommonVariable.strFormmatDatetime, null);
            if (date == new DateTime()) date = DateTime.Now;
            else date = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            return date;
        }
        public static string GetDayofWeek(DateTime date)
        {
            switch (date.DayOfWeek.ToString())
            {
                case "Monday": return "Hai";
                case "Tuesday": return "Ba";
                case "Wednesday": return "Tư";
                case "Thursday": return "Năm";
                case "Friday": return "Sáu";
                case "Saturday": return "Bảy";
                default: return "CN";
            }
        }
        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        public static string GetFileName(string strFileName)
        {
            int intStart = strFileName.LastIndexOf("\\");
            int intEnd = strFileName.LastIndexOf(".");
            return strFileName.Substring(intStart, intEnd - intStart).Replace("\\", "");
        }

        public static bool SaveData(string FileName, byte[] Data)
        {
            BinaryWriter Writer = null;
            string Name = FileName;
            try
            {
                // Create a new stream to write to the file
                Writer = new BinaryWriter(System.IO.File.OpenWrite(Name));

                // Writer raw data                
                Writer.Write(Data);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// dd/mm/yyyy
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime? dt)
        {
            if (dt != null)
                return String.Format("{0:dd/MM/yyyy}", dt);
            return "";
        }

        public static string FormatDateComment(DateTime? dt)
        {
            if (dt != null)
            {
                if (dt.Value.Day == DateTime.Now.Day)
                    return String.Format("{0:T}", dt);
                else return String.Format("{0:d}", dt);
            }
            else return "";
        }
        public static string FormatDateTime(DateTime? dt)
        {
            if (dt != null)
                return String.Format("{0:g}", dt);
            return "";
        }
        /// <summary>
        /// hh:mm
        /// </summary>
        public static string FormatTimeHHMM(DateTime? dt)
        {
            if (dt != null)
            {
                if (dt.Value.Day == DateTime.Now.Day)
                    return String.Format("{0:t}", dt);
                else return String.Format("{0:d}", dt);
            }
            return "";
        }
        public static string FormatTimeAllDay(DateTime? dtFrom, DateTime? dtEnd)
        {
            string rs = "";
            if (dtFrom != null && dtEnd != null)
            {
                if (dtFrom.Value.Day < DateTime.Now.Day)
                    rs = "00:00";
                else rs = String.Format("{0:t}", dtFrom);

                if (dtEnd.Value.Day <= DateTime.Now.Day)
                    rs += " - " + String.Format("{0:t}", dtEnd);
                else rs += " - 23:59";

            }
            return rs;
        }

        public static string FormatTimeMonthYear(DateTime? dtFrom, DateTime? dtEnd)
        {
            string rs = "";
            if (dtFrom != null && dtEnd != null)
            {
                rs = String.Format("{0:dd/MM}", dtFrom);
                rs += " - " + String.Format("{0:dd/MM}", dtEnd);
            }
            return rs;
        }

        public static DateTime? DateTimeParseFormat(string strDate, DateTime? dtmReturnDefaul)
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            if (!string.IsNullOrEmpty(strDate))
            {
                dtmReturnDefaul = Convert.ToDateTime(strDate, dateInfo);
            }
            return dtmReturnDefaul;
        }

        public static DateTime FromFullTimeInform(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, CommonVariable.strFormmatDateFull, null);
            if (date == new DateTime()) date = new DateTime(1750, 1, 1, 0, 0, 0, 0);
            else date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            return date;
        }

        public static DateTime FromFullTimeStamp(string sdate)
        {
            DateTime date = DateTime.ParseExact(sdate, CommonVariable.strFormmatDateFull, null);
            if (date == new DateTime()) date = new DateTime(1750, 1, 1, 0, 0, 0, 0);
            else date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            return date;
        }
    }
}
