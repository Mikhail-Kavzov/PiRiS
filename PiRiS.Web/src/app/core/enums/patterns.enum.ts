export enum Patterns {
    ClientNames = "^[A-z,А-я,Ё,ё]{1,30}$",
    IdentificationNumber = "^[0-9]{7}[A-Z]{1}[0-9]{3}[A-Z]{2}[0-9]{1}$",
    Phone = "^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$",
    PassportSeries = "^[A-Z]{2}$",
    PassportNumber = "^[0-9]{7}$",
    DepositNumber = "^\d{9}$",
    CreditNumber = "^\d{9}$"
}
