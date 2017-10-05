export class AddCommentModel {
    public WorksSupportId: string = "";
    public CommentContent: string = "";
    public User: string = "";
    public ListCommentAttachment: string = "";
    constructor(WorksSupportId: string, CommentContent: string, User: string, ListCommentAttachment: string) {
        this.WorksSupportId = WorksSupportId;
        this.CommentContent = CommentContent;
        this.User = User;
        this.ListCommentAttachment = ListCommentAttachment;
    }
}