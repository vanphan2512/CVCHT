import { KeyNamePair } from "../../../common/models/keyNamePair";
export class AddWorksModel {
    public Id: number = 0;
    public WorksSupportName: string = "";
    public Content: string = "";
    public ExpectedCompletedDate: Date;
    public WorksSupportPriorityId: number = 0;
    public WorksSupportGroupId: number = 0;
    public PeriodId: number = 0;
    public CreateDate: Date;
    public User: string = "";
    public DepartmentId: number = 0;
    public Keywords: string = String.empty;
    public import(item: any) {
        if (!item) { return; }
        this.Id = item.WorksSupportId
        this.WorksSupportName = item.WorksSupportName;
        this.Content = item.Content;
        this.ExpectedCompletedDate = item.ExpectedCompletedDate;
        this.WorksSupportPriorityId = item.WorksSupportPriorityId;
        this.WorksSupportGroupId = item.WorksSupportGroupId;
        this.PeriodId = item.PeriodId;
        this.CreateDate = item.CreateDate;
        this.User = item.User;
    }

    constructor(id: number, name: string) {
        this.DepartmentId = id;
        this.Keywords = name;
    }
}