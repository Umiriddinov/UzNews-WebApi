﻿namespace UzNews.WebApi.Services.Interfaces.Common;

public interface IFileService
{
    public Task<string> UploadImageAsync(IFormFile image);
    public Task<bool> DeleteImageAsync(string subpath);
}
