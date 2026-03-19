namespace ApiDoga.Core;

public class UpdateProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public TypeEnum ProductType { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
