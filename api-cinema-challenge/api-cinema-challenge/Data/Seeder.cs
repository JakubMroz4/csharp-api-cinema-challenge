using api_cinema_challenge.Models;

namespace api_cinema_challenge.Data
{
    public class Seeder
    {
        private List<Customer> _customers = new();
        private List<Movie> _movies = new();
        private List<Screening> _screenings = new();
        private List<Ticket> _tickets = new();

        public void Seed()
        {
            var customer1 = new Customer() { Id = 1, Name = "", Email = "", Phone = "" };
            var customer2 = new Customer() { Id = 2, Name = "", Email = "", Phone = "" };
            var customer3 = new Customer() { Id = 3, Name = "", Email = "", Phone = "" };
            var customer4 = new Customer() { Id = 4, Name = "", Email = "", Phone = "" };
            var customer5 = new Customer() { Id = 5, Name = "", Email = "", Phone = "" };

            var movie1 = new Movie() { Id = 1, Title = "", Rating = "", Description = "", RuntimeMins = 60 };
            var movie2 = new Movie() { Id = 2, Title = "", Rating = "", Description = "", RuntimeMins = 60 };
            var movie3 = new Movie() { Id = 3, Title = "", Rating = "", Description = "", RuntimeMins = 60 };
            var movie4 = new Movie() { Id = 4, Title = "", Rating = "", Description = "", RuntimeMins = 60 };
            var movie5 = new Movie() { Id = 5, Title = "", Rating = "", Description = "", RuntimeMins = 60 };

            var screening1 = new Screening() { Id = 1, MovieId = 1, ScreenNumber = 1, Capacity = 50, StartsAt = DateTime.UtcNow };
            var screening2 = new Screening() { Id = 2, MovieId = 1, ScreenNumber = 1, Capacity = 50, StartsAt = DateTime.UtcNow };

            var ticket1 = new Ticket() { Id = 1, ScreeningId = 1, CustomerId = 1, NumSeats = 1 };
            var ticket2 = new Ticket() { Id = 2, ScreeningId = 1, CustomerId = 2, NumSeats = 1 };
            var ticket3 = new Ticket() { Id = 3, ScreeningId = 1, CustomerId = 3, NumSeats = 1 };

            var ticket4 = new Ticket() { Id = 4, ScreeningId = 2, CustomerId = 1, NumSeats = 1 };
            var ticket5 = new Ticket() { Id = 5, ScreeningId = 2, CustomerId = 2, NumSeats = 1 };
            var ticket6 = new Ticket() { Id = 6, ScreeningId = 2, CustomerId = 3, NumSeats = 1 };

            _customers.Add(customer1);
            _customers.Add(customer2);
            _customers.Add(customer3);
            _customers.Add(customer4);
            _customers.Add(customer5);

            _movies.Add(movie1);
            _movies.Add(movie2);
            _movies.Add(movie3);
            _movies.Add(movie4);
            _movies.Add(movie5);

            _screenings.Add(screening2);
            _screenings.Add(screening2);

            _tickets.Add(ticket1);
            _tickets.Add(ticket2);
            _tickets.Add(ticket3);
            _tickets.Add(ticket4);
            _tickets.Add(ticket5);
            _tickets.Add(ticket6);

        }

        public List<Customer> Customer { get { return _customers; } }
        public List<Movie> Movies { get { return _movies; } }
        public List<Screening> Screenings { get { return _screenings; } }
        public List<Ticket> Tickets { get { return _tickets; } }
    }
}
