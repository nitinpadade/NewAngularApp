using System;

namespace AspCoreDomainModels.Models.JobSeekerProfile
{
    public class WorkExperienceModel
    {
        public int Id { get; set; } 

        public string Designation { get; set; }

        public string Organisation { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string JobProfile { get; set; }

        public int ProfileId { get; set; }
    }
}
