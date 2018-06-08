insert into dbo.Member
(
	member_email,
	is_administrator,
	status_enum,
	created_at
)
select	'stu.ellerbusch@gmail.com',
		1,
		'Active',
		getutcdate()
where	not exists
		(
			select	1
			from	dbo.Member
			where	member_email = 'stu.ellerbusch@gmail.com'
		)