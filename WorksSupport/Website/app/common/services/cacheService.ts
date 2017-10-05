import sessionStorage from "../storages/sessionStorage";
export const CACHE_CONSTANT: any = {
    USER_PROFILE: "user_profile",
    TOKEN: "token",
    EO_WORKSSUPORTGROUP_VIEW: "EO_WORKSSUPORTGROUP_VIEW",
    EO_WORKSSUPORTGROUP_ADD: "EO_WORKSSUPORTGROUP_ADD",
    EO_WORKSSUPORTGROUP_EDIT: "EO_WORKSSUPORTGROUP_EDIT",
    EO_WORKSSUPORTGROUP_DELETE: "EO_WORKSSUPORTGROUP_DELETE",
    EO_WORKSSUPORTWORKS_VIEW: "EO_WORKSSUPPORT_VIEW",
    EO_WORKSSUPPORT_AUTHORIZE_VIEW: "EO_WORKSSUPPORT_AUTHORIZE_VIEW",
    EO_WORKSSUPPORT_AUTHORIZE_EDIT: "EO_WORKSSUPPORT_AUTHORIZE_EDIT",
    strUserName:"strUserName"
};
let cacheService: any = {
    get: get,
    exist: exist,
    set: set,
    remove: remove
};
export default cacheService;
function remove(key: string) {
    sessionStorage.remove(key);
}
function exist(key: string): boolean {
    return sessionStorage.exist(key);
}
function get(key: string): any {
    return sessionStorage.get(key);
}
function set(key: string, data: any): any {
    return sessionStorage.set(key, data);
}