using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerApplication.Models;
using CustomerApplication.DAL;
using CustomerApplication.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApplication.Controllers
{
    public class CustomerBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpContextBase obj = controllerContext.HttpContext;
            CustomerVM custVMObj = new CustomerVM();
            Customer custObj = new Customer { CustomerCode = obj.Request.Form["customer.CustomerCode"],
                CustomerName = obj.Request.Form["customer.CustomerName"] };

            custVMObj.customer = custObj;
            return custVMObj;
        }
    }
    //[Authorize]
    public class CustomerController : AsyncController
    {

        // GET: Customer
        public ActionResult Load()
        {
            Customer obj = new Customer { CustomerCode = "1001", CustomerName = "Masthan Rao" };

            return View("Customer", obj);
        }

        public ActionResult Enter()
        {
            CustomerVM obj = new CustomerVM();
            obj.customer = new Customer();
            //CustomerDAL dal = new CustomerDAL();
            //List<Customer> customerColl = dal.Customers.ToList<Customer>();
            //obj.customers = customerColl;
            return View("EnterCustomer", obj);
        }

        public async Task<ActionResult> Submit(Customer obj)//[ModelBinder(typeof(CustomerBinder))]
                                                            //CustomerVM custVM)
        {
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
           {
               DataAccessLayer dal = new DataAccessLayer();
               //Customer obj = new Customer();
               //obj.CustomerName = Request.Form["Customer.CustomerName"];
               //obj.CustomerCode = Request.Form["Customer.CustomerCode"];
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

            return Json(t1.Result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> SearchCustomer()
        {
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
            {
                CustomerVM obj = new CustomerVM();
                obj.customer = new Customer();
                DataAccessLayer dal = new DataAccessLayer();
                string str = Request.Form["customer.CustomerName"].ToString();
                List<Customer> customerColl = (from x in dal.Customers
                                               where x.CustomerName == str
                                               select x).ToList<Customer>();
                return customerColl;
            });
            await Task.WhenAll(t1);
            //obj.customers = customerColl;
            //return View("SearchCustomer", obj);
            return Json(t1.Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EnterSearch()
        {
            CustomerVM obj = new CustomerVM();
            obj.customers = new List<Customer>();
            return View("SearchCustomer", obj);
        }

        public async Task<ActionResult> getCustomers()
        {
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
            {
                DataAccessLayer dal = new DataAccessLayer();
                List<Customer> customerColl = dal.Customers.ToList<Customer>();
                return customerColl;
            });
            await Task.WhenAll(t1);

            return Json(t1.Result, JsonRequestBehavior.AllowGet);
        }
        [ActionName("getCustomerByName")]
        public async Task<ActionResult> getCustomers(Customer obj)
        {
            Task<List<Customer>> t1 = Task.Factory.StartNew<List<Customer>>(() =>
             {
                 DataAccessLayer dal = new DataAccessLayer();
                 List<Customer> customerColl = (from x in dal.Customers
                                                where x.CustomerName == obj.CustomerName
                                                select x).ToList<Customer>();
                 return customerColl;
             });
            await Task.WhenAll(t1);
            
            return Json(t1.Result, JsonRequestBehavior.AllowGet);
        }
    }
}