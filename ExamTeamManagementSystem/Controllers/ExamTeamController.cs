using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using ExamTeamManagementSystem.Models;
using ExamTeamManagementSystem.Models.BLL;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using LinqToExcel;
using QRCoder;
using System.Data.SqlClient;
using System.Drawing;
//using ImportExceData.Models;

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
            using ( var context = new ExamTeamManagementSystemEntities())
            {
                var data = context.TechnicalIssues.SqlQuery("[dbo].[SelectAllTechIssues]").ToList();
                Debug.WriteLine("Count Tech Issue" + data.Count());
            }
            
            Debug.WriteLine("Username" + u.UserName);
            Debug.WriteLine("Password" + u.Password);
            Debug.WriteLine("Selected Role" + u.URoleList);
            var collist = from s in _db.Users
                          where s.UserName == u.UserName && s.Password == u.Password && s.UserRole == u.URoleList.ToString()
                          select s;
            Debug.WriteLine("ReturnedRows" + collist.Count());

            if(collist.Count() > 0)
            {
                Session["Username"] = u.UserName; 
                if(u.URoleList.ToString() == "ExamTeam")
                {
                    return RedirectToAction("ExamTeamHomePage");
                }
                else if( u.URoleList.ToString() == "ITUnitTeam")
                {
                    return RedirectToAction("ITHomePage","ITUnitTeam");
                }
                else if ( u.URoleList.ToString() == "LogisticsSupportTeam")
                {
                    Debug.WriteLine("I am in LogisticsTeam");
                    return RedirectToAction("LogisticsHomePage", "LogisticsSupportTeam");
                }
                return RedirectToAction("ExamTeamHomePage");
            }
            else
            {
                return RedirectToAction("LoginFailurePage");
            }
        } 

        public ActionResult LoginFailurePage()
        {
            return View();
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
            tc.Comments = tci.techdescription;
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
            tc.Comments = lci.logdescription;
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

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadExcel(StudentFile users, HttpPostedFileBase FileUpload)
        {
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath(null);
                    Debug.WriteLine("File path" + targetpath);
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<StudentFile>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.StudentName != "" && a.Day != "" && a.Time != "")
                            {

                                StudentFile TU = new StudentFile();
                                TU.SN = a.SN;
                                TU.UniversityID = a.UniversityID;
                                TU.StudentName = a.StudentName;
                                TU.Section = a.Section;
                                TU.Day = a.Day;
                                TU.Date = a.Date;
                                TU.Time = a.Time;
                                TU.Lab = a.Lab;
                                TU.Building = a.Building;
                                TU.Floor = a.Floor;
                                TU.Course = a.Course;
                                TU.FullDate = a.FullDate;
                                TU.Password = a.Password;
                                _db.StudentFiles.Add(TU);

                                _db.SaveChanges();



                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.StudentName == "" || a.StudentName == null) data.Add("<li> name is required</li>");
                                if (a.Day == "" || a.Day == null) data.Add("<li> Day is required</li>");
                                if (a.Time == "" || a.Time == null) data.Add("<li>Time is required</li>");

                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("Successfully Uploaded", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult StudentSearch()
        {
            ViewBag.StudentID = null;
            ViewBag.StudentName = null;
            ViewBag.Section = null;
            ViewBag.Day = null;
            ViewBag.Date = null;
            ViewBag.Time = null;
            ViewBag.Lab = null;
            ViewBag.Building = null;
            ViewBag.Floor = null;
            ViewBag.Course = null;
            ViewBag.FullDate = null;
            ViewBag.Password = null;
            return View();
        }

        [HttpPost]
        public ActionResult StudentSearchResult(StudentFile fl, string returnUrl)
        {
            var result =_db.StudentFiles.SingleOrDefault(u => u.UniversityID == fl.UniversityID);
            Debug.WriteLine("The result" + result.StudentName);
            ViewBag.StudentID = result.UniversityID.ToString();
            ViewBag.StudentName = result.StudentName.ToString();
            ViewBag.Section = result.Section.ToString();
            ViewBag.Day = result.Day.ToString();
            ViewBag.Date = result.Date.ToString();
            ViewBag.Time = result.Time.ToString();
            ViewBag.Lab = result.Lab.ToString();
            ViewBag.Building = result.Building.ToString();
            ViewBag.Floor = result.Floor.ToString();
            ViewBag.Course = result.Course.ToString();
            ViewBag.FullDate = result.FullDate.ToString();
            ViewBag.Password = result.Password.ToString();
            return View("StudentSearch");
        }

        public ActionResult GenerateQRCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateQRCode1(string txtQRCode)
        {
            ViewBag.txtQRCode = txtQRCode;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //imgBarCode.Height = 150;
            //imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
            }
            return View("GenerateQRCode");
        }

        public ActionResult StudentSearch1()
        {
            ViewBag.StudentID = null;
            ViewBag.StudentName = null;
            ViewBag.Section = null;
            ViewBag.Day = null;
            ViewBag.Date = null;
            ViewBag.Time = null;
            ViewBag.Lab = null;
            ViewBag.Building = null;
            ViewBag.Floor = null;
            ViewBag.Course = null;
            ViewBag.FullDate = null;
            ViewBag.Password = null;
            return View();
        }

        [HttpPost]
        public ActionResult StudentSearchResult1(StudentFile fl, string returnUrl)
        {
            var result = _db.StudentFiles.SingleOrDefault(u => u.UniversityID == fl.UniversityID);
            Debug.WriteLine("The result" + result.StudentName);
            ViewBag.StudentID = result.UniversityID.ToString();
            ViewBag.StudentName = result.StudentName.ToString();
            ViewBag.Section = result.Section.ToString();
            ViewBag.Day = result.Day.ToString();
            ViewBag.Date = result.Date.ToString();
            ViewBag.Time = result.Time.ToString();
            ViewBag.Lab = result.Lab.ToString();
            ViewBag.Building = result.Building.ToString();
            ViewBag.Floor = result.Floor.ToString();
            ViewBag.Course = result.Course.ToString();
            ViewBag.FullDate = result.FullDate.ToString();
            ViewBag.Password = result.Password.ToString();
            return View("StudentSearch1");
        }
    }
}
