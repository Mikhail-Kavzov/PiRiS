export enum Patterns {
    ClientNames = "^[A-z,А-я,Ё,ё]{1,30}$",
    IdentificationNumber = "^[A-Z,0-9]{14}$",
    Phone = "^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$",
    PassportSeries = "^[A-Z]{2}$",
    PassportNumber = "^[0-9]{7}$"
}
