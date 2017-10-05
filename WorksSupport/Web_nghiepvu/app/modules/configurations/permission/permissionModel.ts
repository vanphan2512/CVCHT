export class PermissionModel {
    public options: any = {};
    public eventKey: string = "worksType_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        this.options = {
            columns: [
                { field: "WorksSupportTypeId", title: resourceHelper.resolve("configurations.worksType.grid.index"), type: "id", index: 0, sort:"true" },
                { field: "OrderIndex", title: resourceHelper.resolve("configurations.worksType.grid.index"), type: "rownumber", index: 1, width: "8%", className: "text-center", sort:"true" },
                { field: "WorksSupportTypeName", title: resourceHelper.resolve("configurations.worksType.grid.name"), type: "text", index: 2, sort:"true", width: "20%" },
                { field: "Description", title: resourceHelper.resolve("configurations.worksType.grid.description"), type: "text", index: 3, sort:"true" },
                { field: "IconUrl", title: resourceHelper.resolve("configurations.worksType.grid.iconUrl"), type: "icon", index: 4, content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", sort:"false", width: "8%" },
                { field: "IsSystem", title: resourceHelper.resolve("configurations.worksType.grid.isSystem"), type: "checkbox", index: 5, width: "8%", className: "text-center", sort:"true" },
                { field: "IsActived", title: resourceHelper.resolve("configurations.worksType.grid.isActive"), type: "toggle", index: 6, width: "8%", className: "text-center", sort:"true"},
            ],
            data: [],
            enableEdit: true,
            enableDelete: false,
            ajaxUrl: "http://nc.erp.workssupport.com/api/v2/workssupporttypes/save"
        };
    }

    public import(items: Array<any>) {
        let eventManager = window.ioc.resolve("IEventManager");
        eventManager.publish(this.eventKey, items);
    }
}