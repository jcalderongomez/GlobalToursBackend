using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class LugarUpdateDto
    {
        [Required]
        public int Id{get; set;}

        [Required]
        public string Nombre {get; set;}

        [Required]
        public string Descripcion {get; set;}

        [Required]
        public double GastoAproximado {get; set;}

        [Required]
        public string ImagenUrl {get; set;}

        [Required]
        public string Pais {get; set;}

        [Required]
        public string  Categoria {get; set;}
    }
}