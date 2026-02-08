namespace StudentAPI.Entity;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Gender { get; set; }
    public int Age { get; set; }
    public int CourseId { get; set; }
    public  Course  Course { get; set; }

    public bool IsDeleted { get; set; } = false;

    public byte[] RowVersion { get; set; }

    public DateTime CreatedAt { get; set; }

}
