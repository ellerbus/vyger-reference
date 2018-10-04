import { Injectable } from '@angular/core';
import { FileInfo } from 'src/models/file-info';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    private names = ['exercises.json', 'logs.json', 'routines.json', 'plans.json'];

    private completed: boolean = false;

    files: FileInfo[] = [];

    //https://github.com/adrianbota/gdrive-appdata/blob/master/src/js/main.js
    loadFiles(): Promise<FileInfo[]> {
        return new Promise((resolve, reject) => {
            let options = {
                spaces: 'appDataFolder',
                fields: 'files(id, name, size, modifiedTime)'
            };

            gapi.client.drive.files
                .list(options)
                .then((res) => {
                    this.files = res.result.files
                        .map(x => FileInfo.fromGoogleFile(x))
                        .filter(x => this.names.indexOf(x.name) > -1)
                        .sort((a, b) => a.name.localeCompare(b.name));

                    let p = Promise.all(this.names.map(x => this.loadFile(x)));

                    resolve(p);
                })
                .then(() => {
                    this.completed = true;
                    resolve();
                });
        });
    }

    private loadFile = (name: string): Promise<any> => {
        const files = this.files.filter(x => x.name === name);

        if (files == null || files.length == 0) {
            return new Promise((resolve, reject) => {
                //  TODO missing / create
                this.files.push(new FileInfo(name));
                resolve();
            });
        }

        let file = files[0];

        return new Promise((resolve, reject) => {
            let options = {
                alt: 'media',
                fileId: file.id
            }

            gapi.client.drive.files
                .get(options)
                .then((res) => {
                    file.contents = res.body;
                    resolve();
                });
        });
    }

    hasUserData(): boolean {
        return this.completed;
    }

    getFile(name: string): FileInfo {
        let files = this.files.filter(x => x.name === name);

        if (files && files.length == 1) {
            return files[0];
        }

        throw 'Missing file: ' + name;
    }

    saveFile(file: FileInfo): Promise<any> {
        if (file.id) {
            return this.updateFile(file);
        }

        return this.createFile(file);
    }

    private updateFile = (file: FileInfo): Promise<any> => {
        return new Promise((resolve, reject) => {
            const options = {};

            return gapi.client.request({
                path: '/upload/drive/v3/files/' + file.id,
                method: 'PATCH',
                params: {
                    uploadType: 'media'
                },
                body: file.contents
            });
        });
    };

    private createFile = (file: FileInfo): Promise<any> => {
        return new Promise((resolve, reject) => {
            const options = {
                fields: 'id',
                resource: {
                    name: file.name,
                    parents: ['appDataFolder'],
                    mimeType: 'text/plain',
                }
            };

            return gapi.client.drive.files
                .create(options)
                .then((res) => {
                    file.id = res.result.id;
                    resolve();
                });
        });
    };

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
