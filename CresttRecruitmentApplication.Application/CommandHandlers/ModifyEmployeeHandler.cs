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
    public class ModifyEmployeeHandler : IRequestHandler<ModifyEmployeeCommand>
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IEmployeeWriteRepository _employeeWriteRepository;
        private readonly IEmployeeBuilder _employeeBuilder;

        public ModifyEmployeeHandler(
            IEmployeeBuilder employeeBuilder,
            IEmployeeReadRepository employeeReadRepository,
            IEmployeeWriteRepository employeeWriteRepository)
        {
            _employeeBuilder = employeeBuilder;
            _employeeReadRepository = employeeReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
        }

        public async Task<Unit> Handle(ModifyEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = _employeeReadRepository.GetById(new Guid(request.Values.Key));

            _employeeBuilder.SetName(request.Values.Name);
            _employeeBuilder.SetPesel(request.Values.Pesel);
            _employeeBuilder.SetLastName(request.Values.LastName);
            _employeeBuilder.SetDateOfBirth(request.Values.DateOfBirth);
            _employeeBuilder.SetGender((GenderType)request.Values.Gender);

            var employee = _employeeBuilder.ToModifiedEmployee(
                (await existingEmployee).Key,
                (await existingEmployee).ID);

            await _employeeWriteRepository.Modify(employee);

            return new Unit();
        }
    }
}