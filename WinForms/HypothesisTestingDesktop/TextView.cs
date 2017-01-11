using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HypothesisTestingDesktop
{
    public partial class TextView : Form
    {
        public TextView()
        {
            InitializeComponent();
        }

        public string FilePath; // Value given before form loads
        public string FilePathNew; // Value assigned at load

        private bool FileOpen = false;
        // Boolean for whether or not a file is open

        private void TextView_Load(object sender, EventArgs e)
        {
            this.Size = new Size(450, 350); // Set initial form size

            if (FilePath != "no-test")
            { // If there is a file to be opened
                FilePathNew = FilePath;
                string TextFileString = "";

                if (FilePath != null) // If filepath value exists
                {
                    try
                    {
                        TextFileString = File.ReadAllText(FilePath);
                        // Read text file to memory
                        FileOpen = true; // File now open
                    }
                    catch (Exception ex)
                    { // Show error
                        MessageBox.Show("Can't open text file",
                            "Hypothesis Testing Calculator Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                editBox.Text = TextFileString;
            }
            else
            {
                FileOpen = false;
                editBox.Text = "";
            }

            lightToolStripMenuItem.PerformClick();
            // Set form theme to 'light'
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(FilePathNew, editBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't write text file. Try using 'Save As'.",
                    "Hypothesis Testing Calculator Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveText(editBox.Text);
        }

        private void SaveText(string OutputString)
        { // 'Save As' method
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            // Filter output to text files
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog1.FileName, OutputString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can't write text file",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); // Close form when exit is clicked.
        }

        private string stringInFile = "";
        private string stringInEdit = "";
        private void TextView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FileOpen)
            { /* Read text from file to check for differences
                 to currently opened version. */
                stringInFile = File.ReadAllText(FilePathNew);
                stringInEdit = editBox.Text;
            }

            if (stringInEdit != stringInFile & FileOpen)
            {
                DialogResult dialogResult =
                    MessageBox.Show("Would you like to save before exiting?",
                    "Leaving so fast?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    saveToolStripMenuItem.PerformClick();
                    // Save before exit
                }
            }
        }

        private void View_Font_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem sent = new ToolStripMenuItem(sender.ToString());

            try
            {
                editBox.Font = new Font(sent.Text, editBox.Font.Size);
            } catch(Exception ex)
            {
                MessageBox.Show("Font not available",
                    "Hypothesis Testing Calculator - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            calibriToolStripMenuItem.Checked = false;
            courierNewToolStripMenuItem.Checked = false;
            cambriaToolStripMenuItem.Checked = false;
            comicSansToolStripMenuItem.Checked = false;

            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                item.Checked = true;
            }
        }

        private void View_FontSize_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;
            toolStripMenuItem7.Checked = false;

            if (item != null)
            {
                editBox.Font =
                    new Font(editBox.Font.Name, float.Parse(item.Text));
                item.Checked = true;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editBox.Undo(); // Undo button
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editBox.Redo(); // Redo button
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(editBox.SelectedText);
            // Copy selected text to clipboard
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(editBox.SelectedText);
            editBox.Cut();
            // Cut selected text to clipboard
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editBox.Paste(); // Paste text from clipboard
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Delete currently selected text
            string clipBefore = "";
            try
            {
                clipBefore = Clipboard.GetText();
            }
            catch (Exception ex)
            {
                Clipboard.SetText("");
            }
            editBox.Cut();

            Clipboard.SetText(clipBefore);
        }

        private void TextView_KeyDown(object sender, KeyEventArgs e)
        { // Shortcuts
            if (e.KeyCode == Keys.E && e.Control)
                exitToolStripMenuItem.PerformClick();

            if (e.KeyCode == Keys.S && e.Control)
                saveToolStripMenuItem.PerformClick();

            if (e.KeyCode == Keys.S && e.Control && e.Shift)
                saveAsToolStripMenuItem.PerformClick();

            if (e.KeyCode == Keys.O && e.Control)
                openToolStripMenuItem.PerformClick();
        }

        private void View_Colours_Click(object sender, EventArgs e)
        {
            // Change form theme
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            lightToolStripMenuItem.Checked = false;
            darkToolStripMenuItem.Checked = false;

            item.Checked = true;

            if (item.Text == "Light")
            {
                editBox.BackColor = Color.White;
                editBox.ForeColor = Color.Black;
                editBorder.BackColor = Color.Black;
            }
            else
            {
                if (item.Text == "Dark")
                {
                    editBox.BackColor = Color.Black;
                    editBox.ForeColor = Color.White;
                    editBorder.BackColor = Color.White;
                }
            }
            editPanel.BackColor = editBox.BackColor;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open file using OpenFileDialog
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "Open Text File";
            OpenFile.Filter = "TXT files|*.txt";

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                FilePath = OpenFile.FileName;

                FilePathNew = FilePath;
                string TextFileString = "";

                if (FilePath != null)
                {
                    try
                    {
                        TextFileString = File.ReadAllText(FilePath);
                        FileOpen = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can't open text file",
                            "Hypothesis Testing Calculator Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                editBox.Text = TextFileString;
            }
        }
    }
}
