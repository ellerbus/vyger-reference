create table dbo.Member
(
	member_id							int not null identity(100000, 1),
	member_email						varchar(120) not null,

	is_administrator					bit not null,
	status_enum							varchar(15) not null,
	created_at							datetime not null,
	updated_at							datetime
)
go

alter table dbo.Member
	add constraint PK_Member
	primary key (member_id)
go

alter table dbo.Member
	add constraint UQ_Member
	unique (member_email)
go
