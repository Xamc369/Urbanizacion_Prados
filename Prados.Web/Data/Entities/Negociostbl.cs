using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prados.Web.Data.Entities
{
    public class Negociostbl
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Neg_Nombre { get; set; }
        [Display(Name = "Descripcion")]
        public string Neg_Descripcion { get; set; }


        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Display(Name = "Telefono/Celular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número de telefono es obligatorio")]
        [StringLength(10, MinimumLength = 10,
        ErrorMessage = "El numero de telefono deben tener un minimo de 10 caracteres")]
        public string Neg_Telefono { get; set; }
        [Display(Name = "Direccion")]
        public string Neg_Direccion { get; set; }
        [Display(Name = "Fecha de Creacion")]
        public DateTime Neg_FechaCreacion { get; set; }
        [Display(Name = "Estado")]
        public Char Neg_Estado { get; set; }
        public Propietariostbl Propietarios { get; set; }
        public ICollection<Productostbl> Producto { get; set; }      
        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }
    }
}
