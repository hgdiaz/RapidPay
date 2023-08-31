using RapidPayAPI.Domain;

namespace RapidPayAPI.Features.Cards
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();

        Task<Card> GetByNumber(string number);

        Task<Card> AddCard(Card card);

        Task<Card> UpdateCard(Card card);


    }
}
