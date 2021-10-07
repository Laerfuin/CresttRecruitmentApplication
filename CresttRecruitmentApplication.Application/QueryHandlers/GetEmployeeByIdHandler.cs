using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Application.Exceptions;
using CresttRecruitmentApplication.Application.Queries;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Application.QueryHandlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, ExtendedEmployeeDto>
    {
        private readonly IEmployeeReadRepository _employeeRepository;

        public GetEmployeeByIdHandler(IEmployeeReadRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ExtendedEmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _employeeRepository.GetById(request.Id);

            if (value == null)
                throw new NotFoundException();

            var result = new ExtendedEmployeeDto
            {
                IdentityNumber = value.IdentityNumber.Value,
                Name = value.Name.Value,
                PeselNumber = value.PeselNumber.Value,
                Id = value.Id.Value,
                Gender = value.Gender.Value,
                LastName = value.LastName.Value,
                DateOfBirth = value.DateOfBirth.Value
            };

            return result;
        }
    }
}