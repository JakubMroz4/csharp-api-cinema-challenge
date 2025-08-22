﻿namespace api_cinema_challenge.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }
        public int SeatNumber { get; set; }
    }
}
