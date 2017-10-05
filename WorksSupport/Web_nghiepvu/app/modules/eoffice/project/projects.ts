/**
 * Writen by    : Nguyen van Huan
 * Create Date  : 10/08/2017
 * Description  : project screen.
 */
import { Component, ViewChild } from "angular2/core";
import { Router, RouteParams } from "angular2/router";
import { Http } from "angular2/http";
import { BasePage } from "../../../common/models/ui";
import { ProjectModel } from "./projectModel";
import { EditModel } from "./editModel";
import { UpdateModel } from "./updateModel";
import { AddProjectModel } from "./addProjectModel";
import projectService from "../_share/services/projectService";
import { FormTextArea } from "../../../common/directive";
import { DeleteModel } from "./projectModel";
import route from "../_share/config/route";
import sessionStorage from "../../../common/storages/sessionStorage";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import { ValidationException } from "../../../common/models/exception";
import { RoleValue } from "../../../common/enum";
@Component({
    templateUrl: "app/modules/eoffice/project/projects.html",
    directives: [FormTextArea]
})

export class Projects extends BasePage {
    private KeySearchMember: any = "";
    public WorksSupportProjectId: number = 0;
    public WorksSupportProjectName: string = String.empty;
    public WorksSupportTypeId: any = "";
    public CurrentWorksSupportTypeId: any;
    public Description: string = String.empty;
    public WorksSupportDepartmentId: number = -1;
    public OrderIndex: number = 0;
    public IsActived: number = 1;
    public IsSystem: number = 0;
    public User: string = String.empty;
    public lstAllWorksType: Array<any> = [];
    public lstWorksType: Array<any> = [];
    public lstDepartment: any = [];
    public lstProject: Array<any> = [];
    public txtKeyNameUser: any = String.empty;
    public lstMember: Array<any> = [];

