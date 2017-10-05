using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nc.Erp.WorksSupport.Api.Models.Project
{
    /// <summary>
    /// Search key param
    /// </summary>
    public class SearchKeyParam
    {
        public string Keywords { get; set; }
    }
    public class SearchKeyParamProject
    {
        public string Keywords { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}