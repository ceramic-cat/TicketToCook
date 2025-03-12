using System;
using System.Reflection;
using System.ComponentModel;
//Using reflection for automatic description retrieval. Can be slower than hardcoding descriptions.
//Alternative: Using a Dictionary to store descriptions for a faster retrieval.

namespace TicketToCode.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();
            }
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
