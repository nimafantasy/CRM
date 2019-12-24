using Core.Domain.Common;
using Data;
using Services;
using SohaCRM.Models.Accounting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class AccountingController : Controller
    {
        // GET: Accounting
        AccountingService _accService;
        CustomerService _customerService;
        HardwareService _hardwareService;
        SoftwareService _softwareService;

        public AccountingController()
        {
            _accService = new AccountingService();
            _customerService = new CustomerService();
            _hardwareService = new HardwareService();
            _softwareService = new SoftwareService();
        }
        public ActionResult PreInvoice()
        {
            var model = new PreInvoiceListModel();
            model.PreInvoiceType = GetPreInvoiceTypes();
            
            return View(model);
        }

        public ActionResult Payment()
        {
            var model = new PaymentListModel();
            

            return View(model);
        }

        [HttpPost]
        //[CheckSession(SubActId = 8)]
        public virtual ActionResult PreInvoiceList(DataSourceRequest command, PreInvoiceListModel model)
        {
            if (model != null)
            {
                if (model.Check_Confirmed && !model.Check_NotConfirmed)
                    model.ConfirmationStatus = 1;
                else if (!model.Check_Confirmed && model.Check_NotConfirmed)
                    model.ConfirmationStatus = 2;
                else
                    model.ConfirmationStatus = 0;

                if (model.Check_Sent && !model.Check_NotSent)
                    model.SendingStatus = 1;
                else if (!model.Check_Sent && model.Check_NotSent)
                    model.SendingStatus = 2;
                else
                    model.SendingStatus = 0;
            }
            if (string.IsNullOrEmpty(model.Description) && model.SendingStatus == 0 && model.PreInvoiceType_ID == 0 && string.IsNullOrEmpty(model.IssueDate) && string.IsNullOrEmpty(model.PreInvoiceNumber))
            {
                var hardwareItems = _accService.GetAllPreInvoice();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new PreInvoiceModel
                    {
                         IssueDate = x.IssueDate,
                         Customer_ID = x.Customer_ID,
                          SendingStatus = x.SendingStatus,
                          ConfirmationStatus = x.ConfirmationStatus,
                           PreInvoiceNumber = x.PreInvoiceNumber,
                            PreInvoiceType = _accService.GetPreInvoiceTypeById(x.PreInvoiceType_ID).PreInvoiceType,
                             PreInvoiceType_ID = x.PreInvoiceType_ID,
                              PreInvoice_ID = x.PreInvoice_ID,
                            Description = x.Description,
                        Check_Sent = x.SendingStatus == 1 ? true : false,
                        Check_NotSent = x.SendingStatus == 2 ? true : false,
                        Status = x.SendingStatus == 1 ? "ارسال شده" : x.SendingStatus == 2 ? "ارسال نشده" : "مشخص نشده",
                        ConfStatus = x.ConfirmationStatus == 1 ? "تایید شده" : x.ConfirmationStatus == 2 ? "تایید نشده" : "مشخص نشده",
                        Check_Confirmed = x.ConfirmationStatus  == 1 ? true : false,
                        Check_NotConfirmed = x.ConfirmationStatus == 2 ? true : false,
                        File = Convert.ToBase64String(x.PreInvoiceUpload),
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName
                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _accService.SearchPreInvoice(model.PreInvoiceNumber, model.IssueDate,model.PreInvoiceType_ID, model.SendingStatus, Convert.ToInt32(model.ConfirmationStatus), model.Description);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new PreInvoiceModel
                    {
                        IssueDate = x.IssueDate,
                        Customer_ID = x.Customer_ID,
                        SendingStatus = x.SendingStatus,
                        ConfirmationStatus = x.ConfirmationStatus,
                        PreInvoiceNumber = x.PreInvoiceNumber,
                        PreInvoiceType = _accService.GetPreInvoiceTypeById(x.PreInvoiceType_ID).PreInvoiceType,
                        PreInvoiceType_ID = x.PreInvoiceType_ID,
                        PreInvoice_ID = x.PreInvoice_ID,
                        Description = x.Description,
                        Check_Sent = x.SendingStatus == 1 ? true : false,
                        Check_NotSent = x.SendingStatus == 2 ? true : false,
                        Status = x.SendingStatus == 1 ? "ارسال شده" : x.SendingStatus == 2 ? "ارسال نشده" : "مشخص نشده",
                        ConfStatus = x.ConfirmationStatus == 1 ? "تایید شده" : x.ConfirmationStatus == 2 ? "تایید نشده" : "مشخص نشده",
                        Check_Confirmed = x.ConfirmationStatus == 1 ? true : false,
                        Check_NotConfirmed = x.ConfirmationStatus == 2 ? true : false,
                        File = Convert.ToBase64String(x.PreInvoiceUpload),
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 8)]
        public ActionResult SubmitPreInvoice(DataSourceRequest command, PreInvoiceListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                

                Tbl_PreInvoice tu = new Tbl_PreInvoice();
                tu.IssueDate = model.IssueDate;
                tu.PreInvoiceNumber = model.PreInvoiceNumber;
                tu.PreInvoiceType_ID = model.PreInvoiceType_ID;
                tu.Customer_ID = model.Customer_ID;
                if (model.File != null)
                {
                    MemoryStream target = new MemoryStream();
                    model.File.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    tu.PreInvoiceUpload = data;
                }

                tu.Description = model.Description;
                tu.PreInvoice_ID = model.PreInvoice_ID;
                if (model.Check_Sent && !model.Check_NotSent)
                    tu.SendingStatus = 1;
                else if (!model.Check_Sent && model.Check_NotSent)
                    tu.SendingStatus = 2;
                else
                    tu.SendingStatus = 0;
                if (model.Check_Confirmed && !model.Check_NotConfirmed)
                    tu.ConfirmationStatus = 1;
                else if (!model.Check_Confirmed && model.Check_NotConfirmed)
                    tu.ConfirmationStatus = 2;
                else
                    tu.ConfirmationStatus = 0;

                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_accService.AddNewPreInvoice(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new PreInvoiceModel
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
                        ExtraData = new PreInvoiceModel
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
                    ExtraData = new PreInvoiceModel
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


        private IEnumerable<SelectListItem> GetPreInvoiceTypes()
        {
            try
            {
                var secs = _accService.GetAllPreInvoiceTypes().Select(x => new SelectListItem
                {
                    Text = x.PreInvoiceType,
                    Value = x.PreInvoiceType_ID.ToString(),
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
        //[CheckSession(SubActId = 8)]
        public virtual ActionResult PaymentList(DataSourceRequest command, PaymentListModel model)
        {
            if (model.Customer_ID == 0 && string.IsNullOrEmpty(model.PreInvoiceNumber) && string.IsNullOrEmpty(model.PaymentDate) && model.PaymentAmount == 0 && string.IsNullOrEmpty(model.Description))
            {
                var hardwareItems = _accService.GetAllPayment();
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new PaymentModel
                    {
                         PaymentAmount = x.PaymentAmount,
                          PaymentDate = x.PaymentDate,
                           Payment_ID = x.Payment_ID,
                        Customer_ID = x.Customer_ID,
                        PreInvoiceNumber = x.PreInvoiceNumber,
                        Description = x.Description,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName
                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var hardwareItems = _accService.SearchPayment(model.Customer_ID, model.PreInvoiceNumber, model.PaymentDate, model.PaymentAmount, model.Description);
                var gridModel = new DataSourceResult
                {

                    Data = hardwareItems.Select(x => new PaymentModel
                    {
                        PaymentAmount = x.PaymentAmount,
                        PaymentDate = x.PaymentDate,
                        Payment_ID = x.Payment_ID,
                        Customer_ID = x.Customer_ID,
                        PreInvoiceNumber = x.PreInvoiceNumber,
                        Description = x.Description,
                        CustomerName = _customerService.GetCustomerById(x.Customer_ID).CustomerName


                    }),
                    Total = hardwareItems.Count()
                };

                return Json(gridModel);
            }
        }

        [HttpPost]
        [CheckSession(SubActId = 8)]
        public ActionResult SubmitPayment(DataSourceRequest command, PaymentListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {


                Tbl_Payment tu = new Tbl_Payment();
                tu.Customer_ID = model.Customer_ID;
                tu.Description = model.Description;
                tu.PaymentAmount = model.PaymentAmount;
                tu.PaymentDate = model.PaymentDate;
                tu.Payment_ID = model.Payment_ID;
                tu.PreInvoiceNumber = model.PreInvoiceNumber;
                

                tu.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tu.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tu.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_accService.AddNewPayment(tu))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new PaymentModel
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
                        ExtraData = new PaymentModel
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
                    ExtraData = new PaymentModel
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
        //[CheckSession]
        public virtual ActionResult CustomerSearch(DataSourceRequest command, PreInvoiceListModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CustomerNameInSearch) && string.IsNullOrEmpty(model.MobileNo) && string.IsNullOrEmpty(model.SubscriptionCode) && string.IsNullOrEmpty(model.TelNo))
                {
                    var customer = _customerService.GetAllCustomers(); //_hardwareService.SearchCustomers(model.CustomerNameInSearch, model.MobileNo, model.SubscriptionCode, model.TelNo);
                    var hardwareRequests = _hardwareService.GetAllHardware();
                    var softwareRequests = _softwareService.GetAllSoftware();
                    var softwareChangeRequests = _softwareService.GetAllSoftwareChange();

                    //var CustomerProductItems = _productService.GetAllCustomerProducts();

                    var query1 = from cus in customer
                                 join hr in hardwareRequests
                                      on cus.Customer_ID equals hr.Customer_ID
                                 where hr.Status.Equals(0)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     hr.Description,
                                     hr.Amount
                                 };

                    var query2 = from cus in customer
                                 join sr in softwareRequests
                                      on cus.Customer_ID equals sr.Customer_ID
                                 where sr.Status.Equals(0)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     sr.Description,
                                     sr.Amount
                                 };

                    var query3 = from cus in customer
                                 join scr in softwareRequests
                                      on cus.Customer_ID equals scr.Customer_ID
                                 where scr.Status.Equals(0)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     scr.Description,
                                     scr.Amount
                                 };



                    var gridModel = new DataSourceResult
                    {

                        Data = query1.Select(x => new PreInvoiceModel
                        {

                            Customer_ID = x.Customer_ID,
                            CustomerNameInSearch = x.CustomerName,
                            ReqType = "خرید سخت‌افزار‌",
                            Amount = x.Amount.ToString(),
                            ReqDesc = x.Description

                        }).Concat(query2.Select(y => new PreInvoiceModel
                        {
                            Customer_ID = y.Customer_ID,
                            CustomerNameInSearch = y.CustomerName,
                            ReqType = "خرید نرم‌افزار‌",
                            Amount = y.Amount.ToString(),
                            ReqDesc = y.Description
                        }).Concat(query3.Select(z => new PreInvoiceModel
                        {
                            Customer_ID = z.Customer_ID,
                            CustomerNameInSearch = z.CustomerName,
                            ReqType = "تغییر نرم‌افزار‌",
                            Amount = z.Amount.ToString(),
                            ReqDesc = z.Description
                        }))),
                        Total = query1.Count() + query2.Count() + query3.Count()
                    };

                    return Json(gridModel);
                }
                else
                {
                    //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
                    var customer = _hardwareService.SearchCustomers(model.CustomerNameInSearch, model.MobileNo, model.SubscriptionCode, model.TelNo);
                    var hardwareRequests = _hardwareService.GetHardwareOfCusomer(customer.FirstOrDefault().Customer_ID);
                    var softwareRequests = _softwareService.GetSoftwareOfCusomer(customer.FirstOrDefault().Customer_ID);
                    var softwareChangeRequests = _softwareService.GetSoftwareChangeOfCusomer(customer.FirstOrDefault().Customer_ID);

                    //var CustomerProductItems = _productService.GetAllCustomerProducts();

                    var query1 = from cus in customer
                                 join hr in hardwareRequests
                                      on cus.Customer_ID equals hr.Customer_ID
                                 where hr.Status.Equals(0)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     hr.Description,
                                     hr.Amount
                                 };

                    var query2 = from cus in customer
                                 join sr in softwareRequests
                                      on cus.Customer_ID equals sr.Customer_ID
                                 where sr.Status.Equals(0)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     sr.Description,
                                     sr.Amount
                                 };

                    var query3 = from cus in customer
                                 join scr in softwareRequests
                                      on cus.Customer_ID equals scr.Customer_ID
                                 where scr.Status.Equals(0)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     scr.Description,
                                     scr.Amount
                                 };



                    var gridModel = new DataSourceResult
                    {

                        Data = query1.Select(x => new PreInvoiceModel
                        {

                            Customer_ID = x.Customer_ID,
                            CustomerNameInSearch = x.CustomerName,
                            ReqType = "خرید سخت‌افزار‌",
                            Amount = x.Amount.ToString(),
                            ReqDesc = x.Description

                        }).Concat(query2.Select(y => new PreInvoiceModel
                        {
                            Customer_ID = y.Customer_ID,
                            CustomerNameInSearch = y.CustomerName,
                            ReqType = "خرید نرم‌افزار‌",
                            Amount = y.Amount.ToString(),
                            ReqDesc = y.Description
                        }).Concat(query3.Select(z => new PreInvoiceModel
                        {
                            Customer_ID = z.Customer_ID,
                            CustomerNameInSearch = z.CustomerName,
                            ReqType = "تغییر نرم‌افزار‌",
                            Amount = z.Amount.ToString(),
                            ReqDesc = z.Description
                        }))),
                        Total = query1.Count() + query2.Count() + query3.Count()
                    };

                    return Json(gridModel);
                }
            }
            catch (Exception ex)
            {
                List<PreInvoiceModel> emptylist = new List<PreInvoiceModel>();
                return Json(new DataSourceResult() { Data = emptylist,Total = 0 });
            }
        }

        [HttpPost]
        //[CheckSession]
        public virtual ActionResult CustomerSearch2(DataSourceRequest command, PreInvoiceListModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.CustomerNameInSearch) && string.IsNullOrEmpty(model.MobileNo) && string.IsNullOrEmpty(model.SubscriptionCode) && string.IsNullOrEmpty(model.TelNo))
                {
                    var customer = _customerService.GetAllCustomers(); //_hardwareService.SearchCustomers(model.CustomerNameInSearch, model.MobileNo, model.SubscriptionCode, model.TelNo);
                    var hardwareRequests = _hardwareService.GetAllHardware();
                    var softwareRequests = _softwareService.GetAllSoftware();
                    var softwareChangeRequests = _softwareService.GetAllSoftwareChange();
                    var preInvoices = _accService.GetAllPreInvoice();
                    //var CustomerProductItems = _productService.GetAllCustomerProducts();

                    var query1 = from cus in customer
                                 join p in preInvoices
                                      on cus.Customer_ID equals p.Customer_ID
                                 where !p.SendingStatus.Equals(1)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     p.Description,
                                     p.PreInvoiceNumber,
                                     p.SendingStatus,
                                     p.ConfirmationStatus
                                 };


                    var gridModel = new DataSourceResult
                    {

                        Data = query1.Select(x => new PaymentModel
                        {

                            Customer_ID = x.Customer_ID,
                            CustomerNameInSearch = x.CustomerName,
                            PreInvoiceNumber = x.PreInvoiceNumber,
                            SendingStatus = x.SendingStatus == 0 ? "نامشخص" : x.SendingStatus == 1 ? "ارسال شده" : x.SendingStatus == 2 ? "ارسال نشده" : "نامشخص",
                            ConfirmationStatus = x.ConfirmationStatus == 1 ? "تایید شده" : x.ConfirmationStatus == 2 ? "تایید نشده" : "مشخص نشده",

                        }),
                        Total = query1.Count()
                    };

                    return Json(gridModel);
                }
                else
                {
                    //var UserItems = _groupService.SearchCustomer(model.GroupName, model.Description);
                    var customer = _hardwareService.SearchCustomers(model.CustomerNameInSearch, model.MobileNo, model.SubscriptionCode, model.TelNo);


                    var preInvoices = _accService.GetAllPreInvoice();
                    //var CustomerProductItems = _productService.GetAllCustomerProducts();

                    var query1 = from cus in customer
                                 join p in preInvoices
                                      on cus.Customer_ID equals p.Customer_ID
                                 where !p.SendingStatus.Equals(1)
                                 select new
                                 {
                                     cus.Customer_ID,
                                     cus.CustomerName,
                                     p.Description,
                                     p.PreInvoiceNumber,
                                     p.SendingStatus,
                                     p.ConfirmationStatus
                                 };


                    var gridModel = new DataSourceResult
                    {

                        Data = query1.Select(x => new PaymentModel
                        {

                            Customer_ID = x.Customer_ID,
                            CustomerNameInSearch = x.CustomerName,
                            PreInvoiceNumber = x.PreInvoiceNumber,
                            SendingStatus = x.SendingStatus == 0 ? "نامشخص" : x.SendingStatus == 1 ? "ارسال شده" : x.SendingStatus == 2 ? "ارسال نشده" : "نامشخص",
                            ConfirmationStatus = x.ConfirmationStatus == 1 ? "تایید شده" : x.ConfirmationStatus == 2 ? "تایید نشده" : "مشخص نشده",

                        }),
                        Total = query1.Count()
                    };

                    return Json(gridModel);
                }
            }
            catch (Exception ex)
            {
                List<PreInvoiceModel> emptylist = new List<PreInvoiceModel>();
                return Json(new DataSourceResult() { Data = emptylist, Total = 0 });
            }
        }
    }
}