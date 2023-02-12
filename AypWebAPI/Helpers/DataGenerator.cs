using AypWebAPI.Context;
using AypWebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AypWebAPI.Helpers
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()))
            {
                if (context.Players.Any())
                {
                    return;
                }

                context.Teams.AddRange(
                    new Team()
                    {
                        Id = 1,
                        Name = "Dallas"
                    });

                context.Players.AddRange(
                    new Player()
                    {
                        Id = 1,
                        Name = "Ja",
                        Surname = "Morant",
                        BackNumber = 12,
                        TeamId = 1,
                        SavedDate = new DateTime(2001, 12, 12)
                    },
                    new Player()
                    {
                        Id = 2,
                        Name = "Luka",
                        Surname = "Doncic",
                        BackNumber = 77,
                        TeamId = 1,
                        SavedDate = new DateTime(2001, 12, 12)
                    },
                    new Player()
                    {
                        Id = 3,
                        Name = "Vince",
                        Surname = "Carter",
                        BackNumber = 15,
                        TeamId = 1,
                        SavedDate = new DateTime(2001, 12, 12)
                    });

                context.SaveChanges();
            }
        }
    }
}
