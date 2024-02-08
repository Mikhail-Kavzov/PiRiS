namespace PiRiS.Common.Constants;

public static class Patterns
{
    public const string ClientName = @"^[A-z,А-я,ё,Ё]{1,30}$";
    public const string IdentificationNumber = @"^[0-9]{7}[A-Z]{1}[0-9]{3}[A-Z]{2}[0-9]{1}$";
    public const string Phone = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
    public const string PassportSeries = @"^[A-Z]{2}$";
    public const string PassportNumber = @"^[0-9]{7}$";
    public const string DepositNumber = @"^[0-9]{9}$";
    public const string CreditNumber = @"^[0-9]{9}$";
    public const string Email = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
}
