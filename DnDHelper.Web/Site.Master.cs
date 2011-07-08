using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DnDHelper.Web
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersOnline.Text = Membership.GetNumberOfUsersOnline().ToString();
            if (Page.User.IsInRole("Administrator"))
            {
                NavigationMenu.Items.Add(new MenuItem() { Text = "Bezpieczeństwo", NavigateUrl = "~/Admin/UserManagement.aspx" });
            }
        }
    }
}
