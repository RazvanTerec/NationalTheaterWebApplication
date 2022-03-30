using DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class ShowViewModel
    {
        public string Title { get; set; }
        public string Distribution { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public TheaterGenre TheaterGenre { get; set; }
        public int NumberofTickets { get; set; }
    }
}
