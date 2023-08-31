using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Data;
using RapidPayAPI.Domain;

namespace RapidPayAPI.Features.Cards
{
    public class CardService : ICardService
    {
        private readonly DataContext _context;

        public CardService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _context.Cards
                .ToListAsync();
        }

        public async Task<Card> GetByNumber(string number)
        {
            return await _context.Cards
                .FirstOrDefaultAsync(x => x.Number == number);
        }

        public async Task<Card> AddCard(Card card)
        {
            await _context.Cards.AddAsync(card);
            return card;
        }

        public async Task<Card> UpdateCard(Card card)
        {
            Card entity = await _context.Cards
                .FirstOrDefaultAsync(x => x.Number == card.Number);
            entity.Balance = card.Balance;
            _context.Cards.Update(entity);
            return card;
        }
    }
}
