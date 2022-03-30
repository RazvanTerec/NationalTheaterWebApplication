using BusinessLayer.Contracts;
using DataAccess.Contracts;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ShowsService : IShowsService
    {
        private IShowsRepository _showsRepository;

        public ShowsService(IShowsRepository repository)
        {
            _showsRepository = repository;
        }
        public void AddShow(ShowViewModel show)
        {
            _showsRepository.AddShow(show);
        }

        public IEnumerable<ShowEntity> GetAllShows()
        {
            return _showsRepository.GetAllShows();

        }
        public ShowEntity GetShowById(int showId)
        {
            return _showsRepository.GetShowById(showId);
        }

        public ShowEntity UpdateShowById(int showId, ShowViewModel show)
        {
            return _showsRepository.UpdateShowById(showId, show);
        }

        public void DeleteShowById(int showId)
        {
            _showsRepository.DeleteShowById(showId);
        }
    }
}
