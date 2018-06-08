create table dbo.WorkoutPlanExercise
(
	plan_id								int not null,
	cycle_id							int not null,
	exercise_id							int not null,
	weight								int not null,
	reps								int not null,
	pullback							int not null,
	is_calculated						bit not null,
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.WorkoutPlanExercise
	add constraint PK_WorkoutPlanExercise
	primary key (plan_id, cycle_id, exercise_id)
go

alter table dbo.WorkoutPlanExercise
	add constraint FK_WorkoutPlanExercise_WorkoutPlanCycle
	foreign key (plan_id, cycle_id)
	references dbo.WorkoutPlanCycle (plan_id, cycle_id)
	on delete cascade on update cascade
go

alter table dbo.WorkoutPlanExercise
	add constraint FK_WorkoutPlanExercise_Exercise
	foreign key (exercise_id)
	references dbo.Exercise (exercise_id)
go