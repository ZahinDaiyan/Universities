using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;

namespace UniversityApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // --------------------------
    // Login user and return UserResponseDto with token
    // --------------------------
    public async Task<UserResponseDto> LoginAsync(LoginUserDto dto, CancellationToken ct = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == dto.Username, ct);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid username or password.");

        var token = await GenerateJwtTokenAsync(user, ct);

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Username,
            Token = token
        };
    }

    // --------------------------
    // Generate JWT for a user
    // --------------------------
    public Task<string> GenerateJwtTokenAsync(User user, CancellationToken ct = default)
    {
        var secret = _configuration["Jwt:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: creds
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
