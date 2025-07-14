using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProductt;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {


        readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {

            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            //if(!ModelState.IsValid)
            //{

            //}
            CreateProductCommandResponse response = await _mediator.Send(request);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok(); // istersen  response döndürebilirrim.
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
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
            //await _productImageFileWriteRepository.SaveAsync();
            //var d1 = _fileReadRepository.GetAll(false); // false means no tracking, base sınıf olduğundan kendisinden türüyenlerin hepsini getirir/sıralar.
            //var d2 = _invoiceFileReadRepository.GetAll(false); // false means no tracking
            //var d3 = _productImageFileReadRepository.GetAll(false); // false means no tracking
            #endregion
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImage([FromRoute] GetProductImagesQueryRequest getProductImagesQuery)
        {

            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQuery);
            return Ok();
        }
        [HttpDelete("[acction]/{Id}")]
        public async Task<IActionResult> DeletProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImagesCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
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
