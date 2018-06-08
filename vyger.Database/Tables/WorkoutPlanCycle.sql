create table dbo.WorkoutPlanCycle
(
	plan_id								int not null,
	cycle_id							int not null,
	status_enum							varchar(15) not null,
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.WorkoutPlanCycle
	add constraint PK_WorkoutPlanCycle
	primary key (plan_id, cycle_id)
go

alter table dbo.WorkoutPlanCycle
	add constraint FK_WorkoutPlanCycle_WorkoutPlan
	foreign key (plan_id)
	references dbo.WorkoutPlan (plan_id)
	on delete cascade on update cascade
go
