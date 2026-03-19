using System.ComponentModel.DataAnnotations;

namespace ApiDoga.Core;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; }
    public TypeEnum ProductType { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public DateTime Created { get; set; }

}
