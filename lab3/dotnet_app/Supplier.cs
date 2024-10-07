using System.ComponentModel.DataAnnotations.Schema;

[Table("Suppliers")]
public class Supplier: Company{
    public int SupplierID { get; set; }
    public string? BankAccountNumber { get; set; }
    public override string ToString(){
        return $"Supplier: {base.ToString()}";
    }
}