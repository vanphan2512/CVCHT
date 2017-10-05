import {Component, Input, Output, EventEmitter} from "angular2/core";
import {BaseControl} from "../../models/ui";
import {ValidationDirective} from "../../directive";
@Component({
    selector: "form-email-input",
    templateUrl: "app/common/directives/form/formEmailInput.html",
    directives: [ValidationDirective]
})
export class FormEmailInput extends BaseControl {
    @Input() labelText: string = String.empty;
    @Input() placeHolderText: string = String.empty;
    @Input() readOnly: boolean = false;
    @Input() validation: Array<string> = [];
    @Input() model: any;
    @Output() modelChange = new EventEmitter();
    public onValueChanged(evt: any) {
        this.modelChange.emit(evt.target.value);
    }
}