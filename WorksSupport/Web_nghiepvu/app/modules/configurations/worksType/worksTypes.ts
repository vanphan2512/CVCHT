import { Component, ViewChild, OnInit } from "angular2/core";
import { Router } from "angular2/router";
import { BasePage } from "../../../common/models/ui";
import { WorksTypesModel } from "./worksTypesModel";
import { DeleteModel } from "./worksTypesModel";
import { StepModel } from "./worksTypesModel";
import { AddWorkTypeModel } from "./worksTypesModel";
import worksTypeService from "../_share/services/worksTypeService";
import { Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, FormNumberInput } from "../../../common/directive";
import { UpdateWorksTypeModel } from "./updateModel";
import { EditModel } from "./editModel";
import { PageAction } from "../../../common/models/ui";
import route from "../_share/config/route";
import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ValidationException } from "../../../common/models/exception";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
import { RoleValue } from "../../../common/enum";
import priorityService from "../_share/services/priorityService";
import qualityService from "../_share/services/qualityService";
import statusService from "../_share/services/statusService";
@Component({
    templateUrl: "app/modules/configurations/worksType/worksTypes.html",
    directives: [Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, FormNumberInput, ErrorMessage]
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
    public objDelete: DeleteModel;
    public isCanAdd: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD) === "true" ? true : false;
    public isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    public userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
    public roleValue: string = RoleValue.Admin;
    public isAdmin: boolean = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    public listPriority: Array<any> = [];
    public listQualities: Array<any> = [];
    public listStep: Array<StepModel> = [];
    public StepNAME: string = "";
    public StepDESCRIPTION: string = "";
    public StepSTEPCOLORCODE: string = "#ffffff";
    public StepMAXPROCESSTIME: string = "1";
    public StepISINITSTEP: boolean = false;
    public StepISFINISHSTEP: boolean = false;
    public listNextStep: string[] = [];
    public listSelectedPriority: string[] = [];
    public listSelectedQualities: string[] = [];
    public StepCount: number = 0;
    public isEdit: boolean = false;
    public currentStep: string = String.empty;
    public StepID: string = String.empty;
    public stepName: string = String.empty;
    public deletedStepId: Array<string> = [];
    constructor(router: Router) {
        super();
        let self: WorksTypes = this;
        self.router = router;
        self.model = new WorksTypesModel(self.i18nHelper);
        self.loadWorkType();
        self.loadPriority();
        self.loadQualities();
    }
    ngOnInit() {
        this.userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
        this.isAdmin = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
        let self: WorksTypes = this;
        let $ = window.jQuery;
        $(function () {
            $('[data-toggle="popover"]').popover({ html: true })
        });
        debugger;
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
        let popOverSettings = {
            selector: '[rel="popover"]',
            content: $('#popover-content')
        };
        $(document).popover(popOverSettings);
        $(document).on('click', function (e: any) {
            $('[data-toggle="popover"],[data-original-title]').each(function () {
                //the 'is' for buttons that trigger popups
                //the 'has' for icons within a button that triggers a popup
                if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                    (($(this).popover('hide').data('bs.popover') || {}).inState || {}).click = false  // fix for BS 3.3.6
                }
            });
        });

    }
    @ViewChild(ErrorMessage)
    private Errors: ErrorMessage;
    @ViewChild(Grid)
    private gridComponent: Grid;

    private onAddNewWorkTypeClicked() {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        this.Errors.errors = [];
        self.ResetData();
        this.clearValidate(true);
        $("#modalAdd").modal('toggle');
        self.IconUrL;
    }

    public onAddClicked(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        self.listSelectedPriority = [];
        self.listSelectedQualities = [];
        if (this.isValid(true)) {
            let typeModal: string = $('#modalAdd')[0].className.indexOf("in") > 0 ? "#modalAdd" : "#modalEdit";
            $(typeModal + ' input.cbPriority:checked').each(function () {
                self.listSelectedPriority.push($(this).attr('data-id'));
            });
            $(typeModal + ' input.cbQualities:checked').each(function () {
                self.listSelectedQualities.push($(this).attr('data-id'));
            });
            self.SaveCurrentStep();
            let user = cacheService.get(CACHE_CONSTANT.strUserName);
            let editModel = new AddWorkTypeModel(self.WorksSupportTypeId, self.WorksSupportTypeName, self.Description, self.IconUrL, self.IsActived, self.IsSystem, self.listSelectedPriority.toString(), self.listSelectedQualities.toString(), JSON.stringify(self.listStep), user, 0, self.deletedStepId.toString())

            if (this.isValidAdd()) {
                worksTypeService.update(editModel).then(function () {
                    self.model = new WorksTypesModel(self.i18nHelper);
                    self.loadWorkType();
                    $('#modalAdd').modal('hide');
                    $('.modal-backdrop').modal('hide');
                })
            }
            setTimeout(function () {
                self.validateOnServer();
            }, 500);
        }
    }
    public validateOnServer() {
        let $ = window.jQuery;
        let arrErr = this.Errors.errors;
        for (let i = 0; i < arrErr.length; i++) {
            let key = arrErr[i].key;
            if (key === "WorksSupportTypeName") {
                $("#WorksSupportTypeName").addClass("has-error");
            }
            if (key === "OrderIndex") {
                $("#OrderIndex").addClass("has-error");
            }
        }
    }
    // clear validation 
    public clearValidate(isAdd: boolean) {
        let $ = window.jQuery;
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
        let validationErrors: ValidationException = new ValidationException();
        let $ = window.jQuery;
        this.clearValidate(isAdd);
        let valAdd = $("#stepName").val();
        let valEdit = $("#stepNameEdit").val();
        if (isAdd) {
            if (!valAdd) {
                validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                $("#stepName").addClass("has-error");
            }
        }
        else {
            if (!valEdit) {
                validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                $("#stepNameEdit").addClass("has-error");
            }
        }


        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }
    public isValid(isAdd: boolean): boolean {
        let validationErrors: ValidationException = new ValidationException();
        let $ = window.jQuery;
        this.clearValidate(isAdd);
        let lenName = (this.WorksSupportTypeName === null ? 0 : this.WorksSupportTypeName.length);
        let lenDesc = (this.Description === null ? 0 : this.Description.length);
        let orderIndex: number = this.OrderIndex;
        let stepCount = $("#ProcessStep li").length;
        let self: WorksTypes = this;
        let isVal = (isAdd !== true) ? ($("#stepNameEdit").val() === undefined ? false : true) : ($("#stepName").val() === undefined ? false : true);
        if (!this.WorksSupportTypeName) {
            validationErrors.add("WorksSupportTypeName", {}, "Tên loại công việc là bắt buộc!");
            $("#WorksSupportTypeName").addClass("has-error");
        }
        if (!isAdd) {
            if (orderIndex <= 0 || orderIndex % 1 != 0 || orderIndex > 999) {
                validationErrors.add("OrderIndex", [], "NCS-000003: Thứ tự loại công việc không hợp lệ!");
                $("#OrderIndex").addClass("has-error");
            }
        }
        if (self.listStep.length < 2) {
            validationErrors.add("#ProcessStep", {}, "Quy trình xử lý công việc là bắt buộc!");
            $("#ProcessStep").addClass("has-error");
        } else {
            this.clearValidate(isAdd);
            let valAdd = $("#stepName").val();
            let valEdit = $("#stepNameEdit").val();
            if (isAdd) {
                if (isVal && !valAdd) {
                    validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                    $("#stepName").addClass("has-error");
                }
            }
            else {
                if (isVal && !valEdit) {
                    validationErrors.add("StepNAME", {}, "Tên bước xử lí là bắt buộc!");
                    $("#stepNameEdit").addClass("has-error");
                }
            }
            if(self.listStep.findIndex(obj => obj.StepISINITSTEP === true || obj.StepISINITSTEP.toString() === "1") === -1) validationErrors.add("StepISINITSTEP", {}, "Phải có bước khởi tạo!");
            if(self.listStep.findIndex(obj => obj.StepISFINISHSTEP === true || obj.StepISFINISHSTEP.toString() === "1") === -1) validationErrors.add("StepISFINISHSTEP", {}, "Phải có bước hoàn thành!");
        }
        if (lenName > 200) {
            validationErrors.add("WorksSupportTypeName", {}, "Tên loại công việc không được vượt quá 200 kí tự!");
            $("#WorksSupportTypeName").addClass("has-error");
        }
        if (lenDesc > 2000) {
            validationErrors.add("Description", {}, "Mô tả không được vượt quá 2000 kí tự!");
            $("#Description").addClass("has-error");
        }
        let processTime: number = Number(this.StepMAXPROCESSTIME);

        if (processTime < 0 || processTime % 1 != 0) {
            validationErrors.add("StepMAXPROCESSTIME", [], "Thời gian xử lý tối đa không hợp lệ!");
            $("#StepMAXPROCESSTIME").addClass("has-error");
        }
        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }



    public onShowPopupDelete(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        let grid = this.gridComponent.grid.rows(".selected").data();
        for (let i = 0; i < grid.length; i++) {
            this.selectedValue.push({ id: grid[i]["WorksSupportTypeId"], name: grid[i]["WorksSupportTypeName"] });
            lstId.push(grid[i]["WorksSupportTypeId"]);
        }
        self.objDelete = new DeleteModel(lstId.toString(), cacheService.get(CACHE_CONSTANT.strUserName));
    }
    public onDeleteWorkTypes(event: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        worksTypeService.delete(this.objDelete).then(function (event: any) {
            $("#modal").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksTypesModel(self.i18nHelper);
            self.loadWorkType();
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
    // Get data by status : 1/0
    public onWorkTypeChange(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        let currentIsDelete = event.currentTarget.value;
        let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        let isCanEdit: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
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
                    false
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
                self.StepISINITSTEP = StepModel.StepISINITSTEP;
                self.StepISFINISHSTEP = StepModel.StepISFINISHSTEP;
                self.listNextStep = StepModel.listNextStep;
                self.currentStep = StepModel.StepID;
            }
            self.StepCount = 0;
            self.isEdit = false;
        })
        $("#modalEdit").modal('toggle');

    }
    public onEditClicked(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        self.listSelectedPriority = [];
        self.listSelectedQualities = [];
        if (this.isValid(false)) {
            let typeModal: string = $('#modalAdd')[0].className.indexOf("in") > 0 ? "#modalAdd" : "#modalEdit";
            $(typeModal + ' input.cbPriority:checked').each(function () {
                self.listSelectedPriority.push($(this).attr('data-id'));
            });
            $(typeModal + ' input.cbQualities:checked').each(function () {
                self.listSelectedQualities.push($(this).attr('data-id'));
            });
            self.SaveCurrentStep();
            let user = cacheService.get(CACHE_CONSTANT.strUserName);
            let editModel = new AddWorkTypeModel(self.WorksSupportTypeId, self.WorksSupportTypeName, self.Description, self.IconUrL, self.IsActived, self.IsSystem, self.listSelectedPriority.toString(), self.listSelectedQualities.toString(), JSON.stringify(self.listStep), user, self.OrderIndex, self.deletedStepId.toString())
            worksTypeService.update(editModel).then(function () {
                self.model = new WorksTypesModel(self.i18nHelper);
                self.loadWorkType();
                $('#modalEdit').modal('hide');
                $('.modal-backdrop').modal('hide');
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
        this.Errors.ResetError();
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
        self.StepMAXPROCESSTIME = "1";
        self.StepISINITSTEP = false;
        self.StepISFINISHSTEP = false;
        self.listStep = [];
        self.listNextStep = [];
        self.StepCount = 0;
        self.isEdit = false;
        self.currentStep = "";
        self.listSelectedQualities = [];
        self.listSelectedPriority = [];
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
            if (self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].isEdit === false) {
                let OldStep = self.currentStep;
                let Step: StepModel = new StepModel(OldStep,
                    self.StepNAME,
                    self.StepDESCRIPTION,
                    self.StepSTEPCOLORCODE,
                    self.StepMAXPROCESSTIME == "" ? "1" : self.StepMAXPROCESSTIME,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepISINITSTEP,
                    self.listStep[self.listStep.findIndex(obj => obj.StepID === self.currentStep)].StepISFINISHSTEP,
                    self.listNextStep,
                    self.isEdit
                )
                if (self.listStep.findIndex(obj => obj.StepID === OldStep) === -1) self.listStep.push(Step);
                else self.listStep[self.listStep.findIndex(obj => obj.StepID === OldStep)] = Step;
            }
        }
    }


    public onTabClick(event: any, addOrEdit: any) {
        let $ = window.jQuery;
        let self: WorksTypes = this;
        let typeModal: string = $('#modalAdd')[0].className.indexOf("in") > 0 ? "#modalAdd" : "#modalEdit";
        this.SaveCurrentStep();
        let StepId = event.target.id;
        if (self.listStep.findIndex(obj => obj.StepID === StepId) === -1) {
            self.StepID = "";
            self.StepNAME = "";
            self.StepDESCRIPTION = "";
            self.StepSTEPCOLORCODE = "#ffffff";
            self.StepMAXPROCESSTIME = "1";
            self.StepISINITSTEP = false;
            self.StepISFINISHSTEP = false;
            self.listNextStep = [];
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
        let isVal = (add !== true) ? $("#stepNameEdit").val() === undefined ? false : true : $("#stepName").val() === undefined ? false : true;
        if (isVal) {
            return false
        }
        if (self.listStep.length > 0) self.SaveCurrentStep();
        self.StepCount--;
        self.StepNAME = "";
        self.StepDESCRIPTION = "";
        self.StepSTEPCOLORCODE = "#ffffff";
        self.StepMAXPROCESSTIME = "1";
        self.StepISINITSTEP = false;
        self.StepISFINISHSTEP = false;
        self.listNextStep = [];
        let Step: StepModel = new StepModel(self.StepCount.toString(),
            "",
            self.StepDESCRIPTION,
            self.StepSTEPCOLORCODE,
            self.StepMAXPROCESSTIME == "" ? "1" : self.StepMAXPROCESSTIME,
            self.StepISINITSTEP,
            self.StepISFINISHSTEP,
            self.listNextStep,
            true
        )
        self.listStep.unshift(Step);
        self.currentStep = self.StepCount.toString();
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
        let indexRemove = self.listStep.findIndex(obj => obj.StepID === id);
        if (indexRemove >= 0) {
            self.listStep.splice(indexRemove, 1);
            if (id > 0) self.deletedStepId.push(id);
        }
        return false;
    }
    public onRename(event: any) {
        let self: WorksTypes = this;
        let $ = window.jQuery;
        let id = event.target.parentNode.parentElement.parentElement.parentElement.id;
        self.listStep[self.listStep.findIndex(obj => obj.StepID === id)].isEdit = true;
        $('[rel="popover"]').popover('hide');
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
    public onEnter(event: any) {
        if (event.target.value.trim() === "") { event.preventDefault(); }
        else {
            let $ = window.jQuery;
            let self: WorksTypes = this;
            self.StepNAME = event.target.value.trim();
            self.listStep[self.listStep.findIndex(obj => obj.StepID === event.target.id)].isEdit = false;
        }
    }
    public setCurrentStep(event: any) {
        let self: WorksTypes = this;
        self.currentStep = event.target.id;
        return false;
    }
}