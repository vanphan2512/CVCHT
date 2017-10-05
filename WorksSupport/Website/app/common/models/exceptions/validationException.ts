import {ValidationError} from "./validationError";
import {CommonEvent} from "../../event";
export class ValidationException {
    constructor(key: string = "", params: any = {}, msg: any = "") {
        if (key !== "") {
            this.add(key, params,msg);
        }
    }
    public errors: Array<ValidationError> = [];
    public add(key: string, params: any = {}, msg: any = ""): any {
        this.errors.push(new ValidationError(key, params, msg));
    }
    public hasError(): boolean {
        return this.errors.length > 0;
    }
    public throwIfHasError(): void {
        window.ioc.resolve("IEventManager").publish(CommonEvent.ValidationFail, this);
    }
}