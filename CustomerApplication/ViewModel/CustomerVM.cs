using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerApplication.Models;

namespace CustomerApplication.ViewModel
{
    public class CustomerVM
    {
        //Customer Model + List of Customers
        public Customer customer { get; set; }
        public List<Customer> customers { get; set; }
    }
}