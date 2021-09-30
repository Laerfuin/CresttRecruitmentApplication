using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Controllers
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

        [HttpGet("{key}")]
        public async Task<IActionResult> GetByKey([FromRoute] Guid key)
        {
            var query = new GetEmployeeByKeyQuery(key);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto value)
        {
            var query = new CreateEmployeeCommand(value);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Modify([FromBody] ExtendedEmployeeDto value)
        {
            var query = new ModifyEmployeeCommand(value);

            await _mediator.Send(query);

            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute] Guid key)
        {
            var query = new DeleteEmployeeCommand(key);

            await _mediator.Send(query);

            return Ok();
        }
    }
}