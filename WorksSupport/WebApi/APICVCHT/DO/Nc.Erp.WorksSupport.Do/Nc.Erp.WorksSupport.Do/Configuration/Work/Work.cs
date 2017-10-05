﻿using System;

namespace Nc.Erp.WorksSupport.Do.Configuration.Work
{
    public class Work
    {
        public int WorksId { get; set; }
        public string WorksName { get; set; }
        public int WorksTypeId { get; set; }
        public string ProjectId { get; set; }
        public string Description { get; set; }
        public DateTime? WorksDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CurrentProgress { get; set; }
        public int IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int WorksStatusId { get; set; }
        public int WorksPriorityId { get; set; }
        public int WorksComplexityId { get; set; }
        public string AssignUser { get; set; }
        public int WorksQualityId { get; set; }
        public int Isreviewed { get; set; }
        public int ReviewedUser { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int CreatedStoreId { get; set; }
        public int CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int IsDeleted { get; set; }
        public int DeletedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int ContentDeleted { get; set; }
        public int CurrentWorksStepId { get; set; }

        public int InputType { get; set; }

        // nguoi thu hien
        public string ExecUser { get; set; }

        // ten du an
        public string ProjectName { get; set; }

        public int WorksSupportId { get; set; }

    }

    public class WorksPermission
    {
        public bool IsCanAddWorkssupport { get; set; }
        public bool IsCanEditWorkssupport { get; set; }
        public bool IsCanDeleteWorkssupport { get; set; }
    }

    public class WorksNextStepPermission
    {
        public bool IsCanEditContent { get; set; }
        public bool IsCanEditSolutionContent { get; set; }
        public bool IsCanAddAttachment { get; set; }
        public bool IsCanComment { get; set; }

        public bool IsCanEditEpectedCompletedDate{get; set;}

        public bool IsCanChangeProcgress { get; set; }

        public bool IsCanEditQuality { get; set; }

        public bool IsMustChooseProcessUser { get; set; }
        public bool IsCanProcessWorkFlow { get; set; }

    }

}