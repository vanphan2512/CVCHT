import { Component, ViewChild, OnInit, Input } from "angular2/core";
import { Router } from "angular2/router";
import { Http, RequestOptions, Headers, Response } from "angular2/http";
import { BasePage } from "../../../common/models/ui";
import { FormDatePicker, FormFilesUpload } from "../../../common/directive";
import worksGroupService from "../_share/services/WorksGroupService";
import route from "../_share/config/route";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ErrorMessage } from "../../../common/directives/common/errorMessage";
import { ValidationException } from "../../../common/models/exception";
import { RoleValue } from "../../../common/enum";
import projectService from "../_share/services/projectService";
import WorksSerice from "../_share/services/worksSerice";
import WorkDetailSerice from "../_share/services/workDetailService";
import { ProcessUserModel } from "../works/AddProcessUser";
import prioritiesService from "../../configurations/_share/services/priorityService";
import { EditModel } from "../works/editModel";
import configHelper from "../../../common/helpers/configHelper";
import reportsService from "../_share/services/report";
import sessionStorage from "../../../common/storages/sessionStorage";
@Component({
    templateUrl: "app/modules/eoffice/report/report.html",
    directives: [FormDatePicker, FormFilesUpload]
})
export class Report extends BasePage implements OnInit {
    private router: Router;
    public user = sessionStorage.get(CACHE_CONSTANT.strUserName);
    public listProject: Array<any> = [];
    public selectedProject: string = "";
    public listWorkGroups: Array<any>;
    public selectedWorkGroups: string = "";
    public txtFromDates: string = "";
    public txtToDates: string = "";
    public listReport: any = null;
    public totalReport: number = 0;
    public totalStatus: number = 0;
    public listReportByStatus: Array<any> = [];
    public listReportByState: any = null;
    public listReportByGroup: Array<any> = [];
    public listReportByDay: Array<any> = [];
    public listReportByWeek: Array<any> = [];
    public listReportByMonth: Array<any> = [];
    public listReportByYear: Array<any> = [];
    public myDoughnutChart: any;
    public myChart: any;
    public selectedViewMode: string = "2";
    public error: string = "";
    @ViewChild('dataPickerFrom') dataPickerFrom: FormDatePicker;
    @ViewChild('dataPickerTo') dataPickerTo: FormDatePicker;
    constructor(router: Router) {
        super();
        this.router = router;
    }
    ngAfterViewInit(){
        let self: Report = this;
        self.dataPickerFrom.resetDateSearch();
        self.dataPickerTo.resetDateSearch();
    }
    ngOnInit() {
        let $ = window.jQuery;
        let self: Report = this;
        $('#mySelect').on('change', function (e: any) {
            $('#myTab li a').eq($(this).val()).tab('show');
        });

        self.loadProject();
        var ctx = document.getElementById("myChart").getContext('2d');
        this.myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                datasets: [{
                    label: 'Trễ hạn',
                    data: [0],
                    backgroundColor: '#d43b3b'
                }, {
                    label: 'Vi phạm',
                    data: [0],
                    backgroundColor: '#232523'
                }
                ],
                labels: ['']
            }
        });
        let data = {
            datasets: [{
                data: [""]
            }],

            // These labels appear in the legend and in the tooltips when hovering different arcs
            labels: [""]
        };
        var ctxDN = document.getElementById("myChartDN").getContext('2d');
        this.myDoughnutChart = new Chart(ctxDN, {
            type: 'pie',
            data: data,
            options: {
                title: {
                    display: true,
                    text: 'Trạng thái công việc cần hỗ trợ',
                    position: 'bottom'
                },
                useHTML: true
            }
        });
    }
    private loadProject() {
        let self: Report = this;
        let $ = window.jQuery;
        let roleValue: string = RoleValue.Admin;
        let isAdmin: boolean = sessionStorage.get(CACHE_CONSTANT.strUserName) === roleValue ? true : false;
        let user = isAdmin === false ? sessionStorage.get(CACHE_CONSTANT.strUserName) : "administrator";
        reportsService.getProject(user).then(function (items: Array<any>) {
            self.listProject = items;
        });
    }
    private getWorkGroup() {
        let self: Report = this;
        let $ = window.jQuery;
        self.listWorkGroups = [];
        reportsService.getWorkGroupByProjectId(self.selectedProject).then(function (items: Array<any>) {
            self.listWorkGroups = items;
            self.selectedWorkGroups = "";
        });
    }
    private search() {
        let self: Report = this;
        let $ = window.jQuery;
        if (this.validate()) {
            reportsService.getReport(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
                self.listReport = items;
                self.totalReport = 0;
                self.totalStatus = 0;
                if (self.listReport !== null && self.listReport != []) {
                    self.listReport.WorkStatusList.forEach(element => {
                        self.totalReport += element.NumberOfWork;
                    });
                    self.totalStatus += self.listReport.WorkWrong.NumberOfWorkLate;
                    self.totalStatus += self.listReport.WorkWrong.NumberOfWorkWrong;
                }
                let labels = self.listReport.WorkStatusList.map(function (a) { return a.StatusName; });
                let datas = self.listReport.WorkStatusList.map(function (a) { return a.NumberOfWork; });
                let color = self.listReport.WorkStatusList.map(function (a) { return a.ColorCode; });
                self.refreshDataPieChart(self.myDoughnutChart, labels, datas, color);
                self.getBarChart();
            });
            reportsService.getWorkStateByStateType(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
                self.listReportByState = items;
                self.listReportByState.listWorkGroupByStateLate = [];
                self.listReportByState.listWorkGroupByStateWrong = [];
                if ($('#liLate').hasClass("tree-collapse")) {
                    $('#liLate').removeClass("tree-collapse");
                    $('#liLate').addClass("tree-collapsed");
                    $('#type1').collapse("hide");
                } if ($('#liWrong').hasClass("tree-collapse")) {
                    $('#liWrong').removeClass("tree-collapse");
                    $('#liWrong').addClass("tree-collapsed");
                    $('#type2').collapse("hide");
                }
            });
            reportsService.getReportByStatus(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
                self.listReportByStatus = items;
                self.listReportByStatus.forEach(element => {
                    element.listWorkGroupByStatus = [];
                });
            });
            reportsService.getWorkGroupByGroupType(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
                self.listReportByGroup = items;
                self.listReportByGroup.forEach(element => {
                    element.listWorkGroupDetailByGroup = [];
                });
            });
        } else {
            $("#modalErorr").modal("toggle");
        }
    }
    private LoadWorkGroupsByState(ID: any, event: any) {
        let self: Report = this;
        let $ = window.jQuery;
        reportsService.loadWorkGroupsByState(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/"), ID).then(function (items: Array<any>) {
            if (ID === 1) {
                self.listReportByState.listWorkGroupByStateLate = items;
                self.listReportByState.listWorkGroupByStateLate.forEach(element => {
                    element.listWorkDetailByStateType = [];
                });
            }
            if (ID === 2) {
                self.listReportByState.listWorkGroupByStateWrong = items;
                self.listReportByState.listWorkGroupByStateWrong.forEach(element => {
                    element.listWorkDetailByStateType = [];
                });
            }
            if (items.length > 0 && event.target.tagName === "LI") {
                if ($(event.target).hasClass("tree-collapsed")) {
                    $(event.target).removeClass("tree-collapsed");
                    $(event.target).addClass("tree-collapse")
                }
                else {
                    $(event.target).removeClass("tree-collapse");
                    $(event.target).addClass("tree-collapsed");
                }
                $('#type' + ID).collapse("toggle");
            }
        });
    }
    private LoadWorkGroupsByStateATag(ID: any) {
        let self: Report = this;
        let $ = window.jQuery;
        reportsService.loadWorkGroupsByState(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/"), ID).then(function (items: Array<any>) {
            let idScroll = "liLate";
            if (ID === 1) {
                self.listReportByState.listWorkGroupByStateLate = items;
                self.listReportByState.listWorkGroupByStateLate.forEach(element => {
                    element.listWorkDetailByStateType = [];
                });
                idScroll = 'liLate';
            }
            if (ID === 2) {
                self.listReportByState.listWorkGroupByStateWrong = items;
                self.listReportByState.listWorkGroupByStateWrong.forEach(element => {
                    element.listWorkDetailByStateType = [];
                });
                idScroll = 'liWrong';
            }
            if (items.length > 0) {
                if ($('#type' + ID)[0].parentNode.className.contains("tree-collapsed")) {
                    $('#type' + ID)[0].parentNode.classList.remove("tree-collapsed");
                    $('#type' + ID)[0].parentNode.classList.add("tree-collapse")
                }
                else {
                    $('#type' + ID)[0].parentNode.classList.remove("tree-collapse");
                    $('#type' + ID)[0].parentNode.classList.add("tree-collapsed");
                }
                $('#type' + ID).collapse("toggle");
                $('#myTab li a').eq("0").tab('show');

                $('html, body').stop().animate({
                    scrollTop: $("#" + idScroll).offset().top
                }, 1000);
            }
        });
    }
    private getWorkDetailByStateType(typeID: any, ID: any, event: any) {
        let self: Report = this;
        let $ = window.jQuery;
        reportsService.getWorkDetailByStateType(self.selectedProject, ID, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/"), typeID).then(function (items: Array<any>) {
            if (typeID === 1) {
                let index = self.listReportByState.listWorkGroupByStateLate.findIndex(_ => _.WorksSupportGroupId.toString() === ID.toString());
                if (index > -1) {
                    self.listReportByState.listWorkGroupByStateLate[index].listWorkDetailByStateType = items;
                }
            }
            if (typeID === 2) {
                let index = self.listReportByState.listWorkGroupByStateWrong.findIndex(_ => _.WorksSupportGroupId.toString() === ID.toString());
                if (index > -1) {
                    self.listReportByState.listWorkGroupByStateWrong[index].listWorkDetailByStateType = items;
                }
            }
            if (items.length > 0 && event.target.tagName === "LI") {
                if ($(event.target).hasClass("tree-collapsed")) {
                    $(event.target).removeClass("tree-collapsed");
                    $(event.target).addClass("tree-collapse")
                }
                else {
                    $(event.target).removeClass("tree-collapse");
                    $(event.target).addClass("tree-collapsed");
                }
                $('#wg' + typeID + ID).collapse("toggle");
            }
        });
        event.stopPropagation();
        //$('#wg'+statusID+ID).collapse("show");
    }
    private getWorkDetailByGroupType(ID: any, event: any) {
        let self: Report = this;
        let $ = window.jQuery;
        reportsService.getWorkDetailByGroupType(self.selectedProject, ID, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
            let index = self.listReportByGroup.findIndex(_ => _.WorksSupportGroupId.toString() === ID.toString());
            if (index > -1) {
                self.listReportByGroup[index].listWorkGroupDetailByGroup = items;
            }
            if (items.length > 0 && event.target.tagName === "LI") {
                if ($(event.target).hasClass("tree-collapsed")) {
                    $(event.target).removeClass("tree-collapsed");
                    $(event.target).addClass("tree-collapse")
                }
                else {
                    $(event.target).removeClass("tree-collapse");
                    $(event.target).addClass("tree-collapsed");
                }
                $('#wgdt' + ID).collapse("toggle");
            }
        });
    }
    private getWorkGroupByStatus(statusID: any, event: any) {
        let self: Report = this;
        let $ = window.jQuery;        
        reportsService.getWorkGroupByStatus(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/"), statusID).then(function (items: Array<any>) {
            let index = self.listReportByStatus.findIndex(_ => _.StatusId.toString() === statusID.toString());
            if (index > -1) {
                self.listReportByStatus[index].listWorkGroupByStatus = items;
                self.listReportByStatus[index].listWorkGroupByStatus.forEach(element => {
                    element.listWorkStatusByWorkGroupId = [];
                });
            }
            if (items.length > 0 && event.target.tagName === "LI") {
                if ($(event.target).hasClass("tree-collapsed")) {
                    $(event.target).removeClass("tree-collapsed");
                    $(event.target).addClass("tree-collapse")
                }
                else {
                    $(event.target).removeClass("tree-collapse");
                    $(event.target).addClass("tree-collapsed");
                }
                $('#' + statusID).collapse("toggle");
            }
        });
    }
    private getWorkGroupByStatusATag(statusID: any, event: any) {
        let self: Report = this;
        let $ = window.jQuery;
        reportsService.getWorkGroupByStatus(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/"), statusID).then(function (items: Array<any>) {
            let index = self.listReportByStatus.findIndex(_ => _.StatusId.toString() === statusID.toString());
            if (index > -1) {
                self.listReportByStatus[index].listWorkGroupByStatus = items;
                self.listReportByStatus[index].listWorkGroupByStatus.forEach(element => {
                    element.listWorkStatusByWorkGroupId = [];
                });
            }
            if (items.length > 0) {
                if ($('#' + statusID)[0].parentNode.className.contains("tree-collapsed")) {
                    $('#' + statusID)[0].parentNode.classList.remove("tree-collapsed");
                    $('#' + statusID)[0].parentNode.classList.add("tree-collapse")
                }
                else {
                    $('#' + statusID)[0].parentNode.classList.remove("tree-collapse");
                    $('#' + statusID)[0].parentNode.classList.add("tree-collapsed");
                }
                $('#' + statusID).collapse("toggle");
                $('#myTab li a').eq("1").tab('show');
                $('html, body').stop().animate({
                    scrollTop: $("#" + statusID).offset().top
                }, 1000);
            }
        });
    }

    private getWorkStatusByWorkGroupId(statusID: any, ID: any, event: any) {
        let self: Report = this;
        let $ = window.jQuery;
        reportsService.getWorkStatusByWorkGroupId(self.selectedProject, ID, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/"), statusID).then(function (items: Array<any>) {
            let index = self.listReportByStatus.findIndex(_ => _.StatusId.toString() === statusID.toString());
            if (index > -1) {
                let idx = self.listReportByStatus[index].listWorkGroupByStatus.findIndex(obj => obj.WorksSupportGroupId.toString() === ID.toString())
                if (idx > -1) self.listReportByStatus[index].listWorkGroupByStatus[idx].listWorkStatusByWorkGroupId = items;
                if (items.length > 0 && event.target.tagName === "LI") {
                    if ($(event.target).hasClass("tree-collapsed")) {
                        $(event.target).removeClass("tree-collapsed");
                        $(event.target).addClass("tree-collapse")
                    }
                    else {
                        $(event.target).removeClass("tree-collapse");
                        $(event.target).addClass("tree-collapsed");
                    }
                    $('#wg' + statusID + ID).collapse("toggle");
                }
            }
        });
        event.stopPropagation();
        //$('#wg'+statusID+ID).collapse("show");
    }
    private chartByDay() {
        let self: Report = this;
        reportsService.GetLineChartByDay(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
            self.listReportByDay = items;
            let labels = self.listReportByDay.map(function (a) { return a.WorkTime; });
            let late = self.listReportByDay.map(function (a) { return a.WorkLate; });
            let wrong = self.listReportByDay.map(function (a) { return a.WorkWrong; });
            let dataLate = new Object({ label: "Trễ hạn", data: late, backgroundColor: "#d43b3b" });
            let dataWrong = new Object({ label: "Vi phạm", data: wrong, backgroundColor: "#232523" });
            self.removeDataBarChart(self.myChart);
            self.addDataBarChart(self.myChart, labels, dataLate);
            self.addDataBarChart(self.myChart, labels, dataWrong);
        });
    }
    private chartByWeek() {
        let self: Report = this;
        reportsService.GetLineChartByWeek(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
            self.listReportByWeek = items;
            let labels = self.listReportByWeek.map(function (a) { return a.WorkTime; });
            let late = self.listReportByWeek.map(function (a) { return a.WorkLate; });
            let wrong = self.listReportByWeek.map(function (a) { return a.WorkWrong; });
            let dataLate = new Object({ label: "Trễ hạn", data: late, backgroundColor: "#d43b3b" });
            let dataWrong = new Object({ label: "Vi phạm", data: wrong, backgroundColor: "#232523" });
            self.removeDataBarChart(self.myChart);
            self.addDataBarChart(self.myChart, labels, dataLate);
            self.addDataBarChart(self.myChart, labels, dataWrong);
        });
    }
    private chartByMonth() {
        let self: Report = this;
        reportsService.GetLineChartByMonth(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
            self.listReportByMonth = items;
            let labels = self.listReportByMonth.map(function (a) { return a.WorkTime; });
            let late = self.listReportByMonth.map(function (a) { return a.WorkLate; });
            let wrong = self.listReportByMonth.map(function (a) { return a.WorkWrong; });
            let dataLate = new Object({ label: "Trễ hạn", data: late, backgroundColor: "#d43b3b" });
            let dataWrong = new Object({ label: "Vi phạm", data: wrong, backgroundColor: "#232523" });
            self.removeDataBarChart(self.myChart);
            self.addDataBarChart(self.myChart, labels, dataLate);
            self.addDataBarChart(self.myChart, labels, dataWrong);
        });
    }
    private chartByYear() {
        let self: Report = this;
        reportsService.GetLineChartByYear(self.selectedProject, self.selectedWorkGroups, self.stringToDate(self.txtFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtToDates, "dd/MM/yyyy", "/")).then(function (items: Array<any>) {
            self.listReportByYear = items;
            let labels = self.listReportByYear.map(function (a) { return a.WorkTime; });
            let late = self.listReportByYear.map(function (a) { return a.WorkLate; });
            let wrong = self.listReportByYear.map(function (a) { return a.WorkWrong; });
            let dataLate = new Object({ label: "Trễ hạn", data: late, backgroundColor: "#d43b3b" });
            let dataWrong = new Object({ label: "Vi phạm", data: wrong, backgroundColor: "#232523" });
            self.removeDataBarChart(self.myChart);
            self.addDataBarChart(self.myChart, labels, dataLate);
            self.addDataBarChart(self.myChart, labels, dataWrong);
        });
    }
    private getBarChart() {
        let self: Report = this;
        switch (self.selectedViewMode) {
            case "0": self.chartByDay(); break;
            case "1": self.chartByWeek(); break;
            case "2": self.chartByMonth(); break;
            case "3": self.chartByYear(); break;
        }
    }
    private refreshDataPieChart(chart: any, label: any, data: any, color: any) {
        chart.data.labels = [];
        chart.data.labels.push.apply(chart.data.labels, label);
        chart.data.datasets.forEach((dataset: any) => {
            dataset.data = [];
            dataset.data.push.apply(dataset.data, data);
            dataset.backgroundColor = [];
            dataset.backgroundColor.push.apply(dataset.backgroundColor, color);
        });
        chart.update();
    }
    private addDataBarChart(chart: any, label: any, data: any) {
        chart.data.labels = [];
        chart.data.labels.push.apply(chart.data.labels, label);
        chart.data.datasets.push(data);
        chart.update();
    }
    private removeDataBarChart(chart: any) {
        chart.data.labels = [];
        chart.data.datasets = [];
        chart.update();
    }
    public stringToDate(_date: any, _format: any, _delimiter: any): Date {
        var formatLowerCase = _format.toLowerCase();
        var formatItems = formatLowerCase.split(_delimiter);
        var dateItems = _date.split(_delimiter);
        var monthIndex = formatItems.indexOf("mm");
        var dayIndex = formatItems.indexOf("dd");
        var yearIndex = formatItems.indexOf("yyyy");
        var month = parseInt(dateItems[monthIndex]);
        month -= 1;
        var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex], 12, 0, 0);
        return formatedDate;
    }
    public stringAsDate(inputFormat: string) {
        if (inputFormat === null || inputFormat === "") return "-";
        function pad(s: any) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }
    /**
 * Writen by  : Nguyen van Huan
 * Create by  : 10.08.2017
 * Description: compare date
 * @param date1 
 * @param date2 
 */
    private compareDate(date1: string, date2: string): boolean {
        if (date1 !== "" && date2 !== "") {
            let oldDate = date1.split("/")[2] + date1.split("/")[1] + date1.split("/")[0];
            let newDate = date2.split("/")[2] + date2.split("/")[1] + date2.split("/")[0];
            return parseInt(newDate) - parseInt(oldDate) >= 0 ? true : false;
        } else {
            return true;
        }
    }
    private validate(): boolean {
        let self = this;
        if (this.compareDate(self.txtFromDates, self.txtToDates) === false) {
            self.error = "Ngày tìm kiếm không hợp lệ!";
            return false;
        }
        if (this.selectedProject === null || this.selectedProject === "") {
            self.error = "Bạn phải chọn dự án!";
            return false;
        }
        return true;
    }
    public exportExcel() {
        if (this.validate()) {
            let self = this;
            let fromDate = null;
            let toDate = null;
            if (self.txtFromDates == "" || self.txtFromDates == undefined || self.txtFromDates == null) {
                fromDate = "";
            } else {
                fromDate = self.txtFromDates.split("/")[1] + "/" + self.txtFromDates.split("/")[0] + "/" + self.txtFromDates.split("/")[2];
            }
            if (self.txtToDates == "" || self.txtToDates == undefined || self.txtToDates == null) {
                toDate = "";
            } else {
                toDate = self.txtToDates.split("/")[1] + "/" + self.txtToDates.split("/")[0] + "/" + self.txtToDates.split("/")[2];
            }
            let projectName = self.listProject.find(project => project.WorksSupportProjectId.toString() === self.selectedProject.toString());
            if (projectName === undefined) {
                projectName = "";
            } else {
                projectName = projectName.WorksSupportProjectName;
            }
            let workGroupName = self.listWorkGroups.find(project => project.WorksSupportGroupId.toString() === self.selectedWorkGroups.toString());
            if (workGroupName === undefined) {
                workGroupName = "";
            } else {
                workGroupName = workGroupName.WorksSupportGroupName;
            }
            let url = String.format("{0}{1}{2}{3}", configHelper.getAppConfig().api.downloadUrl, "DownloadReportExcel?projectId=", self.selectedProject, "&workGroupId=" + self.selectedWorkGroups +
                "&fromDate=" + fromDate + "&toDate=" + toDate + "&projectName=" + projectName + "&workGroupName=" + workGroupName);
            window.open(url);
        } else {
            $("#modalErorr").modal("toggle");
        }
    }
    private redirecttoDetail(id: any) {
        localStorage.setItem('workSupportDetailID', id);
        window.open(route.work.detail.path, "_blank");
    }
    private round(number: any)
    {
        return Math.round(number);
    }
}