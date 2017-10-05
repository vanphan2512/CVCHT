export class ProcessUserModel {
    public WorksSupportId: number = 0;
    public NextWorksSupportStepId: number = 0;
    public ProcessUser: string = "";
    public Note: string = "";
    public UpdatedUser: any = "";
    constructor(WorksSupportId: number, NextWorksSupportStepId: number, UpdatedUser:any, ProcessUser: string, Note: string) {
        this.WorksSupportId = WorksSupportId;
        this.NextWorksSupportStepId = NextWorksSupportStepId;
        this.ProcessUser = ProcessUser;
        this.Note = Note; 
        this.UpdatedUser = UpdatedUser;       
    }

}