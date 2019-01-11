using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Services;
using EnvDTE;
using System.IO;
using System.Runtime.InteropServices;

namespace WatcomVS.Options
{
    public partial class GeneralOptionsUI : UserControl
    {
        public string WatcomPath {
            get => textWatcomPath.Text;
            set => textWatcomPath.Text = value;
        }

        /*private string _report;
        private string Report {
            get => _report;
            set {
                _report = value;
            }
        }
        private enum CheckResult
        {
            OK,
            Warning,
            Error
        }*/

        public GeneralOptionsUI()
        {
            //optionsPage = GeneralOptions.Instance;
            //optionsPage.Load();
            InitializeComponent();
        }

        public void Initialize()
        {
            //textWatcomPath.Text = optionsPage.WatcomPath;
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
                    //optionsPage.WatcomPath = path;
                    //optionsPage.Save();
                }
            }
        }

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
