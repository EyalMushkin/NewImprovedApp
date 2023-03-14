using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewImprovedApp
{
    [Obsolete]
    internal class MyHandler : Handler
    {
        int counter = 0;
        Context context;
        public MyHandler(Context context)
        {
            this.context = context;
        }
        public override void HandleMessage(Message msg)
        {
            Toast.MakeText(context, "" + msg.Arg1, ToastLength.Long).Show();
        }
    }
}