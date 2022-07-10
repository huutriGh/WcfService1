using BaseProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Models
{
    public class EntityBase<T>
    {
        public T Id { get; set; }
    }
}
