import { Component, ViewChild } from "angular2/core";
import { Router } from "angular2/router";
import { BasePage } from "../../../common/models/ui";
import { QualitiesModel } from "./qualitiesModel";
import { DeleteModel } from "./qualitiesModel";
import qualityService from "../_share/services/qualityService";
import { Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, FormNumberInput } from "../../../common/directive";
import { PageAction } from "../../../common/models/ui";
import { UpdateQualityModel } from "./updateModel";
import { EditModel } from "./editModel";
import route from "../_share/config/route";
import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ValidationException } from "../../../common/models/exception";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
import { RoleValue } from "../../../common/enum";
@Component({
    templateUrl: "app/modules/configurations/quality/qualities.html",
    directives: [Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, FormNumberInput, ErrorMessage]
})
export class Qualities extends BasePage {
    public modelIconUrl: string = String.empty;
    public WorksSupportQualityId: number = 0;
    public WorksSupportQualityName: string = String.empty;
    public Description: string = String.empty;
    public IconUrl: string = String.empty;
    public OrderIndex: number = 0;
    public IsActived: boolean;
    public IsSystem: boolean;
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
        let self: Qualities = this;
        self.router = router;
        self.model = new QualitiesModel(self.i18nHelper);
        self.loadQualities();
    }
    ngOnInit() {
        this.userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
        this.isAdmin = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    }
    @ViewChild(Grid)
    private gridComponent: Grid;
    @ViewChild(ErrorMessage)
    private errorMess: ErrorMessage;
    private onAddNewQualitiesClicked() {
        let grid = this.gridComponent.grid.row.add({
            "WorksSupportQualityId": "0",
            "IconUrl": "fa fa-edit"
        }).draw();
        // this.router.navigate([route.priorities.addRole.name]);
    }
    public onEditMemberClicked(event: any) {
        // this.router.navigate([route.role.editRole.name, { id: event.item.id }]);
    }
    public onDeleteMembersItemClicked(event: any) {
        let self: Qualities = this;
        qualityService.delete(event.item.id).then(function () {
            self.loadQualities();
        });
    }
    private loadQualities() {
        let self: Qualities = this;
        qualityService.getQualilies().then(function (items: Array<any>) {
            self.model.importQualities(items);
        });
    }

    // Delete Qualities
    public onShowPopupDelete(event: any) {
        let $ = window.jQuery;
        let self: Qualities = this;
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        let grid = this.gridComponent.grid.rows(".selected").data();
        for (let i = 0; i < grid.length; i++) {
            if (grid[i]["IsSystem"] === true && this.userLogin !== this.roleValue) {
                this.selectedValue.push({ id: grid[i]["WorksSupportQualityId"], name: grid[i]["WorksSupportQualityName"], isSystem: grid[i]["IsSystem"] });
                lstId.push(grid[i]["WorksSupportQualityId"]);
            }
            if (this.userLogin === RoleValue.Admin) {
                this.selectedValue.push({ id: grid[i]["WorksSupportQualityId"], name: grid[i]["WorksSupportQualityName"], IsSystem: grid[i]["IsSystem"] });
                lstId.push(grid[i]["WorksSupportQualityId"]);
            }

        }

        this.objDelete = new DeleteModel(lstId.toString(), this.roleValue);
    }
    public onDeleteQualitys(event: any) {
        let $ = window.jQuery;
        let self: Qualities = this;
        qualityService.delete(this.objDelete).then(function (event: any) {
            self.router.navigate([route.quality.qualities.name]);
            $("#modal").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.loadQualities();
        });
    }

    // Get data by status : 1/0
    public onStatusChange(event: any) {
        let self: Qualities = this;
        let currentIsDelete = event.currentTarget.value;
        let $ = window.jQuery;
        let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        let isCanEdit: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
        let qualitiName = $("#qualitiName").val();
        // get data of dropdownlist.
        qualityService.getQualityBy(qualitiName, currentIsDelete, 0, 1000).then(function (items: Array<any>) {
            self.model.importQualities(items);
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
        let qualitiName = $("#qualitiName").val();
        let currentIsDelete = $(".custom-select .form-control").val();
        let self: Qualities = this;
        qualityService.getQualityBy(qualitiName, currentIsDelete, 0, 1000).then(function (items: Array<any>) {
            self.model.importQualities(items);
        });
    }

    public onToggleSelectChanged(event: any) {
        let self: Qualities = this;
        let model: UpdateQualityModel = new UpdateQualityModel();
        model.import(event.item);
        qualityService.update(model).then(function (event: any) {
            self.router.navigate([route.quality.qualities.name]);
        });
    }

    public onIconSelectChanged(event: any) {
        let self: Qualities = this;
        let model: UpdateQualityModel = new UpdateQualityModel();
        model.import(event.item);
        qualityService.update(model).then(function (event: any) {
            self.router.navigate([route.quality.qualities.name]);
        });
    }
    public onSaveClicked(event: any): void {
        let $ = window.jQuery;
        let self: Qualities = this;
        if (this.isValid(true)) {
            qualityService.createQuanlity(this.model).then(function () {
                $("#modalUpdate").modal("hide");
                $(".modal-backdrop").modal("hide");
                self.model = new QualitiesModel(self.i18nHelper);
                self.loadQualities();
            });
        }
        setTimeout(function () {
            self.validateOnServer();
        }, 500);
    }


    private onEditQuanlityClicked(event: any) {
        this.clearValidate(false);
        let self: Qualities = this;
        let $ = window.jQuery;
        this.errorMess.errors = [];
         this.isAddPopup = false;
        $("div").remove(".alert");
        qualityService.get(event.item.WorksSupportQualityId).then(function (item: any) {
            self.WorksSupportQualityId = item.WorksSupportQualityId;
            self.WorksSupportQualityName = item.WorksSupportQualityName;
            self.Description = item.Description;
            self.IconUrl = item.IconUrl;
            self.OrderIndex = item.OrderIndex;
            self.IsActived = item.IsActived;
            self.IsSystem = item.IsSystem;
            self.User = item.User;
        })
        $('#modalUpdate').modal('toggle');
    }

    public onEditQuanlity(event: any) {
        let self: Qualities = this;
        let $ = window.jQuery;
        let qualityId = this.WorksSupportQualityId;
        let qualityName = this.WorksSupportQualityName;
        let description = this.Description;

        let iconUrl = this.IconUrl;
        let orderIndex = this.OrderIndex;
        let isActived = this.IsActived;
        let isSystem = this.IsSystem;
        let user = cacheService.get(CACHE_CONSTANT.strUserName);      
        let editModel = new EditModel(qualityId, qualityName, orderIndex, description, iconUrl, isActived, isSystem, user);
        if (this.isValid(false)) {
            qualityService.update(editModel).then(function () {
                $('#modalUpdate').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new QualitiesModel(self.i18nHelper);
                self.loadQualities();
            });
        }
        setTimeout(function () {
            self.validateOnServer();
        }, 500);
    }

    private onAddNewQuanlitesClick() {
        this.errorMess.errors = [];
        let $ = window.jQuery;
        let self: Qualities = this;
        self.WorksSupportQualityId = 0;
        self.WorksSupportQualityName = "";
        self.Description = "";
        self.IconUrl = "";
        self.OrderIndex = 0;
        self.IsActived = true;
        self.IsSystem = false;
        self.User = "";
        this.isAddPopup = true;
    }
    public onAddClicked(event: any) {
        let self: Qualities = this;
        let $ = window.jQuery;
        let worksSupportQualityId = this.WorksSupportQualityId;
        let worksSupportQualityName = this.WorksSupportQualityName;
        let description = this.Description;
        let IconUrl = self.IconUrl;
        let orderIndex = this.OrderIndex;
        let isActived = this.IsActived;
        let isSystem = this.IsSystem;
        let user = cacheService.get(CACHE_CONSTANT.strUserName);
        let editModel = new EditModel(worksSupportQualityId, worksSupportQualityName, orderIndex, description, IconUrl, isActived, isSystem, user);
        if (this.isValid(true)) {
            qualityService.createQuanlity(editModel).then(function () {
                $('#modalUpdate').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new QualitiesModel(self.i18nHelper);
                self.loadQualities();
            })
        }
    }

    // select icon on popup edit
    public onIconSelectedEdit() {
        let self: Qualities = this;
        let $ = window.jQuery;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }
    // validation at client
    public isValid(isAdd: boolean): boolean {
        debugger;
        let validationErrors: ValidationException = new ValidationException();
        let $ = window.jQuery;
        this.clearValidate(isAdd);
        let lenName = (this.WorksSupportQualityName === null ? 0 : this.WorksSupportQualityName.length);
        let lenDesc = (this.Description === null ? 0 : this.Description.length);
        let orderIndex: number = this.OrderIndex;
        if (!isAdd) {
            if (orderIndex <= 0 || orderIndex % 1 != 0 || orderIndex > 999) {
                validationErrors.add("OrderIndex", [], "NCS-000003: Thứ tự chất lượng không hợp lệ!");
                $("#OrderIndex").addClass("has-error");
            }
        }
        if (!this.WorksSupportQualityName) {
            validationErrors.add("WorksSupportQualityName", {}, "NCS-000004: Tên chất lượng là bắt buộc!");
            $("#WorksSupportQualityName").addClass("has-error");
        }
        if (lenName > 200) {
            validationErrors.add("WorksSupportQualityName", {}, "NCS-000005: Tên chất lượng không được vượt quá 200 kí tự!");
            $("#WorksSupportQualityName").addClass("has-error");
        }
        if (lenDesc > 2000) {
            validationErrors.add("Description", {}, "NCS-000006: Mô tả không được vượt quá 2000 kí tự!");
            $("#Description").addClass("has-error");
        }
        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }

    // clear validation 
    public clearValidate(isAdd: boolean) {
        let $ = window.jQuery;
        if (!isAdd) {
            $("#OrderIndex").removeClass("has-error");
        }
        $("#WorksSupportQualityName").removeClass("has-error");
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
    public validateOnServer() {
        let $ = window.jQuery;
        let arrErr = this.errorMess.errors;
        for (let i = 0; i < arrErr.length; i++) {
            let key = arrErr[i].key;
            if (key === "WorksSupportQualityName") {
                $("#WorksSupportQualityName").addClass("has-error");
            }
            if (key === "OrderIndex") {
                $("#OrderIndex").addClass("has-error");
            }
        }
    }
     // cancel Add 
    public onCancelAdd() {
        let $ = window.jQuery;
        $("#modalUpdate").modal("hide");
        $(".modal-backdrop").modal("hide");
    }
}