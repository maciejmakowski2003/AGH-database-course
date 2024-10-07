using System.ComponentModel.DataAnnotations.Schema;

[Table("Customers")]
public class Customer: Company{
    public int CustomerID { get; set; }
    public double Discount { get; set; }
    
    public override string ToString(){
        return $"Customer: {base.ToString()}";
    }

}