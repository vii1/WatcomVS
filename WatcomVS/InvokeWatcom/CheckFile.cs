using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcomVS.InvokeWatcom
{
    class CheckFile : ICheck
    {
        public string Path { get; set; }
        public bool Required { get; set; }

        public ReportStatus Status { get; protected set; }
        public string Category => "File";
        public string Description { get; protected set; }

        public CheckFile( string path, bool required )
        {
            Path = path;
            Required = required;
        }

        public ReportStatus Execute()
        {
            try {
                if( !File.Exists( Path ) ) {
                    Description = $"File not found: {Path}";
                    Status = Required ? ReportStatus.Error : ReportStatus.Warning;
                }
                using( var fs = File.OpenRead( Path ) ) {
                    fs.Close();
                }
            } catch( Exception ex ) {
                Description = $"File {Path}: {ex.Message}";
                Status = Required ? ReportStatus.Error : ReportStatus.Warning;
                return Status;
            }
            Description = $"Found: {Path}";
            Status = ReportStatus.OK;
            return Status;
        }

        public Task<ReportStatus> ExecuteAsync()
        {
            return Task.Run( () => Execute() );
        }
    }
}
