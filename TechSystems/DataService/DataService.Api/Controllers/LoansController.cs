using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Interfaces;
using DataService.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Api.Controllers;

[ApiController]
[Route("api/loans")]
[Authorize]
public sealed class LoansController(ILoanService service) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "OfficerOnly")]
    public async Task<ActionResult<PagedResult<LoanDto>>> Get([FromQuery] PageRequest request, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = "OfficerOnly")]
    public async Task<ActionResult<LoanDto>> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "OfficerOnly")]
    public async Task<ActionResult<LoanDto>> Create([FromBody] CreateLoanRequest request, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.LoanId }, result);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "OfficerOnly")]
    public async Task<ActionResult<LoanDto>> Update(long id, [FromBody] UpdateLoanRequest request, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(id, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
