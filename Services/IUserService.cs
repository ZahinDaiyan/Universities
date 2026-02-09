using UniversityApi.DTOs;

namespace UniversityApi.Services;

public interface IUserService
{
	Task<UserResponseDto> RegisterAsync( RegisterUserDto dto , CancellationToken ct=default);
	Task<UserResponseDto> GetUserByIdAsync (int id, CancellaionToken ct=default);
}





