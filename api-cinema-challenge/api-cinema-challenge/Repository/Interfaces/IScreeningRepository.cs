using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository.Interfaces
{
    public interface IScreeningRepository
    {
        public Task<Screening> GetByIdAsync(int id);
        public Task<IEnumerable<Screening>> GetAllAsync();
        public Task<Screening> CreateScreening(Screening screening);
    }
}
