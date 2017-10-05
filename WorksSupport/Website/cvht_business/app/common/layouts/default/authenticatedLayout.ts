import { Input, provide, Component } from "angular2/core";
import { Http } from "angular2/http";
import { Router, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from "angular2/router";
import { UserQuickProfileInfo } from "./directives/all";
import { ProfileDisplayMode } from "../../models/layout";
import { ErrorMessage } from "../../../common/layouts/default/directives/common/errorMessage";
import { BaseApplication } from "../../models/ui";
import route from "../../../modules/eoffice/_share/config/route";

@Component({
    selector: "default-authenticated-layout",
    templateUrl: "app/common/layouts/default/authenticatedLayout.html",
    directives: [
        ROUTER_DIRECTIVES, UserQuickProfileInfo, ErrorMessage
    ]
})

export class DefaultAuthenticatedLayout extends BaseApplication {
    @Input() isAuthenticated: boolean;
    private router: Router;
    public ProfileDisplayMode: any = ProfileDisplayMode;
    constructor(http: Http, router: Router) {
        super(http);
        this.router = router;
    }
    protected onReady() {
        super.onReady();
    }

    private redirectToProject()
    {
        this.router.navigate([route.project.projects.name]);
    }
}
