namespace StudentAPI.DTO;

public class StudentDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; }
}
public class CreateStudentDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public int CourseId { get; set; }
}
public class StudentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string CourseTitle { get; set; }
}


public class UpdateStudentDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public int CourseId { get; set; }
    public byte[] RowVersion { get; set; }
}
public enum Gender
{
    Male,
    FeMale
}