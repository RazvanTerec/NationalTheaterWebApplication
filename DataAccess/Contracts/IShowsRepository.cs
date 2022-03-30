using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IShowsRepository
    {
        public void AddShow(ShowViewModel show);
        public IEnumerable<ShowEntity> GetAllShows();
        public ShowEntity GetShowById(int showId);
        public ShowEntity UpdateShowById(int showId, ShowViewModel show);
        public void DeleteShowById(int showId);
    }
}
