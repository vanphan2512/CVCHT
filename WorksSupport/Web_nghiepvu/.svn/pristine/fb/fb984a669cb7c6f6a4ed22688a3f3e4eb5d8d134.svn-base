import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let prioritiesService = {
    getPriorities: getPriorities,
    getPrioritiesAll: getPrioritiesAll,
    getPrioritiesByIsDelete: getPrioritiesByIsDelete,
    create: create,
    update: update,
    delete: remove,
    get: get, 
    getPrioritiesIsActived: getPrioritiesIsActived,
    getPrioritiesByProjectId: getPrioritiesByProjectId
};
export default prioritiesService;

// Get list
function getPrioritiesAll(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/priorities");
    return connector.get(url);
}

function getPrioritiesIsActived(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/priorities/worksTypeActived");
    return connector.get(url);
}

function getPrioritiesByProjectId(projectId: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}/{2}", configHelper.getAppConfig().api.baseUrl, "v2/priorities/getPriorityByProjectId", projectId);
    return connector.get(url);
}

// Get data by condition search
function getPriorities(keywords: any, isDeleted: number, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    let key: any = "?Keywords=" + keywords;
    let isDel: any = "&IsDeleted=" + isDeleted;
    let page: any = "&PageIndex=" + pageIndex;
    let size: any = "&PageSize=" + pageSize;

    let url = String.format("{0}{1}{2}{3}{4}{5}", configHelper.getAppConfig().api.baseUrl, "v2/priorities/SearchKey",
        key, isDel, page, size
    );
    return connector.get(url);
}

// Get data by id
function get(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/priorities/{1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}

// Create new data
function create(priority: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl,"v2/priorities/insertBy");
    return connector.post(url, priority);
}

// Update data
function update(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/priorities/insertBy");
    return connector.post(url, model);
}

// Delete data
function remove(deletedModel: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}/{1}", configHelper.getAppConfig().api.baseUrl, "v2/priorities/Del");
    return connector.post(url, deletedModel);
}

// Get by status 1:Deleted/ 0: Avaliable

function getPrioritiesByIsDelete(status: any) {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}/{2}", configHelper.getAppConfig().api.baseUrl, "v2/priorities", status);
    return connector.get(url);
}