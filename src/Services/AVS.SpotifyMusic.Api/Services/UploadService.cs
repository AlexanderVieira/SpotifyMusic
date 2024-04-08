using AVS.SpotifyMusic.Api.Services.Interfaces;

namespace AVS.SpotifyMusic.Api.Services
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public UploadService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public async Task<string> SaveImage(IFormFile imageFile, string destino)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                              .Take(10)
                                              .ToArray()
                                         ).Replace(' ', '-');

            imageName = $"{imageName}-{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, $"Resources\\Images\\{destino}", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
 
            return imageName;
        }

        public void DeleteImage(string imageName, string destino)
        {
            if (!string.IsNullOrEmpty(imageName)) 
            {
                var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, $"Resources\\Images\\{destino}", imageName);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }
        }
    }
}