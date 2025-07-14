using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IStorageService _storageService;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        async public Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerNamme)> results = await _storageService.UploadAsync("photo-image", request.Files);

            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            await _productImageFileWriteRepository.AddRangeAsync(results.Select(r => new Domain.Entities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerNamme,
                Storage = _storageService.StorageName,
                Product = new List<Domain.Entities.Product>() { product }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
