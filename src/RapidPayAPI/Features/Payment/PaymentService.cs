using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Data;
using RapidPayAPI.Domain;

namespace RapidPayAPI.Features.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly DataContext _context;

        public PaymentService(DataContext context)
        {
            _context = context;
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByCardAsync(string cardNumber)
        {
            return await _context.Payments
                .Where(x => x.Card.Number == cardNumber)
                .Include(x => x.Card)
                .ToListAsync();
        }

    }
}
