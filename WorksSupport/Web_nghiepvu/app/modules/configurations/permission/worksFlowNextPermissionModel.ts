
// Definination WorksFlowNextPermissionModel
export class WorksFlowNextPermissionModel {
    public WorksSupportStepId: number = 0;
    public WorksSupportTypeId: number = 0;
    public NextWorksSupportStepId: number = 0;
    public WorksSupportMemberRoleId: number = 0;
    constructor(worksSupportStepId: number,
        worksSupportTypeId: number, 
        nextWorksSupportStepId: number,
        worksSupportMemberRoleId: number,
    ) {
        this.WorksSupportStepId = worksSupportStepId;
        this.WorksSupportTypeId = worksSupportTypeId;
        this.NextWorksSupportStepId = nextWorksSupportStepId;
        this.WorksSupportMemberRoleId = worksSupportMemberRoleId;
    }
}