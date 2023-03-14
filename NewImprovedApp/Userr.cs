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
    internal class Userr
    {
        public string username { get; private set; }
        public string password { get; private set; }
        public string email { get; private set; }

        public Userr(string name, string password, string email)
        {
            this.username = name;
            this.password = password;
            this.email = email;
        }

        public override string ToString()
        {
            string str = "";
            str += "username: " + username;
            str += "\npassowrd: " + password;
            str += "\nemail: " + email;
            return str;
        }
    }
}