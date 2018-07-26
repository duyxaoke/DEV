using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Equinox.Domain.Core.Enums
{
    public enum DepWithType
    {
        [Description("Deposit")]
        Deposit = 0,
        [Description("Withdraw")]
        Withdraw = 1,
    }
}
