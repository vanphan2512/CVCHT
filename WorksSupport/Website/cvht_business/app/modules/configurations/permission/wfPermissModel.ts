export class WfPermissModel {
    public WorksSupportStepId: any = 0;
    public WorksSupportMemberRoleId: any = 0;
    public IsCanEditContent: boolean = false;
    public IsCanEditSolutionContent: boolean = false;
    public IsCanAddAttachment: boolean = false;
    public IsCanComment: boolean = false;
    public IsCanEditExpectedCompletedDate: boolean = false;
    public IsCanChangeProgress: boolean = false;
    public IsCanEditQuality: boolean = false;
    public IsCanProcessWorkflow: boolean = false;
    public IsMustChooseProcessUser: boolean = false;
    constructor(WfworksSupportStepId: any,
        WfWorksSupportMemberRoleId: any, WfIsCanEditContent: boolean, WfIsCanEditSolutionContent: boolean,
        WfIsCanAddAttachment: boolean, WfIsCanComment: boolean, WfIsCanEditExpectedCompletedDate: boolean,
        WfIsCanChangeProgress: boolean, WfIsCanEditQuality: boolean, WfIsCanProcessWorkflow: boolean,
        WfIsMustChooseProcessUser: boolean
    ) {
        this.WorksSupportStepId = WfworksSupportStepId;
        this.WorksSupportMemberRoleId = WfWorksSupportMemberRoleId;
        this.IsCanEditContent = WfIsCanEditContent;
        this.IsCanEditSolutionContent = WfIsCanEditSolutionContent;
        this.IsCanAddAttachment = WfIsCanAddAttachment;
        this.IsCanComment = WfIsCanComment;
        this.IsCanEditExpectedCompletedDate = WfIsCanEditExpectedCompletedDate;
        this.IsCanChangeProgress = WfIsCanChangeProgress;
        this.IsCanEditQuality = WfIsCanEditQuality;
        this.IsCanProcessWorkflow = WfIsCanProcessWorkflow;
        this.IsMustChooseProcessUser = WfIsMustChooseProcessUser;
    }
}