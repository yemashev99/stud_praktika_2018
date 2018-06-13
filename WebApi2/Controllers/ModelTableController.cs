using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class ModelTableController : Controller
    {
        private WebApi2Context db = new WebApi2Context();

        public ActionResult Students()
        {
            return View(db.Students.ToList());
        }

        public ActionResult Towns()
        {
            return View(db.Towns.ToList());
        }
    }
}
