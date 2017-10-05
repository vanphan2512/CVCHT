import { Component, Input, Output, EventEmitter } from "angular2/core";
import { BaseControl } from "../../models/ui";
import guidHelper from "../../helpers/guidHelper";
@Component({
    selector: "form-datepicker",
    templateUrl: "app/common/directives/form/datePicker.html",
    host: {
        "[value]": "model"
    }
})
export class FormDatePicker extends BaseControl {
    public id: string = String.format("date-picker-{0}", guidHelper.create());
    @Input() model: any;
    @Output() modelChange: EventEmitter<any> = new EventEmitter<any>();
    @Output() onValueChanged = new EventEmitter();
    protected onReady() {
        let self: FormDatePicker = this;
        let year: number = 0;
        let month: string;
        let day: string;
        window.jQuery("#" + this.id).daterangepicker({
            singleDatePicker: true,
            locale: {
                format: 'DD/MM/YYYY'
            }
        }, function (selectedValue: Date) {
            let objDate = new Date(selectedValue);
            month = objDate.getMonth() + 1 < 10 ? "0" + (objDate.getMonth() + 1) : (objDate.getMonth() + 1).toString();
            day = objDate.getDate()  < 10 ? "0" + objDate.getDate(): objDate.getDate().toString() ;
            self.modelChange.emit(day + "/" + month +"/" + objDate.getFullYear());
        });

    }
}