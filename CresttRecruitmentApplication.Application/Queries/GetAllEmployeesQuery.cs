using CresttRecruitmentApplication.Application.Dtos;
using MediatR;
using System.Collections.Generic;

namespace CresttRecruitmentApplication.Application.Queries
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<ExtendedEmployeeDto>>
    {
    }
}