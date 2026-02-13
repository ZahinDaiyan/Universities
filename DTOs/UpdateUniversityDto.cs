using Microsoft.EntityFrameworkCore.Query.Internal;

namespace UniversityApi.DTOs;

public class UpdateUniversityDto
{
	public string Name { get; set; } = null!;
}
