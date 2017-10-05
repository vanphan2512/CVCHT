import {bootstrap}  from 'angular2/platform/browser';
import { Router, RouteParams } from "angular2/router";
import { Component } from "angular2/core";
import { AddQualityModel } from "./addQualityModel";
import { Page, Form, FormTextInput, FormFooter, FormTextArea } from "../../../common/directive";
import { ValidationDirective } from "../../../common/directive";
import qualityService from "../_share/services/qualityService";
import { FormMode } from "../../../common/enum";
import route from "../_share/config/route";
import { BasePage } from "../../../common/models/ui";
@Component({
    selector: "model",
    templateUrl: "app/modules/configurations/quality/addQuality.html",
    directives: [Page, Form, FormTextInput, FormFooter, FormTextArea]
})
export class AddQuality extends BasePage {
    public model: AddQualityModel = new AddQualityModel();
    private router: Router;
    private mode: FormMode = FormMode.AddNew;
    private itemId: any;
    constructor(router: Router, routeParams: RouteParams) {
        super();
        let self: AddQuality = this;
        self.router = router;
        if (!!routeParams.get("id")) {
            self.mode = FormMode.Edit;
            self.itemId = routeParams.get("id");
            qualityService.get(self.itemId).then(function (item: any) {
                self.model.import(item);
            });
        }
    }
    public onSaveClicked(event: any): void {
        let self: AddQuality = this;
        if (self.mode === FormMode.AddNew) {
            qualityService.create(this.model).then(function () {
                self.router.navigate([route.quality.qualities.name]);
            });
            return;
        }
        qualityService.update(this.model).then(function () {
            self.router.navigate([route.quality.qualities.name]);

        });
    }

    public onCancelClicked(event: any): void {
        let self: AddQuality = this;
        self.router.navigate([route.quality.qualities.name]);
    }
}