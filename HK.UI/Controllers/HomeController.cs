using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using HK.BUS.Common;
using HK.BUS.Infrastructure;

namespace HK.UI.Controllers
{
    public class HomeController : MainController
    {
        public HomeController(IApplicationService appService) : base(appService)
        {
        }

        public ActionResult Index()
        {
            return View(AppService.DataService.GetCurrencyNameList());
        }

        [HttpPost]
        public JsonResult CalculateCurrency(CurrencyModel model)
        {
            return Json(AppService.DataService.CalculateCurrency(model));
        }
    }
}