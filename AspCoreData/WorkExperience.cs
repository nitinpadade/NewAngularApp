using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCoreData
{
    public partial class WorkExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Designation { get; set; }

        public string Organisation { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string JobProfile { get; set; }

        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public JobSeekerProfile JobSeekerProfile { get; set; }


    }
}
