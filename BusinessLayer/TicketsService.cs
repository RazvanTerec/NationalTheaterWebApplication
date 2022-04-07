using BusinessLayer.Contracts;
using DataAccess.Contracts;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class TicketsService : ITicketsService
    {
        public ITicketsRepository _ticketsRepository;

        public TicketsService(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public List<TicketEntity> GetAllTicketsByShowId(int showId)
        {
            return _ticketsRepository.GetAllTicketsByShowId(showId);
        }

        public void DeleteTicketById(int ticketId)
        {
            _ticketsRepository.DeleteTicketById(ticketId);
        }

        public TicketEntity UpdateTicketById(int ticketId, TicketViewModel ticket)
        {
            return _ticketsRepository.UpdateTicketById(ticketId, ticket);
        }

        public bool AvailableTicket(TicketViewModel ticket, int showId)
        {
            return _ticketsRepository.HasAvailableTicket(ticket, showId);
        }

        public string BuyTicket(TicketViewModel ticket, int showId)
        {
            _ticketsRepository.BuyTicketAsync(ticket, showId);
            return "Biletul a fost vandut cu succes";

            return _ticketsRepository.BuyTicketAsync(ticket, showId);

        }
    }
}
