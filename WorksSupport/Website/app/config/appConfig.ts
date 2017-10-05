import { IModule } from "../common/models/layout";
import registration from "../modules/registration/_share/config/module";
import configuration from "../modules/configurations/_share/config/module";
import eoffice from "../modules/eoffice/_share/config/module";

import { Languages } from "../common/enum";
let modules: Array<IModule> = [
    registration,
    configuration,
    eoffice
];
export default {
    app: {
        name: "Khai Báo Loại Công Việc Hỗ Trợ"
    },
    ioc: "./config/ioc",
    modules: modules,
    loginUrl: "http://proerp2015.com/",
    defaultUrl: "/Priorities",
    businessUrl:"/Projects",
    localization: {
        lang: Languages.EN
    },
    auth: {
        token: "authtoken"
    },
    api: {
        baseUrl: "http://nc.erp.workssupport.com/api/"
    },
    localeUrl: "/app/resources/locales/"
};