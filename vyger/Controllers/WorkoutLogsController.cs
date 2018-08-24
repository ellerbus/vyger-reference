using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Services;
using vyger.ViewModels;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Logs"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutLogsController : BaseController
    {
        #region Members

        private IWorkoutLogService _service;
        private IExerciseService _exercises;
        private IExerciseGroupService _groups;
        private IExerciseCategoryService _categories;

        #endregion

        #region Constructors

        public WorkoutLogsController(
            IWorkoutLogService service,
            IExerciseService exercises,
            IExerciseGroupService groups,
            IExerciseCategoryService categories)
        {
            _service = service;
            _exercises = exercises;
            _groups = groups;
            _categories = categories;
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("Details")]
        public virtual ActionResult Details(DateTime date)
        {
            WorkoutLogDetailViewModel vm = new WorkoutLogDetailViewModel();

            vm.LogDate = date;
            vm.Logs = _service.GetWorkoutLogs().Filter(vm.LogDate).ToList();

            if (vm.Logs.Count == 0)
            {
                AddFlashMessage(FlashMessageType.Info, "Add an Exercise to the Workout Log");

                return RedirectToAction("Create", new { date = date.ToYMD() });
            }

            return View(vm);
        }

        [HttpPost, Route("Details"), ValidateAntiForgeryToken]
        public virtual ActionResult Details(WorkoutLogDetailViewModel post, DateTime date)
        {
            if (ModelState.IsValid)
            {
                WorkoutLogCollection allLogs = _service.GetWorkoutLogs();

                IDictionary<string, WorkoutLog> logs = allLogs.Filter(date).ToDictionary(x => x.ExerciseId, x => x);

                //  add or update logs
                foreach (WorkoutLog log in post.Logs)
                {
                    WorkoutLog target = null;

                    if (logs.TryGetValue(log.ExerciseId, out target))
                    {
                        target.OverlayWith(log);

                        logs.Remove(log.ExerciseId);
                    }
                    else
                    {
                        //  not found
                        log.LogDate = date;

                        target = log;

                        allLogs.Add(target);
                    }
                }

                //  do we have any to remove
                if (logs.Count > 0)
                {
                    foreach (WorkoutLog log in logs.Values)
                    {
                        allLogs.Remove(log);
                    }
                }

                _service.SaveWorkoutLogs();

                AddFlashMessage(FlashMessageType.Success, $"Workout Log saved for {date:d}");

                return RedirectToAction("Details", new { date = date.ToYMD() });
            }

            ExerciseCollection exercises = _exercises.GetExercises();

            foreach (var log in post.Logs)
            {
                log.Exercise = exercises.GetByPrimaryKey(log.ExerciseId);
            }

            WorkoutLogDetailViewModel vm = new WorkoutLogDetailViewModel();

            vm.LogDate = date;
            vm.Logs = post.Logs;

            return View(vm);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(DateTime date)
        {
            WorkoutLogCreateViewModel vm = new WorkoutLogCreateViewModel();

            vm.Exercises = _exercises.GetExercises();
            vm.Groups = _groups.GetExerciseGroups();
            vm.Categories = _categories.GetExerciseCategories();
            vm.LogDate = date;

            return View(vm);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(WorkoutLog post, DateTime date)
        {
            if (ModelState.IsValid)
            {
                IDictionary<string, WorkoutLog> logs = _service
                    .GetWorkoutLogs()
                    .Filter(date)
                    .ToDictionary(x => x.ExerciseId, x => x);

                if (logs.ContainsKey(post.ExerciseId))
                {
                    AddFlashMessage(FlashMessageType.Warning, $"Exercise was already logged");
                }
                else
                {
                    post.LogDate = date;

                    post.SequenceNumber = logs.Count;

                    _service.GetWorkoutLogs().Add(post);

                    _service.SaveWorkoutLogs();

                    AddFlashMessage(FlashMessageType.Success, "Exercise logged successfully");
                }

                return RedirectToAction("Details", new { date = date.ToYMD() });
            }

            WorkoutLogCreateViewModel vm = new WorkoutLogCreateViewModel();

            vm.Exercises = _exercises.GetExercises();
            vm.Groups = _groups.GetExerciseGroups();
            vm.Categories = _categories.GetExerciseCategories();
            vm.LogDate = date;
            vm.ExerciseId = post.ExerciseId;
            vm.Workout = post.Workout;

            Exercise exercise = null;

            _exercises.GetExercises().TryGetByPrimaryKey(vm.ExerciseId, out exercise);

            vm.Exercise = exercise;

            return View(vm);
        }

        #endregion

        //#region Create Methods

        //[HttpGet, Route("Plans/{plan}")]
        //public virtual ActionResult Plans(string plan, int cycle, int week, int day)
        //{
        //    WorkoutLogForm form = GetWorkoutPlanLogForm(plan, cycle, week, day);

        //    return View(form);
        //}

        //[HttpPost, Route("Plans/{plan}"), ValidateAntiForgeryToken]
        //public virtual ActionResult Plans(string plan, int cycle, int week, int day, WorkoutLogForm post)
        //{
        //    WorkoutLogForm form = GetWorkoutPlanLogForm(plan, cycle, week, day);

        //    form.LogDate = post.LogDate.Date;

        //    form.Cycle.Status = StatusTypes.Active;

        //    WorkoutLogCollection logs = _service.GetWorkoutLogs();

        //    logs.RemoveAll(form.LogDate);

        //    foreach (WorkoutLog log in post.Logs)
        //    {
        //        log.LogDate = form.LogDate;
        //        log.Workout = WorkoutLogSetCollection.Format(log.Workout);

        //        logs.Add(log);
        //    }

        //    IEnumerable<WorkoutPlanLog> planLogs = form.Cycle.PlanLogs
        //        .Where(x => x.WeekId == week && x.DayId == day);

        //    foreach (WorkoutPlanLog planLog in planLogs)
        //    {
        //        planLog.Status = StatusTypes.Complete;
        //    }

        //    _service.SaveWorkoutLogs();

        //    _plans.SaveWorkoutPlans();

        //    AddFlashMessage(FlashMessageType.Success, $"Workout Log saved for {post.LogDate:d}");

        //    return RedirectToAction(MVC.WorkoutLogs.Index(post.LogDate.Date));
        //}

        //private WorkoutLogForm GetWorkoutPlanLogForm(string plan, int cycle, int week, int day)
        //{
        //    WorkoutLogForm form = new WorkoutLogForm();

        //    form.LogDate = DateTime.Today;

        //    form.Cycle = _plans
        //        .GetWorkoutPlans()
        //        .GetByPrimaryKey(plan)
        //        .Cycles
        //        .GetByPrimaryKey(cycle);

        //    form.Logs = form.Cycle
        //        .PlanLogs
        //        .Where(x => x.WeekId == week && x.DayId == day)
        //        .Select(x => new WorkoutLog(x))
        //        .OrderBy(x => x.SequenceNumber)
        //        .ToList();

        //    return form;
        //}

        //#endregion

        #region Json Query Methods

        [HttpGet, Route("Events")]
        public virtual ActionResult Events(DateTime start, DateTime end)
        {
            var events = _service.GetWorkoutLogs()
                .Filter(start, end)
                .Select(x => x.LogDate.ToYMD())
                .Distinct()
                .Select(x => new { start = x, title = "Workout Details", url = Url.Action("Details", new { date = x }) });

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}