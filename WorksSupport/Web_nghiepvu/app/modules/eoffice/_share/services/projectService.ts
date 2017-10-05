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
    UpdateProject: UpdateProject,
    getMemberRoleByTypeId: getMemberRoleByTypeId,
    checkUserProcess: checkUserProcess
};
export default projectService;

function getProject(users: any, keyWord: any, pageIndex: number, pageSize: number): Promise {

    let connector = window.ioc.resolve("IConnector");
    let user = "user?user=" + users;
    let keySearch = "&keyWork=" + keyWord;
    let index = "&pageIndex=" + -1;
    let size = "&pageSize=" + 500;
    let url = String.format("{0}{1}{2}{3}{4}{5}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/ProjectMember/getProjects/"
        , user
        , keySearch
        , index
        , size);
    return connector.get(url);
}
// Search data by keyWord and isDeleleted
function getProjecBy(keyWord: any, isDelete: number, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    // cunstructer param to search.
    let keySearch = "?Keywords=" + keyWord;
    let isDel = "&IsDeleted=" + 0;
    let index = "&PageIndex=" + -1;
    let size = "&PageSize=" + 500;
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
    let url = String.format("{0}v2/ProjectMember/getProject/{1}", configHelper.getAppConfig().api.baseUrl, Id);
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
function getAllWorksType(userName: any) {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/GetWorksSupportType/", userName);
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
    let url = String.format("{0}v2/ProjectMember/getProjectMember/{1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}
function UpdateProject(lstObject: any) {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/UpdateProject");
    return connector.post(url, lstObject);
}
function getMemberRoleByTypeId(worksTypeId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/ProjectMember/memberRoles/{1}", configHelper.getAppConfig().api.baseUrl, worksTypeId);
    return connector.get(url);
}
function checkUserProcess(userName: any, projectId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/ProjectMember/CheckUserProcess/{1}/{2}", configHelper.getAppConfig().api.baseUrl, userName, projectId);
    return connector.get(url);
}