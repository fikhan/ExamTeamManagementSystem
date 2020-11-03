using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ExamTeamManagementSystem.Models;
using ExamTeamManagementSystem.Models.BLL;

namespace ExamTeamManagementSystem.Controllers
{
    public class ITUnitTeamController : Controller
    {
        ExamTeamManagementSystemEntities _db;
        // GET: ITUnitTeam

        public ITUnitTeamController()
        {
            _db = new ExamTeamManagementSystemEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        //ITUnitTeam Pages 

        public ActionResult ITHomePage()
        {
            return View();
        }

        public ActionResult ITTechnicalIssuePage()
        {
            ViewData["TechIssues"] = _db.TechnicalIssues.ToList();
            return View();
        }

        [HttpPost]
       public ActionResult FormUpdateTech(TechList tl)
        {
            TechList t = new TechList();
            StringBuilder sb = new StringBuilder();
            Debug.WriteLine("I am inside the function");
            foreach(var item in tl.pls)
            {
                Debug.WriteLine("Checked value" + item.IsChecked);
                if (item.IsChecked)
                {
                    Debug.WriteLine("Checked value" + item.IsChecked);
                    int ide = int.Parse(Request.Form["TechID"]);
                    var query = from tb in _db.TechnicalIssues
                                where tb.TechnicalIssueID == ide
                                select tb;


                    foreach (TechnicalIssue ti in query)
                    {
                        ti.Status = item.Text;
                        ti.Comments = tl.techdescription;
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

            return RedirectToAction("ITFinalPage");


        }
        public ActionResult ITTechnicalissueUpdate(int id)
        {
            return RedirectToAction("ITIssueDetailPage", new { i = id });

        }
        public ActionResult ITIssueDetailPage(int i)
        {
            List<ActionSelectionTech> pls1 = new List<ActionSelectionTech>()
            {
               new ActionSelectionTech { Text = "Solved", Value = 1, IsChecked = false },
               new ActionSelectionTech { Text = "Unsolved", Value = 2, IsChecked = false },

            };

            TechList t = new TechList();
            t.pls = pls1;



            TechnicalIssue tc = (from tch in _db.TechnicalIssues
                      where tch.TechnicalIssueID == i
                      select tch).FirstOrDefault();
            ViewData["TechnicalIssueID"] = tc.TechnicalIssueID;
            ViewData["LabNo"] = tc.LabNo;
            ViewData["PCNo"] = tc.PCNo;
            ViewData["ProblemType"] = tc.ProblemType;
            ViewData["RecievedTime"] = tc.Time_Tech;
            ViewData["Priority"] = tc.Priority;
            ViewData["Status"] = tc.Status;
            return View(t);
        }

        public ActionResult ITFinalPage()
        {
            return View();
        }
    }
}