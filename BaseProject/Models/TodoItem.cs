using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Models
{
    public class TodoItem : EntityBase<int>
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
