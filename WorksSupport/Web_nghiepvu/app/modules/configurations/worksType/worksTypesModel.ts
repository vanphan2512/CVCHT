import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
export class WorksTypesModel {
    public options: any = {};
    public eventKey: string = "worksType_ondatasource_changed";
    public actions: Array<any> = [];
    constructor(resourceHelper: any) {
        let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        if (!isCanDelete) {
            this.options = {
                columns: [
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.worksType.grid.index"), type: "rownumber", index: 1, width: "7%", className: "text-center", sort: "true" },
                    { field: "WorksSupportTypeName", title: resourceHelper.resolve("configurations.worksType.grid.name"), type: "text", index: 2, sort: "true", width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.worksType.grid.description"), type: "text", index: 3, sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.worksType.grid.iconUrl"), type: "icon", index: 4, content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", sort: "false", width: "8%" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.worksType.grid.isSystem"), type: "checkbox", index: 5, width: "8%", className: "text-center", sort: "true" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.worksType.grid.isActive"), type: "toggle", index: 6, width: "8%", className: "text-center", sort: "true" },
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/workssupporttypes/save"
            };
        } else {
            this.options = {
                columns: [
                    { field: "WorksSupportTypeId", title: resourceHelper.resolve("configurations.worksType.grid.index"), type: "id", index: 0, sort: "true" },
                    { field: "OrderIndex", title: resourceHelper.resolve("configurations.worksType.grid.index"), type: "rownumber", index: 1, width: "7%", className: "text-center", sort: "true" },
                    { field: "WorksSupportTypeName", title: resourceHelper.resolve("configurations.worksType.grid.name"), type: "text", index: 2, sort: "true", width: "20%" },
                    { field: "Description", title: resourceHelper.resolve("configurations.worksType.grid.description"), type: "text", index: 3, sort: "false" },
                    { field: "IconUrl", title: resourceHelper.resolve("configurations.worksType.grid.iconUrl"), type: "icon", index: 4, content: "<a class=\"modalIcon\" href=\"#\" data-toggle=\"modal\" data-target=\"#myIconModal\"><i class='fa fa-edit' aria-hidden=\"true\"></i></a>", className: "text-center", sort: "false", width: "8%" },
                    { field: "IsSystem", title: resourceHelper.resolve("configurations.worksType.grid.isSystem"), type: "checkbox", index: 5, width: "8%", className: "text-center", sort: "true" },
                    { field: "IsActived", title: resourceHelper.resolve("configurations.worksType.grid.isActive"), type: "toggle", index: 6, width: "8%", className: "text-center", sort: "true" },
                ],
                data: [],
                enableEdit: true,
                enableDelete: false,
                ajaxUrl: "http://nc.erp.workssupport.com/api/v2/workssupporttypes/save"
            };
        }
    }

    public importWorksTypes(items: Array<any>) {
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
export class AddWorkTypeModel {
    public WorksSupportTypeId: number = 0;
    public WorksSupportTypeName: string = String.empty;
    public Description: string = String.empty;
    public IconUrL: string = String.empty;
    public IsActived: boolean = false;
    public IsSystem: boolean = false;
    public Priority: string = String.empty;
    public Quality: string = String.empty;
    public Status: string = String.empty;
    public Step: string = String.empty;
    public User: string = String.empty;
    public OrderIndex: number = 0;
    public DeletedStepId: string = String.empty;
    constructor(WorksSupportTypeId: number, WorksSupportTypeName: string, Description: string, IconUrL: string, IsActived: boolean, IsSystem: boolean, Priority: string, Quality: string, Step: string, User: string, OrderIndex: number, DeletedStepId: string) {
        this.WorksSupportTypeId = WorksSupportTypeId;
        this.WorksSupportTypeName = WorksSupportTypeName;
        this.Description = Description;
        this.IconUrL = IconUrL;
        this.IsActived = IsActived;
        this.IsSystem = IsSystem;
        this.Priority = Priority;
        this.Quality = Quality;
        this.Step = Step;
        this.User = User;
        this.OrderIndex = OrderIndex;
        this.DeletedStepId = DeletedStepId;
    }
}
export class StepModel {
    public StepID: string = String.empty;
    public StepNAME: string = "";
    public StepDESCRIPTION: string = "";
    public StepSTEPCOLORCODE: string = "";
    public StepMAXPROCESSTIME: string = "";
    public listNextStep: string[] = [];
    public isEdit: boolean = false;
    public StepISINITSTEP: boolean = false;
    public StepISFINISHSTEP: boolean = false;
    constructor(StepID: string, StepNAME: string, StepDESCRIPTION: string, StepSTEPCOLORCODE: string, StepMAXPROCESSTIME: string, StepISINITSTEP: boolean, StepISFINISHSTEP: boolean, listNextStep: string[], isEdit: boolean ) {
        this.StepID = StepID;
        this.StepNAME = StepNAME;
        this.StepDESCRIPTION = StepDESCRIPTION;
        this.StepSTEPCOLORCODE = StepSTEPCOLORCODE;
        this.StepMAXPROCESSTIME = StepMAXPROCESSTIME;
        this.StepISINITSTEP = StepISINITSTEP;
        this.StepISFINISHSTEP = StepISFINISHSTEP;
        this.listNextStep = listNextStep;
        this.isEdit = isEdit;        
    }
}