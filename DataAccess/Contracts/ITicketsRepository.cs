using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface ITicketsRepository
    {
        public List<TicketEntity> GetAllTicketsByShowId(int showId);
        public void DeleteTicketById(int ticketId);
        public TicketEntity UpdateTicketById(int ticketId, TicketViewModel ticket);
        public bool HasAvailableTicket(TicketViewModel ticket, int showId);
        public string BuyTicketAsync(TicketViewModel ticket, int id);
    }
}
