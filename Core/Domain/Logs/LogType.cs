using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Logs
{
    public enum LogType
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        Error = 4
    }
}
