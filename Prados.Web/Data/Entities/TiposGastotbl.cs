using Prados.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prados.Web.Data.Entities
{
    public class TiposGastotbl
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Pago")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "La descripcion debe tener un minimo de 3 caracteres y un maximo de 50")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripcion es obligatoria")]
        public string Tip_Descripcion { get; set; }

        public ICollection<Egresostbl> Egresos { get; set; }
    }
}

