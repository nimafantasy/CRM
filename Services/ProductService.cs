using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Services
{
    public class ProductService
    {
        CRMEntities _context;
        public ProductService()
        {
            _context = new CRMEntities();
        }


        public List<Tbl_Products> GetAllProducts()
        {
            return _context.Tbl_Products.ToList();
        }

        public Tbl_Products GetProductById(int id)
        {
            try
            {
                return _context.Tbl_Products.First(x => x.Products_ID == id);
            }
            catch (Exception ex)
            {

                return new Tbl_Products();
            }
            //return new User();
        }

        public List<Tbl_Customer_Products> GetAllCustomerProducts()
        {
            return _context.Tbl_Customer_Products.ToList();
        }


        public List<Tbl_Hardware> GetAllCustomerHardware(int customer_id)
        {
            return _context.Tbl_Hardware.Where(x => x.Customer_ID.Equals(customer_id) && x.Status.Equals(1)).ToList();
        }

        public bool AddNewProduct(Tbl_Products product)
        {

            try
            {
                //check if user already exists
                var res = from t in _context.Tbl_Products
                          where t.Products_ID == product.Products_ID
                          select t;

                if (res.Count() == 0)
                {
                    _context.Tbl_Products.Add(product);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    Tbl_Products prod = _context.Tbl_Products.First(x => x.Products_ID == product.Products_ID);
                    prod.ProductsName = product.ProductsName;
                    prod.Description = product.Description;
                    prod.LastUpdateUser_ID = product.LastUpdateUser_ID;
                    prod.LastUpdateDate = product.LastUpdateDate;
                    prod.LastUpdateTime = product.LastUpdateTime;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //_logRepository.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "AddNewGroup", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });
                return false;
            }
        }

        public List<Tbl_Products> SearchProducts(string Name, string Description)
        {
            try
            {
                //check if user already exists
                var res = from t in _context.Tbl_Products
                          where (!string.IsNullOrEmpty(Name) && t.ProductsName.Contains(Name)) || (!string.IsNullOrEmpty(Description) && t.Description.Contains(Description))
                          select t;
                return res.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_Products>();
            }
        }
    }


}
