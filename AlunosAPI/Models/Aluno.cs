using System.ComponentModel.DataAnnotations;

namespace AlunosAPI.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public required string Nome { get; set; }

        [Required]
        [StringLength(80)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public int Idade { get; set; }
    }
}
