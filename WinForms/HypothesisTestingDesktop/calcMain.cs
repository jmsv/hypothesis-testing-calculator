using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HypothesisTestingDesktop
{
    public partial class calcMain : Form
    {
        public calcMain()
        {
            InitializeComponent();
        }

        public int testType = 0;
        public string H0 = "H₀";
        public string H1 = "H₁";


        Int64 nValue; //sample size
        decimal pValue; //null probabiity of a success
        Int64 rValue; //successful trials from test
        decimal aValue; //significance level for test


        private void calcMain_Load(object sender, EventArgs e)
        { // Uses reset method to set all defaults
            buttonReset.PerformClick();
        }

        private void butMin_Click(object sender, EventArgs e)
        { // Minimize button
            this.WindowState = FormWindowState.Minimized;
        }

        private void butExit_Click(object sender, EventArgs e)
        { // Exit button
            Application.Exit();
        }


        // START - Code to allow movement of main form
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }
        private void Header_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X
                    - this._start_point.X, p.Y
                    - this._start_point.Y);
            }
        }
        // END   - Code to allow movement of main form


        private void butHelp_Click(object sender, EventArgs e)
        {
            this.ActiveControl = promptNP;

            helpFormDesktop helpFormVar = new helpFormDesktop();
            helpFormVar.Show();
        }


        private void radioButton_CheckedChanged(object sender, EventArgs e)
        { // Changing test type for different radio buttons
            RadioButton sent = sender as RadioButton;
            if (sent.Checked) testType = int.Parse((string)sent.Tag);
        }


        // Code here to show textbox 'hints'
        //(information about req. input)
        private void InputLeave(object sender, EventArgs e)
        {
            TextBox sent = sender as TextBox;
            if (sent == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(sent.Text))
            {
                sent.Text = (string)sent.Tag;
            }
        }

        private void InputEnter(object sender, EventArgs e)
        {
            TextBox sent = sender as TextBox;
            if (sent == null)
            {
                return;
            }
            if (sent.Text == (string)sent.Tag)
            {
                sent.Text = string.Empty;
            }
        } // End of code to show and hide hints

        private void buttonReset_Click(object sender, EventArgs e)
        {
            // Code to reset the main form to defaults

            radioButton1.Text = H1 + ":  p < " + H0 + " p";
            radioButton2.Text = H1 + ":  p > " + H0 + " p";
            radioButton3.Text = H1 + ":  p ≠ " + H0 + " p";

            OutComparisonValues.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            testType = 0;

            inputN.Text = "n";
            inputP.Text = "p";
            inputR.Text = "r";
            inputA.Text = "α";

            SetDefaultOutput(-1);

            panelConclusion.Visible = false;
            OutConclusion.Text = "";

            OutRejected.Text = "";
            OutAccepted.Text = "";

            this.ActiveControl = promptNP;

            CalcSuccess = false;

            try
            { // Only show 'sample data' button when debugging

#if DEBUG
                butt_SampleDataLoad.Visible = true;
#else
                butt_SampleDataLoad.Visible = false;
#endif

            }
            catch (Exception ex)
            {
            }
        } // End of reset code

        // -------------     CALCULATE     -------------
        private bool ValuesAreOkay = false; // Verification boolean
        private bool CalcSuccess = false;
        private void buttonCalc_Click(object sender, EventArgs e)
        {
            buttonCalc.Tag = "busy";
            ValuesAreOkay = false;
            CalcSuccess = false;

            // Read all numerical inout to strings
            string n_s = inputN.Text;
            string p_s = inputP.Text;
            string r_s = inputR.Text;
            string a_s = inputA.Text;

            VerifyValues(n_s, p_s, r_s, a_s);
            // Calls method to check variables parse and are within limits

            if (ValuesAreOkay)
            {
                NoTestSpec = false;

                string NullProbString = inputP.Text;
                radioButton1.Text = H1 + ":  p < " + NullProbString;
                radioButton2.Text = H1 + ":  p > " + NullProbString;
                radioButton3.Text = H1 + ":  p ≠ " + NullProbString;

                CalcSuccess = true;

                nValue = Int64.Parse(n_s);
                pValue = decimal.Parse(p_s);
                rValue = Int64.Parse(r_s);
                aValue = decimal.Parse(a_s);
                SetDefaultOutput(rValue);

                decimal P_XetR =
                    htMaths.BinomialProb_XetR(nValue, pValue, rValue);
                string P_XetR_string = P_XetR.ToString("0.0000");
                text_P_XetR.Text += P_XetR_string;

                decimal P_XltR =
                    htMaths.BinomialProb_XltR(nValue, pValue, rValue);
                string P_XltR_string = P_XltR.ToString("0.0000");
                text_P_XltR.Text += P_XltR_string;

                decimal P_XltetR =
                    htMaths.BinomialProb_XltetR(nValue, pValue, rValue);
                string P_XltetR_string = P_XltetR.ToString("0.0000");
                text_P_XltetR.Text += P_XltetR_string;

                decimal P_XgtR =
                    htMaths.BinomialProb_XgtR(nValue, pValue, rValue);
                string P_XgtR_string = P_XgtR.ToString("0.0000");
                text_P_XgtR.Text += P_XgtR_string;

                decimal P_XgtetR =
                    htMaths.BinomialProb_XgtetR(nValue, pValue, rValue);
                string P_XgtetR_string = P_XgtetR.ToString("0.0000");
                text_P_XgtetR.Text += P_XgtetR_string;


                bool AltAcceptedBool =
                    htMaths.AltAccepted(testType, nValue, pValue, rValue, aValue);


                string stringAccept = "";
                string stringReject = "";
                if (AltAcceptedBool)
                {
                    stringAccept = "alternative";
                    stringReject = "null";
                }
                else
                {
                    stringAccept = "null";
                    stringReject = "alternative";
                }
                string stringHowAccept = "accepted";
                if (!AltAcceptedBool) stringHowAccept = "kept";

                OutRejected.Text = "• The "
                    + stringReject
                    + " hypothesis has been rejected.";
                OutAccepted.Text = "• The "
                    + stringAccept
                    + " hypothesis has been "
                    + stringHowAccept
                    + ".";

                string OutConclusionString =
                    htStrings.TestConclusion(false, "",
                    testType, AltAcceptedBool, nValue, pValue);
                panelConclusion.Visible = true;

                OutConclusion.Text = OutConclusionString;
                Clipboard.SetText(OutConclusionString);
                // ^ Copy conclusion to clipboard

                string Comparison =
                    htStrings.CriticalComparison(testType,
                    nValue, pValue, rValue, aValue);

                OutComparisonValues.Text = Comparison;

                if (ShowExactValues)
                { // Exact values button has been clicked
                    string s1 = "P(X = x) = "
                        + P_XetR.ToString("0.00000000000000");
                    string s2 = "P(X < x) = "
                        + P_XltR.ToString("0.00000000000000");
                    string s3 = "P(X ≤ x) = "
                        + P_XltetR.ToString("0.00000000000000");
                    string s4 = "P(X > x) = "
                        + P_XgtR.ToString("0.00000000000000");
                    string s5 = "P(X ≥ x) = "
                        + P_XgtetR.ToString("0.00000000000000");

                    MessageBox.Show(s1 + "\n"
                        + s2 + "\n"
                        + s3 + "\n"
                        + s4 + "\n"
                        + s5,
                        "Exact Values",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ShowExactValues = false;
                }
            }
            else
            {
                ValuesAreOkay = false;
                ShowExactValues = false;
            }

            buttonCalc.Tag = "complete";
            headTitle.Focus();
        }


        private void VerifyValues(string n_s, string p_s, string r_s, string a_s)
        {
            Int64 n;
            decimal p;
            Int64 r;
            decimal a;

            bool nCanParse = Int64.TryParse(n_s, out n);
            bool pCanParse = decimal.TryParse(p_s, out p);
            bool rCanParse = Int64.TryParse(r_s, out r);
            bool aCanParse = decimal.TryParse(a_s, out a);

            if (n > 20 || n < 1) nCanParse = false;
            if (r > n || r < 0) rCanParse = false;
            if (p > 1 || p < 0) pCanParse = false;
            if (a > 100 || a < 0) aCanParse = false;

            if ((Truth(nCanParse, pCanParse, rCanParse, aCanParse) == 4)
                & (testType != 0))
            {
                ValuesAreOkay = true;
            }
            else
            {
                if (!nCanParse)
                {
                    MessageBox.Show(ErrorStringMachine("n", n_s),
                    "Input error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }

                if (!pCanParse)
                {
                    MessageBox.Show(ErrorStringMachine("p", p_s),
                      "Input error",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                }
                if (!rCanParse)
                {
                    MessageBox.Show(ErrorStringMachine("r", r_s),
                    "Input error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                if (!aCanParse)
                {
                    MessageBox.Show(ErrorStringMachine("α", a_s),
                    "Input error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                if (testType == 0)
                {
                    MessageBox.Show("Please select the type of test",
                    "Input error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private string ErrorStringMachine(string WhichValue, string GivenValue)
        {
            string result = "Invalid value for "
                + WhichValue
                + ":  '"
                + GivenValue
                + "'.\nPlease see help for more information.";

            Clipboard.SetText(result);
            return result;
        }

        public static int Truth(params bool[] booleans)
        {
            return booleans.Count(b => b);
        }

        private void inputR_TextChanged(object sender, EventArgs e)
        {
            Int64 rvalue;
            bool correctr = Int64.TryParse(inputR.Text, out rvalue);
            if (correctr)
            {
                SetDefaultOutput(rvalue);
            }
            else
            {
                SetDefaultOutput(-1);
            }
        }

        /// <summary>
        /// Updates text for output boxes' default message.
        /// </summary>
        /// <param name="rvalue_int"></param>
        private void SetDefaultOutput(Int64 rvalue_int)
        {
            // < > ≥ ≤
            if (rvalue_int >= 1)
            {
                text_P_XetR.Text = "P(X = "
                    + rvalue_int.ToString() + ") = ";
                text_P_XltR.Text = "P(X < "
                    + rvalue_int.ToString() + ") = ";
                text_P_XltetR.Text = "P(X ≤ "
                    + rvalue_int.ToString() + ") = ";
                text_P_XgtR.Text = "P(X > "
                    + rvalue_int.ToString() + ") = ";
                text_P_XgtetR.Text = "P(X ≥ "
                    + rvalue_int.ToString() + ") = ";
            }
            else
            {
                text_P_XetR.Text = "P(X = x) = ";
                text_P_XltR.Text = "P(X < x) = ";
                text_P_XltetR.Text = "P(X ≤ x) = ";
                text_P_XgtR.Text = "P(X > x) = ";
                text_P_XgtetR.Text = "P(X ≥ x) = ";
            }
        }

        private void butt_SampleDataLoad_Click(object sender, EventArgs e)
        { // Load sample (example test) data to input boxes
            inputN.Text = "20";
            inputP.Text = "0.5";
            inputR.Text = "17";
            inputA.Text = "5";
            radioButton2.PerformClick();
        }

        private bool ShowExactValues = false;
        private void butt_ExactValues_Click(object sender, EventArgs e)
        {
            ShowExactValues = true;
            buttonCalc.PerformClick();
        }

        private bool NoTestSpec = false;
        private bool OkToOpenTxt = false;
        private string FilePath = "";
        private void butTxtGen_Click(object sender, EventArgs e)
        {
            // Method for when test generator button is clicked.
            if (CalcSuccess)
            {
                NoTestSpec = false;
                bool AcceptAlt =
                    htMaths.AltAccepted(testType, nValue, pValue, rValue, aValue);


                SaveText(htStrings.TestText(nValue, pValue, rValue, aValue, testType, AcceptAlt, htStrings.TestConclusion(false, "", testType, AcceptAlt, nValue, pValue)));
                // SaveText is my own method; listed below.


                if (OkToOpenTxt)
                { // Checks boolean value to confirm it's safe to load txt.
                    TextView TextViewForm = new TextView();
                    TextViewForm.FilePath = FilePath;
                    TextViewForm.Show();
                }
            }
            else if (!CalcSuccess & !NoTestSpec)
            { // Open an empty test editor form.
                DialogResult GenTestDialogResult =
                    MessageBox.Show("No valid test has been run for these values. Would you like to open the editor anyway?",
                    "Load editor without test?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (GenTestDialogResult == DialogResult.Yes)
                {
                    NoTestSpec = true;
                    TextView TextViewForm = new TextView();
                    TextViewForm.FilePath = "no-test";
                    TextViewForm.Show();
                    NoTestSpec = false;
                }
            }
        }

        private void SaveText(string OutputString)
        { // Saves a text file using SaveFileDialog ('Save As').
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter =
                ("txt files (*.txt)|*.txt"/*|All files (*.*)|*.*"*/);
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

                FilePath = saveFileDialog1.FileName;

                OkToOpenTxt = true;
            }
        }

        private void inputP_TextChanged(object sender, EventArgs e)
        { // Resets label when probability input is changed.
            radioButton1.Text = H1 + ":  p < " + H0 + " p";
            radioButton2.Text = H1 + ":  p > " + H0 + " p";
            radioButton3.Text = H1 + ":  p ≠ " + H0 + " p";
        }

        private void InputBoxKeyDown(object sender, KeyEventArgs e)
        { // Enter button acting as a shortcut to 'press calculate'.
            if (e.KeyCode == Keys.Enter) buttonCalc.PerformClick();
        }

    }
}
