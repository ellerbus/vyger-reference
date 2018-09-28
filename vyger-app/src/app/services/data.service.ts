import { Injectable } from '@angular/core';
import { FileInfo, MIME_TYPE_FOLDER } from '../models/file-info';
// declare var UploaderForGoogleDrive;

@Injectable({
    providedIn: 'root'
})
export class DataService {
    files: FileInfo[] = [];

    //https://github.com/adrianbota/gdrive-appdata/blob/master/src/js/main.js
    getFiles(): Promise<FileInfo[]> {
        return new Promise((resolve, reject) => {
            return gapi.client.drive.files.list({
                spaces: 'appDataFolder',
                fields: "files(id, name, modifiedTime)"
            }).then((res) => {
                res.result.files.forEach((file) => this.files.push(FileInfo.fromGoogleFile(file)));
                resolve(this.files);
            });
        });
    }
    //     create(parentId: string, folderName: string) {
    //         var folder = {
    //             name: folderName,
    //             mimeType: MIME_TYPE_FOLDER,
    //             parents: [parentId]
    //         };
    //         return gapi.client.drive.files.create({
    //             resource: folder,
    //             fields: 'id, name, mimeType, modifiedTime, size'
    //         }).then((res) => {
    //             return FileInfo.fromGoogleFile(res.result);
    //         });
    //     }

    //     delete(fileId: string) {
    //         return gapi.client.drive.files.delete({
    //             fileId: fileId
    //         });
    //     }

    //     importFile(parentId: string, file: FileInfo, onError: any, onComplete: any, onProgress: any) {
    //         var contentType = file.Blob.type || 'application/octet-stream';
    //         var metadata = {
    //             name: file.Blob.name,
    //             mimeType: contentType,
    //             parents: [parentId]
    //         };

    //         var uploader = new UploaderForGoogleDrive({
    //             file: file.Blob,
    //             token: gapi.auth2.getAuthInstance().currentUser.get().getAuthResponse().access_token,
    //             metadata: metadata,
    //             onError: onError,
    //             onComplete: onComplete,
    //             onProgress: onProgress,
    //             params: {
    //                 convert: false,
    //                 ocr: false
    //             }

    //         });

    //         uploader.upload();
    //     }
    //     // importFile(parentId:string, name:string, blob:Blob) {
    //     //     const boundary = 'hintdesk';
    //     //     const delimiter = '\r\n--' + boundary + '\r\n';
    //     //     const close_delim = '\r\n--' + boundary + '--';

    //     //     var reader = new FileReader();
    //     //     reader.readAsBinaryString(blob);
    //     //     reader.onload = function (e) {
    //     //         var contentType = blob.type || 'application/octet-stream';
    //     //         var metadata = {
    //     //             name: name,
    //     //             mimeType: contentType,
    //     //             parents: [parentId]
    //     //         };

    //     //         var base64Data = btoa(reader.result.toString());
    //     //         var multipartRequestBody =
    //     //             delimiter +
    //     //             'Content-Type: application/json\r\n\r\n' +
    //     //             JSON.stringify(metadata) +
    //     //             delimiter +
    //     //             'Content-Type: ' + contentType + '\r\n' +
    //     //             'Content-Transfer-Encoding: base64\r\n' +
    //     //             '\r\n' +
    //     //             base64Data +
    //     //             close_delim;

    //     //         return gapi.client.request({
    //     //             'path': '/upload/drive/v3/files',
    //     //             'method': 'POST',
    //     //             'params': { 'uploadType': 'multipart' },
    //     //             'headers': {
    //     //                 'Content-Type': 'multipart/mixed; boundary='' + boundary + '''
    //     //             },
    //     //             'body': multipartRequestBody
    //     //         });
    //     //     }
    //     // }
}