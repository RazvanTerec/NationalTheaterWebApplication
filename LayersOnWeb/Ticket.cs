using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayersOnWeb
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public int seatRow { get; set; }
        public int seatNumber { get; set; }
        public int ShowId { get; set; }

    }
}
