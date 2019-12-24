using Core.Domain.Common;
using Data;
using Services;
using SohaCRM.Models.DeviceFailure;
using SohaCRM.Models.HardwareRequest;
using SohaCRM.Models.HardwareType;
using SohaCRM.Models.ReturnRepairedDevice;
using SohaCRM.Models.SendToRepair;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class HardwareController : Controller
    {
        HardwareService _hardwareService;
        ProductService _productService;
        CustomerService _customerService;
        ProblemType _ptypes;
        TroubleShooting _tsh;

        public HardwareController()
        {
            _hardwareService = new HardwareService();
            _productService = new ProductService();
            _customerService = new CustomerService();
            _ptypes = new ProblemType();
            _tsh = new TroubleShooting();
            
            
        }
        // GET: Hardware

        [CheckSession(SubActId = 8)]
        public ActionResult Hardware_req()
        {
            HttpSessionStateBase session = HttpContext.Session;
            HardwareListModel model = new HardwareListModel();
            model.Hardware = GetHardwareTypes();
            if(session["Customer_ID"] != null)
            {
                model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["Customer_ID"])).CustomerName;
                session.Remove("Customer_ID");
            }
            return View(model);
        }

        [CheckSession(SubActId = 12)]
        public ActionResult DeviceFailure()
        {
            HttpSessionStateBase session = HttpContext.Session;
            DeviceFailureListModel model = new DeviceFailureListModel();
            model.ProblemType = _ptypes.GetProblemTypeList();
            model.TroubleShooting = _tsh.GetTroubleShootingList();
            model.DeliveryDeviceSerialNumber = new List<SelectListItem>();
            if (session["Customer_ID"] != null)
            {
                model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["Customer_ID"])).CustomerName;
                session.Remove("Customer_ID");
            }
            return View(model);
        }
        [CheckSession(SubActId = 18)]
        public ActionResult HardwareType()
        {
            HttpSessionStateBase session = HttpContext.Session;
            HardwareTypeListModel model = new HardwareTypeListModel();
            model.Product = GetProductDropDown();
            //model.TroubleShooting = _tsh.GetTroubleShootingList();
            //if (session["Customer_ID"] != null)
            //{
            //    model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["Customer_ID"])).CustomerName;
            //    session.Remove("Customer_ID");
            //}
            return View(model);
        }

        [CheckSession(SubActId = 13)]
        public ActionResult SendToRepair()
        {
            HttpSessionStateBase session = HttpContext.Session;
            SendToRepairListModel model = new SendToRepairListModel();
            //model.Product = GetProductDropDown();
            //model.TroubleShooting = _tsh.GetTroubleShootingList();
            //if (session["Customer_ID"] != null)
            //{
            //    model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["Customer_ID"])).CustomerName;
            //    session.Remove("Customer_ID");
            //}
            model.DeviceSerialNumber = new List<SelectListItem>();
            return View(model);
        }
        [CheckSession(SubActId = 14)]
        public ActionResult ReturnRepairedDevice()
        {
            HttpSessionStateBase session = HttpContext.Session;
            ReturnRepairedDeviceListModel model = new ReturnRepairedDeviceListModel();
            //model.Product = GetProductDropDown();
            //model.TroubleShooting = _tsh.GetTroubleShootingList();
            //if (session["Customer_ID"] != null)
            //{
            //    model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["Customer_ID"])).CustomerName;
            //    session.Remove("Customer_ID");
            //}
            model.DeviceSerialNumber = new List<SelectListItem>();
            return View(model);
        }

        [HttpPost]
        //[CheckSession]
        public virtual ActionResult CustomerSearch(DataSourceRequest command, HardwareListModel model)
        {
            //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
            var CustomerItems = _hardwareService.SearchCustomers(model.CustomerNameInSearch,   model.MobileNo, model.SubscriptionCode, model.TelNo);
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

                Data = query2.Select(x => new HardwareModel
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
        public ActionResult GetCustomerSerialsDropDownData(DataSourceRequest command, DeviceFailureListModel model)
        {
            var secs = _productService.GetAllCustomerHardware(model.Customer_ID);
                            

            //return new SelectList(secs, "Value", "Text");

            var gridModel = new DataSourceResult
            {

                Data = secs.Select(x => new DeviceFailureModel
                {
                     
                     DeliveryDeviceSerialNumber = x.DeliveryDeviceSerialNumber,
                      Request_ID = x.Request_ID
                }),
                Total = secs.Count()
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GetDeviceInfo(DataSourceRequest command, DeviceFailureListModel model)
        {
            var HardwareItems = _hardwareService.GetAllHardware();
            var HardwareTypeItems = _hardwareService.GetAllHardwareTypes();

            var query1 = from hi in HardwareItems
                         join hti in HardwareTypeItems
                              on hi.Hardware_ID equals hti.Hardware_ID
                         where hi.Request_ID == model.DeliveryDeviceSerialNumber_ID && hi.DeliveryDate!=null
                         select new
                         {
                             hi.DeliveryDate,
                             hti.WarrantyPeriod
                         };

            var gridModel = new DataSourceResult
            {

                ExtraData = new DeviceFailureModel
                {
                     DeliveryDate = query1.FirstOrDefault().DeliveryDate,
                     WarrantyPeriod = query1.FirstOrDefault().WarrantyPeriod
                },
                Total = query1.Count()
            };

            return Json(gridModel);
        }

        public ActionResult GetDeviceInfo2(DataSourceRequest command, SendToRepairListModel model)
        {
            var HardwareItems = _hardwareService.GetAllHardware();
            var HardwareTypeItems = _hardwareService.GetAllHardwareTypes();

            var query1 = from hi in HardwareItems
                         join hti in HardwareTypeItems
                              on hi.Hardware_ID equals hti.Hardware_ID
                         where hi.Request_ID == model.DeviceSerialNumber_ID && hi.DeliveryDate != null
                         select new
                         {
                             hi.DeliveryDate,
                             hti.WarrantyPeriod
                         };

            var gridModel = new DataSourceResult
            {

                ExtraData = new SendToRepairModel
                {
                    DeliveryDate = query1.FirstOrDefault().DeliveryDate,
                    WarrantyPeriod = query1.FirstOrDefault().WarrantyPeriod
                },
                Total = query1.Count()
            };

            return Json(gridModel);
        }

        public ActionResult GetDeviceInfo3(DataSourceRequest command, ReturnRepairedDeviceListModel model)
        {
            var HardwareItems = _hardwareService.GetAllHardware();
            var HardwareTypeItems = _hardwareService.GetAllHardwareTypes();

            var query1 = from hi in HardwareItems
                         join hti in HardwareTypeItems
                              on hi.Hardware_ID equals hti.Hardware_ID
                         where hi.Request_ID == model.DeviceSerialNumber_ID && hi.DeliveryDate != null
                         select new
                         {
                             hi.DeliveryDate,
                             hti.WarrantyPeriod
                         };

            var gridModel = new DataSourceResult
            {

                ExtraData = new ReturnRepairedDeviceModel
                {
                    DeliveryDate = query1.FirstOrDefault().DeliveryDate,
                    WarrantyPeriod = query1.FirstOrDefault().WarrantyPeriod
                },
                Total = query1.Count()
            };

            return Json(gridModel);
        }

        [HttpPost]
        [CheckSession(SubActId = 8)]
        public virtual ActionResult HardwareList(DataSourceRequest command, HardwareListModel model)
        {
            if (string.IsNullOrEmpty(model.CustomerConnector) && model.Hardware_ID == 0 && model.Amount == 0 && string.IsNullOrEmpty(model.RequestDate) && string.IsNullOrEmpty(model.Description) && model.Status == 0 )
            {
                var hardwareItems = _hardwareService.GetAllHardware();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new HardwareModel
                    {
                        Amount = x.Amount,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        Hardware = _hardwareService.GetHardwareById(x.Hardware_ID).HardwareName,
                        CustomerConnector = x.CustomerConnector,
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        HardwareTypeDescription = _hardwareService.GetHardwareTypeById(x.Hardware_ID).Description,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryDescription = x.DeliveryDescription,
                        DeliveryDeviceSerialNumber = x.DeliveryDeviceSerialNumber,
                        Check_Delivery = x.Status == 1 ? true : false,
                        Check_Revocation = x.Status == 2 ? true : false,
                        Customer_ID = x.Customer_ID,
                        Hardware_ID = x.Hardware_ID,
                        Request_ID = x.Request_ID

                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _hardwareService.SearchHardware(model.CustomerConnector,model.Hardware_ID,model.RequestDate,model.Description,model.Status);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new HardwareModel
                    {
                        Amount = x.Amount,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        Hardware = _hardwareService.GetHardwareById(x.Hardware_ID).HardwareName,
                        CustomerConnector = x.CustomerConnector,
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        Hardware_ID = x.Hardware_ID,
                        Request_ID = x.Request_ID,
                        HardwareTypeDescription = _hardwareService.GetHardwareTypeById(x.Hardware_ID).Description,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryDescription = x.DeliveryDescription,
                        DeliveryDeviceSerialNumber = x.DeliveryDeviceSerialNumber,
                        Check_Delivery = x.Status == 1 ? true : false,
                        Check_Revocation = x.Status == 2 ? true : false,


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 12)]
        public virtual ActionResult DeviceFailureList(DataSourceRequest command, DeviceFailureListModel model)
        {
            if (model.Customer_ID == 0 && string.IsNullOrEmpty(model.CustomerConnector) && string.IsNullOrEmpty(model.RequestDate) && model.ProblemType_ID == 0 && model.TroubleShooting_ID == 0 && string.IsNullOrEmpty(model.Reserve) && string.IsNullOrEmpty(model.Description))
            {
                var hardwareItems = _hardwareService.GetAllDeviceFailure();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new DeviceFailureModel
                    {
                       
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        ProblemType = _ptypes.GetLiteralByInt(x.ProblemType),
                        CustomerConnector = x.CustomerConnector,
                         TroubleShooting_ID = x.Troubleshooting,
                          DeliveryDeviceSerialNumber = x.DeliveryDeviceSerialNumber,
                          DeliveryDate = x.DeliveryDate,
                          WarrantyPeriod = x.WarrantyPeriod,
                          TroubleShooting = _tsh.GetLiteralByInt(x.Troubleshooting),
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        ProblemType_ID = x.ProblemType,
                        Request_ID = x.Request_ID


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _hardwareService.SearchDeviceFailure(model.CustomerConnector, model.RequestDate, model.ProblemType_ID, model.TroubleShooting_ID, model.Reserve, model.Description);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new DeviceFailureModel
                    {
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        ProblemType = _ptypes.GetLiteralByInt(x.ProblemType),
                        CustomerConnector = x.CustomerConnector,
                        TroubleShooting_ID = x.Troubleshooting,
                        TroubleShooting = _tsh.GetLiteralByInt(x.Troubleshooting),
                        DeliveryDeviceSerialNumber = x.DeliveryDeviceSerialNumber,
                        DeliveryDate = x.DeliveryDate,
                        WarrantyPeriod = x.WarrantyPeriod,
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        ProblemType_ID = x.ProblemType,
                        Request_ID = x.Request_ID


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 13)]
        public virtual ActionResult SendToRepairList(DataSourceRequest command, SendToRepairListModel model)
        {
            if (model.Customer_ID == 0 && string.IsNullOrEmpty(model.CustomerConnector) && string.IsNullOrEmpty(model.PostageDate) && string.IsNullOrEmpty(model.ReturnDate) && string.IsNullOrEmpty(model.ProblemInfo) && string.IsNullOrEmpty(model.Reserve) && string.IsNullOrEmpty(model.Description))
            {
                var hardwareItems = _hardwareService.GetAllSendToRepair();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new SendToRepairModel
                    {

                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        DeviceSerialNumber = x.DeviceSerialNumber,
                        DeliveryDate = x.DeliveryDate,
                        WarrantyPeriod = x.WarrantyPeriod,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        Request_ID = x.Request_ID,
                         ProblemInfo = x.ProblemInfo,
                          PostageDate = x.PostageDate,
                          ReturnDate = x.ReturnDate
                         


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _hardwareService.SearchSendToRepair(model.CustomerConnector, model.ProblemInfo, model.PostageDate, model.ReturnDate, model.Reserve, model.Description);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new SendToRepairModel
                    {
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        DeviceSerialNumber = x.DeviceSerialNumber,
                        DeliveryDate = x.DeliveryDate,
                        WarrantyPeriod = x.WarrantyPeriod,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        Request_ID = x.Request_ID,
                        ProblemInfo = x.ProblemInfo,
                        PostageDate = x.PostageDate,
                        ReturnDate = x.ReturnDate


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 14)]
        public virtual ActionResult ReturnRepairedDeviceList(DataSourceRequest command, ReturnRepairedDeviceListModel model)
        {
            if (model.Customer_ID == 0 && string.IsNullOrEmpty(model.CustomerConnector) && string.IsNullOrEmpty(model.ReturnDate) && string.IsNullOrEmpty(model.Repairs) && string.IsNullOrEmpty(model.Reserve) && string.IsNullOrEmpty(model.Description))
            {
                var hardwareItems = _hardwareService.GetAllReturnRepairedDevice();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new ReturnRepairedDeviceModel
                    {

                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        DeviceSerialNumber = x.DeviceSerialNumber,
                        DeliveryDate = x.DeliveryDate,
                        WarrantyPeriod = x.WarrantyPeriod,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        Request_ID = x.Request_ID,
                        Repairs = x.Repairs,
                        ReturnDate = x.ReturnDate



                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _hardwareService.SearchReturnRepairedDevice(model.CustomerConnector, model.Repairs, model.ReturnDate, model.Reserve, model.Description);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new ReturnRepairedDeviceModel
                    {
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        DeviceSerialNumber = x.DeviceSerialNumber,
                        DeliveryDate = x.DeliveryDate,
                        WarrantyPeriod = x.WarrantyPeriod,
                        Description = x.Description,
                        Customer_ID = x.Customer_ID,
                        Request_ID = x.Request_ID,
                        Repairs = x.Repairs,
                        ReturnDate = x.ReturnDate


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 18)]
        public virtual ActionResult HardwareTypeList(DataSourceRequest command, HardwareTypeListModel model)
        {
            if (model.Hardware_ID == 0 && string.IsNullOrEmpty(model.Description) && string.IsNullOrEmpty(model.HardwareName) && model.HardwarePrice == 0 && model.Products_ID == 0 && string.IsNullOrEmpty(model.WarrantyPeriod))
            {
                var hardwareItems = _hardwareService.GetAllHardwareTypes();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new HardwareTypeModel
                    {
                         Product = _productService.GetProductById(x.Products_ID).ProductsName,
                         HardwareName = x.HardwareName,
                          HardwarePrice = x.HardwarePrice,
                           Hardware_ID = x.Hardware_ID,
                            Products_ID = x.Products_ID,
                             Description = x.Description,
                             WarrantyPeriod = x.WarrantyPeriod
                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _hardwareService.SearchHardwareType(model.Description,model.HardwareName,model.HardwarePrice,model.Products_ID,model.WarrantyPeriod);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new HardwareTypeModel
                    {
                        Product = _productService.GetProductById(x.Products_ID).ProductsName,
                        HardwareName = x.HardwareName,
                        HardwarePrice = x.HardwarePrice,
                        Hardware_ID = x.Hardware_ID,
                        Products_ID = x.Products_ID,
                        Description = x.Description,
                        WarrantyPeriod = x.WarrantyPeriod
                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        public JsonResult GetHardwareInfo(DataSourceRequest command, HardwareListModel model)
        {
            
            if (model.Hardware_ID!=0)
            {
                model.Amount = _hardwareService.GetHardwareTypeById(model.Hardware_ID).HardwarePrice;
                model.HardwareTypeDescription = _hardwareService.GetHardwareTypeById(model.Hardware_ID).Description;
            }
            return Json(model);
        }

        [HttpPost]
        //[CheckSession]
        public IEnumerable<SelectListItem> GetHardwareTypes()
        {
            var secs = _hardwareService.GetAllHardwareTypes()
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.Hardware_ID.ToString(),
                                        Text = x.HardwareName
                                    });

            return new SelectList(secs, "Value", "Text");

        }

        [HttpPost]
        //[CheckSession]
        public IEnumerable<SelectListItem> GetProductDropDown()
        {
            var secs = _productService.GetAllProducts()
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.Products_ID.ToString(),
                                        Text = x.ProductsName
                                    });

            return new SelectList(secs, "Value", "Text");

        }


        [HttpPost]
        [CheckSession(SubActId = 8)]
        public ActionResult SubmitHardware(DataSourceRequest command, HardwareListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                if (false) // field validation
                {

                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new HardwareListModel
                        {
                            Message = Message.InvalidCharacter,
                        },
                        Total = 1
                    };
                    return Json(gridModel);

                }

                Tbl_Hardware tu = new Tbl_Hardware();
                tu.Request_ID = model.Request_ID;
                tu.CustomerConnector = model.CustomerConnector;
                tu.Customer_ID = model.Customer_ID;
                tu.Description = model.Description;
                tu.Hardware_ID = model.Hardware_ID;
                tu.RequestDate = model.RequestDate;
                if (model.Check_Delivery && !model.Check_Revocation)
                    tu.Status = 1;
                else if (!model.Check_Delivery && model.Check_Revocation)
                    tu.Status = 2;
                else
                    tu.Status = 0;
                tu.DeliveryDate = model.DeliveryDate;
                tu.DeliveryDescription = model.DeliveryDescription;
                tu.DeliveryDeviceSerialNumber = model.DeliveryDeviceSerialNumber;
                tu.Amount = model.Amount;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_hardwareService.AddNewHardware(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new HardwareListModel
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
                        ExtraData = new HardwareListModel
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
                    ExtraData = new HardwareListModel
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
        [CheckSession(SubActId = 12)]
        public ActionResult SubmitDeviceFailure(DeviceFailureModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                Tbl_DeviceFailure tu = new Tbl_DeviceFailure();
                tu.CustomerConnector = model.CustomerConnector;
                tu.Customer_ID = model.Customer_ID;
                tu.Description = model.Description;
                tu.ProblemType = model.ProblemType_ID;
                tu.RequestDate = model.RequestDate;
                tu.DeliveryDate = model.DeliveryDate;
                tu.Request_ID = model.Request_ID;
                tu.DeliveryDeviceSerialNumber = model.Reserve;//.DeliveryDeviceSerialNumber;
                tu.WarrantyPeriod = model.WarrantyPeriod;
                tu.Troubleshooting = model.TroubleShooting_ID;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if(_hardwareService.AddNewDeviceFailure(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new DeviceFailureModel
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
                        ExtraData = new DeviceFailureModel
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
                    ExtraData = new DeviceFailureModel
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
        [CheckSession(SubActId = 13)]
        public ActionResult SubmitSendToRepair(SendToRepairModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                Tbl_SendToRepair tu = new Tbl_SendToRepair();
                tu.Customer_ID = model.Customer_ID;
                tu.CustomerConnector = model.CustomerConnector;
                tu.DeviceSerialNumber = model.Reserve;
                tu.DeliveryDate = model.DeliveryDate;
                tu.WarrantyPeriod = model.WarrantyPeriod;
                tu.ProblemInfo = model.ProblemInfo;
                tu.PostageDate = model.PostageDate;
                tu.ReturnDate = model.ReturnDate;
                tu.Description = model.Description;
                tu.Request_ID = model.Request_ID;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_hardwareService.AddNewSendToRepair(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SendToRepairModel
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
                        ExtraData = new SendToRepairModel
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
                    ExtraData = new SendToRepairModel
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
        [CheckSession(SubActId = 14)]
        public ActionResult SubmitReturnRepairedDevice(ReturnRepairedDeviceModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                Tbl_ReturnRepairedDevice tu = new Tbl_ReturnRepairedDevice();
                tu.Customer_ID = model.Customer_ID;
                tu.CustomerConnector = model.CustomerConnector;
                tu.DeviceSerialNumber = model.Reserve;
                tu.DeliveryDate = model.DeliveryDate;
                tu.WarrantyPeriod = model.WarrantyPeriod;
                tu.Repairs = model.Repairs;
                tu.ReturnDate = model.ReturnDate;
                tu.Description = model.Description;
                tu.Request_ID = model.Request_ID;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_hardwareService.AddNewReturnRepairedDevice(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new ReturnRepairedDeviceModel
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
                        ExtraData = new ReturnRepairedDeviceModel
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
                    ExtraData = new ReturnRepairedDeviceModel
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
        [CheckSession(SubActId = 18)]
        public ActionResult SubmitHardwareType(HardwareTypeListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                Tbl_HardwareType tu = new Tbl_HardwareType();
                tu.Description = model.Description;
                tu.HardwareName = model.HardwareName;
                tu.HardwarePrice = model.HardwarePrice;
                tu.Hardware_ID = model.Hardware_ID;
                tu.Products_ID = model.Products_ID;
                tu.WarrantyPeriod = model.WarrantyPeriod;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_hardwareService.AddNewHardwareType(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new HardwareTypeListModel
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
                        ExtraData = new HardwareTypeListModel
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
                    ExtraData = new HardwareTypeListModel
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