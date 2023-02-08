using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityLogs.Data
{
    public class ActivityLogDTO
    {
        public int ActivityId { get; set; }
        [Required(ErrorMessage = "Name Is Required")]

        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string ActivityDescription { get; set; }
        [Required]
        public string HowIFeel { get; set; }

        [Required]

        public decimal Duration { get; set; }
        [Required]
        public string Value { get; set; }
        public string? Id { get; set; }
        public string? ProjectId { get; set; }
    }

}
