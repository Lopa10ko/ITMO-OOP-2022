using ApplicationTier.DTO;
using ApplicationTier.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationTier.Models.Messages;
using PresentationTier.Models.Reports;

namespace PresentationTier.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReport _reportService;

    public ReportController(IReport reportService)
    {
        _reportService = reportService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("{bossId:guid}/formActivityReport")]
    public async Task<ActionResult<ReportDto>> FormActivityReportAsync(Guid bossId)
    {
        DateTime spanStart = DateTime.MinValue;
        DateTime spanEnd = DateTime.Now;
        var report = await _reportService.GetReportAsync(bossId, spanStart, spanEnd, CancellationToken);
        return Ok(report);
    }

    [HttpGet("{bossId:guid}/getAllReports")]
    public async Task<IEnumerable<ReportInstance>> GetAsync(Guid bossId)
    {
        var reportDtos = await _reportService.GetAllReportsAsync(bossId, CancellationToken);
        return reportDtos.Select(dto => new ReportInstance(dto.Id, dto.EmployeeId, dto.FormedDate, dto.FullText));
    }
}