import { IoCInstanceType } from "../common/models/ioc/enum";
import { RESTConnector } from "../common/connectors/restConnector";
import { EventManager } from "../common/EventManager";
import { ResourceHelper } from "../common/helpers/resourceHelper";
import { ApplicationState, ApplicationStateFactory } from "../applicationState";
let iocRegistrations: any = [
    { name: "IConnector", instanceOf: RESTConnector, type: IoCInstanceType.Singleton },
    { name: "IEventManager", instanceOf: EventManager, type: IoCInstanceType.Singleton },
    { name: "IResource", instanceOf: ResourceHelper, type: IoCInstanceType.Singleton },
    { name: "IApplicationState", instance: ApplicationStateFactory.getInstance(), type: IoCInstanceType.Singleton },
];
export default iocRegistrations;