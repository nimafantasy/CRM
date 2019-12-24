using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserGroupService
    {
        CRMEntities _context;
        public UserGroupService()
        {
            _context = new CRMEntities();
        }

        public List<Tbl_Group> GetUserGroups(int user_id)
        {
            try
            {
                var partialResult = from ug in _context.Tbl_Group
                                    join g in _context.Tbl_User_Group
                                    on ug.Group_ID equals g.Group_ID
                                    where g.User_ID == user_id
                                    select ug;


                return partialResult.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetAllUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
                return new List<Tbl_Group>();
            }
        }

        public bool AddUserToGroup(int user_id, int group_id, int acting_user_id)
        {
            try
            {
                if (!_context.Tbl_User_Group.Any(o => o.Group_ID == group_id && o.User_ID == user_id))
                {
                    _context.Tbl_User_Group.Add(new Tbl_User_Group() { Group_ID = group_id, User_ID = user_id, LastUpdateTime = DateTime.Now.ToString("hh:mm"), LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd"), LastUpdateUser_ID = acting_user_id });
                    return _context.SaveChanges() > 0 ? true : false;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public bool RemoveUserFromGroup(int user_id, int group_id, int acting_user_id)
        {
            try
            {

                Tbl_User_Group entity = _context.Tbl_User_Group.Where(x => x.Group_ID == group_id && x.User_ID == user_id).FirstOrDefault();
                _context.Tbl_User_Group.Remove(entity);
                return _context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddUserToAllGroups(int user_id, int acting_user_id)
        {
            try
            {
                foreach (Tbl_Group group in _context.Tbl_Group)
                {
                    if (!_context.Tbl_User_Group.Any(o => o.Group_ID == group.Group_ID && o.User_ID == user_id))
                        _context.Tbl_User_Group.Add(new Tbl_User_Group() { Group_ID = group.Group_ID, User_ID = user_id, LastUpdateTime = DateTime.Now.ToString("hh:mm"), LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd"), LastUpdateUser_ID = acting_user_id });
                }
                return _context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool RemoveUserFromAllGroups(int user_id, int acting_user_id)
        {
            try
            {

                var entity = _context.Tbl_User_Group.Where(x => x.User_ID == user_id);
                foreach (Tbl_User_Group group in entity)
                {
                    _context.Tbl_User_Group.Remove(group);
                }
                return _context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
