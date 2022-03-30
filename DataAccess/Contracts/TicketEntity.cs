using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class TicketEntity
    {
        [Key]
        public int Id { get; set; }
        public int seatRow { get; set; }
        public int seatNumber { get; set; }
        public int ShowId { get; set; }

    }
}
