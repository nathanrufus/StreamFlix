using Amazon.S3;
using Amazon.S3.Transfer;

namespace StreamFlix.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private const string BucketName = "your-s3-bucket-name";

        public S3Service(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<string> UploadFileAsync(string keyName, IFormFile file)
        {
            using var newMemoryStream = new MemoryStream();
            file.CopyTo(newMemoryStream);
            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(newMemoryStream, BucketName, keyName);
            return $"https://{BucketName}.s3.amazonaws.com/{keyName}";
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            var key = fileUrl.Replace($"https://{BucketName}.s3.amazonaws.com/", "");
            await _s3Client.DeleteObjectAsync(BucketName, key);
        }
    }
}
