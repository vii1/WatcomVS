using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcomVS.InvokeWatcom
{
    interface ICheck : IReportItem
    {
        ReportStatus Execute();
        Task<ReportStatus> ExecuteAsync();
    }
}
