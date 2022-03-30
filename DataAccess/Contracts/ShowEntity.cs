using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class ShowEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Distribution { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public TheaterGenre TheaterGenre { get; set; }
        public int NumberofTickets { get; set; }

        public List<TicketEntity> Tickets { get; set; }
    }

    public enum TheaterGenre
    {
        Opera = 1,
        Balet
    }
}
