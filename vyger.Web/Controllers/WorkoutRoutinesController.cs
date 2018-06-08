using System;
using System.Collections.Generic;
using System.Security;
using System.Web.Mvc;
using FluentValidation.Mvc;
using FluentValidation.Results;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;
using vyger.Web.Models;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Workouts/Routines"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutRoutinesController : BaseController<WorkoutRoutinesController>
    {
        #region Members

        private ISecurityActor _actor;
        private IWorkoutRoutineService _service;

        #endregion

        #region Constructors

        public WorkoutRoutinesController(
            ISecurityActor actor,
            IWorkoutRoutineService service)
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
        public virtual ActionResult Index()
        {
            IList<WorkoutRoutine> routines = _service.GetWorkoutRoutines();

            return View(routines);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            WorkoutRoutineForm form = new WorkoutRoutineForm();

            _actor.VerifyCan(SecurityAccess.Create, form);

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(WorkoutRoutineForm post)
        {
            _actor.VerifyCan(SecurityAccess.Create, post);

            if (ModelState.IsValid)
            {
                post.Status = StatusTypes.Active;

                _service.AddWorkoutRoutine(post);

                AddFlashMessage(FlashMessageType.Success, "Workout Routine created successfully");

                return RedirectToAction(MVC.WorkoutRoutines.Index());
            }

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id:int}")]
        public virtual ActionResult Edit(int id)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutine(id, SecurityAccess.Update);

            if (routine == null)
            {
                return RedirectToAction(MVC.WorkoutRoutines.Index());
            }

            WorkoutRoutineForm form = new WorkoutRoutineForm(routine);

            return View(form);
        }

        [HttpPost, Route("Edit/{id:int}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, WorkoutRoutineForm post)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutine(id, SecurityAccess.Update);

            if (ModelState.IsValid)
            {
                routine.OverlayWith(post);

                _service.SaveChanges();

                AddFlashMessage(FlashMessageType.Success, "Workout Routine saved successfully");

                return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
            }

            return View(post);
        }

        #endregion
    }
}

