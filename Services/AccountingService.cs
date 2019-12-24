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
    public class AccountingService
    {
        CRMEntities _context;
        //private readonly IUser _user;
        //CRMContext _context;
        LogService _logService;
        //private readonly IRepository<User> _userRepository;
        //private readonly IRepository<Department> _departmentRepository;
        Active _comm;

        public AccountingService()
        {
            //db = new CRMEntities();

            //_userRepository = userRepository;
            //_departmentRepository = departmentRepository;
            _comm = new Active();
            //_context = new CRMContext();
            _logService = new LogService();
            _context = new CRMEntities();
        }

        public List<Tbl_PreInvoiceType> GetAllPreInvoiceTypes()
        {
            try
            {
                return _context.Tbl_PreInvoiceType.ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_PreInvoiceType>();
            }
        }

        public List<Tbl_PreInvoice> GetAllPreInvoice()
        {
            try
            {
                return _context.Tbl_PreInvoice.ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_PreInvoice>();
            }
        }

        public Tbl_PreInvoiceType GetPreInvoiceTypeById(int id)
        {
            try
            {
                return _context.Tbl_PreInvoiceType.First(x => x.PreInvoiceType_ID == id);
            }
            catch (Exception ex)
            {

                return new Tbl_PreInvoiceType();
            }
        }

        public bool AddNewPreInvoice(Tbl_PreInvoice invoice)
        {
            try
            {

                if (_context.Tbl_PreInvoice.Any(x => x.PreInvoice_ID == invoice.PreInvoice_ID))
                {
                    // user already exists
                    Tbl_PreInvoice ToBeUpdatedUser = _context.Tbl_PreInvoice.First(x => x.PreInvoice_ID == invoice.PreInvoice_ID);
                    ToBeUpdatedUser.Description = invoice.Description;
                    ToBeUpdatedUser.IssueDate = invoice.IssueDate;
                    ToBeUpdatedUser.PreInvoiceNumber = invoice.PreInvoiceNumber;
                    ToBeUpdatedUser.PreInvoiceType_ID = invoice.PreInvoiceType_ID;
                    if (invoice.PreInvoiceUpload!=null)
                        ToBeUpdatedUser.PreInvoiceUpload = invoice.PreInvoiceUpload;
                    ToBeUpdatedUser.PreInvoice_ID = invoice.PreInvoice_ID;
                    ToBeUpdatedUser.SendingStatus = invoice.SendingStatus;
                    ToBeUpdatedUser.ConfirmationStatus = invoice.ConfirmationStatus;
                    ToBeUpdatedUser.Customer_ID = invoice.Customer_ID;
                    ToBeUpdatedUser.LastUpdateUser_ID = invoice.LastUpdateUser_ID;
                    ToBeUpdatedUser.LastUpdateTime = invoice.LastUpdateTime;
                    ToBeUpdatedUser.LastUpdateDate = invoice.LastUpdateDate;

                }
                else
                {
                    if (_context.Tbl_PreInvoice.Any(x => x.PreInvoice_ID == invoice.PreInvoice_ID))
                        return false;
                    Tbl_PreInvoice ToBeInsertedUser = new Tbl_PreInvoice();
                    ToBeInsertedUser.Description = invoice.Description;
                    ToBeInsertedUser.IssueDate = invoice.IssueDate;
                    ToBeInsertedUser.PreInvoiceNumber = invoice.PreInvoiceNumber;
                    ToBeInsertedUser.PreInvoiceType_ID = invoice.PreInvoiceType_ID;
                    if (invoice.PreInvoiceUpload!=null)
                    ToBeInsertedUser.PreInvoiceUpload = invoice.PreInvoiceUpload;
                    ToBeInsertedUser.PreInvoice_ID = invoice.PreInvoice_ID;
                    ToBeInsertedUser.SendingStatus = invoice.SendingStatus;
                    ToBeInsertedUser.ConfirmationStatus = invoice.ConfirmationStatus;
                    ToBeInsertedUser.Customer_ID = invoice.Customer_ID;
                    ToBeInsertedUser.LastUpdateUser_ID = invoice.LastUpdateUser_ID;
                    ToBeInsertedUser.LastUpdateTime = invoice.LastUpdateTime;
                    ToBeInsertedUser.LastUpdateDate = invoice.LastUpdateDate;
                    _context.Tbl_PreInvoice.Add(ToBeInsertedUser);
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

        public List<Tbl_Payment> GetAllPayment()
        {
            try
            {
                return _context.Tbl_Payment.ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_Payment>();
            }
        }

        public bool AddNewPayment(Tbl_Payment payment)
        {
            try
            {

                if (_context.Tbl_Payment.Any(x => x.Payment_ID == payment.Payment_ID))
                {
                    // user already exists
                    Tbl_Payment ToBeUpdatedUser = _context.Tbl_Payment.First(x => x.Payment_ID == payment.Payment_ID);
                    ToBeUpdatedUser.Description = payment.Description;
                    ToBeUpdatedUser.Customer_ID = payment.Customer_ID;
                    ToBeUpdatedUser.PaymentAmount = payment.PaymentAmount;
                    ToBeUpdatedUser.PaymentDate = payment.PaymentDate;
                    ToBeUpdatedUser.Payment_ID = payment.Payment_ID;
                    ToBeUpdatedUser.PreInvoiceNumber = payment.PreInvoiceNumber;
                    ToBeUpdatedUser.LastUpdateUser_ID = payment.LastUpdateUser_ID;
                    ToBeUpdatedUser.LastUpdateTime = payment.LastUpdateTime;
                    ToBeUpdatedUser.LastUpdateDate = payment.LastUpdateDate;

                }
                else
                {
                    if (_context.Tbl_Payment.Any(x => x.Payment_ID == payment.Payment_ID))
                        return false;
                    Tbl_Payment ToBeInsertedUser = new Tbl_Payment();
                    ToBeInsertedUser.Description = payment.Description;
                    ToBeInsertedUser.Customer_ID = payment.Customer_ID;
                    ToBeInsertedUser.PaymentAmount = payment.PaymentAmount;
                    ToBeInsertedUser.PaymentDate = payment.PaymentDate;
                    ToBeInsertedUser.Payment_ID = payment.Payment_ID;
                    ToBeInsertedUser.PreInvoiceNumber = payment.PreInvoiceNumber;
                    ToBeInsertedUser.LastUpdateUser_ID = payment.LastUpdateUser_ID;
                    ToBeInsertedUser.LastUpdateTime = payment.LastUpdateTime;
                    ToBeInsertedUser.LastUpdateDate = payment.LastUpdateDate;
                    _context.Tbl_Payment.Add(ToBeInsertedUser);
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

        public List<Tbl_PreInvoice> SearchPreInvoice(string invoicenum, string issuedate, int invoicetype, int sendingstatus, int confstatus, string description)
        {
            try
            {
                return _context.Tbl_PreInvoice.Where(x => x.PreInvoiceNumber.Contains(invoicenum) || x.IssueDate.Equals(issuedate) || x.PreInvoiceType_ID.Equals(invoicetype) || x.SendingStatus.Equals(sendingstatus) || x.Description.Contains(description)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_PreInvoice>();
            }
        }

        public List<Tbl_Payment> SearchPayment(int customerid , string preinvoicenum, string paydate, int payamount, string desc)
        {
            try
            {
                return _context.Tbl_Payment.Where(x => x.Customer_ID.Equals(customerid) || x.PreInvoiceNumber.Contains(preinvoicenum) || x.PaymentDate.Equals(paydate) || x.PaymentAmount.Equals(payamount) || x.Description.Contains(desc)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_Payment>();
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
