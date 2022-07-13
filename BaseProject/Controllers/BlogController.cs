using BaseProject.Models;
using BaseProject.Repository;
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
    public class BlogController : ControllerBase
    {
        private readonly IRepository<Blog> respository;
        public BlogController(IRepository<Blog> respository)
        {
            this.respository = respository;
        }

        [HttpPost]
        [SwaggerOperation(
           Summary = "Insert a Blog",
           Description = "Insert a Blog",
           OperationId = "BlogController.CreateBlog",
           Tags = new[] { "Blog" })
       ]
        public async Task<Blog> CreateBlog()
        {
            var listPost = new List<Post>();

            var post1 = new Post()
            {
                Content = "Post 1",
                Title = "Post 1",

            };

            var post2 = new Post()
            {
                Content = "Post 2",
                Title = "Post 2",

            };

            listPost.Add(post1);
            listPost.Add(post2);


            var blog = new Blog()
            {
                Url = "/",
                Posts = listPost
            };
            return await respository.Insert(blog);

        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Get all Blog",
           Description = "Get  log and all post of log by blog id",
           OperationId = "BlogController.GetAllBlog",
           Tags = new[] { "Blog" })
       ]
        public async Task<ActionResult<Blog>> GetBlog(int blogid)
        {
            var blogs = await respository.Get(b => b.BlogId == blogid, includeProperties: b => b.Posts);

            if (blogs.Count > 0)
            {
                return Ok(blogs[0]);
            }
            else
            {
                return NotFound();
            }



        }
    }
}
