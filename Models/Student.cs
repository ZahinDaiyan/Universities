namespace UniversityApi.Models;

public class Student 
{
	public int Id {get; set;}
	public readonly string Name {get;set;}

	// Foreign key
	public int UniversityId {get;set;}
	//Navigation
	public University University {get;set;}
}
