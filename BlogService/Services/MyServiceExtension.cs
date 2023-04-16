using BlogService.Services.PostServices;
using BlogService.Services.PostServices.Interfaces;

namespace BlogService.Services
{
    public static class MyServiceExtension
    {
        public static IServiceCollection AddMyService(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            return services;
        }
    }
}
