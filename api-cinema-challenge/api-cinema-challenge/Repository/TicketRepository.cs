using api_cinema_challenge.Data;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_cinema_challenge.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private CinemaContext _db;

        public TicketRepository(CinemaContext db)
        {
            _db = db;
        }

        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            var exists = await _db.Tickets
                .Where(t => t.CustomerId == ticket.CustomerId)
                .Where(t => t.ScreeningId == ticket.ScreeningId)
                .AnyAsync();

            if (exists) return null;

            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();

            return ticket;
        }

        public async Task<Ticket> GetByIdAsync(int customerId, int screeningId)
        {
            var ticket = await _db.Tickets
                .Where(t => t.CustomerId == customerId)
                .Where(t => t.ScreeningId==screeningId)
                .FirstOrDefaultAsync();

            if (ticket is null)
                return null;

            return ticket;
        }
    }
}
