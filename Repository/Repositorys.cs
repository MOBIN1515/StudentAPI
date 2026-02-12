using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.DTO;
using StudentAPI.Entity;

namespace StudentAPI.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students
            .Include(s => s.Course)
            .Where(s => !s.IsDeleted)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
    }

    public async Task DeleteAsync(Student student)
    {
        student.IsDeleted = true;
        _context.Students.Update(student);
    }



public async Task<(List<Student>, int)> GetPagedAsync(StudentQueryDto query)
    {
        IQueryable<Student> students = _context.Students
            .Include(s => s.Course)
            .Where(s => !s.IsDeleted);


        students = query.SortBy switch
        {
            StudentSortBy.Age => query.Desc
                ? students.OrderByDescending(s => s.Age)
                : students.OrderBy(s => s.Age),

            StudentSortBy.Course => query.Desc
                ? students.OrderByDescending(s => s.Course.Title)
                : students.OrderBy(s => s.Course.Title),

            _ => query.Desc
                ? students.OrderByDescending(s => s.Name)
                : students.OrderBy(s => s.Name)
        };

        var totalCount = await students.CountAsync();

        var items = await students
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return (items, totalCount);
    }

}


public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _db;
    public CourseRepository(AppDbContext db) => _db = db;

    public IEnumerable<Course> GetAll() => _db.Course.AsNoTracking().ToList();
    public Course GetById(int id) => _db.Course.Find(id);
}
