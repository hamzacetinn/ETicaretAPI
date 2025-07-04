using ETicaretAPI.Application.ViewModel.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen ürün adını boş bırakmayınız.")
                .MaximumLength(100)
                .MinimumLength(5)
                    .WithMessage("Ürün adı en fazla 100 ve en az 5 karakter aralığında karakter giriniz.");
            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen stok bilgisini boş bırakmayınız.")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz!");
            RuleFor(p => p.Price)
             .NotEmpty()
             .NotNull()
                 .WithMessage("Lütfen stok bilgisini boş bırakmayınız.")
             .Must(s => s >= 0)
                 .WithMessage("Fiyat bilgisi negatif değer alamaz!");
        
    }

    }
}
