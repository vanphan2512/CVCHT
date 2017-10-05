import { Component, ViewChild } from "angular2/core";
import { Router } from "angular2/router";
import { BasePage } from "../../../common/models/ui";
import { StatusModel } from "./statusModel";
import { UpdateStatusModel } from "./updateModel";
import { DeleteModel } from "./deleteModel";
import statusService from "../_share/services/statusService";
import { Grid, PageActions, Page } from "../../../common/directive";
import { Form, FormTextInput, FormFooter, FormTextArea, FormNumberInput } from "../../../common/directive";
import { PageAction } from "../../../common/models/ui";
import route from "../_share/config/route";
import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ValidationException } from "../../../common/models/exception";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
import { RoleValue } from "../../../common/enum";
@Component({
    templateUrl: "app/modules/configurations/status/status.html",
    directives: [Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, FormNumberInput, ErrorMessage]
})
export class Status extends BasePage {
    public Id: number;
    public WorksSupportStatusName: string = String.empty;
    public IsCompleteStatus: boolean = false;
    public IsCloseStatus: boolean = false;
    public IsInitStatus: boolean = false;
    public Description: string = String.empty;
    public OrderIndex: number = 1;
    public IsDeleted: number = 0;
    public IsActived: boolean = false;
    public IsSystem: boolean = false;
    public IconUrl: string = String.empty;
    public User: string = String.empty;

