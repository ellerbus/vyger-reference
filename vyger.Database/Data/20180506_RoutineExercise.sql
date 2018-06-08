declare @wendler_id int

    --  select	'Wendler 531', -1, 4, 2, getutcdate()
select	@wendler_id = routine_id
from	dbo.WorkoutRoutine
where	routine_name = 'Wendler 531'

insert into dbo.WorkoutRoutineExercise
	(
		routine_id,
		week_id,
		day_id,
		sequence_number,
		created_at,
		workout_routine,
		exercise_id
	)
	select		@wendler_id, 1, 1, 1, getutcdate(),
				'S!*60%x5, S!*70%x5, S!*80%x5, S!*90%x5, 1RM*85%x5',
				(select exercise_id from dbo.Exercise where exercise_name = 'Leg-Press')
union select	@wendler_id, 1, 1, 2, getutcdate(),
				'S!*60%x5, S!*70%x5, S!*80%x5, S!*90%x5, 1RM*85%x5',
				(select exercise_id from dbo.Exercise where exercise_name = 'Close-Grip Bench Press')
union select	@wendler_id, 1, 1, 3, getutcdate(),
				'S!*60%x5, S!*70%x5, S!*80%x5, S!*90%x5, 1RM*85%x5',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shrugs ISO')
union select	@wendler_id, 1, 2, 1, getutcdate(),
				'S!*60%x5, S!*70%x5, S!*80%x5, S!*90%x5, 1RM*85%x5',
				(select exercise_id from dbo.Exercise where exercise_name = 'Deadlifts')
union select	@wendler_id, 1, 2, 2, getutcdate(),
				'S!*60%x5, S!*70%x5, S!*80%x5, S!*90%x5, 1RM*85%x5',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shoulder Press ISO')
union select	@wendler_id, 2, 1, 1, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x3, S!*90%x3, 1RM*90%x3',
				(select exercise_id from dbo.Exercise where exercise_name = 'Leg-Press')
union select	@wendler_id, 2, 1, 2, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x3, S!*90%x3, 1RM*90%x3',
				(select exercise_id from dbo.Exercise where exercise_name = 'Close-Grip Bench Press')
union select	@wendler_id, 2, 1, 3, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x3, S!*90%x3, 1RM*90%x3',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shrugs ISO')
union select	@wendler_id, 2, 2, 1, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x3, S!*90%x3, 1RM*90%x3',
				(select exercise_id from dbo.Exercise where exercise_name = 'Deadlifts')
union select	@wendler_id, 2, 2, 2, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x3, S!*90%x3, 1RM*90%x3',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shoulder Press ISO')
union select	@wendler_id, 3, 1, 1, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x5, S!*90%x3, 1RM*95%',
				(select exercise_id from dbo.Exercise where exercise_name = 'Leg-Press')
union select	@wendler_id, 3, 1, 2, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x5, S!*90%x3, 1RM*95%',
				(select exercise_id from dbo.Exercise where exercise_name = 'Close-Grip Bench Press')
union select	@wendler_id, 3, 1, 3, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x5, S!*90%x3, 1RM*95%',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shrugs ISO')
union select	@wendler_id, 3, 2, 1, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x5, S!*90%x3, 1RM*95%',
				(select exercise_id from dbo.Exercise where exercise_name = 'Deadlifts')
union select	@wendler_id, 3, 2, 2, getutcdate(),
				'S!*60%x5, S!*70%x3, S!*80%x5, S!*90%x3, 1RM*95%',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shoulder Press ISO')
union select	@wendler_id, 4, 1, 1, getutcdate(),
				'S!*80%x8, S!*90%x8, 1RM*60%x8',
				(select exercise_id from dbo.Exercise where exercise_name = 'Leg-Press')
union select	@wendler_id, 4, 1, 2, getutcdate(),
				'S!*80%x8, S!*90%x8, 1RM*60%x8',
				(select exercise_id from dbo.Exercise where exercise_name = 'Close-Grip Bench Press')
union select	@wendler_id, 4, 1, 3, getutcdate(),
				'S!*80%x8, S!*90%x8, 1RM*60%x8',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shrugs ISO')
union select	@wendler_id, 4, 2, 1, getutcdate(),
				'S!*80%x8, S!*90%x8, 1RM*60%x8',
				(select exercise_id from dbo.Exercise where exercise_name = 'Deadlifts')
union select	@wendler_id, 4, 2, 2, getutcdate(),
				'S!*80%x8, S!*90%x8, 1RM*60%x8',
				(select exercise_id from dbo.Exercise where exercise_name = 'Shoulder Press ISO')
