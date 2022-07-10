using BaseProject.Models;
using BaseProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoItemController : ControllerBase
    {
        private readonly IRepository<TodoItem> repository;
        public TodoItemController(IRepository<TodoItem> repository)
        {
            this.repository = repository;
        }


        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Get a Todoitem",
            Description = "Get a Todoitem",
            OperationId = "TodoItemController.GetTodoItemById",
            Tags = new[] { "TodoItem" })
        ]
        public async Task<ActionResult<TodoItem>> GetTodoItemById(int id)
        {
            var todoitem = await repository.GetById(id);
            return todoitem;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Insert a Todoitem",
            Description = "Insert a Todoitem",
            OperationId = "TodoItemController.InsertItem",
            Tags = new[] { "TodoItem" })
        ]
        public async Task<ActionResult<TodoItem>> InsertItem(TodoItem item)
        {
            var todoitem = await repository.Insert(item);
            return todoitem;
        } 


        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a Todoitem",
            Description = "Update a Todoitem",
            OperationId = "TodoItemController.UpdateItem",
            Tags = new[] { "TodoItem" })
        ]
        public async Task<ActionResult<TodoItem>> UpdateItem(TodoItem item)
        {
            
            var todoitem = await repository.Update(item);
            return todoitem;
        }
    }
}
