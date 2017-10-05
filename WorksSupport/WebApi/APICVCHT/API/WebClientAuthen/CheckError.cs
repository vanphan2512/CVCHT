using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebClientAuthen
{
    public class CheckError
    {
        public enum ErrorTypes
        {
            No_Error = 0,
            LoadInfo = 1,
            Insert = 2,
            Update = 3,
            Delete = 4,
            SearchData = 5,
            GetData = 6,
            InvalidIV = 7,
            TokenNotExist = 8,
            TokenInvalid = 9,
            CheckData = 10,
            Others = 11,
            UserDefine = 12 // Loại lỗi thông báo đến người dùng
        }
        public static bool CheckMissSession(int intErrorType, bool bolIsRetry)
        {
            return CheckMissSession(intErrorType, bolIsRetry, string.Empty, string.Empty, string.Empty);
        }

        public static bool CheckMissSession(int intErrorType, bool bolIsRetry, String strContent, String strEvent, String strModuleName)
        {
            if (intErrorType == (int)ErrorTypes.InvalidIV
                || intErrorType == (int)ErrorTypes.TokenInvalid
                    || intErrorType == (int)ErrorTypes.TokenNotExist)
            {
                return true;
            }
            return false;
        }

        public static string GetErrorTypeString(int intErrorType)
        {
            if (intErrorType == (int)ErrorTypes.InvalidIV)
                return "InvalidIV";
            else if (intErrorType == (int)ErrorTypes.TokenInvalid)
                return "TokenInvalid";
            else if (intErrorType == (int)ErrorTypes.TokenNotExist)
                return "TokenNotExist";
            return string.Empty;
        }
    }
}
