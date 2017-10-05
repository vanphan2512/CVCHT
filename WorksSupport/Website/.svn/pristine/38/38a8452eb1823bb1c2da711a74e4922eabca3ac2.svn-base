import { ValidationException } from "../../../common/models/exception";
export class UpdatePriorityModel {
    public Id: number = 0;
    public WorksSupportPriorityName: string = "";
    public Description: string = String.empty;
    public OrderIndex: number = 1;
    public IsActived: boolean = false;
    public IsSystem: boolean = false;
    public IconUrl: string = "";
    public User: string = "";

    public import(item: any) {
        if (!item) { return; }
        this.Id = item.WorksSupportPriorityId;
        this.WorksSupportPriorityName = item.WorksSupportPriorityName;
        this.Description = item.Description;
        this.OrderIndex = item.OrderIndex;
        this.IsActived = item.IsActived;
        this.IsSystem = item.IsSystem;
        if (item.IconUrl !== null && item.IconUrl !== "") {
            this.IconUrl = item.IconUrl;
        }
        this.User = item.User;
    }
}