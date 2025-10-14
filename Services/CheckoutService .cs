using RetailMonolith.Models;

namespace RetailMonolith.Services
{
    public class CheckoutService : ICheckoutService
    {
        public Task<Order> CheckoutAsync(string customerId, string paymentToken, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
