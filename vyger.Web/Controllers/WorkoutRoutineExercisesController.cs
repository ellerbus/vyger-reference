using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Augment;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;
using vyger.Web.Models;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Workouts/Routines/{id:int}/Exercises"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutRoutineExercisesController : BaseController<WorkoutRoutineExercisesController>
    {
        #region Members

        private ISecurityActor _actor;
        private IWorkoutRoutineService _service;
        private IExerciseService _exercises;

        #endregion

        #region Constructors

        public WorkoutRoutineExercisesController(
            ISecurityActor actor,
            IWorkoutRoutineService service,
            IExerciseService exercises)
        {
            _actor = actor;
            _service = service;
            _exercises = exercises;
        }

        #endregion

        #region "On" Methods

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SecurityException)
            {
                AddFlashMessage(FlashMessageType.Danger, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction(MVC.WorkoutRoutines.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(int id, int week)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutineDisplay(id, week, SecurityAccess.View),
                Week = week
            };

            return View(form);
        }

        [HttpPost, Route("Index")]
        public virtual ActionResult Index(int id, int week, WorkoutRoutineExerciseForm post)
        {
            _service.MergeExercisesWith(id, week, post.Routine.RoutineExercises);

            AddFlashMessage(FlashMessageType.Success, $"Saved Exercises for Week {week}");

            return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, week));
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(int id, int day)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutine(id, SecurityAccess.View),
                RoutineExercise = new WorkoutRoutineExercise() { WeekId = 1, DayId = day, WorkoutRoutine = "12RM, 9RM, 6RM" },
                Day = day
            };

            HashSet<int> ids = new HashSet<int>(_service.GetExercisesIn(id, day));

            form.Exercises = _exercises
                .GetExercises(0)
                .Where(x => !ids.Contains(x.ExerciseId))
                .ToList();

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(int id, int day, WorkoutRoutineExerciseForm post)
        {
            if (ModelState.IsValid)
            {
                Exercise exercise = _exercises.GetExercise(post.RoutineExercise.ExerciseId, SecurityAccess.View);

                string defaultWorkout = post.RoutineExercise.WorkoutRoutine.AssertNotNull("1RM");

                _service.AddExerciseTo(id, day, exercise.ExerciseId, defaultWorkout);

                AddFlashMessage(FlashMessageType.Success, $"{exercise.ExerciseName} successfully added to Day {day}");

                return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
            }

            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutine(id, SecurityAccess.View),
                Exercises = _exercises.GetExercises(0),
                RoutineExercise = new WorkoutRoutineExercise() { WeekId = 1, DayId = day, WorkoutRoutine = "12RM, 9RM, 6RM" },
                Day = day
            };

            return View(form);
        }

        #endregion

        #region Delete Methods

        [HttpGet, Route("Delete")]
        public virtual ActionResult Delete(int id, int day, int exercise)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutine(id, SecurityAccess.View),
                Exercise = _exercises.GetExercise(exercise, SecurityAccess.View)
            };

            return View(form);
        }

        [HttpPost, Route("Delete"), ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int id, int day, int exercise, WorkoutRoutineExerciseForm post)
        {
            Exercise check = _exercises.GetExercise(exercise, SecurityAccess.View);

            _service.RemoveExerciseFrom(id, day, exercise);

            AddFlashMessage(FlashMessageType.Success, $"Exercise successfully removed from Day {day}");

            return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
        }

        #endregion

        #region Copy Methods

        [HttpGet, Route("Copy")]
        public virtual ActionResult Copy(int id, int week, int day, int exercise)
        {
            _service.CopyForward(id, week, day, exercise);

            AddFlashMessage(FlashMessageType.Success, "Copied workout routine forward successfully");

            return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, week));
        }

        #endregion
    }
}

