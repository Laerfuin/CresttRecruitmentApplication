using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Application.Exceptions;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Application.QueryHandlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IEmployeeWriteRepository _employeeWriteRepository;

        public DeleteEmployeeHandler(
            IEmployeeReadRepository employeeReadRepository,
            IEmployeeWriteRepository employeeWriteRepository)
        {
            _employeeReadRepository = employeeReadRepository
                   ?? throw new ArgumentNullException(nameof(employeeReadRepository));
            _employeeWriteRepository = employeeWriteRepository
                ?? throw new ArgumentNullException(nameof(employeeWriteRepository));
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingValue = await _employeeReadRepository.GetById(request.Id);

            if (existingValue == null)
                throw new NotFoundException();

            await _employeeWriteRepository.Delete(existingValue);

            return new Unit();
        }
    }
}