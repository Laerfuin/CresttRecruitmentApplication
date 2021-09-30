using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Domain.Builders.Interfaces;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Application.QueryHandlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeeWriteRepository _employeeRepository;
        private readonly IEmployeeBuilder _employeeBuilder;

        public CreateEmployeeHandler(
            IEmployeeWriteRepository employeeRepository,
            IEmployeeBuilder employeeBuilder)
        {
            _employeeBuilder = employeeBuilder;
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _employeeBuilder.SetName(request.Values.Name);
            _employeeBuilder.SetPesel(request.Values.Pesel);
            _employeeBuilder.SetLastName(request.Values.LastName);
            _employeeBuilder.SetDateOfBirth(request.Values.DateOfBirth);
            _employeeBuilder.SetGender((GenderType)request.Values.Gender);

            var employee = _employeeBuilder.ToNewEmployee();

            await _employeeRepository.Create(employee);

            return employee.Key;
        }
    }
}