using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prados.Web.Data.Entities
{
    public class Pagostbl
    {
        public int Id { get; set; }

        [Display(Name = "Fecha de Pago")]
        [DataType(DataType.Date)]
        public DateTime PAG_FECHAPAGADO { get; set; }
        public DateTime PAG_FECHACREACION { get; set; }
        public char PAG_ESTADO { get; set; }
        public char PAG_PAGADO { get; set; }
        public Propietariostbl Propietario { get; set; }
        [Display(Name = "Año")]
        public Aniostbl Anio { get; set; }
        [Display(Name = "Mes")]
        public Mesestbl Mes { get; set; }
        [Display(Name = "Valor")]
        public Valorestbl Val { get; set; }
        [Display(Name = "Tipo de Pago")]
        public TiposPagotbl Tipos { get; set; }
        [Display(Name = "Punto de Pago")]
        public PuntosPagotbl PuntodePago { get; set; }

        public List<EstadosFinancierostbl> EstaFinantbls { get; set; }

        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }

        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://prados.azurewebsites.net{ImageUrl.Substring(1)}";

        public int Borr_Id{get; set;}

    }
}
