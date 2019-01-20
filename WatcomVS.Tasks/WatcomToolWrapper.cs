using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WatcomVS.Tasks
{
    public class WatcomToolWrapper : IDisposable
    {
        public class ExitedEventArgs : EventArgs
        {
            public int ExitCode { get; }
            public DateTime ExitTime { get; }
            internal ExitedEventArgs( Process proc )
            {
                ExitCode = proc.ExitCode;
                ExitTime = proc.ExitTime;
            }
        }

        public string ToolName { get; }
        public event DataReceivedEventHandler OutputDataReceived;
        public event DataReceivedEventHandler ErrorDataReceived;
        public event EventHandler<ExitedEventArgs> Exited;

        private readonly string exePath;
        private Process proc;

        public WatcomToolWrapper( string name, string exePath )
        {
            ToolName = name;
            this.exePath = exePath;
        }

        public void Run( string cmd_line )
        {
            if( !string.IsNullOrEmpty( exePath ) ) {
                if( proc != null ) {
                    Stop();
                }
                proc = new Process();
                proc.StartInfo.FileName = exePath;
                proc.StartInfo.Arguments = cmd_line;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.OutputDataReceived += OutputDataReceived;
                proc.ErrorDataReceived += ErrorDataReceived;
                proc.Exited += ( o, e ) => Exited( o, new ExitedEventArgs( proc ) );
                proc.Start();
                proc.StandardInput.Close();
            }
        }

        private void Stop()
        {
            if( proc != null && !proc.HasExited ) {
                try {
                    proc.Kill();
                } finally {
                    proc.WaitForExit();
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose( bool disposing )
        {
            if( !disposedValue ) {
                if( disposing ) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        //~WatcomToolWrapper()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose( false );
        //}

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( true );
            //GC.SuppressFinalize( this );
        }
        #endregion
    }
}
