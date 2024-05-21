using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCounter.Model
{
    public class CountingSession
    {
        public int Id { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public string Location { get; set; }

        public List<BirdCount> BirdCounts { get; set; }
    }

    public class BirdCount
    {
        public int Id { get; set; }

        [Required]
        public Bird Bird { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public CountingSession CountingSession { get; set; }
    }
}
