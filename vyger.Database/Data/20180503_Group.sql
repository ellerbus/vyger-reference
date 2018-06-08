insert into dbo.ExerciseGroup
	(
		group_name,
		created_at
	)
      select	'Calves', getutcdate()
union select	'Legs', getutcdate()
union select	'Lower Back', getutcdate()
union select	'Upper Back', getutcdate()
union select	'Chest', getutcdate()
union select	'Traps', getutcdate()
union select	'Shoulders', getutcdate()
union select	'Biceps', getutcdate()
union select	'Triceps', getutcdate()
union select	'Forearms', getutcdate()