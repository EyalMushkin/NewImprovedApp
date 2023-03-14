using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Gms.Tasks;
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
using Firebase.Database;
using AndroidX.AppCompat.App;
using FluentValidation.Validators;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Android.Preferences;
using NewImprovedApp;
using static Android.App.DownloadManager;
using Xamarin.Essentials;

namespace NewImprovedApp
{
    [Activity(Label = "LogInCode")]
    public class LogInCode : Activity, IOnSuccessListener
    {
        EditText username1, password1;
        ImageButton btn1;
        public FirebaseFirestore db;
        List<DocumentSnapshot> Docs_In_DataBase = new List<DocumentSnapshot>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Logingin);
            username1 = FindViewById<EditText>(Resource.Id.username1);
            password1 = FindViewById<EditText>(Resource.Id.pass11);
            btn1 = FindViewById<ImageButton>(Resource.Id.ivbtn2);

            db = GetDataBase();
            _ = LoadItemsAsync();

            btn1.Click += btn1_Click;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (var doc in Docs_In_DataBase)
            {
                if (doc.Get("username").Equals(username1.Text) && doc.Get("password").Equals(password1.Text))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                OnOpenActivity.username5 = username1.Text;
                OnSignIn1();
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "Your password or username is not registered", ToastLength.Long).Show();
            }
        }
        public void OnSignIn1()
        {
            //ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            //ISharedPreferencesEditor editor = prefs.Edit();
            //editor.PutBoolean("isSignedIn", true);
            //editor.Apply();
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
        private async System.Threading.Tasks.Task LoadItemsAsync()
        {
            Docs_In_DataBase.Clear();

            Firebase.Firestore.Query q = db.Collection("students");
            await q.Get().AddOnSuccessListener(this);

        }

        public void OnSuccess(Java.Lang.Object result)
        {

            var snapshot = (QuerySnapshot)result;
            foreach (var doc in snapshot.Documents)
            {
                Docs_In_DataBase.Add(doc);

            }
        }

    }
}