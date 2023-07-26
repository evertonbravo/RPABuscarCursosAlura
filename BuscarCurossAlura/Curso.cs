using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscarCurossAlura
{
    
    public class Curso
    {

        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Professor { get; set; }

        public string CargaHoraria { get; set; }
        public string Descricao { get; set; }
        public string PalavraPesquisada { get; set; }
        public DateTime Data { get; set; }
    }
}
