using AutoMapper;
using RapidPayAPI.Domain;

namespace RapidPayAPI.Features.Payments
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Payment, GetPaymentsByCard.PaymentResult>();    

        }
    }
}
