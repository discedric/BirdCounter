using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdCounter.Model
{
    // CountingSession.cs
    public class CountingSession
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public User User { get; set; } // Navigation property
        public IList<BirdCount>? BirdCounts { get; set; } = new List<BirdCount>();
    }
}
