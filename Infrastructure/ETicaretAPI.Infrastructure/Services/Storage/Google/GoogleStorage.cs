using ETicaretAPI.Application.Abstractions.Storage.Google;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Google
{
    internal class GoogleStorage : IGoogleStorage
    {
        readonly DriveService _driveService;
        private readonly string _parentFolderId;

        public GoogleStorage(string parentFolderId, DriveService driveService = null)
        {
            _parentFolderId = parentFolderId;
            _driveService = driveService;
        }

        public Task DeletAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            throw new NotImplementedException();

        }
    }
}
