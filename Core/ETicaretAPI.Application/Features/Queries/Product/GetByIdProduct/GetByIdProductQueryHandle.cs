using ETicaretAPI.Application.Repositories;
using MediatR;
using P = ETicaretAPI.Domain.Entities.Product;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandle : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandle(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P product = await _productReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,

            };
        }
    }
}
