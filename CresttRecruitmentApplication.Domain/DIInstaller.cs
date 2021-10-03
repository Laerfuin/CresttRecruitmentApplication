using CresttRecruitmentApplication.Domain.Repositories;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CresttRecruitmentApplication.Application
{
    public static class DIInstaller
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeeReadRepository, EmployeeReadRepository>();
            services.AddSingleton<IEmployeeWriteRepository, EmployeeWriteRepository>();
            services.AddSingleton<IEmployeeUtilityRepository, EmployeeUtilityRepository>();

            return services;
        }
    }
}