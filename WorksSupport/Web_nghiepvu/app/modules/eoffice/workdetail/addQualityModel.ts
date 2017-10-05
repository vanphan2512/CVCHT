export class AddQualityModel {
    public WorksSupportId: string = "";
    public QualityId: string = "";
    public Note: string = "";
    public User: string = "";
    constructor(WorksSupportId: string, QualityId: string, Note: string, User: string) {
        this.WorksSupportId = WorksSupportId;
        this.QualityId = QualityId;
        this.Note = Note;
        this.User = User;
    }
}