using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public Category Category { get; set; }
        public virtual ICollection<Persional> Persionals { get; set; }

    }
}
