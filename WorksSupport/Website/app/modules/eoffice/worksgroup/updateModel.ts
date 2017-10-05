export class UpdateModel {
    public Id: number;
    public WorksSupportProjectId: number;
    public WorksSupportGroupName: string = String.empty;
    public Description: string = String.empty;
    public IsActived: number = 0;
    public IsSystem: number = 0;
    public IconUrl: string = String.empty;
    public LstNewMember: Array<any> = [];
    public LstDeleteMember: Array<any> = [];
    public LstEditMember: Array<any> = [];
    public User: string=String.empty;
    constructor(id: number, worksSupportProjectId: number, worksSupportGroupName: string, description: string,
        isActived: number, isSystem: number, iconUrl: string,user: string, lstNewMember: Array<any>, lstDeleteMember: Array<any>, lstEditMember: Array<any>) {
        this.Id = id;
        this.WorksSupportProjectId = worksSupportProjectId;
        this.WorksSupportGroupName = worksSupportGroupName;
        this.Description = description;
        this.IsActived = isActived;
        this.IsSystem = isSystem;
        this.LstNewMember = lstNewMember;
        this.LstDeleteMember = lstDeleteMember;
        this.LstEditMember = lstEditMember;
        this.User = user;
    }
}