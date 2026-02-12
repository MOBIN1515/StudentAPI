namespace StudentAPI.DTO;

public class StudentQueryDto
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;

    public StudentSortBy SortBy { get; set; } = StudentSortBy.Name;
    public bool Desc { get; set; } = false;
}
