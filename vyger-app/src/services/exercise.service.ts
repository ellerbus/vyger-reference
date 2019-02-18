import { Injectable } from '@angular/core';
import { Exercise } from 'src/models/exercise';
import { FileInfo } from 'src/models/file-info';
import { GoogleDriveService } from 'src/services/google-drive.service';

@Injectable({
    providedIn: 'root'
})
export class ExerciseService
{
    private file: FileInfo;
    private exercises: Exercise[];

    constructor(
        private googleDriveService: GoogleDriveService) { }

    async getExercises(): Promise<Exercise[]>
    {
        if (this.exercises == null)
        {
            this.file = await this.googleDriveService.getFile('exercises.json');

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

    async getExercise(id: string): Promise<Exercise>
    {
        return this
            .getExercises()
            .then(exercises =>
            {
                let subset = exercises.filter(x => x.id == id);

                if (subset && subset.length == 1)
                {
                    return subset[0];
                }

                return null;
            });
    }

    addExercise(exercise: Exercise): Promise<any>
    {
        this.exercises.push(exercise);

        return this.save();
    }

    async save(): Promise<any>
    {
        this.file.contents = JSON.stringify(this.exercises);

        return this.googleDriveService.saveFile(this.file);
    }
}
