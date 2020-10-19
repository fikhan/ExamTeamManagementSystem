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
    public class ExamTeamController : Controller
    {
        ExamTeamManagementSystemEntities _db;
       

        public ExamTeamController()
        {
            _db = new ExamTeamManagementSystemEntities();
        }
        // GET: ExamTeam  Pages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExamTeamHomePage()
        {
            ViewData["TechIssues"] = _db.TechnicalIssues.ToList();
            ViewData["LogIssues"] = _db.LogisticsIssues.ToList();
            return View();
        }
        public ActionResult ExamTeamLoginPage()
        {
            return View();
        }

        public ActionResult LoginValidation(User u)
        {
            Debug.WriteLine("Username" + u.UserName);
            Debug.WriteLine("Password" + u.Password);
            Debug.WriteLine("Selected Role" + u.URoleList);
            return RedirectToAction("ExamTeamHomePage");
        } 
        public ActionResult CampusSelectionPage()
        {
            return View();
        }
        public ActionResult BuildingSelectionPage()
        {
            return View();
        }

        public ActionResult BuildingSelectionPageTech()
        {
            return View();
        }
        public ActionResult IssuePage()
        {
            return View();
        }

        public ActionResult TechnicalSupportIssuePage(string labno)
        {
            
            List<TechIssues> l = new List<TechIssues>()
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

            List<PrioritySelection> p = new List<PrioritySelection>()
            { 
                new PrioritySelection { Text = "High", Value = 1, IsChecked = false },
                new PrioritySelection { Text = "Medium", Value = 1, IsChecked = false },
                new PrioritySelection { Text = "Low", Value = 1, IsChecked = false },
            };

            TechList tb = new TechList();
            tb.techIs = l;
            tb.pIs = p;
            ViewData["LabNo"] = labno;
            return View(tb);
        }

        [HttpPost]
        public ActionResult FormProcTechSupport(TechList tci)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (var item in tci.techIs)
            {
                Debug.WriteLine("Checked value" + item.IsChecked);
                if (item.IsChecked)
                {
                    //append each checked records into StringBuilder
                    sb.Append(item.Text + ",");

                }


            }

            foreach (var item in tci.pIs)
            {
                Debug.WriteLine("Checked value" + item.IsChecked);
                if (item.IsChecked)
                {
                    //append each checked records into StringBuilder
                    sb1.Append(item.Text + ",");

                }


            }
            DateTime now = DateTime.Now;

            var time24 = DateTime.Now.ToString("HH:mm:ss");

            Debug.WriteLine("The string is " + sb.ToString() + " " + sb1.ToString() + " " + now.Date.ToString());
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(sb.ToString() + " And Priority is " + sb1.ToString() + " The description is " + tci.techdescription + " " + DateTime.Now.ToString("dd/MM/yyyy") + "The time is" + time24);
            TechnicalIssue tc = new TechnicalIssue();
           // tc.TechnicalIssueID = 5;
            tc.BuildingNo = 1;
            tc.LabNo = "1";
            tc.PCNo = 1;
            tc.ProblemType = sb.ToString();
            tc.Priority = sb1.ToString();
            tc.Date_Tech = DateTime.Now.Date;
            tc.Time_Tech = now.TimeOfDay;
            tc.Status = "Not Resolved";
            _db.TechnicalIssues.Add(tc);
            _db.SaveChanges();
            var l = (from tch in _db.TechnicalIssues
                                orderby tch.TechnicalIssueID descending
                                select tch.TechnicalIssueID).First();
          
            string s1 = l.ToString();
            return RedirectToAction("ExamTeamFinalPage",new { s = s1 });
        }

        [HttpPost]
        public ActionResult FormProcLogSupport(LogList lci)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (var item in lci.logIs)
            {
                Debug.WriteLine("Checked value" + item.IsChecked);
                if (item.IsChecked)
                {
                    //append each checked records into StringBuilder
                    sb.Append(item.Text + ",");

                }


            }

            foreach (var item in lci.plIs)
            {
                Debug.WriteLine("Checked value" + item.IsChecked);
                if (item.IsChecked)
                {
                    //append each checked records into StringBuilder
                    sb1.Append(item.Text + ",");

                }


            }
            DateTime now = DateTime.Now;
            var time24 = DateTime.Now.ToString("HH:mm:ss");
            
            Debug.WriteLine("The string is " + sb.ToString() + " " + sb1.ToString());
          //  String s1 = sb.ToString() + " And Priority is "+ sb1.ToString() + " The description is " + lci.logdescription + " " + DateTime.Now.ToString("dd/MM/yyyy") + "The time is" + time24;
            LogisticsIssue tc = new LogisticsIssue();
          //  tc.LogisticsIssueID = 4;
            tc.BuildingNo = 1;
            tc.LabNo = "1";
            tc.PCNo = 1;
            tc.ProblemType = sb.ToString();
            tc.Priority = sb1.ToString();
            tc.Date_Log = DateTime.Now.Date;
            tc.Time_Log = now.TimeOfDay;
            tc.Status = "Not Resolved";
            _db.LogisticsIssues.Add(tc);
            _db.SaveChanges();
            var li = (from tch in _db.LogisticsIssues
                     orderby tch.LogisticsIssueID descending
                     select tch.LogisticsIssueID).First();
            string s1 = li.ToString();
            return RedirectToAction("ExamTeamFinalPage", new { s = s1 });
        }
        public ActionResult TechnicalSupportIssuePageLog(string labno)
        {
            ViewData["LabNo"] = labno;
            return View();
        }


        public ActionResult LogisticsSupportIssuePage(string labno)
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

        public ActionResult ExamTeamFinalPage(string s)
        {
            ViewBag.TechIssue = "Your technical issues is recorded your Issue ID is" + s;
            return View();
        }

        public ActionResult ExamTeamErrorPage()
        {
            return View();
        }

    }
}