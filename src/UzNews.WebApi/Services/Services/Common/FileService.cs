using UzNews.WebApi.Helpers;
using UzNews.WebApi.Services.Interfaces.Common;
using static System.Net.Mime.MediaTypeNames;

namespace UzNews.WebApi.Services.Services.Common;

public class FileService : IFileService
{
    private readonly string IMAGES = "Images";
    private readonly string ROOTPATH;

    public FileService(IWebHostEnvironment env)
    {
        ROOTPATH = env.WebRootPath;
    }

    public async Task<bool> DeleteImageAsync(string subpath)
    {
        if (subpath == "") return true;
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        string newImageName = MediaHelper.MakeImageName(image.FileName);
        string subpath = Path.Combine(IMAGES, newImageName);
        string path = Path.Combine(ROOTPATH, subpath);
        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }
}
