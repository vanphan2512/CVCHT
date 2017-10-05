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
    InsertProcessUser_NextStep:InsertProcessUser_NextStep,
    InsertWS_Member: InsertWS_Member,
    getWorksInvole:getWorksInvole,
    getWorksInvoleBy:getWorksInvoleBy,
    createAddWorkInvole:createAddWorkInvole,
    getWorksInvoleAll:getWorksInvoleAll,
    removeWorkInvole:removeWorkInvole,
    getAllWorksInvole:getAllWorksInvole
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
// Search data by keyWord and isDeleleted
function getWorksBy(keyWord: any, isDelete: number, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    // cunstructer param to search.
    let keySearch = "?Keywords=" + keyWord;
    let isDel = "&IsDeleted=" + isDelete;
    let index = "&PageIndex=" + pageIndex;
    let size = "&PageSize=" + pageSize;

    let url = String.format("{0}{1}{2}{3}{4}{5}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/WorksSupport/Search"
        , keySearch
        , isDel
        , index
        , size);
    return connector.get(url);
}
// get workflow by id works
function getWorkFlow(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}{2}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/",id);
    return connector.get(url);
}


function get(id: any): Promise {
    
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}
// Load ProcessUser by WorksSupportStepId
function getProcessUser(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/WorksSupport_Member/searchByUserId?id={1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}

function create(role: any): Promise {    
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/insertBy" );
    return connector.post(url, role);
}

function update(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport/insertBy");
    return connector.post(url, model);
}

function remove(Id : Array<any> , User: any): Promise {
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
debugger;
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/WorksSupport_Member/insertMember" );
    return connector.post(url, role);
}
// Load list Project on Works Invole
function getWorksInvole(): Promise {
    debugger;
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl,"v2/ProjectMember/projectInvole");
    return connector.get(url);
}
// Search Works Invole by keyworks
function getWorksInvoleBy(projectId:any,keyWord:any,typeSearch: number,startDate: string,endDate: string, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    // cunstructer param to search.
    let project = "?ProjectIds=" + projectId;
    let keySeach = "&Keywords=" + keyWord;
    let typeSearchs = "&TypeSearch=" + typeSearch;
    let startDates = "&StartDate=" + startDate;
    let endDates = "&EndDate=" + endDate;
    let pageIndexs = "&PageIndex=" + pageIndex;
    let pageSizes = "&PageSize=" + pageSize;
    

    let url = String.format("{0}{1}{2}{3}{4}{5}{6}{7}{8}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/works/Search"
        , project
        , keySeach
        , typeSearchs
        , startDates
        , endDates
        , pageIndexs
        , pageSizes);
    return connector.get(url);
}
// Add Works Invole on workssupport
function createAddWorkInvole(role: any): Promise {    
    debugger;
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works/insertBy" );
    return connector.post(url, role);
}
// load list works invole
function getWorksInvoleAll(objWork: object): Promise {
    debugger;
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
    debugger;
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/works");
    return connector.get(url);
}