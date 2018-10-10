import { Injectable } from '@angular/core';

import { DataRepository } from 'src/services/data.repository';
import { FileInfo } from 'src/models/file-info';

import { Routine } from '../models/routine';

@Injectable({
    providedIn: 'root'
})
export class RoutinesRepository
{
    private file: FileInfo;
    private routines: Routine[];

    constructor(
        private dataRepository: DataRepository) { }

    async getRoutines(): Promise<Routine[]>
    {
        if (this.routines == null)
        {
            this.file = await this.dataRepository.getFile('routines.json');

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

        return this.dataRepository.saveFile(this.file);
    }
}
