using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Data.Mapping;
using projecten3_1920_backend_klim03.Domain.Models;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, ApplicationRole, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Adres> Adresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<ApplicationDomain> ApplicationDomains { get; set; }

        public DbSet<ProjectTemplate> ProjectTemplates { get; set; }
        public DbSet<CategoryTemplate> CategoryTemplates { get; set; }
        public DbSet<ProductTemplate> ProductTemplates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AdresConfig());
            builder.ApplyConfiguration(new OrderConfig());
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new GroupConfig());
            builder.ApplyConfiguration(new ClassRoomConfig());
            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new ProjectConfig());
            builder.ApplyConfiguration(new AdresConfig());
            builder.ApplyConfiguration(new SchoolConfig());
            builder.ApplyConfiguration(new PupilConfig());
            builder.ApplyConfiguration(new ApplicationDomainConfig());
            builder.ApplyConfiguration(new ProductVariationTemplateConfig());
            builder.ApplyConfiguration(new EvaluationConfig());
            builder.ApplyConfiguration(new EvaluationCritereaConfig());

            builder.ApplyConfiguration(new ProjectTemplateConfig());
            builder.ApplyConfiguration(new CategoryTemplateConfig());
            builder.ApplyConfiguration(new ProductTemplateConfig());
            builder.ApplyConfiguration(new ProductTemplateProjectTemplateConfig());
            builder.ApplyConfiguration(new PupilGroupConfig());
        }
    }
}
