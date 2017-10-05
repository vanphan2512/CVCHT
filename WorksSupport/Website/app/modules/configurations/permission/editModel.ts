export class EditModel {
    public Id: number = 0;
    public WorksSupportTypeName: string = "";
    public IconUrL: string = "";
    public Description: string = "";
    public OrderIndex: number = -1;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public User: string = "";

    constructor(Id: number, WorksSupportTypeName: string, OrderIndex: number, Description: string, IconUrL: string,
        IsActived: boolean, IsSystem: boolean, User: string ) {
        this.Id = Id;
        this.WorksSupportTypeName = WorksSupportTypeName;
        this.Description = Description;
        this.OrderIndex = OrderIndex;   
        this.IconUrL = IconUrL;     
        this.IsActived = IsActived ? 1 : 0;
        this.IsSystem = IsSystem ? 1 : 0;
        this.User = User;
    }
}