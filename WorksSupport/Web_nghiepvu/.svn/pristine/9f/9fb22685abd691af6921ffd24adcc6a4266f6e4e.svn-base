import { Component, Input, Output, EventEmitter } from "angular2/core";
import { BaseControl } from "../../../models/ui";
import guidHelper from "../../../helpers/guidHelper";
import httpHelper from "../../../helpers/httpHelper";
import jsonHelper from "../../../helpers/jsonHelper";
import configHelper from "../../../helpers/configHelper";
import { LoadingIndicatorEvent, CommonEvent } from "../../../event";
import { EventManager } from "../../../eventManager";
import sessionStorage from "../../../storages/sessionStorage";
import { CACHE_CONSTANT } from "../../../services/cacheService";
@Component({
    selector: "form-filesUpload",
    templateUrl: "app/common/directives/form/upload/filesUpload.html"
})
export class FormFilesUpload extends BaseControl {
    public id: string = guidHelper.create();
    private static eventManager: EventManager;
    @Input() files: Array<any> = [];
    //@Input() maxFileSize: any = 10;
    //@Input() acceptedFilesType: any = ".pdf,.ppt,.pptx,.rar,.zip,.xls,.xlsx,.doc,.docx,.png,.jpg";
    @Output() filesChanged: EventEmitter<any> = new EventEmitter();
    @Input() description: string;
    @Input() existsFile: any;
    @Output() onFileUploaded: EventEmitter<any> = new EventEmitter();
    @Output() onFileRemoved: EventEmitter<any> = new EventEmitter();
    @Output() isUploaded: boolean = false;
    private dropzone: any = {};
    constructor() {
        super();
    }

    protected onReady() {
        let self: FormFilesUpload = this;
        let $ = window.jQuery;
        $(String.format("#{0}_form", this.id)).dropzone({
            init: function () {
                self.dropzone = this;
                this.on("removedfile", function (file: any) {
                    if (file.xhr !== undefined) {
                        let jsonResponse = jsonHelper.parse(file.xhr.responseText);
                        let ev: any = {
                            fileName: jsonResponse.Data.FileName,
                            fileSize: jsonResponse.Data.Size,
                            filePath: jsonResponse.Data.FilePath,
                            fileId: jsonResponse.Data.FileId
                        };
                        self.onFileRemoved.emit(ev);
                    }
                });
                this.on("addedfile", function (file: any) {
                    if (this.files.length) {
                        var _i, _len;
                        for (_i = 0, _len = this.files.length; _i < _len - 1; _i++) // -1 to exclude current file
                        {
                            if (this.files[_i].name === file.name && this.files[_i].size === file.size && this.files[_i].lastModifiedDate.toString() === file.lastModifiedDate.toString()) {
                                this.removeFile(file);
                            }
                        }
                    }
                });
                this.on("sending", function (file: any, xhr: any, formData: any) {
                    // Will sendthe filesize along with the file as POST data.
                    formData.append("user", sessionStorage.get(CACHE_CONSTANT.CURRENT_USER));
                    formData.append("pass", sessionStorage.get(CACHE_CONSTANT.CURRENT_PASSWORD));
                });
            },
            autoProcessQueue: true,
            uploadMultiple: false,
            url: String.format("{0}v2/files/upload", configHelper.getAppConfig().api.baseUrl),
            //removedfile: (response: any) => this.removeUploadedFile(response),
            success: (response: any) => this.fileUploadResponse(response),
            maxFilesize: 10,
            addRemoveLinks: true,
            maxFiles: 10,
            acceptedFiles: ".pdf,.ppt,.pptx,.xls,.xlsx,.doc,.docx,.jpg,.bmp,.png,.gif,.jpeg,.txt,.zip,.rar",
            dictDefaultMessage: "Kéo thả file vào đây để tải lên",
            dictFileTooBig: "Chỉ chấp nhận file tối đa 10 MB",
            dictInvalidFileType: "Chỉ chấp nhận file đính kèm(.pdf, .ppt, .pptx, .xls, .xlsx, .doc, .docx, .jpg, .bmp, .png, .gif, .jpeg, .txt, .zip, .rar)",
            dictRemoveFile: "Xóa file",
            dictCancelUpload: "Hủy",
            dictRemoveFileConfirmation: "Bạn có chắc chắn muốn xóa?",
            dictCancelUploadConfirmation: "Bạn muốn hủy upload file này?",
            dictMaxFilesExceeded: "Chỉ có thể upload tối đa 10 file"
        });
    }
    /* protected onChange() {
        let self: FormFilesUpload = this;
        self.files.forEach(function (file: any) {
            let tempFile = { name: file.fileName, zise: file.size };
            self.dropzone.emit("addedfile", tempFile);
            let photoUrl: string = String.format("{0}/v2/files/{1}/thumbnail", configHelper.getAppConfig().api.baseUrl, file.id);
            self.dropzone.emit("thumbnail", tempFile, photoUrl);
        });
    } */

    private isErrorResponse(response: any) {
        if (response.xhr !== undefined) {
            let jsonResponse = jsonHelper.parse(response.xhr.responseText);
            return httpHelper.isSuccessStatusCode(response.xhr.status) && httpHelper.isSuccessStatusCode(jsonResponse.status);
        }
    }
    private handleErrorUpload(response: any) {
        let ev: any = {
            type: response.type,
            fileName: response.name,
            fileSize: response.size
        };
        if (httpHelper.isSuccessStatusCode(response.xhr.status)) {
            ev.uploadStatus = {
                status: response.xhr.status,
                file: String.empty,
                errors: [httpHelper.resolve(response.xhr.status)]
            };
            this.onFileUploaded.emit(ev);
            return;
        }
        let jsonResponse = jsonHelper.parse(response.xhr.responseText);
        ev.uploadStatus = {
            status: jsonResponse.status,
            file: String.empty,
            errors: jsonResponse.errors
        };
        this.onFileUploaded.emit(ev);
    }
    private handleSuccessUpload(response: any) {
        let self: FormFilesUpload = this;
        let jsonResponse: any;
        jsonResponse = JSON.parse(response.xhr.responseText);
        let ev: any = {
            fileName: jsonResponse.Data.FileName,
            fileSize: jsonResponse.Data.Size,
            filePath: jsonResponse.Data.FilePath,
            fileId: jsonResponse.Data.FileId
        };
        this.filesChanged.emit(this.files);
        this.onFileUploaded.emit(ev);
        self.dropzone.emit("uploadprogress", response);
        self.isUploaded = true;
    }
    public fileUploadResponse(response: any) {
        if (this.isErrorResponse(response)) {
            this.handleErrorUpload(response);
            return;
        }
        this.handleSuccessUpload(response);
    }
    public reset() {
        let self: FormFilesUpload = this;
        self.dropzone.removeAllFiles();
    }
}