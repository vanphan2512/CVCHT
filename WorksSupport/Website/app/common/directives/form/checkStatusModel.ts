import {KeyNamePair} from "../../models/keyNamePair";

export class CheckStatusModel {
    public status: any;
    constructor() {
        this.status = null;
    }
    public setStatus(inputStatus: any) {
        this.status = inputStatus;
    }
}