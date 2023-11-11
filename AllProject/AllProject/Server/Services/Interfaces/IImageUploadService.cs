namespace AllProject.Server.Services.Interfaces
{
    public interface IImageUploadService
    {
        Task<string> Upload(string Id, string ImageUrl);
    }
}
