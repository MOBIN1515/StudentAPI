using StudentAPI.DTO;
using StudentAPI.Entity;

namespace StudentAPI.Repository;
 public interface IStudentRepository
 {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);

    Task<(List<Student>, int)> GetPagedAsync(StudentQueryDto query);


}


public interface ICourseRepository
{
    IEnumerable<Course> GetAll();
    Course GetById(int id);
}
