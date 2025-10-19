using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetailMonolith.Data;
using RetailMonolith.Models;
using System.Threading.Tasks;

namespace RetailMonolith.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync() => Products = await _db.Products.Where(p => p.IsActive).ToListAsync();

        public async Task OnPostAsync(int id)
        {
            // Add to cart logic will go here in the future
            var p = await _db.Products.FindAsync(id);
            if (p is null) return;

            var cart = await _db.Carts
                .Include(c => c.Lines)
                .SingleOrDefaultAsync(c => c.CustomerId == "guest")
                ?? new Models.Cart { CustomerId = "guest" };

            if(cart.Id == 0)
            {
                _db.Carts.Add(cart);
            };

            cart.Lines.Add(new CartLine
            {
                Sku = p.Sku,
                Name = p.Name,
                UnitPrice = p.Price,
                Quantity = 1
            });
            await _db.SaveChangesAsync();
            Response.Redirect("/Cart");
        }
    }
}
