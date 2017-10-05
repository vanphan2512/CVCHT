export class CopyPermissionModel {
    public FromWorksTypeId: string = String.empty;
    public ToWorksTypeId: string = String.empty;
    public User: string = String.empty;
    constructor(formWorksTypId: string, toWorksTypId: string ,  user: string) {
        this.FromWorksTypeId = formWorksTypId;
        this.ToWorksTypeId = toWorksTypId;
        this.User = user;
    }
}