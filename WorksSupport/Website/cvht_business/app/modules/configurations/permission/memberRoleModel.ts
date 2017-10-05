export class MemberRoleModel {
    public WorksSupportTypeId: any = 0;
    public WorksSupportMemberRoleId: any = 0;
    public WorksSupportMemberRoleName: any = String.empty;
    public IsCanAddWorksSupportGroup: boolean = false;
    public IsCanAddWorksSupport: boolean = false;
    public IsCanEditWorksSupportGroup: boolean = false;
    public IsCanEditWorksSupport: boolean = false;
    public IsCanDeleteWorksSupportGroup: boolean = false;
    public IsCanDeleteWorksSupport: boolean = false;
    public IsDefaultRole: boolean = false;
    constructor (WorksSupportTypeId: any, WorksSupportMemberRoleId: any,
        IsCanAddWorksSupportGroup: any, IsCanAddWorksSupport: any,  
        IsCanEditWorksSupportGroup: any, IsCanEditWorksSupport: any, IsCanDeleteWorksSupportGroup: any,
        IsCanDeleteWorksSupport: any, WorksSupportMemberRoleName:any, IsDefaultRole: any) {
            this.WorksSupportTypeId = WorksSupportTypeId,
            this.WorksSupportMemberRoleId = WorksSupportMemberRoleId,
            this.WorksSupportMemberRoleName = WorksSupportMemberRoleName,
            this.IsCanAddWorksSupportGroup = IsCanAddWorksSupportGroup,
            this.IsCanAddWorksSupport = IsCanAddWorksSupport,
            this.IsCanEditWorksSupportGroup = IsCanEditWorksSupportGroup,
            this.IsCanEditWorksSupport = IsCanEditWorksSupport,
            this.IsCanDeleteWorksSupportGroup = IsCanDeleteWorksSupportGroup,
            this.IsCanDeleteWorksSupport = IsCanDeleteWorksSupport,
            this.IsDefaultRole = IsDefaultRole
    }
}