    public actions: Array<PageAction> = [];
    private router: Router;
    public objDelete: DeleteModel;
    public selectedValue: Array<any> = [];
    public isCanAdd: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD) === "true" ? true : false;
    public isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    public userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
    public roleValue: string = RoleValue.Admin;
    public isAdmin: boolean = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    public isAddPopup: boolean = false;
    constructor(router: Router) {
        super();
        let self: Status = this;
        self.router = router;
        self.model = new StatusModel(self.i18nHelper);
        self.loadStatus();
    }
    ngOnInit() {
        this.userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
        this.isAdmin = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    }
    @ViewChild(Grid)
    private gridComponent: Grid;

    @ViewChild(ErrorMessage)
    private errorMess: ErrorMessage;
    // show popup add
    private onShowPopupAdd(event: any) {
        this.model.description = String.empty;
        this.model.worksSupportStatusName = String.empty;
        this.IsInitStatus = false;
        this.IsActived = true;
        this.IsCompleteStatus = false;
        this.IsCloseStatus = false;
        this.IsSystem = false;
        this.IconUrl = String.empty;
        this.IconUrl = String.empty;
        this.errorMess.errors = [];
        this.clearValidate(true);
        this.isAddPopup = true;
    }

    // add status 
    private onAddStatus() {
        let $ = window.jQuery;
        let self: Status = this;
        this.model.IconUrl = self.IconUrl;
        if (this.isValid(true)) {
            this.model.User = cacheService.get(CACHE_CONSTANT.strUserName);
            this.model.IsInitStatus = (this.IsInitStatus === true ? 1 : 0);
            this.model.IsActived = (this.IsActived === true ? 1 : 0);
            this.model.IsCompleteStatus = (this.IsCompleteStatus === true ? 1 : 0);
            this.model.IsCloseStatus = (this.IsCloseStatus === true ? 1 : 0);
            this.model.IsSystem = (this.IsSystem === true ? 1 : 0);
            this.model.OrderIndex = -1;
            statusService.create(this.model).then(function () {
                $("#modalUpdate").modal("hide");
                $(".modal-backdrop").modal("hide");
                self.model = new StatusModel(self.i18nHelper);
                self.loadStatus();
            })
        }
        this.IconUrl = String.empty;
        this.model.IconUrl = String.empty;
        this.IconUrl = String.empty;

        setTimeout(function () {
            self.validateOnServer();
        }, 500);
    }
    // select icon on popup add
    public onIconSelectedAdd() {
        let self: Status = this;
        let $ = window.jQuery;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }

    // cancel add
    public onCancelAdd(event: any) {
        let $ = window.jQuery;
        $("#modalUpdate").modal("hide");
        $(".modal-backdrop").modal("hide");
    }
    // show popup Delete Priority
    public onShowPopupDelete(event: any) {
        let $ = window.jQuery;
        let self: Status = this;
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        let grid = this.gridComponent.grid.rows(".selected").data();
        for (let i = 0; i < grid.length; i++) {
            this.selectedValue.push({ id: grid[i]["WorksSupportStatusId"], name: grid[i]["WorksSupportStatusName"] });
            lstId.push(grid[i]["WorksSupportStatusId"]);
        }
        let User = cacheService.get(CACHE_CONSTANT.strUserName);
        this.objDelete = new DeleteModel(lstId.toString(), User);
    }

    public onDeleteStatus(event: any) {
        let $ = window.jQuery;
        let self: Status = this;
        statusService.delete(this.objDelete).then(function (event: any) {
            $("#modalDelete").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new StatusModel(self.i18nHelper);
            self.loadStatus();
        });
    }

    private loadStatus() {
        let self: Status = this;
        statusService.getStatus().then(function (items: Array<any>) {
            self.model.importStatus(items);
        });
    }
    // Get data by status : 1/0
    public onStatusChange(event: any) {
        let self: Status = this;
        let $ = window.jQuery;
        let currentIsDelete = event.currentTarget.value;
        let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        let isCanEdit: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
        let statusName = $("#statusName").val();
        // get data of dropdownlist.
        statusService.getStatusBy(statusName, currentIsDelete, 0, 1000).then(function (items: Array<any>) {
            self.model.importStatus(items);
        });
        // check status is delete to show delete button.
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
    // Search data by name
    public SearchByKey(event: any) {

        let $ = window.jQuery;
        let statusName = $("#statusName").val();
        let currentIsDelete = $(".custom-select .form-control").val();
        let self: Status = this;
        statusService.getStatusBy(statusName, currentIsDelete, 0, 1000).then(function (items: Array<any>) {
            self.model.importStatus(items);
        });
    }
    public onToggleSelectChanged(event: any) {
        let self: Status = this;
        let model: UpdateStatusModel = new UpdateStatusModel();
        model.import(event.item);
        statusService.update(model).then(function (event: any) {
            self.model = new StatusModel(self.i18nHelper);
            self.loadStatus();
        });
    }

    // show popup update
    public onShowPopupEdit(event: any) {
        let self: Status = this;
        let $ = window.jQuery;
        this.clearValidate(false);
        this.errorMess.errors = [];
        this.isAddPopup = false;
        $("div").remove(".alert");
        statusService.get(event.item.WorksSupportStatusId).then(function (item: any) {
            self.Id = item.WorksSupportStatusId;
            self.model.worksSupportStatusName = item.WorksSupportStatusName;
            self.IconUrl = item.IconUrl;
            self.IsInitStatus = item.IsInitStatus;
            self.IsCompleteStatus = item.IsCompleteStatus;
            self.IsCloseStatus = item.IsCloseStatus;
            self.model.description = item.Description;
            self.model.orderIndex = item.OrderIndex;
            self.IsActived = item.IsActived;
            self.IsSystem = item.IsSystem;
            $("#modalUpdate").modal("toggle");
        });
    }

    public onEditStatus(event: any) {
        let $ = window.jQuery;
        let self: Status = this;
        this.model.User = cacheService.get(CACHE_CONSTANT.strUserName);
        this.model.IsInitStatus = (this.IsInitStatus === true ? 1 : 0);
        this.model.IsActived = (this.IsActived === true ? 1 : 0);
        this.model.IsCompleteStatus = (this.IsCompleteStatus === true ? 1 : 0);
        this.model.IsCloseStatus = (this.IsCloseStatus === true ? 1 : 0);
        this.model.IsSystem = (this.IsSystem === true ? 1 : 0);
        this.model.Id = this.Id;
        this.model.IconUrl = this.IconUrl;
        self.User = cacheService.get(CACHE_CONSTANT.strUserName);
        if (this.isValid(false)) {
            statusService.update(this.model).then(function () {
                $("#modalUpdate").modal("hide");
                $(".modal-backdrop").modal("hide");
                self.model = new StatusModel(self.i18nHelper);
                self.loadStatus();
            });
        }
        self.IconUrl = String.empty;
        this.model.IconUrl = String.empty;
        setTimeout(function () {
            self.validateOnServer();
        }, 500);
    }

    public onCancelEdit(event: any) {
        let $ = window.jQuery;
        $("#modalUpdate").modal("hide");
        $(".modal-backdrop").modal("hide");
    }
    public isValid(isAdd: boolean): boolean {
        let $ = window.jQuery;
        let validationErrors: ValidationException = new ValidationException();
        let lenName = (this.model.worksSupportStatusName === null || this.model.worksSupportStatusName === undefined ? 0 : this.model.worksSupportStatusName.length);
        let lenDesc = (this.model.description === null || this.model.description === undefined ? 0 : this.model.description.length);
        let orderIndex: number = this.model.orderIndex;
        this.clearValidate(isAdd);
        if (!isAdd) {
            if (orderIndex <= 0 || orderIndex % 1 != 0 || orderIndex > 999) {
                validationErrors.add("OrderIndex", [], "NCS-000003: Thứ tự trạng thái không hợp lệ!");
                $("#OrderIndex").addClass("has-error");
            }
        }
        if (lenName == 0) {
            validationErrors.add("WorksSupportStatusName", [], "NCS-000004: Tên trạng thái là bắt buộc!");
            $("#WorksSupportStatusName").addClass("has-error");
        }
        if (lenName > 200) {
            validationErrors.add("WorksSupportStatusName", [], "NCS-000005: Tên trạng thái không được vượt quá 200 kí tự!");
            $("#WorksSupportStatusName").addClass("has-error");
        }
        if (lenDesc > 2000) {
            validationErrors.add("Description", [], "NCS-000006: Mô tả không được vượt quá 2000 kí tự!");
            $("#Description").addClass("has-error");
        }

        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }

    // clear validation of Edit popup
    public clearValidate(isAdd: boolean) {
        let $ = window.jQuery;
        if (!isAdd) {
            $("#OrderIndex").removeClass("has-error");
        }
        $("#WorksSupportStatusName").removeClass("has-error");
        $("#Description").removeClass("has-error");
    }

    // clear validation when key down is pressed
    public clearValidateInput(id: any) {
        let $ = window.jQuery;
        $("#" + id).removeClass("has-error");
        let arrErr = this.errorMess.errors;
        let index: number;
        for (let i = 0; i < arrErr.length; i++) {
            if (arrErr[i].key === id) {
                arrErr.splice(i, 1);
            }
        }
        this.errorMess.errors = arrErr;
    }

    public onIconSelectedEdit() {
        let self: Status = this;
        let $ = window.jQuery;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }

    // check validate items of status
    public checkStatusItem() {
        let self: Status = this;
        self.IsCompleteStatus = false;
        self.IsCloseStatus = false;
        self.IsInitStatus = false;
    }

    public validateOnServer() {
        let $ = window.jQuery;
        let arrErr = this.errorMess.errors;
        for (let i = 0; i < arrErr.length; i++) {
            let key = arrErr[i].key;
            if (key === "WorksSupportStatusName") {
                $("#WorksSupportStatusName").addClass("has-error");
            }
            if (key === "OrderIndex") {
                $("#OrderIndex").addClass("has-error");
            }
        }
    }

}