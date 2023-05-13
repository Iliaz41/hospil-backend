using Hospital.Models;

namespace Hospital.Repository.IRepository
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAll();
        Task<Card> GetById(long id);
        Task Create(Card entity);
        Task Update(Card entity);
        Task Delete(Card entity);
        Task Save();
    }
}
