using Microsoft.AspNetCore.Mvc;
using UniversityApi.DTOs;
using UniversityApi.Services;

namespace UniversityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }


    [HttpPost]
    public async Task<ActionResult<StudentResponseDto>> Create([FromBody] CreateStudentDto dto, CancellationToken ct)
    {
        var result = await _studentService.CreateStudentAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<StudentResponseDto>> Update(int id, [FromBody] UpdateStudentDto dto, CancellationToken ct)
    {
        var result = await _studentService.UpdateStudentAsync(dto, ct);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<StudentResponseDto>> GetById(int id, CancellationToken ct)
    {
        var result = await _studentService.GetStudentByIdAsync(id, ct);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        var success = await _studentService.DeleteStudentAsync(id, ct);
        if (!success) return NotFound();
        return NoContent();
    }


    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentResponseDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await _studentService.GetAllStudentsAsync(page, pageSize, ct);
        return Ok(result);
    }
}
