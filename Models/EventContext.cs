using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Models
{
    public class EventContext : DbContext
    {
        //Constructor for EventContext
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }
        //Creates the Events table
        public DbSet<Event> Events { get; set; }
        //Creates the Organizers table
        public DbSet<Organizer> Organizers { get; set; }
    }
}
