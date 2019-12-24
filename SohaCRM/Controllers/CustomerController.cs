using Core.Domain.Common;
using Data;
using Services;
using SohaCRM.Models.Customer;
using SohaCRM.Models.CustomerField;
using SohaCRM.Models.CustomerProducts;
using SohaCRM.Models.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class CustomerController : Controller
    {
        GroupService _groupService;
        ProductService _productService;
        Active _comm;
        UserService _User;
        CustomerService _customerService;



        public CustomerController()
        {
            _groupService = new GroupService();
            _productService = new ProductService();
            _comm = new Active();
            _User = new UserService();
            _customerService = new CustomerService();
        }

        [CheckSession(SubActId = 5)]
        public virtual ActionResult Index()
        {
            return RedirectToAction("Customer");
        }

        [CheckSession(SubActId = 5)]
        public virtual ActionResult Customer()
        {
            var model = new CustomerListModel();
            model.Active = _comm.GetActiveList();
            model.Fields = GetFields();
            return View(model);
        }

        [CheckSession]
        public virtual ActionResult Training()
        {
            var model = new TrainingListModel();
            
            return View(model);
        }

        [CheckSession(SubActId = 5)]
        public virtual ActionResult Customer_Products()
        {
            var model = new CustomerProductsListModel();
            //model.Active = _comm.GetActiveList();
            //model.Fields = GetFields();
            HttpSessionStateBase session = HttpContext.Session;
            if (session["SelectedCustomerId"] != null)
            {
                model.CustomerName = _customerService.GetCustomerById(Convert.ToInt32(session["SelectedCustomerId"])).CustomerName;
                model.Customer_ID = Convert.ToInt32(session["SelectedCustomerId"]);
                
            }
            return View(model);
        }

        [CheckSession(SubActId = 21)]
        public virtual ActionResult CustomerField()
        {
            var model = new CustomerFieldListModel();
            //model.Active = _comm.GetActiveList();
            //model.Fields = GetFields();
            HttpSessionStateBase session = HttpContext.Session;

            return View(model);
        }

        [HttpPost]
        [CheckSession(SubActId = 5)]
        public virtual ActionResult CustomerList(DataSourceRequest command, CustomerListModel model)
        {
            if (string.IsNullOrEmpty(model.Address) && string.IsNullOrEmpty(model.CompanyRegistrationName) && string.IsNullOrEmpty(model.CustomerConnector) && string.IsNullOrEmpty(model.CustomerName) && string.IsNullOrEmpty(model.CompanyRegistrationName) && string.IsNullOrEmpty(model.EconomicalNumber) && string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.FaxNo) && string.IsNullOrEmpty(model.MobileNo) && string.IsNullOrEmpty(model.NationalID) && string.IsNullOrEmpty(model.PostalCode) && string.IsNullOrEmpty(model.RegistrationNumber) && string.IsNullOrEmpty(model.SubscriptionCode) && string.IsNullOrEmpty(model.TelNo) && model.IsActive_ID ==0 && model.CustomerField_ID==0)
            {
                var Items = _customerService.GetAllCustomers();
                var gridModel = new DataSourceResult
                {
                    Data = Items.Select(x => new CustomerModel
                    {
                        TelNo = x.TelNo,
                        SubscriptionCode = x.SubscriptionCode,
                        RegistrationNumber = x.RegistrationNumber,
                        PostalCode = x.PostalCode,
                        NationalID = x.NationalID,
                        MobileNo = x.MobileNo,
                        Address = x.Address,
                        CompanyRegistrationName = x.CompanyRegistrationName,
                        CustomerConnector = x.CustomerConnector,
                        CustomerField_ID = x.CustomerField_ID,
                        Field = _customerService.GetFieldById(x.CustomerField_ID).CustomerField_Name,
                                   CustomerName = x.CustomerName,
                                    Customer_ID = x.Customer_ID,
                                     EconomicalNumber = x.EconomicalNumber,
                                      Email = x.Email,
                                       FaxNo = x.FaxNo,
                        IsActive_ID = x.Active,
                        IsActive = _comm.GetLiteralByInt(x.Active),
                    }),
                    Total = Items.Count()
                };

                return Json(gridModel);
            }
            else
            {
                //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
                var UserItems = _customerService.SearchCustomers(model.Address, model.CompanyRegistrationName, model.CustomerConnector, model.CustomerName, model.EconomicalNumber, model.Email, model.FaxNo, model.MobileNo, model.NationalID, model.PostalCode, model.RegistrationNumber, model.SubscriptionCode, model.TelNo, model.CustomerField_ID, model.IsActive_ID );
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new CustomerModel
                    {
                        TelNo = x.TelNo,
                        SubscriptionCode = x.SubscriptionCode,
                        RegistrationNumber = x.RegistrationNumber,
                        PostalCode = x.PostalCode,
                        NationalID = x.NationalID,
                        MobileNo = x.MobileNo,
                        Address = x.Address,
                        CompanyRegistrationName = x.CompanyRegistrationName,
                        CustomerConnector = x.CustomerConnector,
                        CustomerField_ID = x.CustomerField_ID,
                        CustomerName = x.CustomerName,
                        Customer_ID = x.Customer_ID,
                        EconomicalNumber = x.EconomicalNumber,
                        Email = x.Email,
                        FaxNo = x.FaxNo,
                        IsActive_ID = x.Active,
                        Field = _customerService.GetFieldById(x.CustomerField_ID).CustomerField_Name,
                        IsActive = _comm.GetLiteralByInt(x.Active)
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);

            }
        }

        [HttpPost]
        [CheckSession(SubActId = 21)]
        public virtual ActionResult CustomerFieldList(DataSourceRequest command, CustomerFieldListModel model)
        {
            if (string.IsNullOrEmpty(model.CustomerField_Name) && string.IsNullOrEmpty(model.Description))
            {
                var Items = _customerService.GetAllCustomerFileds();
                var gridModel = new DataSourceResult
                {
                    Data = Items.Select(x => new CustomerFieldListModel
                    {
                        CustomerField_Name = x.CustomerField_Name,
                         Description = x.Description,
                          CustomerField_ID = x.CustomerField_ID
                    }),
                    Total = Items.Count()
                };

                return Json(gridModel);
            }
            else
            {
                //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
                var UserItems = _customerService.SearchCustomerField(model.CustomerField_Name, model.Description);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new CustomerFieldListModel
                    {
                        CustomerField_Name = x.CustomerField_Name,
                        Description = x.Description,
                        CustomerField_ID = x.CustomerField_ID
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);

            }
        }

        [HttpPost]
        [CheckSession(SubActId = 5)]
        public virtual ActionResult CustomerProductList(DataSourceRequest command, CustomerProductsListModel model)
        {
            if (true)
            {
                var Items = _customerService.GetAllCustomerProducts(model.Customer_ID);
                var gridModel = new DataSourceResult
                {
                    Data = Items.Select(x => new CustomerProductsModel
                    {
                        ProductsName = x.Tbl_Products.ProductsName,
                         Products_ID = x.Tbl_Products.Products_ID
                    }),
                    Total = Items.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 5)]
        public virtual ActionResult ProductList(DataSourceRequest command, CustomerProductsListModel model)
        {
                var GroupItems = _productService.GetAllProducts();
                var gridModel = new DataSourceResult
                {
                    Data = GroupItems.Select(x => new CustomerProductsModel
                    {
                         ProductsName = x.ProductsName,
                          Products_ID = x.Products_ID
                    }),
                    Total = GroupItems.Count()
                };

                return Json(gridModel);
        }

        //    [CheckSession]
        //    public virtual ActionResult View(int id)
        //    {

        //        var group = _groupService.GetGroupById(id);
        //        if (group == null)
        //            //No user found with the specified id
        //            return RedirectToAction("List");

        //        var model = new GroupModel
        //        {
        //            GroupName = group.GroupName,
        //            Description = group.Description,
        //            IsActive_ID = group.Active,
        //            LastUpdateUser_ID = group.LastUpdateUser_ID
        //        };

        //        return View(model);
        //    }

        //    [CheckSession]
        //    [HttpPost]
        //    public virtual ActionResult Delete(int id)
        //    {

        //        var group = _groupService.GetGroupById(id);
        //        if (group == null)
        //            //No log found with the specified id
        //            return RedirectToAction("List");

        //        _groupService.RemoveGroup(group);


        //        //SuccessNotification(_localizationService.GetResource("Admin.System.Log.Deleted"));
        //        return RedirectToAction("List");
        //    }


        [CheckSession(SubActId = 5)]
        [HttpPost]
        public ActionResult EditCustomer(CustomerModel model, string returnurl)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                if (model.Customer_ID != 0)
                {
                    // update process
                    Tbl_Customer tg = new Tbl_Customer();
                    tg.Address = model.Address;
                    tg.CompanyRegistrationName = model.CompanyRegistrationName;
                    tg.CustomerConnector = model.CustomerConnector;
                    tg.CustomerField_ID = model.CustomerField_ID;
                    tg.CustomerName = model.CustomerName;
                    tg.EconomicalNumber = model.EconomicalNumber;
                    tg.Email = model.Email;
                    tg.FaxNo = model.FaxNo;
                    tg.MobileNo = model.MobileNo;
                    tg.NationalID = model.NationalID;
                    tg.PostalCode = model.PostalCode;
                    tg.RegistrationNumber = model.RegistrationNumber;
                    tg.SubscriptionCode = model.SubscriptionCode;
                    tg.TelNo = model.TelNo;
                    tg.Active = model.IsActive_ID;
                    tg.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                    tg.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                    tg.LastUpdateTime = DateTime.Now.ToString("HH:mm");
                    
                    if (_customerService.EditCustomer(tg, model.Customer_ID, model.IsActive_ID, model.CustomerField_ID))
                    {
                        var gridModel = new DataSourceResult
                        {
                            ExtraData = new CustomerListModel
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
                            ExtraData = new CustomerListModel
                            {
                                Message = Message.OperationUnsuccessful,
                                MessageColor = "red",
                            },
                            Total = 1
                        };
                        return Json(gridModel);
                    }
                }
                else
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new CustomerListModel
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
                    ExtraData = new CustomerListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [CheckSession(SubActId = 5)]
        [HttpPost]
        public ActionResult InsertCustomer(CustomerModel model, string returnurl)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                if (model.Customer_ID == 0)
                {
                    // update process
                    Tbl_Customer tg = new Tbl_Customer();
                    tg.Address = model.Address;
                    tg.CompanyRegistrationName = model.CompanyRegistrationName;
                    tg.CustomerConnector = model.CustomerConnector;
                    tg.CustomerField_ID = model.CustomerField_ID;
                    tg.CustomerName = model.CustomerName;
                    tg.EconomicalNumber = model.EconomicalNumber;
                    tg.Email = model.Email;
                    tg.FaxNo = model.FaxNo;
                    tg.MobileNo = model.MobileNo;
                    tg.NationalID = model.NationalID;
                    tg.PostalCode = model.PostalCode;
                    tg.RegistrationNumber = model.RegistrationNumber;
                    tg.SubscriptionCode = model.SubscriptionCode;
                    tg.TelNo = model.TelNo;
                    tg.Active = model.IsActive_ID;
                    tg.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                    tg.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                    tg.LastUpdateTime = DateTime.Now.ToString("HH:mm");
                    int res = _customerService.AddNewCustomer(tg, model.Customer_ID, model.IsActive_ID, model.CustomerField_ID);
                    if (res > 0)
                    {
                        var gridModel = new DataSourceResult
                        {
                            ExtraData = new CustomerListModel
                            {
                                Message = Message.OperationSuccessful,
                                Customer_ID = res,
                                SubscriptionCode = _customerService.GetCustomerById(res).SubscriptionCode,
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
                            ExtraData = new CustomerListModel
                            {
                                Message = Message.OperationUnsuccessful,
                                MessageColor = "red",
                            },
                            Total = 1
                        };
                        return Json(gridModel);
                    }
                }
                else
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new CustomerListModel
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
                    ExtraData = new CustomerListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [CheckSession(SubActId = 21)]
        [HttpPost]
        public ActionResult InsertCustomerField(CustomerFieldListModel model, string returnurl)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                
                    // update process
                    Tbl_CustomerField tg = new Tbl_CustomerField();
                    tg.CustomerField_ID = model.CustomerField_ID;
                    tg.CustomerField_Name = model.CustomerField_Name;
                    tg.Description = model.Description;
                    tg.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                    tg.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                    tg.LastUpdateTime = DateTime.Now.ToString("HH:mm");
                    if (_customerService.AddNewCustomerField(tg))
                    {
                        var gridModel = new DataSourceResult
                        {
                            ExtraData = new CustomerListModel
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
                            ExtraData = new CustomerListModel
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
                    ExtraData = new CustomerListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

        [CheckSession(SubActId = 5)]
        [HttpPost]
        public void FinishUpCustomer(CustomerModel model, string returnurl)
        {
            try
            {
                _customerService.FinishSubmiting();
            }
            catch (Exception ex)
            {

            }
        }

       
        [HttpPost]
        public void SaveCustomerToSession(CustomerModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            session.Add("SelectedCustomerId", model.Customer_ID);
            return;
        }

        
        private IEnumerable<SelectListItem> GetFields()
        {
            try
            {
                var secs = _customerService.GetCustomerFields().Select(x => new SelectListItem
                {
                    Text = x.CustomerField_Name,
                    Value = x.CustomerField_ID.ToString(),
                });
                return new SelectList(secs, "Value", "Text");
            }
            catch (Exception ex)
            {
                var secs = new List<SelectListItem>();
                return new SelectList(secs, "Value", "Text");
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 5)]
        public ActionResult AddProductToCustomer(DataSourceRequest command, CustomerListModel model)
        {
            try
            {
                HttpSessionStateBase session = HttpContext.Session;
                int res = _customerService.AddProductToCustomer(model.Products_ID, model.Customer_ID, Convert.ToInt32(session["UserID"]));
                if (res>0)
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new CustomerListModel
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
                        ExtraData = new CustomerListModel
                        {
                            Message = Message.OperationUnsuccessful,
                            MessageColor = "red",
                        },
                        Total = 1
                    };
                    return Json(gridModel);
                }
            }
            catch(Exception ex)
            {
                var gridModel = new DataSourceResult
                {
                    ExtraData = new CustomerListModel
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
        [CheckSession(SubActId = 5)]
        public ActionResult RemoveProductFromCustomer(DataSourceRequest command, CustomerListModel model)
        {
            try
            {
                HttpSessionStateBase session = HttpContext.Session;
                int res = _customerService.RemoveProductFromCustomer(model.Products_ID, model.Customer_ID, Convert.ToInt32(session["UserID"]));
                if (res > 0)
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new CustomerListModel
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
                        ExtraData = new CustomerListModel
                        {
                            Message = Message.OperationNotAllowed,
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
                    ExtraData = new CustomerListModel
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
        [CheckSession(SubActId = 5)]
        public ActionResult AddAllProductsToCustomer(DataSourceRequest command, CustomerListModel model)
        {
            try
            {
                HttpSessionStateBase session = HttpContext.Session;
                int res = _customerService.AddAllProductsToCustomer(model.Customer_ID, Convert.ToInt32(session["UserID"]));
                if (res > 0)
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new CustomerListModel
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
                        ExtraData = new CustomerListModel
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
                    ExtraData = new CustomerListModel
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
        [CheckSession(SubActId = 5)]
        public ActionResult RemoveAllProductsFromCustomer(DataSourceRequest command, CustomerListModel model)
        {
            try
            {
                HttpSessionStateBase session = HttpContext.Session;
                int res = _customerService.RemoveAllProductsFromCustomer(model.Customer_ID, Convert.ToInt32(session["UserID"]));
                if (res > 0)
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new CustomerListModel
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
                        ExtraData = new CustomerListModel
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
                    ExtraData = new CustomerListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red",
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }
    }

}