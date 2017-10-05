import { Component, ViewChild, OnInit } from "angular2/core";
import { Router } from "angular2/router";
import { BasePage } from "../../../common/models/ui";
import { PermissionModel } from "./permissionModel";
import { MemberRoleModel } from "./memberRoleModel";
import { WfNxPermissModel } from "./wfNxPermissModel";
import { WfPermissModel } from "./wfPermissModel";
import { DeleteModel } from "./deleteModel";
import { UserMemberModel } from "./userMemberModel";
import { CopyPermissionModel } from "./copyPermissionModel";
import { WorksFlowNextPermissionModel } from "./worksFlowNextPermissionModel";
import worksTypeService from "../_share/services/worksTypeService";
import memberService from "../_share/services/memberService";
import { Grid, Page, ValidationDirective } from "../../../common/directive";
import { UpdateWorksTypeModel } from "./updateModel";
import route from "../_share/config/route";
import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ValidationException } from "../../../common/models/exception";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
import { RoleValue } from "../../../common/enum";
@Component({
    templateUrl: "app/modules/configurations/permission/permissions.html",
    directives: [Grid,   ErrorMessage, ValidationDirective]
})
export class Permission extends BasePage {

    public WorksSupportTypeId: number = 0;
    public WorksSupportTypeName: string = "";
    public IconUrL: string = "";
    public Description: string = "";
    public OrderIndex: number = -1;
    public IsActived: boolean;
    public IsSystem: boolean;
    public User: string = "";
    private router: Router;
    public selectedValue: Array<any> = [];
    public objDelete: DeleteModel;

