using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;

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

                if (Jobs.Where(x => x.name == job.name).Count() > 0)
                {
                    result.ValidationErrors.Add(new DbValidationError("name", "Job number must be unique."));
                }
            }

            if (result.ValidationErrors.Count > 0)
            {
                return result;
            }
            else
            {
                return base.ValidateEntity(entityEntry, items);
            }
        }
    }
}