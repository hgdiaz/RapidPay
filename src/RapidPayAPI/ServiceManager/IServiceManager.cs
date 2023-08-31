
using RapidPayAPI.Features.Cards;
using RapidPayAPI.Features.Payments;

namespace RapidPayAPI.ServiceManager
{
    public interface IServiceManager
    {
        Task SaveAsync();

        ICardService Card { get; }
        IPaymentService Payment { get; }
    }
}
