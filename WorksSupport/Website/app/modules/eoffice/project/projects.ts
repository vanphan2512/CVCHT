import { Component, ViewChild } from "angular2/core";
import { Router, RouteParams } from "angular2/router";
import { Http } from "angular2/http";
import { BasePage } from "../../../common/models/ui";
import { ProjectModel } from "./projectModel";
import { EditModel } from "./editModel";
import { UpdateModel } from "./updateModel";
import { AddProjectModel } from "./addProjectModel";
import { FormMode } from "../../../common/enum";
import projectService from "../_share/services/projectService";
import { Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea } from "../../../common/directive";
import { PageAction } from "../../../common/models/ui";
import { DeleteModel } from "./projectModel";
import route from "../_share/config/route";
import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ValidationException } from "../../../common/models/exception";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
import { RoleValue } from "../../../common/enum";
@Component({
    templateUrl: "app/modules/eoffice/project/projects.html",
    directives: [Grid, PageActions, Page, Form, FormTextInput, FormFooter, FormTextArea, ErrorMessage]
})

export class Projects extends BasePage {
    public WorksSupportProjectId: number = 0;
    public WorksSupportProjectName: string = String.empty;
    public WorksSupportTypeId: number = -1;
    public Description: string = String.empty;
    public WorksSupportDepartmentId: number = -1;
    public OrderIndex: number = 0;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public User: string = String.empty;
    public lstWorksType: any = [];
    public lstDepartment: any = [];
    public lstProject: any = [];
    public txtKeyNameUser: any = String.empty;
    public lstMember: Array<any> = [];

