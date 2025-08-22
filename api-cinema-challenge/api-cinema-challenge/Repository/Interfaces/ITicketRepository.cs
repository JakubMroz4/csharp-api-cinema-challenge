using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository.Interfaces
{
    public interface ITicketRepository
    {
        public Task<Screening> GetByIdAsync(int customerId, int screeningId);
        public Task<Screening> CreateScreening(Screening screening);
    }
}
