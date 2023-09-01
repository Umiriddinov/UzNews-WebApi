using FileSystem.WebApi.Configuration;
using UzNews.WebApi.DataAcces.Interfaces.NewsFolder;
using UzNews.WebApi.DataAcces.Interfaces.TagsFolder;
using UzNews.WebApi.DataAcces.Repositories.NewsFolder;
using UzNews.WebApi.DataAcces.Repositories.TagsFolder;
using UzNews.WebApi.Services.Interfaces;
using UzNews.WebApi.Services.Interfaces.Common;
using UzNews.WebApi.Services.Services;
using UzNews.WebApi.Services.Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPaginator, Paginator>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ITagsService , TagsService>();


builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<ITagsRepository, TagsRepository>();
builder.Services.AddScoped<INewsTagsRepository, NewsTagsRepository>();

builder.ConfigureCORSPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
