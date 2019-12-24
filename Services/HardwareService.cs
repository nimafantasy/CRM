using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HardwareService
    {
        CRMEntities _context;
        CustomerService _customerService;
        public HardwareService()
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

        public List<Tbl_Hardware> SearchHardware(string ConnectorName, int Hardware_ID, string RequestDate, string Description, int status)
        {
            try
            {
                return _context.Tbl_Hardware.Where(x => x.CustomerConnector.Contains(ConnectorName) || x.Hardware_ID.Equals(Hardware_ID) || x.RequestDate.Equals(RequestDate) || x.Description.Contains(Description)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_Hardware>();
            }
        }


        public List<Tbl_HardwareType> SearchHardwareType(string desc, string name, int price, int id, string wperiod)
        {
            try
            {
                return _context.Tbl_HardwareType.Where(x => x.Description.Contains(desc) || x.HardwareName.Contains(name) || x.HardwarePrice.Equals(price) || x.Products_ID.Equals(id) || x.WarrantyPeriod.Contains(wperiod)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_HardwareType>();
            }
        }

        public List<Tbl_DeviceFailure> SearchDeviceFailure(string connectorname, string reqdate, int problemtyp, int troubleshoot, string serial, string desc)
        {
            try
            {
                return _context.Tbl_DeviceFailure.Where(x => x.Description.Contains(desc) || x.RequestDate.Equals(reqdate) || x.CustomerConnector.Contains(connectorname) || x.ProblemType.Equals(problemtyp) || x.Troubleshooting.Equals(troubleshoot) || x.DeliveryDeviceSerialNumber.Contains(serial)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_DeviceFailure>();
            }
        }

        public List<Tbl_SendToRepair> SearchSendToRepair(string connectorname, string probleminfo, string postdate, string returndate, string serial, string desc)
        {
            try
            {
                return _context.Tbl_SendToRepair.Where(x => x.Description.Contains(desc) || x.CustomerConnector.Contains(connectorname) || x.ProblemInfo.Contains(probleminfo) || x.PostageDate.Contains(postdate) || x.ReturnDate.Contains(returndate) || x.DeviceSerialNumber.Contains(serial)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_SendToRepair>();
            }
        }

        public List<Tbl_ReturnRepairedDevice> SearchReturnRepairedDevice(string connectorname, string repairs, string returndate, string serial, string desc)
        {
            try
            {
                return _context.Tbl_ReturnRepairedDevice.Where(x => x.Description.Contains(desc) || x.CustomerConnector.Contains(connectorname) || x.Repairs.Contains(repairs) ||  x.ReturnDate.Contains(returndate) || x.DeviceSerialNumber.Contains(serial)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_ReturnRepairedDevice>();
            }
        }

        public List<Tbl_Hardware> GetAllHardware()
        {
            try
            {

                return _context.Tbl_Hardware.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_Hardware>();
            }
        }


        public List<Tbl_DeviceFailure> GetAllDeviceFailure()
        {
            try
            {

                return _context.Tbl_DeviceFailure.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_DeviceFailure>();
            }
        }

        public List<Tbl_SendToRepair> GetAllSendToRepair()
        {
            try
            {

                return _context.Tbl_SendToRepair.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_SendToRepair>();
            }
        }

        public List<Tbl_ReturnRepairedDevice> GetAllReturnRepairedDevice()
        {
            try
            {

                return _context.Tbl_ReturnRepairedDevice.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_ReturnRepairedDevice>();
            }
        }

        public List<Tbl_Hardware> GetHardwareOfCusomer(int id)
        {
            try
            {

                return _context.Tbl_Hardware.Where(y =>  y.Customer_ID ==id).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_Hardware>();
            }
        }

        public List<Tbl_DeviceFailure> GetDeviceFailureOfCusomer(int id)
        {
            try
            {

                return _context.Tbl_DeviceFailure.Where(y => y.Customer_ID == id).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_DeviceFailure>();
            }
        }

        public Tbl_HardwareType GetHardwareById(int id)
        {
            try
            {
                return _context.Tbl_HardwareType.First(o => o.Hardware_ID == id);
            }
            catch (Exception ex)
            {
                return new Tbl_HardwareType();
            }

        }

        public List<Tbl_HardwareType> GetAllHardwareTypes()
        {
            try
            {
                return _context.Tbl_HardwareType.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_HardwareType>();
            }

        }

        public Tbl_HardwareType GetHardwareTypeById(int id)
        {
            try
            {
                return _context.Tbl_HardwareType.Where(c => c.Hardware_ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Tbl_HardwareType();
            }

        }

        public bool AddNewHardware(Tbl_Hardware tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_Hardware ToBeUpdatedHardware = _context.Tbl_Hardware.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedHardware.Amount = tu.Amount;
                    ToBeUpdatedHardware.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedHardware.Description = tu.Description;
                    ToBeUpdatedHardware.Hardware_ID = tu.Hardware_ID;
                    ToBeUpdatedHardware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedHardware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedHardware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeUpdatedHardware.RequestDate = tu.RequestDate;
                    ToBeUpdatedHardware.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedHardware.Status = tu.Status;
                    ToBeUpdatedHardware.DeliveryDate = tu.DeliveryDate;
                    ToBeUpdatedHardware.DeliveryDescription = tu.DeliveryDescription;
                    ToBeUpdatedHardware.DeliveryDeviceSerialNumber = tu.DeliveryDeviceSerialNumber;
                }
                else
                {
                    Tbl_Hardware ToBeInsertedHardware = new Tbl_Hardware();
                    ToBeInsertedHardware.Amount = tu.Amount;
                    ToBeInsertedHardware.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedHardware.Description = tu.Description;
                    ToBeInsertedHardware.Hardware_ID = tu.Hardware_ID;
                    ToBeInsertedHardware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedHardware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedHardware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeInsertedHardware.RequestDate = tu.RequestDate;
                    ToBeInsertedHardware.Customer_ID = tu.Customer_ID;
                    ToBeInsertedHardware.Status = 0;
                    ToBeInsertedHardware.DeliveryDate = tu.DeliveryDate;
                    ToBeInsertedHardware.DeliveryDescription = tu.DeliveryDescription;
                    ToBeInsertedHardware.DeliveryDeviceSerialNumber = tu.DeliveryDeviceSerialNumber;
                    _context.Tbl_Hardware.Add(ToBeInsertedHardware);
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

        public bool AddNewDeviceFailure(Tbl_DeviceFailure tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_DeviceFailure ToBeUpdatedDeviceFailure = _context.Tbl_DeviceFailure.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedDeviceFailure.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedDeviceFailure.Description = tu.Description;
                    ToBeUpdatedDeviceFailure.ProblemType = tu.ProblemType;
                    ToBeUpdatedDeviceFailure.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedDeviceFailure.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedDeviceFailure.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeUpdatedDeviceFailure.RequestDate = tu.RequestDate;
                    ToBeUpdatedDeviceFailure.Troubleshooting = tu.Troubleshooting;
                    ToBeUpdatedDeviceFailure.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedDeviceFailure.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeUpdatedDeviceFailure.DeliveryDate = tu.DeliveryDate;
                    ToBeUpdatedDeviceFailure.DeliveryDeviceSerialNumber = tu.DeliveryDeviceSerialNumber;
                    
                }
                else
                {
                    Tbl_DeviceFailure ToBeInsertedDeviceFailure = new Tbl_DeviceFailure();
                    ToBeInsertedDeviceFailure.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedDeviceFailure.Description = tu.Description;
                    ToBeInsertedDeviceFailure.ProblemType = tu.ProblemType;
                    ToBeInsertedDeviceFailure.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedDeviceFailure.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedDeviceFailure.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeInsertedDeviceFailure.RequestDate = tu.RequestDate;
                    ToBeInsertedDeviceFailure.Customer_ID = tu.Customer_ID;
                    ToBeInsertedDeviceFailure.Troubleshooting = tu.Troubleshooting;
                    ToBeInsertedDeviceFailure.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeInsertedDeviceFailure.DeliveryDate = tu.DeliveryDate;
                    ToBeInsertedDeviceFailure.DeliveryDeviceSerialNumber = tu.DeliveryDeviceSerialNumber;
                    _context.Tbl_DeviceFailure.Add(ToBeInsertedDeviceFailure);

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

        public bool AddNewSendToRepair(Tbl_SendToRepair tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_SendToRepair ToBeUpdatedSendToRepair = _context.Tbl_SendToRepair.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedSendToRepair.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedSendToRepair.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedSendToRepair.DeviceSerialNumber = tu.DeviceSerialNumber;
                    ToBeUpdatedSendToRepair.DeliveryDate = tu.DeliveryDate;
                    ToBeUpdatedSendToRepair.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeUpdatedSendToRepair.ProblemInfo = tu.ProblemInfo;
                    ToBeUpdatedSendToRepair.PostageDate = tu.PostageDate;
                    ToBeUpdatedSendToRepair.ReturnDate = tu.ReturnDate;
                    ToBeUpdatedSendToRepair.Description = tu.Description;
                    ToBeUpdatedSendToRepair.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedSendToRepair.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedSendToRepair.LastUpdateUser_ID = tu.LastUpdateUser_ID;

                }
                else
                {
                    Tbl_SendToRepair ToBeInsertedSendToRepair = new Tbl_SendToRepair();
                    ToBeInsertedSendToRepair.Customer_ID = tu.Customer_ID;
                    ToBeInsertedSendToRepair.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedSendToRepair.DeviceSerialNumber = tu.DeviceSerialNumber;
                    ToBeInsertedSendToRepair.DeliveryDate = tu.DeliveryDate;
                    ToBeInsertedSendToRepair.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeInsertedSendToRepair.ProblemInfo = tu.ProblemInfo;
                    ToBeInsertedSendToRepair.PostageDate = tu.PostageDate;
                    ToBeInsertedSendToRepair.ReturnDate = tu.ReturnDate;
                    ToBeInsertedSendToRepair.Description = tu.Description;
                    ToBeInsertedSendToRepair.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedSendToRepair.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedSendToRepair.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    _context.Tbl_SendToRepair.Add(ToBeInsertedSendToRepair);

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

        public bool AddNewReturnRepairedDevice(Tbl_ReturnRepairedDevice tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_ReturnRepairedDevice ToBeUpdatedSendToRepair = _context.Tbl_ReturnRepairedDevice.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedSendToRepair.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedSendToRepair.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedSendToRepair.DeviceSerialNumber = tu.DeviceSerialNumber;
                    ToBeUpdatedSendToRepair.DeliveryDate = tu.DeliveryDate;
                    ToBeUpdatedSendToRepair.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeUpdatedSendToRepair.Repairs = tu.Repairs;
                    ToBeUpdatedSendToRepair.ReturnDate = tu.ReturnDate;
                    ToBeUpdatedSendToRepair.Description = tu.Description;
                    ToBeUpdatedSendToRepair.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedSendToRepair.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedSendToRepair.LastUpdateUser_ID = tu.LastUpdateUser_ID;

                }
                else
                {
                    Tbl_ReturnRepairedDevice ToBeInsertedSendToRepair = new Tbl_ReturnRepairedDevice();
                    ToBeInsertedSendToRepair.Customer_ID = tu.Customer_ID;
                    ToBeInsertedSendToRepair.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedSendToRepair.DeviceSerialNumber = tu.DeviceSerialNumber;
                    ToBeInsertedSendToRepair.DeliveryDate = tu.DeliveryDate;
                    ToBeInsertedSendToRepair.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeInsertedSendToRepair.Repairs = tu.Repairs;
                    ToBeInsertedSendToRepair.ReturnDate = tu.ReturnDate;
                    ToBeInsertedSendToRepair.Description = tu.Description;
                    ToBeInsertedSendToRepair.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedSendToRepair.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedSendToRepair.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    _context.Tbl_ReturnRepairedDevice.Add(ToBeInsertedSendToRepair);

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

        public bool AddNewHardwareType(Tbl_HardwareType tu)
        {
            try
            {

                if (tu.Hardware_ID != 0)
                {
                    // user already exists
                    Tbl_HardwareType ToBeUpdatedHardware = _context.Tbl_HardwareType.First(x => x.Hardware_ID == tu.Hardware_ID);
                    ToBeUpdatedHardware.Description = tu.Description;
                    ToBeUpdatedHardware.HardwareName = tu.HardwareName;
                    ToBeUpdatedHardware.HardwarePrice = tu.HardwarePrice;
                    ToBeUpdatedHardware.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeUpdatedHardware.Products_ID = tu.Products_ID;
                    ToBeUpdatedHardware.Hardware_ID = tu.Hardware_ID;
                    ToBeUpdatedHardware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedHardware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedHardware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    
                }
                else
                {
                    Tbl_HardwareType ToBeInsertedHardware = new Tbl_HardwareType();
                    ToBeInsertedHardware.Description = tu.Description;
                    ToBeInsertedHardware.HardwareName = tu.HardwareName;
                    ToBeInsertedHardware.HardwarePrice = tu.HardwarePrice;
                    ToBeInsertedHardware.Hardware_ID = tu.Hardware_ID;
                    ToBeInsertedHardware.Products_ID = tu.Products_ID;
                    ToBeInsertedHardware.WarrantyPeriod = tu.WarrantyPeriod;
                    ToBeInsertedHardware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedHardware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedHardware.LastUpdateUser_ID = tu.LastUpdateUser_ID;

                    _context.Tbl_HardwareType.Add(ToBeInsertedHardware);
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
