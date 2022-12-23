using ApplicationTier.DTO;
using ApplicationTier.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationTier.Models.Employees;

namespace PresentationTier.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployee _employeeService;

    public EmployeeController(IEmployee employeeService)
    {
        _employeeService = employeeService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateAsync([FromBody] CreateEmployeeModel model)
    {
        var employee = await _employeeService.CreateEmployeeAsync(model.Name, CancellationToken);
        return Ok(employee);
    }

    [HttpPost("{employeeId:guid}/checkMessage")]
    public async Task<ActionResult> CheckSpecificMessageAsync(Guid employeeId, Guid messageId)
    {
        await _employeeService.CheckMessageAsync(employeeId, messageId, CancellationToken);
        return Ok();
    }

    [HttpPost("{bossId:guid}/addLedEmployee")]
    public async Task<ActionResult> AddSubordinateEmployeeAsync(Guid bossId, Guid employeeId)
    {
        await _employeeService.AddLedEmployeeAsync(employeeId, bossId, CancellationToken);
        return Ok();
    }

    [HttpPost("{bossId:guid}/formActivityReport")]
    public async Task<ActionResult<string>> AddSubordinateEmployeeAsync(Guid bossId)
    {
        DateTime spanStart = DateTime.MinValue;
        DateTime spanEnd = DateTime.Now;
        string report = await _employeeService.GetReportAsync(bossId, spanStart, spanEnd, CancellationToken);
        return Ok(report);
    }

    [HttpGet(Name = "GetAllEmployeeGuids")]
    public IEnumerable<EmployeeInstance> Get()
        => _employeeService
            .GetAllEmployeeGuids()
            .Result
            .Select(pair => new EmployeeInstance(pair.Key, pair.Value))
            .ToList();
}