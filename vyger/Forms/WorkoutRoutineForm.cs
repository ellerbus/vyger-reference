using System.Collections.Generic;
using System.Web.Mvc;
using Augment;
using vyger.Models;

namespace vyger.Forms
{
    public class WorkoutRoutineForm : WorkoutRoutine
    {
        public WorkoutRoutineForm() { }

        public WorkoutRoutineForm(WorkoutRoutine routine) : base(routine) { }

        public IEnumerable<SelectListItem> GetWeekSelectListItems()
        {
            for (int i = 0; i < 9; i++)
            {
                yield return new SelectListItem()
                {
                    Value = (i + 1).ToString(),
                    Text = "{0} Week{1} per Cycle".FormatArgs(i + 1, i > 1 ? "s" : ""),
                    Selected = (i + 1) == Weeks
                };
            }
        }

        public IEnumerable<SelectListItem> GetDaySelectListItems()
        {
            for (int i = 0; i < 7; i++)
            {
                yield return new SelectListItem()
                {
                    Value = (i + 1).ToString(),
                    Text = "{0} Day{1} per Week".FormatArgs(i + 1, i > 1 ? "s" : ""),
                    Selected = (i + 1) == Days
                };
            }
        }
    }
}