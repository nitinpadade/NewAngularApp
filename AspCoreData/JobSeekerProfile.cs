using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AspCoreData
{
    public partial class JobSeekerProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ProfileSummary { get; set; }

        public int UserId { get; set; }

        public string KeySkills { get; set; }

        public string Gender { get; set; }

        public string PermanentAddress { get; set; }

        public string CurrentAddress { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public IEnumerable<WorkExperience> WorkExperience { get; set; }
    }
}
