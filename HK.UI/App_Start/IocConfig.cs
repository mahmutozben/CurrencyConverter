using DryIoc;
using DryIoc.Mvc;
using HK.BUS;
using HK.BUS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK.UI.App_Start
{
    public class IocConfig
    {
        public static void RegisterIOC()
        {
            var di = new Container();

            di.Register<IApplicationService, ApplicationService>(Reuse.InWebRequest);

            di.WithMvc();
        }
    }
}