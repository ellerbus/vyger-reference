using System.Linq;
using System.Net;
using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;
using vyger.Web.Forms;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Workouts/Routines/{id:int}/Exercises"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutRoutineExercisesController : BaseController
    {
        #region Members

        private ISecurityActor _actor;
        private IWorkoutRoutineExerciseService _service;

        #endregion

        #region Constructors

        public WorkoutRoutineExercisesController(
            ISecurityActor actor,
            IWorkoutRoutineExerciseService service)
        {
            _actor = actor;
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
        public virtual ActionResult Index(int id, int week)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutine(id);

            _actor.VerifyCan(SecurityAccess.View, routine);

            _service.AttachWorkoutRoutineExercises(routine, week, 0, 0);

            return View(routine);
        }

        //[HttpPost, Route("Index")]
        //public virtual ActionResult Index(int id, int week, WorkoutRoutineExerciseForm post)
        //{
        //    _service.MergeExercisesWith(id, week, post.Routine.RoutineExercises);

        //    AddFlashMessage(FlashMessageType.Success, $"Saved Exercises for Week {week}");

        //    return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, week));
        //}

        #endregion

        #region Edit Methods

        [HttpPost, Route("Edit"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, int week, int day, int exercise, WorkoutRoutineExercise post)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutine(id);

            if (_actor.Can(SecurityAccess.Update, routine))
            {
                post.RoutineId = routine.RoutineId;
                post.WeekId = week;
                post.DayId = day;
                post.ExerciseId = exercise;

                WorkoutRoutineExercise routineExercise = _service.SaveWorkoutRoutineExercise(post);

                return Json(routineExercise);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            Response.StatusDescription = "Cannot Update Routine";

            return Json(new { message = "Cannot Update Routine" });
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(int id, int day)
        {
            WorkoutRoutineExerciseForm form = GetCreateForm(id, day);

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(int id, int day, WorkoutRoutineExerciseForm post)
        {
            WorkoutRoutineExerciseForm form = GetCreateForm(id, day);

            if (ModelState.IsValid)
            {
                _service.AddWorkoutRoutineExercise(form.Routine, day, post.RoutineExercise.ExerciseId, post.RoutineExercise.WorkoutRoutine);

                AddFlashMessage(FlashMessageType.Success, $"Successfully added to Day {day}");

                return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
            }

            return View(form);
        }

        private WorkoutRoutineExerciseForm GetCreateForm(int id, int day)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutine(id),
                RoutineExercise = new WorkoutRoutineExercise() { WeekId = 1, DayId = day, WorkoutRoutine = "12RM, 9RM, 6RM" }
            };

            _actor.VerifyCan(SecurityAccess.Update, form.Routine);

            _service.AttachWorkoutRoutineExercises(form.Routine, 1, day, 0);

            //  now just trim the exercises to ones that aren't in "day"
            foreach (Exercise ex in form.Routine.RoutineExercises.Select(x => x.Exercise))
            {
                form.Routine.AllExercises.Remove(ex);
            }

            return form;
        }

        #endregion

        #region Delete Methods

        [HttpGet, Route("Delete")]
        public virtual ActionResult Delete(int id, int day, int exercise)
        {
            WorkoutRoutineExerciseForm form = GetDeleteForm(id, day, exercise);

            return View(form);
        }

        [HttpPost, Route("Delete"), ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int id, int day, int exercise, WorkoutRoutineExerciseForm post)
        {
            WorkoutRoutineExerciseForm form = GetDeleteForm(id, 0, 0);

            _service.DeleteWorkoutRoutineExercise(form.Routine, day, exercise);

            AddFlashMessage(FlashMessageType.Success, $"Exercise successfully removed from Day {day}");

            return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
        }

        private WorkoutRoutineExerciseForm GetDeleteForm(int id, int day, int exercise)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutine(id)
            };

            _actor.VerifyCan(SecurityAccess.Update, form.Routine);

            if (day > 0 || exercise > 0)
            {
                _service.AttachWorkoutRoutineExercises(form.Routine, 1, day, exercise);

                form.RoutineExercise = form.Routine.RoutineExercises.First();
            }

            return form;
        }

        #endregion

        #region Copy Methods

        [HttpGet, Route("Copy")]
        public virtual ActionResult Copy(int id, int week, int day, int exercise)
        {
            WorkoutRoutineExerciseForm form = GetCopyForm(id, day, exercise);

            string workoutRoutine = form.Routine
                .RoutineExercises
                .Where(x => x.WeekId == week)
                .Select(x => x.WorkoutRoutine)
                .First();

            foreach (WorkoutRoutineExercise e in form.Routine.RoutineExercises)
            {
                if (e.WeekId > week)
                {
                    e.WorkoutRoutine = workoutRoutine;
                }
            }

            _service.SaveWorkoutRoutineExercises(form.Routine.RoutineExercises);

            AddFlashMessage(FlashMessageType.Success, "Copied workout routine forward successfully");

            return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, week));
        }

        private WorkoutRoutineExerciseForm GetCopyForm(int id, int day, int exercise)
        {
            WorkoutRoutineExerciseForm form = new WorkoutRoutineExerciseForm()
            {
                Routine = _service.GetWorkoutRoutine(id)
            };

            _actor.VerifyCan(SecurityAccess.Update, form.Routine);

            _service.AttachWorkoutRoutineExercises(form.Routine, 0, day, exercise);

            return form;
        }

        #endregion
    }
}

