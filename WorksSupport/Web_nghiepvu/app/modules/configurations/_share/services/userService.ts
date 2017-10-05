import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let userService = {
    getUsers: getUsers
};
export default userService;
function getUsers(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/loadalluser");
    return connector.get(url);
}
