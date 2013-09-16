using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace JDTMVC.Models
{
    public class JobsDataContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        static JobsDataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<JobsDataContext>());
        }
    }
}