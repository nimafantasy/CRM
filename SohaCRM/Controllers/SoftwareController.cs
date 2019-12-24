using Core.Domain.Common;
using Data;
using Services;
using SohaCRM.Models.ChangeSoftware;
using SohaCRM.Models.SoftwareRequest;
using SohaCRM.Models.SoftwareType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class SoftwareController : Controller
    {
        SoftwareService _softwareService;
        ProductService _productService;
        CustomerService _customerService;
        UserService _userService;

        public SoftwareController()
        {
            _softwareService = new SoftwareService();
            _productService = new ProductService();
            _customerService = new CustomerService();
            _userService = new UserService();
        }
        // GET: Software

        
        [CheckSession(SubActId = 9)]
        public ActionResult Software_req()
        {
            HttpSessionStateBase session = HttpContext.Session;
            SoftwareListModel model = new SoftwareListModel();
            model.Software = GetSoftwareTypes();
            if (session["Customer_ID"] != null)
            {
                model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["Customer_ID"])).CustomerName;
            }
            return View(model);
        }
        [CheckSession(SubActId = 19)]
        public ActionResult SoftwareType()
        {
            HttpSessionStateBase session = HttpContext.Session;
            SoftwareTypeListModel model = new SoftwareTypeListModel();
            model.Product = GetProductDropDown();
            //model.TroubleShooting = _tsh.GetTroubleShootingList();
            //if (session["Customer_ID"] != null)
            //{
            //    model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["Customer_ID"])).CustomerName;
            //    session.Remove("Customer_ID");
            //}
            return View(model);
        }


        
        [CheckSession(SubActId = 10)]
        public ActionResult ChangeSoftware_req()
        {
            HttpSessionStateBase session = HttpContext.Session;
            Tbl_User user = _userService.GetUserById(Convert.ToInt32(session["UserID"]));
            session.Add("Section_ID", user.Section_ID);
            return View();
        }

        [HttpPost]
        [CheckSession(SubActId = 19)]
        public virtual ActionResult SoftwareTypeList(DataSourceRequest command, SoftwareTypeListModel model)
        {
            if (model.Software_ID == 0 && string.IsNullOrEmpty(model.Description) && string.IsNullOrEmpty(model.SoftwareName) && model.SoftwarePrice == 0 && model.Products_ID == 0)
            {
                var hardwareItems = _softwareService.GetAllSoftwareTypes();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new SoftwareTypeModel
                    {
                        Product = _productService.GetProductById(x.Products_ID).ProductsName,
                        SoftwareName = x.SoftwareName,
                        SoftwarePrice = x.SoftwarePrice,
                        Software_ID = x.Software_ID,
                        Products_ID = x.Products_ID,
                        Description = x.Description,
                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _softwareService.SearchSoftwareType(model.Description, model.SoftwareName, model.SoftwarePrice, model.Products_ID);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new SoftwareTypeModel
                    {
                        Product = _productService.GetProductById(x.Products_ID).ProductsName,
                        SoftwareName = x.SoftwareName,
                        SoftwarePrice = x.SoftwarePrice,
                        Software_ID = x.Software_ID,
                        Products_ID = x.Products_ID,
                        Description = x.Description,
                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession]
        public virtual ActionResult CustomerSearch(DataSourceRequest command, SoftwareListModel model)
        {
            //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
            var CustomerItems = _softwareService.SearchCustomers(model.CustomerNameInSearch, model.MobileNo, model.SubscriptionCode, model.TelNo);
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

                Data = query2.Select(x => new SoftwareModel
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
        [CheckSession(SubActId = 9)]
        public virtual ActionResult SoftwareList(DataSourceRequest command, SoftwareListModel model)
        {
            if (string.IsNullOrEmpty(model.CustomerConnector) && model.Software_ID == 0 && model.Amount == 0 && string.IsNullOrEmpty(model.RequestDate) && string.IsNullOrEmpty(model.Description))
            {
                var SoftwareItems = _softwareService.GetAllSoftware();
                var gridModel = new DataSourceResult
                {

                    Data = SoftwareItems.Select(x => new SoftwareModel
                    {
                        Amount = x.Amount,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        Software = _softwareService.GetSoftwareById(x.Software_ID).SoftwareName,
                        CustomerConnector = x.CustomerConnector,
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        SoftwareTypeDescription = _softwareService.GetSoftwareById(x.Software_ID).Description,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryDescription = x.DeliveryDescription,
                        Check_Delivery = x.Status == 1 ? true : false,
                        Check_Revocation = x.Status == 2 ? true : false,
                        Customer_ID = x.Customer_ID,
                        Software_ID = x.Software_ID,
                        Request_ID = x.Request_ID


                    }),
                    Total = SoftwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var SoftwareItems = _softwareService.SearchSoftware(model.CustomerConnector, model.Software_ID, model.RequestDate, model.Description);// _softwareService.GetSoftwareOfCusomer(model.Customer_ID);
                var gridModel = new DataSourceResult
                {

                    Data = SoftwareItems.Select(x => new SoftwareModel
                    {
                        Amount = x.Amount,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        Software = _softwareService.GetSoftwareById(x.Software_ID).SoftwareName,
                        CustomerConnector = x.CustomerConnector,
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        SoftwareTypeDescription = _softwareService.GetSoftwareById(x.Software_ID).Description,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryDescription = x.DeliveryDescription,
                        Check_Delivery = x.Status == 1 ? true : false,
                        Check_Revocation = x.Status == 2 ? true : false,
                        Customer_ID = x.Customer_ID,
                        Software_ID = x.Software_ID,
                        Request_ID = x.Request_ID


                    }),
                    Total = SoftwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 10)]
        public virtual ActionResult SoftwareChangeList(DataSourceRequest command, ChangeSoftwareListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            Tbl_User user = _userService.GetUserById(Convert.ToInt32(session["UserID"]));
            if (model.Amount == 0 && string.IsNullOrEmpty(model.CustomerConnector) && model.Customer_ID == 0 && string.IsNullOrEmpty(model.RequestDate) && string.IsNullOrEmpty(model.DeliveryDate) && string.IsNullOrEmpty(model.Description) && string.IsNullOrEmpty(model.RequiredChanges))
            {
                var SoftwareItems = _softwareService.GetAllSoftwareChange();
                var gridModel = new DataSourceResult
                {

                    Data = SoftwareItems.Select(x => new ChangeSoftwareModel
                    {
                        Amount = x.Amount,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        DeliveryDate = x.DeliveryDate,
                        Customer_ID = x.Customer_ID,
                        RowColor =  user.Section_ID == 1 && string.IsNullOrEmpty(x.DeliveryDate) ? 1 : user.Section_ID == 3 && !string.IsNullOrEmpty(x.DeliveryDate) && x.Amount == 0 ? 1 : 0,
                        Request_ID = x.Request_ID,
                         RequiredChanges = x.RequiredChanges
                    }),
                    Total = SoftwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var SoftwareItems = _softwareService.SearchSoftwareChange(model.Amount, model.CustomerConnector, model.Customer_ID,model.RequestDate, model.DeliveryDate,model.Description,model.RequiredChanges);
                var gridModel = new DataSourceResult
                {

                    Data = SoftwareItems.Select(x => new ChangeSoftwareModel
                    {
                        Amount = x.Amount,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName,
                        CustomerConnector = x.CustomerConnector,
                        RequestDate = x.RequestDate,
                        Description = x.Description,
                        DeliveryDate = x.DeliveryDate,
                        Customer_ID = x.Customer_ID,
                        RowColor = user.Section_ID == 1 && string.IsNullOrEmpty(x.DeliveryDate) ? 1 : user.Section_ID == 3 && !string.IsNullOrEmpty(x.DeliveryDate) && x.Amount == 0 ? 1 : 0,
                        Request_ID = x.Request_ID,
                        RequiredChanges = x.RequiredChanges
                    }),
                    Total = SoftwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        public JsonResult GetSoftwareInfo(DataSourceRequest command, SoftwareListModel model)
        {

            if (model.Software_ID != 0)
            {
                model.Amount = _softwareService.GetSoftwareById(model.Software_ID).SoftwarePrice;
                model.SoftwareTypeDescription = _softwareService.GetSoftwareById(model.Software_ID).Description;
            }
            return Json(model);
        }

        [HttpPost]
        [CheckSession]
        public IEnumerable<SelectListItem> GetSoftwareTypes()
        {
            var secs = _softwareService.GetAllSoftwareTypes()
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.Software_ID.ToString(),
                                        Text = x.SoftwareName
                                    });

            return new SelectList(secs, "Value", "Text");

        }

        [HttpPost]
        [CheckSession(SubActId = 9)]
        public ActionResult SubmitSoftware(DataSourceRequest command, SoftwareListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                if (false) // field validation
                {

                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SoftwareListModel
                        {
                            Message = Message.InvalidCharacter,
                        },
                        Total = 1
                    };
                    return Json(gridModel);

                }

                Tbl_Software tu = new Tbl_Software();
                tu.Request_ID = model.Request_ID;
                tu.CustomerConnector = model.CustomerConnector;
                tu.Customer_ID = model.Customer_ID;
                tu.Description = model.Description;
                tu.Software_ID = model.Software_ID;
                tu.RequestDate = model.RequestDate;
                if (model.Check_Delivery && !model.Check_Revocation)
                    tu.Status = 1;
                else if (!model.Check_Delivery && model.Check_Revocation)
                    tu.Status = 2;
                else
                    tu.Status = 0;
                tu.DeliveryDate = model.DeliveryDate;
                tu.DeliveryDescription = model.DeliveryDescription;
                tu.Amount = model.Amount;
                tu.LastUpdateUser_ID =  Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_softwareService.AddNewSoftware(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SoftwareListModel
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
                        ExtraData = new SoftwareListModel
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
                    ExtraData = new SoftwareListModel
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
        [CheckSession(SubActId = 10)]
        public ActionResult SubmitSoftwareChange(DataSourceRequest command, ChangeSoftwareListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                if (false) // field validation
                {

                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SoftwareListModel
                        {
                            Message = Message.InvalidCharacter,
                        },
                        Total = 1
                    };
                    return Json(gridModel);

                }

                Tbl_ChangeSoftware tu = new Tbl_ChangeSoftware();
                tu.Request_ID = model.Request_ID;
                tu.CustomerConnector = model.CustomerConnector;
                tu.Customer_ID = model.Customer_ID;
                tu.Description = model.Description;
                tu.RequestDate = model.RequestDate;
                tu.RequiredChanges = model.RequiredChanges;
                tu.DeliveryDate = model.DeliveryDate;
                tu.Description = model.Description;
                tu.Amount = model.Amount;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_softwareService.AddNewSoftwareChange(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new ChangeSoftwareListModel
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
                        ExtraData = new ChangeSoftwareListModel
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
                    ExtraData = new ChangeSoftwareListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
            //return "";
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
        [CheckSession(SubActId = 19)]
        public ActionResult SubmitSoftwareType(SoftwareTypeListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {

                Tbl_SoftwareType tu = new Tbl_SoftwareType();
                tu.Description = model.Description;
                tu.SoftwareName = model.SoftwareName;
                tu.SoftwarePrice = model.SoftwarePrice;
                tu.Software_ID = model.Software_ID;
                tu.Products_ID = model.Products_ID;
                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_softwareService.AddNewSoftwareType(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new SoftwareTypeListModel
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
                        ExtraData = new SoftwareTypeListModel
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
                    ExtraData = new SoftwareTypeListModel
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