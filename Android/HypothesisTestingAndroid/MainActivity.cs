using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Text;

namespace HypothesisTestingAndroid
{
	[Activity (Label = "Hypothesis Testing", Icon = "@drawable/htcIcon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);

			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Typeface lekton = Typeface.CreateFromAsset(Application.Context.Assets, "Lekton_Regular.ttf"); // Custom font

			TextView p_XetX = FindViewById<TextView> (Resource.Id.p_XetX); // First textview
			p_XetX.Typeface = lekton; // assigning font 'lekton' to the textview
			p_XetX.Text = "P(X = x)=";
			TextView p_XltORetX = FindViewById<TextView> (Resource.Id.p_XltORetX); // Second textview
			p_XltORetX.Typeface = lekton;
			p_XltORetX.Text = "P(X ≤ x)=";

			TextView editN = FindViewById<TextView> (Resource.Id.editN);
			TextView editP = FindViewById<TextView> (Resource.Id.editP);
			TextView editR = FindViewById<TextView> (Resource.Id.editR);
			TextView editA = FindViewById<TextView> (Resource.Id.editA);

			editN.Typeface = lekton;
			editP.Typeface = lekton;
			editR.Typeface = lekton;
			editA.Typeface = lekton;



			RadioButton radioOneTailed1 = FindViewById<RadioButton> (Resource.Id.radioOneTailed1);
			RadioButton radioOneTailed2 = FindViewById<RadioButton> (Resource.Id.radioOneTailed2);
			RadioButton radioTwoTailed = FindViewById<RadioButton> (Resource.Id.radioTwoTailed);

			radioOneTailed1.Typeface = lekton;
			radioOneTailed1.Text = "One-Tailed: P:H0 > P:H1";
			radioOneTailed2.Typeface = lekton;
			radioOneTailed2.Text = "One-Tailed: P:H0 < P:H1";
			radioTwoTailed.Typeface = lekton;
			radioTwoTailed.Text  = "Two-Tailed: P:H0 ≠ P:H1";



			Button calcButt = FindViewById<Button> (Resource.Id.calcButt);
			calcButt.Click += CalcButt_Click;

			Button resetButt = FindViewById<Button> (Resource.Id.resetButt);
			resetButt.Click += ResetButt_Click;


		}

		void ResetButt_Click (object sender, EventArgs e)
		{
			EditText nIn = FindViewById<EditText> (Resource.Id.editN);
			EditText pIn = FindViewById<EditText> (Resource.Id.editP);
			EditText rIn = FindViewById<EditText> (Resource.Id.editR);
			EditText aIn = FindViewById<EditText> (Resource.Id.editA);

			TextView p_XetX = FindViewById<TextView> (Resource.Id.p_XetX); // First textview
			TextView p_XltORetX = FindViewById<TextView> (Resource.Id.p_XltORetX); // Second textview

			nIn.Text = "";
			pIn.Text = "";
			rIn.Text = "";
			aIn.Text = "";

			p_XetX.Text = "P(X = x)=";
			p_XltORetX.Text = "P(X ≤ x)=";

			//ScrollView formScroll = FindViewById<ScrollView> (Resource.Id.scrollView1);
			//await formScroll.
		}

		void CalcButt_Click (object sender, EventArgs e)
		{
			EditText nIn = FindViewById<EditText> (Resource.Id.editN);
			EditText pIn = FindViewById<EditText> (Resource.Id.editP);
			EditText rIn = FindViewById<EditText> (Resource.Id.editR);
			EditText aIn = FindViewById<EditText> (Resource.Id.editA);

			string nTxt = nIn.Text;
			string pTxt = pIn.Text;
			string rTxt = rIn.Text;
			string aTxt = aIn.Text;

			int n = 0;
			double p = 0;
			int r = 0;
			double a = 0;

			bool nInS;
			bool pInS;
			bool rInS;
			bool aInS;

			nInS = Int32.TryParse (nTxt, out n);
			pInS = double.TryParse (pTxt, out p);
			rInS = Int32.TryParse (rTxt, out r);
			aInS = double.TryParse (aTxt, out a);


			Toast nBad = Toast.MakeText (this, "n = " + nTxt + " is not valid", ToastLength.Short);
			Toast pBad = Toast.MakeText (this, "p = " + pTxt + " is not valid", ToastLength.Short);
			Toast rBad = Toast.MakeText (this, "r = " + rTxt + " is not valid", ToastLength.Short);
			Toast aBad = Toast.MakeText (this, "a = " + aTxt + " is not valid", ToastLength.Short);

			bool inError = false; // __________ Displaying Error Messages: __________
			if (!nInS) // Error messages are only displayed if no previous errors were shown, so errors can be dealt with individually.
			{
				if (!inError)
				{
					nBad.Show (); // Show error message
					inError = true;
				}
			}
			if (!pInS || p > 1)
			{
				if (!inError)
				{
					pBad.Show (); // Show error message
					inError = true;
				}
			}
			if (!rInS || r > n)
			{
				if (!inError)
				{
					rBad.Show (); // Show error message
					inError = true;
				}
			}
			if (!aInS)
			{
				if (!inError)
				{
					aBad.Show (); // Show error message
					inError = true;
				}
			} // __________ End of Error Messages __________


			if (!inError) // Calculations only carried out if no parsing/maths errors were found.
			{
				TextView p_XetX = FindViewById<TextView> (Resource.Id.p_XetX); // First textview
				TextView p_XltORetX = FindViewById<TextView> (Resource.Id.p_XltORetX); // Second textview

				// Get values from htMaths class and convert to string
				string p_XetX_string = "P(X = x)= " + ((htMaths.BinomialProb_XetR (n, p, r)).ToString ("0.0000"));
				string p_XltORetX_string = "P(X ≤ x)= " + (htMaths.BinomialProb_XltORetR (n, p, r)).ToString ("0.0000");
				// Assign string values to label text
				p_XetX.Text = p_XetX_string;
				p_XltORetX.Text = p_XltORetX_string;

				// Labels centred



			}

		}
	}
}
