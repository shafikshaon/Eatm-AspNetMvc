using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eatm.Controllers
{
    public class EatmController : Controller
    {
        // GET: Eatm
        public ActionResult Index()
        {
            return View();
        }
    }
}