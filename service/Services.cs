using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StudentAPI.DTO;
using StudentAPI.Entity;
using StudentAPI.Repository;

namespace StudentAPI.Application.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentResponseDto>> GetAllAsync();
    Task<StudentResponseDto> GetByIdAsync(int id);
    Task<StudentResponseDto> CreateAsync(CreateStudentDto dto);
    Task<StudentResponseDto> UpdateAsync(int id, UpdateStudentDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<StudentResponseDto>> GetPagedAsync(StudentQueryDto query);

}

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public StudentService(IStudentRepository repo, IMapper mapper, IMemoryCache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IEnumerable<StudentResponseDto>> GetAllAsync()
    {
        const string cacheKey = "Students_All";
        if (_cache.TryGetValue(cacheKey, out IEnumerable<StudentResponseDto> cached))
            return cached;

        var students = await _repo.GetAllAsync();
        var result = _mapper.Map<IEnumerable<StudentResponseDto>>(students);

        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<StudentResponseDto> GetByIdAsync(int id)
    {
        string cacheKey = $"Student_{id}";
        if (_cache.TryGetValue(cacheKey, out StudentResponseDto cached))
            return cached;

        var student = await _repo.GetByIdAsync(id);
        if (student == null) return null;

        var result = _mapper.Map<StudentResponseDto>(student);
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<StudentResponseDto> CreateAsync(CreateStudentDto dto)
    {
        var student = _mapper.Map<Student>(dto);
        await _repo.AddAsync(student);
        var result = _mapper.Map<StudentResponseDto>(student);

        _cache.Remove("Students_All");
        return result;
    }

    public async Task<StudentResponseDto> UpdateAsync(int id, UpdateStudentDto dto)
    {
        var student = await _repo.GetByIdAsync(id);
        if (student == null) return null;

        if (dto.Age < 5 || dto.Age > 100)
            throw new ArgumentException("Age must be between 5 and 100");

        student.Name = dto.Name;
        student.Age = dto.Age;
        student.Gender = dto.Gender;
        student.CourseId = dto.CourseId;
        student.RowVersion = dto.RowVersion;

        await _repo.UpdateAsync(student);
        _cache.Remove("Students_All");
        _cache.Remove($"Student_{id}");

        return _mapper.Map<StudentResponseDto>(student);
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _repo.GetByIdAsync(id);
        if (student == null) return;

        await _repo.DeleteAsync(student);
        _cache.Remove("Students_All");
        _cache.Remove($"Student_{id}");
    }
    public async Task<IEnumerable<StudentResponseDto>> GetPagedAsync(StudentQueryDto query)
    {
        if (query.PageNumber <= 0)
            query.PageNumber = 1;

        if (query.PageSize <= 0 || query.PageSize > 50)
            query.PageSize = 10;

        var students = await _repo.GetPagedAsync(query);
        return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
    }

}

