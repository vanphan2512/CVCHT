export class WorksGroupsModel {
    public options: any = {};
    public eventKey: string = " WorksGroups_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        this.options = {
            columns: [
                { field: "WorksSupportGroupId", title: resourceHelper.resolve("eoffice.worksgroup.grid.index"), type: "id", index: 0 },
                { field: "OrderIndex", title: resourceHelper.resolve("eoffice.worksgroup.grid.orderIndex"), type: "rownumber", index: 1, width: "10%", className: "reorder text-center" },
                { field: "WorksSupportGroupName", title: resourceHelper.resolve("eoffice.worksgroup.grid.groupName"), type: "text", index: 2 },
                { field: "Description", title: resourceHelper.resolve("eoffice.worksgroup.grid.description"), type: "text", index: 3 },
                { field: "IsActived", title: resourceHelper.resolve("eoffice.worksgroup.grid.isActived"), type: "toggle", index: 4, width: "5%", className: "text-center" }
            ],
            data: [],
            enableEdit: true,
            enableDelete: false,
            ajaxUrl: "http://nc.erp.workssupport.com/api/v2/group/save"
        };
    }

    public importWorksGroup(items: Array<any>) {
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
