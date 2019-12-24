using Core.Data;
using Core.Domain.Logs;
using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GroupService
    {

        CRMEntities db;
        public GroupService()
        {
            db =  new CRMEntities();
            //_GroupRepository = GroupRepository;
            //_logRepository = logRepository;

            
        }

        public bool AddNewGroup(Tbl_Group Group)
        {

            try
            {
                //check if user already exists
                var res = from t in db.Tbl_Group
                          where t.Group_ID == Group.Group_ID
                          select t;

                if (res.Count() == 0)
                {
                    db.Tbl_Group.Add(Group);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    Tbl_Group grp = res.FirstOrDefault();
                    grp.Active = Group.Active;
                    grp.Description = Group.Description;
                    grp.GroupName = Group.GroupName;
                    grp.LastUpdateDate = Group.LastUpdateDate;
                    grp.LastUpdateTime = Group.LastUpdateTime;
                    grp.LastUpdateUser_ID = Group.LastUpdateUser_ID;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //_logRepository.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "AddNewGroup", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
                return false;
            }
        }

        public void RemoveGroup(Tbl_Group Group)
        {
            //try
            //{
            //    //check if user exists
            //    var res = from t in _GroupRepository.Table
            //              where t.Id == Group.Id
            //              select t;

            //    if (res.Count() > 0)
            //    {
            //        _GroupRepository.Delete(Group);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    _logRepository.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "RemoveGroup", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
            //}
        }

        public void EditGroup(Tbl_Group Group)
        {
            //try
            //{
            //    var res = from t in _GroupRepository.Table
            //              where t.Id == Group.Id
            //              select t;

            //    if (res.Count() > 0)
            //    {
            //        _GroupRepository.Update(Group);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logRepository.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "EditGroup", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

            //}
        }


        public List<Tbl_Group> GetAllGroups()
        {
            try
            {
                var res = from t in db.Tbl_Group
                          select t;
                return res.ToList();
            }
            catch (Exception ex)
            {
                //_logRepository.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetAllGroups", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
                return new List<Tbl_Group>() { };
            }
            //return new List<Tbl_Group>();
        }

        public List<Tbl_Group> SearchGroups(string GroupName, string Description, int active)
        {
            try
            {
                //check if user already exists
                var res = from t in db.Tbl_Group
                          where (!string.IsNullOrEmpty(GroupName) && t.GroupName.Contains(GroupName)) || (!string.IsNullOrEmpty(Description) && t.Description.Contains(Description) || t.Active.Equals(active))
                          select t;
                return res.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_Group>();
            }
        }

        public Tbl_Group GetGroupById(int id)
        {
            try
            {

                //check if user already exists
                var res = from t in db.Tbl_Group
                          where t.Group_ID == id
                          select t;

                if (res.Count() > 0)
                {
                    // user already exists
                    return res.FirstOrDefault();
                }
                else
                {
                    return new Tbl_Group();
                }
            }
            catch (Exception ex)
            {
                return new Tbl_Group();
            }
        }

    public List<Tbl_Group> SearchGroups(string FirstName, string LastName, string PersonelId, string Username)
    {
        throw new NotImplementedException();
    }
}
}
