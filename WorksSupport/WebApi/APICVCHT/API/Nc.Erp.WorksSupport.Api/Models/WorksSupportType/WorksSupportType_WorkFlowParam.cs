using System.Collections.Generic;

namespace Nc.Erp.WorksSupport.Api.Models.WorksSupportType
{
    public class WorksSupportTypeWorkFlowParam
    {
        public int WorksSupportStepId { get; set; }
        public int WorksSupportTypeId { get; set; }
        public string WorksSupportStepName { get; set; }
        public string ProccessFunctionId { get; set; }
        public string Stepcolorcode { get; set; }
        public int Maxprocesstime { get; set; }
        public int Isinitstep { get; set; }
        public int Isfinishstep { get; set; }
        public int Ismustchooseprocessuser { get; set; }
        public int Autochangetostatusid { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsSystem { get; set; }
        public int IsCanModifiedContent { get; set; }
        public int IsCanModifiedContentSolution { get; set; }
        public int IsCanModifiedCompletedDate { get; set; }
        public int IsCanattach { get; set; }
        public int IsCanComment { get; set; }
        public int IsCanUpdateProcess { get; set; }
        public int IsCanUppateQuality { get; set; }
        public List<WorksSupportTypeWfNxParam> LstWorksSupportTypeWfNx { get; set; }
        public List<WorksSupportTypeWfPermisParam> LstWorksSupportTypeWfPermis { get; set; }
    }
}