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
    public class SupportService
    {
        CRMEntities _context;

        public SupportService()
        {
            _context = new CRMEntities();
        }

        public bool AddNewRequestType(Tbl_RequestType reqtyp)
        {
            try
            {
                //check if user already exists
                var res = from t in _context.Tbl_RequestType
                          where t.RequestType_ID == reqtyp.RequestType_ID
                          select t;

                if (res.Count() == 0)
                {
                    _context.Tbl_RequestType.Add(reqtyp);
                }
                else
                {
                    Tbl_RequestType prod = _context.Tbl_RequestType.First(x => x.RequestType_ID == reqtyp.RequestType_ID);
                    prod.RequestTypeName = reqtyp.RequestTypeName;
                    prod.Description = reqtyp.Description;
                    prod.LastUpdateUser_ID = reqtyp.LastUpdateUser_ID;
                    prod.LastUpdateDate = reqtyp.LastUpdateDate;
                    prod.LastUpdateTime = reqtyp.LastUpdateTime;
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

        public List<Tbl_RequestType> SearchRequestType(string name, string desc)
        {
            try
            {
                return _context.Tbl_RequestType.Where(x => x.RequestTypeName.Contains(name) || x.Description.Contains(desc)).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_RequestType>();
            }
        }

        public List<Tbl_OtherRequest> SearchOtherRequest(int customerid, string connector, int typeid, string desc)
        {
            try
            {
                return _context.Tbl_OtherRequest.Where(x => x.Customer_ID.Equals(customerid) || x.CustomerConnector.Contains(connector) || x.RequestType_ID.Equals(typeid) || x.Description.Contains(desc)).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_OtherRequest>();
            }
        }

        public List<Tbl_RequestType> GetAllRequestTypes()
        {
            try
            {
                return _context.Tbl_RequestType.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_RequestType>();
            }

        }

        public List<Tbl_Installation> GetAllInstallation()
        {
            try
            {
                return _context.Tbl_Installation.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_Installation>();
            }

        }

        public List<Tbl_OtherRequest> GetAllOtherRequest()
        {
            try
            {
                return _context.Tbl_OtherRequest.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_OtherRequest>();
            }

        }

        public Tbl_RequestType GetRequestTypeById(int id)
        {
            try
            {
                return _context.Tbl_RequestType.Where(x => x.RequestType_ID == id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Tbl_RequestType();
            }
        }

        public bool AddNewSupportService(Tbl_SupportServices tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_SupportServices ToBeUpdatedSoftware = _context.Tbl_SupportServices.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedSoftware.Description = tu.Description;
                    ToBeUpdatedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeUpdatedSoftware.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedSoftware.Guidance = tu.Guidance;
                    ToBeUpdatedSoftware.GuidanceDate = tu.GuidanceDate;
                    ToBeUpdatedSoftware.Problem = tu.Problem;
                    
                }
                else
                {
                    Tbl_SupportServices ToBeInsertedSoftware = new Tbl_SupportServices();
                    ToBeInsertedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedSoftware.Description = tu.Description;
                    ToBeInsertedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeInsertedSoftware.Customer_ID = tu.Customer_ID;
                    ToBeInsertedSoftware.Guidance = tu.Guidance;
                    ToBeInsertedSoftware.GuidanceDate = tu.GuidanceDate;
                    ToBeInsertedSoftware.Problem = tu.Problem;
                    _context.Tbl_SupportServices.Add(ToBeInsertedSoftware);
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

        public bool AddNewOtherRequest(Tbl_OtherRequest tu)
        {
            try
            {

                if (tu.Request_ID != 0)
                {
                    // user already exists
                    Tbl_OtherRequest ToBeUpdatedSoftware = _context.Tbl_OtherRequest.First(x => x.Request_ID == tu.Request_ID);
                    ToBeUpdatedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedSoftware.RequestType_ID = tu.RequestType_ID;
                    ToBeUpdatedSoftware.Description = tu.Description;
                    ToBeUpdatedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeUpdatedSoftware.Customer_ID = tu.Customer_ID;

                }
                else
                {
                    Tbl_OtherRequest ToBeInsertedSoftware = new Tbl_OtherRequest();
                    ToBeInsertedSoftware.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedSoftware.Description = tu.Description;
                    ToBeInsertedSoftware.RequestType_ID = tu.RequestType_ID;
                    ToBeInsertedSoftware.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedSoftware.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedSoftware.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    ToBeInsertedSoftware.Customer_ID = tu.Customer_ID;
                    _context.Tbl_OtherRequest.Add(ToBeInsertedSoftware);
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

        public bool AddNewInstallation(Tbl_Installation tu)
        {
            try
            {

                if (tu.Installation_ID != 0)
                {
                    // user already exists
                    Tbl_Installation ToBeUpdatedInstallation = _context.Tbl_Installation.First(x => x.Installation_ID == tu.Installation_ID);
                    ToBeUpdatedInstallation.AdministratorPassword = tu.AdministratorPassword;
                    ToBeUpdatedInstallation.ClientsNumber = tu.ClientsNumber;
                    ToBeUpdatedInstallation.CPU = tu.CPU;
                    ToBeUpdatedInstallation.CustomerConnector = tu.CustomerConnector;
                    ToBeUpdatedInstallation.Customer_ID = tu.Customer_ID;
                    ToBeUpdatedInstallation.DeliveryDate_Customer = tu.DeliveryDate_Customer;
                    ToBeUpdatedInstallation.DeliveryDate_SOHA = tu.DeliveryDate_SOHA;
                    ToBeUpdatedInstallation.Description = tu.Description;
                    ToBeUpdatedInstallation.InstallationLocation = tu.InstallationLocation;
                    ToBeUpdatedInstallation.InstallationProgram = tu.InstallationProgram;
                    ToBeUpdatedInstallation.Installation_ID = tu.Installation_ID;
                    ToBeUpdatedInstallation.IPServer = tu.IPServer;
                    ToBeUpdatedInstallation.PurchasedDevicesNumber = tu.PurchasedDevicesNumber;
                    ToBeUpdatedInstallation.RAM = tu.RAM;
                    ToBeUpdatedInstallation.SendSystemRequirements = tu.SendSystemRequirements;
                    ToBeUpdatedInstallation.SystemType = tu.SystemType;
                    ToBeUpdatedInstallation.LastUpdateDate = tu.LastUpdateDate;
                    ToBeUpdatedInstallation.LastUpdateTime = tu.LastUpdateTime;
                    ToBeUpdatedInstallation.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    

                }
                else
                {
                    Tbl_Installation ToBeInsertedInstallation = new Tbl_Installation();
                    ToBeInsertedInstallation.AdministratorPassword = tu.AdministratorPassword;
                    ToBeInsertedInstallation.ClientsNumber = tu.ClientsNumber;
                    ToBeInsertedInstallation.CPU = tu.CPU;
                    ToBeInsertedInstallation.CustomerConnector = tu.CustomerConnector;
                    ToBeInsertedInstallation.Customer_ID = tu.Customer_ID;
                    ToBeInsertedInstallation.DeliveryDate_Customer = tu.DeliveryDate_Customer;
                    ToBeInsertedInstallation.DeliveryDate_SOHA = tu.DeliveryDate_SOHA;
                    ToBeInsertedInstallation.Description = tu.Description;
                    ToBeInsertedInstallation.InstallationLocation = tu.InstallationLocation;
                    ToBeInsertedInstallation.InstallationProgram = tu.InstallationProgram;
                    ToBeInsertedInstallation.Installation_ID = tu.Installation_ID;
                    ToBeInsertedInstallation.IPServer = tu.IPServer;
                    ToBeInsertedInstallation.PurchasedDevicesNumber = tu.PurchasedDevicesNumber;
                    ToBeInsertedInstallation.RAM = tu.RAM;
                    ToBeInsertedInstallation.SendSystemRequirements = tu.SendSystemRequirements;
                    ToBeInsertedInstallation.SystemType = tu.SystemType;
                    ToBeInsertedInstallation.LastUpdateDate = tu.LastUpdateDate;
                    ToBeInsertedInstallation.LastUpdateTime = tu.LastUpdateTime;
                    ToBeInsertedInstallation.LastUpdateUser_ID = tu.LastUpdateUser_ID;
                    _context.Tbl_Installation.Add(ToBeInsertedInstallation);
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

        public List<Tbl_SupportServices> GetAllSupportService()
        {
            try
            {

                return _context.Tbl_SupportServices.ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_SupportServices>();
            }
        }

        public List<Tbl_RequestType> GetAllOtherRequestTypes()
        {
            try
            {
                return _context.Tbl_RequestType.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_RequestType>();
            }

        }

        public List<Tbl_SupportServices> SearchSupport(string connectorName, string gdate, string problem, string guide, string description)
        {
            try
            {
                return _context.Tbl_SupportServices.Where(x => x.CustomerConnector.Contains(connectorName) || x.GuidanceDate.Equals(gdate) || x.Problem.Contains(problem) || x.Guidance.Contains(guide) || x.Description.Contains(description)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_SupportServices>();
            }
        }

        public List<Tbl_Installation> SearchInstallation(string connectorName, int sndsysreq, int loc, int insprog, int systyp, string sohadate, string custmrdate, int clientnumber, string pdn, string cpu, string ram, string serverip, string adminpass, string desc)
        {
            try
            {
                return _context.Tbl_Installation.Where(x => x.CustomerConnector.Contains(connectorName) || x.SendSystemRequirements.Equals(sndsysreq) || x.InstallationLocation.Equals(loc) || x.InstallationProgram.Equals(insprog) || x.SystemType.Equals(systyp) || x.DeliveryDate_SOHA.Equals(sohadate) || x.DeliveryDate_Customer.Equals(custmrdate) || x.ClientsNumber.Equals(clientnumber) || x.PurchasedDevicesNumber.Contains(pdn) || x.CPU.Contains(cpu) || x.RAM.Contains(ram) || x.IPServer.Contains(serverip) || x.AdministratorPassword.Contains(adminpass) || x.Description.Contains(desc)).ToList();
            }
            catch (Exception ex)
            {

                return new List<Tbl_Installation>();
            }
        }

        public List<Tbl_Customer> SearchCustomers(string CustomerName, string MobileNo, string SubscriptionCode, string TelNo)
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

    }
}
