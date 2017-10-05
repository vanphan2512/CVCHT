import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let projectService = {
    getProject: getProject,
    getProjecBy: getProjecBy,
    getAllWorksType: getAllWorksType,
    getAllMember: getAllMember,
    getAllDepartment: getAllDepartment,
    getUserBy: getUserBy,
    create: create,
    update: update,
    delete: remove,
    get: get,
    insertMember: insertMember,
    getProjectMember: getProjectMember,
    UpdateProject: UpdateProject
};
export default projectService;

function getProject(user: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/ProjectMember?user={1}", configHelper.getAppConfig().api.baseUrl, user);
    return connector.get(url);
}
// Search data by keyWord and isDeleleted
function getProjecBy(keyWord: any, isDelete: number, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    // cunstructer param to search.
    let keySearch = "?Keywords=" + keyWord;
    let isDel = "&IsDeleted=" + isDelete;
    let index = "&PageIndex=" + pageIndex;
    let size = "&PageSize=" + pageSize;

    let url = String.format("{0}{1}{2}{3}{4}{5}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/ProjectMember/Searchkey"
        , keySearch
        , isDel
        , index
        , size);
    return connector.get(url);
}

function get(Id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/ProjectMember/{1}", configHelper.getAppConfig().api.baseUrl, Id);
    return connector.get(url);
}

function create(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/insertBy");
    return connector.post(url, role);
}

function update(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/insertBy");
    return connector.post(url, model);
}

function remove(deleteModel: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/Del");
    return connector.post(url, deleteModel);
}
function getAllWorksType() {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/workssupporttypes");
    return connector.get(url);
}
function getAllMember() {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "/v2/memberroles");
    return connector.get(url);

}
function getAllDepartment() {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/Department");
    return connector.get(url);

}
function getUserBy(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/SearchUser");
    return connector.post(url, role);
}
function insertMember(lstObject: any) {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/insertMember");
    return connector.post(url, lstObject);
}
// lay thanh vien cua du an khi edit du an
function getProjectMember(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/ProjectMember/projectid?id={1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}
function UpdateProject(lstObject: any) {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/UpdateProject");
    return connector.post(url, lstObject);
}