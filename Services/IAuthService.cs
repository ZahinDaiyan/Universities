using UniversityApi.DTOs;
using UniversityApi.Models;
namespace UniversityApi.Services;

public interface IAuthService


{
	Task<UserResponseDto> LoginAsync(LoginUserDto dto, CancellationToken ct=default);
	Task<string> GenerateJwtTokenAsync(User user, CancellationToken ct=default);
}

