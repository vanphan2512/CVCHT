
export class ProjectModel {
    public options: any = {};
    public eventKey: string = "project_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        this.options = {
            columns: [
                { field: "WorksSupportProjectId", title: resourceHelper.resolve("eoffice.project.grid.index"), type: "id", index: 0 },
                { field: "OrderIndex", title: resourceHelper.resolve("eoffice.project.grid.index"), type: "rownumber", index: 1, className: "reorder text-center", width: "10%", },
                { field: "WorksSupportProjectName", title: resourceHelper.resolve("eoffice.project.grid.name"), type: "text", index: 2 },
                { field: "WorksSupportTypeId", title: resourceHelper.resolve("eoffice.project.grid.nameType"), type: "text", index: 3 },
                { field: "Description", title: resourceHelper.resolve("eoffice.project.grid.description"), type: "text", index: 4 },
                { field: "IsSystem", title: resourceHelper.resolve("eoffice.project.grid.isSystem"), type: "checkbox", index: 5, width: "5%", className: "text-center" },
                { field: "IsActived", title: resourceHelper.resolve("eoffice.project.grid.isActive"), type: "toggle", index: 6, width: "5%", className: "text-center" },
            ],
            data: [],
            enableEdit: true,
            enableDelete: false,
            ajaxUrl: "http://nc.erp.workssupport.com/api/v2/ProjectMember/save"
        };
    }
    public addPageAction(action: any) {
        this.actions.push(action);
    }

    public importProject(items: Array<any>) {
        let eventManager = window.ioc.resolve("IEventManager");
        eventManager.publish(this.eventKey, items);
    }
}
export class DeleteModel {
    public Id: string = String.empty;
    public User: string = String.empty;
    constructor(id: string, user: string) {
        this.Id = id;
        this.User = user;
    }
}
export class UpdateModel {
    public Id: string = String.empty;
    public WorksSupportProjectName: string = String.empty;
    public WorksSupportTypeId: string = String.empty;
    public Description: string = String.empty;
    public OrderIndex: string = String.empty;
    public IsActived: string = String.empty;
    public IsSystem: string = String.empty;
    public User: string = String.empty;
    constructor(id: string, worksSupportProjectName: string, worksSupportTypeId: string, description: string, orderIndex: string, isActived: string, isSystem: string, user: string) {
        this.Id = id;
        this.WorksSupportProjectName = worksSupportProjectName;
        this.WorksSupportTypeId = worksSupportTypeId;
        this.Description = description;
        this.OrderIndex = orderIndex;
        this.IsActived = isActived;
        this.IsSystem = isSystem;
        this.User = user;
    }
}
