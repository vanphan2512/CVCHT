import cacheService from "../../../common/services/cacheService";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
export class GridModel {
    private static NumberOfInstance = 0;
    public id: string = String.format("_grid{0}", GridModel.NumberOfInstance);
    public data: Array<any> = [];
    private options: any = null;
    constructor(options: any) {
        GridModel.NumberOfInstance++;
        this.data = options.data;
        this.options = options;
    }
    public getAjaxUrl(): string {
        let ajaxUrl: string = String.empty;
        if (String.isNullOrWhiteSpace(this.options.ajaxUrl)) { return ajaxUrl; }
        else {
            ajaxUrl = this.options.ajaxUrl;
            return ajaxUrl;
        }
    }
    public getIDSrc(): string {
        let idSrc: string = String.empty;
        if (!this.options || String.isNullOrWhiteSpace(this.options.columns)) { return idSrc; }
        else {
            this.options.columns.forEach(function (column: any, index: number) {
                if (column.type === "id") {
                    idSrc = column.field;
                }
            });
            return idSrc;
        }
    }
    public getOrderIndexSrc(): string {
        let idSrc: string = String.empty;
        if (!this.options || String.isNullOrWhiteSpace(this.options.columns)) { return idSrc; }
        else {
            this.options.columns.forEach(function (column: any, index: number) {
                if (column.type === "rownumber") {
                    idSrc = column.field;
                }
            });
            return idSrc;
        }
    }
    public getFieldEditor(): Array<any> {
        let fields: Array<any> = [];
        if (!this.options || String.isNullOrWhiteSpace(this.options.columns)) { return fields; }
        else {
            this.options.columns.forEach(function (column: any, index: number) {
                switch (column.type) {
                    case "rownumber": {
                        fields.push({ "name": column.field });
                        break;
                    }
                    default:
                        {
                            break;
                        }
                }
            });
            return fields;
        }
    }
    public getColumns(): Array<any> {
        let columns: Array<any> = [];
        let data: any;
        let type: any;
        let row: any;
        let meta: any;
        let isCanEdit: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
        let isCanDelete: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
        let isCanAdd: boolean = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD) === "true" ? true : false;
        if (!this.options || String.isNullOrWhiteSpace(this.options.columns)) { return columns; }
        if (isCanDelete) {
            columns.push({ "targets": 0, "checkboxes": { "selectRow": true }, "orderable": true });
        }
        this.options.columns.forEach(function (column: any, index: number) {
            switch (column.type) {
                case "rownumber": {
                    columns.push({ "data": column.field, title: column.title, "targets": index, "defaultContent": !!column.content ? column.content : String.empty, "className": "reorder text-center", "width": !!column.width ? column.width : null, "visible": column.visible === "false" ? false : true, "orderable": column.sort === "false" ? false : true });
                    break;
                }
                case "checkbox": {
                    columns.push({ "data": column.field, title: column.title, "targets": index, "defaultContent": !!column.content ? column.content : String.empty, "render": function (data: any, type: any, row: any) { if (type === "display") { return "<input type='checkbox' " + (data ? " checked" : "") + " disabled class='editor-active custom-checkbox " + column.field + "'><label></label>"; } return data; }, "className": !!column.className ? column.className : null, "width": !!column.width ? column.width : null, "visible": column.visible === "false" ? false : true, "orderable": column.sort === "false" ? false : true });
                    break;
                }
                case "icon": {
                    columns.push({ "data": column.field, title: column.title, "targets": index, "defaultContent": !!column.content ? column.content : String.empty, "render": function (data: any, type: any, full: any, meta: any) { return "<i class=' fa-lg " + data + "' aria-hidden='true'></i>"; }, "className": !!column.className ? column.className : null, "width": !!column.width ? column.width : null, "visible": column.visible === "false" ? false : true, "orderable": column.sort === "false" ? false : true });
                    break;
                }
                case "toggle": {
                    columns.push({ "data": column.field, title: column.title, "targets": index, "defaultContent": !!column.content ? column.content : String.empty, "render": function (data: any, type: any, full: any, meta: any) { return "<span style='display:none;'>" + data + "</span><span class='ios-tog'><input disabled class='toggle-active' id='" + column.field + "-" + (meta.row + meta.settings._iDisplayStart + 1) + "' " + (data ? " checked" : "") + " type='checkbox'><label for='" + column.field + "-" + (meta.row + meta.settings._iDisplayStart + 1) + "'><i></i></label></span>"; }, "className": !!column.className ? column.className : null, "width": !!column.width ? column.width : null, "visible": column.visible === "false" ? false : true, "orderable": column.sort === "false" ? false : true });
                    break;
                }
                default:
                    {
                        columns.push({ "data": column.field, title: column.title, "targets": index, "defaultContent": !!column.content ? column.content : String.empty, "render": !!column.render ? column.render : null, "className": !!column.className ? column.className : null, "width": !!column.width ? column.width : null, "visible": column.visible === "false" ? false : true, "orderable": column.sort === "false" ? false : true });
                        break;
                    }
            }
        });
        let actionContent = this.getActions();
         if (!String.isNullOrWhiteSpace(actionContent)) {
             if (isCanEdit ) {
                 if(!isCanDelete){
                     columns.push({ width: "67px", orderable: false, data: null, title: "Chỉnh sửa", targets: (columns.length ), defaultContent: actionContent, "className": "text-center btnEdit" });
                 }else{
                     columns.push({ width: "67px", orderable: false, data: null, title: "Chỉnh sửa", targets: (columns.length - 1 ), defaultContent: actionContent, "className": "text-center btnEdit" });
                 }
                
            }
         }
        return columns;
    }
    /* Resolve static text to i18n later */
    private getActions() {
        let html: string = String.empty;
        if (this.options.enableEdit === true) {
            html += "<a type=button class=\"btnEditTd\" id=\"editGridItem\"><i class=\" fa fa-edit fa-lg \" aria-hidden=true></i></a>";
        }
        /*if (this.options.enableDelete === true) {
            html += "<input type=button class=\"btn btn-danger grid-delete-item\" id=\"deleteGridItem\" value=\"Delete\"/>";
        }*/
        return html;
    }
}