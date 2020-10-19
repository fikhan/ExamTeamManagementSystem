using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ExamTeamManagementSystem.Models;
using ExamTeamManagementSystem.Models.BLL;

namespace ExamTeamManagementSystem.Controllers
{
    public class LogisticsSupportTeamController : Controller
    {
        // GET: LogisticsSupportTeam
        ExamTeamManagementSystemEntities _db;

        public LogisticsSupportTeamController()
        {
            _db = new ExamTeamManagementSystemEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        //LogisticsSupportTeam Pages 

        public ActionResult LogisticsHomePage()
        {
            return View();
        }

        public ActionResult LogisticsIssuePage()
        {
            ViewData["LogIssues"] = _db.LogisticsIssues.ToList();
            return View();
        }

        public ActionResult LogisticsissueUpdate(int id)
        {
            return RedirectToAction("LogisticsIssueDetailPage", new { i = id });

        }

        [HttpPost]
        public ActionResult FormUpdateLog(LogList lo)
        {
            LogList t = new LogList();
            StringBuilder sb = new StringBuilder();
            Debug.WriteLine("I am inside the function");
            foreach (var item in lo.ls)
            {
                Debug.WriteLine("Checked value" + item.IsChecked);
                if (item.IsChecked)
                {
                    Debug.WriteLine("Checked value" + item.IsChecked);
                    int ide = int.Parse(Request.Form["LogID"]);
                    var query = from tb in _db.LogisticsIssues
                                where tb.LogisticsIssueID == ide
                                select tb;


                    foreach (LogisticsIssue ti in query)
                    {
                        ti.Status = item.Text;
                    }
                    try
                    {
                        _db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        // Provide for exceptions.
                    }
                }


            }

            return RedirectToAction("LogisticsFinalPage");
        }
        /*public ActionResult LogisticsIssueUpdate()
        {
            LogList ls = new LogList();

        }*/
        public ActionResult LogisticsIssueDetailPage(int i)
        {
            List<ActionSelectionLog> pls = new List<ActionSelectionLog>()
            {
               new ActionSelectionLog { Text = "Solved", Value = 1, IsChecked = false },
               new ActionSelectionLog { Text = "Unsolved", Value = 2, IsChecked = false },
             
            };

            LogList lo = new LogList();
            lo.ls = pls;

            LogisticsIssue lc = (from log in _db.LogisticsIssues
                                 where log.LogisticsIssueID == i
                                 select log).FirstOrDefault();
            ViewData["LogisticsIssueID"] = lc.LogisticsIssueID;
            ViewData["LabNo"] = lc.LabNo;
            ViewData["PCNo"] = lc.PCNo;
            ViewData["ProblemType"] = lc.ProblemType;
            ViewData["RecievedTime"] = lc.Time_Log;
            ViewData["Priority"] = lc.Priority;
            ViewData["Status"] = lc.Status;
            return View(lo);
        }

        public ActionResult LogisticsFinalPage()
        {
            return View();
        }
    }
}