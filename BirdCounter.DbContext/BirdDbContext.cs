using BirdCounter.Model;
using Microsoft.EntityFrameworkCore;

namespace BirdCounter.Core
{
	public class BirdDbContext(DbContextOptions<BirdDbContext> options) : DbContext(options)
	{
		public DbSet<Bird> Birds { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<CountingSession> CountingSessions { get; set; }

		public void Seed()
		{
			var birds = new List<Bird>();
			birds.Add(new Bird { Name = "Blauwe Reiger", Description = "De blauwe reiger is een grote, grijze vogel met lange wit- grijze nek. Hij heeft een zwarte band door het oog, die uitmondt in een kuif, een gele dolkvormige snavel en hoge bruine poten.", ImagePath = "/Images/Blauwe Reiger.jpg" });
			birds.Add(new Bird { Name = "Boomkruiper", Description = "De boomkruiper is een kleine vogel, die met schokjes langs boomschors naar omhoog klautert, op zoek naar insecten en spinnen.", ImagePath = "/Images/Boomkruiper.jpg" });
			birds.Add(new Bird { Name = "Ekster", Description = "De ekster is met zijn zwart-wit verenkleed en zeer lange, groenglanzende staart een onmiskenbare vogel. De ekster bouwt vaak overdekte nesten.", ImagePath = "/Images/Ekster.jpg" });
			Birds.AddRange(birds);
            SaveChanges();

            var users = new List<User>();
			users.Add(new User { Id = 1, Name = "Jan" });
			users.Add(new User { Id = 2, Name = "Piet" });
			Users.AddRange(users);
            SaveChanges();

            var countingSessions = new List<CountingSession>();
			var countedbirds = new List<BirdCount>();
			countedbirds.Add(new BirdCount { Bird_id = 1, Count = 3 });
			countedbirds.Add(new BirdCount { Bird_id = 2, Count = 5 });
			countingSessions.Add(new CountingSession { Start = new DateTime(2021, 1, 1, 8, 0, 0), End = new DateTime(2021, 1, 1, 10, 0, 0), Location = "De Hoge Veluwe", User = Users.ToList()[0] , BirdCounts = countedbirds});
			countedbirds = new List<BirdCount>();
			countedbirds.Add(new BirdCount { Bird_id = 2, Count = 2 });
			countedbirds.Add(new BirdCount { Bird_id = 3, Count = 3 });
			countingSessions.Add(new CountingSession { Start = new DateTime(2021, 1, 2, 8, 0, 0), End = new DateTime(2021, 1, 2, 10, 0, 0), Location = "De Hoge Veluwe", User = Users.ToList()[1] , BirdCounts = countedbirds });
			CountingSessions.AddRange(countingSessions);
			SaveChanges();
		}
	}
}
