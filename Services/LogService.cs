using Core.Data;
using Core.Domain.Logs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    
    public class LogService : ILog
    {

        //private readonly IRepository<Log> _logRepository;
        CRMEntities _context;

        public LogService()
        {
            _context = new CRMEntities();
        }
        public void Insert(Log log)
        {
            //_context.Log.Add(log);
            //_context.SaveChanges();
        }
    }
}
