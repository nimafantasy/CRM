using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GroupActivityService
    {
        CRMEntities _context;
        public GroupActivityService()
        {
            _context = new CRMEntities();
        }

        public List<Tbl_SubActivity> GetGroupSubActivity(int group_id)
        {
            try
            {
                var partialResult = from ug in _context.Tbl_SubActivity
                                    join g in _context.Tbl_Group_Activity
                                    on ug.SubAct_ID equals g.SubAct_ID
                                    where g.Group_ID == group_id
                                    select ug;


                return partialResult.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetAllUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
                return new List<Tbl_SubActivity>();
            }
        }

        public bool AddSubActivityToGroup(int subactivity_id, int group_id, int acting_user_id)
        {
            try
            {
                if (!_context.Tbl_Group_Activity.Any(o => o.Group_ID == group_id && o.SubAct_ID == subactivity_id))
                {
                    _context.Tbl_Group_Activity.Add(new Tbl_Group_Activity() { Active = 1, SubAct_ID = subactivity_id, Group_ID = group_id, LastUpdateTime = DateTime.Now.ToString("hh:mm"), LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd"), LastUpdateUser_ID = acting_user_id });
                    return _context.SaveChanges() > 0 ? true : false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool RemoveSubActivityFromGroup(int subactivity_id, int group_id, int acting_user_id)
        {
            try
            {

                Tbl_Group_Activity entity = _context.Tbl_Group_Activity.Where(x => x.Group_ID == group_id && x.SubAct_ID == subactivity_id).FirstOrDefault();
                _context.Tbl_Group_Activity.Remove(entity);
                return _context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddSubActivityToAllGroups(int group_id, int acting_user_id)
        {
            try
            {
                foreach (Tbl_SubActivity sa in _context.Tbl_SubActivity)
                {
                    if (!_context.Tbl_Group_Activity.Any(o => o.Group_ID == group_id && o.SubAct_ID == sa.SubAct_ID))
                        _context.Tbl_Group_Activity.Add(new Tbl_Group_Activity() { Group_ID = group_id, SubAct_ID = sa.SubAct_ID, LastUpdateTime = DateTime.Now.ToString("hh:mm"), LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd"), LastUpdateUser_ID = acting_user_id });
                }
                return _context.SaveChanges() > 0 ? true: false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool RemoveSubActivityFromAllGroups(int group_id, int acting_user_id)
        {
            try
            {

                var entity = _context.Tbl_Group_Activity.Where(x => x.Group_ID == group_id);
                foreach (Tbl_Group_Activity subac in entity)
                {
                    _context.Tbl_Group_Activity.Remove(subac);
                }
                return _context.SaveChanges() > 0 ? true: false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
