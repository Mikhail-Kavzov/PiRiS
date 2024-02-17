using PiRiS.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Deposit;

public class DepositPaginationDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Skip should be between {0} and {1}")]
    public int Skip { get; set; }

    [Range(0, 100, ErrorMessage = "Take should be between {0} and {1}")]
    public int Take { get; set; }

    [MaxLength(9)]
    public string DepositNumber { get; set; }
}
