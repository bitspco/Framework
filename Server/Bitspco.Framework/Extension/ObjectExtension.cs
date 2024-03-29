﻿using System;

namespace Bitspco.Framework.Extension
{
    public static class ObjectExtension
    {
        public static T Clone<T>(this T obj)
        {
            var type = obj.GetType();
            var tmp = Activator.CreateInstance<T>();
            foreach (var item in type.GetProperties())
            {
                item.SetValue(tmp, item.GetValue(obj));
            }
            return tmp;
        }
        public static T Overwrite<T>(this T obj1, T obj2, bool ignoreDefaults = false)
        {
            if (ignoreDefaults)
            {
                var obj = Activator.CreateInstance<T>();
                foreach (var property in obj1.GetType().GetProperties())
                {
                    var value = property.GetValue(obj2);
                    if (value.Equals(property.GetValue(obj))) continue;
                    property.SetValue(obj1, value);
                }
            }
            else
            {
                foreach (var property in obj1.GetType().GetProperties()) property.SetValue(obj1, property.GetValue(obj2));
            }
            return obj1;
        }
        public static T Overwrite<T, R>(this T obj1, R obj2, bool ignoreDefaults = false) where T : R
        {
            foreach (var property in obj2.GetType().GetProperties()) property.SetValue(obj1, property.GetValue(obj2));
            return obj1;
        }
        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsString(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.String:
                    return true;
                default:
                    return false;
            }
        }
    }
}
