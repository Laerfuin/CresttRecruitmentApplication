using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Application.Queries;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Application.QueryHandlers
{
    public class GetEmployeeByKeyHandler : IRequestHandler<GetEmployeeByKeyQuery, ExtendedEmployeeDto>
    {
        private readonly IEmployeeReadRepository _employeeRepository;

        public GetEmployeeByKeyHandler(IEmployeeReadRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ExtendedEmployeeDto> Handle(GetEmployeeByKeyQuery request, CancellationToken cancellationToken)
        {
            var value = await _employeeRepository.GetById(request.Key);

            if (value == null)
                throw new NullReferenceException();

            var result = new ExtendedEmployeeDto
            {
                ID = value.ID.Value,
                Name = value.Name.Value,
                Pesel = value.Pesel.Value,
                Key = value.Key.ToString(),
                Gender = (byte)value.Gender.Value,
                LastName = value.LastName.Value,
                DateOfBirth = value.DateOfBirth.Value
            };

            return result;
        }
    }
}