    public SelProjectId: number;
    public SelProjectName: string = String.empty;
    public isClickTab: boolean = true;
    public actions: Array<PageAction> = [];
    private router: Router;
    public objDelete: DeleteModel;
    public objUpdate: UpdateModel;
    public selectedValue: Array<any> = [];
    public isCanAdd: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_ADD) === "true" ? true : false;
    public isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_DEL) === "true" ? true : false;
    public userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
    public roleValue: string = RoleValue.Admin;
    public isAdmin: boolean = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    public lstMemberIsAdded: Array<any> = [];
    public lstSearchMember: Array<any> = [];
    public lstShowMember: Array<any> = [];
    public LstDeleteMember: Array<any> = [];
    public LstNewMember: Array<any> = [];
    public LstCurrentMember: Array<any> = [];
    public LstEditMember: Array<any> = [];
    public IsAddPopup: boolean = true;

    constructor(router: Router, routeParams: RouteParams) {
        super();
        let self: Projects = this;
        self.router = router;
        self.model = new ProjectModel(self.i18nHelper);
        self.loadProjects();

    }
    ngOnInit() {
        this.userLogin = cacheService.get(CACHE_CONSTANT.strUserName);
        this.isAdmin = cacheService.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    }
    @ViewChild(Grid)
    private gridComponent: Grid;

    public onAddNewProjectClicked(event: any) {
        this.router.navigate([route.project.addProject.name]);
    }


    private loadProjects() {
        debugger;
        let self: Projects = this;
        let User = cacheService.get(CACHE_CONSTANT.strUserName);
        projectService.getProject(User).then(function (items: Array<any>) {
            console.log(items);
            self.lstProject = items;
        });
    }

    // private loadProjects() {
    //     let self: Projects = this;
    //     projectService.getProject().then(function (items: Array<any>) {
    //         self.model.importProject(items);
    //     });
    // }


    public checkProjectId(id: any, name: any) {
        debugger;
        let $ = window.jQuery;
        let self: Projects = this;
        this.SelProjectId = id;
        this.SelProjectName = name;
        console.log(id, name);
        this.onShowPopupDelete();
    }
    // Delete Priority
    public onShowPopupDelete() {
        let $ = window.jQuery;
        let self: Projects = this;
        $("#ModalDelete").modal('toggle');
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        this.selectedValue.push({ id: this.SelProjectId, name: this.SelProjectName })
        this.objDelete = new DeleteModel(this.selectedValue[0].id.toString(), "administrator");
    }
    // public onShowPopupDelete(event: any) {
    //     let $ = window.jQuery;
    //     let self: Projects = this;
    //     this.selectedValue = new Array();
    //     let lstId: Array<string> = [];
    //     let grid = this.gridComponent.grid.rows(".selected").data();

    //     for (let i = 0; i < grid.length; i++) {
    //         this.selectedValue.push({ id: grid[i]["WorksSupportProjectId"], name: grid[i]["WorksSupportProjectName"] });
    //         lstId.push(grid[i]["WorksSupportProjectId"]);
    //     }
    //     this.objDelete = new DeleteModel(lstId.toString(), "administrator");
    // }

    public onDeleteProjects(event: any) {
        let self: Projects = this;
        let $ = window.jQuery;
        projectService.delete(this.objDelete).then(function (event: any) {
            $("#ModalDelete").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new ProjectModel(self.i18nHelper);
            self.loadProjects();
        });
    }
    public onShowPopupUpdate(event: any) {
        let $ = window.jQuery;
        let self: Projects = this;
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        let grid = this.gridComponent.grid.rows(".selected").data();
        for (let i = 0; i < grid.length; i++) {
            this.selectedValue.push({ id: grid[i]["WorksSupportProjectId"], name: grid[i]["WorksSupportProjectName"], description: grid[i]["Description"], });
            lstId.push(grid[i]["WorksSupportProjectId"]);
        }
        //  this.objUpdate = new UpdateModel(lstId.toString(), "administrator");
    }
    public onUpdateProjects(event: any) {
        let self: Projects = this;
        let $ = window.jQuery;
        projectService.update(this.objUpdate).then(function (event: any) {
            $("#modalUpdate").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new ProjectModel(self.i18nHelper);
            self.loadProjects();
        });
    }
    // Search by IsDelete
    public onStatusChange(event: any) {
        // Get value of dropdown list
        let $ = window.jQuery;
        let currentIsDelete = event.currentTarget.value;
        let projectName = $("#projectName").val();
        let self: Projects = this;
        projectService.getProjecBy(projectName, currentIsDelete, 0, -1).then(function (items: Array<any>) {
            //self.model.importProject(items);
            self.lstProject = items;
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
        debugger;
        let $ = window.jQuery;
        let projectName = $("#projectName").val();
        let self: Projects = this;
        projectService.getProjecBy(projectName, 0, 0, -1).then(function (items: Array<any>) {
            // self.model.importProject(items);
            self.lstProject = items;
            this.filterMemberListAfterSearch(self.lstProject);
        });
    }

    public onSaveClicked(event: any): void {
        let $ = window.jQuery;
        let self: Projects = this;
        projectService.create(this.model).then(function () {
            $("#modalUpdatePrpoject").modal("hide");
            $(".modal-backdrop").modal("hide");
            self.model = new ProjectModel(self.i18nHelper);
            self.loadProjects();
        });
    }
    // validation for add model
    public isValidAdd(): boolean {

        let validationErrors: ValidationException = new ValidationException();
        if (!this.WorksSupportProjectName) {
            validationErrors.add("eoffice.addProject.validation.nameIsRequire");
        }
        if (!this.WorksSupportTypeId) {
            validationErrors.add("eoffice.addProject.validation.nameIsRequire");
        }
        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }
    public isValidEdit(): boolean {
        let validationErrors: ValidationException = new ValidationException();
        if (!this.WorksSupportProjectName) {
            validationErrors.add("eoffice.updateProject.validation.nameIsRequire");
        }
        validationErrors.throwIfHasError();
        return !validationErrors.hasError();
    }
    public onCancelUpdate() {
        let $ = window.jQuery;
        $('#modalUpdate').modal('hide');
        $('.modal-backdrop').modal('hide');

    }
    public onCancelAdd() {
        debugger;
        let $ = window.jQuery;
        $("#modalUpdatePrpoject").modal('hide');

    }
    public resetValidation() {
        let $ = window.jQuery;
        $(".form-control").removeClass("invalid");
        $(".form-control").removeClass("validation");
    }
    private onAddNewProjectClick() {
        this.resetValidation();
        this.IsAddPopup = true;
        let $ = window.jQuery;
        let self: Projects = this;
        self.WorksSupportProjectId = 0;
        self.WorksSupportProjectName = "";
        self.Description = "";
        self.OrderIndex = 0;
        self.IsActived = 0;
        self.IsSystem = 0;
        self.User = "";
        projectService.getAllWorksType().then(function (items: Array<any>) {
            self.lstWorksType = items;
        });
        // Load lai dữ liệu khi mở popup chinh sua truoc
        self.WorksSupportTypeId =-1
        self.lstShowMember =null;
    }
    public objAddProject: any = {};
    public onAddClicked(event: any) {
        debugger;
        let self: Projects = this;
        let $ = window.jQuery;
        let id = this.WorksSupportProjectId;
        let worksSupportProjectName = this.WorksSupportProjectName;
        let worksSupportTypeId = this.WorksSupportTypeId;
        let description = this.Description;
        let iconUrl = this.IconUrl;
        let isSystem = this.IsSystem;
        let isActived = this.IsActived;
        let lstMember = this.getLstMember();
        //1. add project
        if (this.IsAddPopup && this.isValidAdd()) {
            let objProject = new UpdateModel(id, worksSupportProjectName, worksSupportTypeId, description, isActived, isSystem,
                iconUrl, this.userLogin, lstMember, [], []);

            projectService.create(objProject).then(function (item: any) {
                $('#modalUpdatePrpoject').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new ProjectModel(self.i18nHelper);
                self.objAddProject = item;
                self.loadProjects();
            })
        }
        //2. edit project
        if (!this.IsAddPopup && this.isValidAdd()) {
            this.filterMemberListBeforeSubmit();
            let objProject = new UpdateModel(id, worksSupportProjectName, worksSupportTypeId, description, isActived, isSystem,
                iconUrl, this.userLogin, this.LstNewMember, this.LstEditMember, this.LstDeleteMember);

            projectService.UpdateProject(objProject).then(function (item: any) {
                $('#modalUpdatePrpoject').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new ProjectModel(self.i18nHelper);
                self.objAddProject = item;
                self.loadProjects();
            })
        }
    }
    private onEditProjectClicked(event: any) {
        this.resetValidation();
        let self: Projects = this;
        let $ = window.jQuery;
        projectService.getAllWorksType().then(function (items: Array<any>) {
            self.lstWorksType = items;

        });
        projectService.get(event).then(function (item: any) {
            self.WorksSupportProjectId = item.WorksSupportProjectId;
            self.WorksSupportProjectName = item.WorksSupportProjectName;
            self.WorksSupportTypeId = item.WorksSupportTypeId;
            self.Description = item.Description;
            self.OrderIndex = item.OrderIndex;
            self.IsActived = item.IsActived;
            self.IsSystem = item.IsSystem;
            self.User = item.User;
        })

        $('#modalUpdate').modal('toggle');
    }
    public onUpdateClicked(event: any) {
        let self: Projects = this;
        let $ = window.jQuery;
        let WorksSupportProjectId = this.WorksSupportProjectId;
        let WorksSupportProjectName = this.WorksSupportProjectName;
        let WorksSupportTypeId = this.WorksSupportTypeId;
        let description = this.Description;
        let orderIndex = this.OrderIndex;
        let isActived = this.IsActived;
        let isSystem = this.IsSystem;
        let iconUrl = this.IconUrl;
        let user = cacheService.get(CACHE_CONSTANT.strUserName);
        let editModel = new EditModel(WorksSupportProjectId, WorksSupportProjectName, WorksSupportTypeId, orderIndex, description, isActived, isSystem, iconUrl, user);
        if (this.isValidEdit()) {
            projectService.update(editModel).then(function () {
                $('#modalUpdate').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new ProjectModel(self.i18nHelper);
                self.loadProjects();
            });
        }
    }

    public onAddNewMemberManage() {
        debugger;
        let self: Projects = this;
        let $ = window.jQuery;
        projectService.getAllDepartment().then(function (items: Array<any>) {
            self.lstDepartment = items;
        });
        // // $('#modalMember').modal('toggle');F
        // $('#modalAdd').modal('hide');
        // //  $('.modal-backdrop').modal('hide');    
        // $('#modalMember').modal('toggle');

    }
    public SearchUserByKey(event: any) {
        let $ = window.jQuery;
        let nameUser = this.txtKeyNameUser;
        let self: Projects = this;
        this.model.DepartmentId = this.WorksSupportDepartmentId;// DepartmentId
        this.model.Keywords = nameUser;
        this.model.WorksSupportTypeId = this.WorksSupportTypeId; //330;
        projectService.getUserBy(this.model).then(function (items: Array<any>) {
            self.lstMember = items;
            self.isCheckAll = false;
            self.filterMemberListAfterSearch(self.lstMember);
        });
    }
    public checkAllProject(event: any) {
        let $ = window.jQuery;
    }
    // public insertMember(){
    //     let lstObject = [{"WorksSupportProjectId":"123","MemberUserName":"abc"},{"WorksSupportProjectId":"321","MemberUserName":"lala"}];

    //     projectService.insertMember(lstObject).then(function(item: any){

    //     });
    // }
    // select icon on add model
    public IconUrl: string = String.empty;
    public onIconSelectedAdd() {
        let self: Projects = this;
        let $ = window.jQuery;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }
    // chọn User

    public ClickUser(UserName: any) {
        let $ = window.jQuery;
        let index = this.lstMember.findIndex(member => member.UserName === UserName);
        if (this.lstMember[index].MemberUserName === null) {
            this.lstMember[index].MemberUserName = -1;
        } else {
            this.lstMember[index].MemberUserName = null;
        }
    }
    public ClickUserAuto(userName: any) {

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

    public addNewMemberProject(event: any) {
        let self: Projects = this;
        let $ = window.jQuery;
        this.lstMember.forEach(element => {
            if (element.MemberUserName === -1) {
                this.lstSearchMember.push(element);
            }
        });
        this.addSearchMemberIntoMemberList();
        $('#modalMember').modal('hide');
        $("#login").attr("class", "modal-open");

    }
    public isCheckAll: boolean = false;
    public SelectUserName(event: any) {
        let self: Projects = this;
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
    public removeMember(userName: any) {
        debugger;
        let self: Projects = this;
        let $ = window.jQuery;
        for (let i = 0; i < this.lstMemberIsAdded.length; i++) {
            if (this.lstMemberIsAdded[i].UserName === userName) {
                this.lstMemberIsAdded.splice(i, 1);
                break;
            }
        }
    }
    // Them thanh vien du an
    public WorksSupportMemberRoleId: number = 0;
    public onAddMember(event: any) {
        let self: Projects = this;
        let $ = window.jQuery;
        let id = null;
        let worksSupportProjectName = this.model.WorksSupportProjectName;
        let worksSupportTypeId = this.WorksSupportTypeId;
        let description = this.Description;
        let iconUrl = this.IconUrl;
        let isSystem = this.IsSystem;
        let isActived = this.IsActived;

        //1. add popup
        if (this.IsAddPopup) {
            let lstMember = this.getLstMember();
            let objProject = new UpdateModel(id, worksSupportProjectName, worksSupportTypeId, description, isActived, isSystem,
                iconUrl, this.userLogin, lstMember, [], []);
            projectService.insertMember(objProject).then(function () {
                $('#modalUpdatePrpoject').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new ProjectModel(self.i18nHelper);
                self.loadProjects();
            });

        } else { //2. edit popup

        }

    }
    public insertMember() {
        //  let lstObject = [{"WorksSupportProjectId":"123","MemberUserName":"abc"},{"WorksSupportProjectId":"321","MemberUserName":"lala"}];

        projectService.insertMember(this.lstMemberIsAdded).then(function (item: any) {

        });
    }
    private addNewPropertiesOfMember() {
        for (let i = 0; i < this.lstMemberIsAdded.length; i++) {
            this.lstMemberIsAdded[i]["ProjectId"] = "123";
        }
    }

    public onCancelAddMember() {
        debugger;
        let $ = window.jQuery;
        $('#modalMember').modal('hide');
        $("#login").attr("class", "modal-open");
    }
    public IsRole: boolean = false;

    public selectRole(selRole: any) {
        if (selRole === "2") {
            this.IsRole = false;
        } else {
            this.IsRole = true;

        }
    }

    // chức năng lọc thành viên đã tồn tại trong lstShowMember
    private filterMemberListAfterSearch(lstMember: Array<any>) {
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
    //chức năng thêm thành viên mới vào lstShowMember để hiện thị danh sách.
    private addSearchMemberIntoMemberList() {
        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.lstSearchMember.forEach(searchMember => {
            //1.1 duyệt từng đối tượng va so sánh roleid
            let index = this.lstShowMember.findIndex(showMember => showMember.WorksSupportMemberRoleId === searchMember.WorksSupportMemberRoleId);
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
                obj["lstMember"].push(searchMember);
                //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                this.lstShowMember.push(obj);
            }
        });

    }
    //chức năng thêm thành viên mới vào lstShowMember để hiện thị danh sách.
    private addEditMemberIntoMemberList() {
        //1. kiểm tra roleid thuộc roleid nào trong lstShowMember.
        this.LstCurrentMember.forEach(searchMember => {
            //1.1 duyệt từng đối tượng va so sánh roleid
            let index = this.lstShowMember.findIndex(showMember => showMember.WorksSupportMemberRoleId === searchMember.WorksSupportMemberRoleId);
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
                obj["lstMember"].push(searchMember);
                //1.7 thêm đối tượng vừa tạo vào lstShowMember.
                this.lstShowMember.push(obj);
            }
        });

    }
    //chức năng xóa thành viên lstShowMember.
    private removeMemberInMemberList(userName: any) {
        for (let i = 0; i < this.lstShowMember.length; i++) {
            let lstMember: Array<any> = this.lstShowMember[i].lstMember;
            let member = lstMember.find(member => member.UserName === userName);
            if (member !== null) {
                this.lstShowMember[i].lstMember.removeItem(member);
                break;
            }
        }
    }
    public showPopupSearchMember(event: any) {
        let self: Projects = this;
        this.lstSearchMember = [];
        this.lstMember = [];
        projectService.getAllDepartment().then(function (items: Array<any>) {
            self.lstDepartment = items;
        });
    }
    private getLstMember(): Array<any> {
        //1. xoa danh sach thanh vien (lstMember).
        this.lstMember = [];
        for (let i = 0; i < this.lstShowMember.length; i++) {
            let lstMember: Array<any> = this.lstShowMember[i].lstMember;
            for (let j = 0; j < lstMember.length; j++) {
                this.lstMember.push(lstMember[j]);
            }
        }
        return this.lstMember;
    }
    private compare(obj1: any, obj2: any) {
        if (obj1.FullName < obj2.FullName)
            return -1;
        if (obj1.FullName > obj2.FullName)
            return 1;
        return 0;
    }
    // show popup update project
    private onUpdateProjectClick(id: any) {
        debugger;
        let $ = window.jQuery;
        this.IsAddPopup = false;
        $("#modalUpdatePrpoject").modal('toggle');
        this.resetValidation();
        let self: Projects = this;
        this.lstShowMember = [];
        projectService.get(id).then(function (item: any) {
            self.WorksSupportProjectId = item.WorksSupportProjectId;
            self.WorksSupportProjectName = item.WorksSupportProjectName;
            self.WorksSupportTypeId = item.WorksSupportTypeId;
            self.Description = item.Description;
            self.IconUrl = item.IconUrl;
            self.IsSystem = item.IsSystem;
            self.IsActived = item.IsActived;
        });
        projectService.getAllWorksType().then(function (items: Array<any>) {
            self.lstWorksType = items;
        });
    }
    //  public onShowPopupDelete() {
    //     let $ = window.jQuery;
    //     let self: Projects = this;
    //     $("#ModalDelete").modal('toggle');
    //     this.selectedValue = new Array();
    //     let lstId: Array<string> = [];
    //     this.selectedValue.push({ id: this.SelProjectId, name: this.SelProjectName })
    //     this.objDelete = new DeleteModel(this.selectedValue[0].id.toString(), "administrator");
    // }
    // thay doi button luu khi chon tab moi
    public lstMemberRole: Array<any>= [];
    public checkTab(tab: any) {
        debugger;
        let $ = window.jQuery;
        let self: Projects = this;
        let validationErrors: ValidationException = new ValidationException();
        this.isClickTab = (tab === "project") ? true : false;
        this.lstShowMember = [];
        
        if (this.WorksSupportTypeId === -1) {
            // alert(this.WorksSupportTypeId);
            //  $("#clicktab").attr("href","#tabAddProject");
             $("#modalErorr").modal('toggle');
            if (!this.WorksSupportProjectName) {
                validationErrors.add("Error", {}, "Chưa chọn loại công việc!");
                validationErrors.throwIfHasError();
                return !validationErrors.hasError();
                
            }
            
        }
        if (!this.isClickTab && !this.IsAddPopup) {

            if (this.WorksSupportTypeId === -1) {
                alert("loi");
            }
            let self: Projects = this;
            projectService.getProjectMember(this.WorksSupportProjectId).then(function (items: Array<any>) {
                debugger;
                console.log(items);
                self.LstCurrentMember = items;
                self.addEditMemberIntoMemberList()
            });
        }
        // Load danh sách vai tro cua thành viên
        projectService.getMemberRoleByTypeId(this.WorksSupportTypeId).then(function (items: Array<any>) {
            self.lstMemberRole = items;
        });
    }
    // lay nhung thanh vien da xoa
    private filterMemberListBeforeSubmit() {
        // this.LstCurrentMember.forEach(currentMember => {
        //     //1. tim theo roleid
        //     let lstMem: Array<any> = this.lstShowMember.find(project => project.WorksSupportMemberRoleId === currentMember.WorksSupportMemberRoleId).lstMember;
        //     if (lstMem !== null) {
        //         let index = lstMem.findIndex(member => member.UserName === currentMember.UserName);
        //         if (index === -1) {
        //             this.LstDeleteMember.push(currentMember);
        //         }
        //     } else {
        //         this.LstDeleteMember.push(currentMember);
        //     }
        // });
        // lay phan tu da xoa va them lay .
        this.LstEditMember = [];
        this.LstDeleteMember = [];
        this.LstNewMember = [];
        console.log(this.lstShowMember);
        for (let i = 0; i < this.lstShowMember.length; i++) {

            let lstMember: Array<any> = this.lstShowMember[i].lstMember;
            console.log(lstMember);
            for (let j = 0; j < lstMember.length; j++) {
                console.log(lstMember[j].UserName);
                let member = this.LstCurrentMember.find(member => member.UserName === lstMember[j].UserName);
                console.log(member);
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

}
