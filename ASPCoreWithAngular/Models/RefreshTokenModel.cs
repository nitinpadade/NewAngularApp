using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreWithAngular.Models
{
    public class RefreshTokenModel
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
