import { KeyNamePair } from "../../../common/models/keyNamePair";
export class AddProjectModel {
    public Id: number = 0;
    public WorksSupportProjectName: string = "";
    public worksSupportTypeId: string = "";
    public Description: string = "";
    public OrderIndex: string = "";
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public User: string = "";

    public import(item: any) {
        if (!item) { return; }
        this.Id = item.WorksSupportProjectId;
        this.WorksSupportProjectName = item.WorksSupportProjectName;
        this.worksSupportTypeId = item.worksSupportTypeId;
        this.Description = item.Description;
        this.OrderIndex = item.OrderIndex;
        if (item.IsActived === false) {
            this.IsActived = 1;
        }
        if (item.IsSystem === 1 || item.IsSystem) {
            this.IsSystem = 1;
        }
        else {
            this.IsSystem = 0;
        }

        this.User = "administrator";
    }
}