create table dbo.ExerciseGroup
(
	group_id							int not null identity(100000, 1),
	group_name							varchar(120) not null,
	status_enum							varchar(15) not null default 'Active',
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.ExerciseGroup
	add constraint PK_ExerciseGroup
	primary key (group_id)
go

alter table dbo.ExerciseGroup
	add constraint UQ_ExerciseGroup
	unique (group_name)
go
