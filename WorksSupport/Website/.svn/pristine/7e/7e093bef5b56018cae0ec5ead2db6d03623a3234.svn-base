import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let worksService = {
    getWorks: getWorks,
    getWorksBy: getWorksBy,
    create: create,
    update: update,
    delete: remove,
    get: get,
    getWork_TypePeriod: getWork_TypePeriod,
    getWorkFlow:getWorkFlow,
    getProcessUser:getProcessUser,
    getWorkByWorkGroupId: getWorkByWorkGroupId,
    InsertProcessUser_NextStep:InsertProcessUser_NextStep,
    InsertWS_Member: InsertWS_Member,
    getWorksInvole:getWorksInvole,
    getWorksInvoleBy:getWorksInvoleBy,
    createAddWorkInvole:createAddWorkInvole,
    getWorksInvoleAll:getWorksInvoleAll,
    removeWorkInvole:removeWorkInvole,
    getAllWorksInvole:getAllWorksInvole,
    getWorkDetail:getWorkDetail,
    saveWorksProcess:saveWorksProcess,
    getWorkPermisstionByGroupId:getWorkPermisstionByGroupId,
    getQuanlitiesByWorkId:getQuanlitiesByWorkId,
    getWorkSupportDetail:getWorkSupportDetail,
    deleteAttachment:deleteAttachment
};
export default worksService;

function getWorks(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport");
    return connector.get(url);
}
// Load WorksSupportTypePeriod 
function getWork_TypePeriod(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/TypePeriod");
    return connector.get(url);
}
function getWorkByWorkGroupId(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/loadwork/",id);
    return connector.get(url);
}
function getWorkDetail(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/works/",id);
    return connector.get(url);
}
// Search data by keyWord and isDeleleted
function getWorksBy(worksGroupId: any, keyWord: any, pageIndex: number, pageSize: number, user: string): Promise {
    let connector = window.ioc.resolve("IConnector");
    // cunstructer param to search.
    let worksGroupID = "?WorksGroupId=" + worksGroupId;
    let keySearch = "&Keywords=" + keyWord;
    let index = "&PageIndex=" + pageIndex;
    let size = "&PageSize=" + pageSize;
    let User = "&User=" + user;

    let url = String.format("{0}{1}{2}{3}{4}{5}{6}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/WorksSupport/Search"
        , worksGroupID
        , keySearch
        , index
        , size
        , User);
    return connector.get(url);
}
// get workflow by id works
function getWorkFlow(id: any, user: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}/{3}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/loadStep/",id, user);
    return connector.get(url);
}

function get(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}
// Load ProcessUser by WorksSupportStepId
function getProcessUser(WorkId:any, StepId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}/{2}/{3}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/getProcessMember",WorkId,StepId);
    return connector.get(url);
}

function create(role: any): Promise {    
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/insertBy" );
    return connector.post(url, role);
}

function update(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/works/insertBy");
    return connector.post(url, model);
}

function remove(Id : any , User: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/Del");
    return connector.post(url, {Id,User});
}
// Chon nguoi xu ly cho buoc xu ly tiep theo
function InsertProcessUser_NextStep(role: any): Promise {    
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/insertNextStep" );
    return connector.post(url, role);
}
// Chon thanh vien cho cong viec can ho tro
function InsertWS_Member(role: any): Promise {    
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport_Member/insertMember" );
    return connector.post(url, role);
}
// Load list Project on Works Invole
function getWorksInvole(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl,"v2/ProjectMember/projectInvole");
    return connector.get(url);
}
// Search Works Invole by keyworks
function getWorksInvoleBy(projectId:string,keyWord:string,typeSearch: number,startDate: Date,endDate: Date, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
        let url = String.format("{0}{1}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/works/Search"); 
    return connector.post(url,{ProjectIds:projectId,Keywords:keyWord,TypeSearch:typeSearch,StartDate:startDate,EndDate:endDate,PageIndex:pageIndex,PageSize:pageSize});
}
// Add Works Invole on workssupport
function createAddWorkInvole(role: any): Promise {    
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/insertBy" );
    return connector.post(url, role);
}
// load list works invole
function getWorksInvoleAll(objWork: object): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/searchInvole");
    return connector.post(url, objWork);
}
// Delete works invole data
function removeWorkInvole(User: any, Id:number): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/Del");
    return connector.post(url, {User,Id});
}
function getAllWorksInvole(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works");
    return connector.get(url);
}
function saveWorksProcess(model: any): Promise{
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/saveWorksProcess");
    return connector.post(url, model);
}
function getWorkPermisstionByGroupId(User: any, Id:number): Promise{
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}/{2}/{3}", configHelper.getAppConfig().api.baseUrl, "v2/works/getWorksPermission",User,Id);
    return connector.get(url);
}
function getQuanlitiesByWorkId(Id:any): Promise{
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}/{2}", configHelper.getAppConfig().api.baseUrl, "v2/Qualities/loadQuanlities/",Id);
    return connector.get(url);
}
function getWorkSupportDetail(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/works/detailWorks/",id);
    return connector.get(url);
}
function deleteAttachment(User: any, AttachmentId:number): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/deleteWorkSupportAttachment");
    return connector.post(url, {User,AttachmentId});
}