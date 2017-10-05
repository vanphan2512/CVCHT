import { Component, Input, Output, EventEmitter } from "angular2/core";
import { BaseControl } from "../../models/ui";
import { ValidationDirective } from "../../directive";
@Component({
    selector: "form-number-input",
    templateUrl: "app/common/directives/form/formNumberInput.html",
    directives: [ValidationDirective]
})
export class FormNumberInput extends BaseControl {
    @Input() labelText: string = String.empty;
    @Input() placeHolderText: string = String.empty;
    @Input() validation: Array<string> = [];
    @Input() model: any;
    @Input() readOnly: boolean = false;
    @Input() min: number = 0;
    @Input() max: number = 9999;
    @Output() modelChange = new EventEmitter();
    public onValueChanged(evt: any) {
        this.modelChange.emit(evt.target.value);
    }
}