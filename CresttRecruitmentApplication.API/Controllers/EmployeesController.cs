using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Application.Exceptions;
using CresttRecruitmentApplication.Application.Queries;
using CresttRecruitmentApplication.Domain.Models.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllEmployeesQuery();
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetEmployeeByIdQuery(new EmployeeId(id));

            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto value)
        {
            var query = new CreateEmployeeCommand(
                new EmployeeName(value.Name),
                new EmployeeGender(value.Gender),
                new EmployeeLastName(value.LastName),
                new EmployeeDateOfBirth(value.DateOfBirth),
                new EmployeePeselNumber(value.PeselNumber));

            await _mediator.Send(query);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modify([FromBody] ExtendedEmployeeDto value)
        {
            var command = new ModifyEmployeeCommand(
                new EmployeeId(value.Id),
                new EmployeeName(value.Name),
                new EmployeeGender(value.Gender),
                new EmployeeLastName(value.LastName),
                new EmployeeDateOfBirth(value.DateOfBirth),
                new EmployeePeselNumber(value.PeselNumber));

            try
            {
                var result = await _mediator.Send(command);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var query = new DeleteEmployeeCommand(new EmployeeId(id));

            try
            {
                var result = await _mediator.Send(query);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}