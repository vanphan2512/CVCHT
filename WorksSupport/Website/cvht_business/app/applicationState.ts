import { IApplicationState } from "./common/models/app/iapplicationState";
import { EventManager } from "./common/eventManager";
import { LoadingIndicatorEvent, ApplicationStateEvent } from "./common/event";
import { ResourceHelper } from "./common/helpers/resourceHelper";
import configHelper from "./common/helpers/configHelper";
import { Injector } from "angular2/core";
import { Router } from "angular2/router";

export class ApplicationState implements IApplicationState {
    private eventManager: EventManager;
    private injector: Injector = null;
    public registerEvents(): void {
        let self: ApplicationState = this;
        self.eventManager = window.ioc.resolve("IEventManager");
        self.eventManager.subscribe(ApplicationStateEvent.Init, (args: any) => self.onApplicationInit(args));
        self.eventManager.subscribe(ApplicationStateEvent.BeforeReady, (args: any) => self.onApplicationBeforeReady(args));
        self.eventManager.subscribe(ApplicationStateEvent.Ready, (args: any) => self.onApplicationReady(args));
        self.eventManager.subscribe(ApplicationStateEvent.UnAuthorizeRequest, (routeInstruction: any) => self.onUnAuthorizeRequest(routeInstruction));
        self.eventManager.subscribe(ApplicationStateEvent.Unload, (args: any) => self.onApplicationUnload(args));
    }
    public setInjector(injector: Injector): void {
        this.injector = injector;
    }
    public getInjector(): Injector {
        return this.injector;
    }
    private onUnAuthorizeRequest(routeInstruction: any) {
        this.eventManager.publish(LoadingIndicatorEvent.Show);
        let router: Router = this.injector.get(Router);
        window.location.href= configHelper.getAppConfig().loginUrl;
    }
    private onApplicationInit(args: any) {
        this.eventManager.publish(LoadingIndicatorEvent.Show);
        /* Consider to move to event */
        document.title = configHelper.getAppConfig().app.name;
    }
    private onApplicationBeforeReady(args: any) {
        this.eventManager.publish(LoadingIndicatorEvent.Show);
    }
    private onApplicationReady(args: any) {
        this.eventManager.publish(LoadingIndicatorEvent.Show);
    }
    private onApplicationUnload(args: any) {
        this.eventManager.publish(LoadingIndicatorEvent.Show);
    }
}
export class ApplicationStateFactory {
    private static appStateInstance: IApplicationState = null;
    public static getInstance(): IApplicationState {
        if (!ApplicationStateFactory.appStateInstance) {
            ApplicationStateFactory.appStateInstance = new ApplicationState();
        }
        return ApplicationStateFactory.appStateInstance;
    }
}