using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace BirdCounter.Model
{
	public class Bird
	{
		public int Id { get; set; }
		[DisplayName("Name")]
		[Required]
		public String Name { get; set; }
		[DisplayName("Description")]
		public String Description { get; set; }
		[DisplayName("Image Path")]
		public string ImagePath { get; set; }
		[NotMapped]
		public IFormFile ImageFile { get; set; }
	}
}
