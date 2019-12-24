using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Requests
{
    public enum RequestStatus
    {
        WaitingUserResponse = 1,
        WaitingCustomerResponse = 2,
        WaitingCustomerPayment = 3,
        CencelledByUser = 4,
        CancelledByCustomer = 5,
        Delivered = 6
    }
}
