import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let memberService = {
    getMembers: getMembers,
    getMemberBy: getMemberBy,
    create: create,
    createMenber: createMenber,
    update: update,
    delete: remove,
    get: get,
    getByIdAndName: getByIdAndName,
    getMembersByDepartment: getMembersByDepartment,
    getAllDepartment: getAllDepartment,
    getMembersActived: getMembersActived,
    getMemberByActived: getMemberByActived,
    checkRoleIsUsed: checkRoleIsUsed
};

export default memberService;
function getMembers(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles");
    return connector.get(url);
}

function getMembersActived(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles/MemberRoleActived");
    return connector.get(url);
}

// Search data by keyWord and isDeleleted
function getMemberBy(keyWord: any, isDelete: number, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    // cunstructer param to search.
    let keySearch = "?Keywords=" + keyWord;
    let isDel = "&IsDeleted=" + isDelete;
    let index = "&PageIndex=" + pageIndex;
    let size = "&PageSize=" + pageSize;

    let url = String.format("{0}{1}{2}{3}{4}{5}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/memberroles/Search"
        , keySearch
        , isDel
        , index
        , size);
    return connector.get(url);
}

function getMemberByActived(keyWord: any, isDelete: number, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    // cunstructer param to search.
    let keySearch = "?Keywords=" + keyWord;
    let isDel = "&IsDeleted=" + isDelete;
    let index = "&PageIndex=" + pageIndex;
    let size = "&PageSize=" + pageSize;

    let url = String.format("{0}{1}{2}{3}{4}{5}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/memberroles/SearchActived"
        , keySearch
        , isDel
        , index
        , size);
    return connector.get(url);
}

// Get data by id
function get(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles/", id);
    return connector.get(url);
}

// Create new data
function create(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}", configHelper.getAppConfig().api.baseUrl);
    return connector.post(url, role);
}

function createMenber(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/memberroles/insertBy");
    return connector.post(url, model);
}

// Update data
function update(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/memberroles/insertBy");
    return connector.post(url, model);
}

// Delete data
function remove(DeleteModel: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles/del");
    return connector.post(url, DeleteModel);
}
// Get WorksFlow by Id and name
function getByIdAndName(id: any, keyWork: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let strId: any = "?Id=" + id;
    let strKeyWork: any = "&KeyWork=" + keyWork;
    let url = String.format("{0}{1}{2}{3}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/getworksflowbyidandname",
        strId, strKeyWork
    );
    return connector.get(url);
}
function getMembersByDepartment(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles/GetDepartment");
    return connector.get(url);
}
function getAllDepartment() {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember/Department");
    return connector.get(url);
}
function checkRoleIsUsed(worksSupportMemberRoleId: any) {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}/{2}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles/CheckRoleIsUsed", worksSupportMemberRoleId);
    return connector.get(url);
}