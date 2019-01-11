using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcomVS.InvokeWatcom
{
    interface IReportItem
    {
        ReportStatus Status { get; }
        string Category { get; }
        string Description { get; }
    }
}
