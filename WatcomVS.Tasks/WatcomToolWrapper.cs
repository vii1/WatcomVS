/// This class implements the IDEDRV interface for Watcom's DLL tools.
/// In case the DLL cannot be used, it falls back to calling the EXE directly.

using System;
using System.Runtime.InteropServices;

namespace WatcomVS.Tasks
{
    public class WatcomToolWrapper : IDisposable
    {
        #region P/Invoke declarations

        [DllImport( "kernel32.dll" )]
        private static extern IntPtr LoadLibrary( string dllToLoad );

        [DllImport( "kernel32.dll" )]
        private static extern IntPtr GetProcAddress( IntPtr hModule, string procedureName );

        [DllImport( "kernel32.dll" )]
        private static extern bool FreeLibrary( IntPtr hModule );

        private static TDelegate GetProcAddress<TDelegate>( IntPtr hModule, string procedureName )
            => Marshal.GetDelegateForFunctionPointer<TDelegate>( GetProcAddress( hModule, procedureName ) );

        #endregion

        #region IDE Interface

        private enum IDEMsgSeverity : uint
        {
            WARNING,
            ERROR,
            NOTE,
            BANNER,
            DEBUG,
            NOTE_MSG
        }

        [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4 )]
        private struct IDEMsgInfo   // IDE MESSAGE INFORMATION
        {
            IDEMsgSeverity severity;      // - severity code
                                          // Message Information Present
            uint flags;
            // Help Iinformation
            [MarshalAs( UnmanagedType.LPStr )]
            string helpfile;     // - name of help file
            uint helpid;        // - help identifier
                                // Message Information
            [MarshalAs( UnmanagedType.LPStr )]
            string msg;          // - message
            [MarshalAs( UnmanagedType.LPStr )]
            string src_symbol;   // - symbol
            [MarshalAs( UnmanagedType.LPStr )]
            string src_file;     // - source/link-file name
            uint src_line;      // - source-file line
            uint src_col;       // - source-file column
            uint msg_no;        // - message number
            [MarshalAs( UnmanagedType.ByValArray, SizeConst = 8 )]
            string msg_group;  // - message group
        }

        enum IDEInfoType : uint
        {
            GET_SOURCE_FILE = 1,
            GET_TARGET_FILE = 2,
            GET_OBJ_FILE = 3,
            GET_LIB_FILE = 4,
            GET_RES_FILE = 5,
            GET_ENV_VAR = 6,
            GET_AI = 7
        }

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate void BatchFilterDelegate( IntPtr cookie, [MarshalAs( UnmanagedType.LPStr )] string msg );
        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate void BatchDllFilterDelegate( IntPtr cookie, ref IDEMsgInfo errinfo );

