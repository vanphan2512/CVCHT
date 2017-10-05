﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nc.Erp.WorksSupport.Do.Configuration.ReportProces
{
    public class ReportProcess
    {

    }
    public class WorkSupportProject
    {
        public int WorksSupportProjectId { get; set; }
        public string WorksSupportProjectName { get; set; }
    }
    public class WorkSupportGroup
    {
        public int WorksSupportGroupId { get; set; }
        public string WorksSupportGroupName { get; set; }
        public int NumberOfWork { get; set; }
    }
    public class WorkSupportStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string ColorCode { get; set; }
        public int NumberOfWork { get; set; }
    }
    public class WorkSupportReport
    {
        public List<WorkSupportStatus> WorkStatusList { get; set; }
        public Object WorkWrong { get; set; }
    }
    public class WorkSupportReportTime
    {
        public string WorkTime { get; set; }
        public int NumberOfWork { get; set; }
    }
    public class WorkSupportReportDate
    {
        public List<WorkSupportReportTime> WorkLateList { get; set; }
        public List<WorkSupportReportTime> WorkWrongList { get; set; }
    }
    public class WorkStateReportByTime
    {
       public string WorkTime { get; set; }
       public int WorkLate { get; set; }
        public int WorkWrong { get; set; }
    }
    public class WorkSupportDetail
    {
        public int WorksSupportId { get; set; }
        public string WorksSupportName { get; set; }
        public DateTime? ExpectedCompletedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int NumberOfLateDate { get; set; }
        public int NumberOfWork { get; set; }
        public int CurrentProgress { get; set; }
        public string WorksSupportStatusName { get; set; }
    }
    public class ExcelReport
    {
        public int WorksSupportStatusId { get; set; }
        public string WorksSupportStatusName { get; set; }
        public int? WorksSupportGroupId { get; set; }
        public string WorksSupportGroupName { get; set; }
        public int? WorksSupportId { get; set; }
        public string WorksSupportName { get; set; }
        public string ExpectedCompletedDate { get; set; }
        public string CompletedDate { get; set; }
        public int CurrentProgress { get; set; }
        public string ColorCode { get; set; }
    }
    public class PieChart
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int NumberOfWork { get; set; }
        public string ColorCode { get; set; }
    }
    public class ColumnChartData
    {
        public string WorkTime { get; set; }
        public int WorkLate { get; set; }
        public int WorkWrong { get; set; }
    }
}
