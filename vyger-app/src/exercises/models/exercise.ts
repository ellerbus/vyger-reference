import { generateId } from 'src/models/utilities';

export enum Groups {
    Abs = 'Abs',
    Biceps = 'Biceps',
    Chest = 'Chest',
    Forearms = 'Forearms',
    Legs = 'Legs',
    LowerBack = 'LowerBack',
    Shoulders = 'Shoulders',
    Traps = 'Traps',
    Triceps = 'Triceps',
    UpperBack = 'UpperBack',
}
export enum Categories {
    Barbell = 'Barbell',
    Dumbbell = 'Dumbbell',
    Machine = 'Machine',
    Hammer = 'Hammer',
    Body = 'Body',
}
export class Exercise {
    id: string;
    group: Groups;
    category: Categories;
    name: string;

    constructor() {
        this.id = generateId('x', 2);
    }

    get full(): string {
        return this.group + ', ' + this.short;
    }

    get short(): string {
        return this.category + ' ' + this.name;
    }

    static matches(e: Exercise, category: Categories, name: string): boolean {
        if (e.category == category) {
            if (e.name != null && name != null) {
                return e.name.toLowerCase() == name.toLowerCase();
            }
        }

        return false;
    }

    static fromObject(obj: { id?: string, group: string, category: string, name: string }): Exercise {
        let exercise = new Exercise();

        exercise.id = obj.id || exercise.id;
        exercise.group = Groups[obj.group];
        exercise.category = Categories[obj.category];
        exercise.name = obj.name;

        return exercise;
    }

    static defaultList(): Exercise[] {
        return [
            Exercise.fromObject({ group: 'Abs', category: 'Machine', name: 'Crunch' }),
            Exercise.fromObject({ group: 'Abs', category: 'Body', name: 'Crunch' }),

            Exercise.fromObject({ group: 'Biceps', category: 'Barbell', name: 'Curls' }),
            Exercise.fromObject({ group: 'Biceps', category: 'Dumbbell', name: 'Curls' }),

            Exercise.fromObject({ group: 'Chest', category: 'Barbell', name: 'Decline Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Dumbbell', name: 'Decline Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Hammer', name: 'Decline Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Barbell', name: 'Flat Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Dumbbell', name: 'Flat Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Hammer', name: 'Flat Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Barbell', name: 'Incline Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Dumbbell', name: 'Incline Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Hammer', name: 'Incline Bench Press' }),
            Exercise.fromObject({ group: 'Chest', category: 'Machine', name: 'Flyes' }),

            Exercise.fromObject({ group: 'Forearms', category: 'Barbell', name: 'Standing Wrist Curls' }),
            Exercise.fromObject({ group: 'Forearms', category: 'Barbell', name: 'Reverse Wrist Curls' }),

            Exercise.fromObject({ group: 'Legs', category: 'Barbell', name: 'Front Squats' }),
            Exercise.fromObject({ group: 'Legs', category: 'Barbell', name: 'Rear Squats' }),
            Exercise.fromObject({ group: 'Legs', category: 'Machine', name: 'Leg Press' }),

            Exercise.fromObject({ group: 'LowerBack', category: 'Barbell', name: 'Deadlifts' }),
            Exercise.fromObject({ group: 'LowerBack', category: 'Dumbbell', name: 'Deadlifts' }),

            Exercise.fromObject({ group: 'Shoulders', category: 'Dumbbell', name: 'Front Lat Raises' }),
            Exercise.fromObject({ group: 'Shoulders', category: 'Hammer', name: 'Press' }),
            Exercise.fromObject({ group: 'Shoulders', category: 'Dumbbell', name: 'Rear Lat Raises' }),
            Exercise.fromObject({ group: 'Shoulders', category: 'Dumbbell', name: 'Side Lat Raises' }),

            Exercise.fromObject({ group: 'Traps', category: 'Barbell', name: 'Shrugs' }),
            Exercise.fromObject({ group: 'Traps', category: 'Dumbbell', name: 'Shrugs' }),
            Exercise.fromObject({ group: 'Traps', category: 'Hammer', name: 'Shrugs' }),

            Exercise.fromObject({ group: 'Triceps', category: 'Body', name: 'Dips' }),

            Exercise.fromObject({ group: 'UpperBack', category: 'Hammer', name: 'High Rows' }),
            Exercise.fromObject({ group: 'UpperBack', category: 'Hammer', name: 'Low Rows' }),
        ];
    }

    static compare(a: Exercise, b: Exercise): number {
        var group = a.group.localeCompare(b.group);

        if (group != 0) {
            return group;
        }

        var category = a.category.localeCompare(b.category);

        if (category != 0) {
            return category;
        }

        return a.name.localeCompare(b.name);
    }
}
