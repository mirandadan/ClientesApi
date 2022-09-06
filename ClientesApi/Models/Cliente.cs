using System.ComponentModel.DataAnnotations;

namespace ClientesApi.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength (14)]
        public string CPF { get; set; }
       
    }
    
}


