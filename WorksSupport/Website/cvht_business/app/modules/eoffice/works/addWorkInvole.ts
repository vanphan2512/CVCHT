export class WorksInvoleModel {
    public WorksSupportId: number = 0;
    public WorksId: Array<number> = [];
    public Note: Array<any> = [];
    constructor(WorksSupportId: number, WorksId: Array<any> ,Note: Array<any>) {
        this.WorksSupportId = WorksSupportId;
        this.WorksId = WorksId;
        this.Note = Note;    
    }
}