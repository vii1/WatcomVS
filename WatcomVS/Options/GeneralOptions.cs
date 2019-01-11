using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WatcomVS.Options
{
    [Guid( "FBEDE34E-47B9-4E0D-9F9A-56090D928C3F" )]
    internal class GeneralOptions : DialogPage
    {
        [Category( "WatcomVS" )]
        [DisplayName( "Open Watcom installation path" )]
        [Description( "Select the directory where Open Watcom is installed. You can download Open Watcom at http://openwatcom.org" )]
        [DefaultValue( "" )]
        [Editor( typeof( ErikE.Shuriken.FolderPathEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
        public string WatcomPath { get; set; } = "";

        public enum UseOldFilePathsValue
        {
            No,
            LegacyOnly,
            Always
        }

        [Category( "WatcomVS" )]
        [DisplayName( "Convert file paths to old 8.3 format" )]
        [Description( "Some target OS's and some Open Watcom tools are incompatible with long file names or names with spaces.\n" +
            "No: will try to use always the long file names and, when required, will try to rename short files to their long version.\n" +
            "Legacy Only: convert file names to 8.3 format when targeting an OS that does not support long names (p.e. DOS, Windows 3.x).\n" +
            "Always: will always convert long file names to short 8.3 format, no matter the target OS." )]
        [DefaultValue( typeof( UseOldFilePathsValue ), "No" )]
        public UseOldFilePathsValue UseOldFilePaths { get; set; } = UseOldFilePathsValue.No;

        private WeakReference<GeneralOptionsUI> m_ui;

        private void Load( GeneralOptionsUI ui = null )
        {
            if( ui == null && (m_ui == null || !m_ui.TryGetTarget( out ui )) ) {
                return;
            }
            ui.WatcomPath = WatcomPath;
            ui.UseOldFilePaths = UseOldFilePaths;
        }

        private void Save( GeneralOptionsUI ui = null )
        {
            if( ui == null && (m_ui == null || !m_ui.TryGetTarget( out ui )) ) {
                return;
            }
            WatcomPath = ui.WatcomPath;
            UseOldFilePaths = ui.UseOldFilePaths;
        }

        protected override void OnApply( PageApplyEventArgs e )
        {
            if( e.ApplyBehavior == ApplyKind.Apply ) {
                Save();
            }
            base.OnApply( e );
        }

        protected override void OnClosed( EventArgs e )
        {
            Load();
        }

        protected override IWin32Window Window {
            get {
                GeneralOptionsUI ui;
                if( m_ui == null || !m_ui.TryGetTarget( out ui ) ) {
                    ui = new GeneralOptionsUI();
                    m_ui = new WeakReference<GeneralOptionsUI>( ui );
                }
                Load( ui );
                return ui;
            }
        }
    }
}
