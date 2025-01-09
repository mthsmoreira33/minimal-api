﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace minimal_api.Dominio.Entidades
{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;
        [Required]
        [StringLength(255)]
        public string Nome { get; set; } = default!;
        [Required]
        [StringLength(100)]
        public string Marca { get; set; } = default!;
        [Required]
        public int Ano  { get; set; } = default!;
    }
}
