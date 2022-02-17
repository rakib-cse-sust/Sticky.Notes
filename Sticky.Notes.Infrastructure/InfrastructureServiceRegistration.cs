using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sticky.Notes.Application.Contracts.Infrastructure;
using Sticky.Notes.Application.Models.Mail;
using Sticky.Notes.Infrastructure.Mail;

namespace Sticky.Notes.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
