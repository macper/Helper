using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelper.Web
{
    public interface IActivityLogger
    {
        void AddEntryLog(DateTime time, string username, string password, ActivityState state);
        IEnumerable<IActivityLog> GetEntryLogs(int startIndex, int pageSize);
        IEnumerable<IActivityLog> GetEntryLogs(DateTime time, int startIndex, int pageSize);
    }

    public interface IActivityLog
    {
        int Id { get; set; }
        DateTime Time { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        ActivityState State { get; set; }
    }

    public enum ActivityState { Sucess = 0, Failed = 1 }
}