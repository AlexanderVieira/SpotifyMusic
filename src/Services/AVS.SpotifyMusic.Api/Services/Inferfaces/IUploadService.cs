namespace AVS.SpotifyMusic.Api.Services.Interfaces
{
    public interface IUploadService
    {
        Task<string> SaveImage(IFormFile imageFile, string destino);
        void DeleteImage(string imageName, string detino);
    }
}