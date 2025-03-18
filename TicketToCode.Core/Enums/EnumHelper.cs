using System.ComponentModel;
using System.Reflection;

namespace TicketToCode.Core.Enums;

 public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            string description = attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
            return $"{value.ToString()}: {description}";
        }
    }