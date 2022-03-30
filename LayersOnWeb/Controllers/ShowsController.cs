using BusinessLayer;
using BusinessLayer.Contracts;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        public IShowsService _showsService;

        public ShowsController(IShowsService showsService)
        {
            _showsService = showsService;
        }

        [HttpPost("Add-show")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddShow([FromBody] ShowViewModel show)
        {
            _showsService.AddShow(show);
            return Ok();
        }

        [HttpGet("Get-all-shows")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllShows()
        {
            var allShows = _showsService.GetAllShows();
            return Ok(allShows);
        }

        [HttpGet("Get-shows-by-id/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetShowById(int id)
        {
            var show = _showsService.GetShowById(id);
            return Ok(show);
        }

        [HttpPut("Update-show-by-id/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateShowById(int id, [FromBody] ShowViewModel show)
        {
            var updateShow = _showsService.UpdateShowById(id, show);
            return Ok(updateShow);
        }

        [HttpDelete("Delete-show-by-id/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteShowById(int id)
        {
            _showsService.DeleteShowById(id);
            return Ok();
        }
    }
}

