using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelper.Web
{
    public class ActivityLogger : IActivityLogger
    {
        #region IActivityLogger Members

        public void AddEntryLog(DateTime time, string username, string password, ActivityState state)
        {
            using (var context = new Security())
            {
                context.Activity.AddObject(new Activity() { Time = time, UserName = username, Password = password, Status = (byte)state });
                context.SaveChanges();
            }
        }

        public IEnumerable<IActivityLog> GetEntryLogs(int startIndex, int pageSize)
        {
            using (var context = new Security())
            {
                return context.Activity.Skip(startIndex * pageSize).Take(pageSize).ConvertCollection<ActivityLog, Activity>();
            }
        }

        public IEnumerable<IActivityLog> GetEntryLogs(DateTime time, int startIndex, int pageSize)
        {
            using (var context = new Security())
            {
                return context.Activity.Where(f => f.Time >= time).Skip(startIndex * pageSize).Take(pageSize).ConvertCollection<ActivityLog, Activity>();
            }
        }

        #endregion
    }

    public class ActivityLog : IActivityLog, IConvertibleFrom<Activity>
    {
        #region IActivityLog Members

        public int Id
        {
            get
            {
                return _activity.Id;
            }
            set
            {
                _activity.Id = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return _activity.Time;
            }
            set
            {
                _activity.Time = value;
            }
        }

        public string UserName
        {
            get
            {
                return _activity.UserName;
            }
            set
            {
                _activity.UserName = value;
            }
        }

        public string Password
        {
            get
            {
                return _activity.Password;
            }
            set
            {
                _activity.Password = value;
            }
        }

        public ActivityState State
        {
            get
            {
                return (ActivityState)_activity.Status;
            }
            set
            {
                _activity.Status = (byte)value;
            }
        }

        #endregion

        private Activity _activity;
        public ActivityLog()
        {
        }
        public ActivityLog(Activity activity)
        {
            _activity = activity;
        }

        #region IConvertibleFrom<Activity> Members

        public object ConvertFrom(Activity source)
        {
            return new ActivityLog(source);
        }

        #endregion
    }

}