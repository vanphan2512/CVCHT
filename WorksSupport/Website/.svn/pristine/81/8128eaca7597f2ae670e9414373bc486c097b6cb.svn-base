import { Component, Input, Output, EventEmitter } from "angular2/core";
import { BaseControl } from "../../models/ui";
import { FormTextInput } from "./formTextInput";
@Component({
    selector: "form-label",
    templateUrl: "app/common/directives/form/formLabel.html",
    directives: [FormTextInput]
})
export class FormLabel extends BaseControl {
    @Input() labelText: string = String.empty;
    @Input() model: any;
}