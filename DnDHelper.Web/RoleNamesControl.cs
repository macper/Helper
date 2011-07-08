using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelper.Web
{
    public class RoleNamesControl : System.Web.UI.WebControls.Label
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string roles = "";
            foreach (var r in System.Web.Security.Roles.GetRolesForUser())
            {
                roles += r;
                roles += ",";
            }
            Text = roles.TrimEnd(',');
        }
    }
}