    public SelProjectId: number;
    public SelProjectName: string = String.empty;
    public isClickTab: boolean = true;
    private router: Router;
    public objDelete: DeleteModel;
    public objUpdate: UpdateModel;
    public selectedValue: Array<any> = [];
    public isCanAdd: boolean = false;
    public isCanDelete: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_DELETE) === "true" ? true : false;
    public isCanEdit: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_EDIT) === "true" ? true : false;
    public roleValue: string = RoleValue.Admin;
    public isAdmin: boolean = sessionStorage.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    public userLogin = this.isAdmin === true ? "administrator" : sessionStorage.get(CACHE_CONSTANT.strUserName);
    public lstMemberIsAdded: Array<any> = [];
    public lstSearchMember: Array<any> = [];
    public lstShowMember: Array<any> = [];
    public lstShowAllMember: Array<any> = [];
    public lstShowMemberIsSearch: Array<any> = [];
    public LstDeleteMember: Array<any> = [];
    public LstNewMember: Array<any> = [];
    public LstCurrentMember: Array<any> = [];
    public LstEditMember: Array<any> = [];
    public IsAddPopup: boolean = true;
    private IsErrorInfor: boolean = false;
    private SearchMemberPopup: boolean = false;
    private SearchMemberInfor: boolean = false;
    public NumMemberOfProject: number = 0;
    public ErrorProjectName: string = String.empty;
    public ErrorDescription: string = String.empty;
    public ErrorWorksSupportTypeId: string = String.empty;
    public IsDefaultRole: number = -1;
    public lstMemberRole: Array<any> = [];
    public countTabClick: number = 0;
    private ShowProjectTab: boolean = true;
    private IsRemoveMember: boolean = false;
    public IsRole: boolean = true;
    private ShowRoleProject = "default";
    public isCheckAll: boolean = false;
    public isCanAdds: boolean = false;
    public isCanDeletes: boolean = false;
    public isCanEdits: boolean = false;
    public IconUrl: string = String.empty;
    public WorksSupportMemberRoleId: number = 0;
    public worksSupportTypeId: string = "";
    public numberMemberOfProject: number = 0;
    public checkIconUrl: boolean = false;
    private LstShowRoleProject: Array<any> = [{ "roleId": "default", "roleName": "Tất cả" }, { "roleId": "role", "roleName": "Vai trò" }]
    constructor(router: Router, routeParams: RouteParams) {
        super();
        let self: Projects = this;
        self.router = router;
        self.model = new ProjectModel(self.i18nHelper);
        self.loadProjects();
        self.getWorkType();
    }
    ngOnInit() {
        this.userLogin = sessionStorage.get(CACHE_CONSTANT.strUserName);
        this.isAdmin = sessionStorage.get(CACHE_CONSTANT.strUserName) === this.roleValue ? true : false;
    }

    public onAddNewProjectClicked(event: any) {
        this.router.navigate([route.project.addProject.name]);
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * load all projects by userLogin
     */
    private loadProjects() {
        let $ = window.jQuery;
        let self: Projects = this;
        $(".show-error").hide();
        projectService.getProject(this.userLogin, "", -1, 500).then(function (items: Array<any>) {
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
            self.sortProjectNameIsDesc(items);
        });
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * @param id project Id
     * @param name project Name
     */
    public checkProjectId(id: any, name: any) {
        let $ = window.jQuery;
        let self: Projects = this;
        this.SelProjectId = id;
        this.SelProjectName = name;
        this.onShowPopupDelete();
    }
    /**
      * Writen by: Nguyen van Huan
      * Create by: 10.08.2017
      * show popup delete
     */
    public onShowPopupDelete() {
        let $ = window.jQuery;
        let self: Projects = this;
        $("#ModalDelete").modal('toggle');
        this.selectedValue = new Array();
        let lstId: Array<string> = [];
        this.selectedValue.push({ id: this.SelProjectId, name: this.SelProjectName })
        this.objDelete = new DeleteModel(this.selectedValue[0].id.toString(), this.userLogin);
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * click button ok 
     * @param event click
     */
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

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017 
     * search project by project name.
     * @param event 
     */
    public SearchByKey(event: any) {
        let $ = window.jQuery;
        let projectName = $("#projectName").val();
        let self: Projects = this;
        $(".show-error").hide();
        let user = this.userLogin;
        if (self.isAdmin) {
            user = "administrator";
        }
        projectService.getProject(user, projectName, 0, 500).then(function (items: Array<any>) {
            self.lstProject = items;
            if (self.lstProject.length > 0) {
                $(".show-error").hide();
                self.lstProject = items;
            } else {
                $(".show-error").show();
            }
            self.sortProjectNameIsDesc(items);
        });
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017 
     * select icon on add model
     */
    public onIconSelectedAdd() {
        let self: Projects = this;
        let $ = window.jQuery;
        $("#myIconModal li").on("click", function () {
            let icon = $(this).find("i").attr("class");
            self.IconUrl = icon;
            $("#myIconModal").modal("hide");
        });
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017 
     * select or unselect user when click on icon
     */
    public ClickUser(UserName: any) {
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
     * @param userName 
     */
    public removeMember(userName: any) {
        let self: Projects = this;
        let $ = window.jQuery;
        for (let i = 0; i < this.lstMemberIsAdded.length; i++) {
            if (this.lstMemberIsAdded[i].UserName === userName) {
                this.lstMemberIsAdded.splice(i, 1);
                break;
            }
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * default member list
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

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * chức năng lọc thành viên đã tồn tại trong lstShowMember
     * @param lstMember  danh sach thanh vien      
     */
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
            // neu danh sach thanh vien >0 thi day vao lstShowAllMember
            if (this.lstShowMember[i].lstMember.length > 0) {
                this.lstShowAllMember.push(this.lstShowMember[i].lstMember);
            }
        }
        // danh sach mac dinh
        this.setDefaultMemberList();
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
        this.setDefaultMemberList();
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
     * compare properties FullName to order to A->z
     * @param obj1 
     * @param obj2 
     */
    private compare(obj1: any, obj2: any) {
        return ((obj1.FullName).toLowerCase()).localeCompare((obj2.FullName).toLowerCase());
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * show popup update project
     * @param id project id
     */
    private onUpdateProjectClick(projectId: any) {
        this.IsAddPopup = false;
        this.KeySearchMember = "";
        let $ = window.jQuery;
        let user = this.userLogin;
        if (this.isAdmin) {
            user = "administrator";
        }        
        let self: Projects = this;
        this.lstShowMember = [];
        this.countTabClick = 0;
        this.clearValidationPopupProject();
        self.ShowRoleProject = "default";
        self.IsRole = true;
        this.ShowProjectTab = true;
        // lấy loại công việc cho popup edit
        projectService.getAllWorksType(self.userLogin).then(function (items: Array<any>) {
            self.lstWorksType = items;
        });
        // lấy thông tin dự án khi chọn một dự án để sửa.
        projectService.get(projectId).then(function (item: any) {
            self.WorksSupportProjectId = item.WorksSupportProjectId;
            self.CurrentWorksSupportTypeId = item.WorksSupportTypeId;
            self.WorksSupportProjectName = item.WorksSupportProjectName;
            self.WorksSupportTypeId = item.WorksSupportTypeId;
            self.Description = item.Description;
            self.IconUrl = item.IconUrl;
            self.IsSystem = item.IsSystem;
            self.IsActived = item.IsActived;
            self.numberMemberOfProject = item.NumberOfMember;
            // lấy vai trò cho các thành viên khi chỉnh sửa dự án.
            projectService.getMemberRoleByTypeId(self.WorksSupportTypeId).then(function (items: Array<any>) {
                self.lstMemberRole = items;
                self.setRoleNameInLstShowMember();
                // lấy danh sách thành viên trong dự án.
                projectService.getProjectMember(self.WorksSupportProjectId).then(function (items: Array<any>) {
                    self.LstCurrentMember = items;
                    self.addEditMemberIntoMemberList()
                    self.findMemberProject();
                });
            });
        }); 
        projectService.getAllWorksType(user).then(function (items: Array<any>) {
            self.lstWorksType = [];
            items.forEach(item => {
                if (item.IsCanAddProject || self.isAdmin) {
                    self.lstWorksType.push(item);
                }
            })
        }); 
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * filter member list before submit on server.
     */
    private filterMemberListBeforeSubmit() {
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
    public lstWorksGroup: Array<any> = [];

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017 
     * Navigate to worksgroup page
     * @param {*} projectId 
     * @param {*} isDeleted 
     * @memberof Projects
     */
    public LoadWorksGroupByProjectId(projectId: any, isDeleted: any) {
        if (parseInt(isDeleted) === 1) {
            localStorage.setItem('projectId', projectId);
            this.router.navigate([route.worksgroup.worksgroups.name]);
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * change role of member when click on role in dropdownlist . 
     * @param newRoleId 
     * @param userName 
     * @param oldRoleId 
     * @param newRoleName 
     */
    public changeMemberRole(newRoleId: any, userName: any, oldRoleId: any, newRoleName: any) {
        this.editMemberIntoMemberListByRoleId(userName, newRoleId, oldRoleId, newRoleName);
        this.findMemberProject();
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * edit role of member
     * @param userName 
     * @param newRoleId 
     * @param oldRoleId 
     * @param newRoleName 
     */
    private editMemberIntoMemberListByRoleId(userName: any, newRoleId: any, oldRoleId: any, newRoleName: any) {
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
            }
        }
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
     * click on tab and set default value
     */
    public ChangeTag(event: any) {
        let $ = window.jQuery;
        let self: Projects = this;
        // Xet mat dinh tab thong tin cho cho popup 
        this.ShowProjectTab = true;
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * show popup add Project
     */
    private onAddNewProjectClick() {
        this.clearValidationPopupProject();
        let self: Projects = this;
        this.IsAddPopup = true;
        self.WorksSupportProjectId = 0;
        self.numberMemberOfProject = 0;
        self.WorksSupportProjectName = "";
        self.Description = "";
        self.OrderIndex = 0;
        self.IsActived = 1;
        self.IsSystem = 0;
        self.User = "";
        self.IconUrl = null;
        this.WorksSupportTypeId = String.empty;
        this.ShowProjectTab = true;
        this.SearchMemberInfor = false;
        this.SearchMemberPopup = false;
        projectService.getAllWorksType(self.userLogin).then(function (items: Array<any>) {
            self.lstWorksType = [];
            items.forEach(item => {
                if (item.IsCanAddProject || self.isAdmin) {
                    self.lstWorksType.push(item);
                }
            })
        });
        // Load lai dữ liệu khi mở popup chinh sua truoc
        self.lstShowMember = [];
        self.lstShowAllMember = [];
    }
    private getWorkType() {
        let self: Projects = this;
        projectService.getAllWorksType(self.userLogin).then(function (items: Array<any>) {
            self.lstWorksType = [];
            items.forEach(item => {
                if (item.IsCanAddProject || self.isAdmin) {
                    self.lstWorksType.push(item);
                }
            })
        });
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * add or edit member
     */
    public onAddClicked(event: any) {
        let self: Projects = this;
        let $ = window.jQuery;
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
        this.ShowProjectTab = true;
        //1. add project
        if (this.IsAddPopup && this.validationPopupProject()) {
            let lstMember = this.getLstMember();
            if (this.lstMember.length === 0) {
                lstMember = [];
            }
            else {
                let lstMember = this.getLstMember();
            }
            let objProject = new UpdateModel(id, worksSupportProjectName, worksSupportTypeId, description, isActived, isSystem,
                iconUrl, this.userLogin, lstMember, [], []);
            // validate ở client
            projectService.create(objProject).then(function (item: any) {
                $('#modalUpdatePrpoject').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new ProjectModel(self.i18nHelper);
                self.loadProjects();
            }).error(function (error: any) {
                self.validateOnServerOfProject(error);
            });
        }
        //2. edit project
        if (!this.IsAddPopup && this.validationPopupProject()) {
            let lstMember = this.getLstMember();
            this.filterMemberListBeforeSubmit();
            let objProject = new UpdateModel(id, worksSupportProjectName, worksSupportTypeId, description, isActived, isSystem,
                iconUrl, this.userLogin, this.LstNewMember, this.LstEditMember, this.LstDeleteMember);

            projectService.UpdateProject(objProject).then(function (item: any) {
                $('#modalUpdatePrpoject').modal('hide');
                $('.modal-backdrop').modal('hide');
                self.model = new ProjectModel(self.i18nHelper);
                self.loadProjects();
            }).error(function (error: any) {
                self.validateOnServerOfProject(error);
            })
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * check error when click tab
     */
    public checkTab() {
        let $ = window.jQuery;
        let self: Projects = this;
        if (!self.checkWorkType(this.WorksSupportTypeId)) {
            this.ShowProjectTab = false;
            this.IsRemoveMember = false;
            $("#modalErorr").modal('toggle');
            //return true;
        } else if (this.ShowProjectTab) {
            this.ShowProjectTab = false;
        } else {
            this.ShowProjectTab = true;
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017 
     * show member by role or default style.
     */
    public selectShowRoleProject() {
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
     * find project by project name
     * @param projectName 
     */
    private findProjectNameByKey(projectName: any) {
        if (projectName.trim() === "") {
            this.lstProject.forEach(element => {
                element.IsDeleted = false;
            });
        } else {
            this.lstProject.forEach(member => {
                member.IsDeleted = true;
                let name = (member.WorksSupportProjectName === null) ? "" : member.WorksSupportProjectName;
                if ((name).toLowerCase().search(projectName.toLowerCase()) >= 0) {
                    member.IsDeleted = false;
                }
            });

        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * show search member popup 
     */
    public showPopupSearchMember(event: any) {
        let self: Projects = this;
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
     * find all member by department id
     * @param event 
     */
    public SearchUserByKey(event: any) {
        let $ = window.jQuery;
        let nameUser = this.txtKeyNameUser;
        let self: Projects = this;
        this.model.DepartmentId = this.WorksSupportDepartmentId;
        this.model.Keywords = nameUser;
        this.model.WorksSupportTypeId = this.WorksSupportTypeId;
        projectService.getUserBy(this.model).then(function (items: Array<any>) {
            self.lstMember = items;
            self.isCheckAll = false;
            self.filterMemberListAfterSearch(self.lstMember);
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
     * check all or uncheck all user when click on checkbox of search member popup.
     * @param event 
     */
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

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * add new member into member project to show
     * @param event 
     */
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

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * select or unselect user on search member popup.
     */
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

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * remove member in project member list.
     */
    private removeMemberInMemberList(userName: any) {
        // kiem tra truoc khi xoa thanh vien.
        let $ = window.jQuery;
        this.ShowProjectTab = false;
        let self: Projects = this;
        projectService.checkUserProcess(userName, this.WorksSupportProjectId).then(function (item: any) {
            if (item.NumberOfMember > 0) {
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
            self.IsRemoveMember = true;
            $("#modalErorr").modal("toggle");
        });
    }
    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * reload role of member list
     */
    public reloadMemberRoleList(typeId: any) {
        let $ = window.jQuery;
        let self: Projects = this;
        //1. lay role theo loai cong viec
        this.numberMemberOfProject = 0;
        this.WorksSupportTypeId = typeId;
        if (this.checkWorkType(typeId)) {
            projectService.getMemberRoleByTypeId(typeId).then(function (items: Array<any>) {
                self.lstMemberRole = items;
                self.setRoleNameInLstShowMember();
                self.setDefaultMemberList();
            });
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * validation work type 
     */
    public checkWorkType(typeId: any): boolean {
        this.IsErrorInfor = false;
        this.ErrorWorksSupportTypeId = String.empty;
        let typeWork = this.lstWorksType.find(typeWork => (typeWork.WorksSupportTypeId).toString() == typeId.toString());
        // kiểm tra combobox khi duoc chon.
        if (this.WorksSupportTypeId === "") {
            this.ErrorWorksSupportTypeId = "Loại công việc là bắt buộc!";
            this.IsErrorInfor = true;
            return false;
        } else if (typeWork !== undefined && typeWork.IsDefaultRole === 0) {
            this.ErrorWorksSupportTypeId = "Vui lòng khai báo vai trò mặc định cho loại công việc này!";
            this.IsDefaultRole = 0;
            return false;
        } else {
            this.IsDefaultRole = typeWork.IsDefaultRole;
            return true;
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * check the same project name on server.
     */
    public validateOnServerOfProject(error: Array<any>) {
        let self: Projects = this;
        if (error.length > 0) {
            for (let i = 0; i < error.length; i++) {
                if (error[i].Key === "WorksSupportProjectName") {
                    this.ErrorProjectName = error[i].Message;
                    break;
                }
            }
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * validate project name at client.
     */
    public validationProjectName() {
        let projectName = this.WorksSupportProjectName === null ? "" : this.WorksSupportProjectName;
        this.ErrorProjectName = String.empty;
        // kiem tra rong
        if (projectName.length === 0) {
            this.ErrorProjectName = "Tên dự án là bắt buộc!";

        }
        // max length == 200 character.
        if (projectName.length > 200) {
            this.ErrorProjectName = "Tên dự án không được vượt quá 200 kí tự!";
        }
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * validate project popup 
     */
    public validationPopupProject(): boolean {
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
            this.ErrorWorksSupportTypeId = "Vui lòng khai báo vai trò mặc định cho loại công việc!";
            isError = false;
        }

        return isError;
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * clear validate on project popup
     */
    public clearValidationPopupProject() {
        this.ErrorProjectName = String.empty;
        this.ErrorDescription = String.empty;
        this.ErrorWorksSupportTypeId = String.empty;
        this.IsDefaultRole = -1;
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * sort project name by a->z 
     * @param lstProject 
     */
    private sortProjectNameIsDesc(lstProject: Array<any>) {
        let lstIsActived: Array<any> = [];
        let lstNotActived: Array<any> = [];
        for (let i = 0; i < lstProject.length; i++) {
            if (lstProject[i].IsActived === 1) {
                lstIsActived.push(lstProject[i]);
            } else {
                lstNotActived.push(lstProject[i]);
            }
        }
        lstIsActived.sort(this.compareProjectName);
        lstNotActived.sort(this.compareProjectName);
        this.lstProject = lstIsActived;
        lstNotActived.forEach(item => {
            this.lstProject.push(item);
        })
    }

    /**
     * Writen by: Nguyen van Huan
     * Create by: 10.08.2017
     * compare WorksSupportProjectName between objects in project list
     * @param obj1
     * @param obj2 
     */
    private compareProjectName(obj1: any, obj2: any) {
        return ((obj1.WorksSupportProjectName).toLowerCase()).localeCompare((obj2.WorksSupportProjectName).toLowerCase());
    }
}
