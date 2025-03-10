﻿using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configuration;

namespace Restaurants.Infrastructure.Storage
{
    public class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingOptions) : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingOptions.Value;
       
        public async Task<string> UploadToBlobAsync(Stream data, string fileName)
        {
           var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
           var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(data);

            var blobUrl = blobClient.Uri.ToString();
            return blobUrl;
        }
    }
}
