using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTeamManagementSystem.Models.BLL
{
    public class TechIssues
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }


    }
    public class PrioritySelection
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }


    }

    public class ActionSelectionTech
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

    public class TechList
    {
        public List<TechIssues> techIs { get; set; }
        public List<PrioritySelection> pIs { get; set; }

        public List<ActionSelectionTech> pls { get; set; }
        public string techdescription { get; set; }

        public int techUpdateId { get; set; }
        public int logUpdateId { get; set; }
    }

 
}