namespace HypothesisTestingDesktop
{
    partial class helpFormDesktop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.helpViewHtml = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // helpViewHtml
            // 
            this.helpViewHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpViewHtml.Location = new System.Drawing.Point(0, 0);
            this.helpViewHtml.Margin = new System.Windows.Forms.Padding(5);
            this.helpViewHtml.Name = "helpViewHtml";
            this.helpViewHtml.Size = new System.Drawing.Size(484, 461);
            this.helpViewHtml.TabIndex = 0;
            // 
            // helpFormDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.helpViewHtml);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "helpFormDesktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.helpFormDesktop_FormClosed);
            this.Resize += new System.EventHandler(this.helpFormDesktop_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser helpViewHtml;
    }
}