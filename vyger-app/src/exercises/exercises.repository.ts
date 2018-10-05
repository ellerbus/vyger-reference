import { Injectable } from '@angular/core';

import { DataRepository } from 'src/services/data.repository';
import { FileInfo } from 'src/models/file-info';

import { Exercise } from '../models/exercise';

@Injectable({
    providedIn: 'root'
})
export class ExercisesRepository
{

    private file: FileInfo;
    private exercises: Exercise[];

    constructor(
        private dataRepository: DataRepository) { }

    async getExercises(): Promise<Exercise[]>
    {
        if (this.exercises == null)
        {
            this.file = await this.dataRepository.getFile('exercises.json');

            if (this.file.contents && this.file.contents.length > 0)
            {
                let parsed = <Exercise[]>JSON.parse(this.file.contents);

                this.exercises = parsed.map(x => new Exercise(x));
            }
            else
            {
                this.exercises = Exercise.defaultList();

                await this.save();
            }
        }

        return Promise.resolve(this.exercises);
    }

    add(exercise: Exercise): Promise<any>
    {
        this.exercises.push(exercise);

        return this.save();
    }

    async save(): Promise<any>
    {
        this.file.contents = JSON.stringify(this.exercises);

        return this.dataRepository.saveFile(this.file);
    }
}
