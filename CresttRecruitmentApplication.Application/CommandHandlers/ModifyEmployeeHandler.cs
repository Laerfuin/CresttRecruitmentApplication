using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Application.Exceptions;
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
            _employeeReadRepository = employeeReadRepository 
                ?? throw new ArgumentNullException(nameof(employeeReadRepository)); //TODO CR warto sprawdzać czy nie przyszły nulle
            _employeeWriteRepository = employeeWriteRepository 
                ?? throw new ArgumentNullException(nameof(employeeWriteRepository));
            _employeeUtilityRepository = employeeUtilityRepository 
                ?? throw new ArgumentNullException(nameof(employeeUtilityRepository));
        }

        public async Task<Unit> Handle(ModifyEmployeeCommand request, CancellationToken cancellationToken)
        {
            /* TODO CR kilka uwag:
               Rozpatrzmy taki scenariusz:
                1. próbujesz pobrać pracownika po Id (u nas Key)
                2. jeśli go nie ma, to rzucasz wyjątek, że nie istnieje (pewnie kod błędu 404 w kontrolerze)
                3. jeśli jest i pesel ma się zmienić, to sprawdzasz czy już taki nie istnieje u innego pracownika i jeśli tak to ewentualnie rzucasz wyjątek
                4. jeśli jest ok, to na pracowniku wywołujesz metodę "Modify", która zmienia jego parametry

                To idealny scenariusz, którego używamy u nas. ORM nam na to pozwala.
            */

            var existingValue = await _employeeReadRepository.GetById(request.Id);

            if (existingValue == null)
                throw new NotFoundException();

            if (!request.PeselNumber.Equals(existingValue.PeselNumber)
                && _employeeUtilityRepository.CheckIfPeselNumberIsTaken(request.PeselNumber))
            {
                throw new ArgumentException($"Pesel number {request.PeselNumber.Value} is taken");
            }

            existingValue.Update(
                gender: request.Gender,
                name: request.Name,
                peselNumber: request.PeselNumber,
                lastName: request.LastName,
                dateOfBirth: request.DateOfBirth);

            return new Unit();
        }
    }
}