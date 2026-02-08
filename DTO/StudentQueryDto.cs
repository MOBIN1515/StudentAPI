namespace StudentAPI.DTO;

public class StudentQueryDto
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;

    public string SortBy { get; set; } = "name";
    public bool Desc { get; set; } = false;
}
