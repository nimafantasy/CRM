using Core.Data;
using Core.Domain.Common;
using Core.Domain.Departments;
using Core.Domain.Logs;
using Core.Domain.Users;
using Core.Infrastructure;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        CRMEntities _context;
        //private readonly IUser _user;
        //CRMContext _context;
        LogService _logService;
        //private readonly IRepository<User> _userRepository;
        //private readonly IRepository<Department> _departmentRepository;
        Active _comm;

        public UserService()
        {
            //db = new CRMEntities();

            //_userRepository = userRepository;
            //_departmentRepository = departmentRepository;
            _comm = new Active();
            //_context = new CRMContext();
            _logService = new LogService();
            _context = new CRMEntities();
        }

        public bool AddNewUser(Tbl_User User, int departmentId, int activeId)
        {
            try
            {

                if (_context.Tbl_User.Any(x => x.User_ID == User.User_ID))
                {
                    // user already exists
                    Tbl_User ToBeUpdatedUser = _context.Tbl_User.First(x => x.User_ID == User.User_ID);
                    ToBeUpdatedUser.Name = User.Name;
                    ToBeUpdatedUser.LastName = User.LastName;
                    ToBeUpdatedUser.UserName = User.UserName;
                    ToBeUpdatedUser.Password = User.Password;
                    ToBeUpdatedUser.Section_ID = _context.Tbl_Section.First(u => u.Section_ID == departmentId).Section_ID;
                    ToBeUpdatedUser.Active =activeId;
                    ToBeUpdatedUser.PersonnelID = User.PersonnelID;
                    ToBeUpdatedUser.LastUpdateUser_ID = User.LastUpdateUser_ID;
                    ToBeUpdatedUser.LastUpdateTime = User.LastUpdateTime;
                    ToBeUpdatedUser.LastUpdateDate = User.LastUpdateDate;
                    
                }
                else
                {
                    if (_context.Tbl_User.Any(x => x.UserName == User.UserName))
                        return false;
                    Tbl_User ToBeInsertedUser = new Tbl_User();
                    ToBeInsertedUser.Name = User.Name;
                    ToBeInsertedUser.LastName = User.LastName;
                    ToBeInsertedUser.UserName = User.UserName;
                    ToBeInsertedUser.Password = User.Password;
                    ToBeInsertedUser.Section_ID = _context.Tbl_Section.First(u => u.Section_ID == departmentId).Section_ID;
                    ToBeInsertedUser.Active = activeId;
                    ToBeInsertedUser.PersonnelID = User.PersonnelID;
                    ToBeInsertedUser.LastUpdateUser_ID = User.LastUpdateUser_ID;
                    ToBeInsertedUser.LastUpdateTime = User.LastUpdateTime;
                    ToBeInsertedUser.LastUpdateDate = User.LastUpdateDate;
                    _context.Tbl_User.Add(ToBeInsertedUser);
                }
                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool RemoveUser(Tbl_User User)
        {
            //try
            //{

            //    //check if user exists
            //    var res = from t in db.Tbl_User
            //              where t.User_ID == User.User_ID
            //              select t;

            //    if (res.Count() > 0)
            //    {
            //        // user exists
            //        db.Tbl_User.Remove(User);
            //        db.SaveChanges();
            //        return true;
            //    }
            //    else
            //    {
            //        // no such user
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            return true;
        }

        public bool EditUser(User User)
        {
            //try
            //{

            //    //check if user already exists
            //    var res = from t in db.Tbl_User
            //              where t.User_ID == User.User_ID
            //              select t;

            //    if (res.Count() > 0)
            //    {
            //        // user already exists
            //        return false;
            //    }
            //    else
            //    {
            //        db.Tbl_User.Remove(res.FirstOrDefault());
            //        db.Tbl_User.Add(User);
            //        db.SaveChanges();
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            return true;
        }

        public bool AssignRoleToUser()
        {
            return true;
        }

        public List<Tbl_User> GetAllUsers()
        {
            try
            {
                return _context.Tbl_User.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetAllUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_User>();
            }
        }

        public bool UsernameExits(string usrnm)
        {
            try
            {
                return _context.Tbl_User.Any(x => x.UserName == usrnm);
            }
            catch(Exception ex)
            {
                return true;
            }
        }

        public List<Tbl_User> SearchUsers(string FirstName, string LastName, string PersonelId, string Username, int active, int section)
        {
            try
            {
                return _context.Tbl_User.Where(x => x.Name.Contains(FirstName) || x.LastName.Contains(LastName) || x.PersonnelID.Contains(PersonelId) || x.UserName.Contains(Username) || x.Active.Equals(active) || x.Section_ID.Equals(section)).ToList();
            }
            catch (Exception ex)
            {
                _logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_User>();
            }
        }

        public Tbl_User GetUserById(int id)
        {
            try
            {
                return _context.Tbl_User.First(x => x.User_ID == id);
            }
            catch (Exception ex)
            {
                _logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetUserById", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new Tbl_User();
            }
            //return new User();
        }

        public List<Tbl_Group> GetUserGroups(int id)
        {
            try
            {
                var res = from f in _context.Tbl_Group
                          join ff in _context.Tbl_User_Group
                          on f.Group_ID equals ff.Group_ID
                          where ff.User_ID == id
                          select f;

                return res.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "GetUserById", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_Group>();
            }
            //return new User();
        }
    }
}
