using System;
using System.Windows.Forms;

namespace HypothesisTestingDesktop
{
    /// <summary>
    /// Implementation for the help form.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class helpFormDesktop : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="helpFormDesktop"/> class.
        /// </summary>
        public helpFormDesktop()
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(625, 500);
            // Set initial form size.

            this.Text = "Hypothesis Testing Calculator - Help";
            // Assign the form title text.

            helpViewHtml.IsWebBrowserContextMenuEnabled = false;
            // Disallow right click in browser.
            helpViewHtml.AllowWebBrowserDrop = false;
            // Disallow drag and drop to browser.

            string url =
                "https://www.dropbox.com/s/jt487yr9sv0z20h/helpHtml.html?raw=1";
            /* Static link to directly access a HTML
               file hosting the help documentation. */
            helpViewHtml.Navigate(url);
            // Navigate to URL.
        }

        private void helpFormDesktop_FormClosed(object sender, FormClosedEventArgs e)
        {
            helpViewHtml.Refresh(WebBrowserRefreshOption.Completely);
            /* Refresh browser contents
               (to avoid caching of older versions of the help documentation). */
        }

        private void helpFormDesktop_Resize(object sender, EventArgs e)
        {
            /* Display form size in window title if debugging is detected
               (Useful when designing HTML). */

#if DEBUG
            {
                string Xsize = this.Size.Width.ToString();
                string Ysize = this.Size.Height.ToString(); ;

                this.Text = "Hypothesis Testing Calculator - Help ("
                    + Xsize + "x" + Ysize + ")";
            }
#endif
        }
    }
}
