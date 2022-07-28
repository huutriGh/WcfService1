using BaseProject.DTO.Product;
using BaseProject.Models;
using BaseProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<Product> _repository;
        public UploadFileController(IWebHostEnvironment env, IRepository<Product> repository)
        {
            _env = env;
            _repository = repository;
        }

        [Authorize(Policy = "UserRights")]
        [HttpPost]
        [Route("UploadFlie")]
        [SwaggerOperation(
            Summary = "UploadFlie",
            Description = "UploadFlie",
            OperationId = "UploadFlie",
            Tags = new[] { "UploadFlie" })]
        public async Task<ActionResult<HttpResponseMessage>> HandleAsync(List<IFormFile> files, [FromForm] string productJson)
        {

            try
            {

                // Config JSON 
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
                };
                // Convert JSON string sang Object
                var productRequest = JsonSerializer.Deserialize<ProductRequest>(productJson, options);

                // Khoi tao mot product moi
                Product product = null;


                if (files.Count > 0)
                {
                    var formFile = files[0];
                    if (formFile.Length > 0)
                    {
                        // Luu Product xuong BD
                        product = new Product()
                        {
                            ProductName = productRequest.ProductName,
                            Category = productRequest.Category,
                            Price = productRequest.Price,

                        };
                        await _repository.Insert(product);
                        // Sau khi luu Product se co duoc Product Id
                        var filePath = Path.Combine(_env.ContentRootPath, "Images", product.ProductId.ToString());
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        filePath = Path.Combine(filePath, formFile.FileName);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await formFile.CopyToAsync(stream);

                        // Cap nhat lai url cua san pham sau luu xong hinh anh
                        product.ImageUrl = "Images/" + product.ProductId.ToString() + "/" + formFile.FileName;
                        await _repository.Update(product);


                    }
                }
                else
                {
                    return BadRequest();
                }



                var response = new
                {
                    product.ProductId,
                    product.ProductName,
                    product.Price,
                    product.Category,
                    product.ImageUrl,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;
            }



        }
    }
}
