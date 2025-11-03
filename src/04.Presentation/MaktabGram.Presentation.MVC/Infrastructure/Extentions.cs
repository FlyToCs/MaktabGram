namespace MaktabGram.Presentation.MVC.Infrastructure
{
    public static class Extentions
    {
        public static IApplicationBuilder CustomExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

    }
}
