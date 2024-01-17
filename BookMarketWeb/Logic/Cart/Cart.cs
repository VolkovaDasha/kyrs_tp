using BookMarketWeb.Domain.Entities;

namespace BookMarketWeb.Logic.Cart;

public class Cart
{
    private readonly List<CartLine> _lineCollection = new List<CartLine>();
    
    public void AddItem(Book book, int quantity)
    {
        CartLine line = _lineCollection
            .FirstOrDefault(g => g.Book.Id == book.Id);

        if (line == null)
        {
            _lineCollection.Add(new CartLine
            {
                Book = book,
                Quantity = quantity
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public void RemoveLine(Book book)
    {
        _lineCollection.RemoveAll(l => l.Book.Id == book.Id);
    }

    public decimal ComputeTotalValue()
    {
        return _lineCollection.Sum(e => e.Book.Price * e.Quantity);

    }

    public IEnumerable<CartLine> Lines => _lineCollection;
}

public class CartLine
{
    public Book Book { get; set; }
    public int Quantity { get; set; }
}