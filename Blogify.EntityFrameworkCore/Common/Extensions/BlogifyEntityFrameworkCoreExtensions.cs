using Blogify.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Blogify.Common.Extensions;

public static class BlogifyEntityFrameworkCoreExtensions
{
    /// <summary>
    /// Register Default EfCore Repositories Implementation
    /// </summary>
    /// <param name="services">The services collection of active application</param>
    public static void AddBasicEfCoreRepositories(this IServiceCollection services)
    {
        // Register read only repo
        services.AddTransient(typeof(IReadOnlyRepository<,>), typeof(EfCoreReadOnlyRepository<,>));
        services.AddTransient(typeof(IReadOnlyRepository<>), typeof(EfCoreReadOnlyRepository<>));

        // Register basic repo
        services.AddTransient(typeof(IBasicRepository<,>), typeof(EfCoreBasicRepository<,>));
        services.AddTransient(typeof(EfCoreBasicRepository<>), typeof(EfCoreBasicRepository<>));
    }
}