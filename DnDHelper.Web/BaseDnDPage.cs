using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelper.Web
{
    public class BaseDnDPage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}