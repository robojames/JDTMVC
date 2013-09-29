using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace JDTMVC.Models
{
    public class JobsDataContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        static JobsDataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<JobsDataContext>());
        }

        protected override DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

            if (entityEntry.Entity is Job && entityEntry.State == System.Data.EntityState.Added)
            {
                Job job = entityEntry.Entity as Job;

                if (Jobs.Where(x => x.name == job.name && x.curr_Revision==job.curr_Revision).Count() > 0)
                {
                    result.ValidationErrors.Add(new DbValidationError("name", "Job number and revision must be unique."));
                    Debug.WriteLine("Error thrown:  Jobs need to be unique");
                }
            }

            if (result.ValidationErrors.Count > 0)
            {
                return result;
            }
            else
            {
                Debug.WriteLine("No errors thrown here");
                return base.ValidateEntity(entityEntry, items);
            }
        }
    }
}