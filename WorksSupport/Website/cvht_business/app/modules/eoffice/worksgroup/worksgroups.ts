/**
 * Created by  :   Nguyen Van Huan
 * Created date:   16/06/2017
 * Description :   Work group screen.
 */
// import { CustomFormsModule } from 'ng2-validation';
import { Component, ViewChild, OnInit, Input } from "angular2/core";
import { Router } from "angular2/router";
import { Http } from "angular2/http";
import { BasePage } from "../../../common/models/ui";
import { FormTextInput, FormTextArea } from "../../../common/directive";
import { FormMode } from "../../../common/enum";
import { WorksGroupsModel } from "./worksGroupsModel";
import { DeleteModel } from "./deleteModel";
import { UpdateModel } from "./updateModel";
import worksGroupService from "../_share/services/WorksGroupService";
import route from "../_share/config/route";
import sessionStorage from "../../../common/storages/sessionStorage";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { KeyNamePair } from "../../../common/models/keyNamePair";
import { UpdateModelProject } from "./updateModel";
import { ValidationException } from "../../../common/models/exception";
import { RoleValue } from "../../../common/enum";
import projectService from "../_share/services/projectService";

@Component({
    templateUrl: "app/modules/eoffice/worksgroup/worksgroups.html",
    directives: [FormTextInput, FormTextArea]
})

