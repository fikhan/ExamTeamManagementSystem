using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExamTeamManagementSystem.Models.BLL;
using ExamTeamManagementSystem.Models;

namespace ExamTeamManagementSystem.Models.DashboardClasses
{
    public class ExamTeamDashboard
    {
        TechnicalIssueBLL tbll;
        LabBLL lab;
        LogisticsIssueBLL lbll;
        List<TechnicalIssueBLL> ltbll;
        List<LabBLL> llab;
        List<LogisticsIssueBLL> libll;

        public List<TechnicalIssueBLL> AllTechIssues()
        {
            ltbll = new List<TechnicalIssueBLL>();

            return ltbll;

        }
        public List<LabBLL> AllLabs()
        {
            llab = new List<LabBLL>();

            return llab;

        }
        public List<LogisticsIssueBLL> AllLogIssues()
        {
            libll = new List<LogisticsIssueBLL>();

            return libll;

        }
        public void CreateNewIssue()
        {

        }
        public void DisplayTechIssues()
        {

        }
        public void DisplayLogisticsIssues()
        {

        }
        public void DisplayAllLabs()
        {

        }
    }
}