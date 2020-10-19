using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExamTeamManagementSystem.Models.BLL;

namespace ExamTeamManagementSystem.Models.DashboardClasses
{
    public class ITUnitTeamDashboard
    {
        RepositoryLayer repit;
        LabBLL lit;
        List<TechnicalIssueBLL> tit;

        public void CreateNewIssue()
        {

        }

        public List<TechnicalIssueBLL> DisplayTechIssues()
        {
            tit = new List<TechnicalIssueBLL>();

            return tit;

        }

        public void TakeActionIssue()
        {

        }
    }
}