export class WorksGroups extends BasePage implements OnInit {
    private Id: number = 0;
    private WorksSupportGroupName: string = String.empty;
    private WorksSupportProjectName: string = String.empty;
    private WorksSupportProjectNameShow: string = String.empty;
    private WorksSupportProjectId: number = 0;
    private Description: string = String.empty;
    private IsSystem: number = 0;
    private IsActived: number = 0;
    private WorksSupportGroupId: number = 0;
    private Router: Router;
    private route: Router;
    private selectedValue: Array<any> = [];
    private isCanAdd: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD) === "true" ? true : false;
    private isCanDelete: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    private IscanViewWork: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPPORT_VIEW) === "true" ? true : false;
    private roleValue: string = RoleValue.Admin;
    private isAdmin: boolean = sessionStorage.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    private userLogin = this.isAdmin === false ? sessionStorage.get(CACHE_CONSTANT.strUserName) : "administrator";
    private User: string = String.empty;
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
    private CurrentProjectId: number;
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
    private LstShowRole: Array<any> = [{ "roleId": "default", "roleName": "Tất cả" },
    { "roleId": "role", "roleName": "Vai trò" }]
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
    private IsRole: boolean = true;
    private ShowRoleProject = "default";
    constructor(router: Router) {
        super();
        let self: WorksGroups = this;
        let $ = window.jQuery;
        self.Router = router;
        self.model = new WorksGroupsModel(self.i18nHelper);
        let projectId = localStorage.getItem('projectId');
        self.getAllProjectsByUserLogin(projectId);
    }

    ngOnInit() {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        $("ul.list-inline li").hover(function () {
            $("ul.list-inline li").each(function () {
                $(this).html($(this).html().replace("<marquee>", "").replace("</marquee>", ""))
            })
            var text = $(this).html();
            text = "<marquee>" + text + "</marquee>";
            $(this).html(text);
        });
    }

    private redirectToWork(WorksSupportGroupId: any) {
        localStorage.setItem('projectId', this.CurrentProjectId.toString());
        localStorage.setItem('worksGroupId', WorksSupportGroupId.toString());
        localStorage.setItem('WorkGroup_WorkTypeId', this.CurrentWorkTypeId);
        this.Router.navigate([route.work.works.name]);
    }

    private backToProjectPage(event: any) {
        this.Router.navigate([route.project.projects.name]);
    }

    /**
     * Writen by  : Nguyen Van Huan
     * Create by  : 10/08/2017
     * Description: show edit popup of work group.
     * @param worksSupportGroupId 
     */
    private onShowPopupEdit(worksSupportGroupId: any) {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        this.CurrentWorkGroupId = worksSupportGroupId;
        this.IsAddPopup = false;
        this.IsAddWorkGroup = false;
        this.SearchProjectMemberToAdd = String.empty;
        $("#modalUpdateWorkGroup").modal('toggle');
        $("#changeTagInfo,#tabAddWorkgroup").addClass("active");
        $("#changeTagMember,#member").removeClass("active");
        this.LstShowMemberDefault = [];
        this.LstShowMemberRole = [];
        this.LstSearchProjectMember = [];
        this.clearValidationPopupWorkGroup();
        worksGroupService.getWorksGroupsByGroupId(this.CurrentWorkGroupId).then(function (item: any) {
            self.WorksSupportGroupName = item.WorksSupportGroupName;
            self.Description = item.Description;
            self.IconUrl = item.IconUrl === null ? "" : item.IconUrl;
            self.IsSystem = item.IsSystem;
            self.IsActived = item.IsActived;
        });
        this.KeySearchMember = String.empty;
        this.ShowIsDefault = true;
        this.ShowRole = "default";
        worksGroupService.getRoleByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstMemberRole = items;
            self.setRoleNameInLstShowMemberRole();
            self.findWorkGroup("", self.KeySearchMember);
            worksGroupService.getWorksGroupMember(self.CurrentWorkGroupId).then(function (items: Array<any>) {
                self.LstWorkGroupMember = items;
                self.setMembersIsRole();
                self.setMembersIsDefault();
                self.findMemberWorkGroup("");
            });
        });
    }

    private getAllProjectsByUserLogin(projectId: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.lstProject = [];
        worksGroupService.getAllProjectIsActivedByUser(this.userLogin).then(function (items: Array<any>) {
            for (let i = 0; i < items.length; i++) {
                items[i].IconUrl = items[i].IconUrl === null ? "fa fa-folder" : items[i].IconUrl;
                if (parseInt(items[i].WorksSupportProjectId) === parseInt(projectId)) {
                    self.WorksSupportProjectNameShow = items[i].WorksSupportProjectName;
                    self.CurrentWorkTypeId = items[i].WorksSupportTypeId;
                    self.getWorkGroupByProjectId(projectId, "", self.CurrentWorkTypeId, "", items[i].IsCanEdit, items[i].IsSystem, items[i].IconUrl);
                }
            }
            self.lstProject = items;
            self.sortProjectNameIsDesc(self.lstProject);
        });
    }

    private getWorkGroupByProjectId(projectId: any, projectName: any, typeId: any, isActived: any, isCanEditProject: any, isSystem: any, iconUrl: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        self.CurrentIconUrlProject = iconUrl === null ? "fa fa-folder" : iconUrl;
        self.IsCanEditProject = (isCanEditProject && isSystem === 0);
        this.CurrentProjectId = parseInt(projectId);
        this.SearchWorkGroup = false;
        this.txtSearchWorkGroup = String.empty;
        localStorage.setItem("projectId", projectId.toString());
        if (typeId !== "") {
            this.CurrentWorkTypeId = typeId;
        }
        this.model.ProjectId = projectId;
        this.model.WorkTypeId = this.CurrentWorkTypeId;
        this.model.UserName = this.userLogin;
        worksGroupService.getWorksGroups(projectId).then(function (items: Array<any>) {
            self.LstWorksGroup = items;
            self.findWorkGroup("", "");
        });

        //lay quyen vai tro cho nhom trong cong viec
        if (!this.isAdmin) {
            worksGroupService.getWorksGroupRole(projectId, this.CurrentWorkTypeId, this.userLogin).then(function (item: any) {
                self.RoleOfWorkGroup = item;
            });
        }

        this.CurrentProjectId = projectId;
        if (projectName !== "") {
            this.WorksSupportProjectNameShow = projectName;
        }
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: get all member of project by project id
     */
    private getAllMemberByProjectId() {
        let self: WorksGroups = this;
        // add popup
        if (this.IsAddPopup) {
            // lay tat ca thanh vien trong du an
            worksGroupService.getAllMemberByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
                self.NumberMemberOfWorkGroup = 0;
                self.LstProjectMember = items;
                self.setMembersIsRole();
                self.setMembersIsDefault();
                self.findMemberWorkGroup("");
            });
        } else {
            worksGroupService.getWorksGroupMember(this.CurrentWorkGroupId).then(function (items: Array<any>) {
                self.LstWorkGroupMember = items;
                self.findMemberWorkGroup("");
            });
        }
    }

    /**
      * Writen by  : Nguyen van Huan
      * Create by  : 10.08.2017
      * Description: select icon on workgroup popup
      */
    private onIconIsSelected() {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }
    /**
       * Writen by  : Nguyen van Huan
       * Create by  : 10.08.2017
       * Description: select icon on project popup
       */
    private onIconIsSelectedOfProject() {
        if (!(this.IsCanEditProject || this.isAdmin)) {
            return false;
        }
        let $ = window.jQuery;
        let self: WorksGroups = this;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }

    /**
      * Writen by  : Nguyen van Huan
      * Create by  : 10.08.2017
      * Description: show delete workgroup popup      
      * @param id 
      * @param name 
      */
    private ShowPopupDelete(id: any, name: any) {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        $("#ModalRemove").modal('toggle');
        this.SelWorksGroupId = id;
        this.SelWorksGroupName = name;
        this.selectedValue = new Array();
        this.selectedValue.push({ id, name });
        this.objDelete = new DeleteModel(this.selectedValue[0].id, this.userLogin);
    }

    /**
        * Writen by  : Nguyen van Huan
        * Create by  : 10.08.2017
        * Description: remove work group by id 
        * @param event 
        */
    private removeWorkGroupById(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        worksGroupService.delete(this.SelWorksGroupId, this.userLogin).then(function (event: any) {
            $("#ModalRemove").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksGroupsModel(self.i18nHelper);
            self.load();
        });
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: load workgroup
     */
    private load() {
        let self: WorksGroups = this;
        worksGroupService.getWorksGroups(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstWorksGroup = items;
            self.LstWorksGroup.sort(self.compareWorkGroup);
        });
    }
    /**
      * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: show popup add
     * @param event 
     */
    private showPopupAdd(event: any) {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        this.IsAddPopup = true;
        this.IsAddWorkGroup = true;
        this.ShowIsDefault = true;
        this.ShowRole = "default";
        self.WorksSupportGroupName = String.empty;
        self.Description = String.empty;
        self.IconUrl = String.empty;
        this.SearchProjectMemberToAdd = String.empty;
        this.clearValidationPopupWorkGroup();
        $("#changeTagInfo,#tabAddWorkgroup").addClass("active");
        $("#changeTagMember,#member").removeClass("active");
        // get all members in project
        // get members have IsAutoAddMemberToWorksGroup == 1; push into lstShowMemberDefault and LstShowMemberRole
        // this.LstShowMemberRole = [];
        worksGroupService.getRoleByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstMemberRole = items;
            self.setRoleNameInLstShowMemberRole();
            self.findWorkGroup("", this.KeySearchMember);
            self.getAllMemberByProjectId();
        });
        this.LstShowMemberDefault = [];
        this.KeySearchMember = String.empty;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: set member list is default style.
     */
    private setMembersIsDefault() {
        // add popup
        if (this.IsAddPopup) {
            this.LstShowMemberDefault = [{ "Title": "Thành viên nguồn dự án", "LstMember": [] },
            { "Title": "Thành viên cơ bản", "LstMember": [] }];
            this.LstProjectMember.forEach(projectMember => {
                // thanh vien mac dinh hien thi 
                if (projectMember.IsAutoAddMemberToWorksGroup === 1) {
                    this.LstShowMemberDefault[0].LstMember.push(projectMember);
                }
            });
            let lstMember: Array<any> = this.LstShowMemberDefault[0].LstMember;
            lstMember.sort(this.compare);
            this.LstShowMemberDefault[0].LstMember = lstMember;
        } else {
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
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: set role of member
     */
    private setMembersIsRole() {
        // add popup
        if (this.IsAddPopup) {
            //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
            this.LstProjectMember.forEach(projectMember => {
                //1.1 duyệt từng đối tượng va so sánh roleid
                let index = this.LstShowMemberRole.findIndex(showMember => showMember.WorksSupportMemberRoleId === projectMember.WorksSupportMemberRoleId);
                // kiểm tra xem roleid đã tồn tại chưa
                if (index !== -1) {
                    if (projectMember.IsAutoAddMemberToWorksGroup === 1) {
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
                } else {
                    if (projectMember.IsAutoAddMemberToWorksGroup === 1) {
                        //1.6 thêm mới đối tượng cho lstShowMember.
                        let obj = {};
                        obj["WorksSupportMemberRoleId"] = projectMember.WorksSupportMemberRoleId;
                        obj["WorksSupportMemberRoleName"] = projectMember.WorksSupportMemberRoleName;
                        obj["LstMember"] = [];
                        obj["count"] = 0;
                        obj["LstMember"].push(projectMember);
                        //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                        this.LstShowMemberRole.push(obj);
                    }
                }
            });
        } else { // popup Edit
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
                } else {
                    //1.6 thêm mới đối tượng cho lstShowMember.
                    let obj = {};
                    obj["WorksSupportMemberRoleId"] = worksGroupMember.WorksSupportMemberRoleId;
                    obj["WorksSupportMemberRoleName"] = worksGroupMember.WorksSupportMemberRoleName;
                    obj["LstMember"] = [];
                    obj["count"] = 0;
                    obj["LstMember"].push(worksGroupMember);
                    //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                    this.LstShowMemberRole.push(obj);
                }
            });
        }
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
     * Description: show popup search user 
     */
    private showPopupSearchUser() {
        //1. load all project members.
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.SearchMemberWorkGroupInfor = false;
        this.IsCheckAll = false;
        this.LstSearchProjectMember = [];
        this.SearchProjectMemberToAdd = String.empty;
        worksGroupService.getAllMemberByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstSearchProjectMember = items;
            self.filterMemberListAfterSearch();
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
        let self: WorksGroups = this;
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
     * Description: select user of workgroup
     * @param userName 
     * @param keySearch 
     */
    private ClickUserAutoWorkGroup(userName: any, keySearch: any) {
        let $ = window.jQuery;
        // show member default
        for (let i = 0; i < this.LstShowMemberDefault.length; i++) {
            let lstMembers: Array<any> = this.LstShowMemberDefault[i].LstMember;
            let member = lstMembers.find(member => member.UserName === userName);
            if (member !== undefined) {
                if (member.IsAutoAddMemberToWorksGroup === 0) {
                    this.LstShowMemberDefault[1].LstMember.removeItem(member);
                    member.IsAutoAddMemberToWorksGroup = 1;
                    let lstMember: Array<any> = this.LstShowMemberDefault[0].LstMember;
                    lstMember.push(member);
                    lstMember.sort(this.compare);
                    this.LstShowMemberDefault[0].LstMember = lstMember;
                } else {
                    this.LstShowMemberDefault[0].LstMember.removeItem(member);
                    member.IsAutoAddMemberToWorksGroup = 0;
                    let lstMember: Array<any> = this.LstShowMemberDefault[1].LstMember;
                    lstMember.push(member);
                    lstMember.sort(this.compare);
                    this.LstShowMemberDefault[1].LstMember = lstMember;
                }
                break;
            }
        }
        // hien thanh vien 
        this.findMemberWorkGroup(keySearch);
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: remove work group member
     * @param userName 
     */
    private removeWorkGroupMember(userName: any) {
        let $ = window.jQuery;
        this.ShowProjectTab = false;
        let self: WorksGroups = this;
         if (!this.RoleOfWorkGroup.IsCanEditWorksSupportGroup && !this.isAdmin) {
            return false;
        }
        worksGroupService.checkUserProcess(userName, this.CurrentWorkGroupId).then(function (item: any) {
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
                $("#modalErorr").modal("toggle");
            }
        }).error(function (error: any) {
            self.IsRemoveMember = true;
            $("#modalErorr").modal("toggle");
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
        worksGroupService.checkUserProcess(userName, workGroupId).then(function (item: any) {
            let obj = item;
            if (obj.NumberOfMember > 0) {
                return true;
            }
        });
        return false;
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
        this.setDefaultMemberList();
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
     * Description: check all members when search member
     * @param event 
     */
    private selectUserWorkGroup(event: any) {
        let self: WorksGroups = this;
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
     * Description: add or edit workgroup 
     */
    private onEditOrAddWorkGroup() {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        let id = this.WorksSupportGroupId;
        let worksSupportGroupName = this.WorksSupportGroupName;
        let worksSupportProjectId = this.CurrentProjectId;
        let description = this.Description;
        let isActive = 1;
        let isSystem = 1;
        let iconUrl = this.IconUrl;
        let user = this.userLogin;
        let lstNewMember = this.getLstNewMember();
        //1. add workgroup
        if (this.IsAddPopup && this.validationPopupWorkGroup()) {
            let objWorksGroup = new UpdateModel(this.CurrentProjectId, worksSupportProjectId, worksSupportGroupName, description, isActive, isSystem,
                iconUrl, user, lstNewMember, [], []);
            worksGroupService.create(objWorksGroup).then(function (item: any) {
                $('#modalUpdateWorkGroup').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.load();
            }).error(function (error: any) {
                if (!self.validateOnServerOfWorkGroup(error)) {
                    self.IsClickTab = true;
                }
            });

        }
        //2. edit workgroup
        if (!this.IsAddPopup && this.validationPopupWorkGroup()) {
            this.filterMemberListBeforeSubmit();
            let objWorksGroup = new UpdateModel(this.CurrentWorkGroupId, worksSupportProjectId, worksSupportGroupName, description, isActive, isSystem,
                iconUrl, user, this.LstNewMember, this.LstDeleteMember, this.LstEditMember);
            worksGroupService.edit(objWorksGroup).then(function (item: any) {
                $('#modalUpdateWorkGroup').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.load();
            }).error(function (error: any) {
                if (!self.validateOnServerOfWorkGroup(error)) {
                    self.IsClickTab = true;
                }
            });
        }
        if (!this.validationPopupWorkGroup()) {
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
                if (member == undefined) {
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
     * Description: find work group  and sort a->z 
     * @param event 
     * @param keyName work group name
     */
    private findWorkGroup(event: any, keyName: any) {
        let self: WorksGroups = this;
        keyName = keyName === undefined ? "" : keyName;
        // if (keyName.trim() === "") {
        //     this.SearchWorkGroup = false;
        //     for (let i = 0; i < this.LstWorksGroup.length; i++) {
        //         let lstMember: Array<any> = this.LstWorksGroup;
        //         lstMember.forEach(element => {
        //             element.IsDeleted = 0;
        //         });
        //     }
        //     self.LstWorksGroup.sort(self.compareWorkGroup);
        // } else {
        //     let countSearch = 0;
        //     for (let i = 0; i < this.LstWorksGroup.length; i++) {
        //         let lstMember: Array<any> = this.LstWorksGroup;
        //         let countMember = lstMember.length;
        //         lstMember.forEach(member => {
        //             member.IsDeleted = -1;
        //             let WorksSupportGroupName = (member.WorksSupportGroupName === null) ? "" : member.WorksSupportGroupName;
        //             if ((WorksSupportGroupName).toLowerCase().search(keyName.toLowerCase()) >= 0) {
        //                 member.IsDeleted = 0;
        //                 countSearch++;
        //             }
        //         });
        //     }
        //     // show error mesage if not found work group
        //     if (countSearch === 0) {
        //         this.SearchWorkGroup = true;
        //     } else {
        //         this.SearchWorkGroup = false;
        //     }
        //     self.LstWorksGroup.sort(self.compareWorkGroup);
        // }
        worksGroupService.getWorksGroupsByKeyWords(this.CurrentProjectId, keyName).then(function (items: Array<any>) {
            self.LstWorksGroup = items;
            self.LstWorksGroup.sort(self.compareWorkGroup);
        });
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
            } else {
                //11. thêm mới đối tượng cho lstShowMember.
                let obj = {};
                obj["WorksSupportMemberRoleId"] = parseInt(newRoleId);
                obj["WorksSupportMemberRoleName"] = newRoleName;
                obj["LstMember"] = [];
                obj["count"] = 0;
                //12. cap nhat lai roleid cho thanh vien nay
                member.WorksSupportMemberRoleId = parseInt(newRoleId);
                obj["LstMember"].push(member);
                //13. thêm đối tượng vừa tạo vào lstShowMember.
                this.LstShowMemberRole.push(obj);
            }
        }
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
     * Description: validate the same name on server 
     */
    private validateOnServerOfWorkGroup(error: any): boolean {
        let self: WorksGroups = this;
        let index: number;
        if (error != null && error != undefined) {
            for (let i = 0; i < error.length; i++) {
                if (error[i].Key === "workGroupName") {
                    this.ErrorWorkGroupName = error[i].Message;
                    return false;
                }
            }
        }
        return true;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: validate workgroup popup
     */
    private validationPopupWorkGroup(): boolean {
        let workGrouptName = this.WorksSupportGroupName === null || this.WorksSupportGroupName === undefined ? "" : this.WorksSupportGroupName.trim();
        let description = this.Description === null || this.Description === undefined ? "" : this.Description;
        let isError = true;
        this.ErrorWorkGroupName = String.empty;
        this.ErrorDescription = String.empty;
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
     * Writen by  : Nguyen Van Huan
     * Create date: 10/08/2017
     * Description: clear validate on work group popup
     */
    private clearValidationPopupWorkGroup() {
        this.ErrorWorkGroupName = String.empty;
        this.ErrorDescription = String.empty;
    }
    // xắp xếp nhóm công việc theo ABC 
    private compareWorkGroup(obj1: any, obj2: any) {
        return ((obj1.WorksSupportGroupName).toLowerCase()).localeCompare((obj2.WorksSupportGroupName).toLowerCase());
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

    private compareProjectName(obj1: any, obj2: any) {
        return ((obj1.WorksSupportProjectName).toLowerCase()).localeCompare((obj2.WorksSupportProjectName).toLowerCase());
    }
    // End WorksGroup

    // Start project
    // show popup update project
    private worksSupportTypeId: string = "";
    private numberMemberOfProject: number = 0;
    private checkIconUrl: boolean = false;
    private lstShowMember: Array<any> = [];
    private CurrentWorksSupportTypeId: any;
    private WorksSupportTypeId: any = "";
    private lstMemberRole: Array<any> = [];
    private lstMemberIsAdded: Array<any> = [];
    private lstWorksType: Array<any> = [];
    private ErrorProjectName: string = String.empty;
    private ErrorWorksSupportTypeId: string = String.empty;
    private IsDefaultRole: number = -1;
    private WorksSupportDepartmentId: number = -1;
    private lstSearchMember: Array<any> = [];
    private lstDepartment: any = [];
    private lstMember: Array<any> = [];
    private txtKeyNameUser: any = String.empty;
    private isCheckAll: boolean = false;
    private IsCanEditProject: boolean = false;
    private lstShowAllMember: Array<any> = [];
    private SearchMemberPopup: boolean = false;
    private SearchMemberInfor: boolean = false;
    private ShowProjectTab: boolean = false;
    private LstShowRoleProject: Array<any> = [{ "roleId": "default", "roleName": "Tất cả" }, { "roleId": "role", "roleName": "Vai trò" }]
    /**
    * Writen by: Nguyen van Huan
    * Create by: 10.08.2017
    * show popup update project
    * @param id project id
    */
    private onUpdateProjectClick() {
        let $ = window.jQuery;
        this.IsAddPopup = false;
        this.KeySearchMember = "";
        let self: WorksGroups = this;
        this.lstShowMember = [];
        $("#changeTag1, #tabAddProject").removeClass("active");
        this.clearValidationPopupProject();
        self.ShowRoleProject = "default";
        self.IsRole = true;
        this.ShowProjectTab = true;
        projectService.get(this.CurrentProjectId).then(function (item: any) {
            self.WorksSupportProjectId = item.WorksSupportProjectId;
            self.CurrentWorksSupportTypeId = item.WorksSupportTypeId;
            self.WorksSupportProjectName = item.WorksSupportProjectName;
            self.WorksSupportTypeId = item.WorksSupportTypeId;
            self.Description = item.Description;
            self.IconUrl = item.IconUrl;
            self.IsSystem = item.IsSystem;
            self.IsActived = item.IsActived;
            self.numberMemberOfProject = item.NumberOfMember;
            projectService.getMemberRoleByTypeId(self.WorksSupportTypeId).then(function (items: Array<any>) {
                self.lstMemberRole = items;
                // xet vai tro cho thanh vien
                if (self.WorksSupportTypeId.toString() === self.CurrentWorksSupportTypeId.toString()) {
                    self.setRoleNameInLstShowMember();
                    projectService.getProjectMember(self.WorksSupportProjectId).then(function (items: Array<any>) {
                        self.LstCurrentMember = items;
                        self.addEditMemberIntoMemberList()
                        self.findMemberProject();
                        self.setDefaultMemberList();
                    });
                }
            });
        });
        projectService.getAllWorksType(self.userLogin).then(function (items: Array<any>) {
            self.lstWorksType = items;
        });
    }
    /**
    * Writen by: Nguyen van Huan
    * Create by: 10.08.2017
    * clear validate on project popup
    */
    private clearValidationPopupProject() {
        this.ErrorProjectName = String.empty;
        this.ErrorDescription = String.empty;
        this.ErrorWorksSupportTypeId = String.empty;
        this.IsDefaultRole = -1;
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: change style view
     */
    private selectShowRoleProject() {
        if (this.ShowRoleProject === "default") {
            this.IsRole = false;
        } else {
            this.IsRole = true;
        }
        this.findMemberProject();
    }
    /**
      * Writen by: Nguyen van Huan
      * Create by: 10.08.2017
      * set role list to add members.
      */
    private setRoleNameInLstShowMember() {
        this.lstShowMember = [];
        this.lstMemberRole.forEach(role => {
            let obj = {};
            obj["WorksSupportMemberRoleId"] = role.WorksSupportMemberRoleId;
            obj["WorksSupportMemberRoleName"] = role.WorksSupportMemberRoleName;
            obj["count"] = 0;
            obj["lstMember"] = [];
            this.lstShowMember.push(obj);
        });
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * add new member into member list on popup edit
     */
    private addEditMemberIntoMemberList() {

        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.LstCurrentMember.forEach(searchMember => {

            //1.1 duyệt từng đối tượng va so sánh roleid
            let index = this.lstShowMember.findIndex(showMember => parseInt(showMember.WorksSupportMemberRoleId) === parseInt(searchMember.WorksSupportMemberRoleId));
            // kiểm tra xem roleid đã tồn tại chưa
            if (index !== -1) {
                // thêm phần tử và sắp xếp vào lstShowMember.
                //1.2 lấy phần tử trong lstMember trong lstShowMember theo roleid.
                let lstMember: Array<any> = this.lstShowMember[index].lstMember;
                //1.3 thêm phần thành viên mới vào lstMember.              
                lstMember.push(searchMember);
                //1.4 xắp xếp theo thứ tự từ A->Z của trường FullName.
                lstMember.sort(this.compare);
                //1.5 thay lstMember cũ bằng lstMember mới đã được xắp xếp.
                this.lstShowMember[index].lstMember = lstMember;
            } else {
                //1.6 thêm mới đối tượng cho lstShowMember.
                let obj = {};
                obj["WorksSupportMemberRoleId"] = searchMember.WorksSupportMemberRoleId;
                obj["WorksSupportMemberRoleName"] = searchMember.WorksSupportMemberRoleName;
                obj["lstMember"] = [];
                obj["count"] = 0;
                obj["lstMember"].push(searchMember);
                //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                this.lstShowMember.push(obj);
            }
        });

    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * find member of project on client
     */
    private findMemberProject() {
        let keySearch = this.KeySearchMember;
        this.numberMemberOfProject = 0;
        if (keySearch.trim() === "") {
            for (let i = 0; i < this.lstShowMember.length; i++) {
                let lstMember: Array<any> = this.lstShowMember[i].lstMember;
                let count = 0;
                lstMember.forEach(element => {
                    element.IsChecked = false;
                    count++;
                });
                this.lstShowMember[i].count = count;
                this.numberMemberOfProject += count;
                this.SearchMemberInfor = false;
            }
        } else {
            for (let i = 0; i < this.lstShowMember.length; i++) {
                let lstMember: Array<any> = this.lstShowMember[i].lstMember;
                let countMember = lstMember.length;
                let count = 0;
                lstMember.forEach(member => {
                    member.IsChecked = true;
                    let fullName = (member.FullName === null) ? "" : member.FullName;
                    if ((fullName).toLowerCase().search(keySearch.toLowerCase()) >= 0) {
                        member.IsChecked = false;
                        count++;
                    }
                });
                this.lstShowMember[i].count = count;
                this.numberMemberOfProject += count;
            }
            // show message if not found member of project.
            if (this.numberMemberOfProject > 0) {
                this.SearchMemberInfor = false;
            } else {
                this.SearchMemberInfor = true;
            }
        }
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * validate project popup 
     */
    private validationPopupProject(): boolean {
        let projectName = this.WorksSupportProjectName === null ? "" : this.WorksSupportProjectName;
        let discription = this.Description === null ? "" : this.Description;
        let worksSupportTypeId = this.WorksSupportTypeId === null ? "" : this.WorksSupportTypeId;
        let isError = true;
        this.ErrorProjectName = String.empty;
        this.ErrorDescription = String.empty;
        this.ErrorWorksSupportTypeId = String.empty;
        // kiem tra rong
        if (projectName.length === 0) {
            this.ErrorProjectName = "Tên dự án là bắt buộc!";
            isError = false;
        }
        // kiem tra chuoi quá 200 kí tự
        if (projectName.length > 200) {
            this.ErrorProjectName = "Tên dự án không được vượt quá 200 kí tự!";
            isError = false;
        }
        // kiểm tra mô tả vượt quá 2000 kí tự
        if (discription.length > 2000) {
            this.ErrorDescription = "Tên dự án không được vượt quá 2000 kí tự!";
            isError = false;
        }
        // kiểm tra combobox khi duoc chon.
        if (worksSupportTypeId === "") {
            this.ErrorWorksSupportTypeId = "Loại công việc là bắt buộc!";
            isError = false;
        } else if (this.IsDefaultRole === 0) {
            this.ErrorWorksSupportTypeId = "Vui lòng khai báo xét vai trò cho loại công việc!";
            isError = false;
        }
        return isError;
    }
    /**
      * Writen by  : Nguyen van Huan
      * Create by  : 10.08.2017
      * Description:  select user when click into picture
     * @param userName 
     */
    private ClickUserAuto(userName: any) {
        if (!(this.IsCanEditProject || this.isAdmin)) {
            return false;
        }
        let $ = window.jQuery;
        for (let i = 0; i < this.lstShowMember.length; i++) {
            let lstMember: Array<any> = this.lstShowMember[i].lstMember;
            let index = lstMember.findIndex(member => member.UserName === userName);
            if (index !== -1) {
                let isAuto = this.lstShowMember[i].lstMember[index].IsAutoAddMemberToWorksGroup;
                if (isAuto === 0) {
                    this.lstShowMember[i].lstMember[index].IsAutoAddMemberToWorksGroup = 1;
                } else {
                    this.lstShowMember[i].lstMember[index].IsAutoAddMemberToWorksGroup = 0;
                }
                break;
            }
        }
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: remove member in work group member list
     */
    private removeMemberInMemberList(userName: any) {
        let $ = window.jQuery;
        if (!(this.IsCanEditProject || this.isAdmin)) {
            return false;
        }
        let self: WorksGroups = this;
        projectService.checkUserProcess(userName, this.WorksSupportProjectId).then(function (item: any) {
            if (item.NumberOfMember > 0) {
                self.ShowProjectTab = false;
                self.IsRemoveMember = true;
                $("#modalErorr").modal("toggle");
            } else {
                for (let i = 0; i < self.lstShowMember.length; i++) {
                    let lstMember: Array<any> = self.lstShowMember[i].lstMember;
                    let member = lstMember.find(member => member.UserName === userName);
                    if (member !== undefined) {
                        self.lstShowMember[i].lstMember.removeItem(member);
                        self.lstShowMember[i].count--;
                        self.numberMemberOfProject--;
                        self.lstShowAllMember.removeItem(member);
                        break;
                    }
                }
            }
        }).error(function (error: any) {
            self.ShowProjectTab = false;
            self.IsRemoveMember = true;
            $("#modalErorr").modal("toggle");
        });
        this.findMemberProject();
        this.setDefaultMemberList();
    }

    /**
      * Writen by: Nguyen van Huan
      * Create by: 10.08.2017
      * Description:show search member popup 
      */
    private showPopupSearchMember(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.lstSearchMember = [];
        this.lstMember = [];
        this.WorksSupportDepartmentId = -1;
        this.txtKeyNameUser = String.empty;
        this.SearchMemberPopup = false;
        // Load danh sách phòng ban
        projectService.getAllDepartment().then(function (items: Array<any>) {
            self.lstDepartment = items;
        });
        $('.custom-checkbox').prop("checked", false);
    }
    /**
    * Writen by: Nguyen van Huan
    * Create by: 10.08.2017
    * Description:find all member by department id
    * @param event 
    */
    private SearchUserByKey(event: any) {
        let $ = window.jQuery;
        let nameUser = this.txtKeyNameUser;
        let self: WorksGroups = this;
        this.model.DepartmentId = this.WorksSupportDepartmentId;// DepartmentId
        this.model.Keywords = nameUser;
        this.model.WorksSupportTypeId = this.WorksSupportTypeId; //330;
        projectService.getUserBy(this.model).then(function (items: Array<any>) {
            self.lstMember = items;
            self.isCheckAll = false;
            self.filterProjectMember(self.lstMember);
            // checking values find to show message
            if (items.length > 0) {
                self.SearchMemberPopup = false;
            } else {
                self.SearchMemberPopup = true;
            }
        });
    }
    /**
    * Writen by: Nguyen van Huan
    * Create by: 10.08.2017
    * Description: 
     * @param lstMember 
     */
    private filterProjectMember(lstMember: Array<any>) {
        // check lstShowMember is empty
        if (this.lstShowMember.length === 0) {
            return false;
        }
        for (let i = 0; i < this.lstShowMember.length; i++) {
            for (let j = 0; j < this.lstShowMember[i].lstMember.length; j++) {
                let lstMem: Array<any> = this.lstShowMember[i].lstMember;
                let mem = this.lstMember.find(member => member.UserName == lstMem[j].UserName);
                if (mem !== null) {
                    this.lstMember.removeItem(mem);
                }
            }
        }
    }
    /**
    * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description:  select user when click into picture on workgroup member.
     * @param event 
     */
    private SelectUserName(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        if (!this.isCheckAll) {
            this.lstMember.forEach(element => {
                if (element.MemberUserName === null) {
                    element.MemberUserName = -1;
                }
            });
        } else {
            this.lstMember.forEach(element => {
                if (element.MemberUserName === -1) {
                    element.MemberUserName = null;
                }
            });
        }
    }
    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description:  select user when click into picture on workgroup member.
    * @param userName 
    */
    private ClickUser(UserName: any) {
        let $ = window.jQuery;
        let index = this.lstMember.findIndex(member => member.UserName === UserName);
        if (this.lstMember[index].MemberUserName === null) {
            this.lstMember[index].MemberUserName = -1;
        } else {
            this.lstMember[index].MemberUserName = null;
        }
    }
    /**
    * Writen by: Nguyen van Huan
    * Create by: 10.08.2017
    * add new member into member project to show
    * @param event 
    */
    private addNewMemberProject(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.lstMember.forEach(element => {
            if (element.MemberUserName === -1) {
                this.lstSearchMember.push(element);
            }
        });
        this.addSearchMemberIntoMemberList();
        $('#modalProjectMember').modal('hide');
        $("#login").attr("class", "modal-open");
    }
    /**
      * Writen by: Nguyen van Huan
      * Create by: 10.08.2017
      * add member into member list after search.
      */
    private addSearchMemberIntoMemberList() {
        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.lstSearchMember.forEach(searchMember => {
            //1.1 duyệt từng đối tượng va so sánh roleid
            let index = this.lstShowMember.findIndex(showMember => parseInt(showMember.WorksSupportMemberRoleId) === parseInt(searchMember.WorksSupportMemberRoleId));
            // kiểm tra xem roleid đã tồn tại chưa
            if (index !== -1) {
                // thêm phần tử và sắp xếp vào lstShowMember.
                //1.2 lấy phần tử trong lstMember trong lstShowMember theo roleid.
                let lstMember: Array<any> = this.lstShowMember[index].lstMember;
                //1.3 thêm phần thành viên mới vào lstMember.
                lstMember.push(searchMember);
                //1.4 xắp xếp theo thứ tự từ A->Z của trường FullName.
                lstMember.sort(this.compare);
                //1.5 thay lstMember cũ bằng lstMember mới đã được xắp xếp.
                this.lstShowMember[index].lstMember = lstMember;
            } else {
                //1.6 thêm mới đối tượng cho lstShowMember.
                let obj = {};
                obj["WorksSupportMemberRoleId"] = searchMember.WorksSupportMemberRoleId;
                obj["WorksSupportMemberRoleName"] = searchMember.WorksSupportMemberRoleName;
                obj["count"] = 0;
                obj["lstMember"] = [];
                obj["lstMember"].push(searchMember);
                //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                this.lstShowMember.push(obj);
            }
        });
        this.findMemberProject();
        this.setDefaultMemberList();
    }
    /**
    * Writen by: Nguyen van Huan
    * Create by: 10.08.2017
    * add or edit member
    */
    private onAddClicked(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        if (!(this.IsCanEditProject || this.isAdmin)) {
            return false;
        }
        let id = this.WorksSupportProjectId;
        let worksSupportProjectName = this.WorksSupportProjectName;
        let worksSupportTypeId = this.WorksSupportTypeId;
        let description = this.Description;
        let iconUrl = this.IconUrl;
        let isSystem = this.IsSystem;
        let isActived = this.IsActived;
        let isSubmit = true;
        this.ShowRoleProject = "default";
        this.IsRole = true;
        //2. edit project
        if (this.validationPopupProject()) {
            let lstMember = this.getLstMember();
            this.filterProjectMemberListBeforeSubmit();
            let objProject = new UpdateModelProject(id, worksSupportProjectName, worksSupportTypeId, description, isActived, isSystem,
                iconUrl, this.userLogin, this.LstNewMember, this.LstEditMember, this.LstDeleteMember);

            projectService.UpdateProject(objProject).then(function (item: any) {
                $('#modalUpdatePrpoject').modal('hide');
                $('.modal-backdrop').modal('hide');
                let projectId = self.WorksSupportProjectId;
                if (!isActived) {
                    let project = self.lstProject.find(project => project.WorksSupportProjectId === projectId);
                    self.lstProject.removeItem(project);
                    projectId = self.lstProject[0].WorksSupportProjectId;
                }
                localStorage.setItem("projectId", projectId.toString());
                self.getAllProjectsByUserLogin(projectId);
                // self.loadProjects();
            }).error(function (error: any) {
                self.validateOnServerOfProject(error);
            });
        }
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * check the same project name on server.
     */
    private validateOnServerOfProject(error: any): boolean {
        let self: WorksGroups = this;
        let index: number;
        for (let i = 0; i < error.length; i++) {
            if (error[i].Message === "WorksSupportProjectName") {
                this.ErrorProjectName = error[i].Message;
                return false;
            }
        }
        return true;
    }
    /**
    * Writen by: Nguyen van Huan
    * Create by: 10.08.2017
    * get member list
    */
    private getLstMember(): Array<any> {
        //1. xoa danh sach thanh vien (lstMember).
        this.lstMember = [];
        //  if(this.lstMember.length !==0 &&  this.lstShowMember.length !==0){
        if (this.lstShowMember === null) {
            return [];
        }
        for (let i = 0; i < this.lstShowMember.length; i++) {
            let lstMember: Array<any> = this.lstShowMember[i].lstMember;
            for (let j = 0; j < lstMember.length; j++) {
                this.lstMember.push(lstMember[j]);
            }
        }
        return this.lstMember;
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * load all projects by userLogin
     */
    private loadProjects() {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        $(".show-error").hide();
        projectService.getProject(self.userLogin, "", -1, 500).then(function (items: Array<any>) {
            self.lstProject = items;
            let isAdd = items.findIndex(project => project.IsCanAdd === true);
            if (isAdd !== -1) {
                self.isCanAdd = true;
            }
            if (self.lstProject.length > 0) {
                $(".show-error").hide();
            } else {
                $(".show-error").show();
            }
        });

    }
    /**
      * Writen by: Nguyen van Huan
     * Create by: 10.08.2017 
     * Description: filter member project
     */
    private filterProjectMemberListBeforeSubmit() {
        // lay phan tu da xoa va them lay .        
        this.LstEditMember = [];
        this.LstDeleteMember = [];
        this.LstNewMember = [];
        for (let i = 0; i < this.lstShowMember.length; i++) {
            let lstMember: Array<any> = this.lstShowMember[i].lstMember;
            for (let j = 0; j < lstMember.length; j++) {
                let member = this.LstCurrentMember.find(member => member.UserName === lstMember[j].UserName);
                //1. xoa phan tu neu tim thay. va luu vao danh sach edit
                if (member == undefined) {
                    //2. them neu khong tim thay.
                    this.LstNewMember.push(lstMember[j]);
                } else {
                    this.LstCurrentMember.removeItem(member);
                    this.LstEditMember.push(member);
                }
            }
        }
        this.LstDeleteMember = this.LstCurrentMember;
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017  
     * Description: change member by role
     * @param newRoleId 
     * @param userName 
     * @param oldRoleId 
     * @param newRoleName 
     */
    private changeProjectMemberRole(newRoleId: any, userName: any, oldRoleId: any, newRoleName: any) {
        this.editProjectMemberRole(userName, newRoleId, oldRoleId, newRoleName);
        this.findAddMemberProject();
    }
    /**
    * Writen by: Nguyen van Huan
     * Create by: 10.08.2017  
     * Description: change member role by project
     * @param userName 
     * @param newRoleId 
     * @param oldRoleId 
     * @param newRoleName 
     */
    private editProjectMemberRole(userName: any, newRoleId: any, oldRoleId: any, newRoleName: any) {

        //1. tim index UserName theo oldRoleId
        let index = this.lstShowMember.findIndex(project => project.WorksSupportMemberRoleId === parseInt(oldRoleId));
        //2. kiem tra project nao chua UserName nay.
        if (index !== -1) {
            //3. lấy danh sach lstMember trong lstShowMember theo roleid.
            let lstMember: Array<any> = this.lstShowMember[index].lstMember;
            //4. tim user trong lstMember 
            let member = lstMember.find(mem => mem.UserName === userName);
            //5. remove ra khoai danh sach thanh vien.
            lstMember.removeItem(member);
            //5.1 cap nhat lai danh sach
            this.lstShowMember[index].lstMember = lstMember;
            //6. tim chi so cua vai tro moi trong lstShowMember.
            let roleIndex = this.lstShowMember.findIndex(showMember => showMember.WorksSupportMemberRoleId === parseInt(newRoleId));
            //7. cap nhat lai vao nhom vai tro moi trong lstShowmember.
            if (roleIndex !== -1) {
                let lstNewMember: Array<any> = this.lstShowMember[roleIndex].lstMember;
                //8. thêm phần thành viên mới vào lstMember.
                member.WorksSupportMemberRoleId = parseInt(newRoleId);
                lstNewMember.push(member);
                //9. xắp xếp theo thứ tự từ A->Z của trường FullName.
                lstNewMember.sort(this.compare);
                //10. thay lstMember cũ bằng lstMember mới đã được xắp xếp.
                this.lstShowMember[roleIndex].lstMember = lstNewMember;
            } else {
                //11. thêm mới đối tượng cho lstShowMember.
                let obj = {};
                obj["WorksSupportMemberRoleId"] = parseInt(newRoleId);
                obj["WorksSupportMemberRoleName"] = newRoleName;
                obj["lstMember"] = [];
                obj["count"] = 0;
                //12. cap nhat lai roleid cho thanh vien nay
                member.WorksSupportMemberRoleId = parseInt(newRoleId);
                obj["lstMember"].push(member);
                //13. thêm đối tượng vừa tạo vào lstShowMember.
                this.lstShowMember.push(obj);
            }

        }
    }
    /**
    * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: find member project  
     */
    private findAddMemberProject() {
        let keySearch = this.KeySearchMember;
        this.numberMemberOfProject = 0;
        if (keySearch.trim() === "") {
            for (let i = 0; i < this.lstShowMember.length; i++) {
                let lstMember: Array<any> = this.lstShowMember[i].lstMember;
                let count = 0;
                lstMember.forEach(element => {
                    element.IsChecked = false;
                    count++;
                });
                this.lstShowMember[i].count = count;
                this.numberMemberOfProject += count;
            }
        } else {
            for (let i = 0; i < this.lstShowMember.length; i++) {
                let lstMember: Array<any> = this.lstShowMember[i].lstMember;
                let countMember = lstMember.length;
                let count = 0;
                lstMember.forEach(member => {
                    member.IsChecked = true;
                    let fullName = (member.FullName === null) ? "" : member.FullName;
                    if ((fullName).toLowerCase().search(keySearch.toLowerCase()) >= 0) {
                        member.IsChecked = false;
                        count++;
                    }
                });
                this.lstShowMember[i].count = count;
                this.numberMemberOfProject += count;
            }
        }
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
     * Description: check tab on project popup
     * @param tab 
     */
    private checkTab() {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        if (this.ShowProjectTab) {
            this.ShowProjectTab = false;
        } else {
            this.ShowProjectTab = true;
        }
    }

    /**
     * Writen by  : Nguyen van Huan
     * Create by  : 10.08.2017
     * Description: set default member list
     */
    private setDefaultMemberList() {
        this.lstShowAllMember = [];
        for (let i = 0; i < this.lstShowMember.length; i++) {
            //1. kiem tra thanh vien
            let lstMem: Array<any> = this.lstShowMember[i].lstMember;
            lstMem.forEach(mem => {
                this.lstShowAllMember.push(mem);
            });
        }
        //2. xap xep danh sach thanh vien mac dinh theo ABC
        this.lstShowAllMember.sort(this.compare);
    }
}

