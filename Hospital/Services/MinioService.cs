using System;
using Minio;
using Minio.Exceptions;
using Minio.DataModel;
using System.Threading.Tasks;
using System.Drawing;
using System.Text;
using Hospital.Migrations;
using System.Security.AccessControl;
using Hospital.Services.IServices;

namespace Hospital.Services
{
    public class MinioService : IMinioService
    {
        private readonly string endpoint = "play.min.io";
        private readonly string accessKey = "Q3AM3UQ867SPQQA43P2F";
        private readonly string secretKey = "zuf+tfteSlswRu7BJ86wekitnifILbZam1KYY3TG";
        private readonly string bucketName = "hospital";
        private readonly MinioClient client;
        public MinioService() 
        {            
            client = new MinioClient()
                        .WithEndpoint(endpoint)
                        .WithCredentials(accessKey, secretKey)
                        .Build();
        }
        public async Task<string> UploadFile(string fileName, long id)
        {
            var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
            bool found = await client.BucketExistsAsync(beArgs).ConfigureAwait(false);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await client.MakeBucketAsync(mbArgs).ConfigureAwait(false);
            }
            var filePath = String.Concat("Employees/", id, "/", fileName);
            var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucketName)
                    .WithObject(filePath);
            await client.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            Console.WriteLine("Successfully uploaded " + fileName);
            return filePath;
        }
    }
}