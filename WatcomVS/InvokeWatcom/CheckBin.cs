using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Paz = System.IO.Path;

namespace WatcomVS.InvokeWatcom
{
    class CheckBin : CheckFile
    {
        public string Args { get; set; }
        public Regex ExpectedPattern { get; set; }
        public Match Match { get; private set; }
        public string WatcomPath { get; set; }
        public new string Category => "Program";

        public CheckBin( string path, bool required,string watcomPath, string args, Regex expectedPattern ) : base( path, required )
        {
            Args = args;
            ExpectedPattern = expectedPattern;
            WatcomPath = watcomPath;
        }

        public new ReportStatus Execute()
        {
            if(base.Execute() != ReportStatus.OK) {
                return Status;
            }

            var proc = new Process();

            proc.StartInfo.Environment["PATH"] = Paz.GetDirectoryName( Path );
            proc.StartInfo.Environment["WATCOM"] = WatcomPath;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.ErrorDialog = false;
            proc.StartInfo.FileName = Path;
            proc.StartInfo.Arguments = Args;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.StandardOutputEncoding = Encoding.GetEncoding( 850 );

            proc.Start();
            var line = proc.StandardOutput.ReadLine();
            proc.Close();
            Match = ExpectedPattern.Match( line );
            if(!Match.Success) {
                Description = $"{Path}: Found but program output does not match expected result. May be invalid/incompatible.";
                Status = ReportStatus.Warning;
                return Status;
            }
            Description = $"Found: {Path}";
            Status = ReportStatus.OK;
            return Status;
        }

        public new Task<ReportStatus> ExecuteAsync()
        {
            return Task.Run( () => Execute() );
        }
    }
}
