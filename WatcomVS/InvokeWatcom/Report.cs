using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WatcomVS.InvokeWatcom
{
    enum ReportStatus
    {
        OK,
        Warning,
        Error
    }

    class Report
    {
        public ReportStatus Status => (from i in Items select i.Status).DefaultIfEmpty( ReportStatus.OK ).Max();
        public IList<IReportItem> Items { get; private set; } = new List<IReportItem>();
        public string Title { get; set; }

        private void PrepareGeneration(ref string path, out ReportTemplate tt)
        {
            if( path == null ) {
                path = Path.GetTempFileName();
                try {
                    File.Move( path, path + ".html" );
                    path += ".html";
                } catch { }
            }
            tt = new ReportTemplate();
            tt.Session["Timestamp"] = DateTime.Now;
            tt.Session["Report"] = this;
        }

        public async Task<string> GenerateAsync( string path = null )
        {
            PrepareGeneration( ref path, out ReportTemplate tt );
            using(var sw = new StreamWriter(path)) {
                await sw.WriteAsync( tt.TransformText() );
            }
            return path;
        }

        public string Generate( string path = null )
        {
            PrepareGeneration( ref path, out ReportTemplate tt );
            using( var sw = new StreamWriter( path ) ) {
                sw.Write( tt.TransformText() );
            }
            return path;
        }
    }
}
