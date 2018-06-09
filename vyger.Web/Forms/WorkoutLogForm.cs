using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vyger.Common.Models;

namespace vyger.Web.Models
{
    public class WorkoutLogForm
    {
        public WorkoutLogForm() { }

        public WorkoutPlan Plan { get; set; }

        public WorkoutPlanCycle Cycle { get; set; }

        [DisplayName("Workout Date"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime LogDate { get; set; }

        public IList<WorkoutLog> Logs { get; set; }
    }
}