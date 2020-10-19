using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTeamManagementSystem.Models.BLL
{
    public class TechnicalIssueBLL
    {
        public RepositoryLayer reptech;
        public LabBLL labtech;
        public TechList ProblemType;
        public DateTime date_log;
        public DateTime time_log;
        public string Status;
        public string Priority;

        public TechnicalIssueBLL()
        {
            ProblemType = new TechList();
            ProblemType.techIs = new List<TechIssues>()
            {
               new TechIssues { Text = "Exam Software Update File Missing", Value = 1, IsChecked = false },
               new TechIssues { Text = "Monitor Compatibility Issue", Value = 2, IsChecked = false },
               new TechIssues { Text = "Keyboard Issue", Value = 2, IsChecked = false },
               new TechIssues { Text = "Mouse Issue", Value = 2, IsChecked = true },
               new TechIssues { Text = "Internet Issue", Value = 1, IsChecked = false },
               new TechIssues { Text = "AntiVirus Issue", Value = 2, IsChecked = false },
               new TechIssues { Text = "Hanging Issue", Value = 2, IsChecked = false },
               new TechIssues { Text = "PC Boot Error", Value = 2, IsChecked = false },
               new TechIssues { Text = "Headphone Drivers", Value = 2, IsChecked = false },
               new TechIssues { Text = "PC Slow", Value = 2, IsChecked = false },
               new TechIssues { Text = "E - Podium", Value = 2, IsChecked = false },
               new TechIssues { Text = "Audio Issues", Value = 2, IsChecked = false },
               new TechIssues { Text = "Others", Value = 2, IsChecked = false },
            };
        }
        public List<TechnicalIssueBLL> AllTechIssuesTech()
        {
            List<TechnicalIssueBLL> tech = new List<TechnicalIssueBLL>();

            return tech;
        }

        public void CreateNewIssue()
        {

        }

        public void UpdateTechIssue()
        {

        }
    }
}