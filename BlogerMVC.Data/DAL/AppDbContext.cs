using BlogerMVC.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogerMVC.Data.DAL;

public class AppDbContext:IdentityDbContext
{
    public AppDbContext(DbContextOptions options): base(options)
    {
        
    }

    public DbSet<Event> Events { get; set; }

    public DbSet<AppUser> Users { get; set; }
}