    public isCanView: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_VIEW) === "true" ? true : false;
    public isCanEdit: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_EDIT) === "true" ? true : false;

    public userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
    public roleValue: string = RoleValue.Admin;
    public isAdmin: boolean = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    // List WorksType
    public listWorkType: Array<any> = [];
    // List MemberRole
    public listMemberRoleOfPermission: Array<any> = [];
    public listMemberRoleOfProccess: Array<any> = [];
    public WorksTypeId: any = 0;
    // list members of department
    public lstMemberOfDepart: Array<any> = [];
    public lstAddMemberOfDepart: Array<any> = [];
    // List User
    public listUser: Array<any> = [];
    public permissionDetail: any = null;
    public memberRoleModel: MemberRoleModel = null;
    public wfPermissModel: WfPermissModel = null;
    public listNextStep: Array<string> = [];
    public ListWorksSupportTypeMemberRole: Array<any> = [];
    public ListWorksSupportTypeWorkFlow: Array<any> = [];
    public ListWorksSupportProjectPermis: Array<any> = [];
    public ListWorksSupportTypeWfNx: Array<any> = [];
    public ListPermissionMemberRoleDefault: Array<any> = [];
    public ListWorksSupportTypeWfNxPermis: Array<any> = [];
    public ListWorksSupportTypeWfPermis: Array<any> = [];
    public currentStepID: string = String.empty;
    public currentTypeID: string = String.empty;
    public currentMemberRolePermisId: string = String.empty;
    public currentMemberRoleId: string = String.empty;
    public defaultRoleSelected: MemberRoleModel = null;
    public isSaved: boolean = false;
    // Create new list member role to display to screen.
    public listMemberRoleNew: Array<any> = [];
    // Create new list member role to display to screen.
    public lstDepartment: Array<any> = [];
    public ListNewWorksSupportTypeWfPermis: Array<string> = [];
    public fromWorkTypeId: string = String.empty;
    public copyPermissionModel: CopyPermissionModel = null;
    constructor(router: Router) {
        super();
        let self: Permission = this;
        self.router = router;
        self.loadWorkType();
        self.loadMemberRole();
        self.loadMemberRoleOfProccess();
        self.loadAllDepartment();
    }

    ngOnInit() {
        this.userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
        this.isAdmin = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;

        this.isCanView = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_VIEW) === this.isCanView ? true : false;
        this.isCanEdit = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_EDIT) === this.isCanEdit ? true : false;

        let $ = window.jQuery;
        $('#myTab a').click(function (e: any) {
            $(this).tab('show');
            e.stopPropagation();
        });

        $("#ulListMemberRole a").click(function (e: any) {
            $(this).tab('show');
            e.stopPropagation();
        });

        $('.general-search>.dropdown-menu').click(function (e: any) {
            e.stopPropagation();
        });

        $("#searchButton").hover(function () {
            $(this).addClass('active');
        });

        $("#searchButton").focusout(function () {
            $(this).removeClass('active');
        });

        $(".accord-info .panel").on("show.bs.collapse hide.bs.collapse", function (e: any) {
            if (e.type == 'show') {
                $(this).addClass('active');
            } else {
                $(this).removeClass('active');
            }
        });

        $('[data-toggle="tooltip"]').tooltip();
        $('[data-toggle="popover"]').popover();
    }

    ngAfterViewInit() {
        let $ = window.jQuery;
        let self: Permission = this;
        this.loadAllDepartment();
        $("#selDepart").selectpicker("refresh");
        $("[data-id='selDepart']").on("click", this.selectPickerOfDepart);
        $("[data-id='selDepart']").css({ "max-width": "200px", "overflow-x": "scroll;" });
        $('.selectpicker').on('changed.bs.select', function (e: any, clickedIndex: any, newValue: any, oldValue: any) {
            var selected = $(e.currentTarget).val();
            self.findDepartmentByName(selected);
        });
    }

    public reLoadPage(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        self.loadWorkType();
        $('#saveSuccess').modal('hide');
    }

    // Get list WorksType
    private loadWorkType() {
        let self: Permission = this;
        let worksTypeId: any = String.empty;
        let $ = window.jQuery;

        worksTypeService.getWorksTypesActived().then(function (items: Array<any>) {
            localStorage.removeItem('permissionDetail');
            if (items.length > 0) {
                self.listWorkType = items;
                localStorage.setItem('listWorkType', JSON.stringify(items));
                worksTypeId = items[0].WorksSupportTypeId;
                worksTypeService.get(worksTypeId).then(function (item: any) {
                    localStorage.setItem('permissionDetail', JSON.stringify(item));
                    $("#worksSupportTypeDetail").css("display", "inline-block");
                });
            }
        });
        self.permissionDetail = JSON.parse(localStorage.getItem('permissionDetail'));
        self.WorksTypeId = self.permissionDetail.WorksSupportTypeId;
        self.currentTypeID = self.WorksTypeId;
        self.ListWorksSupportTypeWorkFlow = self.permissionDetail.ListWorksSupportTypeWorkFlow;
        self.ListWorksSupportProjectPermis = self.permissionDetail.ListWorksSupportProjectPermis == null ? [] : self.permissionDetail.ListWorksSupportProjectPermis;
        self.ListWorksSupportTypeWfNx = self.ListWorksSupportTypeWorkFlow.length == 0 ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNx == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNx);
        self.ListWorksSupportTypeWfNxPermis = self.ListWorksSupportTypeWorkFlow.length == 0 ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNxPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNxPermis);
        self.ListWorksSupportTypeWfPermis = self.ListWorksSupportTypeWorkFlow.length == 0 ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfPermis);
        self.ListWorksSupportTypeMemberRole = self.permissionDetail.ListWorksSupportTypeMemberRole == null ? [] : self.permissionDetail.ListWorksSupportTypeMemberRole;
        let permissModel = self.ListWorksSupportTypeWfPermis.length == 0 ? null : self.ListWorksSupportTypeWfPermis[0];
        let wfPermissModel: WfPermissModel = null;
        if (permissModel != null) {
            self.wfPermissModel = new WfPermissModel(
                permissModel.WorksSupportStepId,
                permissModel.WorksSupportMemberRoleId,
                permissModel.IsCanEditContent,
                permissModel.IsCanEditSolutionContent,
                permissModel.IsCanAddAttachment,
                permissModel.IsCanComment,
                permissModel.IsCanEditExpectedCompletedDate,
                permissModel.IsCanChangeProgress,
                permissModel.IsCanEditQuality,
                permissModel.IsCanProcessWorkflow,
                permissModel.IsMustChooseProcessUser,
            );
        }
        else {
            self.wfPermissModel = new WfPermissModel(
                self.ListWorksSupportTypeWorkFlow[0].WorksSupportStepId, 0, false, false, false, false, false, false, false, false, false
            );
        }
        let memberRole = self.permissionDetail.ListWorksSupportTypeMemberRole == null ? null : self.ListWorksSupportTypeMemberRole[0];
        let memberRoleModel: MemberRoleModel = null;
        if (memberRole != null) memberRoleModel = new MemberRoleModel(
            memberRole.WorksSupportTypeId, memberRole.WorksSupportMemberRoleId,
            memberRole.IsCanAddWorksSupportGroup, memberRole.IsCanAddWorksSupport,
            memberRole.IsCanEditWorksSupportGroup, memberRole.sCanEditWorksSupport, memberRole.IsCanDeleteWorksSupportGroup,
            memberRole.IsCanDeleteWorksSupport, memberRole.WorksSupportMemberRoleName, memberRole.IsDefaultRole
        );
        else {
            memberRoleModel = new MemberRoleModel(
                self.permissionDetail.WorksSupportTypeId, 0, false, false, false, false, false, false, null, false
            );
        }
        self.memberRoleModel = memberRoleModel;
        let objDefaultMemberRole = self.ListWorksSupportTypeMemberRole == null ? null : (self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.IsDefaultRole === 1) == null ? null : self.ListWorksSupportTypeMemberRole[self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.IsDefaultRole === 1)]);
        if (objDefaultMemberRole != null) {
            self.defaultRoleSelected = objDefaultMemberRole.WorksSupportMemberRoleId;
        }
        self.currentStepID = self.ListWorksSupportTypeWorkFlow.length == 0 ? 0 : self.ListWorksSupportTypeWorkFlow[0].WorksSupportStepId;
        self.isSaved = true;
    }

    // Get list MemberRole
    private loadMemberRole() {
        let self: Permission = this;
        memberService.getMembersActived().then(function (items: Array<any>) {
            self.listMemberRoleOfPermission = items;
            localStorage.setItem('currentMemberRolePermisId', items[0].WorksSupportMemberRoleId);
            self.currentMemberRolePermisId = localStorage.getItem('currentMemberRolePermisId');
            self.currentMemberRoleId = localStorage.getItem('currentMemberRolePermisId');
            localStorage.removeItem("currentMemberRolePermisId");
        });
    }

    private loadMemberRoleOfProccess() {
        let self: Permission = this;
        memberService.getMembersActived().then(function (items: Array<any>) {
            self.listMemberRoleOfProccess = items;
        });
    }

    public getConfirm(event: any) {
        let self: Permission = this;
        let $ = window.jQuery;
        if (self.isSaved === false) {
            localStorage.removeItem('selectedEvent');
            localStorage.setItem('selectedEvent', event.currentTarget.id.replace('Permission-', ''));
            event.stopPropagation();
            $('#confirmationModal').modal('show');
        }
        else {
            self.getDetail(event);
        }
    }

    // WorksType Detail
    public getDetail(event: any) {
        let self: Permission = this;
        let $ = window.jQuery;
        let worksTypeId: any = String.empty;
        this.resetForm();
        if (event == null) {
            worksTypeId = localStorage.getItem('selectedEvent');
            $('#ulPermission #A-' + worksTypeId).tab('show');
            $('#confirmationModal').modal('hide');
            $('#resetConfirm').modal('hide');
            $('#saveSuccess').modal('hide');
        }
        else {
            worksTypeId = event.currentTarget.id.replace('Permission-', '');
        }
        self.WorksTypeId = worksTypeId;
        worksTypeService.get(worksTypeId).then(function (item: any) {
            localStorage.removeItem('permissionDetail');
            localStorage.setItem('permissionDetail', JSON.stringify(item));
            self.permissionDetail = JSON.parse(localStorage.getItem('permissionDetail'));
            self.currentTypeID = self.WorksTypeId;

            self.ListWorksSupportTypeMemberRole = self.permissionDetail.ListWorksSupportTypeMemberRole == null ? [] : self.permissionDetail.ListWorksSupportTypeMemberRole;
            self.ListWorksSupportTypeWorkFlow = self.permissionDetail.ListWorksSupportTypeWorkFlow == null ? [] : self.permissionDetail.ListWorksSupportTypeWorkFlow;
            self.ListWorksSupportTypeWfNx = self.ListWorksSupportTypeWorkFlow == null ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNx == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNx);
            self.ListWorksSupportTypeWfNxPermis = self.ListWorksSupportTypeWorkFlow == null ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNxPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNxPermis);
            self.ListWorksSupportTypeWfPermis = self.ListWorksSupportTypeWorkFlow == null ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfPermis);
            self.ListWorksSupportProjectPermis = self.permissionDetail.ListWorksSupportProjectPermis == null ? [] : self.permissionDetail.ListWorksSupportProjectPermis;

            let permissModel = self.ListWorksSupportTypeWfPermis.length == 0 ? null : self.ListWorksSupportTypeWfPermis[0];
            let wfPermissModel: WfPermissModel = null;
            if (permissModel != null) {
                self.wfPermissModel = new WfPermissModel(
                    permissModel.WorksSupportStepId,
                    permissModel.WorksSupportMemberRoleId,
                    permissModel.IsCanEditContent,
                    permissModel.IsCanEditSolutionContent,
                    permissModel.IsCanAddAttachment,
                    permissModel.IsCanComment,
                    permissModel.IsCanEditExpectedCompletedDate,
                    permissModel.IsCanChangeProgress,
                    permissModel.IsCanEditQuality,
                    permissModel.IsCanProcessWorkflow,
                    permissModel.IsMustChooseProcessUser,
                );
            }
            else {
                self.wfPermissModel = new WfPermissModel(
                    self.ListWorksSupportTypeWorkFlow[0].WorksSupportStepId, 0, false, false, false, false, false, false, false, false, false
                );
            }
            let memberRole = self.permissionDetail.ListWorksSupportTypeMemberRole == null ? null : self.ListWorksSupportTypeMemberRole[0];
            let memberRoleModel: MemberRoleModel = null;
            if (memberRole != null) memberRoleModel = new MemberRoleModel(
                memberRole.WorksSupportTypeId, memberRole.WorksSupportMemberRoleId,
                memberRole.IsCanAddWorksSupportGroup, memberRole.IsCanAddWorksSupport,
                memberRole.IsCanEditWorksSupportGroup, memberRole.IsCanEditWorksSupport, memberRole.IsCanDeleteWorksSupportGroup,
                memberRole.IsCanDeleteWorksSupport, memberRole.WorksSupportMemberRoleName, memberRole.IsDefaultRole
            );
            else {
                memberRoleModel = new MemberRoleModel(
                    self.permissionDetail.WorksSupportTypeId, 0, false, false, false, false, false, false, null, false
                );
            }
            self.memberRoleModel = memberRoleModel;

            let objDefaultMemberRole = self.ListWorksSupportTypeMemberRole == null ? null : (self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.IsDefaultRole === 1) == null ? null : self.ListWorksSupportTypeMemberRole[self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.IsDefaultRole === 1)]);
            if (objDefaultMemberRole != null) {
                self.defaultRoleSelected = objDefaultMemberRole.WorksSupportMemberRoleId;
            }
            self.currentStepID = self.ListWorksSupportTypeWorkFlow.length == 0 ? 0 : self.ListWorksSupportTypeWorkFlow[0].WorksSupportStepId;
            self.isSaved = true;

            self.loadMemberRole();
            self.loadMemberRoleOfProccess();
        });
    }

    // Get Role detail
    public getRoleDetail(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        if (event.target.localName == "a") {
            let currentRoleId = event.currentTarget.id.replace('MemberRole-', '');
            let RoleName = event.currentTarget.children[0].text.trim();
            this.UpdateMemberRole();
            let index: any = self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.WorksSupportMemberRoleId.toString() === currentRoleId.toString());
            if (index > -1) {
                self.memberRoleModel = self.ListWorksSupportTypeMemberRole[index];
            }
            else {
                self.memberRoleModel =
                    new MemberRoleModel(
                        self.WorksTypeId, currentRoleId,
                        false, false,
                        false, false, false,
                        false, RoleName, false
                    );
            }
            self.currentMemberRoleId = currentRoleId;
            self.isSaved = false;
        }
        else {
            let checkboxid = event.target.parentElement.parentElement.id.replace('MemberRole-', '');
            if (event.target.checked == false) {
                let index = self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.WorksSupportMemberRoleId.toString() === checkboxid.toString());
                if (index > -1) self.ListWorksSupportTypeMemberRole.removeItem(self.ListWorksSupportTypeMemberRole[index]);
            }
            else {
                let index = self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.WorksSupportMemberRoleId.toString() === checkboxid.toString());
                if (index === -1) {
                    let activedID = $('#ulListMemberRole .active')[0].id.replace('MemberRole-', '');
                    if (activedID === checkboxid) {
                        self.UpdateMemberRole();
                    }
                    else {
                        let RoleID = event.target.parentElement.parentElement.id.replace('MemberRole-', '');
                        let RoleName = event.target.parentElement.parentElement.innerText.trim();
                        let memberRoleModel = new MemberRoleModel(
                            self.permissionDetail.WorksSupportTypeId, RoleID, false, false, false, false, false, false, RoleName, false
                        );
                        self.ListWorksSupportTypeMemberRole.push(memberRoleModel);
                    }
                }
            }
            self.isSaved = false;
            event.stopPropagation();
        }
    }

    /**
     * Get detail member role of process.
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public getMemeberRoleOfProcess(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        let indexWorksFlow = self.ListWorksSupportTypeWorkFlow.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString());
        if (event.target.localName == "a") {
            let checkId = event.currentTarget.id.replace('MemberRoleProcess-', '');
            this.SavePerMission();
            let index: any = self.ListWorksSupportTypeWfPermis.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString() && obj.WorksSupportMemberRoleId.toString() === checkId.toString());
            if (index > -1) {
                self.wfPermissModel = self.ListWorksSupportTypeWfPermis[index];
                let listNextPermissionTemp = index > -1 ? (self.ListWorksSupportTypeWorkFlow[indexWorksFlow].ListWorksSupportTypeWfNxPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[indexWorksFlow].ListWorksSupportTypeWfNxPermis) : [];
                if (listNextPermissionTemp.length > 0) {
                    this.addListWorksSupportTypeWfNxPermis(listNextPermissionTemp);
                }
            } else {
                self.wfPermissModel = new WfPermissModel(
                    self.currentStepID, checkId, false, false, false, false, false, false, false, false, false
                );
            }

            self.currentMemberRolePermisId = checkId;
            self.isSaved = false;
        }
        else {
            let checkboxid = event.target.parentElement.parentElement.id.replace('MemberRoleProcess-', '');
            if (event.target.checked == false) {
                let index = self.ListWorksSupportTypeWfPermis.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString() && obj.WorksSupportMemberRoleId.toString() === checkboxid.toString());
                if (index > -1) {
                    let listWorksSupportTypeWfPermisTemp: Array<any> = self.ListWorksSupportTypeWorkFlow[indexWorksFlow].ListWorksSupportTypeWfPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[indexWorksFlow].ListWorksSupportTypeWfPermis;
                    let perIndex = listWorksSupportTypeWfPermisTemp.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString() && obj.WorksSupportMemberRoleId.toString() === checkboxid.toString());
                    if (perIndex > -1) {
                        self.ListWorksSupportTypeWorkFlow[indexWorksFlow].ListWorksSupportTypeWfPermis.removeItem(self.ListWorksSupportTypeWorkFlow[indexWorksFlow].ListWorksSupportTypeWfPermis[perIndex]);
                        self.ListWorksSupportTypeWorkFlow[indexWorksFlow].ListWorksSupportTypeWfNxPermis = [];
                    }
                    self.ListWorksSupportTypeWfPermis.removeItem(self.ListWorksSupportTypeWfPermis[index]);
                    // remove next works flow permission.
                    this.removeNxPermission(self.currentStepID, checkboxid);

                    self.wfPermissModel = new WfPermissModel(
                        self.currentStepID, checkboxid, false, false, false, false, false, false, false, false, false
                    );
                }
            }
            else {
                let index = self.ListWorksSupportTypeWfPermis.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString() && obj.WorksSupportMemberRoleId.toString() === checkboxid.toString());
                if (index === -1) {
                    let activedID = $('#roleOfPermissionProcess .active')[0].id.replace('MemberRoleProcess-', '');
                    if (activedID === checkboxid) {
                        self.SavePerMission();
                    }
                    else {
                        let checkId = event.target.parentElement.parentElement.id.replace('MemberRoleProcess-', '');
                        let memberRoleModel = new WfPermissModel(
                            self.currentStepID, checkId, false, false, false, false, false, false, false, false, false
                        );

                        self.ListWorksSupportTypeWfPermis.push(memberRoleModel);
                    }
                }
            }

            self.isSaved = false;
            event.stopPropagation();
        }
    }
    // Remove nextStep permission
    public removeNxPermission(worksSupportStepId: any, worksSupportMemberRoleId: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        let index = self.ListWorksSupportTypeWfNxPermis.findIndex(obj => obj.WorksSupportStepId.toString() === worksSupportStepId && obj.WorksSupportMemberRoleId.toString() === worksSupportMemberRoleId);
        if (index > -1) {
            self.ListWorksSupportTypeWfNxPermis.removeItem(self.ListWorksSupportTypeWfNxPermis[index]);
        }
    }

    public SavePerMission() {
        let $ = window.jQuery;
        let self: Permission = this;
        let index: any = self.ListWorksSupportTypeWfPermis.findIndex(obj => obj.WorksSupportStepId.toString() === self.wfPermissModel.WorksSupportStepId.toString() && obj.WorksSupportMemberRoleId.toString() === self.wfPermissModel.WorksSupportMemberRoleId.toString())
        if ($('#roleOfPermissionProcess #MemberRoleProcess-' + self.currentMemberRolePermisId)[0].children[0].children[0].checked == false) {
            if (index > -1) {
                let objPermission = self.ListWorksSupportTypeWfPermis[index];
                self.ListWorksSupportTypeWfPermis.removeItem(objPermission);
                self.removeNxPermission(objPermission.WorksSupportStepId, objPermission.WorksSupportMemberRoleId);
            }
        }
        else {
            if (index > -1) {
                self.ListWorksSupportTypeWfPermis[index] = self.wfPermissModel;
            }
            else self.ListWorksSupportTypeWfPermis.push(self.wfPermissModel);
        }
        self.isSaved = false;
    }

    /**
     * 
     *  Update member role
     * 
     * @memberof Permission
     */
    public UpdateMemberRole() {
        let $ = window.jQuery;
        let self: Permission = this;
        // Chek member role is existed in list.
        // If existed then remove and then add new member role
        let index: any = self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.WorksSupportMemberRoleId.toString() === self.currentMemberRoleId.toString())
        if ($('#ulListMemberRole #MemberRole-' + self.currentMemberRoleId)[0].children[0].children[0].checked == false) {
            if (index > -1) {
                self.ListWorksSupportTypeMemberRole.removeItem(self.ListWorksSupportTypeMemberRole[index]);
            }
        }
        else {
            if (self.memberRoleModel.WorksSupportMemberRoleId == "") {
                self.memberRoleModel.WorksSupportMemberRoleId = self.currentMemberRoleId.toString();
                self.memberRoleModel.WorksSupportMemberRoleName = $('#ulListMemberRole #MemberRole-' + self.currentMemberRoleId)[0].innerText.trim();
            }
            if (index > -1) {
                self.ListWorksSupportTypeMemberRole[index] = self.memberRoleModel;
            }
            else self.ListWorksSupportTypeMemberRole.push(self.memberRoleModel);
        }
        self.isSaved = false;
    }

    /**
     * 
     * Remove user
     * @param {*} event 
     * @memberof Permission
     */
    public RemoveUser(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        let currentUser = event.currentTarget.id.replace('User-', '');
        self.ListWorksSupportProjectPermis.forEach(element => {
            if (element.UserName === currentUser) {
                self.ListWorksSupportProjectPermis.removeItem(element);
            }
        });
        self.isSaved = false;
        event.stopPropagation();
    }
    CheckSelectedNextStep(item: any): boolean {
        let self: Permission = this;
        let $ = window.jQuery;
        let existed = self.ListWorksSupportTypeWfNxPermis.length == 0 ? false : (self.ListWorksSupportTypeWfNxPermis.findIndex(obj => obj.NextWorksSupportStepId.toString() === item.toString() && obj.WorksSupportMemberRoleId.toString() === self.currentMemberRolePermisId.toString() && obj.WorksSupportStepId.toString() === self.currentStepID.toString()) > -1 ? true : false);
        return existed;
    }

    CheckSelected(item: any): boolean {
        let self: Permission = this;
        let $ = window.jQuery;
        return self.ListWorksSupportTypeMemberRole.length == 0 ? false : (self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.WorksSupportMemberRoleId.toString() === item.toString()) > -1 ? true : false);
    }

    CheckSelectedMemberRole(item: any): boolean {
        let self: Permission = this;
        let $ = window.jQuery;
        return self.ListWorksSupportTypeWfPermis.length == 0 ? false : (self.ListWorksSupportTypeWfPermis.findIndex(obj => obj.WorksSupportMemberRoleId.toString() === item.toString() && obj.WorksSupportStepId.toString() === self.currentStepID.toString()) > -1 ? true : false);
    }

    public onNextStepSelected(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        let index: number = self.listNextStep.indexOf(event.target.id);
        if (event.target.checked === false) {
            if (index > -1) self.listNextStep.removeItem(self.listNextStep[index]);
        }
        else {
            if (index === -1) self.listNextStep.push(event.target.id);
        }
        self.isSaved = false;
    }
    // Get next step
    public getNextStep(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        self.loadMemberRoleOfProccess();
        self.currentStepID = event.currentTarget.id.replace('WorkFlow-', '');
        let index = self.ListWorksSupportTypeWorkFlow.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString());
        self.ListWorksSupportTypeWfNx = index > -1 ? (self.ListWorksSupportTypeWorkFlow[index].ListWorksSupportTypeWfNx == null ? [] : self.ListWorksSupportTypeWorkFlow[index].ListWorksSupportTypeWfNx) : [];
        let listWfPermis = index > -1 ? (self.ListWorksSupportTypeWorkFlow[index].ListWorksSupportTypeWfPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[index].ListWorksSupportTypeWfPermis) : [];
        if (listWfPermis.length > 0) {
            this.addListWorksSupportTypeWfPermis(listWfPermis);
        }
        let ListWorksSupportTypeWfNxPermisTemp = index > -1 ? (self.ListWorksSupportTypeWorkFlow[index].ListWorksSupportTypeWfNxPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[index].ListWorksSupportTypeWfNxPermis) : [];
        if (ListWorksSupportTypeWfNxPermisTemp.length > 0) {
            this.addListWorksSupportTypeWfNxPermis(ListWorksSupportTypeWfNxPermisTemp);
        }

        let worksSupportMemberRoleId = self.listMemberRoleOfProccess[0].WorksSupportMemberRoleId;
        let permissModel: any = null;
        let wfIndex = 0;
        wfIndex = self.ListWorksSupportTypeWfPermis.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString() && obj.WorksSupportMemberRoleId.toString() === worksSupportMemberRoleId.toString());
        if (wfIndex > -1) {
            permissModel = self.ListWorksSupportTypeWfPermis[wfIndex];
        }
        let wfPermissModel: WfPermissModel = null;
        if (permissModel != null) {
            if (worksSupportMemberRoleId === permissModel.WorksSupportMemberRoleId) {
                self.wfPermissModel = new WfPermissModel(
                    permissModel.WorksSupportStepId,
                    permissModel.WorksSupportMemberRoleId,
                    permissModel.IsCanEditContent,
                    permissModel.IsCanEditSolutionContent,
                    permissModel.IsCanAddAttachment,
                    permissModel.IsCanComment,
                    permissModel.IsCanEditExpectedCompletedDate,
                    permissModel.IsCanChangeProgress,
                    permissModel.IsCanEditQuality,
                    permissModel.IsCanProcessWorkflow,
                    permissModel.IsMustChooseProcessUser
                );
            }
            else {
                self.wfPermissModel = new WfPermissModel(
                    self.currentStepID, worksSupportMemberRoleId, false, false, false, false, false, false, false, false, false
                );
            }
        }
        else {
            self.wfPermissModel = new WfPermissModel(
                self.currentStepID, worksSupportMemberRoleId, false, false, false, false, false, false, false, false, false
            );
        }
    }

    public resetForm() {
        let self: Permission = this;
        self.wfPermissModel = new WfPermissModel(
            self.currentStepID, 0, false, false, false, false, false, false, false, false, false
        );
        self.memberRoleModel = new MemberRoleModel(
            self.permissionDetail.WorksSupportTypeId, 0, false, false, false, false, false, false, null, false
        );
    }
    public addListWorksSupportTypeWfNxPermis(list: Array<any>) {
        let self: Permission = this;
        if (self.ListWorksSupportTypeWfNxPermis.length === 0) {
            list.forEach(element => {
                self.ListWorksSupportTypeWfNxPermis.push(element);
            });
        } else {
            list.forEach(element => {
                let index = self.ListWorksSupportTypeWfNxPermis.findIndex(obj => obj.WorksSupportStepId === element.WorksSupportStepId && obj.WorksSupportMemberRoleId === element.WorksSupportMemberRoleId);
                if (index === -1) {
                    self.ListWorksSupportTypeWfNxPermis.push(element);
                }
            });
        }
    }

    /**
     * Add worksTypeSupportTypeWfPermission to list
     * 
     * @param {Array<any>} list 
     * @memberof Permission
     */
    public addListWorksSupportTypeWfPermis(list: Array<any>) {
        let self: Permission = this;
        if (self.ListWorksSupportTypeWfPermis.length === 0) {
            list.forEach(element => {
                self.ListWorksSupportTypeWfPermis.push(element);
            });
        } else {
            list.forEach(element => {
                let index = self.ListWorksSupportTypeWfPermis.findIndex(obj => obj.WorksSupportStepId === element.WorksSupportStepId && obj.WorksSupportMemberRoleId === element.WorksSupportMemberRoleId);
                if (index === -1) {
                    self.ListWorksSupportTypeWfPermis.push(element);
                }
            });
        }
    }

    /**
     * Get Member role by name
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public searchMemerRole(event: any) {
        let self: Permission = this;
        let $ = window.jQuery;
        let strMemberRole = $("#textRole").val();
        memberService.getMemberByActived(strMemberRole, 0, 0, 200).then(function (items: Array<any>) {
            self.listMemberRoleOfPermission = items;
        });
    }

    /**
     * Get Member role by name
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public searchMemerRoleProcess(event: any) {
        let self: Permission = this;
        let $ = window.jQuery;
        let strMemberRole = $("#textRoleOfProccess").val();
        memberService.getMemberByActived(strMemberRole, 0, 0, 200).then(function (items: Array<any>) {
            self.listMemberRoleOfProccess = items;
        });
    }

    /**
     * Get Member role by name
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public searchWorkFlow(event: any) {
        let self: Permission = this;
        let $ = window.jQuery;
        let keyWord = $("#txtWorksFlow").val();
        let id = $("#WorksTypeId").val();
        memberService.getByIdAndName(id, keyWord).then(function (items: Array<any>) {
            self.ListWorksSupportTypeWorkFlow = items;
        });
    }

    /**
     * NextStep Change
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public onNextStepPermisChange(event: any) {
        let self: Permission = this;
        let $ = window.jQuery;
        let StepName = $('#permissionWorksFlow #WorkFlow-' + self.currentStepID)[0].innerText.trim();
        let WFNXId: string = event.target.id.replace('NX-', '');
        let isChecked = $('#roleOfPermissionProcess #CBRole-' + self.currentMemberRolePermisId)[0].checked;
        if (event.target.checked === false) {
            if (self.ListWorksSupportTypeWfNxPermis != null) {
                let index = self.ListWorksSupportTypeWfNxPermis.findIndex(obj => obj.NextWorksSupportStepId.toString() === WFNXId.toString() && obj.WorksSupportMemberRoleId.toString() == self.currentMemberRolePermisId.toString());
                if (index > -1) self.ListWorksSupportTypeWfNxPermis.removeItem(self.ListWorksSupportTypeWfNxPermis[index]);
            }
        }
        else {
            let wfnx: WfNxPermissModel = new WfNxPermissModel(self.currentStepID, StepName, self.currentTypeID, WFNXId, self.currentMemberRolePermisId);
            if (self.ListWorksSupportTypeWfNxPermis != null) {
                let index = self.ListWorksSupportTypeWfNxPermis.findIndex(obj => obj.NextWorksSupportStepId.toString() === WFNXId.toString() && obj.WorksSupportMemberRoleId.toString() == self.currentMemberRolePermisId.toString());
                if (index > -1) self.ListWorksSupportTypeWfNxPermis[index] = wfnx;
                else self.ListWorksSupportTypeWfNxPermis.push(wfnx);
            }
            else self.ListWorksSupportTypeWfNxPermis.push(wfnx);
        }
        self.ListWorksSupportTypeWorkFlow[self.ListWorksSupportTypeWorkFlow.findIndex(obj => obj.WorksSupportStepId.toString() === self.currentStepID.toString())].ListWorksSupportTypeWfNxPermis = self.ListWorksSupportTypeWfNxPermis;
    }

    /**
     * Save data
     * 
     * @memberof Permission
     */
    public onSaveClick() {
        let $ = window.jQuery;
        let self: Permission = this;
        let selectid = self.defaultRoleSelected == undefined ? "" : self.defaultRoleSelected.toString();
        self.ListWorksSupportTypeMemberRole.forEach(element => {
            if (element.WorksSupportMemberRoleId.toString() === selectid) element.IsDefaultRole = true;
            else element.IsDefaultRole = false;
        });
        self.permissionDetail.ListWorksSupportTypeWorkFlow = self.ListWorksSupportTypeWorkFlow;
        self.permissionDetail.ListWorksSupportTypeMemberRole = self.ListWorksSupportTypeMemberRole;
        self.permissionDetail.ListWorksSupportProjectPermis = self.ListWorksSupportProjectPermis;
        self.permissionDetail.ListWorksSupportTypeWfPermis = self.ListWorksSupportTypeWfPermis;
        self.permissionDetail.ListWorksSupportTypeWfNxPermis = self.ListWorksSupportTypeWfNxPermis;
        self.permissionDetail.User = self.userLogin;
        worksTypeService.createPermission(self.permissionDetail).then(function (items: Array<any>) {
            $('#saveSuccess').modal('show');
        });
    }


    /**
     * Reset data
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public resetData(event: any) {
        let self: Permission = this;
        worksTypeService.get(self.WorksTypeId).then(function (item: any) {
            localStorage.removeItem('permissionDetail');
            localStorage.setItem('permissionDetail', JSON.stringify(item));
            self.permissionDetail = JSON.parse(localStorage.getItem('permissionDetail'));
            self.currentTypeID = self.WorksTypeId;

            self.ListWorksSupportTypeMemberRole = self.permissionDetail.ListWorksSupportTypeMemberRole == null ? [] : self.permissionDetail.ListWorksSupportTypeMemberRole;
            self.ListWorksSupportTypeWorkFlow = self.permissionDetail.ListWorksSupportTypeWorkFlow == null ? [] : self.permissionDetail.ListWorksSupportTypeWorkFlow;
            self.ListWorksSupportTypeWfNx = self.ListWorksSupportTypeWorkFlow == null ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNx == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNx);
            self.ListWorksSupportTypeWfNxPermis = self.ListWorksSupportTypeWorkFlow == null ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNxPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfNxPermis);
            self.ListWorksSupportTypeWfPermis = self.ListWorksSupportTypeWorkFlow == null ? [] : (self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfPermis == null ? [] : self.ListWorksSupportTypeWorkFlow[0].ListWorksSupportTypeWfPermis);
            self.ListWorksSupportProjectPermis = self.permissionDetail.ListWorksSupportProjectPermis == null ? [] : self.permissionDetail.ListWorksSupportProjectPermis;

            let permissModel = self.ListWorksSupportTypeWfPermis.length == 0 ? null : self.ListWorksSupportTypeWfPermis[0];
            let wfPermissModel: WfPermissModel = null;
            if (permissModel != null) {
                self.wfPermissModel = new WfPermissModel(
                    permissModel.WorksSupportStepId,
                    permissModel.WorksSupportMemberRoleId,
                    permissModel.IsCanEditContent,
                    permissModel.IsCanEditSolutionContent,
                    permissModel.IsCanAddAttachment,
                    permissModel.IsCanComment,
                    permissModel.IsCanEditExpectedCompletedDate,
                    permissModel.IsCanChangeProgress,
                    permissModel.IsCanEditQuality,
                    permissModel.IsCanProcessWorkflow,
                    permissModel.IsMustChooseProcessUser,
                );
            }
            else {
                self.wfPermissModel = new WfPermissModel(
                    self.ListWorksSupportTypeWorkFlow[0].WorksSupportStepId, 0, false, false, false, false, false, false, false, false, false
                );
            }
            let memberRole = self.permissionDetail.ListWorksSupportTypeMemberRole == null ? null : self.ListWorksSupportTypeMemberRole[0];
            let memberRoleModel: MemberRoleModel = null;
            if (memberRole != null) memberRoleModel = new MemberRoleModel(
                memberRole.WorksSupportTypeId, memberRole.WorksSupportMemberRoleId,
                memberRole.IsCanAddWorksSupportGroup, memberRole.IsCanAddWorksSupport,
                memberRole.IsCanEditWorksSupportGroup, memberRole.IsCanEditWorksSupport, memberRole.IsCanDeleteWorksSupportGroup,
                memberRole.IsCanDeleteWorksSupport, memberRole.WorksSupportMemberRoleName, memberRole.IsDefaultRole
            );
            else {
                memberRoleModel = new MemberRoleModel(
                    self.permissionDetail.WorksSupportTypeId, 0, false, false, false, false, false, false, null, false
                );
            }
            self.memberRoleModel = memberRoleModel;

            let objDefaultMemberRole = self.ListWorksSupportTypeMemberRole == null ? null : (self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.IsDefaultRole === 1) == null ? null : self.ListWorksSupportTypeMemberRole[self.ListWorksSupportTypeMemberRole.findIndex(obj => obj.IsDefaultRole === 1)]);
            if (objDefaultMemberRole != null) {
                self.defaultRoleSelected = objDefaultMemberRole.WorksSupportMemberRoleId;
            }
            self.currentStepID = self.ListWorksSupportTypeWorkFlow.length == 0 ? 0 : self.ListWorksSupportTypeWorkFlow[0].WorksSupportStepId;
            self.isSaved = true;

            self.loadMemberRole();
            self.loadMemberRoleOfProccess();
        });
    }

    // start huan do it
    public IsGetMember: boolean = true;

    public getAllMembersByDepart() {
        let self: Permission = this;
        let $ = window.jQuery;
        // close combobox department
        //  $(".bs-searchbox").parent().css("display", "none");
        let lstTemp = self.ListWorksSupportProjectPermis;
        if (this.IsGetMember) {
            this.IsGetMember = false;
            memberService.getMembersByDepartment().then(function (items: Array<any>) {
                self.lstMemberOfDepart = self.setMemberIsSeleted(items, lstTemp);
            });
        } else {
            self.lstMemberOfDepart = self.setMemberIsSeleted(self.lstMemberOfDepart, lstTemp);
        }
    }

    public addMembersForDepart(id: any, name: string) {
        let self: Permission = this;
        this.lstMemberOfDepart.forEach(element => {
            if (element.IsMember === null && element.UserId === id) {
                this.lstAddMemberOfDepart.push({ "userName": id, "fullName": name });
                element.IsMember = id;
            }
        });
    }

    public loadAllDepartment() {
        let $ = window.jQuery;
        let self: Permission = this;
        memberService.getAllDepartment().then(function (items: Array<any>) {
            self.lstDepartment = items;
            $("#selDepart").selectpicker("refresh");
        });
    }

    public isSelect: boolean = true;
    public selectPickerOfDepart(event: any) {

        let $ = window.jQuery;
        let self: Permission = this;
        if (this.isSelect || this.isSelect == undefined) {
            this.isSelect = false;
            $(".bs-searchbox").parent().css("display", "block");
        } else {
            this.isSelect = true;
            $(".bs-searchbox").parent().css("display", "none");
        }

    }

    public lstTempMemberOfDepart: Array<any> = [];
    public findDepartmentByName(strId: string) {
        if (strId === null) {
            this.lstMemberOfDepart.forEach(element => {
                element.IsShowByDepart = true;
            });
        } else {
            this.lstMemberOfDepart.forEach(element => {
                element.IsShowByDepart = false;
                for (let i = 0; i < strId.length; i++) {
                    if (element.DepartmentId === strId[i]) {
                        element.IsShowByDepart = true;
                        break;
                    }
                }
            });
        }
    }

    public findMembersByDepartments(strName: string) {
        let $ = window.jQuery;
        if (strName.trim() === "") {
            this.lstMemberOfDepart.forEach(element => {
                element.IsShowByMember = true;
            });
        } else {
            this.lstMemberOfDepart.forEach(element => {
                element.IsShowByMember = false;
                let memberName = (element.UserName === null) ? "" : element.UserName;
                if ((memberName).toLowerCase().search(strName.toLowerCase()) >= 0) {
                    element.IsShowByMember = true;
                }
            });
        }
    }

    public removeMemberOfDepart(id: any) {
        let $ = window.jQuery;
        let arrMemberOfDepart = this.lstAddMemberOfDepart;
        for (let i = 0; i < arrMemberOfDepart.length; i++) {
            if (arrMemberOfDepart[i].userName === id) {
                arrMemberOfDepart.splice(i, 1);
                $("#li_" + id).removeClass("selected");
                break;
            }
        }
        this.lstMemberOfDepart.forEach(element => {
            if (element.UserId === id) {
                element.IsMember = null;
            }
        });
        this.lstAddMemberOfDepart = arrMemberOfDepart;
    }
    // end huan

    public addMember(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        this.lstAddMemberOfDepart.forEach(element => {
            let index: any = self.ListWorksSupportProjectPermis.findIndex(obj => obj.UserName === element.userName);
            if (index === -1) {
                let userModel = new UserMemberModel(
                    self.WorksTypeId, element.userName, element.fullName, false, false, false, false
                );
                self.ListWorksSupportProjectPermis.push(userModel);

            }
        });
        this.lstAddMemberOfDepart = [];
        $("#txtSearchMember").val('');
        $("#popupSearch").addClass("open");
    }

    public setMemberIsSeleted(items: Array<any>, lstMember: Array<any>): Array<any> {
        //1. Browse element in array.
        items.forEach(element => {
            element.IsMember = null;
            //2. find elements existed and set IsMember = id after remove element was browsed.
            for (let i = 0; i < lstMember.length; i++) {
                if (lstMember[i].UserName === element.UserId) {
                    element.IsMember = element.UserId;
                }
            }
        });
        return items;
    }

    /**
     * Show popup to copy permission
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public popupCopyPermission(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        $('#popCopyPermission').modal('show');
    }

    /**
     * Copy current permission to another
     * 
     * @param {*} event 
     * @memberof Permission
     */
    public copyPermission(event: any) {
        let $ = window.jQuery;
        let self: Permission = this;
        let model = new CopyPermissionModel(
            self.WorksTypeId, self.fromWorkTypeId, self.userLogin
        );
        worksTypeService.copyPermission(model).then(function (items: Array<any>) {
            $('#saveSuccess').modal('show');
        });
    }
}

