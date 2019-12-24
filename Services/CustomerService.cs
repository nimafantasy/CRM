using Core.Domain.Common;
using Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService
    {
        CRMEntities _context;
        ObjectContext conn;
        DbTransaction trns;
        Active _active;

        public CustomerService()
        {
            _context = new CRMEntities();
            _active = new Active();
            conn = ((IObjectContextAdapter)_context).ObjectContext;
            
        }


        public bool EditCustomer(Tbl_Customer customer, int customer_id, int active_id, int field_id)
        {
            try
            {
                if (_context.Tbl_Customer.Any(x => x.Customer_ID == customer_id))
                {
                    // user already exists
                    Tbl_Customer ToBeUpdatedCustomer = _context.Tbl_Customer.First(x => x.Customer_ID == customer_id);
                    ToBeUpdatedCustomer.Address = customer.Address;
                    ToBeUpdatedCustomer.CompanyRegistrationName = customer.CompanyRegistrationName;
                    ToBeUpdatedCustomer.CustomerConnector = customer.CustomerConnector;
                    ToBeUpdatedCustomer.CustomerName = customer.CustomerName;
                    ToBeUpdatedCustomer.EconomicalNumber = customer.EconomicalNumber;
                    ToBeUpdatedCustomer.Active = customer.Active;
                    ToBeUpdatedCustomer.CustomerField_ID = customer.CustomerField_ID;
                    ToBeUpdatedCustomer.Email = customer.Email;
                    ToBeUpdatedCustomer.FaxNo = customer.FaxNo;
                    ToBeUpdatedCustomer.MobileNo = customer.MobileNo;
                    ToBeUpdatedCustomer.NationalID = customer.NationalID;
                    ToBeUpdatedCustomer.PostalCode = customer.PostalCode;
                    ToBeUpdatedCustomer.RegistrationNumber = customer.RegistrationNumber;
                    //ToBeUpdatedCustomer.SubscriptionCode = customer.SubscriptionCode;
                    ToBeUpdatedCustomer.TelNo = customer.TelNo;
                    ToBeUpdatedCustomer.LastUpdateDate = customer.LastUpdateDate;
                    ToBeUpdatedCustomer.LastUpdateTime = customer.LastUpdateTime;
                    ToBeUpdatedCustomer.LastUpdateUser_ID = customer.LastUpdateUser_ID;
                    if (_context.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public int AddNewCustomer(Tbl_Customer customer, int customer_id, int active_id, int field_id)
        {
            try
            {
                    
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);

                Tbl_Customer ToBeInsertedCustomer = new Tbl_Customer();
                ToBeInsertedCustomer.Address = customer.Address;
                ToBeInsertedCustomer.CompanyRegistrationName = customer.CompanyRegistrationName;
                ToBeInsertedCustomer.CustomerConnector = customer.CustomerConnector;
                ToBeInsertedCustomer.CustomerName = customer.CustomerName;
                ToBeInsertedCustomer.EconomicalNumber = customer.EconomicalNumber;
                ToBeInsertedCustomer.Active = customer.Active;
                ToBeInsertedCustomer.CustomerField_ID = customer.CustomerField_ID;
                ToBeInsertedCustomer.Email = customer.Email;
                ToBeInsertedCustomer.FaxNo = customer.FaxNo;
                ToBeInsertedCustomer.MobileNo = customer.MobileNo;
                ToBeInsertedCustomer.NationalID = customer.NationalID;
                ToBeInsertedCustomer.PostalCode = customer.PostalCode;
                ToBeInsertedCustomer.RegistrationNumber = customer.RegistrationNumber;
                ToBeInsertedCustomer.SubscriptionCode = r.ToString();
                ToBeInsertedCustomer.TelNo = customer.TelNo;
                ToBeInsertedCustomer.LastUpdateDate = customer.LastUpdateDate;
                ToBeInsertedCustomer.LastUpdateTime = customer.LastUpdateTime;
                ToBeInsertedCustomer.LastUpdateUser_ID = customer.LastUpdateUser_ID;
                
                _context.Tbl_Customer.Add(ToBeInsertedCustomer);
                _context.SaveChanges();

                return ToBeInsertedCustomer.Customer_ID;

            }
            catch (Exception ex)
            {
                return 0;
            }
                
        }

        public bool AddNewCustomerField(Tbl_CustomerField customerfield)
        {
            try
            {
                //check if user already exists
                var res = from t in _context.Tbl_CustomerField
                          where t.CustomerField_ID == customerfield.CustomerField_ID
                          select t;

                if (res.Count() == 0)
                {
                    _context.Tbl_CustomerField.Add(customerfield);
                }
                else
                {
                    Tbl_CustomerField prod = _context.Tbl_CustomerField.First(x => x.CustomerField_ID == customerfield.CustomerField_ID);
                    prod.CustomerField_Name = customerfield.CustomerField_Name;
                    prod.Description = customerfield.Description;
                    prod.LastUpdateUser_ID = customerfield.LastUpdateUser_ID;
                    prod.LastUpdateDate = customerfield.LastUpdateDate;
                    prod.LastUpdateTime = customerfield.LastUpdateTime;
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

        public int FinishSubmiting()
        {
            conn.Connection.Open();
            trns.Commit();
            conn.Connection.Close();
            //conn.Connection.Close();
            return 5;
        }

        public List<Tbl_Customer> SearchCustomers(string Address, string CompanyRegistrationName, string CustomerConnector, string CustomerName, string EconomicalNumber, string Email, string FaxNo, string MobileNo, string NationalID, string PostalCode, string RegistrationNumber, string SubscriptionCode, string TelNo, int customerfield_id, int isactive_id)
        {
            try
            {
                return _context.Tbl_Customer.Where(x => x.Address.Contains(Address) || x.CompanyRegistrationName.Contains(CompanyRegistrationName) || x.CustomerConnector.Contains(CustomerConnector) || x.CustomerName.Contains(CustomerName) || x.EconomicalNumber.Contains(EconomicalNumber) || x.Email.Contains(Email) || x.FaxNo.Contains(FaxNo) || x.MobileNo.Contains(MobileNo) || x.NationalID.Contains(NationalID) || x.PostalCode.Contains(PostalCode) || x.RegistrationNumber.Contains(RegistrationNumber) || x.SubscriptionCode.Contains(SubscriptionCode) || x.TelNo.Contains(TelNo) || x.CustomerField_ID.Equals(customerfield_id) || x.Active.Equals(isactive_id)).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_Customer>();
            }
        }

        public List<Tbl_CustomerField> SearchCustomerField(string name, string desc)
        {
            try
            {
                return _context.Tbl_CustomerField.Where(x => x.CustomerField_Name.Contains(name) || x.Description.Contains(desc)).ToList();
            }
            catch (Exception ex)
            {
                //_logService.Insert(new Log() { Type = LogType.Error, Priority = LogPriority.High, Description = ex.Message, PageUrl = "SearchUsers", CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm") });

                return new List<Tbl_CustomerField>();
            }
        }


        public List<Tbl_Customer> GetAllCustomers()
        {
            try
            {
                return _context.Tbl_Customer.ToList();
            }
            catch(Exception ex)
            {
                return new List<Tbl_Customer>();
            }

        }

        public List<Tbl_CustomerField> GetAllCustomerFileds()
        {
            try
            {
                return _context.Tbl_CustomerField.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_CustomerField>();
            }

        }

        public List<Tbl_Customer_Products> GetAllCustomerProducts(int customer_id)
        {
            try
            {
                return _context.Tbl_Customer_Products.Where(o => o.Customer_ID == customer_id).ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_Customer_Products>();
            }

        }


        public List<Tbl_CustomerField> GetCustomerFields()
        {
            try
            {
                return _context.Tbl_CustomerField.ToList();
            }
            catch (Exception ex)
            {
                return new List<Tbl_CustomerField>();
            }

        }

        public Tbl_CustomerField GetFieldById(int id)
        {
            try
            {
                return _context.Tbl_CustomerField.First(x => x.CustomerField_ID == id);
            }
            catch (Exception ex)
            {
                return new Tbl_CustomerField();
            }

        }

        public Tbl_Customer GetCustomerById(int id)
        {
            try
            {
                return _context.Tbl_Customer.First(o => o.Customer_ID == id);
            }
            catch (Exception ex)
            {
                return new Tbl_Customer();
            }

        }

        public bool EditCustomer()
        {
            return true;
        }

        public bool RemoveCustomer(Tbl_Customer customer)
        {
            if(_context.Tbl_Customer.Any(x => x.Customer_ID == customer.Customer_ID))
            {
                _context.Tbl_Customer.Remove(_context.Tbl_Customer.Where(x => x.Customer_ID == customer.Customer_ID).FirstOrDefault());
                _context.SaveChanges();
            }
            return true;
        }

        public int AddProductToCustomer(int product_id, int customer_id, int acting_user_id)
        {
            try
            {
                if (!_context.Tbl_Customer_Products.Any(o => o.Customer_ID == customer_id && o.Products_ID == product_id))
                {
                    _context.Tbl_Customer_Products.Add(new Tbl_Customer_Products() { Customer_ID = customer_id, Products_ID = product_id, LastUpdateTime = DateTime.Now.ToString("hh:mm"), LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd"), LastUpdateUser_ID = acting_user_id });
                    return _context.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public int RemoveProductFromCustomer(int product_id, int customer_id, int acting_user_id)
        {
            try
            {

                Tbl_Customer_Products entity = _context.Tbl_Customer_Products.Where(x => x.Customer_ID == customer_id && x.Products_ID == product_id).FirstOrDefault();
                if (_context.Tbl_Customer_Products.Where(y => y.Customer_ID.Equals(customer_id)).Count() > 1)
                    _context.Tbl_Customer_Products.Remove(entity);
                else
                    return 0;
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int AddAllProductsToCustomer(int customer_id, int acting_user_id)
        {
            try
            {
                foreach (Tbl_Products sa in _context.Tbl_Products)
                {
                    if (!_context.Tbl_Customer_Products.Any(o => o.Customer_ID == customer_id && o.Products_ID == sa.Products_ID))
                        _context.Tbl_Customer_Products.Add(new Tbl_Customer_Products() { Products_ID = sa.Products_ID, Customer_ID = customer_id, LastUpdateTime = DateTime.Now.ToString("hh:mm"), LastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd"), LastUpdateUser_ID = acting_user_id });
                }
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public int RemoveAllProductsFromCustomer(int customer_id, int acting_user_id)
        {
            try
            {

                var entity = _context.Tbl_Customer_Products.Where(x => x.Customer_ID == customer_id);
                foreach (Tbl_Customer_Products subac in entity)
                {
                    _context.Tbl_Customer_Products.Remove(subac);
                }
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
