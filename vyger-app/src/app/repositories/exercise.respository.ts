import { Injectable, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Exercise } from '../models/exercise';
import { DataService } from '../services/data.service';
import { FileInfo } from '../models/file-info';

@Injectable({
    providedIn: 'root'
})
export class ExerciseRepository {

    private file: FileInfo;
    private exercises: Exercise[];

    constructor(
        private dataService: DataService) {
    }

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
            }
        }

        return of(this.exercises);
    }
}
