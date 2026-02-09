using UniversityApi.DTOs;

namespace UniversityApi.Service;

public interface IUniversityService
{
	public Task<ResponseUniversityDto> CreateUniversityAysnc(CreateUniversityDto dto, CancellationToken ct=default);
	public Task<ResponseUniversityDto> UpdateUniversityAysnc(int id,UpdateUniversityDto dto, CancellationToken ct=default);
	public Task<ResponseUniversityDto> GetUniversityByIdAsync(int id,CancellationToken ct=default);
	public Task<bool> DeleteUniversityAysnc(int id , CancellationToken ct=default);


	public Task<PagedResult<UniversityResponseDto>> GetAllUniversitiesAsync(int page = 1, int PageSize = 10, CancellationToken ct=default);

}





