using PiRiS.Business.Enums;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Client;

public class ClientPaginationDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Skip should be between {1} and {2}")]
    public int Skip { get; set; }

    [Range(0, 100, ErrorMessage = "Take should be between {1} and {2}")]
    public int Take { get; set; }

    [MaxLength(30)]
    public string Surname { get; set; }

    public ClientSortField SortField { get; set; } = ClientSortField.Surname;

    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}
