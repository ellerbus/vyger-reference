insert into dbo.WorkoutRoutine
	(
		routine_name,
		owner_id,
		weeks,
		days,
		created_at
	)
      select	'Wendler 531', -1, 4, 2, getutcdate()
union select	'Bill Starr 5x5', -1, 8, 3, getutcdate()
