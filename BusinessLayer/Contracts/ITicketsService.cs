using DataAccess.Contracts;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface ITicketsService
    {
        public List<TicketEntity> GetAllTicketsByShowId(int showId);
        public void DeleteTicketById(int ticketId);
        public TicketEntity UpdateTicketById(int ticketId, TicketViewModel ticket);
        public bool AvailableTicket(TicketViewModel ticket, int showId);
        public string BuyTicket(TicketViewModel ticket, int id);
    }
}
