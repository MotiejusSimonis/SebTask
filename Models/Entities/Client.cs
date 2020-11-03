using System;
using System.ComponentModel.DataAnnotations;

namespace SEBtask.Models.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Timestamp]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Timestamp]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public long PersonalId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
