using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharpDevLib.Extensions.Email;

/// <summary>
/// email dependency register extension
/// </summary>
public static class EmailExtension
{
    /// <summary>
    /// add email service from configuration files
    /// </summary>
    /// <param name="services">service collection</param>
    /// <param name="configuration">configuration</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection("Email"));
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }

    /// <summary>
    /// add email service from code
    /// </summary>
    /// <param name="services">service collection</param>
    /// <param name="options">options</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddEmail(this IServiceCollection services, Action<EmailOptions> options)
    {
        services.Configure(options);
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}
