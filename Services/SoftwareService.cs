using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SoftwareService
    {
        CRMEntities _context;
        CustomerService _customerService;
        public SoftwareService()
        {
            _customerService = new CustomerService();
            _context = new CRMEntities();
        }

        public List<Tbl_Customer> SearchCustomers(string CustomerName,string MobileNo,  string SubscriptionCode, string TelNo)
        {
            try
            {
                
                return _context.Tbl_Customer.Where(x => x.CustomerName.Contains(CustomerName) || x.MobileNo.Contains(MobileNo) || x.SubscriptionCode.Contains(SubscriptionCode) || x.TelNo.Contains(TelNo)).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_Customer>();
            }
        }

        public List<Tbl_Software> SearchSoftware(string ConnectorName, int Software_ID, string RequestDate, string Description)
        {
            try
            {
                return _context.Tbl_Software.Where(x => x.CustomerConnector.Contains(ConnectorName) || x.Software_ID.Equals(Software_ID) || x.RequestDate.Equals(RequestDate) || x.Description.Contains(Description)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_Software>();
            }
        }

        public List<Tbl_SoftwareType> SearchSoftwareType(string desc, string name, int price, int id)
        {
            try
            {
                return _context.Tbl_SoftwareType.Where(x => x.Description.Contains(desc) || x.SoftwareName.Contains(name) || x.SoftwarePrice.Equals(price) || x.Products_ID.Equals(id)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_SoftwareType>();
            }
        }

        public List<Tbl_ChangeSoftware> SearchSoftwareChange(int amount, string customerConnector, int customerId, string requestDate, string deliveryDate, string description, string requiredChanges)
        {
            try
            {
                return _context.Tbl_ChangeSoftware.Where(x => x.CustomerConnector.Contains(customerConnector) || x.Customer_ID.Equals(customerId) || x.RequestDate.Contains(requestDate) || x.DeliveryDate.Contains(deliveryDate) || x.Description.Contains(description) || x.RequiredChanges.Contains(requiredChanges)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_ChangeSoftware>();
            }
        }

        public List<Tbl_Software> GetAllSoftware()
        {
            try
            {

                return _context.Tbl_Software.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_Software>();
            }
        }


       
        public List<Tbl_Software> GetSoftwareOfCusomer(int id)
        {
            try
            {

                return _context.Tbl_Software.Where(y =>  y.Customer_ID ==id).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_Software>();
            }
        }

        public Tbl_SoftwareType GetSoftwareById(int id)
        {
            try
            {
                return _context.Tbl_SoftwareType.Where(c => c.Software_ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Tbl_SoftwareType();
            }

        }

        public List<Tbl_ChangeSoftware> GetAllSoftwareChange()
        {
            try
            {

                return _context.Tbl_ChangeSoftware.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_ChangeSoftware>();
            }
        }

        public List<Tbl_ChangeSoftware> GetSoftwareChangeOfCusomer(int id)
        {
            try
            {

                return _context.Tbl_ChangeSoftware.Where(y => y.Customer_ID == id).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_ChangeSoftware>();
            }
        }


        public List<Tbl_SoftwareType> GetAllSoftwareTypes()
        {
            try
            {
                return _context.Tbl_SoftwareType.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_SoftwareType>();
            }

        }

        public bool AddNewSoftware(Tbl_Software tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_Software ToBeUpdatedSoftware = _context.Tbl_Software.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedSoftware.Amount = tu.Amount;
                    ToBeUpdatedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedSoftware.Description = tu.Description;
                    ToBeUpdatedSoftware.Software_ID = tu.Software_ID;
                    ToBeUpdatedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeUpdatedSoftware.RequestDate = tu.RequestDate;
                    ToBeUpdatedSoftware.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedSoftware.Status = tu.Status;
                    ToBeUpdatedSoftware.DeliveryDate = tu.DeliveryDate;
                    ToBeUpdatedSoftware.DeliveryDescription = tu.DeliveryDescription;
                }
                else
                {
                    Tbl_Software ToBeInsertedSoftware = new Tbl_Software();
                    ToBeInsertedSoftware.Amount = tu.Amount;
                    ToBeInsertedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedSoftware.Description = tu.Description;
                    ToBeInsertedSoftware.Software_ID = tu.Software_ID;
                    ToBeInsertedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeInsertedSoftware.RequestDate = tu.RequestDate;
                    ToBeInsertedSoftware.Customer_ID = tu.Customer_ID;
                    ToBeInsertedSoftware.Status = tu.Status;
                    ToBeInsertedSoftware.DeliveryDate = tu.DeliveryDate;
                    ToBeInsertedSoftware.DeliveryDescription = tu.DeliveryDescription;
                    _context.Tbl_Software.Add(ToBeInsertedSoftware);
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


        public bool AddNewSoftwareChange(Tbl_ChangeSoftware tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_ChangeSoftware ToBeUpdatedSoftware = _context.Tbl_ChangeSoftware.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedSoftware.Amount = tu.Amount;
                    ToBeUpdatedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedSoftware.Description = tu.Description;
                    ToBeUpdatedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeUpdatedSoftware.RequestDate = tu.RequestDate;
                    ToBeUpdatedSoftware.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedSoftware.DeliveryDate = tu.DeliveryDate;
                    ToBeUpdatedSoftware.RequiredChanges = tu.RequiredChanges;
                }
                else
                {
                    Tbl_ChangeSoftware ToBeInsertedSoftware = new Tbl_ChangeSoftware();
                    ToBeInsertedSoftware.Amount = tu.Amount;
                    ToBeInsertedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedSoftware.Description = tu.Description;
                    ToBeInsertedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeInsertedSoftware.RequestDate = tu.RequestDate;
                    ToBeInsertedSoftware.Customer_ID = tu.Customer_ID;
                    ToBeInsertedSoftware.DeliveryDate = tu.DeliveryDate;
                    ToBeInsertedSoftware.RequiredChanges = tu.RequiredChanges;
                    _context.Tbl_ChangeSoftware.Add(ToBeInsertedSoftware);
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

        public bool AddNewSoftwareType(Tbl_SoftwareType tu)
        {
            try
            {

                if (tu.Software_ID != 0)
                {
                    // user already exists
                    Tbl_SoftwareType ToBeUpdatedHardware = _context.Tbl_SoftwareType.First(x => x.Software_ID == tu.Software_ID);
                    ToBeUpdatedHardware.Description = tu.Description;
                    ToBeUpdatedHardware.SoftwareName = tu.SoftwareName;
                    ToBeUpdatedHardware.SoftwarePrice = tu.SoftwarePrice;
                    ToBeUpdatedHardware.Software_ID = tu.Software_ID;
                    ToBeUpdatedHardware.Products_ID = tu.Products_ID;
                    ToBeUpdatedHardware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedHardware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedHardware.LastUpdateUser_ID = tu.LastUpdateUser_ID;

                }
                else
                {
                    Tbl_SoftwareType ToBeInsertedHardware = new Tbl_SoftwareType();
                    ToBeInsertedHardware.Description = tu.Description;
                    ToBeInsertedHardware.SoftwareName = tu.SoftwareName;
                    ToBeInsertedHardware.SoftwarePrice = tu.SoftwarePrice;
                    ToBeInsertedHardware.Software_ID = tu.Software_ID;
                    ToBeInsertedHardware.Products_ID = tu.Products_ID;
                    ToBeInsertedHardware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedHardware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedHardware.LastUpdateUser_ID = tu.LastUpdateUser_ID;

                    _context.Tbl_SoftwareType.Add(ToBeInsertedHardware);
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
    }
}
