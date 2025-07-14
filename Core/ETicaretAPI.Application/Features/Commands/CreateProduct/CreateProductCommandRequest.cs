using ETicaretAPI.Application.ViewModel.Products;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        //public VM_Create_Product Model { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
