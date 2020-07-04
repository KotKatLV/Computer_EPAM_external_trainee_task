using System.ComponentModel.DataAnnotations;

namespace Computer_EPAM_Task.Models
{
    internal class Video
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string URL { get; set; }
    }
}