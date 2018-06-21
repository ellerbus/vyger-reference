using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Forms;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Logs"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutLogsController : BaseController
    {
        #region Members

        private IWorkoutLogService _service;
        private IWorkoutPlanService _plans;

        #endregion

        #region Constructors

        public WorkoutLogsController(
            IWorkoutLogService service,
            IWorkoutPlanService plans)
        {
            _service = service;
            _plans = plans;
        }

        #endregion

        #region "On" Methods

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SecurityException)
            {
                AddFlashMessage(FlashMessageType.Danger, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction(MVC.WorkoutLogs.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(DateTime? date = null)
        {
            WorkoutLogForm form = new WorkoutLogForm();

            form.LogDate = date.GetValueOrDefault(DateTime.Today);
            form.Logs = _service.GetWorkoutLogs().GetWorkoutLogs(form.LogDate);

            return View(form);
        }

        [HttpPost, Route("Index"), ValidateAntiForgeryToken]
        public virtual ActionResult Index(WorkoutLogForm post, DateTime? date = null)
        {
            //WorkoutLogForm form = new WorkoutLogForm();

            //form.LogDate = date.GetValueOrDefault(DateTime.Today);
            //form.Logs = _service.GetWorkoutLogs(form.LogDate);

            //foreach (WorkoutLog log in post.Logs)
            //{
            //    WorkoutLog target = form.Logs.FirstOrDefault(x => x.ExerciseId == log.ExerciseId);

            //    target.OverlayWith(log);
            //}

            //_service.SaveChanges();

            //AddFlashMessage(FlashMessageType.Success, $"Workout Log saved for {post.LogDate:d}");

            //return RedirectToAction(MVC.WorkoutLogs.Index(post.LogDate));
            throw new NotImplementedException();
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Plans/{plan}")]
        public virtual ActionResult Plans(string plan, int cycle, int week, int day)
        {
            WorkoutLogForm form = GetWorkoutPlanLogForm(plan, cycle, week, day);

            return View(form);
        }

        [HttpPost, Route("Plans/{plan}"), ValidateAntiForgeryToken]
        public virtual ActionResult Plans(string plan, int cycle, int week, int day, WorkoutLogForm post)
        {
            WorkoutLogForm form = GetWorkoutPlanLogForm(plan, cycle, week, day);

            form.LogDate = post.LogDate.Date;

            form.Cycle.Status = StatusTypes.Active;

            WorkoutLogCollection logs = _service.GetWorkoutLogs();

            logs.RemoveAll(form.LogDate);

            foreach (WorkoutLog log in post.Logs)
            {
                log.LogDate = form.LogDate;
                log.Workout = WorkoutLogSetCollection.Format(log.Workout);

                logs.Add(log);
            }

            IEnumerable<WorkoutPlanLog> planLogs = form.Cycle.PlanLogs
                .Where(x => x.WeekId == week && x.DayId == day);

            foreach (WorkoutPlanLog planLog in planLogs)
            {
                planLog.Status = StatusTypes.Complete;
            }

            _service.SaveWorkoutPlans();

            _plans.SaveWorkoutPlans();

            AddFlashMessage(FlashMessageType.Success, $"Workout Log saved for {post.LogDate:d}");

            return RedirectToAction(MVC.WorkoutLogs.Index(post.LogDate.Date));
        }

        private WorkoutLogForm GetWorkoutPlanLogForm(string plan, int cycle, int week, int day)
        {
            WorkoutLogForm form = new WorkoutLogForm();

            form.LogDate = DateTime.Today;

            form.Cycle = _plans
                .GetWorkoutPlans()
                .GetByPrimaryKey(plan)
                .Cycles
                .GetByPrimaryKey(cycle);

            form.Logs = form.Cycle
                .PlanLogs
                .Where(x => x.WeekId == week && x.DayId == day)
                .Select(x => new WorkoutLog(x))
                .OrderBy(x => x.SequenceNumber)
                .ToList();

            return form;
        }

        #endregion
    }
}