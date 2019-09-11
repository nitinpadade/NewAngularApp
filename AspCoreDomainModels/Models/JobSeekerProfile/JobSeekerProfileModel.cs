using System.Collections.Generic;

namespace AspCoreDomainModels.Models.JobSeekerProfile
{
    public class JobSeekerProfileModel
    {
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ProfileSummary { get; set; }

        public int UserId { get; set; }

        public string KeySkills { get; set; }

        public string Gender { get; set; }

        public string PermanentAddress { get; set; }

        public string CurrentAddress { get; set; }

        public List<WorkExperienceModel> WorkExperience { get; set; }

    }
}
