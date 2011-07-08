using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;
using System.Security;

namespace DnDHelper.Web.Account
{
    public class SecurityObjectProvider : ISecurityProvider
    {
        HttpApplicationState _application;

        public SecurityObjectProvider(HttpApplicationState app)
        {
            _application = app;
        }

        #region ISecurityProvider Members

        public MembershipUser CreateUser(string userName, string password, out MembershipCreateStatus status)
        {
            using (Security context = new Security())
            {
                Users usrDuplicate = context.Users.FirstOrDefault(f => f.Name == userName);
                if (usrDuplicate != null)
                {
                    throw new SecurityException("Istnieje już użytkownik o tej nazwie");
                }
                Users user = context.CreateObject<Users>();
                user.Name = userName;
                user.Password = GetSHA1Hash(password);
                context.Users.AddObject(user);
                context.SaveChanges();
                status = MembershipCreateStatus.Success;
                return new MembershipUser("MyProvider", userName, user.Id, null, null, null, true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            }
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            using (Security context = new Security())
            {
                Users user = context.Users.FirstOrDefault(f => f.Name == userName);
                if (user == null)
                    return false;
                string hash = GetSHA1Hash(oldPassword);
                if (user.Password == hash)
                {
                    user.Password = GetSHA1Hash(newPassword);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        private string GetSHA1Hash(string input)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(input, "SHA1");
        }


        public bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            using (Security context = new Security())
            {
                Users user = context.Users.FirstOrDefault(f => f.Name == username);
                if (user == null)
                    return false;

                context.Users.DeleteObject(user);
                context.SaveChanges();
                return true;
            }
        }


        public MembershipUserCollection GetUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            using (var context = new Security())
            {
                totalRecords = context.Users.Count(f => f.Name.StartsWith(usernameToMatch));
                var selUsr = from usr in context.Users
                             where usr.Name.StartsWith(usernameToMatch)
                             select usr;
                var toRet = selUsr.OrderBy(o=>o.Id).Skip(pageIndex).Take(pageSize);
                MembershipUserCollection coll = new MembershipUserCollection();
                foreach (var el in toRet)
                {
                    MembershipUser usr = new MembershipUser("MyProvider", el.Name, el.Id, null, null, null, true, false, DateTime.MinValue, el.LastLogin.FromNullable(), DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    coll.Add(usr);
                }
                return coll;
            }
        }

        public MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            using (var context = new Security())
            {
                totalRecords = context.Users.Count();
                MembershipUserCollection coll = new MembershipUserCollection();
                foreach (var el in context.Users.OrderBy(o => o.Id).Skip(pageIndex).Take(pageSize))
                {
                    MembershipUser usr = new MembershipUser("MyProvider", el.Name, el.Id, null, null, null, true, false, DateTime.MinValue, el.LastLogin.FromNullable(), DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    coll.Add(usr);
                }
                return coll;
            }
        }

        public int GetNumbersOfUsersOnline()
        {
            if (_application["UsersOnline"] == null)
            {
                return 0;
            }
            return (int)_application["UsersOnline"];
        }

        public string GetPassword(string username)
        {
            using (var context = new Security())
            {
                return context.Users.FirstOrDefault(f => f.Name == username).Password;
            }
        }

        public MembershipUser GetUser(string username)
        {
            using (var context = new Security())
            {
                var usr = context.Users.FirstOrDefault(f => f.Name == username);
                if (usr == null)
                {
                    return null;
                }
                return new MembershipUser("MyProvider", usr.Name, usr.Id, null, null, null, true, false, DateTime.MinValue, usr.LastLogin.FromNullable(), DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            }
        }

        public MembershipUser GetUser(int p)
        {
            using (var context = new Security())
            {
                var usr = context.Users.FirstOrDefault(f => f.Id == p);
                if (usr == null)
                {
                    return null;
                }
                return new MembershipUser("MyProvider", usr.Name, usr.Id, null, null, null, true, false, DateTime.MinValue, usr.LastLogin.FromNullable(), DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            }
        }

        public void UpdateUser(MembershipUser user)
        {
            using (var context = new Security())
            {
                var usrToUpdate = context.Users.FirstOrDefault(f => f.Id == (int)user.ProviderUserKey);
                usrToUpdate.Name = user.UserName;
                usrToUpdate.Password = user.GetPassword();
                usrToUpdate.LastLogin = user.LastLoginDate;
                context.SaveChanges();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            using (var context = new Security())
            {
                string hash = GetSHA1Hash(password);
                var usr = context.Users.FirstOrDefault(f => f.Name == username && f.Password == hash );
                if (usr == null)
                {
                    return false;
                }
                usr.LastLogin = DateTime.Now;
                context.SaveChanges();
                return true;
            }
        }

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            using (var context = new Security())
            {
                var users = from usr in context.Users where usernames.Contains(usr.Name) select usr;
                var roles = from rol in context.Roles where roleNames.Contains(rol.Name) select rol;
                foreach (var u in users)
                {
                    foreach (var r in roles)
                    {
                        r.UserToRole.Add(new UserToRole() { Roles = r, Users = u });
                    }
                }
                context.SaveChanges();
            }
        }

        public void CreateRole(string roleName)
        {
            using (var context = new Security())
            {
                context.Roles.AddObject(new Roles() { Name = roleName });
                context.SaveChanges();
            }
        }

        public bool DeleteRole(string roleName)
        {
            using (var context = new Security())
            {
                var role = context.Roles.FirstOrDefault(f => f.Name == roleName);
                if (role == null)
                {
                    return false;
                }
                context.Roles.DeleteObject(role);
                context.SaveChanges();
                return true;
            }
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            using (var context = new Security())
            {
                var role = context.Roles.FirstOrDefault(f => f.Name == roleName);
                List<string> users = new List<string>();
                foreach (var usR in role.UserToRole)
                {
                    users.Add(usR.Users.Name);
                }
                return users.ToArray();
            }
        }

        public string[] GetAllRoles()
        {
            using (var context = new Security())
            {
                var ct = from r in context.Roles select r.Name;
                return ct.ToArray();
            }
        }

        public string[] GetRolesForUser(string username)
        {
            using (var context = new Security())
            {
                var user = context.Users.FirstOrDefault(f => f.Name == username);
                if (user == null)
                { return null; }
                var rn = from ru in user.UserToRole select ru.Roles.Name;
                return rn.ToArray();
            }
        }

        public string[] GetUsersInRole(string roleName)
        {
            using (var context = new Security())
            {
                var role = context.Roles.FirstOrDefault(f => f.Name == roleName);
                if (role == null) return null;
                var un = from u in role.UserToRole select u.Users.Name;
                return un.ToArray();
            }
        }

        public bool IsUserInRole(string username, string roleName)
        {
            using (var context = new Security())
            {
                return context.UserToRole.FirstOrDefault(f => f.Users.Name == username && f.Roles.Name == roleName) != null;
            }
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            using (var context = new Security())
            {
                var sel = from utr in context.UserToRole where usernames.Contains(utr.Users.Name) && roleNames.Contains(utr.Roles.Name) select utr;
                foreach (var s in sel)
                {
                    context.UserToRole.DeleteObject(s);
                }
                context.SaveChanges();
            }
        }

        public bool RoleExists(string roleName)
        {
            using (var context = new Security())
            {
                return context.Roles.FirstOrDefault(f => f.Name == roleName) != null;
            }
        }

        #endregion
    }

    
}