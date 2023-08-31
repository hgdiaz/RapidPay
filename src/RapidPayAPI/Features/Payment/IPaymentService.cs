
using RapidPayAPI.Domain;

namespace RapidPayAPI.Features.Payments
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPaymentsByCardAsync(string cardNumber);

        Task<Payment> AddPaymentAsync(Payment payment);

    }
}
