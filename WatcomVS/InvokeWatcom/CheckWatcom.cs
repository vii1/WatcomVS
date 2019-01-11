using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace WatcomVS.InvokeWatcom
{
    class CheckWatcom
    {
        public Report Report { get; private set; }

        private readonly ICheck[] checks;

        public CheckWatcom(string watcomPath)
        {
            var binnt = Path.Combine( watcomPath, "binnt" );
            var help = Path.Combine( binnt, "help" );
            checks = new ICheck[] {
                new CheckPath(watcomPath, true),
                new CheckPath(Path.Combine(watcomPath,"h"),false),
                new CheckBin(Path.Combine(binnt,"wcc.exe"),false,watcomPath,"nul",new Regex(@"Watcom C16 .*Compiler.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wcc386.exe"),false,watcomPath,"nul",new Regex(@"Watcom C32 .*Compiler.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wpp.exe"),false,watcomPath,"nul",new Regex(@"Watcom C\+\+16 .*Compiler.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wpp386.exe"),false,watcomPath,"nul",new Regex(@"Watcom C\+\+32 .*Compiler.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wasm.exe"),false,watcomPath,"nul",new Regex(@"Watcom.*Assembler.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wcl.exe"),false,watcomPath,"nul",new Regex(@"Watcom.*16.*Compile.*Link.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wcl386.exe"),false,watcomPath,"nul",new Regex(@"Watcom.*32.*Compile.*Link.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wlib.exe"),false,watcomPath,"nul",new Regex(@"Watcom.*Library Manager.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wlink.exe"),false,watcomPath,"nul",new Regex(@"Watcom.*Linker.*Version (\d+(\.\d+)+)")),
                new CheckBin(Path.Combine(binnt,"wmake.exe"),false,watcomPath,"/?",new Regex(@"Watcom.*Make.*Version (\d+(\.\d+)+)")),
                new CheckPath(Path.Combine(watcomPath,"lib286"),false),
                new CheckPath(Path.Combine(watcomPath,"lib386"),false),
                new CheckFile(Path.Combine(help,"cguide.chm"),false),
                new CheckFile(Path.Combine(help,"clib.chm"),false),
                new CheckFile(Path.Combine(help,"clr.chm"),false),
                new CheckFile(Path.Combine(help,"cpplib.chm"),false),
                new CheckFile(Path.Combine(help,"lguide.chm"),false),
            };
        }
    }
}
