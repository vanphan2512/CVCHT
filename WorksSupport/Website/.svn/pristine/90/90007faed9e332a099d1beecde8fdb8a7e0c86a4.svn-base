import { Component, Input, Output, EventEmitter } from "angular2/core";
import { BaseControl } from "../../models/ui";
import { ValidationDirective } from "../../directive";
@Component({
    selector: "form-datetime",
    templateUrl: "app/common/directives/form/formDatetime.html",
    directives: [ValidationDirective]
})
export class FormDatetime extends BaseControl {
    @Input() labelText: string = String.empty;
    @Input() model: any;
    @Input() readOnly: boolean = false;
}