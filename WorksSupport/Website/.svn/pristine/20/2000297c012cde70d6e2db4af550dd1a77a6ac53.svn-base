import { Component, Input, OnInit, Output, EventEmitter } from "angular2/core";
import { ValidationError, ValidationException } from "../../../../models/exception";
import { ResourceHelper } from "../../../../helpers/resourceHelper";
import { CommonEvent } from "../../../../event";
import regexHelper from "../../../../helpers/regexHelper";

@Component({
    selector: "error-message",
    templateUrl: "./app/common/layouts/default/directives/common/errorMessage.html"
})

export class ErrorMessage {
    public errors: Array<any> = [];
    @Input() pattern: string = "*";
    @Output() onErrorEvent = new EventEmitter<any>();
    constructor() {
        let eventManager: any = window.ioc.resolve("IEventManager");
        eventManager.subscribe(CommonEvent.ValidationFail, (validation: ValidationException) => { this.onError(validation); });
    }
    public ResetError(){
         let self: ErrorMessage = this;
         let errors: Array<ValidationError> = [];
         self.errors = errors;
    }
    private onError(validation: ValidationException) {
        let self: ErrorMessage = this;
        let resourceHelper: ResourceHelper = window.ioc.resolve("IResource");
        let errors: Array<ValidationError> = [];
        if (validation.errors.length > 0) {
            validation.errors.forEach(function (error: ValidationError) {
                //if (!regexHelper.isMatch(self.pattern, error.key)) { return; }
                console.log(String.format("pattern {0}, value: {1}, error: {2}", self.pattern, error.key, error.msg));
                errors.push(error);
            });
        }
        self.errors = errors;
        self.onErrorEvent.emit(errors);
    }
}