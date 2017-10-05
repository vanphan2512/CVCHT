﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc.Erp.WorksSupport.Do.Configuration
{
    /// <summary>
    /// Created by      : Lương Trung Nhân
    /// Created date    : 30/05/2017
    /// Đối tượng ERP.EO.WORKSSUPPORTQUALITY
    /// </summary>
    public class WorksSupportQuality
    {
        public int WorksSupportQualityId { get; set; }
        public string WorksSupportQualityName { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public string IconUrl { get; set; }
        public string ColorCode { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}