create table dbo.WorkoutRoutine
(
	routine_id							int not null identity(100000, 1),
	owner_id							int not null,
	routine_name						varchar(120) not null,
	weeks								int not null,
	days								int not null,
	status_enum							varchar(15) not null default 'Active',
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.WorkoutRoutine
	add constraint PK_WorkoutRoutine
	primary key (routine_id)
go

alter table dbo.WorkoutRoutine
	add constraint UQ_WorkoutRoutine
	unique (routine_name, owner_id)
go
