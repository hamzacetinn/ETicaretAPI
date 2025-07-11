using ETicaretAPI.Application.Abstractions.Storage.Local;
using ETicaretAPI.Infrastructure.Services.Storage.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : RenameStorage,ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeletAsync(string path, string fileName)
                => File.Delete($"{path}{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists($"{path}\\{fileName}");
        async Task<bool> CoppyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();//Flush  ile filSTream üzerindeki tüm çalışmalar silinir.
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }
        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string path, IFormFileCollection files)
        {
            //wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                var fileNewName = await FileRenameAsync(path, file.Name, HasFile);


                await CoppyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}")); //  $"{parth}\\{fileNewName}" bunun yerine aploadPath de yazılabilir

            }
            //todo eğer ki yukarıdaki if geçerli, değilse burada dosyaların sunucuya yüklenirken bir hata aşındığına dair uyarıcı bir exception oluşturulup fırlatılması gerekiyor!
            return datas;
        }
    }
}
