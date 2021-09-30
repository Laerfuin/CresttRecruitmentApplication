using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Application.QueryHandlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeWriteRepository _employeeRepository;

        public DeleteEmployeeHandler(IEmployeeWriteRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.Delete(request.Key);

            return new Unit();
        }
    }
}