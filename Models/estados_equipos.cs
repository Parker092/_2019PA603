using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace _2019PA603WACRUD.Models
{
    public class estados_equipos
    {
        public int id_estados_equipo { get; set; }
        public string descripcion { get; set; } 
        public string estado { get; set; }
    }
}
