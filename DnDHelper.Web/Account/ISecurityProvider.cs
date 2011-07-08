using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace DnDHelper.Web.Account
{
    public interface ISecurityProvider
    {
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        MembershipUser CreateUser(string userName, string password, out MembershipCreateStatus status);
        bool DeleteUser(string username, bool deleteAllRelatedData);
        MembershipUserCollection GetUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

        MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);

        int GetNumbersOfUsersOnline();

        string GetPassword(string userName);

        MembershipUser GetUser(string username);

        MembershipUser GetUser(int p);

        void UpdateUser(MembershipUser user);

        bool ValidateUser(string username, string password);

        void AddUsersToRoles(string[] usernames, string[] roleNames);

        void CreateRole(string roleName);

        bool DeleteRole(string roleName);

        string[] FindUsersInRole(string roleName, string usernameToMatch);

        string[] GetAllRoles();

        string[] GetRolesForUser(string username);

        string[] GetUsersInRole(string roleName);

        bool IsUserInRole(string username, string roleName);

        void RemoveUsersFromRoles(string[] usernames, string[] roleNames);

        bool RoleExists(string roleName);
    }
}
