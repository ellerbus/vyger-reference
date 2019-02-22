import { Injectable } from '@angular/core';
import { ExerciseLog } from 'src/models/exercise-log';
import { FileInfo } from 'src/models/file-info';
import { GoogleDriveService } from 'src/services/google-drive.service';


@Injectable({
    providedIn: 'root'
})
export class ExerciseLogService
{
    private file: FileInfo;
    private logs: ExerciseLog[];

    constructor(
        private googleDriveService: GoogleDriveService) { }

    async getLogs(): Promise<ExerciseLog[]>
    {
        if (this.logs == null)
        {
            this.file = await this.googleDriveService.getFile('logs.json');

            if (this.file.contents && this.file.contents.length > 0)
            {
                let parsed = <ExerciseLog[]>JSON.parse(this.file.contents);

                this.logs = parsed.map(x => new ExerciseLog(x));
            }
            else
            {
                this.logs = [];

                await this.save();
            }
        }

        return Promise.resolve(this.logs);
    }

    add(log: ExerciseLog): Promise<any>
    {
        let exists = this.logs.some(x => x.ymd == log.ymd && x.id == log.id);

        if (!exists)
        {
            this.logs.push(log);
        }

        return this.save();
    }

    async save(): Promise<any>
    {
        this.file.contents = JSON.stringify(this.logs);

        return this.googleDriveService.saveFile(this.file);
    }
}
