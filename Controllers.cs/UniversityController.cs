using Microsoft.AspNetCore.Mvc;
using UniversityApi.DTOs;
using UniversityApi.Services;

namespace UniversityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityService _universityService;

    public UniversityController(IUniversityService universityService)
    {
        _universityService = universityService;
    }


    [HttpPost]
    public async Task<ActionResult<UniversityResponseDto>> Create([FromBody] CreateUniversityDto dto, CancellationToken ct)
    {
        var result = await _universityService.CreateUniversityAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<UniversityResponseDto>> Update(int id, [FromBody] UpdateUniversityDto dto, CancellationToken ct)
    {
        var result = await _universityService.UpdateUniversityAsync(id, dto, ct);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UniversityResponseDto>> GetById(int id, CancellationToken ct)
    {
        var result = await _universityService.GetUniversityByIdAsync(id, ct);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        var success = await _universityService.DeleteUniversityAsync(id, ct);
        if (!success) return NotFound();
        return NoContent();
    }


    [HttpGet]
    public async Task<ActionResult<PagedResult<UniversityResponseDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await _universityService.GetAllUniversitiesAsync(page, pageSize, ct);
        return Ok(result);
    }
}
