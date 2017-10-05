export class UpdateModelProject {
    public Id: number = 0;
    public WorksSupportProjectName: string = "";
    public WorksSupportTypeId: number = 0;
    public Description: string = "";
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public IconUrl: string = "";
    public User: string = "";
    public LstEditMember: Array<any> = [];
    public LstDeleteMember: Array<any> = [];
    public LstNewMember: Array<any> = [];

    constructor(Id: number, WorksSupportProjectName: string, WorksSupportTypeId: number, Description: string,
        IsActived: number, IsSystem: number, IconUrl: string, User: string, lstNewMember: Array<any>,
        lstEditMember: Array<any>, lstDeleteMember: Array<any>) {
        this.Id = Id;
        this.WorksSupportProjectName = WorksSupportProjectName;
        this.WorksSupportTypeId = WorksSupportTypeId;
        this.Description = Description;
        this.IsActived = IsActived;
        this.IsSystem = IsSystem;
        this.IconUrl = IconUrl;
        this.User = User;
        this.LstEditMember = lstEditMember;
        this.LstDeleteMember = lstDeleteMember;
        this.LstNewMember = lstNewMember;
    }

}