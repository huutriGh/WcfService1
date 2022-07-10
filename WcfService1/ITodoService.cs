using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService1.models;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITodoService" in both code and config file together.
    [ServiceContract]
    public interface ITodoService
    {
        [OperationContract]
        [WebInvoke(

            Method = "GET",
            UriTemplate = "TodoItem/GetAllItem",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
           )]
        IEnumerable<TodoItem> GetAllItem();


        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "TodoItem/AddItem",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
           )]
        void AddItem(TodoItem item);
    }
}
