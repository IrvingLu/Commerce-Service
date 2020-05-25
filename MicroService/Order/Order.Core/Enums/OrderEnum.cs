using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Order.Core.Enums
{
    public enum OrderStatus
    {
        [Description("新订单")]
        neworder,
        [Description("代付款")]
        nopay,

    }
}
