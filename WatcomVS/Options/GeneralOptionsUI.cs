using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace WatcomVS.Options
{
    using UseOldFilePathsValue = GeneralOptions.UseOldFilePathsValue;

    public partial class GeneralOptionsUI : UserControl
    {
        internal string WatcomPath {
            get => textWatcomPath.Text;
            set => textWatcomPath.Text = value;
        }
        internal UseOldFilePathsValue UseOldFilePaths {
            get {
                if( radioOldPathsAlways.Checked ) {
                    return UseOldFilePathsValue.Always;
                } else if( radioOldPathsLegacyOnly.Checked ) {
                    return UseOldFilePathsValue.LegacyOnly;
                } else {
                    return UseOldFilePathsValue.No;
                }
            }
            set {
                switch( value ) {
                    case UseOldFilePathsValue.LegacyOnly:
                        radioOldPathsLegacyOnly.Checked = true;
                        break;
                    case UseOldFilePathsValue.Always:
                        radioOldPathsAlways.Checked = true;
                        break;
                    default:
                        radioOldPathsNo.Checked = true;
                        break;
                }
            }
        }

        public GeneralOptionsUI()
        {
            InitializeComponent();
        }

        private void ButtonWatcomPathBrowse_Click( object sender, EventArgs e )
        {
            var dialog = new ErikE.Shuriken.FolderSelectDialog {
                InitialDirectory = textWatcomPath.Text
            };

            if( dialog.Show( Handle ) ) {
                if( Directory.Exists( dialog.FileName ) ) {
                    var path = dialog.FileName;
                    //StringBuilder shortPath = new StringBuilder( MAX_PATH );
                    //if( GetShortPathName( path, shortPath, MAX_PATH ) > 0 ) {
                    //    path = shortPath.ToString();
                    //}
                    WatcomPath = path;
                }
            }
        }

        // TODO: mover esto a InvokeWatcom
        private const int MAX_PATH = 260;
        [DllImport( "kernel32.dll", CharSet = CharSet.Auto )]
        private static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
             string path,
            [MarshalAs(UnmanagedType.LPTStr)]
             StringBuilder shortPath,
            int shortPathLength
        );

        private void linkLabel_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            System.Diagnostics.Process.Start( "http://openwatcom.org" );
        }
    }
}
