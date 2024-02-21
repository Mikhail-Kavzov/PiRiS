using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto;

public class PaginationDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Skip should be between {1} and {2}")]
    public int Skip { get; set; }

    [Range(0, 100, ErrorMessage = "Take should be between {1} and {2}")]
    public int Take { get; set; }
}
