using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImagesCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImagesCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImagesCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        async public Task<RemoveProductImagesCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            Domain.Entities.ProductImageFile? productImageFile = product?.ProductImageFiles
                .FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            if (productImageFile != null)
                product?.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
