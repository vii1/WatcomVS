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

        private WeakReference<GeneralOptionsUI> m_ui;

        private void Load( GeneralOptionsUI ui = null )
        {
            if( ui == null && (m_ui == null || !m_ui.TryGetTarget( out ui )) ) {
                return;
            }
            ui.WatcomPath = WatcomPath;
        }

        private void Save( GeneralOptionsUI ui = null )
        {
            if( ui == null && (m_ui == null || !m_ui.TryGetTarget( out ui )) ) {
                return;
            }
            WatcomPath = ui.WatcomPath;
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
                //page.Initialize();
                return ui;
            }
        }
    }
}
