using DataAccess.Contracts;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace DataAccess
{
    public class TicketsRepository : ITicketsRepository
    {
        public AppDbContext _context;

        public TicketsRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<TicketEntity> GetAllTicketsByShowId(int showId)
        {
            return _context.Tickets.Where(n => n.ShowId == showId).ToList();
        }

        public void DeleteTicketById(int ticketId)
        {
            var _ticket = _context.Tickets.FirstOrDefault(n => n.Id == ticketId);

            if (_ticket != null)
            {
                _context.Tickets.Remove(_ticket);
                _context.SaveChanges();
            }
        }

        public TicketEntity UpdateTicketById(int ticketId, TicketViewModel ticket)
        {
            var _ticket = _context.Tickets.FirstOrDefault(n => n.Id == ticketId);

            if (_ticket != null)
            {
                _ticket.seatRow = ticket.seatRow;
                _ticket.seatNumber = ticket.seatNumber;

                _context.SaveChanges();
            }

            return _ticket;
        }

        public bool HasAvailableTicket(TicketViewModel ticket, int showId)
        {
            var _movie = _context.Shows.FirstOrDefault(n => n.Id == showId);
            var seat = _context.Tickets.FirstOrDefault(n => n.seatRow == ticket.seatRow && n.seatNumber == ticket.seatNumber && n.ShowId == showId);

            if ((_movie.NumberofTickets > 0) && (seat == null))
            {

                var _ticket = new TicketEntity()
                {
                    seatRow = ticket.seatRow,
                    seatNumber = ticket.seatNumber,
                    ShowId = _movie.Id
                };

                _movie.NumberofTickets--;
                _context.Tickets.Add(_ticket);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public string BuyTicketAsync(TicketViewModel ticket, int showId)
        {

            if (HasAvailableTicket(ticket, showId))
            {
                return "Biletul a fost vandut cu succes";
            }
            else
                return "Nu se poate vinde acest bilet";

        }

    }
}
