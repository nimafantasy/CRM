using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Core.Data;
using Core.Domain.Departments;
using Core.Domain.Logs;

namespace Services
{
    
    public class DepartmentService
    {
        CRMEntities _context;
        //CRMContext _context;
        LogService _logService;
        public DepartmentService()
        {
            //_context = new CRMContext();
            _context = new CRMEntities();
            _logService = new LogService();
        }

        public bool AddNewRole()
        {
            return true;
        }

        public bool EditRole()
        {
            return true;
        }

        public bool RemoveRole()
        {
            return true;
        }

        public List<Tbl_Section> GetAllDepartments()
        {
            try
            {
                return _context.Tbl_Section.ToList();
            }
            catch (Exception ex)
            {
                _logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetAllDepartments", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
                return new List<Tbl_Section>();
            }
        }

        public Tbl_Section GetDepartmentById(int id)
        {
            try
            {
                return _context.Tbl_Section.First(x => x.Section_ID == id);
            }
            catch (Exception ex)
            {
                _logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetDepartmentById", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
                return new Tbl_Section();
            }
        }

    }
}
