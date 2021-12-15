using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab5.Models
{
    public class EventContext : IdentityDbContext<AppUser>
    {
        //Constructor for EventContext
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }
        //Creates the Events table
        public DbSet<Event> Events { get; set; }
        //Creates the Organizers table
        public DbSet<Organizer> Organizers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //You can make customizations here.
        }
    }
}
