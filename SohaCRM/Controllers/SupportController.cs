using Data;
using SohaCRM.Models.RequestType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;
using Core.Domain.Common;
using Services;
using SohaCRM.Models.SupportServices;
using SohaCRM.Models.OtherRequest;
using SohaCRM.Models.Installation;

namespace SohaCRM.Controllers
{
    public class SupportController : Controller
    {

        HardwareService _hardwareService;
        ProductService _productService;
        CustomerService _customerService;
        SupportService _supportService;

        public SupportController()
        {
            _supportService = new SupportService();
            _hardwareService = new HardwareService();
            _productService = new ProductService();
            _customerService = new CustomerService();
        }


        
        [CheckSession(SubActId = 6)]
        public ActionResult Installation()
        {
            var model = new InstallationListModel();
            model.InstallationLocation = GetInstallationLocationDropDown();
            model.InstallationProgram = GetInstallationProgramDropDown();
            model.SendSystemRequirements = GetSendSystemReqDropDown();
            model.SystemType = GetSystemTypeDropDown();
            HttpSessionStateBase session = HttpContext.Session;

            return View(model);
        }
        [CheckSession(SubActId = 15)]
        public ActionResult Other_Req()
        {
            var model = new OtherRequestListModel();
            //model.Active = _comm.GetActiveList();
            //model.Fields = GetFields();
            model.RequestType = GetOtherRequestsDropDown();
            HttpSessionStateBase session = HttpContext.Session;

            return View(model);
        }

        [HttpPost]
        public IEnumerable<SelectListItem> GetOtherRequestsDropDown()
        {
            var secs = _supportService.GetAllOtherRequestTypes()
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.RequestType_ID.ToString(),
                                        Text = x.RequestTypeName
                                    });

