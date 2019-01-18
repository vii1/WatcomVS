using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;

namespace WatcomVS.Tasks
{
    public abstract class WatcomToolTask : Task
    {
        public string WatcomPath { get; set; }

    }
}
