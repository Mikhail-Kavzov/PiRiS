﻿using System.ComponentModel.DataAnnotations;

namespace PiRiS.Business.Dto.Account;

public class AccountPaginationDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Skip should be between {1} and {2}")]
    public int Skip { get; set; }

    [Range(0, 100, ErrorMessage = "Take should be between {1} and {2}")]
    public int Take { get; set; }

    [MaxLength(13)]
    public string AccountNumber { get; set; }
}
