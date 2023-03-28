using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.Orders
{
    public enum OrderStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10, // Wait for Payment

        /// <summary>
        /// Processing
        /// </summary>
        Processing = 20, // Payed for send

        /// <summary>
        /// Processing
        /// </summary>
        Sent = 25, // Sent

        /// <summary>
        /// Complete
        /// </summary>
        Complete = 30,

        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 40
    }
}
