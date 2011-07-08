using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DnDHelper.Web.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginUser.LoggedIn += new EventHandler(LoginUser_LoggedIn);
            LoginUser.LoginError += new EventHandler(LoginUser_LoginError);
        }

        void LoginUser_LoginError(object sender, EventArgs e)
        {
            MacObjectBuilder.GetObject<IActivityLogger>().AddEntryLog(DateTime.Now, LoginUser.UserName, LoginUser.Password, ActivityState.Failed);
        }

        void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            MacObjectBuilder.GetObject<IActivityLogger>().AddEntryLog(DateTime.Now, this.LoginUser.UserName, null, ActivityState.Sucess);
            
        }
    }
}
