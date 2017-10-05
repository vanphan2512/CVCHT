import { Component, ViewChild } from "angular2/core";
import { Router } from "angular2/router";
import { BasePage } from "../../../common/models/ui";
import { WorksModel } from "./worksModel";
import WorksSerice from "../_share/services/worksSerice";
import WorksGroupService from "../_share/services/worksGroupService";
import prioritiesService from "../../configurations/_share/services/priorityService";
import { DeleteModel } from "./deleteModel";
import route from "../_share/config/route";
import { FormMode } from "../../../common/enum";
import { AddWorksModel } from "./addWorksModel";
import projectService from "../_share/services/projectService";
import { ValidationException } from "../../../common/models/exception";
import { FormDatePicker, FormFilesUpload } from "../../../common/directive";
import sessionStorage from "../../../common/storages/sessionStorage";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { EditModel } from "./editModel";
import { ProcessUserModel } from "./AddProcessUser";
import { AddMember } from "./AddWSMember";
import { WorksInvoleModel } from "./addWorkInvole";
import pagingService from "../../../common/services/PagingService";
import { RoleValue } from "../../../common/enum";
import { UpdateModelProject } from "./UpdateModelProject";
import { UpdateModel } from "../worksgroup/updateModel";
import { ErrorNewMessage } from "../../../common/layouts/default/directives/common/ErrorNewMessage";
import configHelper from "../../../common/helpers/configHelper";
import WorkDetailSerice from "../_share/services/workDetailService";
@Component({
    templateUrl: "app/modules/eoffice/works/works.html",
    directives: [FormDatePicker, FormFilesUpload, ErrorNewMessage]
})
export class Works extends BasePage {

