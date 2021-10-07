using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Application.QueryHandlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Unit>
    {
        private readonly IEmployeeWriteRepository _employeeWriteRepository;
        private readonly IEmployeeUtilityRepository _employeeUtilityRepository;

        public CreateEmployeeHandler(
            IEmployeeWriteRepository employeeWriteRepository,
            IEmployeeUtilityRepository employeeUtilityRepository)
        {
            _employeeWriteRepository = employeeWriteRepository
                ?? throw new ArgumentNullException(nameof(employeeWriteRepository));
            _employeeUtilityRepository = employeeUtilityRepository
                ?? throw new ArgumentNullException(nameof(employeeUtilityRepository));
        }

        public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (_employeeUtilityRepository.CheckIfPeselNumberIsTaken(request.PeselNumber))
                throw new ArgumentException($"Pesel {request.PeselNumber.Value} is taken");

            var highestTakenIdentityNumber = _employeeUtilityRepository.GetHighestTakenIdentityNumber();
            var createdIdentityNumber = new EmployeeIdentityNumber(highestTakenIdentityNumber.GetAsNumber() + 1);

            var model = new Employee(
                createdIdentityNumber,
                request.PeselNumber,
                request.DateOfBirth,
                request.LastName,
                request.Name,
                request.Gender);

            await _employeeWriteRepository.Insert(model);

            return new Unit();
        }
    }
}