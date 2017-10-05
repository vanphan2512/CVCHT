import { ValidationException } from "../../../common/models/exception";
export class UpdateStatusModel {
    public Id: number = 0;
    public WorksSupportStatusName: string = "";
    public IsCompleteStatus: boolean = false;
    public IsCloseStatus: boolean = false;
    public IsInitStatus: boolean = false;
    public Description: string = "";
    public OrderIndex: number = -1;
    public IsDeleted: boolean = false;
    public IsActived: boolean = false;
    public IsSystem: boolean = false;
    public IconUrL: string = "";
    public User: string = "";

    public import(item: any) {
        if (!item) { return; }
        this.Id = item.Id;
        this.WorksSupportStatusName = item.WorksSupportStatusName;
        this.Description = item.Description;
        this.OrderIndex = item.OrderIndex;
        this.IsInitStatus = item.IsInitStatus;
        this.IsActived = item.IsActived ;
        this.IsCompleteStatus = item.IsCompleteStatus ;
        this.IsCloseStatus = item.IsCloseStatus ;
        this.IsSystem = item.IsSystem;
        if (item.IconUrl !== null && item.IconUrl !== "") {
            this.IconUrL = item.IconUrl;
        }
        this.User = item.User;
    }
}