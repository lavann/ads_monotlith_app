using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetailMonolith.Data;

namespace RetailMonolith.Pages.Cart
{
    public class IndexModel : PageModel
    {
       
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context) => _context = context;



        //map the cart state in memory without mapping directly to the database
        // Each line represents an item in the cart with its name, quantity, and price
        //is this a good way to represent a cart in memory?
        public List<(string Name, int Quantity, decimal Price)> Lines { get; set; } = new(); 

        public decimal Total => Lines.Sum(line => line.Price * line.Quantity);




    }
}
