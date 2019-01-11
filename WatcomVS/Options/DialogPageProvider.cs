using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WatcomVS.Options
{
    /// <summary>
    /// A provider for custom <see cref="DialogPage" /> implementations.
    /// </summary>
    internal class DialogPageProvider
    {
        [Guid( "FBEDE34E-47B9-4E0D-9F9A-56090D928C3F" )]
        public class General : BaseOptionPage<GeneralOptions>
        {
            private System.WeakReference<GeneralOptionsUI> m_page;

            protected override void OnApply( PageApplyEventArgs e )
            {
                if( m_page!=null && m_page.TryGetTarget( out GeneralOptionsUI page )) {
                    if( e.ApplyBehavior == ApplyKind.Apply ) {
                        GeneralOptions.Instance.WatcomPath = page.WatcomPath;
                        GeneralOptions.Instance.Save();
                    }
                }
                base.OnApply( e );
            }
            protected override IWin32Window Window {
                get {
                    GeneralOptionsUI page;
                    if(m_page==null || !m_page.TryGetTarget(out page)) {
                        page = new GeneralOptionsUI();
                        m_page = new System.WeakReference<GeneralOptionsUI>( page );
                    }
                    GeneralOptions.Instance.Load();
                    page.WatcomPath = GeneralOptions.Instance.WatcomPath;
                    //page.Initialize();
                    return page;
                }
            }
        }
    }
}
