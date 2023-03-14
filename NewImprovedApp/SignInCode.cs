using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Firebase;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NewImprovedApp
{
    [Activity(Label = "SignInCode")]
    public class SignInCode : Activity
    {
        TextView tv1;
        EditText username, password, password2, email;
        ImageButton imgBtn;
        public FirebaseFirestore db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.SigningForm);
            username = FindViewById<EditText>(Resource.Id.username);
            email = FindViewById<EditText>(Resource.Id.email);
            password = FindViewById<EditText>(Resource.Id.pass1);
            password2 = FindViewById<EditText>(Resource.Id.pass2);
            tv1 = FindViewById<TextView>(Resource.Id.logint);
            imgBtn = (ImageButton)FindViewById<ImageButton>(Resource.Id.ivbtn1);
            db = GetDataBase();

            imgBtn.Click += imgBtn_Click;
            tv1.Click += Tv1_Click;
        }

        private void Tv1_Click(object sender, EventArgs e)
        {
            OnOpenActivity.username5 = username.Text;
            OnSignIn();
            Intent intent = new Intent(this, typeof(LogInCode));
            StartActivity(intent);
        }
        public void OnSignIn()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean("isSignedIn", true);
            editor.Apply();
        }

        public FirebaseFirestore GetDataBase()
        {
            FirebaseFirestore db;
            var options = new Firebase.FirebaseOptions.Builder()
                .SetProjectId("hifivestorage-8b9ef")
                .SetApplicationId("hifivestorage-8b9ef")
                .SetApiKey("AIzaSyB3gperHzxqsvlhnUbZZZf_Q3U5y_-pGMo")
                .SetStorageBucket("hifivestorage-8b9ef.appspot.com")
                .Build();
            var app = FirebaseApp.InitializeApp(this, options);
            db = FirebaseFirestore.GetInstance(app);
            return db;
        }

        private void imgBtn_Click(object sender, EventArgs e)
        {
            string emailCheck = email.Text;
            if (IsValidEmail(email))
            {
                if (password.Equals(password2))
                {
                    AddItem();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Re-confirm password", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Invalid email address", ToastLength.Long).Show();
            }
        }

        private void AddItem()
        {
            // Create a HashMap to store your data like an object
            // HashMap is a collection of "keys" and "values
            HashMap map = new HashMap();
            // save data
            // map.Put([field name], content);
            map.Put("email", email.Text.Trim().ToLower());
            map.Put("password", (password.Text.Trim()));
            map.Put("username", username.Text.Trim().ToLower());
            // create an empty document reference for firestore
            DocumentReference docRef = db.Collection("students").Document();
            // puts the map info in the document
            docRef.Set(map);
        }
        private bool IsValidEmail(EditText email)
        {
            string email1 = email.Text.Trim();
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.Match(email1).Success;
        }

    }
}