export class WfNxPermissModel {
    public WorksSupportStepId: string = String.empty;
    public WorksSupportStepName: string = String.empty;
    public WorksSupportTypeId: string = String.empty;
    public NextWorksSupportStepId:  string = String.empty;
    public WorksSupportMemberRoleId: string = String.empty;
    constructor (WorksSupportStepId: string,
    WorksSupportStepName: string,
    WorksSupportTypeId: string,
    NextWorksSupportStepId: string,
    WorksSupportMemberRoleId: string) {
        this.WorksSupportStepId = WorksSupportStepId,
        this.WorksSupportStepName = WorksSupportStepName,
        this.WorksSupportTypeId = WorksSupportTypeId,
        this.NextWorksSupportStepId = NextWorksSupportStepId,
        this.WorksSupportMemberRoleId = WorksSupportMemberRoleId
    }
}