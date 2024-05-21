using BirdCounter.Model;
using Microsoft.EntityFrameworkCore;

namespace BirdCounter.Core
{
	public class BirdDbContext(DbContextOptions<BirdDbContext> options) : DbContext(options)
	{
		public DbSet<Bird> Birds { get; set; }

		public DbSet<CountingSession> CountingSessions { get; set; }

		public void Seed()
		{
			Birds.Add(new Bird { Name = "Blauwe Reiger", Description = "De blauwe reiger is een grote, grijze vogel met lange wit- grijze nek. Hij heeft een zwarte band door het oog, die uitmondt in een kuif, een gele dolkvormige snavel en hoge bruine poten.", ImagePath = "/Images/Blauwe Reiger.jpg" });
			Birds.Add(new Bird { Name = "Boomkruiper", Description = "De boomkruiper is een kleine vogel, die met schokjes langs boomschors naar omhoog klautert, op zoek naar insecten en spinnen.", ImagePath = "/Images/Boomkruiper.jpg" });
			Birds.Add(new Bird { Name = "Ekster", Description = "De ekster is met zijn zwart-wit verenkleed en zeer lange, groenglanzende staart een onmiskenbare vogel. De ekster bouwt vaak overdekte nesten.", ImagePath = "/Images/Ekster.jpg" });
			SaveChanges();
		}
	}
}
