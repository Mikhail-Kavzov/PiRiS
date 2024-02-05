using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto;

public class PaginationDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Skip should be between {0} and {1}")]
    public int Skip { get; set; }

    [Range(0, 100, ErrorMessage = "Take should be between {0} and {1}")]
    public int Take { get; set; }
}
