using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityLogs.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Microsoft.AspNetCore.Identity.IdentityUser> AspNetUsers { get; set; }
        public DbSet<Microsoft.AspNetCore.Identity.IdentityRole> Aspnetroles { get; set; }
    }
}
