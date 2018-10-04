import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { DataService } from 'src/services/data.service';
import { FileInfo } from 'src/models/file-info';

import { Exercise } from './models/exercise';

@Injectable({
    providedIn: 'root'
})
export class ExercisesService {

    private file: FileInfo;
    private exercises: Exercise[];

    constructor(
        private dataService: DataService) { }

    getExercises(): Observable<Exercise[]> {
        if (this.exercises == null) {
            this.file = this.dataService.getFile('exercises.json');

            if (this.file.contents && this.file.contents.length > 0) {
                let parsed = <Exercise[]>JSON.parse(this.file.contents);

                this.exercises = [];

                for (let i = 0; i < parsed.length; i++) {
                    this.exercises.push(Exercise.fromObject(parsed[i]));
                }
            }
            else {
                this.exercises = Exercise.defaultList();

                this.save();
            }
        }

        return of(this.exercises);
    }

    save(): void {
        this.file.contents = JSON.stringify(this.exercises);

        this.dataService.saveFile(this.file);
    }
}
