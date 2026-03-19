namespace ApiDoga.Core;

public class ProductDto
{
    public string ProductName { get; set; }
    public TypeEnum ProductType { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
