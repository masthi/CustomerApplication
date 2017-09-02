using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CustomerApplication.Models;
using System.Threading.Tasks;
using CustomerApplication.DAL;
using System.Web.Mvc;

namespace CustomerApplication.Controllers
{
    public class CustomerWebAPIController : ApiController
    {
        public async Task<List<Customer>> Post(Customer obj)
        {
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
            {
                DataAccessLayer dal = new DataAccessLayer();
              
                if (ModelState.IsValid)
                {
                    //insert the customer object to the database
                    dal.Customers.Add(obj);
                    dal.SaveChanges();
                }
                List<Customer> customerColl = dal.Customers.ToList<Customer>();
                return customerColl;
            });
            await Task.WhenAll(t1);

            return t1.Result;
        }

        public async Task<List<Customer>> Get()
        {
            //Read the Query string
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
            {
                var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
                string customerCode = allUrlKeyValues.SingleOrDefault(x => x.Key == "CustomerCode").Value;
                string customerName = allUrlKeyValues.SingleOrDefault(x => x.Key == "CustomerName").Value;

                DataAccessLayer dal = new DataAccessLayer();
                List<Customer> customerColl = new List<Customer>();
                if (customerName !=null)
                {
                     customerColl = (from t in dal.Customers
                                                   where t.CustomerName == customerName
                                     select t).ToList<Customer>();
                }
                else if (customerCode != null)
                {
                    customerColl = (from t in dal.Customers
                                    where t.CustomerCode == customerCode
                                    select t).ToList<Customer>();
                }
                else
                {
                    customerColl = dal.Customers.ToList<Customer>();
                }

                
                return customerColl;
            });
            await Task.WhenAll(t1);

            return t1.Result;
        }
        //public async Task<List<Customer>> Get(string CustomerName)
        //{
        //    Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
        //    {
        //        DataAccessLayer dal = new DataAccessLayer();
        //        List<Customer> customerColl = (from t in dal.Customers
        //                                       where t.CustomerName == CustomerName
        //                                       select t).ToList<Customer>();
        //        return customerColl;
        //    });
        //    await Task.WhenAll(t1);

        //    return t1.Result;
        //}

       

        public async Task<List<Customer>> Put(Customer obj)
        {
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
            {
                DataAccessLayer dal = new DataAccessLayer();
                Customer customer = (from t in dal.Customers
                                               where t.CustomerName == obj.CustomerName
                                               select t).ToList<Customer>().FirstOrDefault();
                
                customer.CustomerName = obj.CustomerName;
                customer.CustomerAmount = obj.CustomerAmount;
                dal.SaveChanges();
                List<Customer> customerColl = dal.Customers.ToList<Customer>();

                return customerColl;
            });
            await Task.WhenAll(t1);

            return t1.Result;
        }

        public async Task<List<Customer>> Delete(Customer obj)
        {
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
            {
                DataAccessLayer dal = new DataAccessLayer();
                Customer customer = (from t in dal.Customers
                                     where t.CustomerName == obj.CustomerName
                                     select t).ToList<Customer>().FirstOrDefault();

                dal.Customers.Remove(customer);
                dal.SaveChanges();
                List<Customer> customerColl = dal.Customers.ToList<Customer>();

                return customerColl;
            });
            await Task.WhenAll(t1);

            return t1.Result;
        }
    }
}
