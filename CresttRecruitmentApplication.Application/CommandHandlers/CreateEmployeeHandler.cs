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
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeeWriteRepository _employeeWriteRepository;
        private readonly IEmployeeUtilityRepository _employeeUtilityRepository;

        public CreateEmployeeHandler(
            IEmployeeWriteRepository employeeWriteRepository,
            IEmployeeUtilityRepository employeeUtilityRepository)
        {
            _employeeWriteRepository = employeeWriteRepository;
            _employeeUtilityRepository = employeeUtilityRepository;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (_employeeUtilityRepository.CheckIfPeselNumberIsTaken(request.Values.Pesel))
                throw new ArgumentException($"Pesel {request.Values.Pesel} is taken");

            var id = new EmployeeID(_employeeUtilityRepository.GetFreeID());
            var name = new EmployeeName(request.Values.Name);
            var peselNumber = new EmployeePeselNumber(request.Values.Pesel);
            var lastName = new EmployeeLastName(request.Values.LastName);
            var dateOfBirth = new EmployeeDateOfBirth(request.Values.DateOfBirth);
            var gender = new EmployeeGender((GenderType)request.Values.Gender);

            var model = new Employee(
                Guid.NewGuid(),
                id,
                peselNumber,
                dateOfBirth,
                lastName,
                name,
                gender);

            await _employeeWriteRepository.Create(model);

            return model.Key;
        }
    }
}