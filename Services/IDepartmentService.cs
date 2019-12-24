using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Core.Data;
using Core.Domain.Departments;

namespace Services
{
    
    public interface IDepartmentService
    {


        List<Department> GetAllDepartments();


        //public Tbl_Section GetSectionById(int id)
        //{
        //    try
        //    {
        //        //check if user already exists
        //        var res = from t in db.Tbl_Section
        //                  where t.Section_ID == id
        //                  select t;
        //        return res.FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Tbl_Section();
        //    }
        //}

    }
}
