using HK.BUS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK.UI.Controllers
{
    public class MainController : Controller
    {
        public IApplicationService AppService { get; set; }

        public MainController(IApplicationService appService)
        {
            AppService = appService;
        }
    }
}