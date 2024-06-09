using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentWebApp.Controllers
{
    public class DemoGitController : Controller
    {
        // GET: DemoGit
        public ActionResult Index()
        {
            return View("this is a demo Controller");
        }
    }
}