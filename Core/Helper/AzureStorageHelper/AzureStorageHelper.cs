using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper.AzureStorageHelper
{
    public class AzureStorageHelper
    {
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=filestorageilkinbabashov;AccountKey=JGt7ng+bUaS3nc2Q+v2izbdjkpqOXf4tWeQpcLD+xfIeiDd5jtSMXcmG/V6aR+SrDkF6kbuOlyyh+AStEjuRRA==;EndpointSuffix=core.windows.net";
        private readonly string _containerName = "cars";

        public async Task<string> UploadImageToAzure(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

            return blobClient.Uri.ToString();
        }
    }
}
