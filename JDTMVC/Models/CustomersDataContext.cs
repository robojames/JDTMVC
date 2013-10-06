using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace JDTMVC.Models
{
    public class CustomersDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        static CustomersDataContext()
        {
        }

    }
}