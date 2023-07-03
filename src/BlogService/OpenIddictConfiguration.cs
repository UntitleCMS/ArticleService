using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation.SystemNetHttp;
using System.Runtime.CompilerServices;

namespace AuthenticationService;

// some code from https://nwb.one/blog/openid-connect-dotnet-5
public static class OpenIddictConfiguration
{
    public static void AddMyOpendIddictConfiguration(this IServiceCollection services)
    {
        services.AddOpenIddict()

        .AddValidation(options =>
        {
            var issure = Environment.GetEnvironmentVariable("AUTH_SERVER")
                ?? throw new Exception("ENV AUTH_SERVER is null");
            options.SetIssuer(issure);
            //options.SetIssuer("https://172.28.64.1:4434");
            //options.SetIssuer("http://authenticationservice/");
            options.UseSystemNetHttp();

            // Register the ASP.NET Core host.
            options.UseAspNetCore();
        });

        // Allow any certificate ***DANGER FOR PRODUCTION***
        services.AddHttpClient(typeof(OpenIddictValidationSystemNetHttpOptions).Assembly.GetName().Name!)
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });

    }
}
