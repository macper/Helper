using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DnDHelper.Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            MacObjectBuilder.RegisterType(typeof(Account.ISecurityProvider), new Account.SecurityObjectProvider(Application));
            MacObjectBuilder.RegisterType(typeof(IActivityLog), new ActivityLog());
            MacObjectBuilder.RegisterType(typeof(IActivityLogger), new ActivityLogger());
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            if (Application["UsersOnline"] == null)
            {
                Application["UsersOnline"] = 0;
            }
            int usersOnline = (int)Application["UsersOnline"];
            usersOnline++;
            Application["UsersOnline"] = usersOnline;
        }

        void Session_End(object sender, EventArgs e)
        {
            if (Application["UsersOnline"] == null)
            {
                return;
            }
            int usersOnline = (int)Application["UsersOnline"];
            usersOnline--;
            Application["UsersOnline"] = usersOnline;
        }

    }
}
