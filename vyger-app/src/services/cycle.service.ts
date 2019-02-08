import { Injectable } from '@angular/core';
import { DataRepository } from 'src/services/data.repository';
import { FileInfo } from 'src/models/file-info';
import { Cycle } from 'src/models/cycle';

@Injectable({
    providedIn: 'root'
})
export class CycleService
{
    private file: FileInfo;
    private cycles: Cycle[];

    constructor(
        private dataRepository: DataRepository) { }

    async getCycles(): Promise<Cycle[]>
    {
        if (this.cycles == null)
        {
            this.file = await this.dataRepository.getFile('cycles.json');

            if (this.file.contents && this.file.contents.length > 0)
            {
                let parsed = <Cycle[]>JSON.parse(this.file.contents);

                this.cycles = parsed.map(x => new Cycle(x));
            }
            else
            {
                this.cycles = [];

                await this.save();
            }
        }

        return Promise.resolve(this.cycles);
    }

    async getCycle(id: string): Promise<Cycle>
    {
        return this
            .getCycles()
            .then(cycles =>
            {
                let subset = cycles.filter(x => x.id == id);

                if (subset && subset.length == 1)
                {
                    return subset[0];
                }

                return null;
            });
    }

    add(cycle: Cycle): Promise<any>
    {
        return this.getCycles()
            .then(() =>
            {
                cycle.sequence = this.cycles.length + 1;

                this.cycles.push(cycle);

                return this.save();
            });
    }

    async save(): Promise<any>
    {
        this.file.contents = JSON.stringify(this.cycles);

        return this.dataRepository.saveFile(this.file);
    }
}
