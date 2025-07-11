using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.File;
using ETicaretAPI.Application.Repositories.InvoiceFile;
using ETicaretAPI.Application.RequestParametrs;
using ETicaretAPI.Application.ViewModel.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly private IFileReadRepository _fileReadRepository;
        readonly private IFileWriteRepository _fileWriteRepository;
        readonly private IProductImageFileReadRepository _productImageFileReadRepository;
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly private IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly private IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration configuration;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, IWebHostEnvironment webHostEnvironment, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productImageFileReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedData,
                p.UpdatedData
            }).ToList();
            return Ok(new
            {

                totalCount,
                products
            });
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
            await _productWriteRepository.AddAsync(p);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Product p = await _productReadRepository.GetByIdAsync(id);
            _productWriteRepository.SaveAsync();
            return Ok(new
            {
                message = "Silme işlemi başarılı!",
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerNamme)> results = await _storageService.UploadAsync("photo-image", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);
            await _productImageFileWriteRepository.AddRangeAsync(results.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerNamme,
                Storage = _storageService.StorageName,
                Product = new List<Product>() { product }
            }).ToList());
            #region
            //wwwroot/resource/product-images
            //string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
            //if(!Directory.Exists(uploadPath))
            //{
            //    Directory.CreateDirectory(uploadPath);
            //}
            //Random r = new();
            //foreach (IFormFile file in Request.Form.Files)
            //{
            //    string fullpath = Path.Combine(uploadPath,$"{r.Next()}{Path.GetExtension(file.Name)}" );

            //    using FileStream fileStream= new(fullpath, FileMode.Create, FileAccess.Write, FileShare.None,
            //    1024*1024, useAsync: false);
            //    await file.CopyToAsync(fileStream);
            //    await fileStream.FlushAsync();
            //}
            #endregion
            #region
            // var datas = await _fileServices.UploadAsync("resoursce//*product-images*/", Request.Form.Files);
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files);

            //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainer,
            //    Storage =_storageService.StorageName
            //}).ToList());
            //await _productImageFileWriteRepository.SaveAsync();

            //var datas = await _fileServices.UploadAsync("resoursce/invoices", Request.Form.Files);
            //await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //    Price = new Random().Next(100, 1000)
            //}).ToList());
            //await _productImageFileWriteRepository.SaveAsync();

            //var datas = await _fileServices.UploadAsync("resoursce/File", Request.Form.Files);
            //await _fileWriteRepository.AddRangeAsync(datas.Select(d => new Domain.Entities.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //}).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            var d1 = _fileReadRepository.GetAll(false); // false means no tracking, base sınıf olduğundan kendisinden türüyenlerin hepsini getirir/sıralar.
            var d2 = _invoiceFileReadRepository.GetAll(false); // false means no tracking
            var d3 = _productImageFileReadRepository.GetAll(false); // false means no tracking
            #endregion
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImage(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                 .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{configuration}[BaseStorageUrl]/{p.Path}",
                p.FileName,
            }));
        }
        [HttpDelete("[acction]/{id}")]
        public async Task<IActionResult> DeletProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            ProductImageFile productImageFile = product.ProductImageFiles
                .FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productImageFileWriteRepository.SaveAsync();
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
