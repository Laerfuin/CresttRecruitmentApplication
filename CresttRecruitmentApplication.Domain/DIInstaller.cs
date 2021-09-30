using CresttRecruitmentApplication.Domain.Builders.Implementation;
using CresttRecruitmentApplication.Domain.Builders.Interfaces;
using CresttRecruitmentApplication.Domain.Repositories.Implementation;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using CresttRecruitmentApplication.Domain.Services.Implementation;
using CresttRecruitmentApplication.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CresttRecruitmentApplication.Application
{
    public static class DIInstaller
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeeReadRepository, EmployeeReadRepository>();
            services.AddSingleton<IEmployeeWriteRepository, EmployeeWriteRepository>();
            services.AddSingleton<IEmployeeUtilityService, EmployeeUtilityService>();

            services.AddTransient<IEmployeeBuilder, EmployeeBuilder>();

            return services;
        }
    }
}