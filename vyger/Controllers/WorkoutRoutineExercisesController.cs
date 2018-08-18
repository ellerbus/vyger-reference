using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Augment;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Services;
using vyger.ViewModels;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Routines/{id}/Exercises"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutRoutineExercisesController : BaseController
    {
        #region Members

        private IWorkoutRoutineService _service;

        #endregion

        #region Constructors

        public WorkoutRoutineExercisesController(IWorkoutRoutineService service)
        {
            _service = service;
        }

        #endregion

        #region "On" Methods

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SecurityException)
            {
                AddFlashMessage(FlashMessageType.Danger, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Index", "WorkoutRoutines");
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(string id, int week)
        {
            WorkoutRoutineExerciseViewModel vm = new WorkoutRoutineExerciseViewModel();

            vm.Routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            vm.Week = week;

            return View(vm);
        }

        //[HttpPost, Route("Index")]
        //public virtual ActionResult Index(string id, int week, WorkoutRoutineExerciseViewModel post)
        //{
        //    _service.MergeExercisesWith(id, week, post.Routine.RoutineExercises);

        //    AddFlashMessage(FlashMessageType.Success, $"Saved Exercises for Week {week}");

        //    return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, week));
        //}

        #endregion

        #region Edit Methods

        [HttpPost, Route("Edit"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, int week, int day, string exercise, WorkoutRoutineExercise post)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            WorkoutRoutineExercise routineExercise = routine.RoutineExercises.Find(week, day, exercise).FirstOrDefault();

            if (routineExercise != null)
            {
                routineExercise.WorkoutRoutine = WorkoutRoutineSetCollection.Format(post.WorkoutRoutine);
                routineExercise.SequenceNumber = post.SequenceNumber;

                _service.SaveWorkoutRoutines();

                return Json(new { routineExercise.WorkoutRoutine });
            }

            return Json(new { message = "Cannot Update Routine" });
        }

        [HttpPost, Route("Sequence"), ValidateAntiForgeryToken]
        public virtual ActionResult Sequence(string id, int week, int day, string exercises)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            int seq = 0;

            foreach (string exercise in exercises.Split(','))
            {
                seq += 1;

                IEnumerable<WorkoutRoutineExercise> routineExercises = routine
                    .RoutineExercises
                    .Find(0, day, exercise);

                foreach (WorkoutRoutineExercise rex in routineExercises)
                {
                    rex.SequenceNumber = seq;
                }
            }

            _service.SaveWorkoutRoutines();

            return Json(new { message = "Saved" });
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(string id, int day)
        {
            WorkoutRoutineExerciseViewModel vm = GetCreateForm(id, day);

            return View(vm);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(string id, int day, WorkoutRoutineExerciseViewModel post)
        {
            WorkoutRoutineExerciseViewModel vm = GetCreateForm(id, day);

            if (ModelState.IsValid)
            {
                vm.Routine.RoutineExercises.Add(day, post.RoutineExercise.ExerciseId, post.RoutineExercise.WorkoutRoutine);

                _service.SaveWorkoutRoutines();

                AddFlashMessage(FlashMessageType.Success, $"Successfully added to Day {day}");

                return RedirectToAction("Index", new { id, week = 1 });
            }

            return View(vm);
        }

        private WorkoutRoutineExerciseViewModel GetCreateForm(string id, int day)
        {
            WorkoutRoutineExerciseViewModel vm = new WorkoutRoutineExerciseViewModel()
            {
                Routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id),
                RoutineExercise = new WorkoutRoutineExercise() { WeekId = 1, DayId = day, WorkoutRoutine = "12RM, 9RM, 6RM" }
            };

            vm.Exercises = vm.Routine
                .AllExercises
                .NotIncluding(vm.Routine.RoutineExercises.Find(1, day, ""))
                .ToList();

            return vm;
        }

        #endregion

        #region Delete Methods

        [HttpGet, Route("Delete")]
        public virtual ActionResult Delete(string id, int day, string exercise)
        {
            WorkoutRoutineExerciseViewModel vm = GetDeleteForm(id, day, exercise);

            return View(vm);
        }

        [HttpPost, Route("Delete"), ValidateAntiForgeryToken]
        public virtual ActionResult Delete(string id, int day, string exercise, WorkoutRoutineExercise post)
        {
            WorkoutRoutineExerciseViewModel vm = GetDeleteForm(id, 0, "");

            vm.Routine.RoutineExercises.DeleteWorkoutRoutineExercise(day, exercise);

            _service.SaveWorkoutRoutines();

            AddFlashMessage(FlashMessageType.Success, $"Exercise successfully removed from Day {day}");

            return RedirectToAction("Index", new { id, week = 1 });
        }

        private WorkoutRoutineExerciseViewModel GetDeleteForm(string id, int day, string exercise)
        {
            WorkoutRoutineExerciseViewModel vm = new WorkoutRoutineExerciseViewModel()
            {
                Routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id)
            };

            if (day > 0 || exercise.IsNotEmpty())
            {
                vm.RoutineExercise = vm.Routine.RoutineExercises.Find(1, day, exercise).FirstOrDefault();
            }

            return vm;
        }

        #endregion

        #region Copy Methods

        [HttpGet, Route("Copy")]
        public virtual ActionResult Copy(string id, int week, int day, string exercise)
        {
            WorkoutRoutineExerciseViewModel vm = GetCopyForm(id, day, exercise);

            string workoutRoutine = vm.Routine
                .RoutineExercises
                .Find(week, day, exercise)
                .Select(x => x.WorkoutRoutine)
                .First();

            IEnumerable<WorkoutRoutineExercise> routineExercises = vm.Routine
                .RoutineExercises
                .Find(0, day, exercise);

            foreach (WorkoutRoutineExercise rex in routineExercises)
            {
                if (rex.WeekId > week)
                {
                    rex.WorkoutRoutine = workoutRoutine;
                }
            }

            _service.SaveWorkoutRoutines();

            AddFlashMessage(FlashMessageType.Success, "Copied workout routine forward successfully");

            return RedirectToAction("Index", new { id, week });
        }

        private WorkoutRoutineExerciseViewModel GetCopyForm(string id, int day, string exercise)
        {
            WorkoutRoutineExerciseViewModel vm = new WorkoutRoutineExerciseViewModel()
            {
                Routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id)
            };

            return vm;
        }

        #endregion
    }
}

