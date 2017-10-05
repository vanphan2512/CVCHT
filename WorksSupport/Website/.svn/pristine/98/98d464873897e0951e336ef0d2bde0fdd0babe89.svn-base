import {Component, Input, Output, EventEmitter} from "angular2/core";
import {BaseControl} from "../../models/ui";
import guidHelper from "../../helpers/guidHelper";
@Component({
    selector: "form-toggle",
    templateUrl: "app/common/directives/form/toggle.html"
})
export class FormToggle extends BaseControl {
    public id: string = String.format("toggle-{0}", guidHelper.create());
    @Input() model: any = null;
    @Output() modelChange: EventEmitter<any> = new EventEmitter<any>();
    @Input() options: Array<any> = [];

    public getOptions() {
        let self: FormToggle = this;
        this.options.forEach(function (option: any) {
            option.active = (!self.model && option.active === true) || self.model == option.value ? true : false;
        });
        return this.options;
    }
    public onItemClicked(item: any) {
        this.modelChange.emit(item.value);
        this.reset(item.value);
    }
    private reset(value: any) {
        this.options.forEach(function (option: any) {
            option.active = option.value === value;
        });
    }
}