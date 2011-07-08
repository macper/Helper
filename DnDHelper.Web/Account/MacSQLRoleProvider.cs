using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DnDHelper.Web.Account
{
    public class MacSQLRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            MacObjectBuilder.GetObject<ISecurityProvider>().AddUsersToRoles(usernames, roleNames);
        }

        public override string ApplicationName
        {
            get;
            set;
        }

        public override void CreateRole(string roleName)
        {
            MacObjectBuilder.GetObject<ISecurityProvider>().CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().DeleteRole(roleName);
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().FindUsersInRole(roleName, usernameToMatch);
        }

        public override string[] GetAllRoles()
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetAllRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetRolesForUser(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetUsersInRole(roleName);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().IsUserInRole(username, roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            MacObjectBuilder.GetObject<ISecurityProvider>().RemoveUsersFromRoles(usernames, roleNames);
        }

        public override bool RoleExists(string roleName)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().RoleExists(roleName);
        }
    }
}