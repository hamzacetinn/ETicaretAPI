using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.ViewModel.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IOrderReadRepository _orderReadRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        readonly private IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository,IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_productReadRepository.GetAll(false));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id, false);

            return Ok();
        }
        [HttpPost]

        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            //if(!ModelState.IsValid)
            //{

            //}
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product p = await _productReadRepository.GetByIdAsync(model.Id);
            p.Stock = model.Stock;
            p.Name = model.Name;
            p.Price = model.Price;
            _productWriteRepository.AddAsync(p);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Product p = await _productReadRepository.GetByIdAsync(id);
            _productWriteRepository.SaveAsync();
            return Ok(new
            {
                message="Silme işlemi başarılı!",
            });
        }
        [HttpPost("[action]")]
        public async Task <IActionResult> Upload()
        {
            //wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullpath = Path.Combine(uploadPath,$"{r.Next()}{Path.GetExtension(file.Name)}" );

                using FileStream fileStream= new(fullpath, FileMode.Create, FileAccess.Write, FileShare.None,
                1024*1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetById(id);
            return Ok(product);
        }*/

    }
}
