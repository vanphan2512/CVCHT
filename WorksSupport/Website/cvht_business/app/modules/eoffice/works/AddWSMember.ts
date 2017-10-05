export class AddMember {
    public WorksSupportId: number = 0;
    public WorksSupportDate: Date;
    public MemberUserName: string = "";
    public WorksSupportMemberRoleId: number = 0;
    public UpdatedUser: string = "";
    constructor(WorksSupportId: number, WorksSupportDate: Date, MemberUserName:string, WorksSupportMemberRoleId: number,UpdatedUser: string) {
        this.WorksSupportId = WorksSupportId;
        this.WorksSupportDate = WorksSupportDate;
        this.MemberUserName = MemberUserName;
        this.WorksSupportMemberRoleId = WorksSupportMemberRoleId; 
        this.UpdatedUser = UpdatedUser;       
    }
}