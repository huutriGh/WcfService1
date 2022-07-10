using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}