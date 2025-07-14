using ETicaretAPI.Application.Repositories;
using MediatR;
using P = ETicaretAPI.Domain.Entities.Product;

namespace ETicaretAPI.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }


        async public Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            P product = await _productReadRepository.GetByIdAsync(request.Id);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
