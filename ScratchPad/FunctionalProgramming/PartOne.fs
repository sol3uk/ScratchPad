module PartOne

open System
open System.Globalization

let add x y = x + y

let result = add 3 2

let addN xConstant =
    fun y -> y + xConstant
    
let add10 = add 10
let add20 = add 20
let add30 = add 30

add10 20
add20 20
add30 20


let americanParse str = DateTime.ParseExact(str, "MM/dd/yyyy", CultureInfo.InvariantCulture)
let englishParse str = DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture)

let addNDaysToDate daysToAdd (dateparse: string -> DateTime) datestring =
    (dateparse datestring).AddDays daysToAdd

addNDaysToDate 10 americanParse "5/13/2024"
addNDaysToDate 10 englishParse "13/5/2024"


let timesTwo x = x * 2
let addOne x = x + 1
let divideByTwo x = x / 2

let result2 = 1
            |> addOne
            |> timesTwo
            |> divideByTwo

type Currency =
        | GBP
        | EUR
        | USD
        | PLN
        
let selectedCurrency = Currency.GBP

type MonetaryAmount = {
    Currency: Currency
    Amount: decimal
}

let transaction = { Amount = 20; Currency = GBP }

printfn "%d" result