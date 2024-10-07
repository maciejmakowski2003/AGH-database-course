class App{
    static void Main(){
        AppContext context = new AppContext();
        var supplier1 = new Supplier { CompanyName = "Lego", Street = "Street1", 
            City = "City1", ZipCode = "ZipCode1", BankAccountNumber = "123"};
        var supplier2 = new Supplier { CompanyName = "Cobi", Street = "Street1", 
            City = "City1", ZipCode = "ZipCode1", BankAccountNumber = "BankAccountNumber1"};
        var customer1 = new Customer { CompanyName = "Customer1", Street = "Street1",
            City = "City1", ZipCode = "ZipCode1", Discount = 20.3};
        
        

        context.Companies.Add(supplier1);
        context.Companies.Add(supplier2);
        context.Companies.Add(customer1);

        context.SaveChanges();

        foreach (var i in context.Companies){
            Console.WriteLine(i);
        }
    }    
}