using HK.BUS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK.BUS.Infrastructure
{
    public interface IApplicationService
    {
        DataService DataService { get; }
    }
}
