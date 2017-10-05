import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let worksGroupService = {
    getWorksGroups: getWorksGroups,
    delete: remove,
    create : create,
    edit: edit,
    getAllProjectByMemberId: getAllProjectByMemberId,
    getRoleByProjectId: getRoleByProjectId,
    getGroupMember: getGroupMember,
    deleteGroupMember: deleteGroupMember,
    getAllMemberByProjectId: getAllMemberByProjectId
};
export default worksGroupService;
// get work group by id
function getWorksGroups(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/",id);
    return connector.get(url);
}
function remove(id:any, user: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}/{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/Del");
    return connector.post(url,{id,user});
}
function create(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/insertBy" );
    return connector.post(url, role);
}
function edit(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/Update" );
    return connector.post(url, role);
}

function getRoleByProjectId(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles/GetRole");
    return connector.post(url, role);
}

function getAllProjectByMemberId(memberId: string): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/ProjectMember?user=", memberId);
    return connector.get(url);
}
function getGroupMember(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/GetMembers");
    return connector.post(url, role);
}

function deleteGroupMember(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/DeleteMember");
    return connector.post(url, role);
}

function getAllMemberByProjectId(projectId: string): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/GetAllMemberByProjectId?projectId=", projectId);
    return connector.get(url);
}

