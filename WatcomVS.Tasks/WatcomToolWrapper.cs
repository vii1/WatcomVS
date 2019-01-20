using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WatcomVS.Tasks
{
    public class WatcomToolWrapper : IDisposable
    {
        #region wdll (idedrv) interface

        [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4 )]
        private struct IDEDRV
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string dll_name;       // * dll name
            [MarshalAs(UnmanagedType.LPStr)]
            public string ent_name;       // * NULL or entry name
            public IntPtr ide_handle;           // # handle, when WATCOM IDE
            public uint drv_status;   // # status: from IDEDRV (IDEDRV_STATUS)
            public uint dll_status;   // # status: from DLL
            public IntPtr dll_handle;           // $ handle for a loaded DLL
            //[MarshalAs( UnmanagedType.U4 )]
            public bool loaded; // # TRUE ==> dll is loaded

            // * filled in by caller
            // # filled in by IDEDRV (public)
            // $ filled in by IDEDRV (private)
        }

        [DllImport( "wdll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi )]
        private static extern int IdeDrvExecDLL( ref IDEDRV inf, [MarshalAs( UnmanagedType.LPStr )] string cmd_line );
        [DllImport( "wdll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi )]
        private static extern void IdeDrvInit( ref IDEDRV inf, [MarshalAs( UnmanagedType.LPStr )] string dll_name, [MarshalAs( UnmanagedType.LPStr )] string ent_name );
        [DllImport( "wdll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi )]
        private static extern int IdeDrvPrintError( ref IDEDRV inf );
        [DllImport( "wdll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi )]
        private static extern int IdeDrvUnloadDLL( ref IDEDRV inf );
        [DllImport( "wdll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi )]
        private static extern int IdeDrvStopRunning( ref IDEDRV inf );

        #endregion

        public string ToolName { get; }

        IDEDRV drv;
        private string dllPath;
        private string entryName;

        //        private readonly string dllPath;
        private readonly string exePath;
        //      private readonly string entryName;

        public WatcomToolWrapper( string name, string exePath, string dllPath = null, string entryName = null )
        {
            ToolName = name;
            this.exePath = exePath;
            this.dllPath = dllPath;
            this.entryName = entryName;
            if( !string.IsNullOrEmpty( dllPath ) ) {
                IdeDrvInit( ref drv, this.dllPath, this.entryName );
            }
        }

        public void Run( string cmd_line )
        {
            if( !string.IsNullOrEmpty( drv.dll_name ) ) {
                IdeDrvExecDLL( ref drv, cmd_line );
            }
            if( !string.IsNullOrEmpty( exePath ) ) {
                var proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = exePath;
                proc.StartInfo.Arguments = cmd_line;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
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
                if( drv.loaded ) {
                    IdeDrvUnloadDLL( ref drv );
                }

                disposedValue = true;
            }
        }

        ~WatcomToolWrapper()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( false );
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( true );
            GC.SuppressFinalize( this );
        }
        #endregion
    }
}
