using Microsoft.EntityFreameworkCore;
using UniversityApi.Models;

namespace UniversityApi.Data;

public class AppDbContext : DbContext
{
	public AppDbContext ( DbContextOptions<AppDbContext> options) : base {options}

	protcted override void OnModelCreating ( ModelBuilder mb )
	{
		base.OnModelCreating(mb);
	}

	DbSet<University> Universities {get;set;}
	DbSet<Student> Students {get;set;}
	DbSet<User> Users {get;set;}
}
