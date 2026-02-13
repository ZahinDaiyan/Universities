using UniversityApi.DTOs;
using System.Threading;
namespace UniversityApi.Services;

public interface IUserService
{
	Task<UserResponseDto> RegisterAsync(RegisterUserDto dto, CancellationToken ct = default);
	Task<UserResponseDto> GetUserByIdAsync(int id, CancellationToken
 ct = default);
}





