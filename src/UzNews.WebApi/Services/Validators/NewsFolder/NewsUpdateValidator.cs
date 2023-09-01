using FluentValidation;
using UzNews.WebApi.Helpers;
using UzNews.WebApi.Services.Dtos;

namespace UzNews.WebApi.Services.Validators.NewsFolder;

public class NewsUpdateValidator : AbstractValidator<NewsUpdateDto>
{
    public NewsUpdateValidator()
    {
        RuleFor(dto => dto.Title).NotNull().NotEmpty().WithMessage("Title field is required!")
            .MinimumLength(3).WithMessage("Title must be more than 3 characters")
            .MaximumLength(50).WithMessage("Title must be less than 50 characters");
        RuleFor(dto => dto.Content).NotNull().NotEmpty().WithMessage("Content field is required!")
            .MinimumLength(3).WithMessage("Content must be more than 3 characters");

        When(dto => dto.Image is not null, () =>
        {
            int maxImageSizeMB = 5;
            RuleFor(dto => dto.Image!.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
            RuleFor(dto => dto.Image!.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not image file");
        });
    }
}
