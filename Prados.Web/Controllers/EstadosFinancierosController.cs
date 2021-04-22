using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prados.Web.Data;
using Prados.Web.Helpers;
using Prados.Web.Models;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Prados.Web.Controllers
{
    public class EstadosFinancierosController : Controller
    {
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IFlashMessage _flashMessage;
        private readonly DataContext _context;

        public EstadosFinancierosController(DataContext context, IImageHelper imageHelper, IConverterHelper converterHelper, IFlashMessage flashMessage)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        private DataContext db = new DataContext();


        public IActionResult Index1(string aniotbl, string mestbl)
        {
            var ingreso = _context.Pagostbls.Where(X => X.Anio.Ani_Descripcion == aniotbl).Where(X => X.Mes.Mes_Descripcion == mestbl)
                .Include(ing => ing.Anio)
                .Include(ing => ing.Mes)
                .Include(ing => ing.Val)
                .Include(ing => ing.PuntodePago)
                .Include(ing => ing.Tipos)
                .ToList();
            var egreso = _context.Egresostbls
                        .Where(X => X.Anio.Ani_Descripcion == aniotbl).Where(X => X.Mes.Mes_Descripcion == mestbl)
                        .Include(egr => egr.TiposG)
                                    .ToList();
            var anio = _context.Aniostbls.ToList();
            var mes = _context.Mesestbls.ToList();

            ViewBag.Aniostbls = (from c in _context.Aniostbls
                               select c.Ani_Descripcion).Distinct();
            ViewBag.Mesestbls = (from c in _context.Mesestbls
                                select c.Mes_Descripcion).Distinct();

            var view = new EstadosFinancierosViewModel()
            {
                ingresos = ingreso,
                egresos = egreso,
                anios = anio,
                meses = mes
            };

            //var items = new List<SelectListItem>();

            //items = _context.Aniostbls.Select(c => new SelectListItem()

            //{

            //    Text = c.Ani_Descripcion,

            //    Value = c.Id.ToString()

            //}).ToList();

            //ViewBag.Aniostbls = items;

            //var items2 = new List<SelectListItem>();

            //items2 = _context.Mesestbls.Select(c => new SelectListItem()

            //{

            //    Text = c.Mes_Descripcion,

            //    Value = c.Id.ToString()

            //}).ToList();

            //ViewBag.Mesestbls = items2;

            return View(view);
        }

        public async Task<IActionResult> Dashboard(string aniotbl, string mestbl)
        {
            var ingreso = _context.Pagostbls.Where(X => X.Anio.Ani_Descripcion == aniotbl).Where(X => X.Mes.Mes_Descripcion == mestbl)
                .Include(ing => ing.Anio)
                .Include(ing => ing.Mes)
                .Include(ing => ing.Val)
                .Include(ing => ing.PuntodePago)
                .Include(ing => ing.Tipos)
                .ToList();
            var egreso = _context.Egresostbls
                        .Where(X => X.Anio.Ani_Descripcion == aniotbl).Where(X => X.Mes.Mes_Descripcion == mestbl)
                        .Include(egr => egr.TiposG)
                        .ToList();
            var anio = _context.Aniostbls.ToList();
            var mes = _context.Mesestbls.ToList();

            ViewBag.Aniostbls = (from c in _context.Aniostbls
                                 select c.Ani_Descripcion).Distinct();
            ViewBag.Mesestbls = (from c in _context.Mesestbls
                                 select c.Mes_Descripcion).Distinct();

            var view = new EstadosFinancierosViewModel()
            {
                ingresos = ingreso,
                egresos = egreso,
                anios = anio,
                meses = mes
            };



            return View(view);
        }

        public IActionResult Index(string aniotbl, int id2)
        {
            var ingreso = _context.Pagostbls.Where(X => X.Anio.Ani_Descripcion == aniotbl)
               .Include(ing => ing.Anio)
               .Include(ing => ing.Mes)
               .Include(ing => ing.Val)
               .Include(ing => ing.PuntodePago)
               .Include(ing => ing.Tipos)
               .ToList();
            var egreso = _context.Egresostbls.Where(X => X.Anio.Ani_Descripcion == aniotbl)
                               .Include(egr => egr.Anio)
                                .Include(egr => egr.Mes)
                                .Include(egr => egr.TiposG)
                                .ToList();
            var anio = _context.Aniostbls.ToList();
            var mes = _context.Mesestbls.ToList();

            ViewBag.Aniostbls = (from c in _context.Aniostbls
                                 select c.Ani_Descripcion).Distinct();
            ViewBag.Mesestbls = (from c in _context.Mesestbls
                                 select c.Mes_Descripcion).Distinct();

            var view = new EstadosFinancierosViewModel()
            {
                ingresos = ingreso,
                egresos = egreso,
                anios = anio,
                meses = mes
            };

            return View(view);
        }

        // GET: Customers/ContactPDF
        public async Task<IActionResult> PDF1(string aniotbl)
        {
            var ingreso = _context.Pagostbls.Where(X => X.Anio.Ani_Descripcion == aniotbl)
              .Include(ing => ing.Anio)
              .Include(ing => ing.Mes)
              .Include(ing => ing.Val)
              .Include(ing => ing.PuntodePago)
              .Include(ing => ing.Tipos)
              .ToList();

            ViewBag.Aniostbls = (from c in _context.Aniostbls
                                 select c.Ani_Descripcion).Distinct();
            ViewBag.Mesestbls = (from c in _context.Mesestbls
                                 select c.Mes_Descripcion).Distinct();

            var view = new EstadosFinancierosViewModel()
            {
                ingresos = ingreso,
            };

            return View(view);
        }

        public async Task<IActionResult> PDF(string aniotbl)
        {
            var ingreso = _context.Pagostbls.Where(X => X.Anio.Ani_Descripcion == aniotbl)
             .Include(ing => ing.Anio)
             .Include(ing => ing.Mes)
             .Include(ing => ing.Val)
             .Include(ing => ing.PuntodePago)
             .Include(ing => ing.Tipos)
             .ToList();

            ViewBag.Aniostbls = (from c in _context.Aniostbls
                                 select c.Ani_Descripcion).Distinct();
            ViewBag.Mesestbls = (from c in _context.Mesestbls
                                 select c.Mes_Descripcion).Distinct();

            var view = new EstadosFinancierosViewModel()
            {
                ingresos = ingreso,
            };
            // return View(await _context.Customers.ToListAsync());
            return new ViewAsPdf("PDF", view)
            {
                // ...
            };
        }

        // ...

    }
}
