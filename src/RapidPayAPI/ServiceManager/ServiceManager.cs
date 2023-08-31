using RapidPayAPI.Data;
using RapidPayAPI.Features.Cards;
using RapidPayAPI.Features.Payments;

namespace RapidPayAPI.ServiceManager
{
    public class ServiceManager : IServiceManager
    {
        private readonly DataContext _context;

        private ICardService _cardService; 
        private IPaymentService _paymentService;

        public ServiceManager(DataContext context)
        {
            _context = context;
        }

        public ICardService Card
        {
            get
            {
                if (_cardService == null)
                    _cardService = new CardService(_context);

                return _cardService;
            }
        }

        public IPaymentService Payment
        {
            get
            {
                if (_paymentService == null)
                    _paymentService = new PaymentService(_context);

                return _paymentService;
            }
        }


        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
