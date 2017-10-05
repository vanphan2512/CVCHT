import { IModule, Module, MenuItem } from "../../../../common/models/layout";

import { Projects } from "../../project/projects";
import { AddProject } from "../../project/addProject";

import { WorksGroups } from "../../worksgroup/worksgroups";
import { Works } from "../../works/works";
import { AuthenticationMode } from "../../../../common/enum";

import route from "./route";
let module: IModule = createModule();
export default module;
function createModule() {
    let module = new Module("app/modules/eoffice", "eoffice");
    module.addRoutes([
        // Add project route
        { path: route.project.projects.path, name: route.project.projects.name, component: Projects, data: { authentication: AuthenticationMode.Require } },
        { path: route.project.addProject.path, name: route.project.addProject.name, component: AddProject, data: { authentication: AuthenticationMode.Require } },

        // add worksgroup route
        { path: route.worksgroup.worksgroups.path, name: route.worksgroup.worksgroups.name, component: WorksGroups, data: { authentication: AuthenticationMode.Require } },
        { path: route.worksgroup.addworksgroup.path, name: route.worksgroup.addworksgroup.name, component: WorksGroups, data: { authentication: AuthenticationMode.Require } },

         // add works route
        { path: route.work.works.path, name: route.work.works.name, component: Works, data: { authentication: AuthenticationMode.Require } }
 
    ]);
    return module;
}