import sessionStorage from "../../../common/storages/sessionStorage";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
export class StatusModel {
    public options: any = {};
    public eventKey: string = "status_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        let isCanDelete: boolean = sessionStorage.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        if (!isCanDelete) {
            this.options = {
                columns: [
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.status.grid.index"), type: "rownumber", index: 0, className: "text-center", width: "7%" },
                    { field: "WorksSupportStatusName", title: resourceHelper.resolve("configurations.status.grid.name"), type: "text", className: "text-left", index: 1, width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.status.grid.description"), type: "text", index: 2, className: "text-left", sort: "false" },
                    { field: "IsInitStatus", title: resourceHelper.resolve("configurations.status.grid.isInitStatus"), type: "checkbox", index: 3, className: "text-center", width: "7%", sort: "false" },
                    { field: "IsCompleteStatus", title: resourceHelper.resolve("configurations.status.grid.isCompleteStatus"), type: "checkbox", index: 4, className: "text-center", width: "7%", sort: "false" },
                    { field: "IsCloseStatus", title: resourceHelper.resolve("configurations.status.grid.closeStatus"), type: "checkbox", index: 5, className: "text-center", width: "7%", sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.status.grid.iconUrl"), type: "icon", index: 6, sort: "false", content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", width: "8%" },
                    { field: "ColorCode", title: resourceHelper.resolve("configurations.status.grid.color"), type: "icon", index: 7, sort: "false", className: "text-center", width: "7%" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.status.grid.isSystem"), type: "checkbox", index: 8, width: "8%", className: "text-center" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.status.grid.isActive"), type: "toggle", index: 9, width: "8%", className: "text-center", sort: "true" },
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/status/save"
            };
        } else {
            this.options = {
                columns: [
                    { field: "WorksSupportStatusId", title: resourceHelper.resolve("configurations.status.grid.index"), type: "id", index: 0 },
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.status.grid.index"), type: "rownumber", index: 1, className: "text-center", width: "7%" },
                    { field: "WorksSupportStatusName", title: resourceHelper.resolve("configurations.status.grid.name"), type: "text", className: "text-left", index: 2, width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.status.grid.description"), type: "text", index: 3, className: "text-left", sort: "false" },
                    { field: "IsInitStatus", title: resourceHelper.resolve("configurations.status.grid.isInitStatus"), type: "checkbox", index: 4, className: "text-center", width: "7%", sort: "false" },
                    { field: "IsCompleteStatus", title: resourceHelper.resolve("configurations.status.grid.isCompleteStatus"), type: "checkbox", index: 5, className: "text-center", width: "7%", sort: "false" },
                    { field: "IsCloseStatus", title: resourceHelper.resolve("configurations.status.grid.closeStatus"), type: "checkbox", index: 5, className: "text-center", width: "7%", sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.status.grid.iconUrl"), type: "icon", index: 6, sort: "false", content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", width: "8%" },
                    { field: "ColorCode", title: resourceHelper.resolve("configurations.status.grid.color"), type: "color", index: 7, sort: "false", className: "text-center", width: "7%" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.status.grid.isSystem"), type: "checkbox", index: 8, width: "8%", className: "text-center" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.status.grid.isActive"), type: "toggle", index: 9, width: "8%", className: "text-center", sort: "true" },
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/status/save"
            };
        }
    }
    public addPageAction(action: any) {
        this.actions.push(action);
    }

    public importStatus(items: Array<any>) {
        let eventManager = window.ioc.resolve("IEventManager");
        eventManager.publish(this.eventKey, items);
    }
}