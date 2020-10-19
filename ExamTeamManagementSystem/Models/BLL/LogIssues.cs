using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTeamManagementSystem.Models.BLL
{
    public class LogIssues
    {

            public int Value { get; set; }
            public string Text { get; set; }
            public bool IsChecked { get; set; }


    }

    public class PrioritySelectionLog
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }


    }

    public class ActionSelectionLog
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class LogList
    {
            public List<LogIssues> logIs { get; set; }
            public List<PrioritySelectionLog> plIs { get; set; }

            public List<ActionSelectionLog> ls { get; set; }

            public string logdescription { get; set; }
    }

}