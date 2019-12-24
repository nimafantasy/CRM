using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Events
{
    public enum EventType
    {
        Presentation = 1,
        Training = 2,
        Financial = 3,
        DisputeSettlement = 4
    }
}
