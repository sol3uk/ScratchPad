module FunctionalProgramming.Program

open PartTwo

let voucher = createVoucher 55 GBP
                |> Result.bind (debitVoucher 50m GBP) 
                |> Result.bind (debitVoucher 5 EUR)
                
match voucher with
    | Ok value -> printfn $"SUCCESS: {value}"
    | Error message -> printfn $"ERROR: {message.GetType}"