            return new SelectList(secs, "Value", "Text");

        }

        [HttpPost]
        public IEnumerable<SelectListItem> GetSystemTypeDropDown()
        {
            var secs = new List<SelectListItem>();
            secs.Add(new SelectListItem() { Text = "سرور", Value = "1" });
            secs.Add(new SelectListItem() { Text = "کلاینت", Value = "2" });

            return new SelectList(secs, "Value", "Text");

        }

        public string GetSystemTypeLiteral(int id)
        {
            switch (id)
            {
                case 1:
                    return "سرور";
                case 2:
                    return "کلاینت";
                default:
                    return "";
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 6)]
        public IEnumerable<SelectListItem> GetInstallationLocationDropDown()
        {
            var secs = new List<SelectListItem>();
            secs.Add(new SelectListItem() { Text = "شرکت سها", Value = "1" });
            secs.Add(new SelectListItem() { Text = "مکان مشتری", Value = "2" });

            return new SelectList(secs, "Value", "Text");

        }

        public string GetInstallationLocationLiteral(int id)
        {
            switch(id)
            {
                case 1:
                    return "شرکت سها";
                case 2:
                    return "مکان مشتری";
                default:
                    return "";
            }
        }

        public string GetInstallationProgramLiteral(int id)
        {
            switch (id)
            {
                case 1:
                    return "آخرین ورژن برنامه";
                case 2:
                    return "نسخه رایگان";
                default:
                    return "";
            }
        }

        public string GetSendSysReqLiteral(int id)
        {
            switch (id)
            {
                case 1:
                    return "ارسال شده است";
                case 2:
                    return "ارسال نشده است";
                default:
                    return "";
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 6)]
        public IEnumerable<SelectListItem> GetInstallationProgramDropDown()
        {
            var secs = new List<SelectListItem>();
            secs.Add(new SelectListItem() { Text = "آخرین ورژن برنامه", Value = "1" });
            secs.Add(new SelectListItem() { Text = "نسخه رایگان", Value = "2" });

            return new SelectList(secs, "Value", "Text");

        }

        [HttpPost]
        //[CheckSession]
        public IEnumerable<SelectListItem> GetSendSystemReqDropDown()
        {
            var secs = new List<SelectListItem>();
            secs.Add(new SelectListItem() { Text = "ارسال شده است", Value = "1" });
            secs.Add(new SelectListItem() { Text = "ارسال نشده است", Value = "2" });

            return new SelectList(secs, "Value", "Text");

        }
        [CheckSession(SubActId = 22)]
        public ActionResult RequestType()
        {
            var model = new RequestTypeListModel();
            //model.Active = _comm.GetActiveList();
            //model.Fields = GetFields();
            
            HttpSessionStateBase session = HttpContext.Session;

            return View(model);
        }

        
        [CheckSession(SubActId = 11)]
        public ActionResult SupportServices()
        {
            return View();
        }

        [HttpPost]
        [CheckSession(SubActId = 22)]
        public virtual ActionResult RequestTypeList(DataSourceRequest command, RequestTypeListModel model)
        {
            if (string.IsNullOrEmpty(model.RequestTypeName) && string.IsNullOrEmpty(model.Description))
            {
                var Items = _supportService.GetAllRequestTypes();
                var gridModel = new DataSourceResult
                {
                    Data = Items.Select(x => new RequestTypeListModel
                    {
                         RequestTypeName = x.RequestTypeName,
                        Description = x.Description,
                         RequestType_ID = x.RequestType_ID
                    }),
                    Total = Items.Count()
                };

                return Json(gridModel);
            }
            else
            {
                //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
                var UserItems = _supportService.SearchRequestType(model.RequestTypeName, model.Description);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new RequestTypeListModel
                    {
                        RequestTypeName = x.RequestTypeName,
                        Description = x.Description,
                        RequestType_ID = x.RequestType_ID
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);

            }
        }

        [HttpPost]
        [CheckSession(SubActId = 6)]
        public virtual ActionResult InstallationList(DataSourceRequest command, InstallationListModel model)
        {
            if (string.IsNullOrEmpty(model.AdministratorPassword) && string.IsNullOrEmpty(model.CPU) && string.IsNullOrEmpty(model.CustomerConnector) && string.IsNullOrEmpty(model.DeliveryDate_Customer) && string.IsNullOrEmpty(model.DeliveryDate_SOHA) && string.IsNullOrEmpty(model.Description) && string.IsNullOrEmpty(model.IPServer) && string.IsNullOrEmpty(model.PurchasedDevicesNumber) && string.IsNullOrEmpty(model.RAM) && model.ClientsNumber == 0 && model.Customer_ID == 0 && model.InstallationLocation_ID == 0 && model.InstallationProgram_ID == 0 && model.Installation_ID == 0 && model.SendSystemRequirements_ID == 0 && model.SystemType_ID == 0)
            {
                var Items = _supportService.GetAllInstallation();
                var gridModel = new DataSourceResult
                {
                    Data = Items.Select(x => new InstallationModel
                    {
                         AdministratorPassword = x.AdministratorPassword,
                          ClientsNumber = x.ClientsNumber,
                           CPU = x.CPU,
                            CustomerConnector = x.CustomerConnector,
                             CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                              Customer_ID = x.Customer_ID,
                               DeliveryDate_Customer = x.DeliveryDate_Customer,
                                DeliveryDate_SOHA = x.DeliveryDate_SOHA,
                                 Description = x.Description,
                                  InstallationLocation = GetInstallationLocationLiteral(x.InstallationLocation),
                                   InstallationLocation_ID = x.InstallationLocation,
                                    InstallationProgram = GetInstallationProgramLiteral(x.InstallationProgram),
                                     InstallationProgram_ID = x.InstallationProgram,
                                      Installation_ID = x.Installation_ID,
                                       IPServer = x.IPServer,
                                        PurchasedDevicesNumber = x.PurchasedDevicesNumber,
                                         RAM = x.RAM,
                                          SendSystemRequirements = GetSendSysReqLiteral(x.SendSystemRequirements),
                                           SendSystemRequirements_ID = x.SendSystemRequirements,
                                            SystemType = GetSystemTypeLiteral(x.SystemType),
                                             SystemType_ID = x.SystemType
                                              
                                            

                    }),
                    Total = Items.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var UserItems = _supportService.SearchInstallation(model.CustomerConnector, model.SendSystemRequirements_ID, model.InstallationLocation_ID, model.InstallationProgram_ID, model.SystemType_ID, model.DeliveryDate_SOHA, model.DeliveryDate_Customer, model.ClientsNumber, model.PurchasedDevicesNumber, model.CPU, model.RAM, model.IPServer, model.AdministratorPassword, model.Description);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new InstallationModel
                    {
                        AdministratorPassword = x.AdministratorPassword,
                        ClientsNumber = x.ClientsNumber,
                        CPU = x.CPU,
                        CustomerConnector = x.CustomerConnector,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        Customer_ID = x.Customer_ID,
                        DeliveryDate_Customer = x.DeliveryDate_Customer,
                        DeliveryDate_SOHA = x.DeliveryDate_SOHA,
                        Description = x.Description,
                        InstallationLocation = GetInstallationLocationLiteral(x.InstallationLocation),
                        InstallationLocation_ID = x.InstallationLocation,
                        InstallationProgram = GetInstallationProgramLiteral(x.InstallationProgram),
                        InstallationProgram_ID = x.InstallationProgram,
                        Installation_ID = x.Installation_ID,
                        IPServer = x.IPServer,
                        PurchasedDevicesNumber = x.PurchasedDevicesNumber,
                        RAM = x.RAM,
                        SendSystemRequirements = GetSendSysReqLiteral(x.SendSystemRequirements),
                        SendSystemRequirements_ID = x.SendSystemRequirements,
                        SystemType = GetSystemTypeLiteral(x.SystemType),
                        SystemType_ID = x.SystemType
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);

            }
        }

        [HttpPost]
        [CheckSession(SubActId = 15)]
        public virtual ActionResult OtherRequestList(DataSourceRequest command, OtherRequestListModel model)
        {
            if (model.Customer_ID==0 && string.IsNullOrEmpty(model.CustomerConnector) && model.RequestType_ID == 0 && string.IsNullOrEmpty(model.Description))
            {
                var Items = _supportService.GetAllOtherRequest();
                var gridModel = new DataSourceResult
                {
                    Data = Items.Select(x => new OtherRequestModel
                    {
                        CustomerConnector = x.CustomerConnector,
                        Description = x.Description,
                         CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                          Customer_ID = x.Customer_ID,
                           RequestType = _supportService.GetRequestTypeById(x.RequestType_ID).RequestTypeName,
                           Request_ID = x.Request_ID, 
                        RequestType_ID = x.RequestType_ID
                    }),
                    Total = Items.Count()
                };

                return Json(gridModel);
            }
            else
            {
                //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
                var UserItems = _supportService.SearchOtherRequest(model.Customer_ID, model.CustomerConnector, model.RequestType_ID, model.Description);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new OtherRequestModel
                    {
                        CustomerConnector = x.CustomerConnector,
                        Description = x.Description,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        Customer_ID = x.Customer_ID,
                        RequestType = _supportService.GetRequestTypeById(x.RequestType_ID).RequestTypeName,
                        Request_ID = x.Request_ID,
                        RequestType_ID = x.RequestType_ID
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);

            }
        }

        [CheckSession(SubActId = 22)]
        [HttpPost]
        public ActionResult InsertRequestType(RequestTypeListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                // update process
                Tbl_RequestType tg = new Tbl_RequestType();
                tg.Description = model.Description;
                tg.RequestTypeName = model.RequestTypeName;
                tg.RequestType_ID = model.RequestType_ID;
                tg.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tg.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tg.LastUpdateTime = DateTime.Now.ToString("HH:mm");
                if (_supportService.AddNewRequestType(tg))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new RequestTypeListModel
                        {
                            Message = Message.OperationSuccessful,
                            MessageColor = "green",
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }
                else
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new RequestTypeListModel
                        {
                            Message = Message.OperationUnsuccessful,
                            MessageColor = "red",
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }

            }
            catch (Exception ex)
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new RequestTypeListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [HttpPost]
        //[CheckSession]
        public virtual ActionResult CustomerSearch(DataSourceRequest command, SupportServicesListModel model)
        {
            //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
            var CustomerItems = _supportService.SearchCustomers(model.CustomerNameInSearch, model.MobileNo, model.SubscriptionCode, model.TelNo);
            var ProductItems = _productService.GetAllProducts();
            var CustomerProductItems = _productService.GetAllCustomerProducts();

            var query1 = from cus in CustomerItems
                         join cusprod in CustomerProductItems
                              on cus.Customer_ID equals cusprod.Customer_ID
                         select new
                         {
                             cus.Customer_ID,
                             cus.CustomerName,
                             cus.SubscriptionCode,
                             cus.TelNo,
                             cus.MobileNo,
                             cusprod.Products_ID
                         };

            var query2 = from qu1 in query1
                         join prod in ProductItems
                              on qu1.Products_ID equals prod.Products_ID
                         select new
                         {
                             qu1.Customer_ID,
                             qu1.CustomerName,
                             qu1.SubscriptionCode,
                             qu1.TelNo,
                             qu1.MobileNo,
                             prod.ProductsName
                         };



            var gridModel = new DataSourceResult
            {

                Data = query2.Select(x => new SupportServicesModel
                {
                    TelNo = x.TelNo,
                    SubscriptionCode = x.SubscriptionCode,
                    MobileNo = x.MobileNo,
                    Customer_ID = x.Customer_ID,
                    ProductsName = x.ProductsName,
                    CustomerNameInSearch = x.CustomerName
                }),
                Total = query2.Count()
            };

            return Json(gridModel);

        }


        [HttpPost]
        [CheckSession(SubActId = 11)]
        public virtual ActionResult SupportList(DataSourceRequest command, SupportServicesListModel model)
        {
            if (string.IsNullOrEmpty(model.CustomerConnector) && string.IsNullOrEmpty(model.CustomerName) && string.IsNullOrEmpty(model.Description) && string.IsNullOrEmpty(model.Problem) && string.IsNullOrEmpty(model.Guidance) && string.IsNullOrEmpty(model.GuidanceDate))
            {
                var SoftwareItems = _supportService.GetAllSupportService();
                var gridModel = new DataSourceResult
                {

                    Data = SoftwareItems.Select(x => new SupportServicesListModel
                    {
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        GuidanceDate = x.GuidanceDate,
                        Problem = x.Problem,
                        Guidance = x.Guidance,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        Request_ID = x.Request_ID


                    }),
                    Total = SoftwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var SoftwareItems = _supportService.SearchSupport(model.CustomerConnector, model.GuidanceDate, model.Problem, model.Guidance, model.Description);// _softwareService.GetSoftwareOfCusomer(model.Customer_ID);
                var gridModel = new DataSourceResult
                {

                    Data = SoftwareItems.Select(x => new SupportServicesListModel
                    {
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        GuidanceDate = x.GuidanceDate,
                        Problem = x.Problem,
                        Guidance = x.Guidance,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        Request_ID = x.Request_ID

                    }),
                    Total = SoftwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 11)]
        public ActionResult SubmitSupport(DataSourceRequest command, SupportServicesListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                if (false) // field validation
                {

                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SupportServicesListModel
                        {
                            Message = Message.InvalidCharacter,
                        },
                        Total = 1
                    };
                    return Json(gridModel);

                }

                Tbl_SupportServices tu = new Tbl_SupportServices();
                tu.Request_ID = model.Request_ID;
                tu.CustomerConnector = model.CustomerConnector;
                tu.Customer_ID = model.Customer_ID;
                tu.Description = model.Description;
                tu.Guidance = model.Guidance;
                tu.GuidanceDate = model.GuidanceDate;
                tu.Problem = model.Problem;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_supportService.AddNewSupportService(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SupportServicesListModel
                        {
                            Message = Message.OperationSuccessful,
                            MessageColor = "green"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }
                else
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SupportServicesListModel
                        {
                            Message = Message.OperationUnsuccessful,
                            MessageColor = "red"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }

            }
            catch (Exception ex)
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new SupportServicesListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            //return "";
        }

        [HttpPost]
        [CheckSession(SubActId = 15)]
        public ActionResult SubmitOtherRequest(OtherRequestListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                Tbl_OtherRequest tu = new Tbl_OtherRequest();
                tu.Description = model.Description;
                tu.Customer_ID = model.Customer_ID;
                tu.CustomerConnector = model.CustomerConnector;
                tu.Request_ID = model.Request_ID;
                tu.RequestType_ID = model.RequestType_ID;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_supportService.AddNewOtherRequest(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new OtherRequestModel
                        {
                            Message = Message.OperationSuccessful,
                            MessageColor = "green"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }
                else
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new OtherRequestModel
                        {
                            Message = Message.OperationUnsuccessful,
                            MessageColor = "red"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }

            }
            catch (Exception ex)
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new OtherRequestModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            //return "";
        }

        [HttpPost]
        [CheckSession(SubActId = 6)]
        public ActionResult SubmitInstallation(InstallationListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                Tbl_Installation tu = new Tbl_Installation();
                tu.AdministratorPassword = model.AdministratorPassword;
                tu.ClientsNumber = model.ClientsNumber;
                tu.CPU = model.CPU;
                tu.CustomerConnector = model.CustomerConnector;
                tu.Customer_ID = model.Customer_ID;
                tu.DeliveryDate_Customer = model.DeliveryDate_Customer;
                tu.DeliveryDate_SOHA = model.DeliveryDate_SOHA;
                tu.Description = model.Description;
                tu.InstallationLocation = model.InstallationLocation_ID;
                tu.InstallationProgram = model.InstallationProgram_ID;
                tu.Installation_ID = model.Installation_ID;
                tu.IPServer = model.IPServer;
                tu.PurchasedDevicesNumber = model.PurchasedDevicesNumber;
                tu.RAM = model.RAM;
                tu.SendSystemRequirements = model.SendSystemRequirements_ID;
                tu.SystemType = model.SystemType_ID;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_supportService.AddNewInstallation(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new InstallationModel
                        {
                            Message = Message.OperationSuccessful,
                            MessageColor = "green"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }
                else
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new InstallationModel
                        {
                            Message = Message.OperationUnsuccessful,
                            MessageColor = "red"
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }

            }
            catch (Exception ex)
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new InstallationModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            //return "";
        }
    }
}