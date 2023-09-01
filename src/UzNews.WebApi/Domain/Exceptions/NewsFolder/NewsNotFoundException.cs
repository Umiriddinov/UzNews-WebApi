namespace UzNews.WebApi.Domain.Exceptions.NewsFolder
{
    public class NewsNotFoundException : NotFoundException
    {
        public NewsNotFoundException()
        {
            this.TitleMessage = "News not found !";
        }
    }
}
