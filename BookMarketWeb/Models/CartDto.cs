namespace BookMarketWeb.Models;

public class CartDto
{
    public IEnumerable<CartLineDto> Lines = [];
    
    public decimal TotalAmount { get; init; }
}

public class CartLineDto
{
    public BookDto Book { get; set; }
    public int Quantity { get; set; }
    
    public decimal Amount { get; set; }
}