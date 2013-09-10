using Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [DiagnosticFilter]
        public ActionResult Index()
        {
            return View();
        }

    }
}
