using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Models
{
    public class Persional
    {
        public string PersionalId { get; set; }
        public string PersionalName { get; set; }
        public virtual ICollection<Service > Services { get; set; }


    }
}
