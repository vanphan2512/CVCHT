// Created by:   Nguyen Van Huan
// Created date: 16/06/2017. 
import { Component, ViewChild, OnInit } from "angular2/core";
import { Router } from "angular2/router";
import { Http } from "angular2/http";
import { BasePage } from "../../../common/models/ui";
import { Grid, PageActions, Page } from "../../../common/directive";
import { Form, FormTextInput, FormFooter, FormTextArea } from "../../../common/directive";
import { FormMode } from "../../../common/enum";
import { PageAction } from "../../../common/models/ui";
import { WorksGroupsModel } from "./worksGroupsModel";
import { DeleteModel } from "./deleteModel";
import { UpdateModel } from "./updateModel";
import worksGroupService from "../_share/services/WorksGroupService";
import route from "../_share/config/route";
import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { KeyNamePair } from "../../../common/models/keyNamePair";
import { ErrorMessage } from "../../../common/directives/common/errorMessage";

@Component({
    templateUrl: "app/modules/eoffice/worksgroup/worksgroups.html",
    directives: [Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, ErrorMessage]
})

export class WorksGroups extends BasePage implements OnInit {
    public Id: number = 0;
    public WorksSupportGroupName: string = String.empty;
    public WorksSupportProjectName: string = String.empty;
    public WorksSupportProjectId: number = 0;
    public Description: string = String.empty;
    public IsSystem: number = 0;
    public IsActived: boolean = true;
    public WorksSupportGroupId: number = 0;
    public Actions: Array<PageAction> = [];
    private Router: Router;
    public selectedValue: Array<any> = [];
    public isCanAdd: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD) === "true" ? true : false;
    public isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    public userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
    public User: string = String.empty;
    public lstProject: any = [];
    public lstRole: any = [];
    public lstGroupMembers: any = [];
    public lstWorksGroup: any = [];
    public LstProjectMember: Array<any> = [];
    public LstSearchProjectMember: Array<any> = [];
    public LstShowMemberDefault: Array<any> = [];
    public LstShowMemberRole: Array<any> = [];
    public LstNewMember: Array<any> = [];
    public LstDeleteMember: Array<any> = [];
    public LstEditMember: Array<any> = [];
    public IconUrl: string = String.empty;
    public CurrentProjectId: number;
    public IsSourceMember: boolean = false;
    public IsNormalceMember: boolean = false;
    public SelWorksGroupId: number;
    public SelWorksGroupName: string = String.empty;
    public objDelete: DeleteModel;
    private IsAddPopup: boolean = true;
    private ShowIsDefault: boolean = false;
    private IsCheckAll: boolean = false;
    constructor(router: Router) {
        super();
        let self: WorksGroups = this;
        let $ = window.jQuery;
        self.Router = router;
        self.model = new WorksGroupsModel(self.i18nHelper);
        self.getAllProjectsByUserLogin();
    }
    @ViewChild(Grid)
    @ViewChild(ErrorMessage)
    private gridComponent: Grid;
    private errorMess: ErrorMessage;
    // show popup edit
    public onShowPopupEdit(event: any) {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        let lstId: Array<string> = [];
        this.selectedValue = new Array();
        var gridEdit = this.gridComponent.grid.rows('.selected').data();
        if (gridEdit.length > 0) {
            this.WorksSupportGroupName = gridEdit[0]["WorksSupportGroupName"];
            this.WorksSupportProjectId = gridEdit[0]["WorksSupportProjectId"];
            this.Description = gridEdit[0]["Description"];
            this.IsSystem = gridEdit[0]["IsActived"];
            this.IsActived = gridEdit[0]["IsSystem"];
            this.WorksSupportGroupId = gridEdit[0]["WorksSupportGroupId"];
        } else {
            this.WorksSupportGroupName = String.empty;
            this.WorksSupportProjectId = 0;
            this.Description = "";
            this.IsSystem = 0;
            this.IsActived = false;
        }

    }

    //add works group
    public onEditWorksGroup(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        let id = this.WorksSupportGroupId;
        let projectId = this.WorksSupportProjectId;
        let groupName = this.WorksSupportGroupName;
        let description = this.Description;
        let isActive = this.IsActived;
        let IsSystem = this.IsSystem;
        // let updateModel = new UpdateModel(id, projectId, groupName, description, 0, IsSystem);
        // worksGroupService.edit(updateModel).then(function () {
        //     $('#modalUpdate').modal('hide');
        //     $('.modal-backdrop').modal('hide');
        //     //self.model = new WorksGroupsModel(self.i18nHelper);
        //     self.loadWorksGroup();
        // })
    }


    public getAllProjectsByUserLogin() {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        worksGroupService.getAllProjectByMemberId(this.userLogin).then(function (items: Array<any>) {
            self.lstProject = items;
        });
    }

    public getWorkGroupByProjectId(projectId: any, projectName: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        worksGroupService.getWorksGroups(projectId).then(function (items: Array<any>) {
            self.lstWorksGroup = items;
        });
        this.CurrentProjectId = projectId;
        this.WorksSupportProjectName = projectName;
    }

    public getAllMemberByProjectId() {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.IsSourceMember = false;
        this.IsNormalceMember = false;
        worksGroupService.getAllMemberByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstProjectMember = items;
            console.log(items);
            let indexSource = items.findIndex(element => element.IsAutoAddMemberWorksGroup === 1);
            let indexNormal = items.findIndex(element => element.IsAutoAddMemberWorksGroup === 0);
            if (indexSource !== -1) {
                self.IsSourceMember = true;
            }
            if (indexNormal !== 0) {
                self.IsNormalceMember = true;
            }
        });
    }

    //add works group
    public onAddWorksGroup(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.model.User = cacheService.get(CACHE_CONSTANT.strUserName);
        worksGroupService.create(this.model).then(function () {
            $('#modalAdd').modal('hide');
            $('.modal-backdrop').modal('hide');
            //self.model = new WorksGroupsModel(self.i18nHelper);

        })
    }

    // delete group members
    public deleteGroupMember(event: any) {
        //deleteGroupMember
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.model.UserId = event;
        this.model.User = cacheService.get(CACHE_CONSTANT.strUserName);
        this.model.GroupId = 121;
        worksGroupService.deleteGroupMember(this.model).then(function (items: Array<any>) {
            console.log(items);
            self.lstGroupMembers = items;

        });

    }

    public onIconIsSelected() {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }

    // Xóa nhóm công việc theo WorksSupportGroupId
    public ShowPopupDelete(id: any, name: any) {
        let $ = window.jQuery;
        let self: WorksGroups = this;
        $("#ModalRemove").modal('toggle');
        this.SelWorksGroupId = id;
        this.SelWorksGroupName = name;
        this.selectedValue = new Array();
        this.selectedValue.push({ id, name });
        this.objDelete = new DeleteModel(this.selectedValue[0].id, this.userLogin);

    }
    public removeWorkGroupById(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        worksGroupService.delete(this.SelWorksGroupId, this.userLogin).then(function (event: any) {
            $("#ModalRemove").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new WorksGroupsModel(self.i18nHelper);
        });
        worksGroupService.getWorksGroups(this.CurrentProjectId).then(function (items: Array<any>) {
            self.lstWorksGroup = items;
        });
    }

    // 23.7.2017 show popup add
    public showPopupAdd(event: any) {
        let $ = window.jQuery;
        this.IsAddPopup = true;
        // get all members in project
        this.getAllMemberByProjectId();
        // get members have IsAutoAddMemberToWorksGroup == 1; push into lstShowMemberDefault and LstShowMemberRole
        this.LstShowMemberRole = [];
        this.LstShowMemberDefault = [];
        this.setMembersIsRole();
        this.setMembersIsDefault();

    }
    private setMembersIsDefault() {
        this.LstShowMemberDefault = [{ "Title": "Thành viên nguồn dự án", "LstMember": [] },
        { "Title": "Thành viên cơ bản", "LstMember": [] }];
        this.LstProjectMember.forEach(projectMember => {
            if (projectMember.IsAutoAddMemberToWorksGroup === 1) {
                this.LstShowMemberDefault[0].LstMember.push(projectMember);
            }
        });
        let lstMember: Array<any> = this.LstShowMemberDefault[0].LstMember;
        lstMember.sort(this.compare);
        this.LstShowMemberDefault[0].LstMember = lstMember;
    }
    private setSearchMembersIsDefault() {
        this.LstSearchProjectMember.forEach(projectMember => {
            if (projectMember.IsAutoAddMemberToWorksGroup === 1) {
                this.LstShowMemberDefault[0].LstMember.push(projectMember);
            } else {
                this.LstShowMemberDefault[1].LstMember.push(projectMember);
            }
        });
        let lstMember: Array<any> = this.LstShowMemberDefault[0].LstMember;
        lstMember.sort(this.compare);
        this.LstShowMemberDefault[0].LstMember = lstMember;
    }
    private setMembersIsRole() {
        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.LstProjectMember.forEach(projectMember => {
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
            } else {
                //1.6 thêm mới đối tượng cho lstShowMember.
                let obj = {};
                obj["WorksSupportMemberRoleId"] = projectMember.WorksSupportMemberRoleId;
                obj["WorksSupportMemberRoleName"] = projectMember.WorksSupportMemberRoleName;
                obj["LstMember"] = [];
                obj["LstMember"].push(projectMember);
                //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                this.LstShowMemberRole.push(obj);
            }
        });
    }
    private setSearchMembersIsRole() {
        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.LstSearchProjectMember.forEach(projectMember => {
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
            } else {
                //1.6 thêm mới đối tượng cho lstShowMember.
                let obj = {};
                obj["WorksSupportMemberRoleId"] = projectMember.WorksSupportMemberRoleId;
                obj["WorksSupportMemberRoleName"] = projectMember.WorksSupportMemberRoleName;
                obj["LstMember"] = [];
                obj["LstMember"].push(projectMember);
                //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                this.LstShowMemberRole.push(obj);
            }
        });
    }
    private compare(obj1: any, obj2: any) {
        if (obj1.FullName < obj2.FullName)
            return -1;
        if (obj1.FullName > obj2.FullName)
            return 1;
        return 0;
    }
    public SearchUserByKey(event: any) {
        let $ = window.jQuery;
        // let nameUser = this.txtKeyNameUser;
        // let self: WorksGroups = this;
        // this.model.DepartmentId = this.WorksSupportDepartmentId;// DepartmentId
        // this.model.Keywords = nameUser;
        // this.model.WorksSupportTypeId = this.WorksSupportTypeId; //330;
        // projectService.getUserBy(this.model).then(function (items: Array<any>) {
        //     self.lstMember = items;
        //     self.isCheckAll = false;
        //     self.filterMemberListAfterSearch(self.lstMember);
        // });
    }

    // 23.7.2017 show popup search user
    public showPopupSearchUser() {
        //1. load all project members.
        let self: WorksGroups = this;
        let $ = window.jQuery;
        this.IsSourceMember = false;
        this.IsNormalceMember = false;
        worksGroupService.getAllMemberByProjectId(this.CurrentProjectId).then(function (items: Array<any>) {
            self.LstSearchProjectMember = items;
            self.filterMemberListAfterSearch();
        });

    }

    public onClickMemberTab() {
        // check popup is add or edit popup

        if (this.IsAddPopup && this.ShowIsDefault) {

            this.ShowIsDefault = false;
        } else {
            this.ShowIsDefault = true;
        }


    }
    public selectRole(selRole: any) {
        if (selRole === "2") {
            this.ShowIsDefault = false;
        } else {
            this.ShowIsDefault = true;

        }
    }
    public ClickUserAuto(userName: any) {

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
    }
    private removeMemberInMemberList(userName: any) {
        // remove is role
        for (let i = 0; i < this.LstShowMemberRole.length; i++) {
            let lstMember: Array<any> = this.LstShowMemberRole[i].LstMember;
            let member = lstMember.find(member => member.UserName === userName);
            if (member !== null) {
                this.LstShowMemberRole[i].LstMember.removeItem(member);
                break;
            }
        }

        // remove is default;
        for (let i = 0; i < this.LstShowMemberDefault.length; i++) {
            let lstMember: Array<any> = this.LstShowMemberDefault[i].LstMember;
            let member = lstMember.find(member => member.UserName === userName);
            if (member !== null) {
                this.LstShowMemberDefault[i].LstMember.removeItem(member);
                break;
            }
        }
    }
    // chức năng lọc thành viên đã tồn tại trong lstShowMember
    private filterMemberListAfterSearch() {
        // check lstShowMember is empty
        for (let i = 0; i < this.LstShowMemberRole.length; i++) {
            for (let j = 0; j < this.LstShowMemberRole[i].LstMember.length; j++) {
                let lstMem: Array<any> = this.LstShowMemberRole[i].LstMember;
                let mem = this.LstSearchProjectMember.find(member => member.UserName == lstMem[j].UserName);
                if (mem !== null) {
                    this.LstSearchProjectMember.removeItem(mem);
                }
            }
        }
    }
    public clickUser(UserName: any) {
        let $ = window.jQuery;
        let index = this.LstSearchProjectMember.findIndex(member => member.UserName === UserName);
        if (this.LstSearchProjectMember[index].MemberUserName === null) {
            this.LstSearchProjectMember[index].MemberUserName = -1;
        } else {
            this.LstSearchProjectMember[index].MemberUserName = null;
        }
    }
    public selectUserName(event: any) {
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

    public addNewMemberWorkGroup(event: any) {
        let $ = window.jQuery;
        // this.LstSearchProjectMember.forEach(element => {
        //     if (element.MemberUserName === -1) {
        //         this.LstSearchProjectMember.push(element);
        //     }
        // });
        this.setSearchMembersIsRole();
        this.setSearchMembersIsDefault();
        $('#modalMember').modal('hide');
        $("#login").attr("class", "modal-open");

    }

    public onAddClicked(event: any) {
        let self: WorksGroups = this;
        let $ = window.jQuery;
        let id = this.WorksSupportGroupId;
        let worksSupportGroupName = this.WorksSupportGroupName;
        let worksSupportProjectId = this.CurrentProjectId;
        let description = this.Description;
        let isActive = 1;
        let isSystem = 1;
        let iconUrl = this.IconUrl;
        let User = this.userLogin;
        let lstNewMember = this.getLstNewMember();
        //1. add project
        // if (this.IsAddPopup && this.isValidAdd()) {
        let objWorksGroup = new UpdateModel(id, worksSupportProjectId, worksSupportGroupName, description, isActive, isSystem,
            iconUrl, lstNewMember, [], []);
        worksGroupService.create(objWorksGroup).then(function (item: any) {
            $('#modalUpdatePrpoject').modal('hide');
            $('.modal-backdrop').modal('hide');
            // self.model = new WorksGroups(self.i18nHelper);
            //self.objAddProject = item;
            self.getAllProjectsByUserLogin();

        });
    }

    private getLstNewMember(): Array<any> {
        //1. xoa danh sach thanh vien (lstMember).
        this.LstNewMember = [];
        for (let i = 0; i < this.LstShowMemberRole.length; i++) {
            let lstMember: Array<any> = this.LstShowMemberRole[i].lstMember;
            for (let j = 0; j < lstMember.length; j++) {
                this.LstNewMember.push(lstMember[j]);
            }
        }
        return this.LstNewMember;
    }


}

