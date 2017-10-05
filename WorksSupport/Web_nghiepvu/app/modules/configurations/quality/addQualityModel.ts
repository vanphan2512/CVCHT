import {KeyNamePair} from "../../../common/models/keyNamePair";
export class AddQualityModel {
    public Id: number = 0;
    public WorksSupportQualityName: string = "";
    public Description: string = "";
    public IconUrl: string = "";
    public OrderIndex: number = 0;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public User: string = "";

    public import(item: any) {
        if (!item) { return; }
        this.Id = item.WorksSupportQualityId;
        this.WorksSupportQualityName = item.WorksSupportQualityName;
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
        this.User = "administrator";
    }
    
}