using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
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
        private readonly IEmployeeUtilityRepository _employeeUtilityRepository;

        public ModifyEmployeeHandler(
            IEmployeeReadRepository employeeReadRepository,
            IEmployeeWriteRepository employeeWriteRepository,
            IEmployeeUtilityRepository employeeUtilityRepository)
        {
            _employeeReadRepository = employeeReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
            _employeeUtilityRepository = employeeUtilityRepository;
        }

        public async Task<Unit> Handle(ModifyEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingValue = await _employeeReadRepository.GetById(new Guid(request.Values.Key));

            var peselNumber = new EmployeePeselNumber(request.Values.Pesel);

            if (!peselNumber.Equals(existingValue.Pesel)
                && _employeeUtilityRepository.CheckIfPeselNumberIsTaken(request.Values.Pesel))
            {
                throw new ArgumentException($"Pesel {request.Values.Pesel} is taken");
            }

            var name = new EmployeeName(request.Values.Name);
            var lastName = new EmployeeLastName(request.Values.LastName);
            var dateOfBirth = new EmployeeDateOfBirth(request.Values.DateOfBirth);
            var gender = new EmployeeGender((GenderType)request.Values.Gender);

            var model = new Employee(
                existingValue.Key,
                existingValue.ID,
                peselNumber,
                dateOfBirth,
                lastName,
                name,
                gender);

            await _employeeWriteRepository.Modify(model);

            return new Unit();
        }
    }
}