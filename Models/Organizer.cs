using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class Organizer
    {
        //Organizer ID, simply named "ID" so  Entity Framework knows it's the ID
        public int ID { get; set; }

        //Organizer Name which will be displayed in the Events table based on the ID
        public string Name { get; set; }

        //public ICollection<Event> Events { get; set; }
    }
}
