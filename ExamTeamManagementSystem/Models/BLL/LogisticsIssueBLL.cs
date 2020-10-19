using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTeamManagementSystem.Models.BLL
{
    public class LogisticsIssueBLL
    {
        RepositoryLayer reptech;
        LabBLL labtech;
        string ProblemType;
        DateTime date_log;
        DateTime time_log;
        string Status;
        string Priority;
        public List<LogisticsIssueBLL> AllTechIssuesTech()
        {
            List<LogisticsIssueBLL> logbll = new List<LogisticsIssueBLL>();

            return logbll;
        }

        public void CreateNewIssue()
        {

        }

        public void UpdateLogIssue()
        {

        }
    }
}