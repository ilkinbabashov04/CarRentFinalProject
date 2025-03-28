using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=filestorageilkinbabashov;AccountKey=JGt7ng+bUaS3nc2Q+v2izbdjkpqOXf4tWeQpcLD+xfIeiDd5jtSMXcmG/V6aR+SrDkF6kbuOlyyh+AStEjuRRA==;EndpointSuffix=core.windows.net";
        private readonly string shareName = "cars";

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty!");

            try
            {
                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(shareName);
                await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

                var blobClient = blobContainerClient.GetBlobClient(file.FileName);
                using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

                return Ok(new { Message = "File uploaded successfully!", FileUrl = blobClient.Uri.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("Download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(shareName);
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                var downloadInfo = await blobClient.DownloadAsync();
                return File(downloadInfo.Value.Content, downloadInfo.Value.ContentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
