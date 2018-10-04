export const MIME_TYPE_FOLDER = "application/vnd.google-apps.folder";

export class FileInfo {
    id: string;
    mimeType: string;
    modifiedTime: Date;
    name: string;
    progress: number;
    size: string;
    contents: string;

    constructor(name?: string) {
        this.name = name;
    }

    static fromGoogleFile(file: gapi.client.drive.File): FileInfo {
        let fileInfo = new FileInfo(file.name);

        fileInfo.id = file.id;
        fileInfo.mimeType = file.mimeType;
        fileInfo.modifiedTime = new Date(file.modifiedTime);
        fileInfo.size = file.size;

        return fileInfo;
    }

    // public get ModifiedTimeText(): string {
    //     return this.ModifiedTime.getDate() + "." + (this.ModifiedTime.getMonth() + 1) + "." + this.ModifiedTime.getFullYear();
    // }

    // public get SizeText(): string {
    //     if (!this.Size) return "-";

    //     let size: number = parseInt(this.Size);
    //     if (size < Math.pow(1024, 1))
    //         return size.toString();
    //     else if (size < Math.pow(1024, 2))
    //         return Math.floor(size / Math.pow(1024, 1)) + " KB";
    //     else if (size < Math.pow(1024, 3))
    //         return Math.floor(size / Math.pow(1024, 2)) + " MB";
    //     else if (size < Math.pow(1024, 3))
    //         return Math.floor(size / Math.pow(1024, 3)) + " GB";
    //     else
    //         return Math.floor(size / Math.pow(1024, 4)) + " GB";
    // }
}
