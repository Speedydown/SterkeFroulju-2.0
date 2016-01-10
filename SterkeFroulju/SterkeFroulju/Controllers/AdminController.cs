using SterkeFroulju.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WZWVAPI;

namespace SterkeFroulju.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public System.Web.Mvc.ActionResult Index(int ID = 1)
        {
            if (!UserHandler.IsLoggedIn())
            {
                return RedirectToAction("NotLoggedIn");
            }

            List<Pagina> Paginas = PaginaHandler.instance.GetPaginaList(false);

            foreach (Pagina p in Paginas)
            {
                if (p.ID == ID)
                {
                    ViewData["Pagina"] = p;
                    break;
                }
            }

            ViewData["Paginas"] = Paginas;
            ViewData["Images"] = SterkeFroulju.Models.ImageHandler.instance.GetImageList();

            return View();
        }

        public System.Web.Mvc.ActionResult NotLoggedIn()
        {
            return View("NotLoggedIn");
        }

        [HttpPost]
        public System.Web.Mvc.ActionResult Login(FormCollection collection)
        {
            try
            {
                string UserName = collection.Get("UserName");
                string PassWord = collection.Get("Password");
                UserHandler.UH.LoginUser(UserName, PassWord, false);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("NotLoggedIn");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public System.Web.Mvc.ActionResult AddPagina(FormCollection collection)
        {
            if (UserHandler.IsLoggedIn())
            {
                Pagina pagina = new Pagina(0, collection["Header"], "", DateTime.Now, 1, false);

                return RedirectToAction("Index", new { ID = pagina.ID });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public System.Web.Mvc.ActionResult UpdatePagina(FormCollection collection, int ID)
        {
            if (UserHandler.IsLoggedIn() && ID != 0)
            {
                Pagina pagina = PaginaHandler.instance.GetObjectByID(ID) as Pagina;

                if (pagina != null)
                {
                    PaginaHandler.instance.UpdateObject(new Pagina(
                        ID,
                        collection["Header"],
                        collection["Body"],
                        DateTime.Now,
                        UserHandler.GetLoggedInUser().ID,
                        (collection["Published"]) == "on"));

                    return RedirectToAction("Index", new { ID = pagina.ID });
                }
            }

            return RedirectToAction("Index");
        }

        public System.Web.Mvc.ActionResult DeletePagina(int ID)
        {
            if (UserHandler.IsLoggedIn())
            {
                PaginaHandler.instance.DeletePagina(ID);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public System.Web.Mvc.ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (UserHandler.IsLoggedIn())
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"),
                                                   Path.GetFileName(file.FileName));

                        new Image(0, "/Images/" + file.FileName.Split('\\').Last(), "", DateTime.Now, UserHandler.GetLoggedInUser().ID);

                        file.SaveAs(path);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}
