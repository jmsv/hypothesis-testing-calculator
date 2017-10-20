using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HypothesisTestingAndroid
{
	using System.Threading;

	using Android.App;
	using Android.OS;

	[Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, Label = "Hypothesis Testing", Icon = "@drawable/htcIcon")]
	public class SplashActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			//SetContentView (Resource.Layout.splashLayout);
			base.OnCreate(bundle);

			//Thread.Sleep(100); // Simulate a long loading process on app startup.
			StartActivity(typeof(HypothesisTestingAndroid.MainActivity));
		}
	}
}

