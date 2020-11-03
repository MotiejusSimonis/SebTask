using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEBtask.Models.Entities
{
    public class Agreement
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
        public long ClientPersonalId { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,4)")]
        public decimal Amount { get; set; }

        [Required]
        public string BaseRateCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,4)")]
        public decimal Margin { get; set; }

        [Required]
        public long Duration { get; set; }
    }
}
