using DataAccess.Contracts;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ShowsRepository : IShowsRepository
    {
        private AppDbContext _context;

        public ShowsRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddShow(ShowViewModel show)
        {
            var _show = new ShowEntity()
            {
                Title = show.Title,
                Distribution = show.Distribution,
                Price = show.Price,
                Date = show.Date,
                TheaterGenre = show.TheaterGenre,
                NumberofTickets = show.NumberofTickets
            };
            _context.Shows.Add(_show);
            _context.SaveChanges();
        }

        public IEnumerable<ShowEntity> GetAllShows()
        {
            return _context.Shows.ToList();
        }
        public ShowEntity GetShowById(int showId)
        {
            return _context.Shows.FirstOrDefault(n => n.Id == showId);
        }

        public ShowEntity UpdateShowById(int showId, ShowViewModel show)
        {
            var _show = _context.Shows.FirstOrDefault(n => n.Id == showId);

            if (_show != null)
            {
                _show.Title = show.Title;
                _show.Distribution = show.Distribution;
                _show.Price = show.Price;
                _show.Date = show.Date;
                _show.TheaterGenre = show.TheaterGenre;
                _show.NumberofTickets = show.NumberofTickets;

                _context.SaveChanges();
            }

            return _show;
        }

        public void DeleteShowById(int showId)
        {
            var _show = _context.Shows.FirstOrDefault(n => n.Id == showId);

            if (_show != null)
            {
                _context.Shows.Remove(_show);
                _context.SaveChanges();
            }
        }
    }
}
