using System;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Augment;
using vyger.Common;
using vyger.Forms;
using vyger.Models;
using vyger.Services;

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
                filterContext.Result = RedirectToAction(MVC.WorkoutRoutines.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(string id, int week)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm();

            form.Routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            form.Week = week;

            return View(form);
        }

        //[HttpPost, Route("Index")]
        //public virtual ActionResult Index(string id, int week, WorkoutRoutineExerciseForm post)
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

            if (routineExercise != null)
            {
                routineExercise.WorkoutRoutine = WorkoutRoutineSetCollection.Format(post.WorkoutRoutine);
                routineExercise.SequenceNumber = post.SequenceNumber;

                _service.SaveWorkoutRoutines();

                return Json(new { message = "Saved" });
            }

            return Json(new { message = "Cannot Update Routine" });
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(string id, int day)
        {
            WorkoutRoutineExerciseForm form = GetCreateForm(id, day);

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(string id, int day, WorkoutRoutineExerciseForm post)
        {
            WorkoutRoutineExerciseForm form = GetCreateForm(id, day);

            if (ModelState.IsValid)
            {
                form.Routine.RoutineExercises.Add(day, post.RoutineExercise.ExerciseId, post.RoutineExercise.WorkoutRoutine);

                _service.SaveWorkoutRoutines();

                AddFlashMessage(FlashMessageType.Success, $"Successfully added to Day {day}");

                return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
            }

            return View(form);
        }

        private WorkoutRoutineExerciseForm GetCreateForm(string id, int day)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id),
                RoutineExercise = new WorkoutRoutineExercise() { WeekId = 1, DayId = day, WorkoutRoutine = "12RM, 9RM, 6RM" }
            };

            form.Exercises = form.Routine
                .AllExercises
                .NotIncluding(form.Routine.RoutineExercises.Find(1, day, ""))
                .ToList();

            return form;
        }

        #endregion

        #region Delete Methods

        [HttpGet, Route("Delete")]
        public virtual ActionResult Delete(string id, int day, string exercise)
        {
            WorkoutRoutineExerciseForm form = GetDeleteForm(id, day, exercise);

            return View(form);
        }

        [HttpPost, Route("Delete"), ValidateAntiForgeryToken]
        public virtual ActionResult Delete(string id, int day, string exercise, WorkoutRoutineExercise post)
        {
            WorkoutRoutineExerciseForm form = GetDeleteForm(id, 0, "");

            form.Routine.RoutineExercises.DeleteWorkoutRoutineExercise(day, exercise);

            _service.SaveWorkoutRoutines();

            AddFlashMessage(FlashMessageType.Success, $"Exercise successfully removed from Day {day}");

            return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
        }

        private WorkoutRoutineExerciseForm GetDeleteForm(string id, int day, string exercise)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id)
            };

            if (day > 0 || exercise.IsNotEmpty())
            {
                form.RoutineExercise = form.Routine.RoutineExercises.Find(1, day, exercise).FirstOrDefault();
            }

            return form;
        }

        #endregion

        #region Copy Methods

        [HttpGet, Route("Copy")]
        public virtual ActionResult Copy(string id, int week, int day, string exercise)
        {
            //WorkoutRoutineExerciseForm form = GetCopyForm(id, day, exercise);

            //string workoutRoutine = form.Routine
            //    .RoutineExercises
            //    .Where(x => x.WeekId == week)
            //    .Select(x => x.WorkoutRoutine)
            //    .First();

            //foreach (WorkoutRoutineExercise e in form.Routine.RoutineExercises)
            //{
            //    if (e.WeekId > week)
            //    {
            //        e.WorkoutRoutine = workoutRoutine;
            //    }
            //}

            //_service.SaveWorkoutRoutineExercises(form.Routine.RoutineExercises);

            //AddFlashMessage(FlashMessageType.Success, "Copied workout routine forward successfully");

            //return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, week));
            throw new NotImplementedException();
        }

        //private WorkoutRoutineExerciseForm GetCopyForm(string id, int day, string exercise)
        //{
        //    WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
        //    {
        //        Routine = _service.GetWorkoutRoutine(id)
        //    };

        //    _actor.VerifyCan(SecurityAccess.Update, form.Routine);

        //    _service.AttachWorkoutRoutineExercises(form.Routine, 0, day, exercise);

        //    return form;
        //}

        #endregion
    }
}

