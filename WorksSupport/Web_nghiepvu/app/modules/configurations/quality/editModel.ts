export class EditModel {
    public Id: number = 0;
    public WorksSupportQualityName: string = "";
    public Description: string = "";
    public IconUrl: string = ""
    public OrderIndex: number = 0;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public User: string = "";

    constructor(Id: number, WorksSupportQualityName: string, OrderIndex: number, Description: string,IconUrl: string,
        IsActived: boolean, IsSystem: boolean, User: string ) {
        this.Id = Id;
        this.WorksSupportQualityName = WorksSupportQualityName;
        this.Description = Description;
        this.IconUrl = IconUrl;
        this.OrderIndex = OrderIndex;        
        this.IsActived = IsActived ? 1 : 0;
        this.IsSystem = IsSystem ? 1 : 0;
        this.User =User;

    }

}