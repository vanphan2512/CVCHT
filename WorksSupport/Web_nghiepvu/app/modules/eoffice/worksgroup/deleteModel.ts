export class DeleteModel {
    public Id: string = String.empty;
    public User: string = String.empty;
    constructor(id: string, User: string) {
        this.Id = id;
        this.User = User;
    }
}