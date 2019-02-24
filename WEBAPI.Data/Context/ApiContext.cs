using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.Data.Model;

namespace WEBAPI.Data.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext()
        : base("ApiContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // one-to-many

            modelBuilder.Entity<Article>()
            .HasRequired<User>(x => x.User)
            .WithMany(x => x.Articles)
            .HasForeignKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
