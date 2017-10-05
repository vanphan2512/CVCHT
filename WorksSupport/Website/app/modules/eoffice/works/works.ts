import { Component, ViewChild } from "angular2/core";
import { Router } from "angular2/router";
import { Http } from "angular2/http";
import { BasePage } from "../../../common/models/ui";
import { WorksModel } from "./worksModel";
import WorksSerice from "../_share/services/worksSerice";
import prioritiesService from "../../configurations/_share/services/priorityService";
import { PageAction } from "../../../common/models/ui";
import { DeleteModel } from "./deleteModel";
import route from "../_share/config/route";
import { FormMode } from "../../../common/enum";
import { AddWorksModel } from "./addWorksModel";
import projectService from "../_share/services/projectService";
import { ValidationException } from "../../../common/models/exception";
import { Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, FormDatePicker } from "../../../common/directive";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { EditModel } from "./editModel";
import { ProcessUserModel } from "./AddProcessUser";
import { AddMember } from "./AddWSMember";
import { WorksInvoleModel } from "./addWorkInvole";

@Component({
    templateUrl: "app/modules/eoffice/works/works.html",
    // directives: [Grid, PageActions, Page]
    directives: [Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, ErrorMessage, FormDatePicker]
})

export class Works extends BasePage {
    public WorksSupportTypeId: number = 0;
    public WorksSupportId: number = 0;
    public WorksSupportName: string = String.empty;
    public Content: string = String.empty;
    public WorksSupportGroupId: number = 0;
    public WorksSupportProjectId: number = 0;
    public WorksSupportPriorityId: number = -1;
    public PeriodId: number = -1;
    public ExpectedCompletedDate: Date;
    public ExpectedCompletedDate1: string;
    public MemberUserName: string = String.empty;
    public UpdatedUser: string = String.empty;
    // public CreateDate: Date ;
    public TypeSearch: number = -1;
    public NextWorksSupportStepId: number = 0;
    public SelWorkId: number;
    public Note: string = String.empty;
    public PeriodName: string = String.empty;
    public CreateDate1: string;
    public CreateDate: Date;
    public CreatedUser: string = String.empty;
    public WorksSupportTypeName: string = String.empty;
    public lstWorksType: any = [];
    public lstWorksFlow: any = [];
    public lstProcessUser: any = [];
    public WorksSupportStepId: number = -1;
    public lstWorksTypePeriod: any = [];
    public ProjectId: number = -1;
    public ProjectIDInvole: string = String.empty;
    public ProjectName: string = String.empty;
    public lstWorksInvole: any = [];
    public lstWorksInvole1: any = [];
    public actions: Array<PageAction> = [];
    private router: Router;
    public startDate: string;
    public endDate: string;
    public keySearch: string = String.empty;
    public User: string = String.empty;
    public objDelete: DeleteModel;
    public selectedValue: Array<any> = [];
    public listWorksInvole: Array<any> = [];
    public listWorks: Array<any> = [];
    public lstDepartment: any = [];
    public KeyWork: string = String.empty;
    public lstMember: any = [];
    public WorksSupportDepartmentId: number = -1;
    public DepartmentId: number = -1;
    public txtKeyNameUser: any = String.empty;
    public WorksSupportDate: Date;
    public WorksSupportMemberRoleId: number = -1;
    public WorksName: string = String.empty;
    public isCanEditXL: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTWORKS_ADD) === "true" ? true : false;
    public isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_DEL) === "true" ? true : false;
    public isCanXL: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_DEL) === "true" ? true : false;
    private tempDate: string = String.empty;
    public listId: Array<any> = [];
    constructor(router: Router) {
        super();
        let self: Works = this;
        self.router = router;
        // self.model = new WorksModel(self.i18nHelper);
        self.loadWorks();
    }

    @ViewChild(Grid)
    private gridComponent: Grid;

    private loadWorks() {
        debugger;
        let self: Works = this;
        WorksSerice.getWorks().then(function (items: Array<any>) {
            self.listWorks = items;
        });
    }
    // Search by IsDelete
    public onStatusChange(event: any) {
        // Get value of dropdown list
        let $ = window.jQuery;
        let currentIsDelete = event.currentTarget.value;
        let self: Works = this;
        WorksSerice.getWorksBy("", currentIsDelete, -1, -1).then(function (items: Array<any>) {
            //self.model.importWorks(items);
            self.listWorks = items;
        });
        // check status is delete to show delete button.
        if (currentIsDelete === "1") {
            $("#btnDeletePopup").css("display", "none");
        } else {
            $("#btnDeletePopup").css("display", "inline-block");
        }
    }
    // Search data by name
    public SearchByKey(event: any) {
        let $ = window.jQuery;
        let worksName = $("#worksName").val();
        let self: Works = this;
        WorksSerice.getWorksBy(worksName, 0, 0, -1).then(function (items: Array<any>) {
            // self.model.importWorks(items);
            self.listWorks = items;
        });
    }
    // show popup delete
    // public onShowPopupDelete(event: any) {
    //     let $ = window.jQuery;
    //     let self: Works = this;
    //     this.selectedValue = new Array();
    //     let lstId: Array<string> = [];
    //     let grid = this.gridComponent.grid.rows(".selected").data();
    //     this.User = cacheService.get(CACHE_CONSTANT.strUserName);
    //     for (let i = 0; i < grid.length; i++) {
    //         this.selectedValue.push({ id: grid[i]["WorksSupportId"], name: grid[i]["WorksSupportName"] });
    //         lstId.push(grid[i]["WorksSupportId"]);
    //     }
    //     this.objDelete = new DeleteModel(lstId.toString(), this.User);
    // }
    // public onShowPopupDelete(event: any) {
    //     let $ = window.jQuery;
    //     let self: Works = this;
    //     this.selectedValue = new Array();
    //     let lstId: Array<string> = [];
    //     this.User = cacheService.get(CACHE_CONSTANT.strUserName);
    //     for (let i = 0; i < self.SelWorkId; i++) {
    //         this.selectedValue.push({ id: this.SelWorkId[i], name: this.SelWorkName[i] });
    //         lstId.push( this.SelWorkId[i]);
    //     }
    //     this.objDelete = new DeleteModel(lstId.toString(),this.User);

    // }
    public onShowPopupDelete(event: any) {
        debugger;
        let $ = window.jQuery;
        let self: Works = this;
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        this.User = cacheService.get(CACHE_CONSTANT.strUserName);
        for (let i = 0; i < this.lstWorksId.length; i++) {
            this.selectedValue.push({ id: this.lstWorksId[i], name: this.SelWorkName[i] })
        }

        this.objDelete = new DeleteModel(this.selectedValue[0].id.toString(), this.User);
    }
    // delte Works
    public onDeleteWorks(event: any) {
        debugger;
        let self: Works = this;
        let $ = window.jQuery;
        let Id = this.lstWorksId;
        this.User = cacheService.get(CACHE_CONSTANT.strUserName);

        WorksSerice.delete(Id, this.User).then(function (event: any) {
            $("#modal").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksModel(self.i18nHelper);
            self.loadWorks();
        });

    }
    public resetValidation() {
        let $ = window.jQuery;
        $(".form-control").removeClass("invalid");
        $(".form-control").removeClass("validation");
    }
    // show popup add works
    private onAddNewWorksClick() {
        prioritiesService.getPrioritiesAll().then(function (items: Array<any>) {
            self.lstWorksType = items;
        });
        WorksSerice.getWork_TypePeriod().then(function (items: Array<any>) {
            self.lstWorksTypePeriod = items;
        })
        this.resetValidation();
        let $ = window.jQuery;
        let self: Works = this;
        self.WorksSupportId = 0;
        self.WorksSupportName = "";
        self.Content = "";
        self.WorksSupportGroupId = 0;
        self.WorksSupportProjectId = 0;
        self.WorksSupportPriorityId = this.WorksSupportPriorityId;
        self.PeriodId = this.PeriodId;
        self.CreateDate = this.CreateDate;
        self.CreatedUser = "";
        self.WorksSupportTypeName = "";

        // WorksSerice.getAllWorksType().then(function (items: Array<any>) {
        //     self.lstWorksType = items;
        // });
        $("#modalAdd").modal('toggle');
        this.loadWorkInvoles();
    }
    // validation for add model
    public isValidAdd(): boolean {
        let validationErrors: ValidationException = new ValidationException();
        if (!this.model.worksSupportPriorityName) {
            validationErrors.add("configurations.addOrUpdatePriority.validation.nameIsRequireAdd");
        }
        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }

    // Add new WorksSupport
    public onAddWorks(event: any) {
        debugger;
        let self: Works = this;
        let $ = window.jQuery;
        let Id = this.WorksSupportId;
        let WorksSupportName = this.WorksSupportName;
        let CreateDate = this.CreateDate;
        // let CreateDate = this.CreateDate.setFullYear(this.CreateDate.getUTCFullYear(), this.CreateDate.getUTCMonth(), this.CreateDate.getUTCDate() + 1);
        let Content = this.Content;
        let ExpectedCompletedDate = this.ExpectedCompletedDate;
        let WorksSupportPriorityId = this.WorksSupportPriorityId;
        let WorksSupportGroupId = this.WorksSupportGroupId;
        let WorksSupportTypeId = this.WorksSupportTypeId;
        let PeriodId = this.PeriodId;
        let user = cacheService.get(CACHE_CONSTANT.strUserName);
        let EditModels = new EditModel(Id, WorksSupportName, user, CreateDate, Content, ExpectedCompletedDate, WorksSupportPriorityId, WorksSupportGroupId, PeriodId, 2);
        WorksSerice.create(EditModels).then(function () {
            $("#modalAdd").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksModel(self.i18nHelper);
            self.loadWorks();
        })
    }
    // Cancel of popupAdd
    public onCancelAdd() {
        let $ = window.jQuery;
        $('#modalAdd').modal('hide');
        $('.modal-backdrop').modal('hide');
    }
    // set datapicker
    public setPicker1(event: any) {
        // this.CreateDate.getDay();
        // let year = this.CreateDate1._i[0].toString();
        //  let month = this.CreateDate1._i[1].toString();
        //   let day = this.CreateDate1._i[2].toString();
        //   this.CreateDate1 = day+"/"+month+"/"+year;
        this.CreateDate1 = "22/4/2017";
        //    this.CreateDate = val;
        // console.log(da);
    }
    private onStepClick(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        WorksSerice.getProcessUser(event).then(function (items: Array<any>) {
            self.lstProcessUser = items;
        });
    }
    // show popup add works
    private onXuLyClick(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        WorksSerice.getWorkFlow(event).then(function (items: Array<any>) {
            self.lstWorksFlow = items;
        });
        $("#modalXL").modal('toggle');
    }
    public onAddNewMemberManage(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        projectService.getAllDepartment().then(function (items: Array<any>) {
            self.lstDepartment = items;
        });
        $('#modalMember').modal('toggle');
    }
    public Keywords: string;
    public SearchUserByKey(event: any) {
        let $ = window.jQuery;
        let DepartmentId = $("#selDepartment").val();
        let nameUser = this.txtKeyNameUser;
        let self: Works = this;
        self.DepartmentId = 9;
        self.Keywords = nameUser;
        let lstModel: any = [];
        lstModel.DepartmentId = DepartmentId;
        lstModel.Keywords = nameUser;
        let modelAdd = new AddWorksModel(DepartmentId, nameUser);

        projectService.getUserBy({ "DepartmentId": 9, "Keywords": nameUser }).then(function (items: Array<any>) {
            // self.model.importProject(items);
            self.lstMember = items;
        });
    }
    public lstWorksId: Array<any> = [];
    public SelWorkName: Array<any> = [];
    public checkWorkId(id: any, name: any) {
        this.SelWorkId = id;
        let item = this.lstWorksId.push(id);
        let names = this.SelWorkName.push(name);
    }
    // Add ProcessUser of NextStepID
    public onAddProcessUser_NextStep(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        let WorksSupportId = $(".chkIsDeleted").val();
        // let NextWorksSupportStepId = document.getElementsByClassName("nextstep");
        let NextWorksSupportStepId = this.NextWorksSupportStepId;
        let CreateDate = this.CreateDate;
        let MemberUserName = this.MemberUserName;
        let Note = this.Note;
        let UpdatedUser = cacheService.get(CACHE_CONSTANT.strUserName);
        let Model = new ProcessUserModel(WorksSupportId, NextWorksSupportStepId, UpdatedUser, MemberUserName, Note);
        WorksSerice.InsertProcessUser_NextStep(Model).then(function () {
            $("#modalAdd").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksModel(self.i18nHelper);
            self.loadWorks();
        })
    }
    // Them thanh vien cho cong viec can ho tro
    public onAddWorks_Member(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        let WorksSupportId = this.WorksSupportId;
        let WorksSupportDate = this.WorksSupportDate;
        let MemberUserName = this.MemberUserName;
        // let CreateDate = this.CreateDate.setFullYear(this.CreateDate.getUTCFullYear(), this.CreateDate.getUTCMonth(), this.CreateDate.getUTCDate() + 1);
        let WorksSupportMemberRoleId = this.WorksSupportMemberRoleId;
        let UpdatedUser = cacheService.get(CACHE_CONSTANT.strUserName);
        let EditModels = new AddMember(WorksSupportId, WorksSupportDate, MemberUserName, WorksSupportMemberRoleId, UpdatedUser);
        WorksSerice.InsertWS_Member(EditModels).then(function () {
            $("#modalMember").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksModel(self.i18nHelper);
            self.loadWorks();
        })
    }
    public onAddWorks_MemberClick(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        let currentDepartmentName = event.currentTarget.value;

    }
    public onCancelAddWorks() {
        let $ = window.jQuery;
        $('#modalAdd').modal('hide');
        $('.modal-backdrop').modal('hide');
    }
    // show popup add works
    private onAddWorksInvoleClick() {
        WorksSerice.getWorksInvole().then(function (items: Array<any>) {
            self.lstWorksInvole = items;
        })
        this.resetValidation();
        let $ = window.jQuery;
        let self: Works = this;
        self.PeriodName = "";
        $('.chkIsWorksInvole').prop("checked", false);
        $(document).on("change", ".chkIsWorksInvole", function () {
            $("#ckCheckAll").prop("checked", $(".chkIsWorksInvole:checked").length == $(".chkIsWorksInvole").length);
             let val: Array<any> = [];
            $('[name="ids[]"]:checkbox:checked').each(function (i: any) {
                val[i] = $(this).val();
            });
            let Item = val;
        })
        $(document).on("change", "#ckCheckAll", function () {
            $(".chkIsWorksInvole").prop("checked", $("#ckCheckAll").is(":checked"));

            debugger;
             let val: Array<any> = [];
            $('[name="ids[]"]:checkbox:checked').each(function (i: any) {
                val[i] = $(this).val();
            });
            let Item = val;
        })
    }
    
    // public onAddWorksInvole(){

    // }
    // Search data of Works Invole
    public SearchWorksInvoleByKey(event: any) {
        let $ = window.jQuery;
        let worksName = $("#worksName").val();
        let self: Works = this;
        WorksSerice.getWorksBy(worksName, 0, 0, -1).then(function (items: Array<any>) {
            // self.model.importWorks(items);
            self.listWorks = items;
        });
    }
    public SearchWorkInvoleByKey(event: any) {
        let $ = window.jQuery;
        let ProjectId = $("#selProjectInvole").val();
        let nameUser = this.txtKeyNameUser;
        let self: Works = this;
        self.DepartmentId = 9;
        self.Keywords = nameUser;
        let lstModel: any = [];
        lstModel.DepartmentId = ProjectId;
        lstModel.Keywords = nameUser;
        let modelAdd = new AddWorksModel(ProjectId, nameUser);

        projectService.getUserBy({ "DepartmentId": 9, "Keywords": nameUser }).then(function (items: Array<any>) {
            // self.model.importProject(items);
            self.lstMember = items;
        });
    }
    // Search by IsDelete
    public ProjectIds: string = String.empty;
    public onSearchWorkInvoleChange(event: any) {
        // Get value of dropdown list
        let $ = window.jQuery;
        let objDate = new Date(Date.now());
        //  let objDate = new Date(selectedValue);
        let month = objDate.getMonth() + 1 < 10 ? "0" + (objDate.getMonth() + 1) : (objDate.getMonth() + 1).toString();
        let day = objDate.getDate() < 10 ? "0" + objDate.getDate() : objDate.getDate().toString();
        let year = objDate.getFullYear();
        let txtKeyName = $("#txtKeyName").val();
        let typename = $("#names").text();
        let typeSearchVal = -1;
        if (typename === "Tìm theo") {
            let typeSearchVal = -1;
        }
        else if (typename === "Tên công viêc") {
            typeSearchVal = 1;
        }
        else {
            typeSearchVal = 2;
        }
        // let typeSearchVal = $(".dropdownWorkInvole").attr('data-value');
        let ProjectIds = this.ProjectIds == "" ? "-1" : this.ProjectIds;
        // let startDate = (this.startDate === null || this.startDate === undefined) ? day+"/"+ month+"/"+year  :this.startDate;
        let startDate = this.startDate;
        // let EndDate = (this.endDate === null || this.endDate === undefined) ? day+"/"+ month+"/"+year  :this.endDate;
        let EndDate = this.endDate;
        let keyworks = txtKeyName;
        let typeSearch = typeSearchVal;
        let pageIndex = -1;
        let pageSize = -1;
        let self: Works = this;
        WorksSerice.getWorksInvoleBy(ProjectIds, keyworks, typeSearch, startDate, EndDate, pageIndex, pageSize).then(function (items: Array<any>) {
            self.listWorksInvole = items;
        });
        this.loadWorkInvoles();
    }

    public workTypeByDate(event: any) {
    }

    // add works invole on works support
    public worksId: Array<number> = [];
    public note: Array<any> = [];
    public lstWorkInvole: Array<any> = [];
    public onAddWorksInvole(event: any) {
        debugger;
        let self: Works = this;
        let $ = window.jQuery;
        let worksId = this.listId;

        let worksSupportId = 12;
        let note = this.note;
        let EditModels = new WorksInvoleModel(worksSupportId, worksId, note);
        WorksSerice.createAddWorkInvole(EditModels).then(function (items: Array<any>) {
            $("#modalWorksInvole").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.loadWorkInvoles();

        });
    }

    //check works invole
    public SelWorkInName: Array<any> = [];
    public checkWorkInvoleId(id: any, names: any) {
        let item = this.listId.push(id);
        this.SelWorkInName.push({ "id": id, "name": names });
    }
    public lstIds: Array<any> = [];

    public changeDateByIndex(index: any) {
        let objDate = new Date(Date.now());
        //  let objDate = new Date(selectedValue);
        let month = objDate.getMonth() + 1 < 10 ? "0" + (objDate.getMonth() + 1) : (objDate.getMonth() + 1).toString();
        let day = objDate.getDate() < 10 ? "0" + objDate.getDate() : objDate.getDate().toString();
        let year = objDate.getFullYear();
        let nextday = new Date(objDate.getFullYear(), objDate.getMonth(), objDate.getDate() + 19);
        let weekDate: string;
        let startDate = (this.ExpectedCompletedDate1 === null || this.ExpectedCompletedDate1 === undefined) ? day + "/" + month + "/" + year : this.ExpectedCompletedDate1;
        if (index === "-1") {

        } else if (index === "1") {
            let newDate = this.stringToDate(startDate, "dd/MM/yyyy", "/");
            this.ExpectedCompletedDate1 = this.dateToString(new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate() + 1));
        } else if (index === "2") {
            let newDate = this.stringToDate(startDate, "dd/MM/yyyy", "/");
            this.ExpectedCompletedDate1 = this.dateToString(new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate() + 7));
        } else if (index === "3") {
            let newDate = this.stringToDate(startDate, "dd/MM/yyyy", "/");
            this.ExpectedCompletedDate1 = this.dateToString(new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate() + 30));
        }
    }
    public dateToString(objDate: Date) {
        let month = objDate.getMonth() + 1 < 10 ? "0" + (objDate.getMonth() + 1) : (objDate.getMonth() + 1).toString();
        let day = objDate.getDate() < 10 ? "0" + objDate.getDate() : objDate.getDate().toString();
        let year = objDate.getFullYear();
        return day + "/" + month + "/" + year;
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
        var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
        return formatedDate;
    }

    public onShowPopupDelete_WorksInvole(id: any, name: any) {
        let $ = window.jQuery;
        let self: Works = this;
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        this.User = cacheService.get(CACHE_CONSTANT.strUserName);
        this.selectedValue.push({ "id": id, "name": name });
        this.objDelete = new DeleteModel(this.selectedValue[0].id.toString(), this.User);
        $("#modal_WIn").modal("toggle");
    }
    // delte Works
    public onDeleteWorksInvole(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        let Id = this.objDelete.Id;
        this.User = cacheService.get(CACHE_CONSTANT.strUserName);

        WorksSerice.removeWorkInvole(this.User, Id).then(function (event: any) {
            $("#modal_WIn").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksModel(self.i18nHelper);
            self.loadWorkInvoles();
        });
    }
    public lstAllWorks: Array<any> = [];
    private loadWorkInvoles() {
        let self: Works = this;
        // let worksId = this.listId;
        // let note = this.note;
        WorksSerice.getAllWorksInvole().then(function (items: Array<any>) {
            self.lstAllWorks = items;
        });
    }
}