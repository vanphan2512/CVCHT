import { ElementRef, Input, Output, Component, EventEmitter } from "angular2/core";
import { Http } from "angular2/http";
import { EventManager } from "../../eventManager";
import { ResourceHelper } from "../../helpers/resourceHelper";
import { ValidationMode, ValidationException } from "../../models/exception";
import { ValidationEvent } from "../../event";
import { BaseControl } from "../../models/ui";
import { GridModel } from "./gridModel";
import { CACHE_CONSTANT } from "../../../common/services/cacheService";
import session from "../../../common/storages/sessionStorage";
@Component({
    selector: "grid",
    templateUrl: "app/common/directives/grid/grid.html"
})
export class Grid extends BaseControl {
    public model: GridModel;
    private errorKeys: Array<string>;
    public grid: any;
    public iconRowSelect: any;
    public isCanDelete: boolean = session.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    public isCanEdit: boolean = session.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
    public isCanAdd: boolean = session.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_ADD) === "true" ? true : false;
    public index: number = 0;
    @Input() eventKey: string = String.empty;
    @Input() options: any = {};
    @Output() onItemEditClicked = new EventEmitter();
    @Output() onItemDeleteClicked = new EventEmitter();
    @Output() onToggleSelectChanged = new EventEmitter();
    @Output() onIconSelectChanged = new EventEmitter();
    constructor() {
        super();
    }
    protected onInit() {
        let self: Grid = this;
        self.model = new GridModel(self.options);
        if (!String.isNullOrWhiteSpace(self.eventKey)) {
            self.registerEvent(self.eventKey, function (data: any) { self.onDataSourceChanged(data); });
        }
        this.isCanDelete = session.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "true" ? true : false;
    }
    private onDataSourceChanged(items: Array<any>) {
        let $ = window.jQuery;
        let self: Grid = this;
        self.grid.clear();
        if (items.length > 0) {
            self.grid.rows.add(items).draw();
            $(".pagination").css("display", "inline-block");
            $(".dataTables_length select").removeAttr('disabled');
        }
        else {
            self.grid.rows.add(items).draw();
            $(".pagination").css("display", "none");
            $(".dataTables_length select").attr('disabled', 'disabled');
        }
    }
    protected onReady() {
        let $ = window.jQuery;
        let self: Grid = this;
        $(document).ready(function () {
            if (session.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_DELETE) === "false") {
                this.index = 0;
            }
            else {
                this.index = 1;
            }
            /*let editor = new $.fn.dataTable.Editor({
                ajax: self.model.getAjaxUrl(),
                table: "#" + self.model.id,
                idSrc: self.model.getIDSrc(),
                fields: self.model.getFieldEditor()
            });*/
            self.grid = $("#" + self.model.id).DataTable({

                // dom: "Bfrtip",
                data: self.model.data,
                responsive: true,
                columnDefs: self.model.getColumns(),

                select: {
                    style: "multi",
                    selector: "td:first-child",
                    info: false
                },
                searching: false,
                language: {
                    "lengthMenu": "Hiển thị _MENU_ dòng",
                    "zeroRecords": "Không có kết quả",
                    "info": "",
                    select: {
                        rows: {
                            _: "",
                        }
                    }
                },

                //rowReorder: {
                //    dataSrc: self.model.getOrderIndexSrc(),
                //    selector: "td:nth-last-child(2)",
                //    editor: editor
                //},
                lengthMenu: [[10, 20, 50, 100, -1], [10, 20, 50, 100, "Tất cả"]],
                dom: "<'top'i>rt<'bottom'flp><'clear'>",
                order: [[this.index, "asc"]],
                buttons: [
                ]

            });

            /*editor.on("preSubmit", function (event: any, data: any, action: any) {
                console.log(JSON.stringify(data.data));
                data.id = Object.keys(data.data)[0];
                let html: string = "";
                for (let i = 0; i < Object.keys(data.data[data.id]).length; i++) {
                    let key = Object.keys(data.data[data.id])[i];
                    let value = data.data[data.id][key];
                    html += "data." + key + " = '" + value + "';\n";
                }
                eval(html);
                data.User = cacheService.get(CACHE_CONSTANT.strUserName);
                data.data = JSON.stringify(data.data);
                delete data.action;
            });
            editor.on("submitError", function (event: any, data: any, action: any, json: any) {
                console.log("Cập nhật lỗi");
            });*/
            self.bindEvents();
        });
    }
    private bindEvents() {
        let self: Grid = this;
        let $ = window.jQuery;
        $("#" + self.model.id + " tbody").on("click", "#editGridItem", function () {
            let data = self.grid.row($(this).parents("tr")).data();
            self.onItemEditClicked.emit({ item: data, gtid: self.grid });
        });

        $("#" + self.model.id + " tbody").on("click", ".grid-delete-item", function () {
            let data = self.grid.row($(this).parents("tr")).data();
            self.onItemDeleteClicked.emit({ item: data, gtid: self.grid });
        });
        $("#" + self.model.id + " tbody").on("click", ".toggle-active", function () {
            this.isCanEdit = session.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
            if (this.isCanEdit) {
                let data = self.grid.row($(this).parents("tr")).data();
                self.onToggleSelectChanged.emit({ item: data, gtid: self.grid });
            }
        });

        /*$("#" + self.model.id + " tbody").on("click", ".modalIcon", function () {
            this.isCanEdit = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
            if (this.isCanEdit) {
                this.iconRowSelect = self.grid.row($(this).parents("tr")).data();
                localStorage.removeItem("rowSelected");
                localStorage.setItem("rowSelected", JSON.stringify(this.iconRowSelect));
            }
        });
        $("#myIconModalGird li").on("click", function () {
            this.isCanEdit = cacheService.get(CACHE_CONSTANT.EO_WORKSSUPORTGROUP_EDIT) === "true" ? true : false;
            if (this.isCanEdit) {
                let icon = $(this).find("i").attr("class");
                let data = JSON.parse(localStorage.getItem("rowSelected"));
                self.onIconSelectChanged.emit({ item: data, icon: icon, gtid: self.grid });
                $("#myIconModalGird").modal("hide");
            }
        });*/
    }
}