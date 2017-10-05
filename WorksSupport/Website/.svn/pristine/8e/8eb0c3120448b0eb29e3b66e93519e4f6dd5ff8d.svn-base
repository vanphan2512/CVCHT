import { Component } from "angular2/core";
import { UserLoginModel } from "./userLoginModel";
import { Router, RouteParams } from "angular2/router";
import userService from "../_share/services/userService";
import configHelper from "../../../common/helpers/configHelper";
import authService from "../../../common/services/authService";
import { AuthenticatedEvent, CommonEvent } from "../../../common/event";
import { ValidationException } from "../../../common/models/exception";
import { BasePage } from "../../../common/models/ui";
import { ValidationDirective } from "../../../common/directive";

@Component({
    selector: "user-login",
    templateUrl: "app/modules/registration/users/userLogin.html",
    directives: [ValidationDirective]
})
export class UserLogin extends BasePage {
    private router: Router;
    private ErrorLogin: string = String.empty;
    constructor(router: Router, routeParams: RouteParams) {
        super();
        this.router = router;
        let user = "";
        let pass = "";
        if (!!routeParams.get("u")) {
            user = routeParams.get("u");
        }
        if (!!routeParams.get("p")) {
            pass = routeParams.get("p");
        }
        this.login(user, pass);
    }

    private login(user: any, password: any) {
        let self: UserLogin = this;
        let userModel: UserLoginModel = new UserLoginModel(user, password);
        userService.signin(userModel).then(function (token: any) {
            authService.setAuth(token);
            self.eventManager.publish(AuthenticatedEvent.AuthenticationChanged, true);
            self.router.navigate([configHelper.getAppConfig().defaultUrl]);
            localStorage.setItem(configHelper.getAppConfig().defaultUrl + "_notify", "false");
            return true;
        }).error(function (error: Array<any>) {
            if (error.length > 0) {
                for (let i = 0; i < error.length; i++) {
                    if (error[i].Key === "username") {
                        break;
                    }
                }
            }
            return false;
        });
    }
    // public onSignInClicked(event: any) {
    //     let self: UserLogin = this;
    //     if (!this.model.isValid()) { return; }
    //     userService.signin(this.model).then(function (token: any) {
    //         authService.setAuth(token);
    //         self.eventManager.publish(AuthenticatedEvent.AuthenticationChanged, true);
    //         self.router.navigate([configHelper.getAppConfig().defaultUrl]);
    //         localStorage.setItem(configHelper.getAppConfig().defaultUrl + "_notify", "false");
    //         return true;

    //     }).error(function (error: Array<any>) {
    //         if (error.length > 0) {
    //             for (let i = 0; i < error.length; i++) {
    //                 if (error[i].Key === "username") {
    //                     self.ErrorLogin = error[i].Message;
    //                     break;
    //                 }
    //             }
    //         }
    //         return false;
    //     });
    // }
    /**
     * Writen by    : nguyen van huan
     * Create date  : 21.08.2017
     * Description  : clear validation
     */
    private validateLogin() {
        this.ErrorLogin = String.empty;
    }
}