    private router: Router;
    public isCanAdd: boolean = false;
    public isCanEdit: boolean = false;
    public isCanDel: boolean = false;
    public roleValue: string = RoleValue.Admin;
    public listProject: Array<any> = [];
    public listWorks: Array<any> = [];
    public listWorksGroup: Array<any> = [];
    public listWorksFlow: Array<any> = [];
    public selectedStepID: string = String.empty;
    public selectedProjectID: string = "";
    public selectedWorksGroupID: string = "";
    public selectedPriority: string = String.empty;
    public listPriority: Array<any> = [];
    public WorksSupportId: number = 0;
    public WorksSupportName: string = String.empty;
    public Content: string = String.empty;
    public ExpectedCompletedDate: string = "";
    public selectedDeletedId: string = String.empty;
    public selectedDeletedName: string = String.empty;
    public projectName: string = String.empty;
    public projectIcon: string = String.empty;
    public wGName: string = String.empty;
    public wGIcon: string = String.empty;
    public index: number = 1;
    public size: number = 10;
    public pager: any = {};
    public totalRecords: number = 0;
    public selectedUser: string = "";
    public listUser: Array<any> = [];
    public Note: string = String.empty;
    public lstProjectInvole: any = [];
    public selectedProjectInvoleID: string = String.empty;
    public searchTypeProjectInvole: string = "1";
    public txtInvoleFromDates: string = String.empty;
    public txtInvoleToDates: string = String.empty;
    public listInvole: Array<any> = [];
    public searchInvoleKeyword: string = String.empty;
    public selectedListInvole: Array<any> = [];
    public mustChooseNextStepUser: boolean = true;
    private lstShowAllMember: Array<any> = [];
    public fileUploaded: Array<any> = [];
    public isAdmin: boolean = sessionStorage.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    public fileAttachment: Array<any> = [];
    public currentEditID: string = "";
    private ErrorWorksSupportName: string = String.empty;
    private ErrorContent: string = String.empty;
    private ErrorSelPrioriry: string = String.empty;
    private IsInformationTab: boolean = true;
    private MinDate: string = String.empty;
    private user: string = sessionStorage.get(CACHE_CONSTANT.strUserName);
    public detailPermisstion: Array<any> = [];
    private ErrorStepUser: string = String.empty;
    private FileUpdated: number = 0;
    private IsUploadFile: boolean = true;
    public deletedAttachFile: Array<any> = []; constructor(router: Router) {
        super();
        let self: Works = this;
        self.router = router;
        self.selectedProjectID = localStorage.getItem('projectId');
        self.selectedWorksGroupID = localStorage.getItem('worksGroupId');
        self.user = sessionStorage.get(CACHE_CONSTANT.strUserName);
        self.loadWorkGroup();
        self.loadProject();
        self.loadWorks();
        self.getPermisstion();
        self.getWorksGroupRole();
    }
    ngOnInit() {
        let self: Works = this;
        self.user = sessionStorage.get(CACHE_CONSTANT.strUserName);
        // this.loadScript('../../../resources/themes/nc/vendors/bootstrap-daterangepicker/daterangepicker.js');
        let $ = window.jQuery;
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip({ delay: 10000 });
        });
        this.setPage(this.index);
        this.MinDate = this.nowDate();
    }
    @ViewChild('uploadAdd') formFilesUploadAdd: FormFilesUpload;
    @ViewChild('uploadEdit') formFilesUploadEdit: FormFilesUpload;
    @ViewChild("dataPickerFrom") dataPickerFrom: FormDatePicker;
    @ViewChild("dataPickerTo") dataPickerTo: FormDatePicker;
    @ViewChild("dateRangePickerAdd") picker: FormDatePicker;
    @ViewChild("dateRangePickerEdit") pickerEdit: FormDatePicker;
    private loadProject() {
        let self: Works = this;
        if (this.isAdmin) {
            self.user = "administrator";
        }
        WorksGroupService.getAllProjectIsActivedByUser(self.user).then(function (items: Array<any>) {
            for (let i = 0; i < items.length; i++) {
                items[i].IconUrl = items[i].IconUrl === null ? "fa fa-folder" : items[i].IconUrl;
            }
            self.listProject = items;
            if (self.selectedProjectID !== null && self.listProject.length > 0) {
                let index = self.listProject.findIndex(obj => obj.WorksSupportProjectId.toString() === self.selectedProjectID.toString());
                if (index > -1) {
                    self.projectName = self.listProject[index].WorksSupportProjectName;
                    self.projectIcon = self.listProject[index].IconUrl;
                }
            }
            self.sortProjectNameIsDesc(self.listProject);
        });
    }
    private loadWorkGroup() {
        let self: Works = this;
        WorksGroupService.getWorksGroups(self.selectedProjectID).then(function (items: Array<any>) {
            for (let i = 0; i < items.length; i++) {
                items[i].IconUrl = items[i].IconUrl === null ? "fa fa-files-o" : items[i].IconUrl;
            }
            self.listWorksGroup = items;
            if (self.selectedWorksGroupID !== null && self.listWorksGroup.length > 0) {
                let index = self.listWorksGroup.findIndex(obj => obj.WorksSupportGroupId.toString() === self.selectedWorksGroupID.toString());
                if (index > -1) {
                    self.wGName = self.listWorksGroup[index].WorksSupportGroupName;
                    self.wGIcon = self.listWorksGroup[index].IconUrl;
                }
            }
        });
    }

    private loadWorks() {
        let self: Works = this;
        let $ = window.jQuery;
        WorksSerice.getWorksBy(self.selectedWorksGroupID, String.empty, self.index - 1, self.size, self.user).then(function (items: Array<any>) {
            if (items.length > 0) {
                self.listWorks = items;
                self.totalRecords = items[0].TotalRow;
            }
            else {
                self.listWorks = [];
                self.totalRecords = 0;
            }
            self.setPage(self.index);
        });
        if (self.selectedWorksGroupID !== null && self.listWorksGroup.length > 0) {
            let index = self.listWorksGroup.findIndex(obj => obj.WorksSupportGroupId.toString() === self.selectedWorksGroupID.toString());
            if (index > -1) {
                self.wGName = self.listWorksGroup[index].WorksSupportGroupName;
                self.wGIcon = self.listWorksGroup[index].IconUrl;
            }
        }
    }
    private onXuLyClick(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        WorksSerice.getWorkFlow(event, this.user).then(function (items: Array<any>) {
            self.listWorksFlow = items;
        });
        self.ErrorStepUser = String.empty;
        self.listUser = [];
        self.selectedStepID = "";
        self.selectedUser = "";
        self.Note = "";
    }
    // Search by IsDelete
    public onStatusChange(event: any) {
        // Get value of dropdown list
        let $ = window.jQuery;
        let currentIsDelete = event.currentTarget.value;
        let self: Works = this;
        WorksSerice.getWorksBy("", currentIsDelete, -1, -1, self.user).then(function (items: Array<any>) {
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
    public onPageSizeChange() {
        let self: Works = this;
        let $ = window.jQuery;
        self.index = 1;
        self.SearchByKey();
    }
    public onSearch() {
        let self: Works = this;
        let $ = window.jQuery;
        self.index = 1;
        self.SearchByKey();
    }
    // Search data by name
    public SearchByKey() {
        let $ = window.jQuery;
        let worksName = $("#worksName").val();
        let self: Works = this;
        WorksSerice.getWorksBy(self.selectedWorksGroupID, worksName, self.index - 1, self.size, self.user).then(function (items: Array<any>) {
            if (items.length > 0) {
                self.listWorks = items;
                self.totalRecords = items[0].TotalRow;
                self.setPage(self.index);
            }
            else {
                self.listWorks = [];
                self.totalRecords = 0;
                self.index = 1;
                self.setPage(self.index);
            }
        });
    }
    disableCheckBoxInvolve(id: any) {
        let rs: boolean = false;
        let self: Works = this;
        if (self.selectedListInvole.findIndex(_ => _.WorksId.toString() === id.toString()) > -1) rs = true;
        return rs;
    }
    private onAddNewWorksClick() {
        let self: Works = this;
        let $ = window.jQuery;
        this.IsInformationTab = true;
        this.IsAdd = true;
        this.FileUpdated = 0;
        prioritiesService.getPrioritiesByProjectId(self.selectedProjectID).then(function (items: Array<any>) {
            self.listPriority = items;
        });
        self.WorksSupportId = 0;
        self.WorksSupportName = "";
        self.Content = ""
        self.ExpectedCompletedDate = this.nowDate();
        self.picker.resetDate();
        self.selectedPriority = "";
        self.selectedListInvole = [];
        self.fileUploaded = [];
        self.errorMess.errors = [];
        self.ErrorWorksSupportName = String.empty;
        self.formFilesUploadAdd.reset();
        self.clearValidateWork();
        $('.nav-tabs a[href="#tab-works-info"]').tab('show');
        $("#modalAdd").modal('toggle');
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: set now datetime
     */
    private nowDate() {
        let objDate = new Date();
        let month = objDate.getMonth() + 1 < 10 ? "0" + (objDate.getMonth() + 1) : (objDate.getMonth() + 1).toString();
        let day = objDate.getDate() < 10 ? "0" + objDate.getDate() : objDate.getDate().toString();
        let getDate = (day + "/" + month + "/" + objDate.getFullYear());
        return getDate;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: change information tab
     */
    private changeInforTab() {
        if (this.IsInformationTab) {
            this.IsInformationTab = false;
        } else {
            this.IsInformationTab = true;
        }
    }

    public onAddWorks(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        let Id = this.WorksSupportId;
        let WorksSupportName = self.WorksSupportName;
        let Content = self.Content;
        let ExpectedCompletedDate = null;
        if(self.ExpectedCompletedDate !== "")
        {
            ExpectedCompletedDate = self.stringToDate(self.ExpectedCompletedDate, "dd/MM/yyyy", "/");
        }
        let WorksSupportPriorityId = self.selectedPriority === "" ? 0 : parseInt(self.selectedPriority);
        let ListInvole = self.selectedListInvole.map(_ => _.WorksId).toString();
        
        let EditModels = new EditModel(Id, WorksSupportName.trim(), self.user, Content,ExpectedCompletedDate , WorksSupportPriorityId, parseInt(self.selectedWorksGroupID), ListInvole, JSON.stringify(self.fileUploaded), "");
        $('.nav-tabs a[href="#tab-works-info"]').tab('show');
        if (this.validationPopupWork(true)) {
            WorksSerice.create(EditModels).then(function () {
                self.model = new WorksModel(self.i18nHelper);
                self.index = 1;
                self.loadWorks();
                $("#modalAdd").modal("hide");
                $(".modal-backdrop").modal("hide");
            }).error(function (error: any) {
                self.validateOnServerOfWork(error);
            });
        }
        this.IsInformationTab = true;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: validate the same work support name on  server. 
     */
    private validateOnServerOfWork(error: any): boolean {
        let self: Works = this;
        for (let i = 0; i < error.length; i++) {
            if (error[i].Key === "WorksSupportName") {
                this.ErrorWorksSupportName = error[i].Message;
                return false;
            }
        }
        return true;
    }

    private StartDate: string = "";
    private IsAdd: boolean = true;
    private onEditWorksClick(id: any) {
        let self: Works = this;
        let $ = window.jQuery;
        $('#modalEdit').fadeOut('slow', function () {
            $(this).fadeIn('slow');
        });
        self.clearValidateWork();
        this.IsAdd = false;
        this.IsInformationTab = true;
        prioritiesService.getPrioritiesByProjectId(self.selectedProjectID).then(function (items: Array<any>) {
            self.listPriority = items;
        });
        WorksSerice.getWorkDetail(id).then(function (items: any) {
            self.WorksSupportId = items.WorksSupportId;
            self.WorksSupportName = items.WorksSupportName;
            self.Content = items.Content;
            if (items.ExpectedCompletedDate != null) {
                self.ExpectedCompletedDate = self.stringAsDate(items.ExpectedCompletedDate);
                self.pickerEdit.resetDateEdit(self.ExpectedCompletedDate);
            } else {
                self.ExpectedCompletedDate = items.ExpectedCompletedDate;
            }
            self.selectedPriority = items.WorksSupportPriorityId;
            self.selectedListInvole = items.ListWorksInvole;
            self.fileAttachment = items.ListWorksSupportAttachment;
            self.currentEditID = id;
            self.FileUpdated = self.fileAttachment.length;
            self.fileUploaded = [];
            self.getPermisstionEdit(id);
            if (self.formFilesUploadEdit !== undefined) self.formFilesUploadEdit.reset();
            self.deletedAttachFile = [];
            $('.nav-tabs a[href="#tab-works-info"]').tab('show');
            $("#modalEdit").modal('toggle');
            $('#modalEdit').html();
        });
    }
    public onEditWorks(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        let Id = self.WorksSupportId;
        let WorksSupportName = self.WorksSupportName;
        let Content = self.Content;
        let ExpectedCompletedDate = null;
        if(self.ExpectedCompletedDate != "" && self.ExpectedCompletedDate != null)
        {
            ExpectedCompletedDate = self.stringToDate(self.ExpectedCompletedDate, "dd/MM/yyyy", "/");
        }
        let WorksSupportPriorityId = self.selectedPriority === "" ? 0 : parseInt(self.selectedPriority);
        let ListInvole = self.selectedListInvole.map(_ => _.WorksId).toString();
        let EditModels = new EditModel(Id, WorksSupportName.trim(), self.user, Content, ExpectedCompletedDate, WorksSupportPriorityId, parseInt(self.selectedWorksGroupID), ListInvole, JSON.stringify(self.fileUploaded), self.deletedAttachFile.toString());
        $('.nav-tabs a[href="#tab-works-info"]').tab('show');
        if (this.validationPopupWork(true)) {
            WorksSerice.create(EditModels).then(function () {
                self.model = new WorksModel(self.i18nHelper);
                self.index = 1;
                self.loadWorks();
                $("#modalEdit").modal("hide");
                $(".modal-backdrop").modal("hide");
            }).error(function (error: any) {
                self.validateOnServerOfWork(error);
            })
        }
        this.IsInformationTab = true;

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
        var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex], 12, 0, 0, 0);
        return formatedDate;
    }
    public stringAsDate(inputFormat: string) {
        function pad(s: any) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }
    public confirmDelete(id: any, name: any) {
        let self: Works = this;
        let $ = window.jQuery;
        self.selectedDeletedId = id;
        self.selectedDeletedName = name;
        $("#modalDelete").modal("toggle");
    }
    public onDeleteWorks() {
        let self: Works = this;
        let $ = window.jQuery;
        WorksSerice.delete(self.selectedDeletedId, self.user).then(function (event: any) {
            self.model = new WorksModel(self.i18nHelper);
            self.index = 1;
            self.loadWorks();
            $("#modalDelete").modal("hide");
            $(".modal-backdrop").modal("hide");
        });

    }
    public onProjectChange(id: any) {
        localStorage.setItem('projectId', id);
        this.router.navigate([route.worksgroup.worksgroups.name]);
    }
    public onWorksGroupChange(id: any) {
        let self: Works = this;
        let $ = window.jQuery;
        self.selectedWorksGroupID = id.toString();
        localStorage.setItem('worksGroupId', id);
        self.index = 1;
        self.loadWorks();
        self.getPermisstion();
        self.getWorksGroupRole();
        $("#worksName").val('');
    }
    public onPageChange(page: number) {
        let self: Works = this;
        self.setPage(page);
        self.SearchByKey();
    }
    public setPage(page: number) {
        let self: Works = this;
        if (page < 1) {
            return;
        }
        self.pager = pagingService.getPager(self.totalRecords, page, self.size);
        self.index = self.pager.currentPage;
    }
    public getStepUser(id: any) {
        let self: Works = this;
        self.listUser = [];
        self.selectedUser = String.empty;
        self.mustChooseNextStepUser = self.listWorksFlow[self.listWorksFlow.findIndex(_ => _.NextWorksSupportStepId.toString() === self.selectedStepID.toString())].IsMustChooseProcessUser;
        WorksSerice.getProcessUser(id, self.selectedStepID).then(function (items: Array<any>) {
            self.listUser = items;
        });
    }
    /**
     * Created by  :   Nguyen Van Huan
     * Created date:   25/08/2017
     * Description :   clear validat for process step
     */
    private clearValidateProcessStep() {
        this.ErrorStepUser = String.empty;
    }
    /**
     * Created by  :   Nguyen Van Huan
     * Created date:   25/08/2017
     * Description :   validat for process step
     * @param mustChooseNextStepUser 
     * @param selectedUser 
     */
    private validateProcessStep(mustChooseNextStepUser: boolean, selectedUser: any): boolean {
        this.ErrorStepUser = String.empty;
        if (mustChooseNextStepUser === true && selectedUser === "") {
            this.ErrorStepUser = "Vui lòng chọn người xử lý!";
            return false;
        } else {
            return true;
        }
    }
    public onProcessClicked(workSupportId: any) {
        let self: Works = this;
        let model: ProcessUserModel = new ProcessUserModel(workSupportId, parseInt(self.selectedStepID), self.user, self.selectedUser, self.Note);
        if (self.validateProcessStep(this.mustChooseNextStepUser, this.selectedUser)) {
            WorksSerice.saveWorksProcess(model).then(function (items: Array<any>) {
                self.model = new WorksModel(self.i18nHelper);
                self.loadWorks();
            });
        }
    }
    public getPermisstion() {
        let self: Works = this;

        WorksSerice.getWorkPermisstionByGroupId(self.user, parseInt(self.selectedWorksGroupID)).then(function (items: any) {
            self.isCanAdd = items.IsCanAddWorkssupport;
            self.isCanEdit = items.IsCanEditWorkssupport;
            self.isCanDel = items.IsCanDeleteWorkssupport;
        });
    }

    private onAddWorksInvoleClick() {
        let $ = window.jQuery;
        let self: Works = this;
        self.lstProjectInvole = [];
        self.selectedProjectInvoleID = String.empty;
        self.searchTypeProjectInvole = "1";
        self.listInvole = [];
        self.dataPickerFrom.resetDateSearch();
        self.dataPickerTo.resetDateSearch();
        self.searchInvoleKeyword = "";
        self.txtInvoleFromDates = "";
        self.txtInvoleToDates = ""
        WorksSerice.getWorksInvole().then(function (items: Array<any>) {
            self.lstProjectInvole = items;
        })
        $('#modal-add-works-group-related').modal('toggle');
    }
    public SearchInvole() {
        // Get value of dropdown list
        let $ = window.jQuery;
        let pageIndex = -1;
        let pageSize = -1;
        let self: Works = this;
        if (this.compareDate(this.txtInvoleFromDates, this.txtInvoleToDates)) {
            WorksSerice.getWorksInvoleBy(self.selectedProjectInvoleID.toString(), self.searchInvoleKeyword.toString(), parseInt(self.searchTypeProjectInvole), self.stringToDate(self.txtInvoleFromDates, "dd/MM/yyyy", "/"), self.stringToDate(self.txtInvoleToDates, "dd/MM/yyyy", "/"), pageIndex, pageSize).then(function (items: Array<any>) {
                self.listInvole = items;
                self.listInvole.forEach(element => {
                    if (self.selectedListInvole.findIndex(_ => _.WorksId === element.WorksId) > -1) element.state = true;
                    else element.state = false;
                });
            });
        } else {
            $("#modalErorr").modal("toggle");
        }
    }
    checkAll(ev: any) {
        let self: Works = this;
        self.listInvole.forEach(x => x.state = ev.target.checked)
    }

    isAllChecked() {
        let self: Works = this;
        if (self.listInvole.length === 0) return false;
        else return self.listInvole.every(_ => _.state);
    }
    private onSaveWorkInvole() {
        let self: Works = this;
        let $ = window.jQuery;
        let difference = self.listInvole
            .filter(x => x.state === true && self.selectedListInvole.findIndex(_ => _.WorksId === x.WorksId) == -1)
            .concat(self.selectedListInvole.filter(x => self.selectedListInvole.findIndex(_ => _.WorksId === x.WorksId) == -1));
        self.selectedListInvole.push.apply(self.selectedListInvole, difference);
        $('#modal-add-works-group-related').modal('hide');
    }
    private removeWorkInvole(id: any) {
        let self: Works = this;
        let index = self.selectedListInvole.find(_ => _.WorksId === id);
        self.selectedListInvole = self.selectedListInvole.removeItem(index);
    }
    private redirecttoDetail(id: any) {
        localStorage.setItem('workSupportDetailID', id);
        this.router.navigate([route.work.detail.name]);
    }
    // Start workgroup
    private Id: number = 0;
    private WorksSupportGroupName: string = String.empty;
    private WorksSupportProjectName: string = String.empty;
    private WorksSupportProjectNameShow: string = String.empty;
    private WorksSupportProjectId: number = 0;
    private Description: string = String.empty;
    private IsSystem: number = 0;
    private IsActived: number = 0;
    private WorksSupportGroupId: number = 0;
    private lstProject: Array<any> = [];
    private LstMemberRole: Array<any> = [];
    private lstGroupMembers: any = [];
    private LstWorksGroup: any = [];
    private LstWorkGroupMember: Array<any> = [];
    private LstProjectMember: Array<any> = [];
    private LstSearchProjectMember: Array<any> = [];
    private LstShowMemberDefault: Array<any> = [];
    private LstShowMemberRole: Array<any> = [];
    private LstNewMember: Array<any> = [];
    private LstDeleteMember: Array<any> = [];
    private LstEditMember: Array<any> = [];
    private IconUrl: string = String.empty;
    private CurrentProjectId: number = parseInt(this.selectedProjectID);
    private CurrentWorkGroupId: number;
    private IsSourceMember: boolean = false;
    private IsNormalceMember: boolean = false;
    private SelWorksGroupId: number;
    private SelWorksGroupName: string = String.empty;
    private objDelete: DeleteModel;
    private IsAddPopup: boolean = true;
    private IsAddWorkGroup: boolean = true;
    private ShowIsDefault: boolean = true;
    private IsCheckAll: boolean = false;
    private IsClickTab: boolean = true;
    private LstCurrentMember: Array<any> = [];
    private NumberMemberOfWorkGroup: number = 0;
    private KeySearchMember: string = String.empty;
    private CurrentWorkTypeId: any;
    private ShowRole: string = "default";
    private LstShowRole: Array<any> = [{ "roleId": "default", "roleName": "Tất cả" }, { "roleId": "role", "roleName": "Vai trò" }]
    private RoleOfWorkGroup: any = {};
    private SearchProjectMemberToAdd: string = String.empty;
    private ErrorWorkGroupName: string = String.empty;
    private ErrorDescription: string = String.empty;
    private SearchWorkGroup: boolean = false;
    private SearchMemberWorkGroup: boolean = false;
    private SearchMemberWorkGroupInfor: boolean = false;
    private txtSearchWorkGroup: string = String.empty;
    private IsRemoveMember: boolean = false;
    private CurrentIconUrlProject: string = "fa fa-folder";
    private ShowProjectTab: boolean = false;
    /**
   * Writen by  : Nguyen Van Huan
   * Create by  : 10/08/2017
   * Description: show edit popup of work group.
   * @param worksSupportGroupId 
   */
    @ViewChild(ErrorNewMessage)
    private errorMess: ErrorNewMessage;

    public onShowPopupEdit() {
        let $ = window.jQuery;
        let self: Works = this;
        this.CurrentWorkGroupId = parseInt(this.selectedWorksGroupID);
        this.CurrentProjectId = parseInt(this.selectedProjectID);
        this.IsAddPopup = false;
        this.IsAddWorkGroup = false;
        this.SearchProjectMemberToAdd = String.empty;
        this.IsClickTab = false;
        this.LstShowMemberDefault = [];
        this.LstShowMemberRole = [];
        this.LstSearchProjectMember = [];
        this.clearValidationPopupWorkGroup();
        WorksGroupService.getWorksGroupsByGroupId(this.CurrentWorkGroupId).then(function (item: any) {
            self.WorksSupportGroupName = item.WorksSupportGroupName;
            self.Description = item.Description;
            self.IconUrl = item.IconUrl === null ? "" : item.IconUrl;
            self.IsSystem = item.IsSystem;
            self.IsActived = item.IsActived;
        });
        this.KeySearchMember = String.empty;
        this.ShowIsDefault = true;
        this.ShowRole = "default";
        WorksGroupService.getRoleByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstMemberRole = items;
            self.setRoleNameInLstShowMemberRole();
            //self.findWorkGroup("", self.KeySearchMember);
            WorksGroupService.getWorksGroupMember(self.CurrentWorkGroupId).then(function (items: Array<any>) {
                self.LstWorkGroupMember = items;
                self.setMembersIsRole();
                self.setMembersIsDefault();
                self.findMemberWorkGroup("");
            });
        });
    }
    /**
    * Writen by  : Nguyen Van Huan
    * Create date: 10/08/2017
    * Description: clear validate on work group popup
    */
    private clearValidationPopupWorkGroup() {
        this.ErrorWorkGroupName = String.empty;
        this.ErrorDescription = String.empty;
        this.errorMess.errors = [];
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: set role of member list. 
     */
    private setRoleNameInLstShowMemberRole() {
        this.LstShowMemberRole = [];
        this.LstMemberRole.forEach(role => {
            let obj = {};
            obj["WorksSupportMemberRoleId"] = role.WorksSupportMemberRoleId;
            obj["WorksSupportMemberRoleName"] = role.WorksSupportMemberRoleName;
            obj["count"] = 0;
            obj["LstMember"] = [];
            this.LstShowMemberRole.push(obj);
        });
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: set role of member
     */
    private setMembersIsRole() {
        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.LstWorkGroupMember.forEach(worksGroupMember => {
            //1.1 duyệt từng đối tượng va so sánh roleid
            let index = this.LstShowMemberRole.findIndex(showMember => showMember.WorksSupportMemberRoleId === worksGroupMember.WorksSupportMemberRoleId);
            // kiểm tra xem roleid đã tồn tại chưa
            if (index !== -1) {
                // thêm phần tử và sắp xếp vào lstShowMember.
                //1.2 lấy phần tử trong lstMember trong lstShowMember theo roleid.
                let lstMember: Array<any> = this.LstShowMemberRole[index].LstMember;
                //1.3 thêm phần thành viên mới vào lstMember.
                lstMember.push(worksGroupMember);
                //1.4 xắp xếp theo thứ tự từ A->Z của trường FullName.
                lstMember.sort(this.compare);
                //1.5 thay lstMember cũ bằng lstMember mới đã được xắp xếp.
                this.LstShowMemberRole[index].LstMember = lstMember;
            }
        });
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: compare fullName de xap xep ten 
     * @param obj1 
     * @param obj2 
     */
    private compare(obj1: any, obj2: any) {
        return ((obj1.FullName).toLowerCase()).localeCompare((obj2.FullName).toLowerCase());
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: set member list is default style.
     */
    private setMembersIsDefault() {
        // Edit popup
        this.LstShowMemberDefault = [{ "Title": "Thành viên nguồn dự án", "LstMember": [] },
        { "Title": "Thành viên cơ bản", "LstMember": [] }];
        this.LstWorkGroupMember.forEach(projectMember => {
            if (projectMember.IsAutoAddMemberToWorksGroup === 1) {
                this.LstShowMemberDefault[0].LstMember.push(projectMember);
            } else {
                this.LstShowMemberDefault[1].LstMember.push(projectMember);
            }
        });
        // xap xep lai thanh vien theo FullName
        for (let i = 0; i < 2; i++) {
            let lstMember: Array<any> = this.LstShowMemberDefault[i].LstMember;
            lstMember.sort(this.compare);
            this.LstShowMemberDefault[i].LstMember = lstMember;
        }
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: find member of work group
     */
    private findMemberWorkGroup(keySearch: any) {
        this.NumberMemberOfWorkGroup = 0;
        this.SearchMemberWorkGroup = false;
        // tim thanh vien trong danh sach thanh vien theo vai tro
        keySearch = keySearch === undefined ? "" : keySearch;
        if (keySearch.trim() === "") {
            for (let i = 0; i < this.LstShowMemberRole.length; i++) {
                let lstMember: Array<any> = this.LstShowMemberRole[i].LstMember;
                let count = 0;
                lstMember.forEach(element => {
                    element.IsDeleted = 0;
                    count++;
                });
                this.LstShowMemberRole[i].count = count;
                this.NumberMemberOfWorkGroup += count;
            }
            this.SearchMemberWorkGroup = false;
        } else {
            for (let i = 0; i < this.LstShowMemberRole.length; i++) {
                let lstMember: Array<any> = this.LstShowMemberRole[i].LstMember;
                let countMember = lstMember.length;
                let count = 0;
                lstMember.forEach(member => {
                    member.IsDeleted = 1;
                    let fullName = (member.FullName === null) ? "" : member.FullName;
                    if ((fullName).toLowerCase().search(keySearch.toLowerCase()) >= 0) {
                        member.IsDeleted = 0;
                        count++;
                    }
                });
                this.LstShowMemberRole[i].count = count;
                this.NumberMemberOfWorkGroup += count;
            }
            if (this.NumberMemberOfWorkGroup > 0) {
                this.SearchMemberWorkGroup = false;
            } else {
                this.SearchMemberWorkGroup = true;
            }
        }
        // tim thanh vien trong danh sach mac dinh!
        if (keySearch.trim() === "") {
            for (let i = 0; i < this.LstShowMemberDefault.length; i++) {
                let lstMember: Array<any> = this.LstShowMemberDefault[i].LstMember;
                let count = 0;
                lstMember.forEach(element => {
                    element.IsDeleted = 0;
                    count++;
                });
                this.LstShowMemberDefault[i].count = count;

            }
        } else {
            for (let i = 0; i < this.LstShowMemberDefault.length; i++) {
                let lstMember: Array<any> = this.LstShowMemberDefault[i].LstMember;
                let countMember = lstMember.length;
                let count = 0;
                lstMember.forEach(member => {
                    member.IsDeleted = 1;
                    let fullName = (member.FullName === null) ? "" : member.FullName;
                    if ((fullName).toLowerCase().search(keySearch.toLowerCase()) >= 0) {
                        member.IsDeleted = 0;
                        count++;
                    }
                });
                this.LstShowMemberDefault[i].count = count;
            }
        }
    }
    /**
    * Writen by  : Nguyen van Huan
    * Create by  : 10.08.2017
    * Description: select icon on workgroup popup
    */
    private onIconIsSelected() {
        let $ = window.jQuery;
        let self: Works = this;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: check tab to show
     */
    private onClickMemberTab(tab: any) {
        // check popup is add or edit popup
        let $ = window.jQuery;
        let self: Works = this;
        if (this.IsClickTab) {
            this.IsClickTab = false;
        } else {
            this.IsClickTab = true;
        }

    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: change style view mode
     */
    private selectShowRole() {
        if (this.ShowRole === "default") {
            this.ShowIsDefault = false;
        } else {
            this.ShowIsDefault = true;
        }
        this.findMemberWorkGroup(this.KeySearchMember);
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: remove work group member
     * @param userName 
     */
    private removeWorkGroupMember(userName: any) {
        let $ = window.jQuery;
        let self: Works = this;
        this.ShowProjectTab = false;
        if (!this.RoleOfWorkGroup.IsCanEditWorksSupportGroup && !this.isAdmin) {
            return false;
        }
        WorksGroupService.checkUserProcess(userName, this.CurrentWorkGroupId).then(function (item: any) {
            if (item.NumberOfMember === 0) {
                // remove is role
                for (let i = 0; i < self.LstShowMemberRole.length; i++) {
                    let lstMember: Array<any> = self.LstShowMemberRole[i].LstMember;
                    let member = lstMember.find(member => member.UserName === userName);
                    if (member !== undefined) {
                        self.LstShowMemberRole[i].LstMember.removeItem(member);
                        break;
                    }
                }
                // remove is default;
                for (let i = 0; i < self.LstShowMemberDefault.length; i++) {
                    let lstMember: Array<any> = self.LstShowMemberDefault[i].LstMember;
                    let member = lstMember.find(member => member.UserName === userName);
                    if (member !== undefined) {
                        self.LstShowMemberDefault[i].LstMember.removeItem(member);
                        break;
                    }
                }
                self.findMemberWorkGroup(self.KeySearchMember);
            } else {
                self.IsRemoveMember = true;
                $("#modalMemberErorr").modal("toggle");
            }
        }).error(function (error: any) {
            self.IsRemoveMember = true;
            $("#modalMemberErorr").modal("toggle");
        });
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: checking user is processing work
     * @param userName 
     * @param workGroupId 
     */
    private checkUserProcessWorkGroup(userName: any, workGroupId: any): boolean {
        let $ = window.jQuery;
        WorksGroupService.checkUserProcess(userName, workGroupId).then(function (item: any) {
            let obj = item;
            if (obj.NumberOfMember > 0) {
                return true;
            } else {
                return false;
            }
        }).error(function (error: any) {
            return false;
        });
        return false;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: change member role
     * @param newRoleId 
     * @param userName 
     * @param oldRoleId 
     * @param newRoleName 
     */
    private changeMemberRole(newRoleId: any, userName: any, oldRoleId: any, newRoleName: any) {
        this.editMemberIntoMemberListByRoleId(userName, newRoleId, oldRoleId, newRoleName);
        this.findMemberWorkGroup(this.KeySearchMember);
    }
    /**
     * edit role of member and sort member in member list
     * @param userName 
     * @param newRoleId 
     * @param oldRoleId 
     * @param newRoleName 
     */
    private editMemberIntoMemberListByRoleId(userName: any, newRoleId: any, oldRoleId: any, newRoleName: any) {
        //1. tim index UserName theo oldRoleId
        let index = this.LstShowMemberRole.findIndex(workGroup => workGroup.WorksSupportMemberRoleId === parseInt(oldRoleId));
        //2. kiem tra project nao chua UserName nay.
        if (index !== -1) {
            //3. lấy danh sach lstMember trong lstShowMember theo roleid.
            let lstMember: Array<any> = this.LstShowMemberRole[index].LstMember;
            //4. tim user trong lstMember 
            let member = lstMember.find(mem => mem.UserName === userName);
            //5. remove ra khoai danh sach thanh vien.
            lstMember.removeItem(member);
            //5.1 cap nhat lai danh sach
            this.LstShowMemberRole[index].LstMember = lstMember;
            //6. tim chi so cua vai tro moi trong lstShowMember.
            let roleIndex = this.LstShowMemberRole.findIndex(showMember => showMember.WorksSupportMemberRoleId === parseInt(newRoleId));
            //7. cap nhat lai vao nhom vai tro moi trong lstShowmember.
            if (roleIndex !== -1) {
                let lstNewMember: Array<any> = this.LstShowMemberRole[roleIndex].LstMember;
                //8. thêm phần thành viên mới vào lstMember.
                member.WorksSupportMemberRoleId = parseInt(newRoleId);
                lstNewMember.push(member);
                //9. xắp xếp theo thứ tự từ A->Z của trường FullName.
                lstNewMember.sort(this.compare);
                //10. thay lstMember cũ bằng lstMember mới đã được xắp xếp.
                this.LstShowMemberRole[roleIndex].LstMember = lstNewMember;
            }
        }
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: show popup search user 
     */
    private showPopupSearchUser() {
        //1. load all project members.
        let self: Works = this;
        let $ = window.jQuery;
        this.SearchMemberWorkGroupInfor = false;
        this.IsCheckAll = false;
        this.LstSearchProjectMember = [];
        this.SearchProjectMemberToAdd = String.empty;
        WorksGroupService.getAllMemberByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstSearchProjectMember = items;
            self.filterMemberListAfterSearch();
        });
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: filter member list alfter search member.
     */
    private filterMemberListAfterSearch() {
        // check LstShowMemberRole is empty
        for (let i = 0; i < this.LstShowMemberRole.length; i++) {
            for (let j = 0; j < this.LstShowMemberRole[i].LstMember.length; j++) {
                let lstMem: Array<any> = this.LstShowMemberRole[i].LstMember;
                let mem = this.LstSearchProjectMember.find(member => member.UserName == lstMem[j].UserName);
                if (mem !== undefined) {
                    this.LstSearchProjectMember.removeItem(mem);
                }
            }
        }
        this.LstSearchProjectMember.sort(this.compareMemberWorkGroup);
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: compare member to sort by fullname
     * @param obj1 
     * @param obj2 
     */
    private compareMemberWorkGroup(obj1: any, obj2: any) {
        return ((obj1.FullName).toLowerCase()).localeCompare((obj2.FullName).toLowerCase());
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description:  find member in project member
     * @param keyName full name of member
     */
    private findMemberInProjectMember(keyName: any) {
        keyName = keyName === undefined ? "" : keyName;
        if (keyName.trim() === "") {
            for (let i = 0; i < this.LstSearchProjectMember.length; i++) {
                let lstMember: Array<any> = this.LstSearchProjectMember;
                lstMember.forEach(element => {
                    element.IsActived = 0;
                });
            }
            this.SearchMemberWorkGroupInfor = false;
        } else {
            let countSearch = 0;
            for (let i = 0; i < this.LstShowMemberDefault.length; i++) {
                let lstMember: Array<any> = this.LstSearchProjectMember;
                let countMember = lstMember.length;
                lstMember.forEach(member => {
                    member.IsActived = -1;
                    let fullName = (member.FullName === null) ? "" : member.FullName;
                    if ((fullName).toLowerCase().search(keyName.toLowerCase()) >= 0) {
                        member.IsActived = 0;
                        countSearch++;
                    }
                });
            }
            if (countSearch === 0) {
                this.SearchMemberWorkGroupInfor = true;
            } else {
                this.SearchMemberWorkGroupInfor = false;
            }
        }
    }
    /**
    * Writen by  : Nguyen van Huan
    * Create by  : 10.08.2017
    * Description: check all members when search member
    * @param event 
    */
    private selectUserWorkGroup(event: any) {
        let self: Works = this;
        let $ = window.jQuery;
        if (!this.IsCheckAll) {
            this.LstSearchProjectMember.forEach(element => {
                if (element.MemberUserName === null) {
                    element.MemberUserName = -1;
                }
            });
        } else {
            this.LstSearchProjectMember.forEach(element => {
                if (element.MemberUserName === -1) {
                    element.MemberUserName = null;
                }
            });
        }
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: check or uncheck a user when user is clicked
     * @param UserName 
     */
    private clickUser(UserName: any) {
        let $ = window.jQuery;
        let index = this.LstSearchProjectMember.findIndex(member => member.UserName === UserName);
        if (this.LstSearchProjectMember[index].MemberUserName === null) {
            this.LstSearchProjectMember[index].MemberUserName = -1;
        } else {
            this.LstSearchProjectMember[index].MemberUserName = null;
        }
    }
    /**
      * Writen by  : Nguyen van Huan
      * Create by  : 10.08.2017
      * Description: add new member into member list
      * @param event 
      */
    private addNewMemberWorkGroup(event: any) {
        let $ = window.jQuery;
        $('#modalMember').modal('hide');
        this.setSearchMembersIsRole();
        this.setSearchMembersIsDefault();
        this.findMemberWorkGroup(this.KeySearchMember);
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: sort member by default view
     */
    private setSearchMembersIsDefault() {
        this.LstSearchProjectMember.forEach(projectMember => {
            // thanh vien duoc chon
            if (projectMember.MemberUserName !== null) {
                if (projectMember.IsAutoAddMemberToWorksGroup === 1) {
                    this.LstShowMemberDefault[0].LstMember.push(projectMember);
                } else {
                    this.LstShowMemberDefault[1].LstMember.push(projectMember);
                }
            }
        });
        // xap xep lai thanh vien theo FullName
        for (let i = 0; i < 2; i++) {
            let lstMember: Array<any> = this.LstShowMemberDefault[i].LstMember;
            lstMember.sort(this.compare);
            this.LstShowMemberDefault[i].LstMember = lstMember;
        }
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: sort member by role
     */
    private setSearchMembersIsRole() {
        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.LstSearchProjectMember.forEach(projectMember => {
            if (projectMember.MemberUserName !== null) {
                //1.1 duyệt từng đối tượng va so sánh roleid
                let index = this.LstShowMemberRole.findIndex(showMember => showMember.WorksSupportMemberRoleId === projectMember.WorksSupportMemberRoleId);
                // kiểm tra xem roleid đã tồn tại chưa
                if (index !== -1) {
                    // thêm phần tử và sắp xếp vào lstShowMember.
                    //1.2 lấy phần tử trong lstMember trong lstShowMember theo roleid.
                    let lstMember: Array<any> = this.LstShowMemberRole[index].LstMember;
                    //1.3 thêm phần thành viên mới vào lstMember.
                    lstMember.push(projectMember);
                    //1.4 xắp xếp theo thứ tự từ A->Z của trường FullName.
                    lstMember.sort(this.compare);
                    //1.5 thay lstMember cũ bằng lstMember mới đã được xắp xếp.
                    this.LstShowMemberRole[index].LstMember = lstMember;
                }
            }
        });
    }
    /**
         * Writen by  : Nguyen van Huan
         * Create by  : 10.08.2017
         * Description: add or edit workgroup 
         */
    private onEditOrAddWorkGroup() {
        let self: Works = this;
        let $ = window.jQuery;
        let id = this.WorksSupportGroupId;
        let worksSupportGroupName = this.WorksSupportGroupName;
        let worksSupportProjectId = this.CurrentProjectId;
        let description = this.Description;
        let isActive = 1;
        let isSystem = 1;
        let iconUrl = this.IconUrl;
        let lstNewMember = this.getLstNewMember();
        //2. edit workgroup
        if (this.validationPopupWorkGroup()) {
            this.filterMemberListBeforeSubmit();
            let objWorksGroup = new UpdateModel(this.CurrentWorkGroupId, worksSupportProjectId, worksSupportGroupName, description, isActive, isSystem,
                iconUrl, self.user, this.LstNewMember, this.LstDeleteMember, this.LstEditMember);
            WorksGroupService.edit(objWorksGroup).then(function (item: any) {
                $('#modalUpdateWorkGroup').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.loadWorkGroup();
                self.getPermisstion();
                self.loadWorks();
            }).error(function (error: any) {
                self.validateOnServerOfWorkGroup(error);
                self.IsClickTab = true;
            });
        } else {
            self.IsClickTab = true;
        }
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: get new member list to submit on server
     */
    private getLstNewMember(): Array<any> {
        //1. xoa danh sach thanh vien (lstMember).
        this.LstNewMember = [];
        for (let i = 0; i < this.LstShowMemberRole.length; i++) {
            let lstMember: Array<any> = this.LstShowMemberRole[i].LstMember;
            for (let j = 0; j < lstMember.length; j++) {
                this.LstNewMember.push(lstMember[j]);
            }
        }
        return this.LstNewMember;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: validate workgroup popup
     */
    private validationPopupWorkGroup(): boolean {
        let workGrouptName = this.WorksSupportGroupName === null || this.WorksSupportGroupName === undefined ? "" : this.WorksSupportGroupName.trim();
        let description = this.Description === null || this.Description === undefined ? "" : this.Description.trim();
        let isError = true;
        this.ErrorWorkGroupName = String.empty;
        this.ErrorDescription = String.empty;
        this.errorMess.errors = [];
        // kiem tra rong
        if (workGrouptName.length === 0) {
            this.ErrorWorkGroupName = "Tên nhóm công việc là bắt buộc!";
            isError = false;
        }
        // kiem tra chuoi quá 200 kí tự
        if (workGrouptName.length > 200) {
            this.ErrorWorkGroupName = "Tên nhóm công việc không được vượt quá 200 kí tự!";
            isError = false;
        }
        // kiểm tra mô tả vượt quá 2000 kí tự
        if (description.length > 2000) {
            this.ErrorDescription = "Tên mô tả không được vượt quá 2000 kí tự!";
            isError = false;
        }
        return isError;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: filter member before submit
     */
    private filterMemberListBeforeSubmit() {
        // lay phan tu da xoa va them lay .
        this.LstEditMember = [];
        this.LstDeleteMember = [];
        this.LstNewMember = [];
        for (let i = 0; i < this.LstShowMemberRole.length; i++) {
            let lstMember: Array<any> = this.LstShowMemberRole[i].LstMember;
            for (let j = 0; j < lstMember.length; j++) {
                let member = this.LstWorkGroupMember.find(member => member.UserName === lstMember[j].UserName);
                //1. xoa phan tu neu tim thay. va luu vao danh sach edit
                if (member === undefined) {
                    //2. them neu khong tim thay.
                    this.LstNewMember.push(lstMember[j]);
                } else {
                    this.LstWorkGroupMember.removeItem(member);
                    this.LstEditMember.push(member);
                }
            }
        }
        this.LstDeleteMember = this.LstWorkGroupMember;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: validate the same name on server 
     */
    private validateOnServerOfWorkGroup(error: any): boolean {
        let self: Works = this;
        for (let i = 0; i < error.length; i++) {
            if (error[i].Key === "workGroupName") {
                this.ErrorWorkGroupName = error[i].Message;
                return false;
            }
        }
        return true;
    }
    private getWorksGroupRole() {
        let self: Works = this;
        if (!this.isAdmin) {
            let workTypeId = localStorage.getItem('WorkGroup_WorkTypeId');
            WorksGroupService.getWorksGroupRole(this.selectedProjectID, workTypeId, self.user).then(function (item: any) {
                self.RoleOfWorkGroup = item;
            });
        }
    }

    public onFileUploaded(event: any) {
        let self: Works = this;
        let obj: any = { FILEPATH: event.filePath, FILENAME: event.fileName, FILEID: event.fileId };
        self.fileUploaded.push(obj);
    }
    public onDeleteUploadedFile(id: any) {
        let self: Works = this;
        let $ = window.jQuery;
        let index = self.fileAttachment.findIndex(_ => _.AttachmentId.toString() === id.toString());
        if (index > -1) {
            self.deletedAttachFile.push(id);
            self.fileAttachment.removeItem(self.fileAttachment[index]);
        }

    }
    public onFileRemoved(event: any) {
        let self: Works = this;
        let obj: any = { FILEPATH: event.filePath, FILENAME: event.fileName, FILEID: event.fileId };
        let index = self.fileUploaded.findIndex(_ => _.FILEID === obj.FILEID);
        if (index > -1) {
            self.fileUploaded.removeItem(self.fileUploaded[index]);
        }
    }
    public onDownload(id: any) {
        let url = String.format("{0}{1}{2}{3}", configHelper.getAppConfig().api.downloadUrl, "DownloadFile?fileId=", id, "&type=1");
        window.open(url);
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 16.08.2017
     * Description: validate workgroup popup
     */
    private validationPopupWork(typeSubmit: any): boolean {
        let workName = this.WorksSupportName === null || this.WorksSupportName === undefined ? "" : this.WorksSupportName.trim();
        let content = this.Content === null || this.Content === undefined ? "" : this.Content.trim();
        let selected = this.selectedPriority === null || this.selectedPriority === undefined ? "" : this.selectedPriority;
        let isError = true;
        if (typeSubmit === true) {
            this.ErrorWorksSupportName = String.empty;
            this.ErrorContent = String.empty;
            this.ErrorSelPrioriry = String.empty;
            this.errorMess.errors = [];
            if (workName.length === 0) {
                this.ErrorWorksSupportName = "Tên công việc là bắt buộc!";
                isError = false;
            }
            if (workName.length > 500) {
                this.ErrorWorksSupportName = "Tên công việc không được vượt quá 500 kí tự!";
                isError = false;
            }
            if (selected.length === 0) {
                this.ErrorSelPrioriry = "Độ ưu tiên là bắt buộc!";
                isError = false;
            }
            if (content.length === 0) {
                this.ErrorContent = "Nội dung là bắt buộc!";
                isError = false;
            }
        } else {
            if (typeSubmit === "name") {
                this.ErrorWorksSupportName = String.empty;
                this.errorMess.errors = [];
                if (workName.length === 0) {
                    this.ErrorWorksSupportName = "Tên công việc là bắt buộc!";
                    isError = false;
                }
                if (workName.length > 500) {
                    this.ErrorWorksSupportName = "Tên công việc không được vượt quá 500 kí tự!";
                    isError = false;
                }

                return isError;
            }
            if (typeSubmit === "sel") {
                this.ErrorSelPrioriry = String.empty;
            }
            if (typeSubmit === "content") {
                this.ErrorContent = String.empty;
                if (content.length === 0) {
                    this.ErrorContent = "Nội dung là bắt buộc!";
                    isError = false;
                }
            }
        }
        return isError;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 16.08.2017
     * Description: remove validate
     */
    private clearValidateWork() {
        this.ErrorWorksSupportName = String.empty;
        this.ErrorContent = String.empty;
        this.ErrorSelPrioriry = String.empty;
        this.errorMess.errors = [];
    }
    /**
      * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: sort project name form a->z
     * @param lstProject 
     */
    private sortProjectNameIsDesc(lstProject: Array<any>) {
        lstProject.sort(this.compareProjectName);
    }
    /**
      * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: sort project name form a->z
     * @param lstProject 
     */
    private compareProjectName(obj1: any, obj2: any) {
        return ((obj1.WorksSupportProjectName).toLowerCase()).localeCompare((obj2.WorksSupportProjectName).toLowerCase());
    }
    private getPermisstionEdit(id: any) {
        let self: Works = this;
        self.user = sessionStorage.get(CACHE_CONSTANT.strUserName);
        WorkDetailSerice.getWorksNextStepPermission(self.user, id).then(function (items: Array<any>) {
            self.detailPermisstion = items;
        });
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
}