using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;

namespace DnDHelper.Web.Account
{
    public class MacSqlMembershipProvider : MembershipProvider
    {
        private string _appName;

        public override string ApplicationName
        {
            get
            {
                return _appName;
            }
            set
            {
                _appName = value;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().ChangePassword(username, oldPassword, newPassword);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().CreateUser(username, password, out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().DeleteUser(username, deleteAllRelatedData);
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public override int GetNumberOfUsersOnline()
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetNumbersOfUsersOnline();
        }

        public override string GetPassword(string username, string answer)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetPassword(username);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetUser(username);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().GetUser((int)providerUserKey);
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            MacObjectBuilder.GetObject<ISecurityProvider>().UpdateUser(user);
        }

        public override bool ValidateUser(string username, string password)
        {
            return MacObjectBuilder.GetObject<ISecurityProvider>().ValidateUser(username, password);
        }
    }

    
}