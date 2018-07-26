using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Equinox.Domain.Core.Commands
{
    public static class Helper
    {
        public const int Enable = 1;
        public const int Disable = 0;
        public const bool Active = true;
        public const bool InActive = false;
        public const string MessageSuccess = "Thành công!";
        public const string MessageError = "Thất bại!";

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


    }
}
