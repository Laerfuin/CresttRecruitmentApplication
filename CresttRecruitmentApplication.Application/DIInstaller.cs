using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CresttRecruitmentApplication.Application
{
    public static class DIInstaller
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}