using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Application.Queries;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Application.QueryHandlers
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<ExtendedEmployeeDto>>
    {
        private readonly IEmployeeReadRepository _employeeRepository;

        public GetAllEmployeesHandler(IEmployeeReadRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<ExtendedEmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var values = await _employeeRepository.GetAll();

            var result = values.Select(a => new ExtendedEmployeeDto
            {
                IdentityNumber = a.IdentityNumber.Value,
                Name = a.Name.Value,
                PeselNumber = a.PeselNumber.Value,
                Id = a.Id.Value,
                Gender = a.Gender.Value,
                LastName = a.LastName.Value,
                DateOfBirth = a.DateOfBirth.Value
            }).ToList();

            return result;
        }
    }
}