using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewImprovedApp
{
    [Activity(Label = "OnOpenActivity", MainLauncher =true)]
    public class OnOpenActivity : Activity
    {
        ImageButton imgbtnHi;
        public static string username5;
        public static string reusername5;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            imgbtnHi = FindViewById<ImageButton>(Resource.Id.action_menu_presenter);
            imgbtnHi.Click += ImgbtnHi_Click;
        }

        private void ImgbtnHi_Click(object sender, EventArgs e)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            bool isSignedIn = prefs.GetBoolean("isSignedIn", false);
            if (isSignedIn)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(SignInCode));
                StartActivity(intent);
            }
        }
    }
}