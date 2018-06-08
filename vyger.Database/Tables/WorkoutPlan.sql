create table dbo.WorkoutPlan
(
	plan_id								int not null identity(100000, 1),
	owner_id							int not null,
	routine_id							int not null,
	cycle_id							int not null,
	status_enum							varchar(15) not null,
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.WorkoutPlan
	add constraint PK_WorkoutPlan
	primary key (plan_id)
go

alter table dbo.WorkoutPlan
	add constraint FK_WorkoutPlan_WorkoutRoutine
	foreign key (routine_id)
	references dbo.WorkoutRoutine (routine_id)
	on delete cascade on update cascade
go
