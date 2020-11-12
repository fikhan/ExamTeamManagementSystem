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
                        ti.Comments = lo.logdescription;
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

        public ActionResult BuildingSelectionPageLog()
        {
            return View();
        }
        public ActionResult CreateNewLogIssue(string labno)
        {
            List<LogIssues> l = new List<LogIssues>()
            {
               new LogIssues { Text = "Cleaning Issue", Value = 1, IsChecked = false },
               new LogIssues { Text = "Lighting Issue ", Value = 2, IsChecked = false },
               new LogIssues { Text = "Air Conditioning Issue", Value = 2, IsChecked = false },
               new LogIssues { Text = "Carpet Issue", Value = 2, IsChecked = false },
               new LogIssues { Text = "Chair Issue", Value = 1, IsChecked = false },
               new LogIssues { Text = "Table Issue", Value = 2, IsChecked = false },
               new LogIssues { Text = "Others", Value = 2, IsChecked = false },

            };

            List<PrioritySelectionLog> pl = new List<PrioritySelectionLog>()
            {
                new PrioritySelectionLog { Text = "High", Value = 1, IsChecked = false },
                new PrioritySelectionLog { Text = "Medium", Value = 1, IsChecked = false },
                new PrioritySelectionLog { Text = "Low", Value = 1, IsChecked = false },
            };

            LogList tb = new LogList();
            tb.logIs = l;
            tb.plIs = pl;
            ViewData["LabNo"] = labno;
            return View(tb);
        }

        public ActionResult LogOutAction()
        {
            if (Session["Username"] != null)
            {
                Session.Abandon();
                Debug.WriteLine("Session Username" + Session["Username"]);
                return View();
            }
            else
            {
                return View();
            }


        }
        public ActionResult LogFinalSubmissionPage(string s)
        {
            ViewBag.TechIssue = "Your technical issues is recorded your Issue ID is" + s;
            
            return View();
        }
    }
}