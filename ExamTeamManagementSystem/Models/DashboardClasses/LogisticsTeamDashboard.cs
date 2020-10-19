using ExamTeamManagementSystem.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExamTeamManagementSystem.Models;

namespace ExamTeamManagementSystem.Models.DashboardClasses
{
    public class LogisticsTeamDashboard
    {
        RepositoryLayer rep;
        LabBLL l;
        List<LogisticsIssueBLL> lo;

        public void CreateNewIssue()
        {

        }

        public List<LogisticsIssueBLL> DisplayLogIssues()
        {
            lo = new List<LogisticsIssueBLL>();

            return lo;

        }

        public void TakeActionIssue()
        {

        }

    }
}