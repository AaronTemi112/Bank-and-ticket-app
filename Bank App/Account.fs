module Account

open System
open System.Threading

//records defined
type Customer =
    {
        LegalFirstName: string
        LegalSurname: string
        AccountNumber: string
        mutable Balance: float
        mutable Email: string
    }
//print function
    member this.Print() =
        Console.WriteLine($" First Name: {this.LegalFirstName}")
        Console.WriteLine($" Last Name: {this.LegalSurname}")
        Console.WriteLine($" Account number: {this.AccountNumber}")
        Console.WriteLine($" Your balance is: {this.Balance}")

        Console.WriteLine($" Your email is: {this.Email}")
        Console.WriteLine(" ")
//new function to update a balance
    member this.UpdateBalance newBalance =
        {this with Balance = newBalance} //new balance matched with original 
//withdraw
    member this.Withdraw(amount: float) =
        if amount <= 0.0 then 
            failwith "Withdraw amount must be greater than 0"
        elif amount > this.Balance then
            failwith "Cannot withdraw more than balance"
        else
            this.UpdateBalance(this.Balance - amount)
    // higher than zero
    member this.Deposit(amount: float) = 
        if amount <= 0 then
            failwith "Cannot deposit this amount"
        else 
            this.UpdateBalance(this.Balance + amount)
//function and rules for balance 
let BalancePhrase balance =
    match balance with
    | x when x >= 10.0 && x <= 100.0 -> "Your balance is ok"
    | x when x < 10.0 -> "Your balance is low"
    | _ -> "Your balance is high"

(*let VacantTickets() = 
    
    let mutable tickets = [for n in 1..10 -> {Ticket.seat = n; Ticket.customer = ""}]
    tickets |> List.filter (fun ticket -> ticket.customer = "") |> List.take 10*)

//function for printing customer instances
let run() =
    let aaron = {LegalFirstName = "Aaron"; LegalSurname = "Temiwoluwa"; AccountNumber = "123456"; Balance = 200.0; Email = "aaron@gmail.com"}
    let kim = {LegalFirstName = "Kim"; LegalSurname = "Lee"; AccountNumber = "987654"; Balance = 40405.0; Email = "kim@gmail.com"}
    Console.WriteLine("")
    aaron.Print()
    kim.Print()

    let aaron = aaron.Withdraw(100.0)
    Console.WriteLine("Aaron's Updated Account Info: ")
    aaron.Print()

    let aaron = aaron.Deposit(50.0)
    Console.WriteLine("Aaron's Updated Account Info: ")
    aaron.Print()
    Console.WriteLine("")
//creating 3 instances of customer accounts/details
    let account1 = {LegalFirstName = "Devon"; LegalSurname = "Snipes"; AccountNumber = "0001"; Balance = 0.0; Email = "snipes@gmail.com"}
    let account2 = {LegalFirstName = "Oscar"; LegalSurname = "William"; AccountNumber = "0002"; Balance = 51.0; Email = "wiliam@gmail.com"}
    let account3 = {LegalFirstName = "Anthony"; LegalSurname = "Joshua"; AccountNumber = "0003"; Balance = 5.0; Email = "joshua@gmail.com"}
    let account4 = {LegalFirstName = "William"; LegalSurname = "Regal"; AccountNumber = "0004"; Balance = 0.1; Email = "regal@gmail.com"}
    let account5 = {LegalFirstName = "Daniel"; LegalSurname = "Sturridge"; AccountNumber = "0005"; Balance = 5.9; Email = "sturridge@gmail.com"}
    let account6 = {LegalFirstName = "Marcus"; LegalSurname = "Stanley"; AccountNumber = "0006"; Balance = 65.8; Email = "stanley@gmail.com"}
// the balance check (balancephrase function)
    Console.WriteLine("")
    Console.WriteLine($"Customer: {account1.LegalFirstName} {BalancePhrase account1.Balance}" )
    Console.WriteLine($"Customer: {account2.LegalFirstName} {BalancePhrase account2.Balance}")
    Console.WriteLine($"Customer: {account3.LegalFirstName} {BalancePhrase account3.Balance}")
    Console.WriteLine($"Customer: {account4.LegalFirstName} {BalancePhrase account4.Balance}")
    Console.WriteLine($"Customer: {account5.LegalFirstName} {BalancePhrase account5.Balance}")
    Console.WriteLine($"Customer: {account6.LegalFirstName} {BalancePhrase account6.Balance}")
    Console.WriteLine("")
    account1.Print()
    account2.Print()
    account3.Print()
    account4.Print()
    account5.Print()
    account6.Print()

    
    let accountsList = [account1; account2; account3; account4; account5; account6;]
    let below50, above50 = List.partition(fun acc -> acc.Balance < 50.0) accountsList

    Console.WriteLine("")
    Console.WriteLine("Accounts with a balance below 50: ")
    for item in below50 do 
        Console.WriteLine(item)
        Console.WriteLine("")
    
    Console.WriteLine("")
    Console.WriteLine("Accounts with a balance above 50: ")
    for item in above50 do 
        Console.WriteLine(item)
        Console.WriteLine("")

    
    

type Ticket = {seat:int; customer:string}

let mutable tickets = [for n in 1..10 -> {Ticket.seat = n; Ticket.customer = ""}]
let recordLockObj = new Object() // record locking

let DisplayTickets() = 
    for item in tickets do
        Console.WriteLine($"Customer: {item.customer}, Seat: {item.seat}")
    printfn ""
        
let BookSeats(customerName:string)(customerSeat:int) = 
    lock recordLockObj (fun() ->

    if customerSeat >= 1 && customerSeat <= 10 then 
        let updatedTickets =
            tickets |> List.mapi (fun i ticket ->
                if i = customerSeat - 1 && ticket.customer = "" then  
                    { ticket with customer = customerName}
                else
                    ticket
            )
        if updatedTickets = tickets then
            Console.WriteLine($"Seat: {customerSeat} is already booked")
        else
            Console.WriteLine($"Seat: {customerSeat} booked for {customerName}")
            tickets <- updatedTickets
    else
        Console.WriteLine("Invalid seat")
    )
    Thread.Sleep(3000) // simulate threading


let ticketSystem () =
    DisplayTickets()

    printfn ""
    Console.WriteLine("Enter your name here: ")
    let customerName = Console.ReadLine()
    printfn ""
    Console.WriteLine("Enter a seat number here: ")
    let customerSeat = int(Console.ReadLine())
    printfn ""

    let thread1 = new Thread(fun() -> BookSeats customerName customerSeat)
    thread1.Start()

    let thread2 = new Thread(fun() -> BookSeats "my name" 3)
    thread2.Start()


    thread1.Join()
    thread2.Join()

    DisplayTickets()
