import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
export class QualitiesModel {
    public options: any = {};
    public eventKey: string = "quality_ondatasource_changed";
    public actions: Array<any> = [];
    public WorksSupportQualityName: string = "";
    constructor(resourceHelper: any) {
        let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        if (!isCanDelete) {
            this.options = {
                columns: [
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.quality.grid.index"), type: "text", index: 0, width: "7%", className: "text-center" },
                    { field: "WorksSupportQualityName", title: resourceHelper.resolve("configurations.quality.grid.name"), type: "text", index: 1, sort: "true", width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.quality.grid.description"), type: "text", index: 2, sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.quality.grid.iconUrl"), type: "icon", index: 3, content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", width: "8%", sort: "false" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.quality.grid.isSystem"), type: "checkbox", index: 4, width: "8%", className: "text-center", sort: "true" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.quality.grid.isActive"), type: "toggle", index: 5, width: "8%", className: "text-center", sort: "true" }
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/Qualities/save"
            };
        } else {
            this.options = {
                columns: [
                    { field: "WorksSupportQualityId", title: resourceHelper.resolve("configurations.quality.grid.index"), type: "id", index: 0 },
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.quality.grid.index"), type: "text", index: 1, width: "7%", className: "text-center" },
                    { field: "WorksSupportQualityName", title: resourceHelper.resolve("configurations.quality.grid.name"), type: "text", index: 2, sort: "true", width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.quality.grid.description"), type: "text", index: 3, sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.quality.grid.iconUrl"), type: "icon", index: 3, content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", width: "8%", sort: "false" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.quality.grid.isSystem"), type: "checkbox", index: 5, width: "8%", className: "text-center", sort: "true" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.quality.grid.isActive"), type: "toggle", index: 6, width: "8%", className: "text-center", sort: "true" }
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/Qualities/save"
            };
        }
    }

    public addPageAction(action: any) {
        this.actions.push(action);
    }

    public importQualities(items: Array<any>) {
        let eventManager = window.ioc.resolve("IEventManager");
        eventManager.publish(this.eventKey, items);
    }
}
export class DeleteModel {
    public Id: string = String.empty;
    public User: string = String.empty;
    constructor(id: string, User: string) {
        this.Id = id;
        this.User = User;
    }
}