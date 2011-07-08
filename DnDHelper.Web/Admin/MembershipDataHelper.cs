using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DnDHelper.Web.Admin
{
    public class MembershipDataHelper
    {
        public static MembershipUserCollection GetAllUsers(int startIndex, int pageSize)
        {
            int tot;
            return Membership.GetAllUsers(startIndex, pageSize, out tot);
        }

        public static MembershipUserCollection GetUsersByName(string name, int startIndex, int pageSize)
        {
            int tot;
            return Membership.FindUsersByName(name, startIndex, pageSize, out tot);
        }

        public static int GetUsersCount()
        {
            return Membership.GetAllUsers().Count;
        }

        public static int GetUsersCount(string name)
        {
            return Membership.FindUsersByName(name).Count;
        }

        public static string[] GetAllRoles()
        {
            return System.Web.Security.Roles.GetAllRoles();
        }
    }
}