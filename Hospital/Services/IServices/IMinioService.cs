namespace Hospital.Services.IServices
{
    public interface IMinioService
    {
        public Task<string> UploadFile(string fileName, long id);
    }
}
