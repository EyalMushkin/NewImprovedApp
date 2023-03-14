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
using System.Threading;

namespace NewImprovedApp
{
    [Activity(Label = "MyService")]
    public class MyService : Service
    {
        int counter;
        MyHandler myhandler;
        public override void OnCreate()
        {
            base.OnCreate();
            myhandler = new MyHandler(this);
            // Create your application here
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            counter = intent.GetIntExtra("counter", 300);
            Toast.MakeText(this, "service started" + counter, ToastLength.Long).Show();
            Thread t = new Thread(Run);
            t.Start();
            return base.OnStartCommand(intent, flags, startId);
        }
        private void Run()
        {
            while (counter > 0)
            {
                Thread.Sleep(1000);
                Message mes = new Message();
                mes.Arg1 = counter;
                myhandler.SendMessage(mes);
                counter--;
                Console.WriteLine("MyService used");
            }
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}