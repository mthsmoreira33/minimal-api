using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace minimal_api.Domain.Entities
{
    public class Veicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = default!;
        [Required]
        [StringLength(100)]
        public string Brand { get; set; } = default!;
        [Required]
        public int Year { get; set; } = default!;
    }
}
