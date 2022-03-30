using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public class TicketModel
    {
        public int Id { get; set; }
        public int seatRow { get; set; }
        public int seatNumber { get; set; }
        public int ShowId { get; set; }
    }
}
