using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vyger.Core.Models;

namespace vyger.ViewModels
{
    public class WorkoutLogDetailViewModel
    {
        public WorkoutLogDetailViewModel()
        {
        }

        //public WorkoutPlan Plan { get; set; }

        //public WorkoutPlanCycle Cycle { get; set; }

        [DisplayName("Workout Date"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime LogDate { get; set; }

        public IList<WorkoutLog> Logs { get; set; } = new WorkoutLog[0];
    }
}