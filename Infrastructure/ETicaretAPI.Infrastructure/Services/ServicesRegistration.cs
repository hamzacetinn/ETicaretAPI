using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Infrastructure.enums;
using ETicaretAPI.Infrastructure.Services.Storage;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Infrastructure.Services.Storage.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Infrastructure.Services
{
    public static class ServicesRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            //services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IStorageService, StorageService>();

        }

        public static void AddStorage<T>(this IServiceCollection servicesCollections) where T : RenameStorage, IStorage
        {
            //servicesCollections.AddScoped<IStorage, T>();

        }
        
        public static void AddStorage(this IServiceCollection servicesCollections, StorageType storageType)
        {

            switch (storageType) // bu yapı tercih edilmez genelde, t yazan yere AzureStogre yazarsak daha temiz bir kullanım olur, çeşitlilik olsun diye eklendi.
            {
                case StorageType.Local:
                    servicesCollections.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    servicesCollections.AddScoped<IStorage, AzureStorage>();

                    break;
                case StorageType.AWS:

                    break;
                default:
                    servicesCollections.AddScoped<IStorage, LocalStorage>();
                    break;

            }
        }
    }
}