using System.ComponentModel;
using System.Reflection;
//Using reflection for automatic description retrieval. Can be slower than hardcoding descriptions.
//Alternative: Using a Dictionary to store descriptions for a faster retrieval
//Alternative: Using a switch statement to return descriptions

namespace TicketToCode.Core.Enums;

public static class EnumUtilities
{
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        string description = attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();

        if (attributes != null && attributes.Length > 0)
        {
            return $"{value.ToString()}: {description}";

        }
        else
        {
            return value.ToString();
        }

    }
}