using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;

namespace UniversityApi.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StudentResponseDto>
        CreateStudentAsync(CreateStudentDto dto, CancellationToken ct = default)
    {
        var universityExists = await _context.Universities
            .AnyAsync(u => u.Id == dto.UniversityId, ct);

        if (!universityExists)
            throw new KeyNotFoundException("University not found.");

        var student = new Student
        {
            Name = dto.Name,
            UniversityId = dto.UniversityId
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync(ct);

        return new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            UniversityId = student.UniversityId
        };
    }

    public async Task<StudentResponseDto>
        UpdateStudentAsync(UpdateStudentDto dto, CancellationToken ct = default)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == dto.Id, ct);

        if (student is null)
            throw new KeyNotFoundException("Student not found.");

        student.Name = dto.Name;

        await _context.SaveChangesAsync(ct);

        return new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            UniversityId = student.UniversityId
        };
    }

    public async Task<StudentResponseDto>
        GetStudentByIdAsync(int id, CancellationToken ct = default)
    {
        var student = await _context.Students
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new StudentResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                UniversityId = s.UniversityId,
                UniversityName = s.University.Name
            })
            .FirstOrDefaultAsync(ct);

        if (student is null)
            throw new KeyNotFoundException("Student not found.");

        return student;
    }

    public async Task<bool>
        DeleteStudentAsync(int id, CancellationToken ct = default)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id, ct);

        if (student is null)
            return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync(ct);

        return true;
    }

    public async Task<PagedResult<StudentResponseDto>>
        GetAllStudentsAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100);

        var query = _context.Students
            .AsNoTracking();

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderBy(s => s.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                UniversityId = s.UniversityId,
                UniversityName = s.University.Name
            })
            .ToListAsync(ct);

        return new PagedResult<StudentResponseDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = pageSize
        };
    }
}
