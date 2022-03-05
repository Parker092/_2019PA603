using System.ComponentModel.DataAnnotations;
namespace _2019PA603WACRUD.Models
{
    public class marcas
    {
        [Key]
        public int id_marcas { get; set; }
        public string nombre_marcas { get; set; }
        public string estados { get; set; }
    }
}
