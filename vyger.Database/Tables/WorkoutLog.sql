create table dbo.WorkoutLog
(
	member_id							int not null,
	log_date							datetime not null,
	exercise_id							int not null,
	workout								varchar(128) not null,

	plan_id								int not null,
	cycle_id							int not null,
	week_id								int not null,
	day_id								int not null,
	sequence_number						int not null,

	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.WorkoutLog
	add constraint PK_WorkoutLog
	primary key (member_id, log_date, exercise_id)
go

alter table dbo.WorkoutLog
	add constraint FK_WorkoutLog_Member
	foreign key (member_id)
	references dbo.Member (member_id)
	on delete cascade on update cascade
go

alter table dbo.WorkoutLog
	add constraint FK_WorkoutLog_Exercise
	foreign key (exercise_id)
	references dbo.Exercise (exercise_id)
go
