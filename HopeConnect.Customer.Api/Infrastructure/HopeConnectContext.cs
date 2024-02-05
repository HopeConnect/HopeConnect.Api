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
        public DbSet<Food> Foods { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<UserActivitiy> UserActivities { get; set; }
    }
    public class SampleDataSeeder
    {
        public static void Seed(HopeConnectContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    FullName = "John Doe",
                });
            }
            if (!context.Foods.Any())
            {
                context.Foods.AddRange(
                    new Food
                    {
                        ImageUrl = "/images/food1.jpg",
                        Title = "Food for Family",
                        Name = "John Doe",
                        Location = "New York, USA",
                        Description = "Family of 4 in need of groceries"
                    },
                    new Food
                    {
                        ImageUrl = "/images/food2.jpg",
                        Title = "Meal Support",
                        Name = "Jane Doe",
                        Location = "London, UK",
                        Description = "Individual seeking meal support"
                    },
                    new Food
                    {
                        ImageUrl = "/images/food3.jpg",
                        Title = "Emergency Food",
                        Name = "Smith",
                        Location = "Mumbai, India",
                        Description = "Urgent need for food due to job loss"
                    },
                    new Food
                    {
                        ImageUrl = "/images/food4.jpg",
                        Title = "Community Kitchen",
                        Name = "Alice",
                        Location = "Berlin, Germany",
                        Description = "Support for a community kitchen project"
                    },
                    new Food
                    {
                        ImageUrl = "/images/food5.jpg",
                        Title = "Vegetarian Meals",
                        Name = "David",
                        Location = "San Francisco, USA",
                        Description = "Vegetarian meals for a local shelter"
                    });
            }
            if (!context.Clothes.Any())
            {
                context.Clothes.AddRange(
                    new Clothes
                    {
                        ImageUrl = "/images/clothes1.jpg",
                        Title = "Winter Clothing",
                        Name = "Emily",
                        Location = "Toronto, Canada",
                        Description = "Warm clothes needed for winter"
                    },
                    new Clothes
                    {
                        ImageUrl = "/images/clothes2.jpg",
                        Title = "Kids Clothing",
                        Name = "Rodriguez",
                        Location = "Mexico City, MX",
                        Description = "Clothing for children in need"
                    },
                    new Clothes
                    {
                        ImageUrl = "/images/clothes3.jpg",
                        Title = "Professional Attire",
                        Name = "Alex",
                        Location = "Berlin, Germany",
                        Description = "Business attire needed for job interview"
                    },
                    new Clothes
                    {
                        ImageUrl = "/images/clothes4.jpg",
                        Title = "Casual Wear",
                        Name = "Sophia",
                        Location = "Sydney, Australia",
                        Description = "Casual clothing for a local shelter"
                    },
                    new Clothes
                    {
                        ImageUrl = "/images/clothes5.jpg",
                        Title = "Sports Gear",
                        Name = "Michael",
                        Location = "New Delhi, India",
                        Description = "Sports clothing for a youth sports program"
                    });
            }
            if (!context.Educations.Any())
            {
                context.Educations.AddRange(
                    new Education
                    {
                        ImageUrl = "/images/education1.jpg",
                        Title = "Educational Materials",
                        Name = "Sara",
                        Location = "Sydney, Australia",
                        Description = "School supplies needed for online learning"
                    },
                    new Education
                    {
                        ImageUrl = "/images/education2.jpg",
                        Title = "Scholarship Support",
                        Name = "Ahmed",
                        Location = "Cairo, Egypt",
                        Description = "Financial support for university studies"
                    },
                    new Education
                    {
                        ImageUrl = "/images/education3.jpg",
                        Title = "Coding Bootcamp Fee",
                        Name = "Maria",
                        Location = "San Francisco, USA",
                        Description = "Support for coding bootcamp registration"
                    },
                    new Education
                    {
                        ImageUrl = "/images/education4.jpg",
                        Title = "Language Learning",
                        Name = "Hiroshi",
                        Location = "Tokyo, Japan",
                        Description = "Language learning materials for a community program"
                    },
                    new Education
                    {
                        ImageUrl = "/images/education5.jpg",
                        Title = "STEM Education",
                        Name = "Sophie",
                        Location = "Berlin, Germany",
                        Description = "Materials for a STEM education initiative"
                    });
            }
            if (!context.Accommodations.Any())
            {
                context.Accommodations.AddRange(
                    new Accommodation
                    {
                        ImageName = "/images/accommodation1.jpg",
                        Title = "Temporary Shelter",
                        Name = "Johnson",
                        Location = "Paris, France",
                        Description = "Seeking temporary shelter due to eviction"
                    },
                    new Accommodation
                    {
						ImageName = "/images/accommodation2.jpg",
                        Title = "Housing Assistance",
                        Name = "Kim",
                        Location = "Seoul, South Korea",
                        Description = "Help needed for securing stable housing"
                    },
                    new Accommodation
                    {
						ImageName = "/images/accommodation3.jpg",
                        Title = "Home for Family",
                        Name = "Rodriguez",
                        Location = "Buenos Aires, AR",
                        Description = "Family of 5 in need of a permanent home"
                    },
                    new Accommodation
                    {
						ImageName = "/images/accommodation4.jpg",
                        Title = "Shared Housing",
                        Name = "Olga",
                        Location = "Moscow, Russia",
                        Description = "Seeking shared housing arrangement"
                    },
                    new Accommodation
                    {
						ImageName = "/images/accommodation5.jpg",
                        Title = "Emergency Shelter",
                        Name = "Juan",
                        Location = "Mexico City, MX",
                        Description = "Emergency shelter needed for a week"
                    });
            }
            context.SaveChanges();
        }
    }
}