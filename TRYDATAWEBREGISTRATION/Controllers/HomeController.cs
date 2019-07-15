using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TRYDATAWEBREGISTRATION.Controllers
{
    

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            RegistrationDBContext context = new RegistrationDBContext();
            ViewBag.Country = new SelectList(context.tbl_countryStateCity.Select(x => x.Cauntry).Distinct().OrderBy(x => x));
            ViewBag.State = new SelectList(new List<string>());
            ViewBag.City = new SelectList(new List<string>());
            return View();
        }

        [HttpPost]
        public ActionResult Register(tbl_regis userDB)
        {
            // Country list should always be created
            // becase it is lost when submit the form.

            // if State is null, then city also nu,,
            // We need to create State list and City list
            // if
            RegistrationDBContext context = new RegistrationDBContext();

            ViewBag.Country = new SelectList(context.tbl_countryStateCity.Select(x => x.Cauntry).Distinct().OrderBy(x => x));
            ViewBag.State = new SelectList(new List<string>());
            ViewBag.City = new SelectList(new List<string>());
            if (!string.IsNullOrEmpty(userDB.country))
            {
                // If state not selected, return to view with state list 
                ViewBag.State = new SelectList(context.tbl_countryStateCity.Where(x => x.Cauntry == userDB.country).Select(x => x.State).Distinct().OrderBy(state => state));

            }
            if (!string.IsNullOrEmpty(userDB.State))
            {
                // If city not selected, return to view with city list
                ViewBag.City = new SelectList(context.tbl_countryStateCity.Where(x => x.State == userDB.State).Select(x => x.City).Distinct().OrderBy(city => city));

            }
            if (string.IsNullOrEmpty(userDB.country) || string.IsNullOrEmpty(userDB.State) || string.IsNullOrEmpty(userDB.City))
                return View(userDB);
            // make manual validation for example

            if (string.IsNullOrEmpty(userDB.username))
            {
                ModelState.AddModelError("UserName", "Please enter the username");
                return View(userDB);
            }
            if (string.IsNullOrEmpty(userDB.email))
            {
                ModelState.AddModelError("Email", "Please enter email address");
                return View(userDB);
            }

            // Here country, state and city are selected, you can save safely
            // YOu don't have a country, site and state tables/!

            context.tbl_regis.Add(userDB);

            context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}