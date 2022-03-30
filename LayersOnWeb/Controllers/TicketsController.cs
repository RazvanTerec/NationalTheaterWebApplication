using BusinessLayer.Contracts;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        public ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        [HttpGet("Get-all-tickets-by-show-id")]
        [Authorize(Roles = "Cashier")]
        public IActionResult GetAllTickets(int showId)
        {
            var alltTckets = _ticketsService.GetAllTicketsByShowId(showId);
            return Ok(alltTckets);
        }

        [HttpPut("Edit-ticket-by-id")]
        [Authorize(Roles = "Cashier")]
        public IActionResult UpdateTicketById(int id, [FromBody] TicketViewModel ticket)
        {
            var updateTicket = _ticketsService.UpdateTicketById(id, ticket);
            return Ok(ticket);
        }

        [HttpDelete("Cancel-reservation")]
        [Authorize(Roles = "Cashier")]
        public IActionResult DeleteTicketById(int id)
        {
            _ticketsService.DeleteTicketById(id);
            return Ok();
        }

        [HttpPost("Buy-ticket")]
        [Authorize(Roles = "Cashier")]
        public string BuyTicket([FromBody] TicketViewModel ticket, int id)
        {
            if (_ticketsService.AvailableTicket(ticket, id))
            {
                return _ticketsService.BuyTicket(ticket, id);
            }
            else
                return "Nu se poate vinde acest bilet";
        }


        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult Export(int showId)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=.;DataBase=Theater_assignment1;Trusted_Connection=True;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("Select * From dbo.Tickets where ShowId='" + showId + "'" + "", connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        string csvData = TransformTableToCsv(ds.Tables[0]);

                        var fileBytes = Encoding.UTF8.GetBytes(csvData);
                        return File(fileBytes, "text/csv", "TicketsData.csv");

                    }
                }
            }
        }

        private string TransformTableToCsv(DataTable dataTable)
        {
            StringBuilder csvBuilder = new StringBuilder();
            IEnumerable<string> columnNames = dataTable.Columns.Cast<DataColumn>()
                .Select(x => x.ColumnName);
            csvBuilder.AppendLine(string.Join(',', columnNames));
            foreach (DataRow row in dataTable.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select
                    (x => string.Concat("\"", x.ToString().Replace("\"", "\"\""), "\""));
                csvBuilder.AppendLine(string.Join(',', fields));
            }
            return csvBuilder.ToString();
        }
    }
}
