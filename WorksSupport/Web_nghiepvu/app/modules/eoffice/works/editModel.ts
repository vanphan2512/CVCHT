export class EditModel {
    public Id: number = 0;
    public WorksSupportName: string = "";
    public User: string = "";
    public Content: string = "";
    public ExpectedCompletedDate: Date;
    public WorksSupportPriorityId: number = 0;
    public WorksSupportGroupId: number = 0;
    public ListInvoleId: string = "";
    public ListAttachment: string = "";
    public ListDeletedAttachment: string = "";
    constructor(Id: number, WorksSupportName: string, User: string, Content: string,
        ExpectedCompletedDate: Date, WorksSupportPriorityId: number, WorksSupportGroupId: number, ListInvoleId: string, ListAttachment: string, ListDeletedAttachment: string) {
        this.Id = Id;
        this.WorksSupportName = WorksSupportName;
        this.User = User;
        this.Content = Content;
        this.ExpectedCompletedDate = ExpectedCompletedDate;
        this.WorksSupportPriorityId = WorksSupportPriorityId;
        this.WorksSupportGroupId = WorksSupportGroupId;
        this.ListInvoleId = ListInvoleId;
        this.ListAttachment = ListAttachment;
        this.ListDeletedAttachment = ListDeletedAttachment;
    }
}