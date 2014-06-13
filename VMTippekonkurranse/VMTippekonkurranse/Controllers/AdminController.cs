using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMTippekonkurranse.Models;

namespace VMTippekonkurranse.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //using (var context = new TippeContext())
            //{
            //    if (context.Grupper.Any())
            //        return View();
            //    context.Grupper.Add(new Gruppe
            //    {
            //        Navn = "A"
            //    });
            //    context.SaveChanges();
            //}
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);

                // then save on the server...
                var path = Path.Combine(Server.MapPath("~/uploads"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }
    }
}