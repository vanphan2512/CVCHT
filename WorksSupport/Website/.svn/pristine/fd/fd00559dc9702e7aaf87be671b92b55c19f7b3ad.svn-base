import { Component, Input, Output, EventEmitter } from "angular2/core";
import { BaseControl } from "../../models/ui";
import { FormToggle } from "../../directive";
import {FormSelectType} from "../../enum";
@Component({
    selector: "select-options",
    templateUrl: "app/common/directives/form/selectOptions.html",
    directives: [FormToggle]
})
export class SelectOptions extends BaseControl {
    @Input() labelText: string = String.empty;
    @Input() model: any;
    @Output() modelChange = new EventEmitter();
    @Input() type: FormSelectType = FormSelectType.SingleButton;
    @Input() dataSource: any = {};
    @Input() textPattern: string = "{0}";
    public FormSelectType: any = FormSelectType;
    public onModelChanged(value: any) {
        this.model = value;
        this.modelChange.emit(value);
    }
    public getOptions() {
        let options: Array<any> = [];
        if (!this.dataSource || this.dataSource.length <= 0) { return options; }
        for (let pro in this.dataSource) {
            if (isNaN(parseInt(pro)) || !this.dataSource.hasOwnProperty(pro)) { continue; }
            options.push({ value: pro, text: this.i18nHelper.resolve(String.format(this.textPattern, this.dataSource[pro].firstCharToLower())) });
        }
        options[0].active = true;
        return options;
    }
}