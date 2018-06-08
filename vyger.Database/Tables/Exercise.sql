create table dbo.Exercise
(
	exercise_id							int not null identity(100000, 1),
	owner_id							int not null,
	exercise_name						varchar(120) not null,
	group_id							int not null,
	status_enum							varchar(15) not null default 'Active',
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.Exercise
	add constraint PK_Exercise
	primary key (exercise_id)
go

alter table dbo.Exercise
	add constraint UQ_Exercise
	unique (exercise_name, owner_id)
go

alter table dbo.Exercise
	add constraint FK_Exercise_ExerciseGroup
	foreign key (group_id)
	references dbo.ExerciseGroup (group_id)
	on delete cascade on update cascade
go
