import { Injectable } from '@angular/core';

import { GoogleDriveService } from 'src/services/google-drive.service';
import { FileInfo } from 'src/models/file-info';

import { Routine } from '../models/routine';

@Injectable({
    providedIn: 'root'
})
export class RoutineService
{
    private file: FileInfo;
    private routines: Routine[];

    constructor(
        private googleDriveService: GoogleDriveService) { }

    async getRoutines(): Promise<Routine[]>
    {
        if (this.routines == null)
        {
            this.file = await this.googleDriveService.getFile('routines.json');

            if (this.file.contents && this.file.contents.length > 0)
            {
                let parsed = <Routine[]>JSON.parse(this.file.contents);

                this.routines = parsed.map(x => new Routine(x));
            }
            else
            {
                this.routines = Routine.defaultList();

                await this.save();
            }
        }

        return Promise.resolve(this.routines);
    }

    async getRoutine(id: string): Promise<Routine>
    {
        return this
            .getRoutines()
            .then(routines =>
            {
                let subset = routines.filter(x => x.id == id);

                if (subset && subset.length == 1)
                {
                    return subset[0];
                }

                return null;
            });
    }

    add(routine: Routine): Promise<any>
    {
        this.routines.push(routine);

        return this.save();
    }

    async save(): Promise<any>
    {
        this.file.contents = JSON.stringify(this.routines);

        return this.googleDriveService.saveFile(this.file);
    }
}
