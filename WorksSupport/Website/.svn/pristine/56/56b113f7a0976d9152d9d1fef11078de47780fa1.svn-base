import { ValidationException } from "../../../common/models/exception";
export class UpdateMemberModel {
    public Id: number = 0;
    public WorksSupportMemberRoleName: string = "";
    public Description: number = 0;
    public IconUrl: number=0;
    public OrderIndex: number = 0;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public User: string = "";

    public import(item: any) {
        if (!item) { return; }
        this.Id = item.WorksSupportMemberRoleId;
        this.WorksSupportMemberRoleName = item.WorksSupportMemberRoleName;
        this.Description = item.Description;
        this.OrderIndex = item.OrderIndex;
        if (item.IsActived === false) {
            this.IsActived = 1;
        }
        if (item.IsSystem === true) {
            this.IsSystem = 1;
        }
        else {
            this.IsSystem = 0;
        }
        if (item.IconUrl !== null && item.IconUrl !== "") {
            this.IconUrl = item.IconUrl;
        }
    }
}