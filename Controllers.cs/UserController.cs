using Microsoft.AspNetCore.Mvc;
using UniversityApi.DTOs;
using UniversityApi.Services;

namespace UniversityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UserController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterUserDto dto, CancellationToken ct)
    {
        var result = await _userService.RegisterAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login([FromBody] LoginUserDto dto, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(dto, ct);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(int id, CancellationToken ct)
    {
        var result = await _userService.GetUserByIdAsync(id, ct);
        return Ok(result);
    }
}
