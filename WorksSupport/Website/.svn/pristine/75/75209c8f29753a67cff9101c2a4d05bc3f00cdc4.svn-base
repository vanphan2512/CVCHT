import { IModule, Module, MenuItem } from "../../../../common/models/layout";
import { Priorities } from "../../priority/priorities";
import { Status } from "../../status/status";
import { Members } from "../../member/members";
import { Qualities } from "../../quality/qualities";
import { AddQuality } from "../../quality/addQuality";
import { WorksTypes } from "../../worksType/worksTypes";
import { Permission } from "../../permission/permissions";
import { AuthenticationMode } from "../../../../common/enum";
import route from "./route";

import cacheService from "../../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../../common/services/cacheService";

let module: IModule = createModule();
export default module;
function createModule() {
    let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    let module = new Module("app/modules/configurations", "configurations");
    module.menus.push(
        new MenuItem("Độ ưu tiên", route.priority.priorities.name, "fa fa-sort-amount-desc m-right-10"),
        new MenuItem("Trạng Thái", route.status.status.name, "fa fa-signal m-right-10"),
        new MenuItem("Vai Trò", route.member.members.name, "fa fa-user-circle-o m-right-10"),
        new MenuItem("Đánh Giá Chất Lượng", route.quality.qualities.name, "fa fa-check-square-o m-right-10"),
        new MenuItem("Loại Công Việc", route.worksType.worksTypes.name, "fa fa-th-list m-right-10"),
        new MenuItem("Phân Quyền Loại CV", route.permission.permissions.name, "fa fa-sitemap m-right-10")
    );

    module.addRoutes([

        // Add Priority route
        { path: route.priority.priorities.path, name: route.priority.priorities.name, component: Priorities, data: { authentication: AuthenticationMode.Require }, useAsDefault: true },
        // Add Status route
        { path: route.status.status.path, name: route.status.status.name, component: Status, data: { authentication: AuthenticationMode.Require } },
        // Add Member route
        { path: route.member.members.path, name: route.member.members.name, component: Members, data: { authentication: AuthenticationMode.Require } },
        // Add Quality route
        { path: route.quality.qualities.path, name: route.quality.qualities.name, component: Qualities, data: { authentication: AuthenticationMode.Require } },
        { path: route.quality.addQuality.path, name: route.quality.addQuality.name, component: AddQuality, data: { authentication: AuthenticationMode.Require } },
        // Add WorksType route
        { path: route.worksType.worksTypes.path, name: route.worksType.worksTypes.name, component: WorksTypes, data: { authentication: AuthenticationMode.Require } },
        // Add Permission route
        { path: route.permission.permissions.path, name: route.permission.permissions.name, component: Permission, data: { authentication: AuthenticationMode.Require } }

    ]);

    return module;
}