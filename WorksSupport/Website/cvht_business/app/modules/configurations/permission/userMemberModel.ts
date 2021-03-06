export class UserMemberModel {
    public WorksSupportTypeId: number = 0;
    public UserName: string = String.empty;
    public FullName: string = String.empty;
    public IsCanAddProject: boolean = false;
    public IsCanEditProject: boolean = false;
    public IsCanDeleteProject: boolean = false;
    public IsCanViewProject: boolean = false;
    constructor(WorksSupportTypeId: number,
        UserName: string, FullName: string, IsCanAddProject: boolean,
        IsCanEditProject: boolean, IsCanDeleteProject: boolean, 
        IsCanViewProject: boolean) {
            this.WorksSupportTypeId = WorksSupportTypeId,
            this.UserName = UserName,
            this.FullName = FullName,
            this.IsCanAddProject = IsCanAddProject,
            this.IsCanEditProject = IsCanEditProject,
            this.IsCanDeleteProject = IsCanDeleteProject,
            this.IsCanViewProject = IsCanViewProject
    }
}