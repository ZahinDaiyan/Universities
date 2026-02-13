using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.DTOs;
using UniversityApi.Models;

namespace UniversityApi.Services;

public class UniversityService : IUniversityService
{
    private readonly AppDbContext _context;

    public UniversityService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UniversityResponseDto>
        CreateUniversityAsync(CreateUniversityDto dto, CancellationToken ct = default)
    {
        var university = new University
        {
            Name = dto.Name
        };

        _context.Universities.Add(university);
        await _context.SaveChangesAsync(ct);

        return new UniversityResponseDto
        {
            Id = university.Id,
            Name = university.Name
        };
    }

    public async Task<UniversityResponseDto>
        UpdateUniversityAsync(int id, UpdateUniversityDto dto, CancellationToken ct = default)
    {
        var university = await _context.Universities
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        if (university is null)
            throw new KeyNotFoundException($"University with id {id} not found.");

        university.Name = dto.Name;

        await _context.SaveChangesAsync(ct);

        return new UniversityResponseDto
        {
            Id = university.Id,
            Name = university.Name
        };
    }

    public async Task<UniversityResponseDto>
        GetUniversityByIdAsync(int id, CancellationToken ct = default)
    {
        var university = await _context.Universities
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Select(u => new UniversityResponseDto
            {
                Id = u.Id,
                Name = u.Name
            })
            .FirstOrDefaultAsync(ct);

        if (university is null)
            throw new KeyNotFoundException($"University with id {id} not found.");

        return university;
    }

    public async Task<bool>
        DeleteUniversityAsync(int id, CancellationToken ct = default)
    {
        var university = await _context.Universities
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        if (university is null)
            return false;

        _context.Universities.Remove(university);
        await _context.SaveChangesAsync(ct);

        return true;
    }

    public async Task<PagedResult<UniversityResponseDto>>
        GetAllUniversitiesAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100);

        var query = _context.Universities
            .AsNoTracking();

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderBy(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UniversityResponseDto
            {
                Id = u.Id,
                Name = u.Name
            })
            .ToListAsync(ct);

        return new PagedResult<UniversityResponseDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = pageSize
        };
    }
}
