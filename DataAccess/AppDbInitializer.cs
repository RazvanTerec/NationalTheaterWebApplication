using DataAccess.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Shows.Any())
                {
                    context.Shows.AddRange(new List<ShowEntity>()
                    {
                        new ShowEntity()
                        {
                            Title = "Noapte furtunoasa",
                            Distribution = "Ovidiu Crişan, Miron Maxim, Ioan Isaiu",
                            Price = 30.5,
                            Date = DateTime.Now.AddDays(3),
                            TheaterGenre = TheaterGenre.Opera,
                            NumberofTickets = 100

                        },

                        new ShowEntity()
                        {
                            Title = "In Oglinda",
                            Distribution = "Dan Chiorean, Alexandra Tarce, Andreea Șovan",
                            Price = 55.5,
                            Date = DateTime.Now.AddDays(6),
                            TheaterGenre = TheaterGenre.Balet,
                            NumberofTickets = 34
                        },

                        new ShowEntity()
                        {
                            Title = "Tiganiada",
                            Distribution = "Mihai-Florian Nițu, Dragoș Pop, Cristian Rigman, Matei Rotaru, Cosmin Stănilă, Alexandra Tarce, Sânziana Tarța",
                            Price = 25.7,
                            Date = DateTime.Now.AddDays(12),
                            TheaterGenre = TheaterGenre.Opera,
                            NumberofTickets = 77
                        }
               });
                    context.SaveChanges();
                }
            }
        }
    }
}
