insert into dbo.Exercise
	(
		exercise_name,
		owner_id,
		created_at,
		group_id
	)
      select	'Close-Grip Bench Press', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Chest')
union select	'Deadlifts', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Lower Back')
union select	'Hyper-Extensions', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Lower Back')
union select	'Dumbbell Curls', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Biceps')
union select	'Side-Lat Raises', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Shoulders')
union select	'Front-Lat Raises', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Shoulders')
union select	'Shoulder Press ISO', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Shoulders')
union select	'Decline Press ISO', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Chest')
union select	'Shrugs ISO', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Traps')
union select	'Front Barbell Squats', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Legs')
union select	'Leg-Press', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Legs')
union select	'Write Curls', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Forearms')
union select	'Reverse Write Curls', -1, getutcdate(), (select group_id from ExerciseGroup where group_name = 'Forearms')
