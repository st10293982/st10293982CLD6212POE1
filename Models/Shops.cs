namespace st10293982CLD6212POE1.Models
{
    using System.ComponentModel.DataAnnotations;

    
        public class Shops
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [StringLength(500)]
            public string Description { get; set; }

            [Required]
            [Range(0.01, 10000.00)]
            public decimal Price { get; set; }

            [StringLength(255)]
            public string ImageURL { get; set; }
        }
    }


