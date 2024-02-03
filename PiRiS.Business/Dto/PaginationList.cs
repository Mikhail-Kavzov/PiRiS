namespace PiRiS.Business.Dto;

public class PaginationList<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
}
