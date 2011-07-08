using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelper.Web
{
    public static class ExtensionClass
    {
        public static IEnumerable<T> ConvertCollection<T,K>(this IQueryable<K> collection) where T:IConvertibleFrom<K>
        {
            List<T> list = new List<T>();
            foreach (var e in collection)
            {
                list.Add((T)MacObjectBuilder.GetObject<T>().ConvertFrom(e));
            }
            return list;
        }

        public static DateTime FromNullable(this DateTime? source)
        {
            if (source == null)
                return DateTime.MinValue;
            return (DateTime)source;
        }
    }

    public interface IConvertibleFrom<T>
    {
        object ConvertFrom(T source);
    }

  
}