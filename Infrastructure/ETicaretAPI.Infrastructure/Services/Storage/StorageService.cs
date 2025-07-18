﻿using ETicaretAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeletAsync(string pathOrContainerName, string fileName)
            => await _storage.DeletAsync(pathOrContainerName, fileName);

        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);
        public Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => _storage.UploadAsync(pathOrContainerName, files);
    }
}
