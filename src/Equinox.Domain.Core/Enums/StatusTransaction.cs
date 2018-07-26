using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Equinox.Domain.Core.Enums
{
    public enum StatusTransaction
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Confirmed")]
        Confirmed = 1,
        [Description("Cancel")]
        Cancel = 2,
    }
}
