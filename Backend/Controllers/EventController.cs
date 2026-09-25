using Backend.Data;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly iRequestRepository repository;

    public EventController(iRequestRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<EventResponse>>> GetAll()
    {
        var events = await repository.GetAll();
        return Ok(events.Select(e => e.ToResponse()).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EventResponse>> GetById(Guid id)
    {
        var @event = await repository.GetById(id);
        if (@event is null) return NotFound();

        return Ok(@event.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<EventResponse>> Create(EventRequest request)
    {
        try
        {
            var created = await repository.Create(request, AppDbContext.DevMemberId);
            return CreatedAtAction(nameof(GetById), new { id = created.Guid }, created.ToResponse());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return Conflict(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EventResponse>> Update(Guid id, EventRequest request)
    {
        try
        {
            var updated = await repository.Update(id, request);
            if (updated is null) return NotFound();

            return Ok(updated.ToResponse());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return Conflict(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<EventResponse>> UpdateStatus(Guid id, StatusRequest request)
    {
        var updated = await repository.UpdateStatus(id, request.Status);
        if (updated is null) return NotFound();

        return Ok(updated.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await repository.Delete(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
