using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=filestorageilkinbabashov;AccountKey=JGt7ng+bUaS3nc2Q+v2izbdjkpqOXf4tWeQpcLD+xfIeiDd5jtSMXcmG/V6aR+SrDkF6kbuOlyyh+AStEjuRRA==;EndpointSuffix=core.windows.net";
        private readonly string _containerName = "cars";

        public async Task<bool> DeleteImageFromAzure(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl)) return false;

                var blobServiceClient = new BlobServiceClient(_connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

                // Şəkilin adı URL-dən çıxarılır
                var blobName = new Uri(imageUrl).Segments.Last();
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                // Asinxron olaraq şəkili silirik
                var deleteResponse = await blobClient.DeleteIfExistsAsync();
                return deleteResponse.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Şəkil silinərkən xəta baş verdi: {ex.Message}");
                return false;
            }
        }


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
