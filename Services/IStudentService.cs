using UniversityApi.DTOs;
using UniversityApi.Models;
using System.Threading;

namespace UniversityApi.Services;

public interface IStudentService
{
	Task<StudentResponseDto> CreateStudentAsync(CreateStudentDto dto, CancellationToken ct = default);
	Task<StudentResponseDto> UpdateStudentAsync(UpdateStudentDto dto, CancellationToken ct = default);
	Task<StudentResponseDto> GetStudentByIdAsync(int id, CancellationToken ct = default);
	Task<bool> DeleteStudentAsync(int id, CancellationToken ct = default);

	Task<PagedResult<StudentResponseDto>> GetAllStudentsAsync(int page = 1, int pageSize = 10, CancellationToken ct = default);
}
