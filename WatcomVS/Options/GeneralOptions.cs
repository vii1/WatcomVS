using System;
using System.ComponentModel;

namespace WatcomVS.Options
{
    internal class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category( "WatcomVS" )]
        [DisplayName( "Open Watcom installation path" )]
        [Description( "Select the directory where Open Watcom is installed. You can download Open Watcom at http://openwatcom.org" )]
        [DefaultValue( "" )]
        [Editor( typeof( ErikE.Shuriken.FolderPathEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
        public string WatcomPath { get; set; } = "";
        //internal string C16Bin { get; set; } = "";
        //internal string C32Bin { get; set; } = "";
        //internal string Cpp16Bin { get; set; } = "";
        //internal string Cpp32Bin { get; set; } = "";
        //internal string AsmBin { get; set; } = "";
        //internal string LinkBin { get; set; } = "";
        //internal string MakeBin { get; set; } = "";
    }
}
