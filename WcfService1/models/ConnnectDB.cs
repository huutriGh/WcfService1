using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WcfService1.models
{
    public class ConnnectDB : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public ConnnectDB() : base("name=dbconnection")
        {

        }
    }
}