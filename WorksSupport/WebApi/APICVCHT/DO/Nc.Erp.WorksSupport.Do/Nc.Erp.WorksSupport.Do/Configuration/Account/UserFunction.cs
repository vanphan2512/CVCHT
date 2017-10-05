using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc.Erp.WorksSupport.Do.Configuration.Account
{
    public class UserFunction
    {
        public const string colDescription = "Description";
        public const string colFunctionID = "FunctionID";
        public const string colFunctionName = "FunctionName";
        public const string colSecurityLevelId = "SecurityLevelId";

        //public UserFunction();

        public string Description { get; set; }
        public string FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int SecurityLevelId { get; set; }
    }
}
