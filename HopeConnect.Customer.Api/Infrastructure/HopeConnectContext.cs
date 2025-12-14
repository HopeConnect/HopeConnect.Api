using HopeConnect.Customer.Api.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace HopeConnect.Customer.Api.Infrastructure
{
    public class HopeConnectContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"workstation id=HopeConnectDB.mssql.somee.com;Data Source=HopeConnectDB.mssql.somee.com;Initial Catalog=HopeConnectDB;User Id = HopeConnect_SQLLogin_1; Password = wx7dvqargn;persist security info=False; Trust Server Certificate=true;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivitiy> UserActivities { get; set; }
        public DbSet<Recipient> Recipients { get; set; }    
    }
}