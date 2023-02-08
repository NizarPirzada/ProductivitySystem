using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityLogs.Data
{
    public class ActivityViewModel
    {
        public int ActivityId { get; set; }
    
        public string ActivityDescription { get; set; }

        public string HowIFeel { get; set; }
   
        public decimal? Duration { get; set; }
     
        public string Value { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
        public DateTime? fromDate { get; set; } 
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
        public DateTime? ToDate { get; set; } 
        public DateTime? Date { get; set; } 

        public string Id { get; set; }
        public int ProjectId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }
        public string ProjectName { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Identity.IdentityUser> AspNetUserList { get; set; }
        public Microsoft.AspNetCore.Identity.IdentityUser AspNetUserLists { get; set; }
        public List<Project> ProjectList { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ToDate < fromDate)
            {
                yield return
                  new ValidationResult(errorMessage: "EndDate must be greater than StartDate",
                                       memberNames: new[] { "EndDate" });
            }
        }
    }
}
