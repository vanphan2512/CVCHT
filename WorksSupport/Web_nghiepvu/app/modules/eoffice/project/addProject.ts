import { BasePage } from "../../../common/models/ui";
import { Router, RouteParams } from "angular2/router";
import { Http } from "angular2/http";
import { Component } from "angular2/core";
import { AddProjectModel } from "./addProjectModel";
import { Page, Form, FormTextInput, FormFooter, FormTextArea } from "../../../common/directive";
import { ValidationDirective } from "../../../common/directive";
import projectService from "../_share/services/projectService";
import { FormMode } from "../../../common/enum";
import route from "../_share/config/route";
@Component({
    templateUrl: "app/modules/eoffice/project/addProject.html",
    directives: [Page, Form, FormTextInput, FormFooter, FormTextArea]
})
export class AddProject extends BasePage {
    public model: AddProjectModel = new AddProjectModel();
    private router: Router;
    private mode: FormMode = FormMode.AddNew;
    constructor(router: Router, routeParams: RouteParams) {
        super();
        let self: AddProject = this;
        self.router = router;
    }
    public onSaveClicked(event: any): void {
        let self: AddProject = this;
        if (self.mode === FormMode.AddNew) {
            projectService.create(this.model).then(function () {
                self.router.navigate([route.project.projects.name]);
            });
            return;
        }
        projectService.update(this.model).then(function () {
            self.router.navigate([route.project.projects.name]);
        });
    }
    public onCancelClicked(event: any): void {
        let self: AddProject = this;
        self.router.navigate([route.project.projects.name]);
    }
}