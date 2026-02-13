namespace UniversityApi.Models;

public class Student
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;

	// Foreign key
	public int UniversityId { get; set; }

	// Navigation
	public University University { get; set; } = null!;
}
