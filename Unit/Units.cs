using StudentAPI.Data;
using StudentAPI.Repository;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace StudentAPI.Unit;

public interface IUnitOfWork
{
    IStudentRepository Students { get; }
    ICourseRepository Courses { get; }
    Task CommitAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;

    public IStudentRepository Students { get; }
    public ICourseRepository Courses { get; }

    public UnitOfWork(AppDbContext db, IStudentRepository students, ICourseRepository courses)
    {
        _db = db;
        Students = students;
        Courses = courses;
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _db.Database.BeginTransactionAsync();
    }
}
