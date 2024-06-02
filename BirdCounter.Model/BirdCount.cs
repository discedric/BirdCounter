using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BirdCounter.Model
{
    public class BirdCount
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Bird_id { get; set; } // Navigation property
        public int Count { get; set; }
    }
}
