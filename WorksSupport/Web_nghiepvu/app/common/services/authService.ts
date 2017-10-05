import {PromiseFactory} from "../models/promise";
import { CACHE_CONSTANT } from "./cacheService";
import session from "../storages/sessionStorage";

let authService: any = {
    getUserProfile: getUserProfile,
    setAuth: setAuth,
    getAuth: getAuth,
    removeAuth: removeAuth,
    isAuthenticated: isAuthenticated,
    isAuthorized: isAuthorized
};
export default authService;
function isAuthorized(routeInstruction: any) {
    let profile = getUserProfile();
    return isAuthenticated(profile);
}

function removeAuth(): void {
    session.remove(CACHE_CONSTANT.USER_PROFILE);
    session.remove(CACHE_CONSTANT.TOKEN);
}

function isAuthenticated(profile: any) {
    return !!profile;
}

function setAuth(auth: any) {
    session.set(CACHE_CONSTANT.USER_PROFILE, auth.profile);
    session.set(CACHE_CONSTANT.TOKEN, auth.profile.strTokenString);
    session.set(CACHE_CONSTANT.strUserName, auth.profile.strUserName);
    let listFunction: Array<any> = auth.lsUserFunction;
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPORTGROUP_VIEW") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_VIEW, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_VIEW, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPORTGROUP_ADD") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPORTGROUP_EDIT") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPORTGROUP_DELETE") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPPORT_AUTHORIZE_VIEW") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_VIEW, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_VIEW, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPPORT_AUTHORIZE_EDIT") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_EDIT, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_AUTHORIZE_EDIT, "true");
    // Set project Role
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPORTPROJECT_ADD") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_ADD, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_ADD, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPORTPROJECT_DEL") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_DEL, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_DEL, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPORTPROJECT_EDIT") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_EDIT, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPORTPROJECT_EDIT, "true");

    // Set WorksSupport Role
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPPORT_VIEW") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_VIEW, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_VIEW, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPPORT_ADD") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_ADD, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_ADD, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPPORT_EDIT") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_EDIT, "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_EDIT, "true");
    listFunction.findIndex(obj => obj.FunctionID === "EO_WORKSSUPPORT_DELETE") === -1 ? session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_DELETE , "false") : session.set(CACHE_CONSTANT.EO_WORKSSUPPORT_DELETE , "true");
}
function getAuth(): any {
    let auth: any = {
        profile: session.get(CACHE_CONSTANT.USER_PROFILE),
        token: session.get(CACHE_CONSTANT.TOKEN)
    };
    return auth;
}
function getUserProfile(): any {
    if (!session.exist(CACHE_CONSTANT.USER_PROFILE)) {
        return null;
    }
    let userProfile = session.get(CACHE_CONSTANT.USER_PROFILE);
    return userProfile;
}