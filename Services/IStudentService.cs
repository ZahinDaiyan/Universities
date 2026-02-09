using UniversityApi.DTOs;

namespace UniversityApi.Services;

public interface IStudentService
{
	Task<StudentResponseDto> CreateStudentAsync(CreateStuedentDto dto, CancellationToken ct=default);
	Task<StudentResponseDto> UpdateStudentAsync(UpdateStudentdto dto, CancellationToken ct=default);
	Task<StudentResponseDto> GetStudentByIdAsync(int id, CancellationToken ct=default);
	Task<bool> DeleteStudentAsync(int id , CancellationToken ct=default);

	Task<PagedResult<StudentResponseDto>> GetAllStudentsAsync(int page = 1, int PageSize = 10, CancellationToken ct=defalut);
}


