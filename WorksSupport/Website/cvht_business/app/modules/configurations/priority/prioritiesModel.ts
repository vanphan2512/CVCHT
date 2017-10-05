import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
export class PrioritiesModel {
    public options: any = {};
    public eventKey: string = "priorities_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        if (!isCanDelete) {
            this.options = {
                columns: [                   
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.priorities.grid.index"), type: "rownumber", index: 0, width: "7%", className: "text-center" },
                    { field: "WorksSupportPriorityName", title: resourceHelper.resolve("configurations.priorities.grid.name"), type: "text", index: 1, sort: "true", width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.priorities.grid.description"), type: "text", index: 2, sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.priorities.grid.iconUrl"), type: "icon", index: 3, content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", width: "8%", sort: "false" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.priorities.grid.isSystem"), type: "checkbox", index: 4, width: "8%", className: "text-center" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.priorities.grid.isActive"), type: "toggle", index: 5, width: "8%", className: "text-center", sort: "true" }
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/priorities/save"
            };
        } else {
            this.options = {
                columns: [
                    { field: "WorksSupportPriorityId", title: resourceHelper.resolve("configurations.priorities.grid.index"), type: "id", index: 0 },
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.priorities.grid.index"), type: "rownumber", index: 1, width: "7%", className: "text-center" },
                    { field: "WorksSupportPriorityName", title: resourceHelper.resolve("configurations.priorities.grid.name"), type: "text", index: 1, sort: "true", width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.priorities.grid.description"), type: "text", index: 2, sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.priorities.grid.iconUrl"), type: "icon", index: 3, content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", width: "8%", sort: "false" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.priorities.grid.isSystem"), type: "checkbox", index: 4, width: "8%", className: "text-center" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.priorities.grid.isActive"), type: "toggle", index: 5, width: "8%", className: "text-center", sort: "true" }
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/priorities/save"
            };
        }
    }

    public addPageAction(action: any) {
        this.actions.push(action);
    }

    public importPriority(items: Array<any>) {
        let eventManager = window.ioc.resolve("IEventManager");
        eventManager.publish(this.eventKey, items);
    }
}