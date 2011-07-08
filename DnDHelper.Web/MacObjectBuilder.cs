using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelper.Web
{
    public static class MacObjectBuilder
    {
        private static Dictionary<Type, object> _registeredTypes = new Dictionary<Type,object>();

        public static void RegisterType(Type t, object provider)
        {
            _registeredTypes.Add(t, provider);
        }

        public static T GetObject<T>()
        {
            Type t = typeof(T);
            if (_registeredTypes.Keys.Contains(t))
            {
                return (T)_registeredTypes[t];
            }
            throw new ApplicationException("Nie zarejestrowano typu " + t.ToString());
        }
    }

}