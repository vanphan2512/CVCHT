
import { Component, ViewChild, OnInit } from "angular2/core";
import { Router } from "angular2/router";
import { BasePage } from "../../../common/models/ui";
import { WorksTypesModel } from "./worksTypesModel";
import { DeleteModel } from "./worksTypesModel";
import { StepModel } from "./worksTypesModel";
import { CopyModel } from "./worksTypesModel";
import { AddWorkTypeModel } from "./worksTypesModel";
import worksTypeService from "../_share/services/worksTypeService";
import { Grid, PageActions, Page, SelectStatus } from "../../../common/directive";
import { UpdateWorksTypeModel } from "./updateModel";
import { EditModel } from "./editModel";
import { PageAction } from "../../../common/models/ui";
import route from "../_share/config/route";
import sessionStorage from "../../../common/storages/sessionStorage";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ValidationException } from "../../../common/models/exception";
import { RoleValue } from "../../../common/enum";
import priorityService from "../_share/services/priorityService";
import qualityService from "../_share/services/qualityService";
import statusService from "../_share/services/statusService";
import configHelper from "../../../common/helpers/configHelper";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
@Component({
    templateUrl: "app/modules/configurations/worksType/worksTypes.html",
    directives: [Grid, PageActions, Page, SelectStatus, ErrorMessage]
})
export class WorksTypes extends BasePage {
    public WorksSupportTypeId: number = 0;
    public WorksSupportTypeName: string = "";
    public IconUrL: string = "";
    public Description: string = "";
    public OrderIndex: number = 1;
    public IsActived: boolean;
    public IsSystem: boolean;
    public User: string = "";
    public actions: Array<PageAction> = [];
    private router: Router;
    public selectedValue: Array<any> = [];
    public selectedCopyValue: Array<any> = [];
    public objDelete: DeleteModel;
    public isCanAdd: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD) === "true" ? true : false;
    public isCanDelete: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    public userLogin = sessionStorage.get(CACHE_CONSTANT.strUserName);
    public roleValue: string = RoleValue.Admin;
    public isAdmin: boolean = sessionStorage.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    public listPriority: Array<any> = [];
    public listQualities: Array<any> = [];
    public listStep: Array<StepModel> = [];
    public StepNAME: string = "";
    public StepDESCRIPTION: string = "";
    public StepSTEPCOLORCODE: string = "#ffffff";
    public StepMAXPROCESSTIME: string = "";
    public WORKSSUPPORTSTEPPROGRESS: string = "";
    private currentTimeProcess: string = "1";
    private currentTimeMax: string = "1";
    public StepISINITSTEP: boolean = false; public StepISFINISHSTEP: boolean = false;
    public StepSTATUS: string = String.empty;
    public listNextStep: string[] = [];
    public listSelectedPriority: string[] = [];
    public listSelectedQualities: string[] = [];
    public StepCount: number = 0;
    public isEdit: boolean = false;
    public currentStep: string = String.empty;
    public StepID: string = String.empty;
    public stepName: string = String.empty;
    public deletedStepId: Array<string> = [];
    public isShowTextBoxEdit: boolean = false;
    public listStatus: Array<any> = [];
    public copyModel: CopyModel;
    private IsAdd: boolean = true;
    private isMustChooseProcessUser: boolean = false;
    private IsRename: boolean = true;
    constructor(router: Router) {
        super();
        let self: WorksTypes = this;
        self.router = router;
        self.model = new WorksTypesModel(self.i18nHelper);
        self.loadWorkType();
        self.loadPriority();
        self.loadQualities();
        self.loadStatus();
    }
    
    ngOnInit() {
        this.userLogin = sessionStorage.get(CACHE_CONSTANT.strUserName);
        this.isAdmin = sessionStorage.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
        let self: WorksTypes = this;
        let $ = window.jQuery;
        // $(function () {
        //     $('[data-toggle="popover"]').popover({ html: true });
        // });
        
        $('[data-toggle="popover"]').popover({ html: true });

        $("input.full-popover").ColorPickerSliders({
            color: self.StepSTEPCOLORCODE,
            placement: 'right',
            hsvpanel: true,
            previewformat: 'hex',
            sliders: false,
            swatches: false,
            onchange: function (container: any, color: any) {
                self.StepSTEPCOLORCODE = color.tiny.toHexString();
            }
        });
        $(document).ready(function() {
			let popOverSettings = {
				selector: '[rel="popover"]',
				content: $('#popover-content')
			};
            $(document).popover(popOverSettings);
        });

        $(document).on('click', function (e: any) {
            $('[data-toggle="popover"],[data-original-title]').each(function () {
                //the 'is' for buttons that trigger popups
                //the 'has' for icons within a button that triggers a popup
                if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                    (($(this).popover('hide').data('bs.popover') || {}).inState || {}).click = false  // fix for BS 3.3.6
                }
            });
        });
        $('body').on('hidden.bs.popover', function (e: any) {
            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false }
        });
    }
    @ViewChild(Grid)
    private gridComponent: Grid;
    private onAddNewWorkTypeClicked() {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        this.IsAdd = true;
        self.ResetData();
        this.clearValidate(true);
        this.clearValidateStep();
        $("#modalAdd").modal('toggle');
        $('#modalAdd').on('shown.bs.modal', function () {
            $('#modalAdd .modal-body').scrollTop(0);
        });
        self.IconUrL;
    }
    @ViewChild(ErrorMessage)
    private errorMess: ErrorMessage;

    public onAddClicked(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        self.isShowTextBoxEdit = false;

        this.validateStep();
        if (this.isValid(true) && this.validateStep()) {
            let typeModal: string = $('#modalAdd')[0].className.indexOf("in") > 0 ? "#modalAdd" : "#modalEdit";
            self.listSelectedPriority = [];
            self.listSelectedQualities = [];
            $(typeModal + ' input.cbPriority:checked').each(function () {
                self.listSelectedPriority.push($(this).attr('data-id'));
            });
            $(typeModal + ' input.cbQualities:checked').each(function () {
                self.listSelectedQualities.push($(this).attr('data-id'));
            });
            self.SaveCurrentStep();
            let user = sessionStorage.get(CACHE_CONSTANT.strUserName);
            let editModel = new AddWorkTypeModel(self.WorksSupportTypeId, self.WorksSupportTypeName, self.Description,
                self.IconUrL, self.IsActived, self.IsSystem, self.listSelectedPriority.toString(),
                self.listSelectedQualities.toString(), JSON.stringify(self.listStep), user, 0, self.deletedStepId.toString(), self.WORKSSUPPORTSTEPPROGRESS);

            worksTypeService.update(editModel).then(function () {

                self.model = new WorksTypesModel(self.i18nHelper);
                self.loadWorkType();
                $('#modalAdd').modal('hide');
                $('.modal-backdrop').modal('hide');
                configHelper.showNotify("add", true);
            }).error(function (error: Array<any>) {

                self.validateOnServer(error);
            });
        }
    }
    public validateOnServer(error: Array<any>) {
        let $ = window.jQuery;
        for (let i = 0; i < error.length; i++) {
            let key = error[i].Key;
            if (key === "WorksSupportTypeName") {
                this.ErrorTypeName = true;
            }
        }
    }
    // clear validation 
    public clearValidate(isAdd: boolean) {
        let $ = window.jQuery;
        this.validationErrors = new ValidationException();
        this.errorMess.errors = [];
        this.ErrorDescript = false;
        this.ErrorTypeName = false;
        $("#WorksSupportTypeName").removeClass("has-error");
        $("#Description").removeClass("has-error");
        $("#stepName").removeClass("has-error");
        $("#stepNameEdit").removeClass("has-error");
        $("#ProcessStep").removeClass("has-error");
        if (!isAdd) {
            $("#OrderIndex").removeClass("has-error");
        }
    }
    // validation at client
    public isValStepName(isAdd: boolean): boolean {
        let $ = window.jQuery;
        this.clearValidate(isAdd);
        let valAdd = $("#stepName").val();
        let valEdit = $("#stepNameEdit").val();
        if (isAdd) {
            if (!valAdd) {
                this.validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                $("#stepName").addClass("has-error");
            }
        }
        else {
            if (!valEdit) {
                this.validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                $("#stepNameEdit").addClass("has-error");
            }
        }
        this.validationErrors.throwIfHasError();
        return !this.validationErrors.hasError();
    }
    private ErrorTypeName: boolean = false;
    private ErrorDescription: boolean = false;
    private validationErrors: ValidationException = new ValidationException();
    public isValid(isAdd: boolean): boolean {
        this.validationErrors = new ValidationException();
        let $ = window.jQuery;
        this.clearValidate(isAdd);
        let lenName = (this.WorksSupportTypeName === null ? 0 : this.WorksSupportTypeName.length);
        let lenDesc = (this.Description === null ? 0 : this.Description.length);
        let orderIndex: number = this.OrderIndex;
        let stepCount = $("#ProcessStep li").length;
        let self: WorksTypes = this;
        let isVal = (isAdd !== true) ? ($("#stepNameEdit").val() === undefined ? false : true) : ($("#stepName").val() === undefined ? false : true);
        if (!this.WorksSupportTypeName) {
            this.validationErrors.add("WorksSupportTypeName", {}, "Tên loại công việc là bắt buộc!");
            this.ErrorTypeName = true;
        }
        if (!isAdd) {
            if (orderIndex <= 0 || orderIndex % 1 != 0 || orderIndex > 999) {
                this.validationErrors.add("OrderIndex", [], "Thứ tự loại công việc không hợp lệ!");
                $("#OrderIndex").addClass("has-error");
            }
        }
        if (self.listStep.length < 2) {
            this.validationErrors.add("#ProcessStep", {}, "Quy trình xử lý công việc là bắt buộc!");
            //  $("#ProcessStep").addClass("has-error");
        } else {
            let valAdd = $("#stepName").val();
            let valEdit = $("#stepNameEdit").val();
            if (isAdd) {
                if (isVal && !valAdd) {
                    this.validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                    //  $("#stepName").addClass("has-error");
                }
            }
            else {
                if (isVal && !valEdit) {
                    this.validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                    // $("#stepNameEdit").addClass("has-error");
                }
            }
            if (self.listStep.findIndex(obj => obj.StepISINITSTEP === true || obj.StepISINITSTEP.toString() === "1") === -1)
                this.validationErrors.add("StepISINITSTEP", {}, "Phải có bước khởi tạo!");
            if (self.listStep.findIndex(obj => obj.StepISFINISHSTEP === true || obj.StepISFINISHSTEP.toString() === "1") === -1)
                this.validationErrors.add("StepISFINISHSTEP", {}, "Phải có bước hoàn thành!");
        }
        if (lenName > 200) {
            this.validationErrors.add("WorksSupportTypeName", {}, "Tên loại công việc không được vượt quá 200 kí tự!");
            this.ErrorDescription = true;
        }
        if (lenDesc > 2000) {
            this.validationErrors.add("Description", {}, "Mô tả không được vượt quá 2000 kí tự!");
            $("#Description").addClass("has-error");
        }

        if (self.listStep.findIndex(obj => obj.StepSTATUS === "") > -1) {
            this.validationErrors.add("StepSTATUS", {}, "Phải chọn trạng thái bước xử lý!");
        }
        this.validationErrors.throwIfHasError();
        return !this.validationErrors.hasError();
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 23.08.2017
     * Description: valid for work type name.
     */
    private validErrorTypeName() {
        let $ = window.jQuery;
        let lenName = (this.WorksSupportTypeName === null ? 0 : this.WorksSupportTypeName.length);
        if (!this.WorksSupportTypeName) {
            this.ErrorTypeName = true;
            this.clearInvalid("WorksSupportTypeName");
            this.errorMess.errors.push({ key: "WorksSupportTypeName", msg: "Tên loại công việc là bắt buộc!" });
        } else if (lenName > 200) {
            this.clearInvalid("WorksSupportTypeName");
            this.errorMess.errors.push({ key: "WorksSupportTypeName", msg: "Tên loại công việc không được vượt quá 200 kí tự!" });
            this.ErrorTypeName = true;
        } else {
            this.clearInvalid("WorksSupportTypeName");
            this.ErrorTypeName = false;
        }
    }
    /**
    * Writen by  : Nguyen van Huan
    * Create by  : 23.08.2017
    * Description: valid for work type name.
    */
    private ErrorDescript: boolean = false;
    private validErrorDescription() {
        let $ = window.jQuery;
        let lenDesc = (this.Description === null ? 0 : this.Description.length);
        if (lenDesc > 2000) {
            this.clearInvalid("Description");
            this.errorMess.errors.push({ key: "Description", msg: "Mô tả không được vượt quá 2000 kí tự!" });
            this.ErrorDescript = true;
        } else {
            this.clearInvalid("Description");
            this.ErrorDescript = false;
        }
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 23.08.2017
     * Description: clear valid for work type name.
     * @param keyName key error to remove
     */
    private clearInvalid(keyName: any) {
        let arrErr = this.errorMess.errors;
        for (let i = 0; i < arrErr.length; i++) {
            if (arrErr[i].key === keyName) {
                arrErr.splice(i, 1);
            }
        }
        this.errorMess.errors = arrErr;
    }

    public onCopyClick(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        this.selectedCopyValue = new Array();
        let lstId: Array<string> = [];
        let grid = this.gridComponent.grid.rows(".selected").data();
        for (let i = 0; i < grid.length; i++) {
            this.selectedCopyValue.push({ id: grid[i]["WorksSupportTypeId"], name: grid[i]["WorksSupportTypeName"] });
            lstId.push(grid[i]["WorksSupportTypeId"]);
        }
        self.copyModel = new CopyModel(lstId.toString(), sessionStorage.get(CACHE_CONSTANT.strUserName));
    }

    /**
     * Copy Wors
     * 
     * @param {*} event 
     * @memberof WorksTypes
     */
    public onCopyWorksTypeClick(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        worksTypeService.copyWorksType(this.copyModel).then(function (event: any) {
            $("#copyModel").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksTypesModel(self.i18nHelper);
            configHelper.showNotify("copy", true);
            self.loadWorkType();
        });
    }

    /**
     * Show popup delete
     * 
     * @param {*} event 
     * @memberof WorksTypes
     */
    public onShowPopupDelete(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        let grid = this.gridComponent.grid.rows(".selected").data();
        for (let i = 0; i < grid.length; i++) {
            if (grid[i]["IsSystem"] !== true && this.userLogin !== this.roleValue) {
                this.selectedValue.push({ id: grid[i]["WorksSupportTypeId"], name: grid[i]["WorksSupportTypeName"], IsSystem: grid[i]["IsSystem"] });
                lstId.push(grid[i]["WorksSupportTypeId"]);
            }
            if (this.userLogin === RoleValue.Admin) {
                this.selectedValue.push({ id: grid[i]["WorksSupportTypeId"], name: grid[i]["WorksSupportTypeName"], IsSystem: grid[i]["IsSystem"] });
                lstId.push(grid[i]["WorksSupportTypeId"]);
            }
        }
        self.objDelete = new DeleteModel(lstId.toString(), sessionStorage.get(CACHE_CONSTANT.strUserName));
    }

    public onDeleteWorkTypes(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        worksTypeService.delete(this.objDelete).then(function (event: any) {
            $("#modal").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksTypesModel(self.i18nHelper);
            configHelper.showNotify("delete", true);
            self.loadWorkType();
            self.gridComponent.grid.columns().checkboxes.deselectAll();
        });
    }
    private loadWorkType() {
        let self: WorksTypes = this;
        worksTypeService.getWorksTypes().then(function (items: Array<any>) {
            self.model.importWorksTypes(items);
        });
    }
    private loadPriority() {
        let self: WorksTypes = this;
        priorityService.getPrioritiesIsActived().then(function (items: Array<any>) {
            self.listPriority = items;
        });
    }
    private loadQualities() {
        let self: WorksTypes = this;
        qualityService.getQualiliesActived().then(function (items: Array<any>) {
            self.listQualities = items;
        });
    }
    private loadStatus() {
        let self: WorksTypes = this;
        statusService.getStatusActived().then(function (items: Array<any>) {
            self.listStatus = items;
        });
    }
    // Get data by status : 1/0
    public onWorkTypeChange(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        let currentIsDelete = event.currentTarget.value;
        let isCanDelete: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        let isCanEdit: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
        let worksTypeName = $("#worksTypeName").val();
        // get data of dropdownlist.
        worksTypeService.getWorkTypeBy(worksTypeName, currentIsDelete, 0, 1000).then(function (items: Array<any>) {
            self.model.importWorksTypes(items);
        });

        if (currentIsDelete === "1") {
            $("#btnDeletePopup").css("display", "none");
            if (isCanDelete) {
                self.gridComponent.grid.column(0).visible(false);
            }
            if (isCanEdit) {
                var num = self.gridComponent.grid.column.length - 1;
                self.gridComponent.grid.column(num).visible(false);
            }
        } else {
            $("#btnDeletePopup").css("display", "inline-block");
            if (isCanDelete) {
                self.gridComponent.grid.column(0).visible(true);
            }
            if (isCanEdit) {
                var num = self.gridComponent.grid.column.length - 1;
                self.gridComponent.grid.column(num).visible(true);
            }

        }
    }

    public onToggleSelectChanged(event: any) {
        let self: WorksTypes = this;
        let model: UpdateWorksTypeModel = new UpdateWorksTypeModel();
        model.import(event.item);
        worksTypeService.update(model).then(function (event: any) {
            self.model = new WorksTypesModel(self.i18nHelper);
            self.loadWorkType();
        });

    }

    // Search data by name
    public SearchByKey(event: any) {
        let $ = window.jQuery;
        let worksTypeName = $("#worksTypeName").val();
        let currentIsDelete = $(".custom-select .form-control").val();
        let self: WorksTypes = this;
        worksTypeService.getWorkTypeBy(worksTypeName, currentIsDelete, 0, 1000).then(function (items: Array<any>) {
            self.model.importWorksTypes(items);
        });
    }

    public onIconSelectChanged(event: any) {

        let self: WorksTypes = this;
        let model: UpdateWorksTypeModel = new UpdateWorksTypeModel();
        event.item.IconUrl = event.icon;
        model.import(event.item);
        worksTypeService.update(model).then(function (event: any) {
            self.model = new WorksTypesModel(self.i18nHelper);
            self.loadWorkType();
        });
    }
    public onSaveClicked(event: any): void {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        if (this.isValidAdd())
            worksTypeService.create(this.model).then(function () {
                $("#modalAdd").modal("hide");
                $(".modal-backdrop").modal("hide");
                self.model = new WorksTypesModel(self.i18nHelper);
                self.loadWorkType();
            });
    }
    public onEditWorkTypeClicked(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        this.IsAdd = false;
        this.clearValidate(false);
        this.clearValidateStep();
        self.ResetData();
        if ($("#modalEdit").hasClass("has-error") == false) {
            $(".alert.alert-danger.error-msg").remove();
        }
        worksTypeService.get(event.item.WorksSupportTypeId).then(function (item: any) {
            self.WorksSupportTypeId = item.WorksSupportTypeId;
            self.WorksSupportTypeName = item.WorksSupportTypeName;
            self.Description = item.Description;
            self.IconUrL = item.IconUrl;
            self.IsActived = item.IsActived;
            self.IsSystem = item.IsSystem;
            self.OrderIndex = item.OrderIndex;
            self.listStep = [];
            self.listSelectedPriority = item.ListWorksSupportTypePriority == null ? [] : item.ListWorksSupportTypePriority.map(function (a: any) { return a.WorksSupportPriorityId; });
            self.listSelectedQualities = item.ListWorksSupportTypeQuality == null ? [] : item.ListWorksSupportTypeQuality.map(function (a: any) { return a.WorksSupportQualityId; });
            for (var index = 0; index < item.ListWorksSupportTypeWorkFlow.length; index++) {
                var element = item.ListWorksSupportTypeWorkFlow[index];
                let Step: StepModel = new StepModel(element.WorksSupportStepId.toString(),
                    element.WorksSupportStepName,
                    element.Description,
                    element.StepColorCode,
                    element.MaxProcessTime,
                    element.IsInitStep,
                    element.IsFinishStep,
                    element.ListWorksSupportTypeWfNx == null ? [] : element.ListWorksSupportTypeWfNx.map(function (a: any) { return a.NextWorksSupportStepsId.toString(); }),
                    false,
                    element.WorksSupportStatusId,
                    element.WorksSupportStepProgress,
                    element.IsMustChooseProcessUser === 1 ? true : false
                )
                self.listStep.push(Step);
            }
            if (self.listStep.length > 0) {
                let StepModel: StepModel = self.listStep[0];
                self.StepID = StepModel.StepID;
                self.StepNAME = StepModel.StepNAME;
                self.StepDESCRIPTION = StepModel.StepDESCRIPTION;
                self.StepSTEPCOLORCODE = StepModel.StepSTEPCOLORCODE;
                self.StepMAXPROCESSTIME = StepModel.StepMAXPROCESSTIME;
                self.WORKSSUPPORTSTEPPROGRESS = StepModel.WORKSSUPPORTSTEPPROGRESS;
                self.StepISINITSTEP = StepModel.StepISINITSTEP;
                self.StepISFINISHSTEP = StepModel.StepISFINISHSTEP;
                self.listNextStep = StepModel.listNextStep;
                self.currentStep = StepModel.StepID;
                self.StepSTATUS = StepModel.StepSTATUS;
                self.isMustChooseProcessUser = StepModel.isMustChooseProcessUser;
            }
            self.StepCount = 0;
            self.isEdit = false;
        });
        $('#modalEdit').on('shown.bs.modal', function () {
            $('#modalEdit .modal-body').scrollTop(0);
        });
        $("#modalEdit").modal('toggle');

    }
    public onEditClicked(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        this.validateStep();
        if (this.isValid(false) && this.validateStep()) {
            let typeModal: string = $('#modalAdd')[0].className.indexOf("in") > 0 ? "#modalAdd" : "#modalEdit";

            self.listSelectedPriority = [];
            self.listSelectedQualities = [];
            $(typeModal + ' input.cbPriority:checked').each(function () {
                self.listSelectedPriority.push($(this).attr('data-id'));
            });
            $(typeModal + ' input.cbQualities:checked').each(function () {
                self.listSelectedQualities.push($(this).attr('data-id'));
            });
            self.SaveCurrentStep();
            let user = sessionStorage.get(CACHE_CONSTANT.strUserName);
            let editModel = new AddWorkTypeModel(self.WorksSupportTypeId, self.WorksSupportTypeName, self.Description, self.IconUrL, self.IsActived, self.IsSystem, self.listSelectedPriority.toString(), self.listSelectedQualities.toString(), JSON.stringify(self.listStep),
                user, self.OrderIndex, self.deletedStepId.toString(), self.WORKSSUPPORTSTEPPROGRESS)
            worksTypeService.update(editModel).then(function () {
                self.model = new WorksTypesModel(self.i18nHelper);
                configHelper.showNotify("edit", true);
                $('#modalEdit').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.loadWorkType();
            })
        }
    }
    // cancel add
    public onCancelAdd() {
        let $ = window.jQuery;
        $('#modalAdd').modal('hide');
        $('.modal-backdrop').modal('hide');
    }
    // cancel edit
    public onCancelEdit() {
        let $ = window.jQuery;
        $('#modalEdit').modal('hide');
        $('.modal-backdrop').modal('hide');
    }
    public isValidEdit(): boolean {
        let validationErrors: ValidationException = new ValidationException();
        if (!this.WorksSupportTypeName) {
            validationErrors.add("configurations.addWorksType.validation.nameIsRequire");
        }
        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }
    public isValidAdd(): boolean {
        let validationErrors: ValidationException = new ValidationException();
        let $ = window.jQuery;
        if (!this.WorksSupportTypeName) {
            validationErrors.add("WorksSupportTypeName", [], "Tên Loại công việc phải bắt buộc!");
            $('#WorksSupportTypeNameEdit').addClass("has-error");
        }

        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }
    public resetValidation() {
        let controls = document.getElementsByClassName("has-error");
        for (var i = 0; i < controls.length; i++) {
            controls[i].classList.remove('has-error');
        }
    }
    public onIconSelected() {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrL = icon;
            $("#myIconModal").modal("hide");
        });
    }
    public onErrorEvent(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        event.forEach(function (error: any) {
            let key = error.key;
            let controls = document.getElementsByName(key);
            for (var i = 0; i < controls.length; i++) {
                controls[i].className += " has-error";
            }
        });
    }
    public ResetData() {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        self.WorksSupportTypeId = 0;
        self.WorksSupportTypeName = "";
        self.Description = "";
        self.IconUrL = "";
        self.IsActived = true;
        self.IsSystem = false;
        self.StepID = "";
        self.StepNAME = "";
        self.StepDESCRIPTION = "";
        self.StepSTEPCOLORCODE = "#ffffff";
        self.StepMAXPROCESSTIME = "";
        self.isMustChooseProcessUser = false;
        self.StepISINITSTEP = false;
        self.StepISFINISHSTEP = false;
        self.listStep = [];
        self.listNextStep = [];
        self.StepCount = 0;
        self.isEdit = false;
        self.currentStep = "";
        self.listSelectedQualities = [];
        self.listSelectedPriority = [];
        self.isShowTextBoxEdit = false;
        self.StepSTATUS = self.listStatus !== null ? self.listStatus[0].WorksSupportStatusId : "";
        self.WORKSSUPPORTSTEPPROGRESS = "";
        $('input.cbPriority:checked').each(function () {
            $(this).removeAttr("checked");
        });
        let selectedQualities: any = [];
        $('input.cbQualities:checked').each(function () {
            $(this).removeAttr("checked");
        });
        self.deletedStepId = [];
    }


    public SaveCurrentStep() {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        let selectedNextStep: Array<any> = [];
        let typeModal: string = $('#modalAdd')[0].className.indexOf("in") > 0 ? "#modalAdd" : "#modalEdit";
        if (self.currentStep !== "") {
            //if actived step doesn't in edit mode, allow to save data
            let index = self.listStep.findIndex(obj => obj.StepID === self.currentStep);
            if (index !== -1 && self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].isEdit === false) {
                self.isShowTextBoxEdit = false;
                let OldStep = self.currentStep;
                let Step: StepModel = new StepModel(OldStep,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepNAME,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepDESCRIPTION,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepSTEPCOLORCODE = self.StepSTEPCOLORCODE,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepMAXPROCESSTIME,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepISINITSTEP,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepISFINISHSTEP,
                    self.listNextStep,
                    self.isEdit,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepSTATUS,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].WORKSSUPPORTSTEPPROGRESS,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].isMustChooseProcessUser
                )
                if (self.listStep.findIndex(obj => obj.StepID === OldStep) === -1) {
                    self.listStep.push(Step);
                } else {
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === OldStep)] = Step;
                    self.StepSTEPCOLORCODE = "#ffffff";
                }
            }
        }
    }
    public onTabClick(event: any, addOrEdit: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        // validate step
        if (!this.validateStep()) {
            return false;
        }
        self.currentStep = event.target.id;
        let StepId = event.target.id;
        this.SaveCurrentStep();
        if (self.listStep.findIndex(obj => obj.StepID !== self.currentStep) !== -1) {            
            self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].isEdit = false;
        }
        this.isShowTextBoxEdit = false;
        this.currentSelectStep = this.currentStep;
        if (self.listStep.findIndex(obj => obj.StepID === StepId) === -1) {
            self.StepID = "";
            self.StepNAME = "";
            self.StepDESCRIPTION = "";
            self.StepSTEPCOLORCODE = "#ffffff";
            self.StepMAXPROCESSTIME = "";
            self.StepISINITSTEP = false;
            self.StepISFINISHSTEP = false;
            self.listNextStep = [];
            self.StepSTATUS = String.empty;
            self.WORKSSUPPORTSTEPPROGRESS = "";
            self.isMustChooseProcessUser = false;
        }
        else {
            let Step: StepModel = self.listStep[self.listStep.findIndex(obj => obj.StepID === StepId)];
            self.StepID = Step.StepID;
            self.StepNAME = Step.StepNAME;
            self.StepDESCRIPTION = Step.StepDESCRIPTION;
            self.StepSTEPCOLORCODE = Step.StepSTEPCOLORCODE;
            self.StepMAXPROCESSTIME = Step.StepMAXPROCESSTIME;
            self.StepISINITSTEP = Step.StepISINITSTEP;
            self.StepISFINISHSTEP = Step.StepISFINISHSTEP;
            self.listNextStep = Step.listNextStep;
            self.StepSTATUS = Step.StepSTATUS;
            self.WORKSSUPPORTSTEPPROGRESS = Step.WORKSSUPPORTSTEPPROGRESS;
            self.isMustChooseProcessUser = Step.isMustChooseProcessUser;
        }
    }
    public renameStep(id: any, addOrEdit: any) {
        let $ = window.jQuery;
        this.listStep[this.listStep.findIndex(obj => obj.StepID === id)].isEdit = true;
        if (addOrEdit === "add") {
            $('#Add-Span-' + id).popover('hide');
        } else {
            $('#Edit-Span-' + id).popover('hide');
        }
        this.SaveCurrentStep();
    }
    public addStep(add: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        let popOverSettings = {
            selector: '[rel="popover"]',
            content: $('#popover-content')
        };
        $(document).popover(popOverSettings);
        if (!this.validateStep()) {
            return false;
        }
        let isVal = (add !== true) ? $("#stepNameEdit").val() === undefined ? false : true : $("#stepName").val() === undefined ? false : true;
        if (isVal) {
            return false;
        }
        if (self.isShowTextBoxEdit === false) {
            self.isShowTextBoxEdit = true;
            if (self.listStep.length > 0) self.SaveCurrentStep();
            self.StepCount--;
            self.StepNAME = "";
            self.StepDESCRIPTION = "";
            self.StepSTEPCOLORCODE = "#ffffff";
            self.StepMAXPROCESSTIME = "";
            self.StepISINITSTEP = false;
            self.StepISFINISHSTEP = false;
            self.isMustChooseProcessUser = false;
            self.listNextStep = [];
            let Step: StepModel = new StepModel(self.StepCount.toString(),
                "",
                self.StepDESCRIPTION,
                self.StepSTEPCOLORCODE,
                self.StepMAXPROCESSTIME = "",
                self.StepISINITSTEP,
                self.StepISFINISHSTEP,
                self.listNextStep,
                true,
                self.listStatus !== null ? self.listStatus[0].WorksSupportStatusId : "",
                self.WORKSSUPPORTSTEPPROGRESS = "",
                self.isMustChooseProcessUser
            )
            self.listStep.unshift(Step);
            self.currentStep = self.StepCount.toString();
            self.StepSTEPCOLORCODE = "#ffffff";
        }
    }

    public onNextStepSelected(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        let idVal12 = event.target.id;
        let idVal = event.target.value;
        let index: number = self.listNextStep.indexOf(idVal);
        if (event.target.checked === false) {
            if (index > -1) self.listNextStep.removeItem(self.listNextStep[index]);

        }
        else {
            if (index === -1) self.listNextStep.push(idVal);
        }
    }
    public onDelete(event: any) {
        let id = event.target.parentNode.parentElement.parentElement.parentElement.id;
        let self: WorksTypes = this;
        let $ = window.jQuery;
        let indexRemove = self.listStep.findIndex(obj => obj.StepID === id);
        if (indexRemove >= 0) {
            self.listStep.splice(indexRemove, 1);
            self.clearValidateStep();
            if (id > 0) self.deletedStepId.push(id);
        }
        // de gia tri mac dinh sau khi xoa
        if (self.listStep.length > 0) {
            if (id === self.currentStep) {
                self.currentStep = self.listStep[0].StepID;
            }
        } else {
            self.currentStep = "";
        }
        self.isShowTextBoxEdit = false;
        return false;
    }
    public onRename(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        if (this.validateStep()) {
            if (self.listStep.findIndex(obj => obj.StepID === this.currentStep) !== -1) {
                self.listStep[self.listStep.findIndex(obj => obj.StepID === this.currentStep)].isEdit = false;
            }
            let id = event.target.parentNode.parentElement.parentElement.parentElement.id;
            this.currentStep = id;
            self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].isEdit = true;
            // self.isShowTextBoxEdit = true;
            $('[rel="popover"]').popover('hide');
        }
    }
    public setInit(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        let id = event.target.parentNode.parentElement.parentElement.parentElement.id;
        self.listStep.forEach(element => {
            element.StepISINITSTEP = false;
        });
        if (self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISFINISHSTEP === true || self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISFINISHSTEP.toString() === "1") self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISFINISHSTEP = false;
        self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISINITSTEP = true;
        $('[rel="popover"]').popover('hide');
    }
    public setFinish(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        let id = event.target.parentNode.parentElement.parentElement.parentElement.id;
        self.listStep.forEach(element => {
            element.StepISFINISHSTEP = false;
        });
        if (self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISINITSTEP === true || self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISINITSTEP.toString() === "1") self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISINITSTEP = false;
        self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].StepISFINISHSTEP = true;
        $('[rel="popover"]').popover('hide');
    }
    private ErrorStepProgress: boolean = false;
    private ErrorMaxTime: boolean = false;
    public onEnter(event: any) {
        let $ = window.jQuery;
        if (this.validateStep()) {
            let self: WorksTypes = this;
            self.listStep[self.listStep.findIndex(obj => obj.StepID === event.target.id)].isEdit = false;
            self.isShowTextBoxEdit = false;
        } else {
            event.preventDefault();
            this.isShowTextBoxEdit = true;
            return false;
        }
        // }
    }
    private ErrorStepName: boolean = false;
    private validateStep() {
        this.clearValidateStep();
        let error: boolean = true;
        let step = this.listStep.find(step => step.isEdit === true);
        let stepCurrent = this.listStep.find(step => step.StepID === this.currentStep);
        if (step !== undefined) {
            let processsStep: number = Number(step.WORKSSUPPORTSTEPPROGRESS);
            let processTime: number = Number(step.StepMAXPROCESSTIME);
            if (step.StepNAME === "" || step.StepNAME === null) {
                this.ErrorStepName = true;
                error = false;
            }
            if (step.WORKSSUPPORTSTEPPROGRESS === "" || step.WORKSSUPPORTSTEPPROGRESS === null || processsStep < 0 || processsStep % 1 != 0) {
                this.ErrorStepProgress = true;
                error = false;
            }
            if (step.StepMAXPROCESSTIME === "" || step.StepMAXPROCESSTIME === null || processTime < 0 || processTime % 1 != 0) {
                this.ErrorMaxTime = true;
                error = false;
            }
            if (error) {
                this.listStep[this.listStep.findIndex(obj => obj.isEdit === true)].isEdit = false;
            }
            return error;
        }
        if (stepCurrent !== undefined) {
            let processsStep: number = Number(stepCurrent.WORKSSUPPORTSTEPPROGRESS);
            let processTime: number = Number(stepCurrent.StepMAXPROCESSTIME);
            if (stepCurrent.StepNAME === "" || stepCurrent.StepNAME === null) {
                this.ErrorStepName = true;
                error = false;
            }
            if (stepCurrent.WORKSSUPPORTSTEPPROGRESS === "" || stepCurrent.WORKSSUPPORTSTEPPROGRESS === null || processsStep < 0 || processsStep % 1 != 0) {
                this.ErrorStepProgress = true;
                error = false;
            }
            if (stepCurrent.StepMAXPROCESSTIME === "" || stepCurrent.StepMAXPROCESSTIME === null || processTime < 0 || processTime % 1 != 0) {
                this.ErrorMaxTime = true;
                error = false;
            }
            if (error) {
                this.listStep[this.listStep.findIndex(obj => obj.StepID === this.currentStep)].isEdit = false;
            }
            return error;
        }
        return error;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: clear validate step!. 
     */
    private clearValidateStep() {
        this.ErrorStepProgress = false;
        this.ErrorMaxTime = false;
        this.ErrorStepName = false;
    }
    private currentSelectStep: string = String.empty;
    public setCurrentStep(event: any) {
        if (!this.validateStep()) {
            return false;
        }
        let self: WorksTypes = this;
        self.currentStep = event.target.id;
        this.currentSelectStep = this.currentStep;
        return false;
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: validate time process!.
     * @param timeProcess 
     * @param event 
     */
    private handleTimeProcess(timeProcess: any, event: any) {
        let keycode = event.keyCode;
        let step = this.listStep.find(obj => obj.StepID === this.currentStep);
        if (keycode === 110 || keycode === 190) {
            step.WORKSSUPPORTSTEPPROGRESS = "";
            this.ErrorStepProgress = true;
            return false;
        }
        if (keycode === 189) {
            step.WORKSSUPPORTSTEPPROGRESS = "";
            this.ErrorStepProgress = true;
            return false;
        }
        if (parseInt(timeProcess) > 100) {
            step.WORKSSUPPORTSTEPPROGRESS = "";
            this.ErrorStepProgress = true;
            return false;
        }
        if (timeProcess === "") {
            this.WORKSSUPPORTSTEPPROGRESS = "";
            this.ErrorStepProgress = true;
            return false;
        }
        this.ErrorStepProgress = false;
        return true;
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: validate max time   
     * @param maxTime 
     * @param event 
     */
    private handleTimeMax(maxTime: any, event: any) {
        let keycode = event.keyCode;
        let step = this.listStep.find(obj => obj.StepID === this.currentStep);
        if (keycode === 110 || keycode === 190) {
            step.StepMAXPROCESSTIME = "";
            this.ErrorMaxTime = true;
            return false;
        }
        if (keycode === 189) {
            step.StepMAXPROCESSTIME = "";
            this.ErrorMaxTime = true;
            return false;
        }
        if (maxTime > 9999 || maxTime < 0) {
            step.StepMAXPROCESSTIME = "";
            this.ErrorMaxTime = true;
            return false;
        }
        if (maxTime === "") {
            this.ErrorMaxTime = true;
            return false;
        }
        this.ErrorMaxTime = false;
        return true;
    }

    private onClose() {
        if (!this.validateStep())
            return false;
    }
}