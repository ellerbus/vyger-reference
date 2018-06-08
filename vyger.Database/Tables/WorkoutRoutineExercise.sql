create table dbo.WorkoutRoutineExercise
(
	routine_id							int not null,
	week_id								int not null,
	day_id								int not null,
	exercise_id							int not null,
	sequence_number						int not null,
	workout_routine						varchar(128) not null,
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.WorkoutRoutineExercise
	add constraint PK_WorkoutRoutineExercise
	primary key (routine_id, week_id, day_id, exercise_id)
go

alter table dbo.WorkoutRoutineExercise
	add constraint FK_WorkoutRoutineExercise_WorkoutRoutine
	foreign key (routine_id)
	references dbo.WorkoutRoutine (routine_id)
	on delete cascade on update cascade
go

alter table dbo.WorkoutRoutineExercise
	add constraint FK_WorkoutRoutineExercise_Exercise
	foreign key (exercise_id)
	references dbo.Exercise (exercise_id)
go

