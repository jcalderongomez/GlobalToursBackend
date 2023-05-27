using Microsoft.Build.Framework;

namespace API.Dtos
{
    public class CategoriaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}
