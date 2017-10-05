export class UpdateModel {
    public Id: number;
    public WorksSupportProjectId: number;
    public WorksSupportGroupName: string = String.empty;
    public Description: string = String.empty;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public iconUrl: string = String.empty;
    public LstNewMember: Array<any> = [];
    public LstDeleteMember: Array<any> = [];
    public LstEditMember: Array<any> = [];
    public User: string=String.empty;
    constructor(Id: number, worksSupportProjectId: number, worksSupportGroupName: string, description: string,
        isActived: number, isSystem: number, iconUrl: string,user: string, lstNewMember: Array<any>, lstDeleteMember: Array<any>, lstEditMember: Array<any>) {
        this.Id = Id;
        this.WorksSupportProjectId = worksSupportProjectId;
        this.WorksSupportGroupName = worksSupportGroupName;
        this.Description = description;
        this.IsActived = isActived;
        this.IsSystem = isSystem;
        this.iconUrl = iconUrl;
        this.LstNewMember = lstNewMember;
        this.LstDeleteMember = lstDeleteMember;
        this.LstEditMember = lstEditMember;
        this.User = user;
    }     
}

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