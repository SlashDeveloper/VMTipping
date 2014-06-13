using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VMTippekonkurranse.Models;
using VMTipping.Model;

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
                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes(Convert.ToInt32(file.InputStream.Length));

                string result = System.Text.Encoding.UTF8.GetString(binData);

                var list = JsonConvert.DeserializeObject<List<User>>(result);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }
    }
}