using HK.BUS.Infrastructure;
using HK.BUS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK.BUS
{
    public class ApplicationService : IApplicationService, IDisposable
    {

        public ApplicationService()
        {
        }

        private DataService _dataService;
        public DataService DataService => _dataService ?? (_dataService = new DataService(this));

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
