import { Injectable } from '@angular/core';
import { FileInfo } from 'src/models/file-info';

@Injectable({
    providedIn: 'root'
})
export class GoogleDriveService
{
    constructor()
    {
    }

    async getFile(name: string): Promise<FileInfo>
    {
        return this
            .getMetaData(name)
            .then(this.loadContents);
    }

    private getMetaData = (name: string): Promise<FileInfo> =>
    {
        let options = {
            spaces: 'appDataFolder',
            fields: 'files(id, name, size, modifiedTime)',
            q: `name='${name}'`
        };

        return new Promise((resolve, reject) =>
        {
            gapi.client.drive.files
                .list(options)
                .then((res) =>
                {
                    let results = res.result.files;

                    if (results && results.length > 0)
                    {
                        let file = new FileInfo(results[0].id, name);

                        resolve(file);
                    }
                    else
                    {
                        return this.createFile(name)
                            .then(x =>
                            {
                                resolve(x);
                            });
                    }
                });
        });
    }

    private loadContents = (file: FileInfo): Promise<FileInfo> =>
    {
        return new Promise((resolve, reject) =>
        {
            let options = {
                alt: 'media',
                fileId: file.id
            }

            return gapi.client.drive.files
                .get(options)
                .then((res) =>
                {
                    file.contents = res.body;
                    resolve(file);
                });
        });
    };

    private createFile = (name: string): Promise<FileInfo> =>
    {
        return new Promise((resolve, reject) =>
        {
            let options = {
                fields: 'id',
                resource: {
                    name: name,
                    parents: ['appDataFolder'],
                    mimeType: 'text/plain',
                }
            };

            return gapi.client.drive.files
                .create(options)
                .then((res) =>
                {
                    let file = new FileInfo(res.result.id, name);

                    resolve(file);
                });
        });
    };

    saveFile(file: FileInfo): Promise<FileInfo>
    {
        return new Promise((resolve, reject) =>
        {
            let options = {
                path: '/upload/drive/v3/files/' + file.id,
                method: 'PATCH',
                params: {
                    uploadType: 'media'
                },
                body: file.contents
            };

            return gapi.client.request(options).then(() => resolve(file));
        });
    };

    // //https://github.com/adrianbota/gdrive-appdata/blob/master/src/js/main.js
    // loadFiles(): Promise<FileInfo[]> {
    //     return new Promise((resolve, reject) => {
    //         let options = {
    //             spaces: 'appDataFolder',
    //             fields: 'files(id, name, size, modifiedTime)'
    //         };

    //         gapi.client.drive.files
    //             .list(options)
    //             .then((res) => {
    //                 this.files = res.result.files
    //                     .map(x => FileInfo.fromGoogleFile(x))
    //                     .filter(x => this.names.indexOf(x.name) > -1)
    //                     .sort((a, b) => a.name.localeCompare(b.name));

    //                 let p = Promise.all(this.names.map(x => this.loadFile(x)));

    //                 resolve(p);
    //             })
    //             .then(() => {
    //                 this.completed = true;
    //                 resolve();
    //             });
    //     });
    // }

    // hasUserData(): boolean {
    //     return this.completed;
    // }

    // getFile(name: string): FileInfo {
    //     let files = this.files.filter(x => x.name === name);

    //     if (files && files.length == 1) {
    //         return files[0];
    //     }

    //     throw 'Missing file: ' + name;
    // }

    // saveFile(file: FileInfo): Promise<any> {
    //     if (file.id) {
    //         return this.updateFile(file);
    //     }

    //     return this.createFile(file).then(() => this.updateFile(file));
    // }


}
