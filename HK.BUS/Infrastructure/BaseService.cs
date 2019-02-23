using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK.BUS.Infrastructure
{
    public class BaseService : IDisposable
    {
        protected readonly IApplicationService AppService;

        internal BaseService(IApplicationService appService)
        {
            AppService = appService;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
