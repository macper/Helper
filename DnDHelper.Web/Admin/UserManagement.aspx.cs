using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DnDHelper.Web.Account;
using System.Web.Security;

namespace DnDHelper.Web.Admin
{
    public partial class UserManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void usersGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand'";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(usersGrid, "Select$" + e.Row.RowIndex);
                
            }
        }

        protected void usersGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            userRoles.DataSource = System.Web.Security.Roles.GetRolesForUser(usersGrid.SelectedRow.Cells[1].Text);
            userRoles.DataBind();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (tbSearchName.Text != string.Empty)
            {
                usersSource.SelectMethod = "GetUsersByName";
                if (usersSource.SelectParameters.Count == 0)
                {
                    usersSource.SelectParameters.Add(new ControlParameter("name", "tbSearchName", "Text"));
                }
            }
            else
            {
                usersSource.SelectMethod = "GetAllUsers";
                foreach (Parameter p in usersSource.SelectParameters)
                {
                    ControlParameter c = p as ControlParameter;
                    if (c != null)
                    {
                        usersSource.SelectParameters.Remove(c);
                        break;
                    }
                }
            }
            usersGrid.DataBind();
        }

        protected void addRole_Click(object sender, EventArgs e)
        {
            if (allRoles.SelectedValue != string.Empty)
            {
                if (usersGrid.SelectedRow != null)
                {
                    System.Web.Security.Roles.AddUserToRole(usersGrid.SelectedRow.Cells[1].Text, allRoles.SelectedValue);
                    userRoles.DataSource = System.Web.Security.Roles.GetRolesForUser(usersGrid.SelectedRow.Cells[1].Text);
                    userRoles.DataBind();
                }
            }
        }

        protected void removeRole_Click(object sender, EventArgs e)
        {
            if (userRoles.SelectedValue != string.Empty)
            {
                if (usersGrid.SelectedRow != null)
                {
                    System.Web.Security.Roles.RemoveUserFromRole(usersGrid.SelectedRow.Cells[1].Text, userRoles.SelectedValue);
                    userRoles.DataSource = System.Web.Security.Roles.GetRolesForUser(usersGrid.SelectedRow.Cells[1].Text);
                    userRoles.DataBind();
                }
            }
        }

        protected void removeUser_Click(object sender, EventArgs e)
        {
            if (usersGrid.SelectedRow != null)
            {
                Membership.DeleteUser(usersGrid.SelectedRow.Cells[1].Text);
                usersGrid.DataBind();
            }
        }
    }
}