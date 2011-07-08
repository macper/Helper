using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Security;

namespace DnDHelper.Web.Admin
{
    /// <summary>
    /// Summary description for UserManagementService
    /// </summary>
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserManagementService : System.Web.Services.WebService
    {

        [WebMethod]
        public Result CreateUser(string userName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    return new Result() { ErrorCode = 1, ErrorDescription = "Nazwa użytkownika i hasło muszą być ustawione" };
                }
                Membership.CreateUser(userName, password);
                return new Result() { ErrorCode = 0 };
            }
            catch (Exception exc)
            {
                return new Result() { ErrorCode = 2, ErrorDescription = "Wystąpił błąd: " + exc.ToString() };
            }
        }
    }

    public class Result
    {
        public int ErrorCode;
        public string ErrorDescription;
    }
}
