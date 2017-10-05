import configHelper from "../../common/helpers/configHelper";
import authService from "../services/authService";
import {Promise} from "../../common/models/promise";

let statusService = {
    getStatus: getStatus
};
export default statusService;

function getStatus(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/status/checkStatus");
    return connector.get(url);
}
