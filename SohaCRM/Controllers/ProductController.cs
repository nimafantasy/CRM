using Core.Domain.Common;
using Data;
using Services;
using SohaCRM.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework;
using Web.Framework.Kendoui;

namespace SohaCRM.Controllers
{
    public class ProductController : Controller
    {

        ProductService _productService;
        Active _comm;
        UserService _User;

        public ProductController()
        {
            _productService = new ProductService();
            _comm = new Active();
            _User = new UserService();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View("Products");
        }

        [CheckSession(SubActId = 20)]
        public virtual ActionResult Products()
        {
            var model = new ProductListModel();
            return View(model);
        }

        [HttpPost]
        [CheckSession(SubActId = 20)]
        public virtual ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            if (string.IsNullOrEmpty(model.ProductName) && string.IsNullOrEmpty(model.Description))
            {
                var GroupItems = _productService.GetAllProducts();
                var gridModel = new DataSourceResult
                {
                    Data = GroupItems.Select(x => new ProductModel
                    {
                        ProductName = x.ProductsName,
                        Product_ID = x.Products_ID,
                        Description = x.Description,
                        LastUpdateUser_ID = x.LastUpdateUser_ID
                    }),
                    Total = GroupItems.Count()
                };

                return Json(gridModel);
            }
            else
            {
                var UserItems = _productService.SearchProducts(model.ProductName, model.Description);
                var gridModel = new DataSourceResult
                {
                    Data = UserItems.Select(x => new ProductModel
                    {
                        ProductName = x.ProductsName,
                        Description = x.Description,
                        Product_ID = x.Products_ID,
                        LastUpdateUser_ID = x.LastUpdateUser_ID,
                    }),
                    Total = UserItems.Count()
                };

                return Json(gridModel);
            }
        }

        //[CheckSession(SubActId = 2)]
        //public virtual ActionResult View(int id)
        //{

        //    var group = _productService.get.GetGroupById(id);
        //    if (group == null)
        //        //No user found with the specified id
        //        return RedirectToAction("List");

        //    var model = new ProductModel
        //    {
        //        GroupName = group.GroupName,
        //        Description = group.Description,
        //        IsActive_ID = group.Active,
        //        LastUpdateUser_ID = group.LastUpdateUser_ID
        //    };

        //    return View(model);
        //}

        //[CheckSession(SubActId = 2)]
        //[HttpPost]
        //public virtual ActionResult Delete(int id)
        //{

        //    var group = _productService..GetGroupById(id);
        //    if (group == null)
        //        //No log found with the specified id
        //        return RedirectToAction("List");

        //    _productService.RemoveGroup(group);


        //    //SuccessNotification(_localizationService.GetResource("Admin.System.Log.Deleted"));
        //    return RedirectToAction("List");
        //}

        [HttpPost]
        [CheckSession(SubActId = 20)]
        public ActionResult SubmitProduct(ProductListModel model)
        {
            HttpSessionStateBase session = HttpContext.Session;
            try
            {
                Tbl_Products tg = new Tbl_Products();
                tg.ProductsName = model.ProductName;
                tg.Description = model.Description;
                tg.Products_ID = model.Product_ID;
                tg.LastUpdateUser_ID = Convert.ToInt32(session["UserID"]);
                tg.LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                tg.LastUpdateTime = DateTime.Now.ToString("HH:mm");

                if (_productService.AddNewProduct(tg))
                {
                    var gridModel = new DataSourceResult
                    {
                        ExtraData = new ProductListModel
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
                        ExtraData = new ProductListModel
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
                    ExtraData = new ProductListModel
                    {
                        Message = Message.OperationUnsuccessful,
                        MessageColor = "red"
                    },
                    Total = 1
                };
                return Json(gridModel);
            }
        }

    }
}
