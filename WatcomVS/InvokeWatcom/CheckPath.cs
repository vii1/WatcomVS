using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcomVS.InvokeWatcom
{
    class CheckPath : ICheck
    {
        public string Path { get; set; }
        public bool Required { get; set; }
        public CheckPath( string path, bool required ) {
            Path = path;
            Required = required;
        }
        public ReportStatus Status { get; private set; }
        public string Category => "Directory";
        public string Description { get; private set; }

        public ReportStatus Execute()
        {
            try {
                if(!Directory.Exists(Path)) {
                    Description = $"Directory not found: {Path}";
                    Status = Required ? ReportStatus.Error : ReportStatus.Warning;
                    return Status;
                }
                var fs = Directory.EnumerateFileSystemEntries( Path );
                fs.Count(); // solo para obligar a enumerar el contenido de la carpeta
            } catch(Exception ex) {
                Description = $"Directory {Path}: {ex.Message}";
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
