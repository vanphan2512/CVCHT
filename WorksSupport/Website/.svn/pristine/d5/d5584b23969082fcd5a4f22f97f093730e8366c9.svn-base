import { Component, Input, OnInit, Output, EventEmitter } from "angular2/core";
import { ValidationError, ValidationException } from "../../../../models/exception";
import { ResourceHelper } from "../../../../helpers/resourceHelper";
import { CommonEvent } from "../../../../event";
import regexHelper from "../../../../helpers/regexHelper";

@Component({
    selector: "error-new-message",
    templateUrl: "./app/common/layouts/default/directives/common/newErrorMessage.html"
})

export class ErrorNewMessage {
    public errors: Array<any> = [];
    @Input() pattern: string = "*";
    @Output() onErrorEvent = new EventEmitter<any>();
    constructor() {
        let eventManager: any = window.ioc.resolve("IEventManager");
        eventManager.subscribe(CommonEvent.ValidationFail, (validation: ValidationException) => { this.onError(validation); });
    }
    public ResetError(){
         let self: ErrorNewMessage = this;
         let errors: Array<ValidationError> = [];
         self.errors = errors;
    }
    private onError(validation: ValidationException) {
        let self: ErrorNewMessage = this;
        let resourceHelper: ResourceHelper = window.ioc.resolve("IResource");
        let errors: Array<ValidationError> = [];
        if (validation.errors.length > 0) {
            validation.errors.forEach(function (error: ValidationError) {                
                errors.push(error);
            });
        }
        self.errors = errors;
        self.onErrorEvent.emit(errors);
    }
}