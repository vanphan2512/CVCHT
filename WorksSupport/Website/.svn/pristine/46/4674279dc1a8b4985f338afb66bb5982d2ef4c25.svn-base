import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let workDetailService = {
  removeInvole:removeInvole,
  addQuality:addQuality,
  addWorksInvoleForDetail:addWorksInvoleForDetail,
  deleteSolution:deleteSolution,
  addSolution:addSolution,
  addComment:addComment,
  getWorksNextStepPermission:getWorksNextStepPermission,
  deleteSolutionAttachment:deleteSolutionAttachment
};
export default workDetailService;

function removeInvole(WorkInvoleId : any , WorksId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/deleteWorksInvole");
    return connector.post(url, {WorkInvoleId,WorksId});
}
function addQuality(model : any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/addQuality");
    return connector.post(url, model);
}
function addWorksInvoleForDetail(WorksSupportId : any, ListWorkId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/addWorksInvoleForDetail");
    return connector.post(url, {WorksSupportId,ListWorkId});
}
function deleteSolution(WorksSupportId : any, User: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/deleteSolution");
    return connector.post(url, {WorksSupportId,User});
}
function addSolution(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/addSolution");
    return connector.post(url, model);
}
function addComment(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/addComment");
    return connector.post(url, model);
}
function getWorksNextStepPermission(user: any, id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}/{2}/{3}", configHelper.getAppConfig().api.baseUrl, "v2/works/getWorksNextStepPermission",user,id);
    return connector.get(url);
}
function deleteSolutionAttachment(AttachmentId : any, User: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/deleteSolutionAttachment");
    return connector.post(url, {AttachmentId,User});
}