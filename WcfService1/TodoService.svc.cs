using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.models;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TodoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TodoService.svc or TodoService.svc.cs at the Solution Explorer and start debugging.
    public class TodoService : ITodoService
    {
        ConnnectDB db = new ConnnectDB();
        public void AddItem(TodoItem item)
        {
            db.TodoItems.Add(item);
            db.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAllItem()
        {
            return db.TodoItems.ToList();
        }
    }
}
