using BookMarketWeb.Domain.Entities;
using BookMarketWeb.Infrastructure.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace BookMarketWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Book> _bookRepository;

        public HomeController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext?.Session is not null)
            {
                // Заглушка для фиксации id сессии
                HttpContext.Session.Set("cart", new byte[]{1});
            }

            var books = await _bookRepository.GetAllAsync();
            
            return View(books);
        }
    }
}
