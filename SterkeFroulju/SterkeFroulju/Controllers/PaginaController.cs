using SterkeFroulju.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterkeFroulju.Controllers
{
    public class PaginaController : Controller
    {
        public ActionResult DisplayPage(string URL)
        {
            return View(PaginaHandler.instance.GetPageByURLOrDefault(URL));
        }
    }
}
