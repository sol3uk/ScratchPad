module PartTwo

printfn "Hello from F#"

type Currency =
    | GBP
    | EUR
    | USD
    
type MonetaryAmount = { Amount: decimal; Currency: Currency }

type VoucherEvent =
    | VoucherCreated of startingBalance: MonetaryAmount
    | VoucherDebited of debitAmount: MonetaryAmount
    | VoucherExpired
    
type VoucherState = { Balance: MonetaryAmount; Expired: bool }

let defaultVoucherState = { Balance = { Amount = 0m; Currency = GBP }; Expired = false }
    
type Voucher = Voucher of VoucherEvent list

type VoucherError =
    | NotEnoughBalance
    | CanNotCreateVoucherWithNoBalance

let createVoucher amount currency =
    if amount <= 0m then
        Error CanNotCreateVoucherWithNoBalance
    else 
        Ok (Voucher [ VoucherCreated { Currency = currency; Amount = amount }; ])
    
let applyEvent (state: VoucherState) (event: VoucherEvent) =
    match event with
        | VoucherCreated voucherAmount -> { Balance = voucherAmount; Expired = false }
        | VoucherDebited voucherAmount -> { Balance = { Amount = state.Balance.Amount - voucherAmount.Amount; Currency = state.Balance.Currency }; Expired = false }
        | VoucherExpired -> { Balance = state.Balance; Expired = true }
    
let hydrateVoucher voucherEvents = voucherEvents |> List.fold applyEvent defaultVoucherState 

let debitVoucher amount currency (Voucher voucherEvents) =
    let voucherState = hydrateVoucher voucherEvents
    
    if amount > voucherState.Balance.Amount then
        Error NotEnoughBalance
    else
        Ok (Voucher ( VoucherDebited { Currency = currency; Amount = amount } :: voucherEvents ))
    

