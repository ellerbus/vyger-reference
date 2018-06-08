create table dbo.WorkoutPlanLog
(
	plan_id								int not null,
	cycle_id							int not null,
	week_id								int not null,
	day_id								int not null,
	exercise_id							int not null,
	sequence_number						int not null,
	workout_plan						varchar(128) not null,
	status_enum							varchar(15) not null,
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.WorkoutPlanLog
	add constraint PK_WorkoutPlanLog
	primary key (plan_id, cycle_id, week_id, day_id, exercise_id)
go

alter table dbo.WorkoutPlanLog
	add constraint FK_WorkoutPlanLog_WorkoutPlanExercise
	foreign key (plan_id, cycle_id, exercise_id)
	references dbo.WorkoutPlanExercise (plan_id, cycle_id, exercise_id)
	on delete cascade on update cascade
go

alter table dbo.WorkoutPlanLog
	add constraint FK_WorkoutPlanLog_Exercise
	foreign key (exercise_id)
	references dbo.Exercise (exercise_id)
go