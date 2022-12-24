using ApplicationTier.DTO;
using ApplicationTier.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationTier.Models.Messages;

namespace PresentationTier.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessage _messageService;

    public MessageController(IMessage messageService)
    {
        _messageService = messageService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateAsync([FromBody] CreateMessageModel model)
    {
        var message = await _messageService.CreateMessageAsync(model.Text, model.EmployeeId, CancellationToken);
        return Ok(message);
    }

    [HttpGet("{employeeId:guid}/getAllMessages")]
    public async Task<IEnumerable<MessageInstance>> GetAsync(Guid employeeId)
    {
        var messageDtos = await _messageService.GetAllMessageGuids(employeeId, CancellationToken);
        return messageDtos.Select(dto => new MessageInstance(dto.Id, dto.EmployeeId, dto.State, dto.HandledTime));
    }
}