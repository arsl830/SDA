using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using CMS.classes;
using CMS.Services;
namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Table T1)
        {
            if (ModelState.IsValid)
            {
                dbserv db = new dbserv();
                var credentials= db.Login(T1);
                if (credentials == null)
                {
                    TempData["Msg1"] = "Wrong Email or Password";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    calculate cal = new calculate();
                    TempData["val"]=cal.compute(credentials.Identifier);
                    if(credentials.Identifier=="student")
                    {
                        authlogin auth = new authlogin();
                        string value=auth.loginauth(credentials.Identifier);
                        TempData["data"] = value;
                        TempData["email"] = T1.Email;
                        return RedirectToAction("student", "Home");
                    }
                    else if(credentials.Identifier=="teacher")
                    {
                        authlogin auth = new authlogin();
                        string value = auth.loginauth(credentials.Identifier);
                        TempData["data"] = value;
                        return RedirectToAction("teacher", "Home");
                    }
                    return View();
                }
            }
            else
            {
                return View(T1);
            }
        }
        public ActionResult Addstudent()
        {
            List<Table> listAllstudents;
            List<Table> newlist=new List<Table>();
            dbserv db = new dbserv();
            listAllstudents = db.getallstudents();
            for (var i=0;i<listAllstudents.Count;i++)
            {
                if (listAllstudents[i].Identifier == "student")
                {
                    newlist.Add(listAllstudents[i]);
                }
            }
                return View(newlist);
        }
        [HttpPost]
        public ActionResult Addstudent(Teacher T1)
        {   
            if (ModelState.IsValid)
            {
                dbserv db = new dbserv();
                db.addnewstudent(T1);
                ModelState.Clear();
                return RedirectToAction("teacher", "Home");
            }
            else
            {
                return View(T1);
            }
        }
        public ActionResult student()
        {
            string val = TempData["email"].ToString();
            List<Course> listofsubjects;
            List<Course> newlist = new List<Course>();
            dbserv db = new dbserv();
            listofsubjects = db.getlistofsubjects();
            for (var i = 0; i < listofsubjects.Count; i++)
            {
                if (listofsubjects[i].Email == val)
                {
                    newlist.Add(listofsubjects[i]);
                }
            }
            if(newlist!=null)
            {
                Records rec = new Records();
                string vall=rec.totalrec(newlist.Count.ToString());
                TempData["data1"] = vall;
                return View(newlist);
            }
            else if(newlist==null)
            {
                TempData["not"] = "hello";
                return View();
            }
            else
            {
                return View();
            }
        }
        public async Task<ActionResult> teacherAsync()
        {
            Records rec = new Records();
            List<Teacher> listofstudents;
            dbserv dbb = new dbserv();
            listofstudents=dbb.getlist();
            if(listofstudents!=null)
            {
                string count = rec.totalrec(listofstudents.Count.ToString());
                TempData["data1"] = count;
                return View(listofstudents);
            }
            else
            {
                return View();
            }    
        }
        public ActionResult AssignCourse()
        {
            List<Teacher> listofstudents;
            dbserv db = new dbserv();
            listofstudents=db.getlist();
            if (listofstudents != null)
            {
                return View(listofstudents);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AssignCourse(Course C)
        {
            if (ModelState.IsValid)
            {
                dbserv dbbb = new dbserv();
                dbbb.assigncourse(C);
                ModelState.Clear();
                return RedirectToAction("teacher", "Home");
            }
            else
            {
                return View(C);
            }
        }
        public ActionResult delete(int num)
        {
            try
            {
                dbserv db = new dbserv();
                db.deletestudent(num);
                return RedirectToAction("teacher", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
