using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Services
{
    public class SubActivityService
    {
        CRMEntities _context;

        public SubActivityService()
        {
            _context = new CRMEntities();
        }
        public List<Tbl_SubActivity> GetAllSubActivities()
        {
            try
            {
                return _context.Tbl_SubActivity.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetAllUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_SubActivity>();
            }
        }
    }
}
