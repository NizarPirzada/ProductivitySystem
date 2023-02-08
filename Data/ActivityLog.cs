using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityLogs.Data
{
    public class ActivityLog
    {
        [Key]
        public int ActivityId { get; set; }


        public DateTime Date { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "This Field Is Required")]
        public string ActivityDescription { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        public string HowIFeel { get; set; }
       
        [Required(ErrorMessage = "This Field Is Required")]
        [Range(1, 9999, ErrorMessage = "Enter Valid Duration ")]
        public decimal? Duration { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
       
        public string Value { get; set; }
  
        public string? Id { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        public string? ProjectId { get; set; }
    }

}
