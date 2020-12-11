using DLL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

    }
}
