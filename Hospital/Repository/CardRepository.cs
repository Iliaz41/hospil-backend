using Hospital.Data;
using Hospital.Models;
using Hospital.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CardRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Card entity)
        {
            await _dbContext.Cards.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Card entity)
        {
            _dbContext.Cards.Remove(entity);
            await Save();
        }

        public async Task<List<Card>> GetAll()
        {
            List<Card> cards = await _dbContext.Cards.ToListAsync();
            return cards;
        }

        public async Task<Card> GetById(long id)
        {
            Card card = await _dbContext.Cards.FindAsync(id);
            return card;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Card entity)
        {
            _dbContext.Cards.Update(entity);
            await Save();
        }
    }
}
