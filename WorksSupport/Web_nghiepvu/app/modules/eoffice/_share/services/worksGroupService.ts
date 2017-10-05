import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let worksGroupService = {
    getWorksGroups: getWorksGroups,
    delete: remove,
    create: create,
    edit: edit,
    getAllProjectByMemberId: getAllProjectByMemberId,
    getRoleByProjectId: getRoleByProjectId,
    getGroupMember: getGroupMember,
    deleteGroupMember: deleteGroupMember,
    getAllMemberByProjectId: getAllMemberByProjectId,
    getWorksGroupsByGroupId: getWorksGroupsByGroupId,
    getWorksGroupMember: getWorksGroupMember,
    getWorksGroupRole: getWorksGroupRole,
    checkUserProcess: checkUserProcess,
    getAllProjectIsActivedByUser: getAllProjectIsActivedByUser,
    getWorksGroupsByKeyWords: getWorksGroupsByKeyWords
};
export default worksGroupService;

// get work group by projectId
function getWorksGroups(projectId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/GetworksGroupByProjectId/",projectId);
    return connector.get(url);
}
// get work group by groupid
function getWorksGroupsByGroupId(groupid: any): Promise {
    let connector = window.ioc.resolve("IConnector");
     let url = String.format("{0}v2/worksgroup/groupId?worksGroupId={1}", configHelper.getAppConfig().api.baseUrl, groupid);
    return connector.get(url);
}
function remove(id: any, user: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}/{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/Del");
    return connector.post(url, { id, user });
}
function create(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/insertBy");
    return connector.post(url, role);
}
function edit(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/updateWorkGroup");
    return connector.post(url, role);
}

function getRoleByProjectId(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/memberroles/GetRole/",id);
    return connector.get(url);
}

function getAllProjectByMemberId(memberId: string): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/getProjectsIsActive/", memberId);
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

function getAllMemberByProjectId(projectId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/GetAllMemberByProjectId/", projectId);
    return connector.get(url);
}
// lay thanh vien cua nhom cong viec khi edit du an
function getWorksGroupMember(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/worksgroup/getMemberByGroupId/{1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}
// get work group by projectId
function getWorksGroupRole(projectId: any, workTypeId: any, userName: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}/{3}/{4}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/GetworksGroupRole/", projectId, workTypeId, userName);
    return connector.get(url);
}
function checkUserProcess(userName: any, workGroupId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/worksgroup/CheckUserProcess/{1}/{2}", configHelper.getAppConfig().api.baseUrl, userName, workGroupId);
    return connector.get(url);
}
function getAllProjectIsActivedByUser(user: string): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/getProjectsIsActiveByUser/", user);
    return connector.get(url);
}
function getWorksGroupsByKeyWords(projectId: any, keyWords: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}/{3}", configHelper.getAppConfig().api.baseUrl, "v2/worksgroup/GetworksGroupByProjectId/", projectId, keyWords);
    return connector.get(url);
}