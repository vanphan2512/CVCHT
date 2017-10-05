import configHelper from "../../../../common/helpers/configHelper";
import { Promise } from "../../../../common/models/promise";
let qualityService = {
    getQualilies: getQualilies,
    create: create,
    createQuanlity: createQuanlity,
    update: update,
    delete: remove,
    get: get,
    getQualityBy: getQualityBy,
    getQualiliesActived: getQualiliesActived
};

export default qualityService;

// Get list
function getQualilies(): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/Qualities");
    return connector.get(url);
}

function getQualiliesActived(): Promise{
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/Qualities/QualitiesActived");
    return connector.get(url);
}

// Get data by id
function get(id: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}v2/Qualities/{1}", configHelper.getAppConfig().api.baseUrl, id);
    return connector.get(url);
}

// Create new data
function create(role: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}", configHelper.getAppConfig().api.baseUrl);
    return connector.post(url, role);
}

function createQuanlity(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/Qualities/insertBy");
    return connector.post(url, model);
}

// Update data
function update(model: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}",
        configHelper.getAppConfig().api.baseUrl, "v2/Qualities/insertBy");
    return connector.post(url, model);
}


// Delete data
function remove(DeleteModel: any): Promise {
    let connector = window.ioc.resolve("IConnector");
    let url = String.format("{0}{1}", configHelper.getAppConfig().api.baseUrl, "v2/Qualities/Delete");
    return connector.post(url, DeleteModel);
}

// Get data by
function getQualityBy(keyWord: any, isDelete: number, pageIndex: number, pageSize: number): Promise {
    let connector = window.ioc.resolve("IConnector");
    // Cunstructer param to search.
    let keySearch = "?Keywords=" + keyWord;
    let isDel = "&IsDeleted=" + isDelete;
    let index = "&PageIndex=" + pageIndex;
    let size = "&PageSize=" + pageSize;

    let url = String.format("{0}{1}{2}{3}{4}{5}"
        , configHelper.getAppConfig().api.baseUrl
        , "v2/Qualities/Search"
        , keySearch
        , isDel
        , index
        , size);
    return connector.get(url);
}
