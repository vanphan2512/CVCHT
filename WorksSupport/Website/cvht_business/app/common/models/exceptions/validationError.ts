export class ValidationError {
    public key: string = undefined;
    public params: any = {};
    public msg: string = "";
    constructor(key: string, params: any, msg: any) {
        this.key = key;
        this.params = params;
        this.msg = msg;
    }
}