using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TRYDATAWEBREGISTRATION.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            RegistrationDBContext context = new RegistrationDBContext();

         ViewBag.Country = new SelectList(context.tbl_countryStateCity.Select(x => x.Cauntry).Distinct().OrderBy(x => x));

            ViewBag.State = new SelectList(new List<string>());

            ViewBag.City = new SelectList(new List<string>());

            return View();
        }

        [HttpPost]
        public ActionResult Register(tbl_regis regis)
        {
            RegistrationDBContext context = new RegistrationDBContext();

         ViewBag.Country = new SelectList(context.tbl_countryStateCity.Select(x => x.Cauntry).Distinct().OrderBy(x => x));

            ViewBag.State = new SelectList(new List<string>());

            ViewBag.City = new SelectList(new List<string>());

            if (!string.IsNullOrEmpty(regis.country))
            {
             ViewBag.State = new SelectList(context.tbl_countryStateCity.Where(x => x.Cauntry == regis.country).Select(x => x.State).Distinct().OrderBy(state => state));
            }

            if (!string.IsNullOrEmpty(regis.State))
            {
                ViewBag.City = new SelectList(context.tbl_countryStateCity.Where(x => x.State == regis.State).Select(x => x.City).OrderBy(city => city));
            }

            if (string.IsNullOrEmpty(regis.country) || string.IsNullOrEmpty(regis.State) || string.IsNullOrEmpty(regis.City))

                return View(regis);
         

            context.tbl_regis.Add(regis);

            context.SaveChanges();


            return View();
        }
    }
}