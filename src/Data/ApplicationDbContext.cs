using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace booka.counter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Count> Counts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }

    public class Count
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Guid TagId { get; set; }

        public string UserId { get; set; }

        public DateTime TimeStamp { get; set; }
    }

    public class Tag
    {
        public Guid Id { get; set; }

        [Description("Titel")]
        public string Title { get; set; }

        [Description("Beschreibung")]
        public string Description { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}