        [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4 )]
        private struct IDECallBacks
        {
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate ushort RunBatchDelegate( IntPtr hdl,
                [MarshalAs( UnmanagedType.LPStr )] string cmdline,
                BatchFilterDelegate cb,
                IntPtr cookie );

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate ushort PrintMsgDelegate( IntPtr hdl,
                [MarshalAs( UnmanagedType.LPStr )] string message );

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate ushort GetInfoDelegate( IntPtr hdl, IDEInfoType type, uint wparam, uint lparam );

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate ushort MsgInfoDelegate( IntPtr hdl,
                ref IDEMsgInfo info );

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate ushort RunDllDelegate( IntPtr hdl,
                [MarshalAs( UnmanagedType.LPStr )] string dllname,
                [MarshalAs( UnmanagedType.LPStr )] string cmdline,
                BatchDllFilterDelegate cb,
                IntPtr cookie );

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate ushort RunBatchCwdDelegate( IntPtr hdl,
                [MarshalAs( UnmanagedType.LPStr )] string cmdline,
                [MarshalAs( UnmanagedType.LPStr )] string cwd,
                BatchFilterDelegate cb,
                IntPtr cookie );

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate void ReceiveIndexDelegate( IntPtr hdl, uint index /* 0-99 */ );

            // building functions
            public RunBatchDelegate RunBatch;
            public PrintMsgDelegate PrintMessage;
            public PrintMsgDelegate PrintWithCRLF;
            public MsgInfoDelegate PrintWithInfo;

            // Query functions
            public GetInfoDelegate GetInfo;

            public PrintMsgDelegate ProgressMessage;
            public RunDllDelegate RunDll;         // may be NULL
            public RunBatchCwdDelegate RunBatchCWD;    // may be NULL
            public IntPtr OpenJavaSource; // may be NULL
            public IntPtr OpenClassFile;  // may be NULL
            public IntPtr PackageExists;  // may be NULL
            public IntPtr GetSize;        // may be NULL
            public IntPtr GetTimeStamp;   // may be NULL
            public IntPtr IsReadOnly;     // may be NULL
            public IntPtr ReadData;       // may be NULL
            public IntPtr Close;          // may be NULL
            public IntPtr ReceiveOutput;  // may be NULL

            public ReceiveIndexDelegate ProgressIndex;

            public IntPtr SrcDepBegin;    // may be NULL (begin java src dependencies)
            public IntPtr SrcDepFile;     // (java src dependency file)
            public IntPtr SrcDepEnd;      // (end java src dependendency)
        }

        private IDECallBacks IDECallBacksInstance = new IDECallBacks {
            RunBatch = RunBatch,
            PrintMessage = PrintMessage,
            PrintWithCRLF = PrintWithCRLF,
            PrintWithInfo = PrintWithInfo,
            GetInfo = GetInfo,
            ProgressMessage = ProgressMessage,
            RunDll = RunDll,
            RunBatchCWD = RunBatchCWD,
            ProgressIndex = ProgressIndex
        };

        private static ushort RunBatch( IntPtr hdl, string cmdline, BatchFilterDelegate cb, IntPtr cookie )
            => throw new NotImplementedException();

        private static ushort PrintMessage( IntPtr hdl, string message )
            => throw new NotImplementedException();

        private static ushort PrintWithCRLF( IntPtr hdl, string message )
            => throw new NotImplementedException();

        private static ushort PrintWithInfo( IntPtr hdl, ref IDEMsgInfo info )
            => throw new NotImplementedException();

        private static ushort GetInfo( IntPtr hdl, IDEInfoType type, uint wparam, uint lparam )
            => throw new NotImplementedException();

        private static ushort ProgressMessage( IntPtr hdl, string message )
            => throw new NotImplementedException();

        private static ushort RunDll( IntPtr hdl, string dllname, string cmdline, BatchDllFilterDelegate cb, IntPtr cookie )
            => throw new NotImplementedException();

        private static ushort RunBatchCWD( IntPtr hdl, string cmdline, string cwd, BatchFilterDelegate cb, IntPtr cookie )
            => throw new NotImplementedException();

        private static void ProgressIndex( IntPtr hdl, uint index )
            => throw new NotImplementedException();

        #endregion

        #region DLL Interface

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate uint GetVersionDelegate();

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate ushort InitDLLDelegate( IntPtr hdl, ref IDECallBacks cb, ref IntPtr info );

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate ushort RunSelfDelegate( IntPtr hdl, [MarshalAs( UnmanagedType.LPStr )] string opts, ref ushort fatalerr );

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate ushort RunSelfArgvDelegate( IntPtr hdl, int argc, [MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPStr )] string[] argv, ref ushort fatalerr );

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate void FiniDLLDelegate( IntPtr hdl );

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate void StopRunDelegate();

        private const uint IDE_CUR_INFO_VER = 6;

        [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4 )]
        private struct IDEInitInfo
        {
            public uint ver;
            [MarshalAs( UnmanagedType.U1 )]
            public bool ignore_env;
            [MarshalAs( UnmanagedType.U1 )]
            public bool cmd_line_has_files;     //VERSION 2
            [MarshalAs( UnmanagedType.U1 )]
            public bool console_output;         //VERSION 3
            [MarshalAs( UnmanagedType.U1 )]
            public bool progress_messages;      //VERSION 4
            [MarshalAs( UnmanagedType.U1 )]
            public bool progress_index;         //VERSION 5
        }

        private IDEInitInfo IDEInitInfoInstance = new IDEInitInfo {
            ver = IDE_CUR_INFO_VER,
            ignore_env = false,
            cmd_line_has_files = true,
            console_output = false,
            progress_messages = false,
            progress_index = false
        };

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate ushort PassInitInfoDelegate( IntPtr hdl, ref IDEInitInfo info );

        #endregion

        #region Wrapper

        public string ToolName { get; }

        private IntPtr hModule = IntPtr.Zero;
        private readonly string dllPath;
        private readonly string exePath;
        private readonly string entryName;

        private bool Loaded => hModule != IntPtr.Zero;

        public int DllStatus { get; private set; }
        public RetCode DrvStatus { get; private set; }

        public WatcomToolWrapper( string name, string exePath, string dllPath = null, string entryName = null )
        {
            ToolName = name;
            this.dllPath = dllPath;
            this.exePath = exePath;
            this.entryName = entryName;
        }

        public void Run( string cmd_line )
        {
            if( !string.IsNullOrEmpty( dllPath ) ) {
                IdeDrvExecDLL( cmd_line );
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

        #endregion

        /*  int IdeDrvExecDLL               // EXECUTE THE DLL ONE TIME (LOAD IF REQ'D)
  ( IDEDRV* inf               // - driver control information
      , char const * cmd_line )    // - command line
      ;
          int IdeDrvExecDLLArgv           // EXECUTE THE DLL ONE TIME (LOAD IF REQ'D)
          ( IDEDRV* inf               // - driver control information
              , int argc                  // - # of arguments
              , char** argv )             // - argument vector
              ;
          void IdeDrvInit                 // INITIALIZE IDEDRV INFORMATION
          ( IDEDRV* inf               // - information
              , char const * dll_name      // - dll name
              , char const * ent_name )    // - entry name
      ;
          int IdeDrvPrintError            // UNLOAD THE DLL
          ( IDEDRV* inf )             // - driver control information
          ;
          int IdeDrvUnloadDLL             // UNLOAD THE DLL
          ( IDEDRV* inf )             // - driver control information
          ;
          int IdeDrvStopRunning           // SIGNAL A BREAK
          ( IDEDRV* inf )             // - driver control information
          ;

          void IdeDrvChainCallbacks       // SET CALLBACKS FOR DLL CALLLING A DLL
          ( void* cb                  // - parent dll callbacks
              , void* info )              // - parent dll initialization
              ;

          void* IdeDrvGetCallbacks        // GET CALLBACKS (TO FILL IN BLANKS)
          ( void )
          ;
          void IdeDrvSetCallbacks         // SET CALLBACKS (TO FILL IN BLANKS)
          ( void* cb )
          ;*/

        #region IdeDrv interface

        public enum RetCode
        {
            SUCCESS,
            ERR_LOAD,
            ERR_UNLOAD,
            ERR_INIT,
            ERR_INIT_EXEC,
            ERR_INFO,
            ERR_INFO_EXEC,
            ERR_RUN,
            ERR_RUN_EXEC,
            ERR_RUN_FATAL,
        }

        public class IdeDrvException : Exception
        {
            public RetCode RetCode { get; private set; }

            public override string Message {
                get {
                    switch( RetCode ) {
                        case RetCode.ERR_LOAD:
                            return "Cannot load DLL";
                        case RetCode.ERR_UNLOAD:
                            return "Cannot unload DLL";
                        case RetCode.ERR_INIT:
                            return "Cannot find init routine";
                        case RetCode.ERR_INIT_EXEC:
                            return "Error return from init routine";
                        case RetCode.ERR_INFO:
                            return "Cannot find init-info routine";
                        case RetCode.ERR_INFO_EXEC:
                            return "Error return from init-info routine";
                        case RetCode.ERR_RUN:
                            return "Cannot find run-self routine";
                        case RetCode.ERR_RUN_EXEC:
                            return "Error return from run-self routine";
                        case RetCode.ERR_RUN_FATAL:
                            return "Fatal return from run-self routine";
                        default:
                            return base.Message;
                    }
                }
            }

            internal IdeDrvException( RetCode retcode )
            {
                RetCode = retcode;
            }
        }

        private const string IDETOOL_GETVER = "_IDEGetVersion@0";
        private const string IDETOOL_INITDLL = "_IDEInitDLL@12";
        private const string IDETOOL_RUNSELF = "_IDERunYourSelf@12";
        private const string IDETOOL_RUNSELF_ARGV = "_IDERunYourSelfArgv@16";
        private const string IDETOOL_FINIDLL = "_IDEFiniDLL@4";
        private const string IDETOOL_STOPRUN = "_IDEStopRunning@0";
        private const string IDETOOL_INITINFO = "_IDEPassInitInfo@8";

        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate int UserDLLFuncDelegate( [MarshalAs( UnmanagedType.LPStr )] string cmd );
        [UnmanagedFunctionPointer( CallingConvention.StdCall )]
        private delegate int UserDLLFuncArgvDelegate( int argc, [MarshalAs( UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 0 )] string[] argv );

        IntPtr ide_handle;

        private void EnsureLoaded()
        {
            if( !Loaded ) {
                var handle = LoadLibrary( dllPath );
                if( handle == IntPtr.Zero ) {
                    throw new IdeDrvException( RetCode.ERR_LOAD );
                }
                try {
                    if( string.IsNullOrEmpty( entryName ) ) {
                        var InitDLL = GetProcAddress<InitDLLDelegate>( handle, IDETOOL_INITDLL );
                        if( InitDLL == null ) {
                            throw new IdeDrvException( RetCode.ERR_INIT );
                        }
                        if( InitDLL( IntPtr.Zero, ref IDECallBacksInstance, ref ide_handle ) != 0 ) {
                            throw new IdeDrvException( RetCode.ERR_INIT_EXEC );
                        }
                        var InitInfo = GetProcAddress<PassInitInfoDelegate>( handle, IDETOOL_INITINFO );
                        if( InitInfo == null ) {
                            throw new IdeDrvException( RetCode.ERR_INFO );
                        }
                        IDEInitInfoInstance.console_output = !Console.IsOutputRedirected;
                        if( InitInfo( ide_handle, ref IDEInitInfoInstance ) != 0 ) {
                            throw new IdeDrvException( RetCode.ERR_INFO_EXEC );
                        }
                    }
                } catch {
                    FreeLibrary( handle );
                    throw;
                }
                hModule = handle;
            }
        }

        private void IdeDrvExecDLL( string cmd_line )
        {
            int runcode = 0;
            try {
                EnsureLoaded();
                if( string.IsNullOrEmpty( entryName ) ) {
                    RunSelfDelegate RunSelf = GetProcAddress<RunSelfDelegate>( hModule, IDETOOL_RUNSELF );
                    if( RunSelf == null ) {
                        runcode = 1;
                        throw new IdeDrvException( RetCode.ERR_RUN );
                    }
                    ushort fatal = 0;
                    InitConsole();
                    runcode = RunSelf( ide_handle, cmd_line, ref fatal );
                    FiniConsole();
                    RetCode retcode = RetCodeFromFatal( fatal, runcode, RetCode.SUCCESS );
                    if( retcode != RetCode.SUCCESS ) {
                        if( retcode == RetCode.ERR_RUN_FATAL ) {
                            FreeLibrary( hModule );
                            hModule = IntPtr.Zero;
                        }
                        throw new IdeDrvException( retcode );
                    }
                } else {
                    UserDLLFuncDelegate func = GetProcAddress<UserDLLFuncDelegate>( hModule, entryName );
                    if( func == null ) {
                        runcode = 1;
                        throw new IdeDrvException( RetCode.ERR_RUN );
                    } else {
                        InitConsole();
                        runcode = func( cmd_line );
                        FiniConsole();
                        RetCode retcode = RetCodeFromFatal( 0, runcode, RetCode.SUCCESS );
                        if( retcode != RetCode.SUCCESS ) {
                            throw new IdeDrvException( retcode );
                        }
                    }
                }
            } catch( IdeDrvException ex ) {
                DrvStatus = ex.RetCode;
                throw;
            } finally {
                DllStatus = runcode;
            }
        }

        RetCode RetCodeFromFatal( ushort fatal, int runcode, RetCode retcode )
        {
            if( fatal != 0 ) {
                retcode = RetCode.ERR_RUN_FATAL;
            } else if( runcode != 0 ) {
                retcode = RetCode.ERR_RUN_EXEC;
            }
            return retcode;
        }

        private void InitConsole()
        {
            if( IDEInitInfoInstance.console_output ) {
                Console.CancelKeyPress += Console_CancelKeyPress;
            }
        }

        private void FiniConsole()
        {
            if( IDEInitInfoInstance.console_output ) {
                Console.CancelKeyPress -= Console_CancelKeyPress;
            }
        }

        private void Console_CancelKeyPress( object sender, ConsoleCancelEventArgs e )
        {
            if( Loaded ) {
                GetProcAddress<StopRunDelegate>( hModule, IDETOOL_STOPRUN )?.Invoke();
            }
        }

        #endregion

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
                if( Loaded ) {
                    FreeLibrary( hModule );
                    hModule = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
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
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize( this );
        }
        #endregion
    }
}
