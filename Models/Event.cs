using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class Event
    {
        //Event ID, simply named "ID" so  Entity Framework knows it's the ID
        public int ID { get; set; }

        //Changes the display name on the Index page to a much more user friendly name
        [Display(Name = "Event Name")]
        public string eventName { get; set; }

        [Display(Name = "Event Type")]
        public string eventType { get; set; }

        [Display(Name = "Number of Setups")]
        public int numOfSetups { get; set; }

        //Foreign Key
        public int OrganizerID { get; set; }

        [Display(Name = "Organizer")]
        //Organizer Object (This is how we get the organizer name)
        public Organizer orgObj { get; set; }

        //public virtual List<Organizer> organizerList { get; set; }
    }
}
