using UniversityApi.DTOs;
using UniversityApi.Models;
using System.Threading;

namespace UniversityApi.Services;

public interface IUniversityService
{
	Task<UniversityResponseDto> CreateUniversityAsync(CreateUniversityDto dto, CancellationToken ct = default);
	Task<UniversityResponseDto> UpdateUniversityAsync(int id, UpdateUniversityDto dto, CancellationToken ct = default);
	Task<UniversityResponseDto> GetUniversityByIdAsync(int id, CancellationToken ct = default);
	Task<bool> DeleteUniversityAsync(int id, CancellationToken ct = default);

	Task<PagedResult<UniversityResponseDto>> GetAllUniversitiesAsync(int page = 1, int pageSize = 10, CancellationToken ct = default);
}
