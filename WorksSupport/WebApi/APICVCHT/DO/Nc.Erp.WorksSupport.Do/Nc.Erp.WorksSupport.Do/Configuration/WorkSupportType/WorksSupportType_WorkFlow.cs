﻿using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Do.Configuration.WorkSupportType
{
    /// <summary>
    /// Created by      : Lương Trung Nhân
    /// Created date    : 02/06/2017
    /// Đối tượng ERP.EO.WorkSupportType_WorkFlow
    /// </summary>
    public class WorksSupportTypeWorkFlow
    {
        public int WorksSupportStepId { get; set; }
        public int WorksSupportTypeId { get; set; }
        public string WorksSupportStepName { get; set; }
        public string ProcessFunctionId { get; set; }
        public string StepColorCode { get; set; }
        public int MaxProcessTime { get; set; }
        public int IsInitStep { get; set; }
        public int IsFinishStep { get; set; }
        public int IsMustChooseProcessUser { get; set; }
        public int AutoChangeToStatusId { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        public int IsCanModifiedContent { get; set; }
        public int IsCanModifiedContentSolution { get; set; }
        public int IsCanModifiedCompletedDate { get; set; }
        public int IsCanAttach { get; set; }
        public int IsCanComment { get; set; }
        public int IsCanUpdateProcess { get; set; }
        public int IsCanUpDateQuality { get; set; }

        public int WorksSupportStatusId { get; set; }

        public int WorksSupportStepProgress { get; set; }

        //Quyền chọn bước xử lý tiếp theo
        public List<WorksSupportTypeWfnxPermis> ListWorksSupportTypeWfNxPermis { get; set; }

        //Bước xử lý kế tiếp
        public List<WorksSupportTypeWfNx> ListWorksSupportTypeWfNx { get; set; }
        //Quyền trên mỗi bước
        public List<WorksSupportTypeWfPermis> ListWorksSupportTypeWfPermis { get; set; }
  
    }
}
