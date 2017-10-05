export class WorksModel {
    public options: any = {};
    public eventKey: string = "works_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        this.options = {
            columns: [
                { field: "WorksSupportId", title: resourceHelper.resolve("eoffice.works.grid.index"), type: "id", index: 0 },
                { field: "WorksSupportName", title: resourceHelper.resolve("eoffice.works.grid.name"), type: "text", index: 1, className: "reorder text-center", width: "10%", },
                { field: "CreatedUser", title: resourceHelper.resolve("eoffice.works.grid.createduser"), type: "text", index: 2 },
                { field: "CreateDate", title: resourceHelper.resolve("eoffice.works.grid.createddate"), type: "text", index: 3 },
                { field: "WorksSupportStatusName", title: resourceHelper.resolve("eoffice.works.grid.status"), type: "text", index: 4, width: "5%", className: "text-center" },
                { field: "Currentprogress", title: resourceHelper.resolve("eoffice.works.grid.currentprogress"), type: "text", index: 5, width: "5%", className: "text-center" },
                { field: "WorksSupportId", title: resourceHelper.resolve("eoffice.works.grid.id"), type: "text", index: 5, width: "5%", className: "text-center" },
            ],
            data: [],
            enableEdit: false,
            enableDelete: true,
            enableXL:true,
            ajaxUrl: "http://nc.erp.workssupport.com/api/v2/WorksSupport"
        };
    }
    public addPageAction(action: any) {
        this.actions.push(action);
    }

    public importWorks(items: Array<any>) {
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